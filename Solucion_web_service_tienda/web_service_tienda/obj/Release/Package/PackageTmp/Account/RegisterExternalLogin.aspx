<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RegisterExternalLogin.aspx.vb" Inherits="web_service_tienda.RegisterExternalLogin" Async="true" %>

<%@ Import Namespace="web_service_tienda" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <main>
        <h3>Regístrese con su cuenta <%: ProviderName %></h3>
        <asp:PlaceHolder runat="server">
            <div>
                <h4>Formulario de asociación</h4>
                <hr />
                <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                <p class="text-info">
                    You've authenticated with <strong><%: ProviderName %></strong>. Please enter an email below for the current site
                    and click the Log in button.
                </p>

                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="email" CssClass="col-md-2 col-form-label">Correo electrónico</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="email" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="email"
                            Display="Dynamic" CssClass="text-danger" ErrorMessage="Se requiere el correo electrónico" />
                        <asp:ModelErrorMessage runat="server" ModelStateKey="email" CssClass="text-error" />
                    </div>
                </div>

                <div class="row">
                    <div class="offset-md-2 col-md-10">
                        <asp:Button runat="server" Text="Iniciar sesión" CssClass="btn btn-outline-dark" OnClick="LogIn_Click" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>
    </main>
</asp:Content>
