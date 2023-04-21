Imports System.ComponentModel
Imports System.Data.SqlClient
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
    Public Function InsertarVenta(fecha As Date, monto As Double) As String
        Try
            Dim sql As String
            Dim mycmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(Get_ConectionString())
            Dim idEstaVenta As Integer

            'Recuperar id De Ultima Venta
            conexion.Open()

            sql = "SELECT TOP (1) [idVenta] FROM [tienda].[dbo].[Ventas] ORDER BY [idVenta] DESC"

            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With

            reader = mycmd.ExecuteReader

            If reader.HasRows Then
                While reader.Read()
                    idEstaVenta = Convert.ToInt32(reader("idVenta")) + 1
                End While
            Else
                Return "Error al Agregar Venta"
            End If
            conexion.Close()

            'Insertar la Venta
            conexion.Open()
            sql = "INSERT INTO [dbo].[Ventas] ([idVenta] ,[fechaVenta] ,[monto]) VALUES(" + idEstaVenta.ToString() + ",'" + fecha.ToString("yyyy/MM/dd") + "'," + monto.ToString() + ")"

            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With
            reader = mycmd.ExecuteReader
            conexion.Close()

            Return idEstaVenta.ToString()
        Catch ex As Exception
            Return ex.ToString
        End Try
        Return Nothing
    End Function

    <WebMethod()>
    Public Function IsertarVentaProducto(idVenta As String, idProducto As String, cantidad As String) As String
        Try
            Dim sql As String
            Dim mycmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(Get_ConectionString())
            Dim idEsteRegistro As Integer

            'Recuperar ultimo id de ultimo registro
            conexion.Open()

            sql = "SELECT TOP (1) [idVentaProducto] FROM [tienda].[dbo].[VentaProducto] ORDER BY [idVentaProducto] DESC"

            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With

            reader = mycmd.ExecuteReader

            If reader.HasRows Then
                While reader.Read()
                    idEsteRegistro = Convert.ToInt32(reader("idVentaProducto")) + 1
                End While
            Else
                Return "Error al Agregar Venta"
            End If
            conexion.Close()
            ' Realizar Inserccion
            conexion.Open()
            sql = "INSERT INTO [dbo].[VentaProducto] ([idVentaProducto] ,[idVenta] ,[idProducto], [cantidadProducto]) VALUES(" + idEsteRegistro.ToString() + "," + idVenta + "," + idProducto + "," + cantidad + " )"
            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With
            reader = mycmd.ExecuteReader
            conexion.Close()

            Return idEsteRegistro.ToString()
        Catch ex As Exception
            Return ex.ToString
        End Try
        Return Nothing
    End Function

    <WebMethod()>
    Public Function IsertarUsuarioVenta(idUsuario As String, idVenta As String) As String
        Try
            Dim sql As String
            Dim mycmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(Get_ConectionString())
            Dim idEsteRegistro As Integer

            'Recuperar ultimo id de ultimo registro
            conexion.Open()

            sql = "SELECT TOP (1) [idVentaVendedor] FROM [tienda].[dbo].[VentaVendedor] ORDER BY [idVentaVendedor] DESC"

            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With

            reader = mycmd.ExecuteReader

            If reader.HasRows Then
                While reader.Read()
                    idEsteRegistro = Convert.ToInt32(reader("idVentaVendedor")) + 1
                End While
            Else
                Return "Error al Agregar Venta"
            End If

            conexion.Close()
            ' Realizar Inserccion
            conexion.Open()
            sql = "INSERT INTO [dbo].[VentaVendedor] ([idVentaVendedor] ,[idUsuario] ,[idVenta]) VALUES(" + idEsteRegistro.ToString() + "," + idUsuario + "," + idVenta + " )"
            With mycmd
                .CommandText = sql
                .Connection = conexion
            End With
            reader = mycmd.ExecuteReader
            conexion.Close()

            Return idEsteRegistro.ToString()
        Catch ex As Exception
            Return ex.ToString
        End Try

        Return Nothing
    End Function

End Class