Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraReports.Configuration
Imports System.Data.SqlClient 'เป็นไรบรารี้ที่ใช้กับ Microsoft SQL Server ใช้ในการติดต่อกับฐานข้อมูล
' . . .  
' Create a data source.  

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function FillDataset() As DataTable
        'Dim SchedulerDBConnection As String = "Data Source=.\;Initial Catalog=MPlanx; User Id=sa; Password=4ward;"

        Dim SchedulerDBConnection As String = "Data Source=13.76.157.17;Initial Catalog=MPlanx; User Id=FCAdmin; Password=4Ward#4Ward#;"

        Dim cnn As New SqlConnection(SchedulerDBConnection)

        Dim Dt_Area As New DataTable
        Try
            With cnn
                If .State = ConnectionState.Open Then .Close()
                .Open()
            End With
            'cnn.Open()
            Dim cmd As New SqlCommand()
            With cmd
                .CommandText = "proc_IVM_InventoryBalanceTentative_2566"
                .CommandType = CommandType.StoredProcedure
                '.Parameters.Clear()
                '.Parameters.AddWithValue("@FieldStorageID", SiteID)
                '.Parameters.AddWithValue("@ParentStorageID", ParentAreaID)
                .Connection = cnn
            End With
            Dt_Area.Load(cmd.ExecuteReader)

            cnn.Close()
        Catch ex As Exception
            Dt_Area = Nothing
            cnn.Close()
        End Try

        Return Dt_Area

    End Function
    Private Function CreateReport() As XtraReport
        ' Create a new report instance. 
        Dim report As New XtraReport()

        '    ' Assign the data source to the report. 
        '    report.DataSource = FillDataset()
        '    report.DataMember = "customQuery"

        '    ' Add a detail band to the report. 
        '    Dim detailBand As New DetailBand()
        '    detailBand.Height = 50
        '    report.Bands.Add(detailBand)

        '    ' Add a label to the detail band. 
        '    Dim label As New XRLabel() With {
        '    .WidthF = 300
        '}
        '    label.DataBindings.Add("Text", Nothing, "customQuery.ProductName")
        '    detailBand.Controls.Add(label)

        '    report.ShowPreviewDialog()

        Dim rep As New XtraReport()

        ' Set the XtraReport.DataSource
        rep.DataSource = FillDataset()
        'rep.DataMember = (CType(rep.DataSource, DataSet)).Tables(0).TableName

        ' Add required bands to the Xtrareport.Bands collection
        InitBands(rep)

        ' Add requited styles to the XtraReport.StyleSheet collection
        InitStyles(rep)

        ' Arrange required controls on bands and bind the controls to data
        'If radioButton1.Checked Then
        '    ' Use XRTable to display data
        '    InitDetailsBasedonXRTable(rep)
        'Else
        '    ' Use XRLabels to display data
        '    InitDetailsBasedOnXRLabel(rep)

        'End If
        InitDetailsBasedOnXRLabel(rep)
        ' Create a chart, bind it to data and add to the report
        InitChart(rep)

        ' Display the result
        rep.ShowPreviewDialog()

        Return Report

    End Function
    Public Sub InitBands(ByVal rep As XtraReport)
        ' Create bands
        Dim detail As New DetailBand()
        Dim pageHeader As New PageHeaderBand()
        Dim reportFooter As New ReportFooterBand()
        detail.Height = 20
        reportFooter.Height = 380
        pageHeader.Height = 20

        ' Place the bands onto a report
        rep.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {detail, pageHeader, reportFooter})
    End Sub
    Public Sub InitStyles(ByVal rep As XtraReport)
        ' Create different odd and even styles
        Dim oddStyle As New XRControlStyle()
        Dim evenStyle As New XRControlStyle()

        ' Specify the odd style appearance
        oddStyle.BackColor = Color.LightBlue
        oddStyle.StyleUsing.UseBackColor = True
        oddStyle.StyleUsing.UseBorders = False
        oddStyle.Name = "OddStyle"

        ' Specify the even style appearance
        evenStyle.BackColor = Color.LightPink
        evenStyle.StyleUsing.UseBackColor = True
        evenStyle.StyleUsing.UseBorders = False
        evenStyle.Name = "EvenStyle"

        ' Add styles to report's style sheet
        rep.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {oddStyle, evenStyle})
    End Sub

    Public Sub InitDetailsBasedOnXRLabel(ByVal rep As XtraReport)
        'Dim ds As DataSet = (CType(rep.DataSource, DataSet))
        Dim ds As DataTable = (CType(rep.DataSource, DataTable))
        Dim colCount As Integer = ds.Columns.Count
        'Dim colWidth As Integer = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right)) / colCount
        Dim colWidth As Integer = 50
        'ตั้งค่าหน้ากระดาษ แนวนอน
        rep.Landscape = True
        ' Create header captions
        For i As Integer = 1 To colCount - 1
            Dim label As New XRLabel()
            If (i = 1) Then
                colWidth = 100
            End If
            'label.Location = New Point(colWidth * i, 0)
            label.Location = New Point(colWidth * (i - 1), 0)
            label.Size = New Size(colWidth, 40)
            label.Text = ds.Columns(i).Caption
            If i > 0 Then
                label.Borders = DevExpress.XtraPrinting.BorderSide.Right Or DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom
            Else
                label.Borders = DevExpress.XtraPrinting.BorderSide.All
            End If

            ' Place the headers onto a PageHeader band
            rep.Bands(BandKind.PageHeader).Controls.Add(label)
        Next i
        ' Create data-bound labels with different odd and even backgrounds
        For i As Integer = 1 To colCount - 1
            Dim label As New XRLabel()
            If (i = 1) Then
                colWidth = 100

            End If

            label.Location = New Point(colWidth * (i - 1), 0)
            label.Size = New Size(colWidth, 40)
            label.DataBindings.Add("Text", Nothing, ds.Columns(i).Caption)
            label.OddStyleName = "OddStyle"
            label.EvenStyleName = "EvenStyle"
            If i > 0 Then
                label.Borders = DevExpress.XtraPrinting.BorderSide.Right Or DevExpress.XtraPrinting.BorderSide.Bottom
            Else
                label.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right Or DevExpress.XtraPrinting.BorderSide.Bottom
            End If

            ' Place the labels onto a Detail band
            rep.Bands(BandKind.Detail).Controls.Add(label)
        Next i
    End Sub
    Public Sub InitDetailsBasedonXRTable(ByVal rep As XtraReport)
        Dim ds As DataSet = (CType(rep.DataSource, DataSet))
        Dim colCount As Integer = ds.Tables(0).Columns.Count
        Dim colWidth As Integer = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right)) / colCount

        ' Create a table to represent headers
        Dim tableHeader As New XRTable()
        tableHeader.Height = 20
        tableHeader.Width = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right))
        Dim headerRow As New XRTableRow()
        headerRow.Width = tableHeader.Width
        tableHeader.Rows.Add(headerRow)

        tableHeader.BeginInit()

        ' Create a table to display data
        Dim tableDetail As New XRTable()
        tableDetail.Height = 20
        tableDetail.Width = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right))
        Dim detailRow As New XRTableRow()
        detailRow.Width = tableDetail.Width
        tableDetail.Rows.Add(detailRow)
        tableDetail.EvenStyleName = "EvenStyle"
        tableDetail.OddStyleName = "OddStyle"

        tableDetail.BeginInit()

        ' Create table cells, fill the header cells with text, bind the cells to data
        For i As Integer = 0 To colCount - 1
            Dim headerCell As New XRTableCell()
            headerCell.Width = colWidth
            headerCell.Text = ds.Tables(0).Columns(i).Caption

            Dim detailCell As New XRTableCell()
            detailCell.Width = colWidth
            detailCell.DataBindings.Add("Text", Nothing, ds.Tables(0).Columns(i).Caption)
            If i = 0 Then
                headerCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom
            Else
                headerCell.Borders = DevExpress.XtraPrinting.BorderSide.All
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.All
            End If

            ' Place the cells into the corresponding tables
            headerRow.Cells.Add(headerCell)
            detailRow.Cells.Add(detailCell)
        Next i
        tableHeader.EndInit()
        tableDetail.EndInit()
        ' Place the table onto a report's Detail band
        rep.Bands(BandKind.PageHeader).Controls.Add(tableHeader)
        rep.Bands(BandKind.Detail).Controls.Add(tableDetail)
    End Sub
    Public Sub InitChart(ByVal rep As XtraReport)
        Dim ds As DataTable = (CType(rep.DataSource, DataTable))

        ' Create a chart
        Dim xrChart1 As XRChart = New DevExpress.XtraReports.UI.XRChart()

        xrChart1.Location = New System.Drawing.Point(0, 0)
        xrChart1.Name = "xrChart1"

        ' Create chart series and bind them to data
        For i As Integer = 1 To ds.Columns.Count - 1
            If ds.Columns(i).DataType Is GetType(Integer) OrElse ds.Columns(i).DataType Is GetType(Double) Then
                Dim series As New DevExpress.XtraCharts.Series(ds.Columns(i).Caption, DevExpress.XtraCharts.ViewType.Bar)
                series.DataSource = ds
                series.ArgumentDataMember = ds.Columns(1).Caption
                series.PointOptionsTypeName = "PointOptions"
                series.ValueDataMembers(0) = ds.Columns(i).Caption
                xrChart1.Series.Add(series)

            End If
        Next i
        ' Customize the chart appearance
        CType(xrChart1.Diagram, DevExpress.XtraCharts.XYDiagram).AxisX.Label.Angle = 20
        CType(xrChart1.Diagram, DevExpress.XtraCharts.XYDiagram).AxisX.Label.Font = New System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(204)))
        CType(xrChart1.Diagram, DevExpress.XtraCharts.XYDiagram).AxisX.Label.Antialiasing = True

        xrChart1.SeriesTemplate.PointOptionsTypeName = "PointOptions"
        xrChart1.Size = New System.Drawing.Size(650, 360)

        ' Place the chart onto a report footer
        rep.Bands(BandKind.ReportFooter).Controls.Add(xrChart1)
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        CreateReport()
        ' Create XtraReport instance
        'Dim rep As New XtraReport()

        '' Set the XtraReport.DataSource
        'rep.DataSource = FillDataset()
        ''rep.DataMember = FillDataset().TableName(0)

        ''rep.DataMember = (CType(rep.DataSource, DataSet)).Tables(0).TableName

        '' Add required bands to the Xtrareport.Bands collection
        'InitBands(rep)

        '' Add requited styles to the XtraReport.StyleSheet collection
        'InitStyles(rep)

        '' Arrange required controls on bands and bind the controls to data
        ''If radioButton1.Checked Then
        ''    ' Use XRTable to display data
        ''    InitDetailsBasedonXRTable(rep)
        ''Else
        ''    ' Use XRLabels to display data
        ''    InitDetailsBasedOnXRLabel(rep)

        ''End If
        'InitDetailsBasedonXRTable(rep)
        '' Create a chart, bind it to data and add to the report
        'InitChart(rep)

        '' Display the result
        'rep.ShowPreviewDialog()
    End Sub
End Class
