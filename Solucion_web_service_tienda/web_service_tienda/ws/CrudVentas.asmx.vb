Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CrudVentas
    Inherits System.Web.Services.WebService

    Public Function Get_ConectionString() As String
        Dim SQLServer_Conection_String = "Data Source=DESKTOP-H08JMC2\SQLEXPRESS; Initial Catalog=tienda; User ID=sa; Password=1234.abcd"
        Return SQLServer_Conection_String
    End Function

    <WebMethod()>
    Public Function InsertarVenta() As String
        Return "Hola a todos"
    End Function

End Class