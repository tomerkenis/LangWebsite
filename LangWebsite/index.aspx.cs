using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    public static String msg = "";
    public static String state = "<font color='blue'>Not signed in!</font>";

    protected void Page_Load(object sender, EventArgs e)
    {
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

    }
}