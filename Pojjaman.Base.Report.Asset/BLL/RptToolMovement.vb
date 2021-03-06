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

Namespace Longkong.Pojjaman.BusinessLogic
  Public Class RptToolMovement
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
      Dim lkg As Longkong.Pojjaman.Gui.Components.LKGrid = CType(m_grid, Longkong.Pojjaman.Gui.Components.LKGrid)
      lkg.DefaultBehavior = False
      lkg.HilightWhenMinus = True
      lkg.Init()
      lkg.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.SystemTheme
      Dim tm As New TreeManager(GetSimpleSchemaTable, New TreeGrid)
      ListInGrid(tm)
      lkg.TreeTableStyle = CreateSimpleTableStyle()
      lkg.TreeTable = tm.Treetable
      
        lkg.Rows.HeaderCount = 2
        lkg.Rows.FrozenCount = 2

      lkg.Refresh()
    End Sub
    Private Sub CellDblClick(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs)
      Dim dr As DataRow = CType(m_hashData(e.RowIndex), DataRow)
      If dr Is Nothing Then
        Return
      End If

      Dim drh As New DataRowHelper(dr)

      Dim docId As Integer = drh.GetValue(Of Integer)("DocID")
      Dim docType As Integer = drh.GetValue(Of Integer)("DocType")

      Trace.WriteLine(docId.ToString & ":" & docType.ToString)

      If docId > 0 AndAlso docType > 0 Then
        Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
        Dim en As SimpleBusinessEntityBase = SimpleBusinessEntityBase.GetEntity(Entity.GetFullClassName(docType), docId)
        myEntityPanelService.OpenDetailPanel(en)
      End If
    End Sub
    Public Overrides Sub ListInGrid(ByVal tm As TreeManager)
      Me.m_treemanager = tm
      Me.m_treemanager.Treetable.Clear()
      CreateHeader()
      PopulateData()
    End Sub
    Private Sub CreateHeader()
      If Me.m_treemanager Is Nothing Then
        Return
      End If

      Dim indent As String = Space(3)

     
        ' Level 1
        Dim tr As TreeRow = Me.m_treemanager.Treetable.Childs.Add
      tr("col0") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptToolStatus.ToolCode}") '"����"
      tr("col1") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptToolStatus.ToolName}") '"����"
      tr("col2") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.OwnerCC}") '"CC��Ңͧ"

        ' Level 2
        tr = Me.m_treemanager.Treetable.Childs.Add
      tr("col0") = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEQTIncome.DocCode}") '"�����͡���"
      tr("col1") = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptSpecialJournalEntry.DocDate}") '"�ѹ���"
      tr("col2") = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEQTIncome.DocType}") '"�������͡���"
      tr("col3") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentMovement.ToolStatus}") '"ʶҹ�����ͧ���"
      tr("col4") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEQTIncome.ToCC}") '"CC�Ѻ"
      tr("col5") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptToolMovement.Qty}") '"��ª��ͼ����"
      tr("col6") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptToolStatus.Unit}") '"��Ť�ҫ��Ͷ֧��˹�"
      tr("col7") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentMovement.RentalAmount}") '"���ի���(�֧��˹�)"

    End Sub
    Private Sub PopulateData()
      Dim dtTool As DataTable = Me.DataSet.Tables(0)
      Dim dtDoc As DataTable = Me.DataSet.Tables(1)

      If dtTool.Rows.Count = 0 Then
        Return
      End If

      Dim indent As String = Space(3)
      Dim trToolCode As TreeRow
      Dim trEqtDoc As TreeRow
      Dim currentStockCode As String = ""
      Dim currStockId As String = ""
      Dim rowIndex As Integer = 0
      m_hashData = New Hashtable

      For Each tRow As DataRow In dtTool.Rows
        Dim dht As New DataRowHelper(tRow)

        trToolCode = Me.Treemanager.Treetable.Childs.Add
        trToolCode.Tag = "Font.Bold"
        trToolCode.Tag = tRow

        trToolCode("col0") = dht.GetValue(Of String)("tool_code")
        trToolCode("col1") = dht.GetValue(Of String)("tool_name")
        trToolCode("col2") = dht.GetValue(Of String)("CC")
        
        trToolCode.State = RowExpandState.Expanded
        For Each eqtRow As DataRow In dtDoc.Select("tool_id=" & tRow("tool_id").ToString)
          Dim deh As New DataRowHelper(eqtRow)

          If Not trToolCode Is Nothing Then
            trEqtDoc = trToolCode.Childs.Add
            trEqtDoc.Tag = eqtRow
            trEqtDoc("col0") = indent & deh.GetValue(Of String)("eqtstock_code")
            trEqtDoc("col1") = deh.GetValue(Of Date)("eqtstock_docdate").ToShortDateString
            trEqtDoc("col2") = indent & deh.GetValue(Of String)("DoctypeName")
            trEqtDoc("col3") = deh.GetValue(Of String)("eqttoolstatus")
            trEqtDoc("col4") = deh.GetValue(Of String)("tocc")
            trEqtDoc("col5") = Configuration.FormatToString(deh.GetValue(Of Decimal)("eqtstocki_qty"), DigitConfig.Int)
            trEqtDoc("col6") = deh.GetValue(Of String)("unit_name")
            trEqtDoc("col7") = Configuration.FormatToString(deh.GetValue(Of Decimal)("eqtstocki_Amount"), DigitConfig.Price)

          End If
        Next

      Next

      Dim lineNumber As Integer = 1
      For Each tr As TreeRow In Me.m_treemanager.Treetable.Rows
        If Not tr.Tag Is Nothing AndAlso TypeOf tr.Tag Is DataRow Then
          m_hashData(lineNumber) = CType(tr.Tag, DataRow)
        End If

        lineNumber += 1
      Next
    End Sub
    Private Function SearchTag(ByVal id As Integer) As TreeRow
      If Me.m_treemanager Is Nothing Then
        Return Nothing
      End If
      Dim dt As TreeTable = m_treemanager.Treetable
      For Each row As TreeRow In dt.Rows
        If IsNumeric(row.Tag) AndAlso CInt(row.Tag) = id Then
          Return row
        End If
      Next
    End Function
    Public Overrides Function GetSimpleSchemaTable() As TreeTable
      Dim myDatatable As New TreeTable("Report")
      myDatatable.Columns.Add(New DataColumn("col0", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("col1", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("col2", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("col3", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("col4", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("col5", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("col6", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("col7", GetType(String)))

      Return myDatatable
    End Function
    Public Overrides Function CreateSimpleTableStyle() As DataGridTableStyle
      Dim dst As New DataGridTableStyle
      dst.MappingName = "Report"
      Dim widths As New ArrayList
      Dim iCol As Integer = 7 'IIf(Me.ShowDetailInGrid = 0, 6, 7)

      widths.Add(100)
      widths.Add(100)
      widths.Add(220)
      widths.Add(120)
      widths.Add(200)
      widths.Add(100)
      widths.Add(70)
      widths.Add(120)

      For i As Integer = 0 To iCol
        If i = 1 Then
          'If m_showDetailInGrid <> 0 Then
          Dim cs As New PlusMinusTreeTextColumn
          cs.MappingName = "col" & i
          cs.HeaderText = ""
          cs.Width = CInt(widths(i))
          cs.NullText = ""
          cs.Alignment = HorizontalAlignment.Left
          cs.ReadOnly = True
          cs.Format = "s"
          AddHandler cs.CheckCellHilighted, AddressOf Me.SetHilightValues
          dst.GridColumnStyles.Add(cs)
        Else
          Dim cs As New TreeTextColumn(i, True, Color.Khaki)
          cs.MappingName = "col" & i
          cs.HeaderText = ""
          cs.Width = CInt(widths(i))
          cs.NullText = ""
          cs.Alignment = HorizontalAlignment.Left
          'If Me.m_showDetailInGrid <> 0 Then
          Select Case i
            Case 0, 1, 2, 3, 4, 6
              cs.Alignment = HorizontalAlignment.Left
              cs.DataAlignment = HorizontalAlignment.Left
              cs.Format = "s"
            Case Else
              cs.Alignment = HorizontalAlignment.Right
              cs.DataAlignment = HorizontalAlignment.Right
              cs.Format = "d"
          End Select

          cs.ReadOnly = True

          AddHandler cs.CheckCellHilighted, AddressOf Me.SetHilightValues
          dst.GridColumnStyles.Add(cs)
        End If
      Next

      Return dst
    End Function
    Public Overrides Sub SetHilightValues(ByVal sender As Object, ByVal e As DataGridHilightEventArgs)
      e.HilightValue = False
      If e.Row <= 1 Then
        e.HilightValue = True
      End If
    End Sub
#End Region#Region "Shared"
#End Region#Region "Properties"    Public Overrides ReadOnly Property ClassName() As String
      Get
        Return "RptToolMovement"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.RptToolMovement.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.RptToolMovement"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.RptToolMovement"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.RptToolMovement.ListLabel}"
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

      For rowIndex As Integer = 3 To Me.m_grid.RowCount
        For colIndex As Integer = 1 To Me.m_grid.ColCount
          dpi = New DocPrintingItem
          dpi.Mapping = String.Format("col{0}", colIndex)
          dpi.Value = m_grid(rowIndex, colIndex).CellValue
          dpi.DataType = "System.Sting"
          dpi.Row = rowIndex - 2
          dpi.Table = "Item"
          dpiColl.Add(dpi)
        Next
      Next

      'Dim i As Integer = 0
      'Dim indent As String = Space(3)

      'Dim line As Decimal = 0

      'For Each itemrow As TreeRow In Me.Treemanager.Treetable.Childs
      'For i As Decimal = 0 To Me.Treemanager.Treetable.Childs.Count - 2
      '  Dim itemrow As TreeRow = Me.Treemanager.Treetable.Childs.Item(i + 2)
      '  Dim dhstockrow As New DataRowHelper(CType(itemrow, DataRow))

      '  'Item.LineNumber
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "linenumber"
      '  dpi.Value = line + 1
      '  dpi.DataType = "System.Sting"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'stock.DocCode
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "Stock.DocCode"
      '  dpi.Value = dhstockrow.GetValue(Of String)("Col0")
      '  dpi.DataType = "System.String"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'Item.DocDate
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "Stock.DocDate"
      '  dpi.Value = dhstockrow.GetValue(Of String)("Col1")
      '  'dpi.Value = dhstockrow.GetValue(Of Date)("Col1").ToShortDateString
      '  dpi.DataType = "System.DateTime"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'GL.DocCode
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "GL.DocCode"
      '  dpi.Value = dhstockrow.GetValue(Of String)("Col2")
      '  dpi.DataType = "System.String"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'Type
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "DocType"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col3")
      '  dpi.DataType = "System.String"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'SupplierCode
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "SupplierCode"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col4")
      '  dpi.DataType = "System.String"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'SupplierName
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "SupplierName"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col5")
      '  dpi.DataType = "System.String"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'StockTaxBase
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "StockTaxBase"
      '  'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("col6"), DigitConfig.Price)
      '  dpi.Value = dhstockrow.GetValue(Of String)("col6")
      '  dpi.DataType = "System.Decimal"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'StockTaxAmt
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "StockTaxAmt"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col7")
      '  'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("stock_taxAmt"), DigitConfig.Price)
      '  dpi.DataType = "System.Decimal"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'StockTaxAmt
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "bfdeferTaxBase"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col8")
      '  'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("bfdeferTaxBase"), DigitConfig.Price)
      '  dpi.DataType = "System.Decimal"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'bfdeferTaxAmt
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "bfdeferTaxAmt"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col9")
      '  'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("bfdeferTaxAmt"), DigitConfig.Price)
      '  dpi.DataType = "System.Decimal"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'duetaxBase
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "duetaxBase"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col10")
      '  'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("duetaxBase"), DigitConfig.Price)
      '  dpi.DataType = "System.Decimal"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'duetaxAmt
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "duetaxAmt"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col11")
      '  'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("duetaxAmt"), DigitConfig.Price)
      '  dpi.DataType = "System.Decimal"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'baldeferTaxBase
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "baldeferTaxBase"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col12")
      '  'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("baldeferTaxBase"), DigitConfig.Price)
      '  dpi.DataType = "System.Decimal"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'baldeferTaxAmt
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "baldeferTaxAmt"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col13")
      '  'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("baldeferTaxAmt"), DigitConfig.Price)
      '  dpi.DataType = "System.Decimal"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  'GlNote
      '  dpi = New DocPrintingItem
      '  dpi.Mapping = "GlNote"
      '  dpi.Value = dhstockrow.GetValue(Of String)("col14")
      '  dpi.DataType = "System.String"
      '  dpi.Row = i + 1
      '  dpi.Table = "Item"
      '  dpiColl.Add(dpi)

      '  line += 1
      '  'add childs
      '  If itemrow IsNot Nothing AndAlso Not itemrow.IsLeafRow Then
      '    For Each paysrow As TreeRow In itemrow.Childs
      '      i += 1
      '      Dim prh As New DataRowHelper(paysrow)


      '      'stock.DocCode
      '      dpi = New DocPrintingItem
      '      dpi.Mapping = "Stock.DocCode"
      '      dpi.Value = indent & prh.GetValue(Of String)("Col0")
      '      dpi.DataType = "System.String"
      '      dpi.Row = i + 1
      '      dpi.Table = "Item"
      '      dpiColl.Add(dpi)

      '      'stock.DocCode
      '      dpi = New DocPrintingItem
      '      dpi.Mapping = "Stock.DocDate"
      '      dpi.Value = indent & prh.GetValue(Of String)("Col1")
      '      dpi.DataType = "System.String"
      '      dpi.Row = i + 1
      '      dpi.Table = "Item"
      '      dpiColl.Add(dpi)

      '      'stock.DocCode
      '      dpi = New DocPrintingItem
      '      dpi.Mapping = "DocType"
      '      dpi.Value = indent & prh.GetValue(Of String)("Col3")
      '      dpi.DataType = "System.String"
      '      dpi.Row = i + 1
      '      dpi.Table = "Item"
      '      dpiColl.Add(dpi)

      '      'SupplierCode
      '      dpi = New DocPrintingItem
      '      dpi.Mapping = "SupplierCode"
      '      dpi.Value = indent & prh.GetValue(Of String)("col4")
      '      dpi.DataType = "System.String"
      '      dpi.Row = i + 1
      '      dpi.Table = "Item"
      '      dpiColl.Add(dpi)

      '      'SupplierName
      '      dpi = New DocPrintingItem
      '      dpi.Mapping = "SupplierName"
      '      dpi.Value = indent & prh.GetValue(Of String)("col5")
      '      dpi.DataType = "System.String"
      '      dpi.Row = i + 1
      '      dpi.Table = "Item"
      '      dpiColl.Add(dpi)

      '      'duetaxBase
      '      dpi = New DocPrintingItem
      '      dpi.Mapping = "duetaxBase"
      '      dpi.Value = prh.GetValue(Of String)("col10")
      '      'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("duetaxBase"), DigitConfig.Price)
      '      dpi.DataType = "System.Decimal"
      '      dpi.Row = i + 1
      '      dpi.Table = "Item"
      '      dpiColl.Add(dpi)

      '      'duetaxAmt
      '      dpi = New DocPrintingItem
      '      dpi.Mapping = "duetaxAmt"
      '      dpi.Value = prh.GetValue(Of String)("col11")
      '      'dpi.Value = Configuration.FormatToString(dhstockrow.GetValue(Of Decimal)("duetaxAmt"), DigitConfig.Price)
      '      dpi.DataType = "System.Decimal"
      '      dpi.Row = i + 1
      '      dpi.Table = "Item"
      '      dpiColl.Add(dpi)

      '    Next
      '  End If
      'Next

      Return dpiColl
    End Function
#End Region
  End Class
End Namespace

