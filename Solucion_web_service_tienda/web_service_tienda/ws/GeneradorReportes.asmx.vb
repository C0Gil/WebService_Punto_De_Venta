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
    Public Sub MostrarVentasPorMes(ByVal mesSeleccionado As Integer)
        Dim cadenaConexion As String = "Data Source=LAPTOP-I0DKJOIN\ERICKSQLEXPRESS; Initial Catalog=tienda; User ID= sa; Password=1706Erick"
        Dim conexion As New SqlConnection(cadenaConexion)
        Dim comando As New SqlCommand("MostrarVentasPorMes", conexion)
        comando.CommandType = CommandType.StoredProcedure
        comando.Parameters.AddWithValue("@Mes", mesSeleccionado)

        Dim adaptador As New SqlDataAdapter(comando)
        Dim tablaVentas As New DataTable()
        adaptador.Fill(tablaVentas)

        Dim pdfDoc As New iTextSharp.text.Document()
        Dim pdfWriter As PdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream)
        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename=ReporteVentas-{mesSeleccionado}.pdf")
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        pdfDoc.Open()
        pdfDoc.Add(New Paragraph("Reporte de ventas del mes " + mesSeleccionado.ToString() + vbCrLf + vbCrLf))

        Dim fechaActual As Date = Date.MinValue
        Dim totalMonto As Decimal = 0
        Dim totalPrecioVenta As Decimal = 0

        For Each fila As DataRow In tablaVentas.Rows
            Dim fechaVenta As Date = Convert.ToDateTime(fila("FechaVenta"))
            Dim producto As String = fila("producto").ToString()
            Dim monto As Decimal = Convert.ToDecimal(fila("Monto"))
            Dim precioVenta As Decimal = Convert.ToDecimal(fila("precioVenta"))

            If fechaVenta <> fechaActual Then
                If fechaActual <> Date.MinValue Then
                    pdfDoc.Add(New Paragraph($"Total monto: {totalMonto}, Total precio venta: {totalPrecioVenta}"))
                    pdfDoc.Add(New Paragraph(vbCrLf))
                End If

                pdfDoc.Add(New Paragraph($"Fecha: {fechaVenta.ToString("dd/MM/yyyy")}"))
                pdfDoc.Add(New Paragraph(vbCrLf))

                fechaActual = fechaVenta
                totalMonto = 0
                totalPrecioVenta = 0
            End If

            pdfDoc.Add(New Paragraph($"{producto} : {monto} : {precioVenta}"))

            totalMonto += monto
            totalPrecioVenta += precioVenta
        Next

        pdfDoc.Add(New Paragraph($"Total monto: {totalMonto}, Total precio venta: {totalPrecioVenta}"))

        pdfDoc.Close()
        HttpContext.Current.Response.End()
    End Sub



End Class