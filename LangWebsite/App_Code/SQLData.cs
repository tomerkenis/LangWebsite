using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for SQLCommand
/// </summary>
public class SQLData
{
    SqlDataReader sqldr;
    bool exists;
    Object[] items;
    int readRows;

    DataTable dt;
    public DataTable getTable() { return dt; }
    int currentRow;

    //Double constructors, not the best solution...

    public SQLData(SqlDataReader sqldr, int readRows)
    {
        this.sqldr = sqldr;
        items = new Object[sqldr.FieldCount];
        this.readRows = readRows; //Not used yet, will be implemented in the future || nope, won't be added
        exists = nextRow(); /* SqlDataReader#HasRows does the same, except also goes forward in rows*/
        if (!exists) return;
        for (int i = 0; i < sqldr.FieldCount; i++) items[i] = sqldr.GetValue(i);
    }

    public SQLData(SqlDataAdapter sqlda)
    {
        dt = new DataTable();
        sqlda.Fill(dt);
        exists = dt.Rows.Count > 0;
    }

    public Object[] getRowItems()
    {
        return items;
    }

    [Obsolete("Only works once (in constructor)")]
    public bool nextRow()
    {
        if (sqldr != null)
        {
            return sqldr.Read();
        }
        else if (dt.Rows.Count > currentRow)
        {
            currentRow++;
            return true;
        }
        else return false;
    }

    public String printRow() //I used it for debugging
    {
        if (!exists) return "<font color='red'>null</font>";
        String toReturn = "<font color='green'>";
        if (sqldr != null)
        {  
            for (int i = 0; i < items.Length; i++) toReturn += getRowItems()[i] + " | ";
            toReturn += "</font>";
        } 
        else
        {
            for (int i = 0; i < items.Length; i++) toReturn += dt.Rows[currentRow].ItemArray[i] + " | ";
            toReturn += "</font>";
        }

        return toReturn;
    }



    public bool doesExist() { return exists; }

    //public string printDataTable(string tableName)
    //{
    //    string printStr = "<table" +
    //        //" border='1'" +
    //        " class='" + tableName + "-table'>";
    //    if (sqldr == null)
    //    {
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            printStr += "<tr class='" + tableName + "-row'>";
    //            foreach (object myItemArray in row.ItemArray)
    //            {
    //                printStr += "<td class='" + tableName + "-cell'>" + myItemArray.ToString() + "</td>";
    //            }
    //            printStr += "</tr>";
    //        }
    //        printStr += "</table>";
    //    }
    //    else return "<font color='red'>Error: Can't use DataAdapter for creating a table. Please contact an admin!</font>";

    //    return printStr;
    //}

    //List<int> ids = null;
    //public List<int> getIDs() { return ids; }

    public string printDataTable(Filter[] filters)
    {
        String[] toColor = null;
        String color = null;
        String className = null;
        String formName = null;
        String[] disabled = null;
        String buttonName = null;

        foreach (Filter filter in filters)
            switch (filter.getType())
            {
                case Filter.FilterType.COLOR:
                    toColor = (String[])filter.getValue();
                    color = filter.getSecondValue() != null ? (String)filter.getSecondValue() : "red";
                    break;
                case Filter.FilterType.ADD_CLASSES:
                    className = (String)filter.getValue();
                    break;
                case Filter.FilterType.CHECKBOX:
                    formName = (String)filter.getValue();
                    disabled = (String[])filter.getSecondValue();
                    //ids = new List<int>();
                    break;
                case Filter.FilterType.ADD_BUTTON:
                    buttonName = (String)filter.getValue();
                    break;
            }
        if (buttonName != null && formName == null)
            buttonName = null;

        if (sqldr == null)
        {
            string printStr = formName != null ? "<form method=\"post\">" : "";
            printStr += "<table";
            printStr += className != null ? " class='" + className + "-table'>" : " border='1'>";
            foreach (DataRow row in dt.Rows)
            {
                printStr += "<tr";
                printStr += className != null ? " class='" + className + "-row'>" : ">";
                if (formName != null)
                {
                    printStr += "<td><input type='checkbox' name='" + formName + "' value=\"" + row.ItemArray[0].ToString() + "\"";
                    if (disabled != null && disabled.Contains(row.ItemArray[1].ToString())) printStr += " disabled=\"disabled\"";
                    printStr += "></td>";
                    //ids.Add(int.Parse(row.ItemArray[0].ToString()));
                }
                
                foreach (object myItemArray in row.ItemArray)
                {
                    printStr += "<td";
                    printStr += className != null ? " class='" + className + "-cell' " : "";
                    printStr += toColor != null ? toColor.Contains(myItemArray.ToString()) ? "style='color: " + color + ";'>": ">" : ">";
                    printStr += myItemArray.ToString() + "</td>";
                }
                printStr += "</tr>";
            }
            printStr += "</table>";
            printStr += buttonName != null ? "<input type='submit' name='" + buttonName + "' value='" + textify(buttonName) + "' />" : "";
            printStr += formName != null ? "</form>" : "";
            return printStr;
        }
        else return "<font color='red'>Error: Can't use DataAdapter for creating a table. Please contact an admin!</font>";

    }

    private String textify(string s)
    {
        s = s.ToLower();
        return s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1);
    }

    public class Filter
    {
        //I thought about creating Filter and then creating Instance Classes 
        // (I don't know how it's called, java = extends) or using interfaces but i didn't.
        FilterType ft; public FilterType getType() { return ft; }
        Object value; public Object getValue() { return value; }
        Object value2; public Object getSecondValue() { return value2; }

        public Filter(FilterType ft, Object value)
        {
            this.ft = ft;
            this.value = value;
        }

        public Filter(FilterType ft, Object value, Object value2)
        {
            this.ft = ft;
            this.value = value;
            this.value2 = value2;
        }

        public enum FilterType {
            COLOR, ADD_CLASSES, CHECKBOX, ADD_BUTTON
        }
    }

    //public class ColorFilter : Filter
    //{
    //    Object value2; public Object getSecondValue() { return value2; }
    //    public ColorFilter(Object value, Object value2)
    //    {
    //        base(FilterType.COLOR, value);
    //        this.value2 = value2;
    //    }

    //}


}