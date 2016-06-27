<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/mycss.css" />
    <script>
        function mostrarmodal() {
            $("#myModal").modal("show");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" Runat="Server">
    <div class="boton">
        <h3 >Oficinas</h3>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div runat="server" id="msjok" visible="false" class="alert alert-success alert-dismissable">
  <button type="button" class="close" data-dismiss="alert">&times;</button>
  <strong>¡Todo se Realizo correctamente!</strong>
</div>
 <div runat="server" id="msjmal" visible="false" class="alert alert-danger alert-dismissable">
  <button type="button" class="close" data-dismiss="alert">&times;</button>
  <strong>Algo no salio bien, intentelo de nuevo </strong> <%=Error() %>
</div>
    <!-- Trigger the modal with a button -->
   <div class="boton">
       <button type="button" class="btn btn-info" runat="server"  data-toggle="modal" data-target="#myModal">Crear Oficina</button>
   </div>
    <div class="boton">
    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" AllowSorting="true"
    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
    CssClass="Grid" DataSourceID="SQLSource" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                     <asp:ImageButton ID="ImageButton1" CausesValidation="false" ImageUrl="~/images/icon-edit.gif" ToolTip="Editar" runat="server" CommandArgument='<%#Eval("ID") %>' OnClick="EditImage_Clcik" />
                     <asp:ImageButton ID="ImageButton2" CausesValidation="false" ImageUrl="~/images/icon-delete.gif" ToolTip="Eliminar" runat="server" CommandArgument='<%#Eval("ID") %>' 
                         OnClientClick="if(!confirm('Esta seguro que desea eliminar?'))return false;" OnClick="Eliminar_Clcik" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                     <%#Eval("Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Descripcion">
                <ItemTemplate>
                     <%#Eval("Descripcion") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Telefono">
                <ItemTemplate>
                     <%#Eval("Telefono") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Direccion">
                <ItemTemplate >
                     <%#Eval("Direccion") %>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
        <EmptyDataTemplate>
            <div style="text-align: center; padding: 25px">
                <b>No se encontraron registros</b>
            </div> 
        </EmptyDataTemplate>
    </asp:GridView>
</div>

    <!-- Modal -->
    <asp:SqlDataSource runat="server" ID="SQLSource" ConnectionString='<%$ ConnectionStrings:SerprofingConnectionString %>' SelectCommand="SELECT [ID],[Nombre], [Descripcion], [Telefono], [Direccion] FROM [Oficina] ORDER BY [ID]"></asp:SqlDataSource>
    <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Crear Nueva Oficina</h4>
      </div>
      <div class="modal-body">
             <div runat="server" id="divmensaje" visible="false" class="alert alert-danger alert-dismissable">
            <button type="button" class="close" data-dismiss="alert">&times;</button>
            <strong><%=Mensaje() %></strong> 
        </div>
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="txtid" Value="" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="texto">
                    Nombre:
                </td>
                <td class="CampoTxt">
                    <asp:TextBox ID="txtNombre" Width="100%" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="Es un campo requerido" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="texto">
                    Descripción: 
                </td>
                <td class="CampoTxt">
                    <asp:TextBox ID="txtDesc" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Es un campo requerido" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="texto">
                    Teléfono:
                </td>
                <td>
                    <asp:TextBox ID="txtTel" Width="100%" runat="server" TextMode="Phone"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Es un campo requerido" ControlToValidate="txtTel"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="texto">
                    Dirección: 
                </td>
                <td class="CampoTxt">
                    <asp:TextBox ID="txtDireccion" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Es un campo requerido" ControlToValidate="txtDireccion"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
      </div>
      <div class="modal-footer">
            <button type="button" class="btn btn-success" runat="server" onserverclick="Button1_Click">Success</button>
            <button type="button" class="btn btn-default"  runat="server" onserverclick="Unnamed_ServerClick" data-dismiss="modal">Close</button>
        
      </div>
    </div>

  </div>
</div>

</asp:Content>

