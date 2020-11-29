using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class board : System.Web.UI.Page
{
    public static String msg = "";
    public static String state = "<font color='blue'>Not signed in!</font>";
    public static string posts = "";
    Dictionary<int, String> used = new Dictionary<int, String>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["send"] != null)
        {
            //Not gonna implement all ASCII
            String text = Request.Form["send-text"].ToString()
                .Replace("'", "&#39;");
            String date = (String) DateTime.Now.ToString().Clone();
            
            if (Session["user"] != null && text.Length > 0)
            {
                new SQLConnection().executeCommand("insert into [Board] (message,date,sender) values" +
                    " ('" + date + "','" + text + "','" + Session["user"] + "')").end();
                Response.Redirect(Request.RawUrl);
            }
        }

        state = Session["user"] == null ? "<font color='blue'>Not signed in!</font>" :
            "<font color='blue'>Signed in as: " + Session["user"] + "</font>";

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

        if (Session["user"] != null)
        {
            posts = "<form method=\"post\">";
            foreach (DataRow row in ((SQLData) new SQLConnection().getDataTable("select * from [Board]").end()[0]).getTable().Rows)
            {
                int i = (int)row.ItemArray[0];
                String msg = ((String)row.ItemArray[2])
                    .Replace("&#39;", "'");
                String date = ((String)row.ItemArray[1]);
                String username = ((String)row.ItemArray[3]);

                if (Session["user"].ToString() == username || LocalVars.ADMINS.Contains(Session["user"].ToString()))
                    used.Add(i, username);

                posts += "<table class='post-table'>";
                posts += "<tr><td class='data-cell'>" + date + " | " + username + "</td></tr>";
                posts += "<tr><td class='message-cell'>" + msg;
                if (Session["user"].ToString() == username || LocalVars.ADMINS.Contains(Session["user"].ToString()))
                    posts += "<input type=\"submit\" value=\"X\" name=\"delete-" + i + "\" style=\"float: right; margin-top: 10px;\" />";
                posts += "</td></tr>";

                posts += "</table><br/>";
            }
            posts += "</form><div class='buffer'></div>";
        }
        else
            posts = "<font color='red'>You need to sign in to view this page!</font>";

        foreach (int i in used.Keys)
        {
            if (Request.Form["delete-" + i] != null)
            {
                int rowsAffected = (int) new SQLConnection().executeCommand("delete from [Board] where id=\'" + i + "\' and sender=\'" + used[i] + "\'").end()[0];
                if (rowsAffected != 1) posts += "<font color='red'>An error occurred, please report to an admin!</font>";
                else Response.Redirect(Request.RawUrl);
            }
        }
    }
}