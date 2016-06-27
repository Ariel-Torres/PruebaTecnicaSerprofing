using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Descripción breve de DBManager
/// </summary>
public class DBManager
{
    public static DateTime DbNullToDate(object Value)
    { return DbNullToDate(Value, DateTime.MinValue); }
    public static DateTime DbNullToDate(object Value, DateTime defValue)
    {
        if (Value == DBNull.Value || Value == null) return defValue;
        return (DateTime)Value;
    }
    public static bool DbNullToBool(object Value)
    { return DbNullToBool(Value, false); }
    public static bool DbNullToBool(object Value, bool defValue)
    {
        if (Value == DBNull.Value || Value == null) return defValue;
        return (bool)Value;

    }
    public static int DbNullToInt(object Value)
    { return DbNullToInt(Value, 0); }
    public static int DbNullToInt(object Value, int defValue)
    {
        if (Value == DBNull.Value || Value == null) return defValue;
        return (int)Value;
    }
	public DBManager()
	{
		
	}
    static public string SiteURL
    {
        get
        {
            setSiteURL();
            return "http://" + _SiteURL;
        }
    }
    private static string _SiteURL = "";
    static void setSiteURL()
    {
        try
        {
            HttpRequest Request = HttpContext.Current.Request;
            string lpHost = Request.ServerVariables["Server_Name"];//HTTP_HOST
            string lpPort = Request.ServerVariables["SERVER_PORT"];
            if (lpPort != null && lpPort.Trim().Length > 0)
                lpPort = ":" + lpPort;
            _SiteURL = lpHost + lpPort + Request.ApplicationPath;
            if (!_SiteURL.EndsWith("/"))
            { _SiteURL += "/"; }
        }
        catch { _SiteURL = "localhost/FerticaCobros/"; }
    }
    public static IDbConnection GetConnection()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        conn.Open();
        return conn;
    }
    public static IDataReader ExecuteReader(string storedProcName,  IDbConnection Conncetion)
    {
        IDbCommand command = null;
        try
        {
            command = Conncetion.CreateCommand();
            command.CommandText = storedProcName;
            command.CommandType = CommandType.Text;
            IDataReader reader = command.ExecuteReader();
            return reader;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            if (command != null)
            {
                command.Dispose();
                command = null;
            }
        }
    }
    private static string _new_line = null;
    public static string NewLine
    {
        get
        {
            if (_new_line != null) return _new_line;
            _new_line = char.ConvertFromUtf32(13) +
                        char.ConvertFromUtf32(10);
            return _new_line;
        }
    }
    public static object ExecuteScalar(string storedProcName)
    {
        IDbCommand command = null;
        try
        {
            command = GetConnection().CreateCommand();
            command.CommandText = storedProcName;
            command.CommandType = CommandType.Text;
            return command.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            if (command != null)
            {
                command.Dispose();
                command = null;
            }

        }
    }
    public static int ExecuteNonQuery(string CommandText, CommandType CommandType)
    {
        try
        {

            IDbCommand command = GetConnection().CreateCommand();
            command.CommandType = CommandType;
            command.CommandText = CommandText;
            int retValue = command.ExecuteNonQuery();
            command.Dispose();
            command = null;
            return retValue;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return 0;
    }

}
