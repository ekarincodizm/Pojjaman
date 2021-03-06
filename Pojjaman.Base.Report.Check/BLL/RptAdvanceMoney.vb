Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Reflection
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.TextHelper
Imports Longkong.Pojjaman.Services

Namespace Longkong.Pojjaman.BusinessLogic
    Public Class RptAdvanceMoney
        Inherits Report
        Implements INewReport

#Region "Members"
        Private m_reportColumns As ReportColumnCollection
        Private m_hashData As Hashtable
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
            Dim tr As Object = m_hashData(e.RowIndex)
            If tr Is Nothing Then
                Return
            End If

            If TypeOf tr Is DataRow Then
                Dim dr As DataRow = CType(tr, DataRow)
                If dr Is Nothing Then
                    Return
                End If

                Dim drh As New DataRowHelper(dr)

                Dim docId As Integer = 0
                Dim docType As Integer = 0

                If dr.Table.Columns.Contains("payment_refdoctype") Then
                    docId = drh.GetValue(Of Integer)("refdocid")
                    docType = drh.GetValue(Of Integer)("payment_refdoctype")
                Else
                    docId = drh.GetValue(Of Integer)("docid")
                    docType = 174
                End If


                If docId > 0 AndAlso docType > 0 Then
                    Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
                    Dim en As SimpleBusinessEntityBase = SimpleBusinessEntityBase.GetEntity(Entity.GetFullClassName(docType), docId)
                    myEntityPanelService.OpenDetailPanel(en)
                End If
            End If
        End Sub
        Private Sub CreateHeader()
            m_grid.RowCount = 2
            m_grid.ColCount = 11

            m_grid.ColWidths(1) = 150
            m_grid.ColWidths(2) = 200
            m_grid.ColWidths(3) = 150
            m_grid.ColWidths(4) = 150
            m_grid.ColWidths(5) = 140
            m_grid.ColWidths(6) = 140
            m_grid.ColWidths(7) = 140
            m_grid.ColWidths(8) = 140
            m_grid.ColWidths(9) = 140
            m_grid.ColWidths(10) = 140
            m_grid.ColWidths(11) = 140

            m_grid.ColStyles(1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(3).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(4).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(5).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid.ColStyles(6).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid.ColStyles(7).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid.ColStyles(8).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid.ColStyles(9).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid.ColStyles(10).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid.ColStyles(11).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

            m_grid.Rows.HeaderCount = 2
            m_grid.Rows.FrozenCount = 2

            Dim indent As String = Space(3)
            m_grid(0, 1).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.AdvmCode}") '"�Ţ����Թ���ͧ����"
            m_grid(0, 2).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.AdvmName}") '"�����Թ���ͧ����"
            m_grid(0, 3).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.AdvmDate}") '"�ѹ����Թ���ͧ����"
            m_grid(0, 4).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.PVcode}") '"��Ӥѭ����"

            m_grid(0, 7).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.AdvmOpen}") '"�Թ���ͧ���� ¡��"
            m_grid(0, 8).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.AdvmAmt}") '"ǧ�Թ���ͧ����"
            m_grid(0, 10).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.AdvmAmt}") '"ǧ�Թ���ͧ����"
            m_grid(0, 11).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.Note}") '"�����˵�"

            m_grid(1, 1).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.DocDate}")  '"�Ţ����͡���"
            m_grid(1, 2).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.DocCode}") '"�����͡�����ҧ�ԧ"
            m_grid(1, 3).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.DocType}") '"�������͡���"
            m_grid(1, 4).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.PVcode}") '"��Ӥѭ����"
            m_grid(1, 5).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.CCCode}") '"���� Cost Cenetr"
            m_grid(1, 6).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.CCName}") '"���� Cost Cenetr"

            m_grid(1, 9).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.PayAmt}") '"�ʹ�Ѵ����"
            m_grid(1, 10).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.Remain}") '"�ʹ�������"
            m_grid(1, 11).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.Note}") '"�����˵�"

            m_grid(2, 1).Text = indent & indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.ItemCode}") '"�����Թ���"
            m_grid(2, 2).Text = indent & indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.ItemName}") '"��������´"

            m_grid(2, 11).Text = indent & indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.Note}") '"�����˵�"

            m_grid(0, 1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(0, 3).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

            m_grid(0, 7).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid(0, 8).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right

            m_grid(1, 1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(1, 2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(1, 3).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(1, 4).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(1, 5).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(1, 6).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

            m_grid(1, 7).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid(1, 8).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid(1, 9).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
            m_grid(1, 10).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right

            m_grid(2, 1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
            m_grid(2, 2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

        End Sub
        Private Sub PopulateData()
            m_hashData = New Hashtable
            Dim AdvmDt As DataTable = Me.DataSet.Tables(0)

            'Dim RefDocDt As DataTable = Me.DataSet.Tables(2)
            'Dim ItemDt As DataTable = Me.DataSet.Tables(3)
            Dim currentAdvmCode As String = ""
            Dim currentRefDocCode As String = ""
            Dim currentItemCode As String = ""
            Dim tmpAdvmRemainAmt As Decimal = 0
            Dim tmpInitAdvmAmt As Decimal = 0
            Dim tmpPayAmt As Decimal = 0
            Dim tmpCriteria As String = ""

            Dim currAdvmIndex As Integer = -1
            Dim currRefDocIndex As Integer = -1
            Dim currItemIndex As Integer = -1
            Dim indent As String = Space(3)

            Dim sumAdvmNetAmt As Decimal = 0
            Dim sumAdvmOpbAmt As Decimal = 0
            Dim sumPaymentAmt As Decimal = 0
            Dim sumPaymentNetAmt As Decimal = 0
            Dim myRefDocRow As DataRow()
            Dim PayRow As DataRow()
            Dim PayAmt As Decimal = 0

            Dim PaymentOPBAmt As Decimal = 0
            Dim ADVisOPB As Boolean = False
            For Each ADVMrow As DataRow In AdvmDt.Rows
                If ADVMrow("DocId").ToString <> currentAdvmCode Then

                    PayRow = Me.DataSet.Tables(4).Select("ADVMId=" & CInt(ADVMrow("DocId")))
                    PayAmt = 0
                    If PayRow.Length > 0 Then
                        PayAmt = CDec(PayRow(0)("PayAmt"))
                    End If
                    ADVisOPB = CBool(ADVMrow("isOPB"))

                    myRefDocRow = Me.DataSet.Tables(2).Select("AdvmId=" & CInt(ADVMrow("DocId")))

                    PaymentOPBAmt = 0
                    For Each RefDocRow As DataRow In myRefDocRow
                        If CBool(RefDocRow("IsOpb")) Then
                            PaymentOPBAmt += RefDocRow("PayOPBAmt")
                        End If
                    Next

                    If (ADVisOPB AndAlso (((CDec(ADVMrow("AdvmAmt")) - PaymentOPBAmt)) <> 0)) OrElse Not (ADVisOPB) Then

                        tmpAdvmRemainAmt = 0
                        tmpInitAdvmAmt = 0
                        tmpPayAmt = 0
                        m_grid.RowCount += 1
                        currAdvmIndex = m_grid.RowCount
                        m_hashData(m_grid.RowCount) = ADVMrow
                        m_grid.RowStyles(currAdvmIndex).BackColor = Color.FromArgb(128, 255, 128)
                        m_grid.RowStyles(currAdvmIndex).Font.Bold = True
                        m_grid.RowStyles(currAdvmIndex).ReadOnly = True
                        m_grid(currAdvmIndex, 1).CellValue = ADVMrow("DocCode")
                        m_grid(currAdvmIndex, 2).CellValue = ADVMrow("DocName")
                        m_grid(currAdvmIndex, 3).CellValue = CDate(ADVMrow("DocDate")).ToShortDateString()
                        m_grid(currAdvmIndex, 4).CellValue = ADVMrow("PVcode")
                        m_grid(currAdvmIndex, 11).CellValue = ADVMrow("advm_note")

                        If IsNumeric(ADVMrow("AdvmAmt")) Then


                            If ADVisOPB Then
                                m_grid(currAdvmIndex, 7).CellValue = Configuration.FormatToString(CDec(ADVMrow("AdvmAmt")) - PaymentOPBAmt, DigitConfig.Price)
                                tmpAdvmRemainAmt = CDec(ADVMrow("AdvmAmt")) - PaymentOPBAmt
                                tmpInitAdvmAmt = CDec(ADVMrow("AdvmAmt")) - PaymentOPBAmt

                                sumAdvmOpbAmt += CDec(ADVMrow("AdvmAmt")) - PaymentOPBAmt
                            Else
                                m_grid(currAdvmIndex, 8).CellValue = Configuration.FormatToString(CDec(ADVMrow("AdvmAmt")), DigitConfig.Price)
                                tmpAdvmRemainAmt = CDec(ADVMrow("AdvmAmt"))
                                tmpInitAdvmAmt = CDec(ADVMrow("AdvmAmt"))

                                sumAdvmNetAmt += CDec(ADVMrow("AdvmAmt"))
                            End If



                        End If
                        currentAdvmCode = ADVMrow("DocId").ToString

                        If PaymentOPBAmt <> 0 Then

                            'tmpAdvmRemainAmt -= PaymentOPBAmt
                            tmpPayAmt += PaymentOPBAmt




                            If Not (ADVisOPB) Then
                                sumPaymentNetAmt += PaymentOPBAmt
                                'sumPaymentAmt += PaymentOPBAmt

                                m_grid.RowCount += 1
                                currRefDocIndex = m_grid.RowCount
                                m_grid.RowStyles(currRefDocIndex).BackColor = Color.FromArgb(250, 227, 197)
                                m_grid.RowStyles(currRefDocIndex).Font.Bold = True
                                m_grid.RowStyles(currRefDocIndex).ReadOnly = True
                                m_grid(currRefDocIndex, 8).CellValue = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.OpenningBalance}")
                                m_grid(currRefDocIndex, 8).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
                                m_grid(currRefDocIndex, 9).CellValue = Configuration.FormatToString(PaymentOPBAmt, DigitConfig.Price)
                                m_grid(currRefDocIndex, 10).CellValue = Configuration.FormatToString(CDec(tmpAdvmRemainAmt), DigitConfig.Price)

                            End If

                        End If
                        For Each RefDocRow As DataRow In myRefDocRow
                            If Not CBool(RefDocRow("IsOpb")) Then

                                m_hashData(m_grid.RowCount) = RefDocRow

                                'If Not (ADVisOPB) Then
                                m_grid.RowCount += 1
                                currRefDocIndex = m_grid.RowCount
                                m_grid.RowStyles(currRefDocIndex).BackColor = Color.FromArgb(250, 227, 197)
                                m_grid.RowStyles(currRefDocIndex).Font.Bold = True
                                m_grid.RowStyles(currRefDocIndex).ReadOnly = True
                                'End If

                                'If Not (ADVisOPB) Then
                                If IsDate(RefDocRow("RefDocDate")) Then
                                    m_grid(currRefDocIndex, 1).CellValue = indent & CDate(RefDocRow("RefDocDate")).ToShortDateString
                                End If
                                If Not RefDocRow.IsNull("RefDocCode") Then
                                    m_grid(currRefDocIndex, 2).CellValue = indent & RefDocRow("RefDocCode").ToString
                                End If
                                If Not RefDocRow.IsNull("RefDocType") Then
                                    m_grid(currRefDocIndex, 3).CellValue = indent & RefDocRow("RefDocType").ToString
                                End If
                                If Not RefDocRow.IsNull("CCCode") Then
                                    m_grid(currRefDocIndex, 5).CellValue = indent & RefDocRow("CCCode").ToString
                                End If
                                If Not RefDocRow.IsNull("CCName") Then
                                    m_grid(currRefDocIndex, 6).CellValue = indent & RefDocRow("CCName").ToString
                                End If
                                If Not RefDocRow.IsNull("Note") Then
                                    m_grid(currRefDocIndex, 11).CellValue = indent & RefDocRow("Note").ToString
                                End If
                                'End If



                                If IsNumeric(RefDocRow("PayAmt")) Then

                                    'If Not (ADVisOPB) Then
                                    m_grid(currRefDocIndex, 9).CellValue = indent & Configuration.FormatToString(CDec(RefDocRow("PayAmt")), DigitConfig.Price)
                                    'End If

                                    tmpAdvmRemainAmt -= CDec(RefDocRow("PayAmt"))
                                    tmpPayAmt += CDec(RefDocRow("PayAmt"))

                                    sumPaymentAmt += CDec(RefDocRow("PayAmt"))
                                    sumPaymentNetAmt += CDec(RefDocRow("PayAmt"))
                                End If

                                'If Not (ADVisOPB) Then
                                If IsNumeric(tmpAdvmRemainAmt) Then
                                    m_grid(currRefDocIndex, 10).CellValue = indent & Configuration.FormatToString(CDec(tmpAdvmRemainAmt), DigitConfig.Price)
                                End If
                                m_grid(currRefDocIndex, 4).CellValue = indent & RefDocRow("PV").ToString
                                'End If

                                'If Not (ADVisOPB) Then
                                Dim myItemRow As DataRow() = Me.DataSet.Tables(3).Select("StockId=" & CStr(RefDocRow("RefDocId")))
                                For Each ItemRow As DataRow In myItemRow
                                    If Not ItemRow.IsNull("StockCode") Then
                                        m_grid.RowCount += 1
                                        currItemIndex = m_grid.RowCount
                                        m_grid.RowStyles(currItemIndex).ReadOnly = True
                                        If Not ItemRow.IsNull("ItemCode") Then
                                            m_grid(currItemIndex, 1).CellValue = indent & indent & ItemRow("ItemCode").ToString
                                        End If
                                        If Not ItemRow.IsNull("ItemName") Then
                                            m_grid(currItemIndex, 2).CellValue = indent & indent & ItemRow("ItemName").ToString
                                        End If
                                        If Not ItemRow.IsNull("Amount") Then
                                            m_grid(currItemIndex, 9).CellValue = indent & indent & Configuration.FormatToString(CDec(ItemRow("Amount")), DigitConfig.Price)
                                        End If
                                        If Not ItemRow.IsNull("Note") Then
                                            m_grid(currItemIndex, 11).CellValue = indent & indent & ItemRow("Note").ToString
                                        End If
                                    End If
                                Next
                                'End If

                            End If
                        Next

                        If ADVisOPB Then
                            m_grid(currAdvmIndex, 9).CellValue = Configuration.FormatToString(sumPaymentAmt, DigitConfig.Price)
                            m_grid(currAdvmIndex, 10).CellValue = Configuration.FormatToString((CDec(ADVMrow("AdvmAmt")) - PaymentOPBAmt) - sumPaymentAmt, DigitConfig.Price)
                        Else
                            m_grid(currAdvmIndex, 9).CellValue = Configuration.FormatToString(sumPaymentAmt + PaymentOPBAmt, DigitConfig.Price)
                            m_grid(currAdvmIndex, 10).CellValue = Configuration.FormatToString((CDec(ADVMrow("AdvmAmt")) - sumPaymentAmt) - PaymentOPBAmt, DigitConfig.Price)
                        End If

                        m_grid(currAdvmIndex, 1).Tag = "Font.Bold"
                        sumPaymentAmt = 0


                    End If
                End If
            Next
            m_grid.RowCount += 1
            currAdvmIndex = m_grid.RowCount
            m_grid.RowStyles(currAdvmIndex).ReadOnly = True
            m_grid(currAdvmIndex, 6).CellValue = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.Total}") '"���"
            m_grid(currAdvmIndex, 7).CellValue = Configuration.FormatToString(sumAdvmOpbAmt, DigitConfig.Price)
            m_grid(currAdvmIndex, 8).CellValue = Configuration.FormatToString(sumAdvmNetAmt, DigitConfig.Price)
            m_grid(currAdvmIndex, 9).CellValue = Configuration.FormatToString(sumPaymentNetAmt, DigitConfig.Price)
            m_grid(currAdvmIndex, 10).CellValue = Configuration.FormatToString((sumAdvmOpbAmt + sumAdvmNetAmt) - sumPaymentNetAmt, DigitConfig.Price)
            m_grid(currAdvmIndex, 1).Tag = "Font.Bold"

        End Sub
#End Region#Region "Shared"
#End Region#Region "Properties"        Public Overrides ReadOnly Property ClassName() As String
            Get
                Return "RptAdvanceMoney"
            End Get
        End Property
        Public Overrides ReadOnly Property DetailPanelTitle() As String
            Get
                Return "${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.DetailLabel}"
            End Get
        End Property
        Public Overrides ReadOnly Property DetailPanelIcon() As String
            Get
                Return "Icons.16x16.RptAdvanceMoney"
            End Get
        End Property
        Public Overrides ReadOnly Property ListPanelIcon() As String
            Get
                Return "Icons.16x16.RptAdvanceMoney"
            End Get
        End Property
        Public Overrides ReadOnly Property ListPanelTitle() As String
            Get
                Return "${res:Longkong.Pojjaman.BusinessLogic.RptAdvanceMoney.ListLabel}"
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
            Return "RptAdvanceMoney"
        End Function
        Public Overrides Function GetDefaultForm() As String
            Return "RptAdvanceMoney"
        End Function
        Public Overrides Function GetDocPrintingEntries() As DocPrintingItemCollection
            Dim dpiColl As New DocPrintingItemCollection
            Dim dpi As DocPrintingItem

            For Each fixDpi As DocPrintingItem In Me.FixValueCollection
                dpiColl.Add(fixDpi)
            Next

            Dim n As Integer = 0
            Dim fn As Font
            For rowIndex As Integer = 3 To m_grid.RowCount
                If Not m_grid(rowIndex, 1).Tag Is Nothing Then
                    fn = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
                Else
                    fn = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
                End If

                dpi = New DocPrintingItem
                dpi.Mapping = "col0"
                dpi.Value = m_grid(rowIndex, 1).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col1"
                dpi.Value = m_grid(rowIndex, 2).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col2"
                dpi.Value = m_grid(rowIndex, 3).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col3"
                dpi.Value = m_grid(rowIndex, 4).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col4"
                dpi.Value = m_grid(rowIndex, 5).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col5"
                dpi.Value = m_grid(rowIndex, 6).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col6"
                dpi.Value = m_grid(rowIndex, 7).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col7"
                dpi.Value = m_grid(rowIndex, 8).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col8"
                dpi.Value = m_grid(rowIndex, 9).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                dpi = New DocPrintingItem
                dpi.Mapping = "col9"
                dpi.Value = m_grid(rowIndex, 10).CellValue
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "Item"
                dpi.Font = fn
                dpiColl.Add(dpi)

                n += 1
            Next

            Return dpiColl
        End Function
#End Region
    End Class
End Namespace

