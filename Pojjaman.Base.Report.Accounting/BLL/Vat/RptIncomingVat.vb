Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Reflection
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.TextHelper
Imports Longkong.Pojjaman.Services
Imports Longkong.Pojjaman.DataAccessLayer
Imports System.Collections.Generic

Namespace Longkong.Pojjaman.BusinessLogic
    Public Class RptIncomingVat
        Inherits Report
        Implements INewReport

#Region "Members"
        Private m_reportColumns As ReportColumnCollection
#End Region

#Region "Constructors"
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal filters As Filter(), ByVal fixValueCollection As DocPrintingItemCollection)
            MyBase.New(filters, fixValueCollection)
        End Sub
#End Region

#Region "Methods"
        Private m_grid As Syncfusion.Windows.Forms.Grid.GridControl
        Public Overrides Sub ListInNewGrid(ByVal grid As Syncfusion.Windows.Forms.Grid.GridControl)
            m_grid = grid
            RemoveHandler m_grid.CellDoubleClick, AddressOf CellDblClick
            AddHandler m_grid.CellDoubleClick, AddressOf CellDblClick
            m_grid.BeginUpdate()
            m_grid.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.SystemTheme
            m_grid.Model.Options.NumberedColHeaders = False
            m_grid.Model.Options.WrapCellBehavior = Syncfusion.Windows.Forms.Grid.GridWrapCellBehavior.WrapRow
            CreateHeader()
            PopulateData()
            m_grid.EndUpdate()
        End Sub
        Private Sub CellDblClick(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs)

            Dim dr As DataRow = CType(m_grid(e.RowIndex, 0).Tag, DataRow)
            If dr Is Nothing Then
                Return
            End If

            Dim drh As New DataRowHelper(dr)

            Dim docId As Integer = drh.GetValue(Of Integer)("docId")
            Dim docType As Integer = drh.GetValue(Of Integer)("doctype")

            If docId > 0 AndAlso docType > 0 Then
                Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
                Dim en As SimpleBusinessEntityBase = SimpleBusinessEntityBase.GetEntity(Entity.GetFullClassName(docType), docId)
                myEntityPanelService.OpenDetailPanel(en)
            End If
        End Sub
        Private Sub CreateHeader()
            m_grid.RowCount = 0
            m_grid.ColCount = 17

            m_grid.ColWidths(1) = 100
            m_grid.ColWidths(2) = 120

            m_grid.ColWidths(3) = 120
            m_grid.ColWidths(4) = 120
            m_grid.ColWidths(5) = 90
            m_grid.ColWidths(6) = 80

            m_grid.ColWidths(7) = 120
            m_grid.ColWidths(8) = 100
            m_grid.ColWidths(9) = 100

            m_grid.ColWidths(10) = 80
            m_grid.ColWidths(11) = 100
            m_grid.ColWidths(12) = 120

            m_grid.ColWidths(13) = 120
            m_grid.ColWidths(14) = 120
            m_grid.ColWidths(15) = 100
            m_grid.ColWidths(16) = 120
            m_grid.ColWidths(17) = 200

            m_grid.ColStyles(1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

            m_grid.ColStyles(3).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(4).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(5).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center
            m_grid.ColStyles(6).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center

            m_grid.ColStyles(7).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid.ColStyles(8).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid.ColStyles(9).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right

            m_grid.ColStyles(10).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid.ColStyles(11).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(12).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

            m_grid.ColStyles(13).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(14).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(15).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(16).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(17).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

            m_grid.Rows.HeaderCount = 0
            m_grid.Rows.FrozenCount = 0

            m_grid(0, 1).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.DocDate}") '"�ѹ���"
            m_grid(0, 2).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.Invoice}") '"㺡ӡѺ����"

            m_grid(0, 3).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.SupplierName}") '"���ͼ����"
            m_grid(0, 4).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.TaxId}") '"�Ţ��Шӵ�Ǽ����������"
            m_grid(0, 5).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.BranchHead}") '"�ӹѡ�ҹ�˭�"
            m_grid(0, 6).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.BranchId}") '"�Ңҷ��"

            m_grid(0, 7).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.BeforeTax}") '"��Ť���Թ���/��ԡ��"
            m_grid(0, 8).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.TaxAmount}") '"�ӹǹ�Թ����"
            m_grid(0, 9).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.AfterTax}") '"���"

            m_grid(0, 10).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.TaxRate}") '"�ѵ������"
            m_grid(0, 11).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.SubmitalDate}") '"�ѹ����������"
            m_grid(0, 12).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.GroupName}") '"���������"


            m_grid(0, 13).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptCCPOSummary.CCName}") '"���� Cost Center"
            m_grid(0, 14).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.DocCode}") '"�Ţ����͡���"
            m_grid(0, 15).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.GLCode}") '"GLCode"
            m_grid(0, 16).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.VatRunNumber}") '"�Ţ����ѹ"
            m_grid(0, 17).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.RelatedDoc}") '"�Ţ����͡�������Ǣ�ͧ"

            m_grid(0, 1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

            m_grid(0, 3).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 4).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 5).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center
            m_grid(0, 6).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center

            m_grid(0, 7).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid(0, 8).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid(0, 9).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right

            m_grid(0, 10).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid(0, 11).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 12).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

            m_grid(0, 13).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 14).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 15).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 16).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 17).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left







        End Sub
        Private Sub PopulateData()
            Dim dt As DataTable = Me.DataSet.Tables(0)
            Dim dt2 As DataTable = Me.DataSet.Tables(1)
            Dim currentDocCode As String = ""
            Dim currentLine As String = ""
            Dim SumBeforeTax As Decimal = 0
            Dim SumTaxAmount As Decimal = 0
            Dim SumAfterTax As Decimal = 0

            Dim RelateDic As New DictionaryOfJoinStringbyInteger(dt2, "paysi_pays", "stock_code", ",")

            Dim DocId As Integer
            Dim DocType As Integer

            Dim BranchId As Integer = -1

            Dim currDocIndex As Integer = -1
            For Each row As DataRow In dt.Rows
                Dim drh As New DataRowHelper(row)
                DocId = drh.GetValue(Of Integer)("docid")
                DocType = drh.GetValue(Of Integer)("doctype")

                If row("docId").ToString & row("vatrunnumber").ToString <> currentDocCode Then
                    currentDocCode = row("docId").ToString & row("vatrunnumber").ToString
                    m_grid.RowCount += 1
                    currDocIndex = m_grid.RowCount
                    m_grid(currDocIndex, 0).Tag = row
                    m_grid.RowStyles(currDocIndex).ReadOnly = True


                    If IsDate(row("DocDate")) Then
                        m_grid(currDocIndex, 1).CellValue = CDate(row("DocDate")).ToShortDateString
                    End If
                    If Not row.IsNull("Invoice") Then
                        m_grid(currDocIndex, 2).CellValue = row("Invoice").ToString
                    End If

                    If Not row.IsNull("Supplier") Then
                        m_grid(currDocIndex, 3).CellValue = row("Supplier").ToString
                    End If
                    If Not row.IsNull("TaxId") Then
                        m_grid(currDocIndex, 4).CellValue = row("TaxId").ToString
                    End If

                    BranchId = -1
                    If IsNumeric(row("BranchId")) Then
                        BranchId = CInt(row("BranchId"))
                    End If
                    If BranchId = 0 Then
                        m_grid(currDocIndex, 5).CellValue = "X"
                    End If
                    m_grid(currDocIndex, 6).CellValue = Configuration.BranchString(BranchId)

                    If IsNumeric(row("BeforeTax")) Then
                        m_grid(currDocIndex, 7).CellValue = Configuration.FormatToString(CDec(row("BeforeTax")), DigitConfig.Price)
                        SumBeforeTax += Configuration.Format(CDec(row("BeforeTax")), DigitConfig.Price)
                    End If
                    If IsNumeric(row("TaxAmt")) Then
                        m_grid(currDocIndex, 8).CellValue = Configuration.FormatToString(CDec(row("TaxAmt")), DigitConfig.Price)
                        SumTaxAmount += Configuration.Format(CDec(row("TaxAmt")), DigitConfig.Price)
                    End If
                    If IsNumeric(row("AfterTax")) Then
                        m_grid(currDocIndex, 9).CellValue = Configuration.FormatToString(CDec(row("AfterTax")), DigitConfig.Price)
                        SumAfterTax += Configuration.Format(CDec(row("AfterTax")), DigitConfig.Price)
                    End If

                    If IsNumeric(row("taxrate")) Then
                        m_grid(currDocIndex, 10).CellValue = Configuration.FormatToString(CDec(row("taxrate")), DigitConfig.Price)
                    End If
                    If IsDate(row("SubmitalDate")) Then
                        m_grid(currDocIndex, 11).CellValue = CDate(row("SubmitalDate")).ToShortDateString
                    End If
                    If Not row.IsNull("GroupName") Then
                        m_grid(currDocIndex, 12).CellValue = row("GroupName").ToString
                    End If

                    If Not row.IsNull("CostcenterName") Then
                        m_grid(currDocIndex, 13).CellValue = row("CostcenterName").ToString
                    End If
                    If Not row.IsNull("DocCode") Then
                        m_grid(currDocIndex, 14).CellValue = row("DocCode").ToString
                    End If
                    If Not row.IsNull("gl_code") Then
                        m_grid(currDocIndex, 15).CellValue = row("gl_code")
                    End If
                    If Not row.IsNull("VatRunNumber") Then
                        m_grid(currDocIndex, 16).CellValue = row("VatRunNumber").ToString
                    End If
                    If DocType = 226 AndAlso RelateDic.ContainsKey(DocId) Then
                        m_grid(currDocIndex, 17).CellValue = RelateDic.GetValue(DocId)
                    ElseIf Not row.IsNull("RelatedDoc") Then
                        m_grid(currDocIndex, 17).CellValue = row("RelatedDoc").ToString
                    End If


                End If
            Next
            m_grid.RowCount += 1
            currDocIndex = m_grid.RowCount
            m_grid.RowStyles(currDocIndex).BackColor = Color.FromArgb(128, 255, 128)
            m_grid.RowStyles(currDocIndex).Font.Bold = True
            m_grid.RowStyles(currDocIndex).ReadOnly = True
            m_grid(currDocIndex, 6).CellValue = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.SumAll}") '"���������"
            m_grid(currDocIndex, 7).CellValue = Configuration.FormatToString(SumBeforeTax, DigitConfig.Price)
            m_grid(currDocIndex, 8).CellValue = Configuration.FormatToString(SumTaxAmount, DigitConfig.Price)
            m_grid(currDocIndex, 9).CellValue = Configuration.FormatToString(SumAfterTax, DigitConfig.Price)
        End Sub
#End Region#Region "Shared"
#End Region#Region "Properties"        Public Overrides ReadOnly Property ClassName() As String
            Get
                Return "RptIncomingVat"
            End Get
        End Property
        Public Overrides ReadOnly Property DetailPanelTitle() As String
            Get
                Return "${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.DetailLabel}"
            End Get
        End Property
        Public Overrides ReadOnly Property DetailPanelIcon() As String
            Get
                Return "Icons.16x16.RptIncomingVat"
            End Get
        End Property
        Public Overrides ReadOnly Property ListPanelIcon() As String
            Get
                Return "Icons.16x16.RptIncomingVat"
            End Get
        End Property
        Public Overrides ReadOnly Property ListPanelTitle() As String
            Get
                Return "${res:Longkong.Pojjaman.BusinessLogic.RptIncomingVat.ListLabel}"
            End Get
        End Property
        Public Overrides ReadOnly Property TabPageText() As String
            Get
                Dim tpt As String = Me.StringParserService.Parse(Me.DetailPanelTitle) & " (" & Me.Code & ")"
                If tpt.EndsWith("()") Then
                    tpt.TrimEnd("()".ToCharArray)
                End If
                Return tpt
            End Get
        End Property
#End Region#Region "IPrintableEntity"
        Public Overrides Function GetDefaultFormPath() As String
            Return "C:\Documents and Settings\Administrator\Desktop\Report.dfm"
        End Function
        Public Overrides Function GetDefaultForm() As String

        End Function
        Public Overrides Function GetDocPrintingEntries() As DocPrintingItemCollection
            Dim dpiColl As New DocPrintingItemCollection
            Dim dpi As DocPrintingItem

            For Each fixDpi As DocPrintingItem In Me.FixValueCollection
                dpiColl.Add(fixDpi)
            Next

            Dim i As Integer = 0
            Dim SumTaxAmount As Decimal = 0
            Dim SumBeforeTax As Decimal = 0
            Dim SumAfterTax As Decimal = 0

            Dim BranchId As Integer = -1

            For Each itemRow As DataRow In Me.DataSet.Tables(0).Rows
                'Item.LineNumber
                dpi = New DocPrintingItem
                dpi.Mapping = "linenumber"
                dpi.Value = i + 1
                dpi.DataType = "System.Sting"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.DocDate
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.DocDate"
                dpi.Value = itemRow("docdate")
                dpi.DataType = "System.DateTime"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.Invoice
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.Invoice"
                dpi.Value = itemRow("invoice")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.DocCode
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.DocCode"
                dpi.Value = itemRow("docCode")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.VatRunNumber
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.vatrunnumber"
                dpi.Value = itemRow("vatrunnumber")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.RelatedDoc
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.RelatedDoc"
                dpi.Value = itemRow("RelatedDoc")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.SubmitalDate
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.SubmitalDate"
                dpi.Value = itemRow("SubmitalDate")
                dpi.DataType = "System.DateTime"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.Supplier
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.Supplier"
                dpi.Value = itemRow("supplier")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.BeforeTax
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.BeforeTax"
                dpi.Value = itemRow("beforetax")
                dpi.DataType = "System.Decimal"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)
                If IsNumeric(itemRow("beforetax")) Then
                    SumBeforeTax += Configuration.Format(CDec(itemRow("beforetax")), DigitConfig.Price)
                End If

                'Item.TaxAmount
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.TaxAmount"
                dpi.Value = itemRow("taxamt")
                dpi.DataType = "System.Decimal"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)
                If IsNumeric(itemRow("taxamt")) Then
                    SumTaxAmount += Configuration.Format(CDec(itemRow("taxamt")), DigitConfig.Price)
                End If

                'Item.AfterTax
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.AfterTax"
                dpi.Value = itemRow("aftertax")
                dpi.DataType = "System.Decimal"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)
                If IsNumeric(itemRow("aftertax")) Then
                    SumAfterTax += CDec(itemRow("aftertax"))
                End If

                'Item.Invoice
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.Invoice"
                dpi.Value = itemRow("Invoice")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.GroupName
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.GroupName"
                dpi.Value = itemRow("GroupName")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.CostcenterName
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.CostcenterName"
                dpi.Value = itemRow("CostcenterName")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)

                'Item.GLCode
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.GLCode"
                dpi.Value = itemRow("gl_code")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)


                'Item.TaxId
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.TaxId"
                dpi.Value = itemRow("TaxId")
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)



                BranchId = -1
                If IsNumeric(itemRow("BranchId")) Then
                    BranchId = CInt(itemRow("BranchId"))
                End If


                'Item.BranchHead
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.BranchHead"
                If BranchId = 0 Then
                    dpi.Value = "X"
                Else
                    dpi.Value = ""
                End If
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)


                'Item.BranchId
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.BranchId"
                dpi.Value = Configuration.BranchString(BranchId)
                dpi.DataType = "System.String"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)


                'Item.TaxRate
                dpi = New DocPrintingItem
                dpi.Mapping = "Item.TaxRate"
                dpi.Value = itemRow("taxrate")
                dpi.DataType = "System.Decimal"
                dpi.Row = i + 1
                dpi.Table = "Item"
                dpiColl.Add(dpi)


                i += 1
            Next

            'SumText
            dpi = New DocPrintingItem
            dpi.Mapping = "SumText"
            dpi.Value = "���"
            dpi.DataType = "System.String"
            dpi.PrintingFrequency = DocPrintingItem.Frequency.LastPage
            dpiColl.Add(dpi)

            'SumCol5
            dpi = New DocPrintingItem
            dpi.Mapping = "SumCol5"
            dpi.Value = Configuration.FormatToString(SumBeforeTax, DigitConfig.Price)
            dpi.DataType = "System.Decimal"
            dpi.PrintingFrequency = DocPrintingItem.Frequency.LastPage
            dpiColl.Add(dpi)

            'SumCol6
            dpi = New DocPrintingItem
            dpi.Mapping = "SumCol6"
            dpi.Value = Configuration.FormatToString(SumTaxAmount, DigitConfig.Price)
            dpi.DataType = "System.Decimal"
            dpi.PrintingFrequency = DocPrintingItem.Frequency.LastPage
            dpiColl.Add(dpi)

            'SumBeforeTax
            dpi = New DocPrintingItem
            dpi.Mapping = "SumAfterTax"
            dpi.Value = Configuration.FormatToString(SumAfterTax, DigitConfig.Price)
            dpi.DataType = "System.Decimal"
            dpi.PrintingFrequency = DocPrintingItem.Frequency.LastPage
            dpiColl.Add(dpi)

            Return dpiColl
        End Function
#End Region
    End Class
End Namespace

