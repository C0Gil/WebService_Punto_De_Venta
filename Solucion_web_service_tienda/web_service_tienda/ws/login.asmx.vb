Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.Services.Protocols

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class login1
    Inherits System.Web.Services.WebService
    Public Function Get_ConectionString() As String
        Dim SQLServer_Conection_String As String = "Data Source=LAPTOP-I0DKJOIN\ERICKSQLEXPRESS; Initial Catalog=tienda; User ID=sa; Password=1706Erick"
        Return SQLServer_Conection_String
    End Function

    <WebMethod>
    Public Function ValidarSesion(usuario As String, psw As String) As Boolean
        Dim resultado As Boolean = False
        Dim connectionString As String = Get_ConectionString()
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT COUNT(*) FROM Usuarios WHERE usuario=@usuario AND psw=@psw"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@usuario", usuario)
                    command.Parameters.AddWithValue("@psw", psw)
                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                    resultado = count > 0
                End Using
            End Using
        Catch ex As Exception
            resultado = False
        End Try
        Return resultado
    End Function




End Class