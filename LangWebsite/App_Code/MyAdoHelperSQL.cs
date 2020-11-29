using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for MyAdoHelper
/// פעולות עזר לשימוש במסד נתונים  מסוג 
/// SQL SERVER
///  App_Data המסד ממוקם בתקיה 
/// </summary>

public class MyAdoHelper
{
    private const String dbFileName = "~/App_Data/Database.mdf";
    public MyAdoHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static SqlConnection ConnectToDb(string fileName)
    {
        string path = HttpContext.Current.Server.MapPath(dbFileName);//מיקום מסד בפורוייקט
        //path += fileName;
        //string path = HttpContext.Current.Server.MapPath("App_Data/" + fileName);//מאתר את מיקום מסד הנתונים מהשורש ועד התקייה בה ממוקם המסד
        string connString = string.Format(@"server=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True", path);


        SqlConnection conn = new SqlConnection(connString);
        return conn;

    }

    /// <summary>
    /// To Execute update / insert / delete queries
    ///  הפעולה מקבלת שם קובץ ומשפט לביצוע ומבצעת את הפעולה על המסד
    /// </summary>

    public static void DoQuery(string fileName, string sql)//הפעולה מקבלת שם מסד נתונים ומחרוזת מחיקה/ הוספה/ עדכון
    //ומבצעת את הפקודה על המסד הפיזי
    {

        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        com.ExecuteNonQuery();
        com.Dispose();
        conn.Close();

    }


    /// <summary>
    /// To Execute update / insert / delete queries
    ///  הפעולה מקבלת שם קובץ ומשפט לביצוע ומחזירה את מספר השורות שהושפעו מביצוע הפעולה
    /// </summary>
    public static int RowsAffected(string fileName, string sql)//הפעולה מקבלת מסלול מסד נתונים ופקודת עדכון
    //ומבצעת את הפקודה על המסד הפיזי
    {

        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        int rowsA = com.ExecuteNonQuery();
        conn.Close();
        return rowsA;
    }

    /// <summary>
    /// הפעולה מקבלת שם קובץ ומשפט לחיפוש ערך - מחזירה אמת אם הערך נמצא ושקר אחרת
    /// </summary>
    public static bool IsExist(string fileName, string sql)//הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה אמת אם הנתונים קיימים ושקר אחרת
    {

        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        SqlDataReader data = com.ExecuteReader();
        bool found;
        found = (bool) data.Read();// אם יש נתונים לקריאה יושם אמת אחרת שקר - הערך קיים במסד הנתונים
        conn.Close();
        return found;

    }


    public static DataTable ExecuteDataTable(string fileName, string sql)
    {
        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlDataAdapter tableAdapter = new SqlDataAdapter(sql, conn);
        DataTable dt = new DataTable();
        tableAdapter.Fill(dt);
        return dt;
    }


    public static void ExecuteNonQuery(string fileName, string sql)
    {
        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlCommand command = new SqlCommand(sql, conn);
        command.ExecuteNonQuery();
        conn.Close();
    }

    public static string printDataTable(string fileName, string sql, string tableName)//הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה טבלא
    {


        DataTable dt = ExecuteDataTable(fileName, sql);

        string printStr = "<table border='1' class='" + tableName + "-table'>";

        foreach (DataRow row in dt.Rows)
        {
            printStr += "<tr class='" + tableName + "-row'>";
            foreach (object myItemArray in row.ItemArray)
            {
                printStr += "<td class='" + tableName + "-cell'>" + myItemArray.ToString() + "</td>";
            }
            printStr += "</tr>";
        }
        printStr += "</table>";

        return printStr;
    }

    //runs a select query and returns a string array - this should only be used if the select returns 1 row
    //e.g selecting a specific user with a unique email/username etc.
    public static string[] stringDataTable(string fileName, string sql)
    {
        DataTable dt = ExecuteDataTable(fileName, sql);
        string[] result = new string[0];


        foreach (DataRow row in dt.Rows)
        {
            result = new string[row.ItemArray.Length];
            int i = 0;

            foreach (object myItemArray in row.ItemArray)
            {

                result[i] = myItemArray.ToString();
                i++;
            }

        }

        return result;
    }

    public static string[,] fullDataTable(string fileName, string sql) // returns a 2D string array respresenting a select query result. 
    {
        DataTable dt = ExecuteDataTable(fileName, sql);
        string[,] result = new string[0, 0];
        int i = 0;
        result = new string[dt.Rows.Count, dt.Columns.Count];


        foreach (DataRow row in dt.Rows)
        {
            int j = 0;

            foreach (object myItemArray in row.ItemArray)
            {

                result[i, j] = myItemArray.ToString();
                j++;
            }
            i++;
        }

        return result;
    }

}