Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports iTextSharp.text
Imports iTextSharp.text.pdf

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class GeneradorTickets
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GenerarTicket(ByVal idVenta As Integer) As String
        Dim ticket As New StringBuilder()
        Dim connectionString As String = "Data Source=DESKTOP-H08JMC2\\SQLEXPRESS; Initial Catalog = tienda; User ID = sa; Password=1234.abcd"

        'Obtenemos los datos de la venta
        Dim query As String = $"SELECT v.*  FROM Ventas v  WHERE v.IdVenta = {idVenta}"

        Dim connection As New SqlConnection(connectionString)
        Dim command As New SqlCommand(query, connection)
        connection.Open()
        Dim reader As SqlDataReader = command.ExecuteReader()
        reader.Read()
        Dim fechaVenta As String = reader("fechaVenta").ToString()
        Dim totalVenta As Double = Double.Parse(reader("monto").ToString())
        reader.Close()

        'Generamos el ticket
        ticket.AppendLine("================================")
        ticket.AppendLine("         TICKET DE VENTA")
        ticket.AppendLine("================================")
        ticket.AppendLine($"FECHA: {fechaVenta}")
        ticket.AppendLine("--------------------------------")
        ticket.AppendLine($"TOTAL VENTA: {totalVenta.ToString("C2").PadLeft(26)}")
        ticket.AppendLine("--------------------------------")

        'Cerramos la conexión a la base de datos
        connection.Close()

        Return ticket.ToString()
    End Function

    <WebMethod()>
    Public Sub ImprimirTicket(ByVal idVenta As Integer)
        'Generamos el ticket
        Dim ticket As String = GenerarTicket(idVenta)

        'Generamos el archivo PDF
        Dim pdfDoc As New iTextSharp.text.Document()
        Dim pdfWriter As PdfWriter = pdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream)
        pdfDoc.Open()
        pdfDoc.Add(New Paragraph(ticket))
        pdfDoc.Close()

        'Descargamos el archivo PDF
        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename=TICKET-{idVenta}.pdf")
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Write(pdfDoc)
        HttpContext.Current.Response.End()
    End Sub

End Class