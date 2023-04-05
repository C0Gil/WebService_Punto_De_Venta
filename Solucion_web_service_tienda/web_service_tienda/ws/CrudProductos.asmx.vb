Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.Services.Protocols

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CrudProductos
    Inherits System.Web.Services.WebService

    Public Function Get_ConectionString() As String
        Dim SQLServer_Conection_String = "Data Source=JONATHAN\SQLEXPRESS; Initial Catalog=Tienda; User ID= sa; Password=aaa"
        Return SQLServer_Conection_String
    End Function

    <WebMethod()>
    Public Function InsertarProducto(idpro As Integer, pro As String, preven As Double, precom As Double, cadu As Date, sto As Integer, disp As Boolean, idcat As Integer) As String


        Try
            Dim sql As String
            Dim mycmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(Get_ConectionString())
            conexion.Open()


            sql = "INSERT INTO [dbo].[Productos] ([idProducto], [producto], [precioVenta], [precioCompra], [fechaCaducidad], [stock], [disponibilidad], [idCategoria]) VALUES(" + idpro.ToString() + ",'" + pro + "'," + preven.ToString() + "," + precom.ToString() + ",'" + cadu.ToString("dd/MM/yyyy") + "'," + sto.ToString() + "," + If(disp, "1", "0") + "," + idcat.ToString() + " )"


            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With
            reader = mycmd.ExecuteReader
            conexion.Close()

            Return "Registro Insertado con Exito "
        Catch ex As Exception
            Return ex.ToString
        End Try

    End Function

    <WebMethod()>
    Public Function ModificarProducto(idpro As Integer, pro As String, preven As Double, precom As Double, cadu As Date, sto As Integer, disp As Boolean, idcat As Integer) As String
        Try
            Dim sql As String
            Dim mycmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(Get_ConectionString())
            conexion.Open()


            sql = "UPDATE [dbo].[Productos] SET [producto] = '" + pro + "', [precioVenta] = " + preven.ToString() + ", [precioCompra] = " + precom.ToString() + ", [fechaCaducidad] = '" + cadu.ToString("dd/MM/yyyy") + "', [stock] = " + sto.ToString() + ", [disponibilidad] = " + If(disp, "1", "0") + ", [idCategoria] = " + idcat.ToString() + " WHERE [idProducto] = " + idpro.ToString()

            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With
            reader = mycmd.ExecuteReader
            conexion.Close()
            Return "Registro Modificado con Exito"
        Catch ex As Exception
            Return ex.ToString
        End Try

    End Function

    <WebMethod()>
    Public Function EliminarProducto(idpro As Integer) As String
        Try
            Dim sql As String
            Dim mycmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(Get_ConectionString())
            conexion.Open()
            sql = "DELETE FROM [dbo].[Productos]" + " WHERE [idProducto] ='" + idpro.ToString() + "'  "
            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With
            reader = mycmd.ExecuteReader
            conexion.Close()
            Return "Registro Eliminado con Exito"
        Catch ex As Exception
            Return ex.ToString

        End Try

    End Function

    <WebMethod()>
    Public Function BuscarProducto(idpro As Integer) As String
        Try
            Dim sql As String
            Dim mycmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(Get_ConectionString())
            conexion.Open()

            ' Sentencia SELECT para recuperar el producto basado en su ID

            sql = "SELECT [producto], [precioVenta], [precioCompra], [fechaCaducidad], [stock], [disponibilidad], [idCategoria] FROM [dbo].[Productos] WHERE [idProducto] = " + idpro.ToString()
            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With

            ' Ejecuta el comando y obtiene el objeto SqlDataReader
            reader = mycmd.ExecuteReader

            ' Si hay datos en el objeto SqlDataReader, recupera el nombre del producto
            If reader.HasRows Then
                While reader.Read()
                    Dim producto As String = reader("producto").ToString()
                    Dim precioVenta As Double = Convert.ToDouble(reader("precioVenta"))
                    Dim precioCompra As Double = Convert.ToDouble(reader("precioCompra"))
                    Dim fechaCaducidad As Date = Convert.ToDateTime(reader("fechaCaducidad"))
                    Dim stock As Integer = Convert.ToInt32(reader("stock"))
                    Dim disponibilidad As Boolean = Convert.ToBoolean(reader("disponibilidad"))
                    Dim idCategoria As Integer = Convert.ToInt32(reader("idCategoria"))

                    Return "Nombre del producto: " + producto + "<br/>" +
                       "Precio de venta: " + precioVenta.ToString() + "<br/>" +
                       "Precio de compra: " + precioCompra.ToString() + "<br/>" +
                       "Fecha de caducidad: " + fechaCaducidad.ToString("dd/MM/yyyy") + "<br/>" +
                       "Stock: " + stock.ToString() + "<br/>" +
                       "Disponibilidad: " + disponibilidad.ToString() + "<br/>" +
                       "ID de categoría: " + idCategoria.ToString()
                End While
            Else
                Return "No se encontró ningún producto con el ID especificado"
            End If

            conexion.Close()

        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function




End Class