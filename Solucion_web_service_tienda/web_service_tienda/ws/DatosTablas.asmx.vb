Imports System.ComponentModel
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Xml.Serialization

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class DatosTablas
    Inherits System.Web.Services.WebService

    Public Function Get_ConectionString() As String
        Dim SQLServer_Conection_String = "Data Source=DESKTOP-H08JMC2\SQLEXPRESS; Initial Catalog=tienda; User ID=sa; Password=1234.abcd"
        Return SQLServer_Conection_String
    End Function

    <WebMethod()>
    Public Function DatosTablaProductos() As DataTable

        Dim sql As String
        Dim dataTable As New DataTable()

        dataTable.TableName = "Tabla1"
        dataTable.Columns.Add("idProducto")
        dataTable.Columns.Add("Producto")
        dataTable.Columns.Add("Precio de Venta")
        dataTable.Columns.Add("Precio de Compra")
        dataTable.Columns.Add("Caducidad")
        dataTable.Columns.Add("Stock")
        dataTable.Columns.Add("Disponibilidad")
        dataTable.Columns.Add("Categoria")

        sql = "SELECT * FROM [tienda].[dbo].[Productos]"

        Using conexion As New SqlConnection(Get_ConectionString())
            conexion.Open()
            Using command As New SqlCommand(sql, conexion)
                Using adapter As New SqlDataAdapter(command)
                    adapter.Fill(dataTable)
                End Using
            End Using
        End Using

        Dim xmlSerializer As New XmlSerializer(GetType(DataTable))
        Dim stringWriter As New StringWriter()
        xmlSerializer.Serialize(stringWriter, DatosTablaProductos)
        Dim xmlString As String = stringWriter.ToString()

        Return dataTable
    End Function
End Class