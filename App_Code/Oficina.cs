using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Oficina
/// </summary>
public class Oficina
{
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

    protected string _Direccion = "";
    public virtual string Direccion
    {
        get { return _Direccion; }
        set { _Direccion = value; }
    }
    protected string _Telefono = "";
    public virtual string Telefono
    {
        get { return _Telefono; }
        set { _Telefono = value; }
    }
    protected string _Descripcion = "";
    public virtual string Descripcion
    {
        get { return _Descripcion; }
        set { _Descripcion = value; }
    }
	public Oficina()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public void SelectData(int ID) {
        try
        {
            string sql = "Select * from Oficina where id = " + ID;
          IDataReader reader =   DBManager.ExecuteReader(sql, DBManager.GetConnection());
          if (reader.Read()) {
              Direccion =  reader["Direccion"].ToString();
              Telefono = reader["Telefono"].ToString();
              Descripcion = reader["Descripcion"].ToString();
              Nombre = reader["Nombre"].ToString();
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
                sql = string.Format(@"Insert into Oficina (Nombre,Descripcion,Telefono,Direccion) Values
                ('"+Nombre+"','" +Descripcion+ "','" + Telefono + "','" + Direccion + "');");

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
                sql = "Update Oficina set Nombre= '"+Nombre+"', Descripcion = '" + Descripcion + "', Telefono = '" + Telefono + "',Direccion =  '" + Direccion + "' where id=" + ID;
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
            //Verificar si hay empleados asignados a la oficina
            string sql = "Select Count(*) as cantidad from Oficina a , Empleado b where b.id_oficina=a.id";
            IDataReader reader = DBManager.ExecuteReader(sql, DBManager.GetConnection());
            if (reader.Read()) {
                if (int.Parse(reader["cantidad"].ToString()) > 0)
                {
                    //Lanzar error, existen empleados en esa ofician
                    value = false;
                    _ErrorMessage = "Existen Empleados en esa Oficina";
                }
                else { 
                    //no hay empleados en la oficina
                    sql = "";
                    sql = "Delete from Oficina where id = " + ID;
                    if (DBManager.ExecuteNonQuery(sql, CommandType.Text) > 0)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
                    }
                }
            }
        }
        catch (Exception ex) { 
            
        }
        return value;
    }

}