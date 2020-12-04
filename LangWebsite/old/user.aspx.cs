using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user : System.Web.UI.Page
{
    public static String msg = "";
    public static String state = "<font color='blue'>Not signed in!</font>";
    public static string msgRegister = "";

    public static string usernameCurrent = "";
    public static string emailCurrent = "";
    public static string commentsCurrent = "";
    //public static string ageCurrent = "";
    public static string genderCurrent = "";
    //public static string languagesCurrent = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        state = Session["user"] == null ? "<font color='blue'>Not signed in!</font>" :
            "<font color='blue'>Signed in as: " + Session["user"] + "</font>";

        if (Session["user"] != null)
        {
            SQLData row = (SQLData) new SQLConnection().getData("select * from [Users] where email='" + Session["email"] + "'").end()[0];
            usernameCurrent = (String) row.getRowItems()[1];
            emailCurrent = (String) row.getRowItems()[3];
            commentsCurrent = (String) row.getRowItems()[4];
            //ageCurrent = (String) row.getRowItems()[5];
            genderCurrent = (String) row.getRowItems()[6];
        }

        msgRegister = "";

        if (Request.Form["update"] != null)
        {
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("username", Request.Form["username-new"]);
            map.Add("password", Request.Form["password-new"]);
            map.Add("email", Request.Form["email-new"]);
            map.Add("comments", Request.Form["comments"]);
            map.Add("age", Request.Form["age"]);
            map.Add("gender", Request.Form["gender"]);
            map.Add("languages", Request.Form["languages"]);

            string sql = "update [Users] set [password]=''" + map["password"] + "' where email='" + Session["email"] + "'"; //TODO: update all

            int rowsAffected = (int) new SQLConnection().executeCommand(sql).end()[0];

            if (rowsAffected == 1)
                msgRegister = "<font color='blue'>You successfully updated your profile!</font>";
            else
                msgRegister = "<font color='red'>Update did not succeed.</font>";
            

        }

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

            if (Request.Form["submit"] != null)
            {
                string username = Request.Form["username"];
                MyAdoHelper.DoQuery("Database.mdf", "sql");
                Session["user"] = username;
            }

        }


    }
}