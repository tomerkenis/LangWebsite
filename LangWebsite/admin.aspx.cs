using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin : System.Web.UI.Page
{
    public static String msg = "";
    public static String state = "<font color='blue'>Not signed in!</font>";
    public static String msgAdmin = "";
    public static String deleteError = "";

    //public List<int> ids;

    protected void Page_Load(object sender, EventArgs e)
    {

        deleteError = "";
        state = Session["user"] == null ? "<font color='blue'>Not signed in!</font>" :
            "<font color='blue'>Signed in as: " + Session["user"] + "</font>";

        loadTable();

        //if (Request.Form["delete-user"] != null)
        //{
            //int id;
            //if (int.TryParse(Request.Form["to-delete"], out id))
            //{
            //    int rowsAffected = (int)new SQLConnection().executeCommand("delete from [Users] where id=" + id).end()[0];
            //    if (rowsAffected > 0)
            //        deleteError = "<font color='blue'>User from '" + id + "' was deleted!</font>";
            //    else
            //        deleteError = "<font color='blue'>User from '" + id + "' does not exist!</font>";
            //    loadTable();
            //}
            //else deleteError = "<font color='red'>'" + Request.Form["to-delete"] + "' is not a number!</font>";
            
        if (Request.Form["delete"] != null)
        {
            String rawIDs = Request.Form["to-delete"];
            if (rawIDs != null)
            {
                String[] toDelete = rawIDs.Split(',');
                if (toDelete.Length > 0)
                {
                    String query = "delete from [Users] where (";
                    Array.ForEach(toDelete, id => query += " id='" + id + "' or ");
                    query = query.Substring(0, query.Length - 3) + ") and (";

                    //Admin proofing
                    Array.ForEach(LocalVars.ADMINS, admin => query += " not username='" + admin + "' and ");
                    query = query.Substring(0, query.Length - 4) + ")";

                    ////Debugging
                    //Response.Write(query);

                    int rowsAffected = (int)new SQLConnection().executeCommand(query).end()[0];
                    if (rowsAffected > 0)
                        deleteError = "<font color='blue'>Users from '" + rawIDs + "' were deleted!</font>";
                    else
                        deleteError = "<font color='blue'>The users don't exist</font>";
                    loadTable();
                }
                else
                {
                    deleteError = "<font color='blue'>Please select a user to delete!</font>";
                }
            }
        }
        //}

        //string name = Request.Form["delete"];
        //string sql = "delete from users where id=" + name;
        //MyAdoHelper.DoQuery("shachar.accdb", sql);
        //msg2 = "<font size=3>the user was deleted</font>";
        //msg = MyAdoHelper.printDataTable("shachar.accdb", "select * from [users]", "");

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
                    loadTable();
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


    public void loadTable()
    {
        if (Session["user"] != null)
        {
            if (LocalVars.ADMINS.Contains(Session["user"].ToString()))
            {
                SQLData data = (SQLData)new SQLConnection().getDataTable("select * from [Users]").end()[0];
                msgAdmin = "User table: <br/>" +
                data.printDataTable(new SQLData.Filter[] {
                        new SQLData.Filter(SQLData.Filter.FilterType.ADD_CLASSES, "admin"),
                        new SQLData.Filter(SQLData.Filter.FilterType.COLOR, LocalVars.ADMINS, "blue"),
                        new SQLData.Filter(SQLData.Filter.FilterType.CHECKBOX, "to-delete", LocalVars.ADMINS),
                        new SQLData.Filter(SQLData.Filter.FilterType.ADD_BUTTON, "delete")
                })
                //+ "<br/>" + "<form method=\"post\"> <input type='text' name='to-delete' id='to - delete' style='margin-bottom:5px;'>" +
                //"<input type='submit' value='Delete User' name='delete-user' id='delete-user' style='padding:5px;'> </form>"
                ;
                //ids = data.getIDs();
            }
            else
            {
                msgAdmin = "<font color='red'>You need to be an admin to view this page!</font>";
            }
        }
        else
        {
            msgAdmin = "<font color='red'>You need to be signed in to view this page!</font>";
        }
    }

}