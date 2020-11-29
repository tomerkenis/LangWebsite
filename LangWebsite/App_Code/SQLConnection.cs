using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SQLConnection
/// </summary>
public class SQLConnection
{
    private const String dbFileName = "Database.mdf";
    SqlConnection con;
    List<Object> answers;

    public SQLConnection()
    {
        string path = HttpContext.Current.Server.MapPath("~/App_Data/" + dbFileName);
        string conCmd = string.Format(@"server=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True", path);
        answers = new List<Object>();
        con = new SqlConnection(conCmd);
        con.Open();
    }

    public List<Object> end() { con.Close(); return answers; }

    //public List<Object> tempEnd() { 
    //    //con.Close(); 
    //    return answers; 
    //}

    public SQLConnection executeCommand(string sql)
    {
        SqlCommand com = new SqlCommand(sql, con);
        answers.Add(com.ExecuteNonQuery());
        com.Dispose();
        return this;
    }

    public SQLConnection getData(string sql)
    {
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader data = cmd.ExecuteReader();
        answers.Add(new SQLData(data, 1));
        return this;
    }

    public SQLConnection getDataTable(string sql)
    {
        SqlDataAdapter ta = new SqlDataAdapter(sql, con);
        answers.Add(new SQLData(ta));
        return this;
    }


}