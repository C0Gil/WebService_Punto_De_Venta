Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports iTextSharp.text
Imports iTextSharp.text.pdf

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class GeneradorReportes
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function MostrarVentasPorMes(mesSeleccionado As Integer) As String
        Dim cadenaConexion As String = "Data Source=LAPTOP-4N86038V; Initial Catalog=tiendaa; User ID= sa; Password=jmsa"
        Dim conexion As New SqlConnection(cadenaConexion)
        Dim comando As New SqlCommand("MostrarVentasPorMes", conexion)
        comando.CommandType = CommandType.StoredProcedure

        comando.Parameters.AddWithValue("@Mes", mesSeleccionado)

        conexion.Open()

        Dim adaptador As New SqlDataAdapter(comando)
        Dim tablaVentas As New DataTable()

        adaptador.Fill(tablaVentas)
        conexion.Close()

        Dim ventas As String = ""
        For Each fila As DataRow In tablaVentas.Rows
            ventas += fila("FechaVenta").ToString() + " -> " + fila("producto").ToString() + " -> " + fila("Monto").ToString() + " -> " + fila("precioVenta").ToString() + vbCrLf
        Next

        Return ventas
    End Function

    <WebMethod()>
    Public Sub ImprimirTicket(ByVal fechaVenta As Integer)
        'Generamos el ticket
        Dim report As String = MostrarVentasPorMes(fechaVenta)

        'Generamos el archivo PDF
        Dim pdfDoc As New iTextSharp.text.Document()
        Dim pdfWriter As PdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream)
        pdfDoc.Open()
        pdfDoc.Add(New Paragraph(report))
        pdfDoc.Close()

        'Descargamos el archivo PDF
        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename=TICKET-{fechaVenta}.pdf")
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Write(pdfDoc)
        HttpContext.Current.Response.End()
    End Sub

End Class