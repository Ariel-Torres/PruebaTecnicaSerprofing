using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
   
    public string _Error = "";
    public string Error() {
        return _Error;
    }
    public string _Mensaje = "";
    public string Mensaje()
    {
        return _Mensaje;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Oficina oficina = new Oficina();
            if (txtid.Value != "")
            {
                oficina.ID = int.Parse(txtid.Value);
            }
            else
            {
                oficina.ID = 0;
            }
            if (!ValidarTelefono(txtTel.Text))
            {
                _Mensaje = "El Teléfono no es Válido, deben ser 8 números";
                divmensaje.Visible = true;
                string script = "mostrarmodal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modal", script, true);
                return;
            }
            oficina.Direccion = txtDireccion.Text;
            oficina.Telefono = txtTel.Text;
            oficina.Descripcion = txtDesc.Text;
            oficina.Nombre = txtNombre.Text;
            if (oficina.Save())
            {
                msjok.Visible = true;
                clean();
                GridView1.DataBind();
            }
            else {
                _Error = oficina._ErrorMessage;
                msjmal.Visible = true;
            }
            
        }
        catch (Exception ex) {
            _Error = ex.Message;
            msjmal.Visible = true;
        }
    }
    protected void EditImage_Clcik(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int id;
        if (int.TryParse(btn.CommandArgument, out id)) {
            Oficina oficina = new Oficina();
            oficina.SelectData(id);
            txtid.Value = id.ToString(); ;
            txtDesc.Text = oficina.Descripcion;
            txtDireccion.Text = oficina.Direccion;
            txtTel.Text = oficina.Telefono;
            txtNombre.Text = oficina.Nombre;
            string script = "mostrarmodal()";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "modal", script, true); 
        }
    }
    protected void Eliminar_Clcik(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int id;
        if (int.TryParse(btn.CommandArgument, out id)) {
            Oficina oficina = new Oficina();
            if (oficina.Delete(id))
            {
                msjok.Visible = true;
            }
            else {
                _Error = oficina._ErrorMessage;
                msjmal.Visible = true;
            }
        }
        
        GridView1.DataBind();
    }
    protected void Unnamed_ServerClick(object sender, EventArgs e)
    {
        clean();
    }
    public void clean() {
        txtNombre.Text = "";
        txtTel.Text = "";
        txtid.Value = "";
        txtDireccion.Text = "";
        txtDesc.Text = "";
        divmensaje.Visible = false;
    }
    public bool ValidarTelefono(string Telefono)
    {
        string patron = "\\b\\d{8}";
        if (Regex.IsMatch(Telefono, patron))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}