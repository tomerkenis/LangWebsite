using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page
{

    public static String msg = "";
    public static String state = "<font color='blue'>Not signed in!</font>";
    public static string msgRegister = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        msgRegister = "";

        state = Session["user"] == null ? "<font color='blue'>Not signed in!</font>" :
            "<font color='blue'>Signed in as: " + Session["user"] + "</font>";

        //Response.Write(Request.Form["username"]);
        //MyAdoHelper.DoQuery("Database.mdf", "insert into [Users] (username,[password],email,comments,age,gender,languages) " +
        //    "values('TomerKenis', 'tomer123', 'tomer.kenis@gmail.com', 'Holla niños!', '14', 'male', 'spanish, italian, arabic')");
        //MyAdoHelper.DoQuery("Database.mdf", "insert into [Users] (email,[password],age,option,city,knowledge) values ('matankenis@gmail.com','matan12','15','something','hadera','alot')");
        //const string filename = "Database.mdf";

        if (Request.Form["login"] != null)
        {
            if (Session["user"] == null)
            {
                String email = Request.Form["email"];
                String password = Request.Form["password"];

                String sql = "select * from [Users] where [password]='" + password + "' and [email]='" + email + "'";

                SQLData row = (SQLData)new SQLConnection().getData(sql).end()[0];
                if (row.doesExist())
                {
                    Session["user"] = (String)row.getRowItems()[1];
                    Session["email"] = (String)row.getRowItems()[3];
                    msg = "";
                    //"<font color='blue'> Login successful </font>";
                    state = "<font color='blue'>Signed in as: " + Session["user"] + "</font>";
                }
                else msg = "<font color='red'>The email doesn't match password.</font>";
            }
            else
            {
                Session.Abandon();
                state = "<font color='blue'>Not signed in!</font>";
                Response.Redirect(Request.RawUrl);
            }
        }

        if (Request.Form["sign-up"] != null)
        {
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("username", Request.Form["username-new"]);
            map.Add("password", Request.Form["password-new"]);
            map.Add("email", Request.Form["email-new"]);
            map.Add("comments", Request.Form["comments"]);
            map.Add("age", Request.Form["age"]);
            map.Add("gender", Request.Form["gender"]);
            map.Add("languages", Request.Form["languages"]);

            string sql = "insert into [Users] (";
            foreach (string s in map.Keys) { sql += "[" + s + "],"; }
            sql = sql.Substring(0, sql.Length - 1) + ") values (";

            foreach (string s in map.Values) { sql += "'" + s + "',"; }
            sql = sql.Substring(0, sql.Length - 1) + ")";

            //USERNAME doesn't check
            string sqlselect = "select * from [Users] where username = '" + map["username"] + "'" + " or email = '" + map["email"] + "'";
            SQLData row = (SQLData)new SQLConnection().getData(sqlselect).end()[0];
            if (!row.doesExist())
            {
                new SQLConnection().executeCommand(sql).end();
                msgRegister = "<font color='lawngreen'>You successfully registered!</font>";
                Session["user"] = map["username"];
                Session["email"] = map["email"];
                state = "<font color='blue'>Signed in as: " + Session["user"] + "</font>";
            }
            else msgRegister = "<font color='red'>Username or email already taken. Choose another one.</font>";

        }
        

    }
}