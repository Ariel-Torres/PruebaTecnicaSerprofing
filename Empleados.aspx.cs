using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Empleados : System.Web.UI.Page
{
    public string _Error = "";
    public string Error1()
    {
        return _Error;
    }
    public string _Mensaje = "";
    public string Mensaje() {
        return _Mensaje;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Empleado empleado = new Empleado();
            if (txtid.Value != "")
            {
                empleado.ID = int.Parse(txtid.Value);
            }
            else
            {
                empleado.ID = 0;
            }
            if (ddOficina.SelectedValue == null || ddOficina.SelectedValue == "") {
                _Mensaje = "Se requiere una oficina para crear al empleado";
                divmensaje.Visible = true;
                string script = "mostrarmodal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modal", script, true);
                return;
            }
            if (!ValidarDui(txtDui.Text)) {
                _Mensaje = "El Dui no es Válido, el formato es 00000000-0";
                divmensaje.Visible = true;
                string script = "mostrarmodal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modal", script, true);
                return;
            }
            if (!ValidarTelefono(txtTel.Text))
            {
                _Mensaje = "El Teléfono no es Válido, deben ser 8 números";
                divmensaje.Visible = true;
                string script = "mostrarmodal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modal", script, true);
                return;
            }
            empleado.Nombre = txtNombre.Text;
            empleado.Direccion = txtDireccion.Text;
            empleado.Telefono = txtTel.Text;
            empleado.Dui = txtDui.Text;
            empleado.Activo = chkactivo.Checked;
            empleado.FechaIngreso = DateTime.Parse(txtfecha.Text);
            empleado._IDoficina = int.Parse(ddOficina.SelectedValue);
            if (empleado.Save())
            {
                msjok.Visible = true;
                clean();
                GridView1.DataBind();
            }
            else
            {
                _Error = empleado._ErrorMessage;
                msjmal.Visible = true;
            }

        }
        catch (Exception ex)
        {
            _Error = ex.Message;
            msjmal.Visible = true;
        }
    }
    protected void EditImage_Clcik(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int id;
        if (int.TryParse(btn.CommandArgument, out id))
        {
            Empleado empleado = new Empleado();
            empleado.SelectData(id);
            txtid.Value = id.ToString(); ;
            txtDireccion.Text = empleado.Direccion;
            txtTel.Text = empleado.Telefono;
            txtNombre.Text = empleado.Nombre;
            txtDui.Text = empleado.Dui;
            txtfecha.Text = empleado.FechaIngreso.ToString();
            ddOficina.SelectedValue = empleado._IDoficina.ToString() ;
            chkactivo.Checked = empleado.Activo;
            txtfecha.Text = empleado.FechaIngreso.ToString("yyyy-MM-dd");

            string script = "mostrarmodal()";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "modal", script, true);
        }
    }
    protected void Eliminar_Clcik(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int id;
        if (int.TryParse(btn.CommandArgument, out id))
        {
            Empleado empleado = new Empleado();
            if (empleado.Delete(id))
            {
                msjok.Visible = true;
            }
            else
            {
                string script = "alert('" + empleado._ErrorMessage + "')";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modal", script, true);
            }
        }

        GridView1.DataBind();
    }
    protected string GetDate(object date)
    {
        if (date == null)
            return "";
        else
        {
            if (date.ToString() == "") return "";
            DateTime dt = (DateTime)date;
            if (dt.ToShortDateString() == "1/1/1900") return "";
            return dt.ToShortDateString();
        }
    }
    protected string GetActive(object active) {
        bool activo = (bool)active;
        if (activo)
        {
            return "Activo";
        }
        else {
            return "Inactivo";
        }
    }

    protected void Cambiar(object sender, EventArgs e)
    {
        clean();
    }
    public void clean() {
        txtNombre.Text = "";
        txtTel.Text = "";
        txtid.Value = "";
        txtDireccion.Text = "";
        txtfecha.Text = "";
        txtDui.Text = "";
        txtfecha.Text = "";
        divmensaje.Visible = false;
    }

    public bool ValidarDui(string DUI) {
        string patron = "\\b\\d{8}[-]?\\d";
        if (Regex.IsMatch(DUI, patron) && DUI.Length == 10)
        {
            return true;
        }
        else {
            return false;
        }
    }
    public bool  ValidarTelefono(string Telefono){
        string patron = "\\b\\d{8}";
        if (Regex.IsMatch(Telefono, patron))
        {
            return true;
        }
        else {
            return false;
        }
    }
}