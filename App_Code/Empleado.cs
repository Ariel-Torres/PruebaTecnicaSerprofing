using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Empleado
/// </summary>
public class Empleado
{
	public Empleado()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
     public string _ErrorMessage = "";
    protected IDbConnection _DbConnection = null;
    public virtual IDbConnection DbConnection
    {
        get { return _DbConnection; }
        set { _DbConnection = value; }
    }
    protected int _ID = 0;
    public virtual int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    protected string _Nombre = "";
    public virtual string Nombre
    {
        get { return _Nombre; }
        set { _Nombre = value; }
    }

    protected string _Dui = "";
    public virtual string Dui
    {
        get { return _Dui; }
        set { _Dui = value; }
    }
    protected string _Telefono = "";
    public virtual string Telefono
    {
        get { return _Telefono; }
        set { _Telefono = value; }
    }
    protected string _Direccion = "";
    public virtual string Direccion
    {
        get { return _Direccion; }
        set { _Direccion = value; }
    }
    protected DateTime _FechaIngreso;
    public virtual DateTime FechaIngreso
    {
        get { return _FechaIngreso; }
        set { _FechaIngreso = value; }
    }
    protected bool _Activo;
    public virtual bool Activo
    {
        get { return _Activo; }
        set { _Activo = value; }
    }
    public int _IDoficina;
    public void SelectData(int ID) {
        try
        {
            string sql = "Select * from Empleado where id = " + ID;
          IDataReader reader =   DBManager.ExecuteReader(sql, DBManager.GetConnection());
          if (reader.Read()) {
              Nombre = reader["Nombre"].ToString();
              Dui = reader["Dui"].ToString();
              Direccion =  reader["Direccion"].ToString();
              Telefono = reader["Telefono"].ToString();
              FechaIngreso = DBManager.DbNullToDate(reader["Fecha_Ingreso"]);
              Activo = DBManager.DbNullToBool(reader["Activo"]);
              _IDoficina = DBManager.DbNullToInt(reader["ID_Oficina"]);
              
          }
        }
        catch (Exception ex) { }
    }

    public bool Save()
    {
        bool CloseConnection = false;
        bool retValue;
        string sql = "";
        try
        {
            if (DbConnection == null)
            {
                CloseConnection = true;
                DbConnection = DBManager.GetConnection();
            }

            if (ID == 0)
            {
                sql = string.Format(@"INSERT INTO [dbo].[Empleado]
           ([Nombre]
           ,[Dui]
           ,[Direccion]
           ,[Telefono]
           ,[Fecha_Ingreso]
           ,[Activo]
           ,[ID_Oficina])
     VALUES
           ('" + Nombre + "', '" + Dui + "','" + Direccion + "', '" + Telefono + "',CONVERT(datetime,'"+FechaIngreso+"',103),'" + Activo + "'," + _IDoficina + ")");

                sql += DBManager.NewLine + " SELECT SCOPE_IDENTITY()";
                object retVal = DBManager.ExecuteScalar(sql);
                if (retVal != DBNull.Value && retVal != null)
                {
                    this.ID = int.Parse(retVal.ToString());
                    retValue = true;
                }
                else
                    retValue = false;

            }
            else
            {
            sql = "UPDATE [dbo].[Empleado]"+
                  "SET [Nombre] = '"+Nombre+"' "+
                  ",[Dui] = '"+Dui+"'"+
                  ",[Direccion] = '"+Direccion+"'"+
                  ",[Telefono] = '"+Telefono+"'"+
                  ",[Fecha_Ingreso] = CONVERT(datetime,'" + FechaIngreso + "',103)" +
                  ",[Activo] = '"+Activo+"' "+
                  ",[ID_Oficina] = "+_IDoficina+" "+
                    "WHERE ID = " + ID;
                if (DBManager.ExecuteNonQuery(sql, CommandType.Text) > 0)
                {
                    retValue = true;
                }
                else
                {
                    retValue = false;
                }

            }

        }
        catch (Exception ex)
        {
            retValue = false;
            _ErrorMessage = ex.Message;
        }
        if (DbConnection != null && CloseConnection)
        {
            if (DbConnection.State == ConnectionState.Open)
                DbConnection.Close();
            DbConnection.Dispose();
            DbConnection = null;
        }
        return retValue;
    }
    public bool Delete(int ID) {
        bool value = false;
        try
        {
             string sql = "";
             sql = "Delete from Empleado where id = " + ID;
                    if (DBManager.ExecuteNonQuery(sql, CommandType.Text) > 0)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
                    }
                
        }
        catch (Exception ex) { 
            
        }
        return value;
    }

}