Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Reflection
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.TextHelper
Imports Syncfusion.Windows.Forms.Grid
Imports Longkong.Pojjaman.Services
Imports System.Collections.Generic

Namespace Longkong.Pojjaman.BusinessLogic
  Public Class SumControlAccount
    Public Property IsControl As Boolean
    Public Property OpDr As Decimal
    Public Property OpCr As Decimal
    Public Property DocDr As Decimal
    Public Property DocCr As Decimal
    Public Property SumDr As Decimal
    Public Property SumCr As Decimal
    Public Property Balance As Decimal
    Public Property ParentNode As TreeRow
    Public Property parent As String
    Sub New(ByVal tr As TreeRow, ByVal par As String)
      ParentNode = tr
      parent = par
    End Sub
  End Class
  Public Class RptTrialBalanceByCC
    Inherits Report
    Implements INewReport

#Region "Members"
    Private m_reportColumns As ReportColumnCollection
    Private m_cc As CostCenter
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

#Region "Style"
    Public Shared Function CreateTableStyle() As DataGridTableStyle
      Dim dst As New DataGridTableStyle
      dst.MappingName = "JournalEntryByCCList"
      Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)

      'Dim csLineNumber As New TreeTextColumn
      'csLineNumber.MappingName = "LineNumber"
      'csLineNumber.HeaderText = "#" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.LineNumberHeaderText}")
      'csLineNumber.NullText = ""
      'csLineNumber.Width = 30
      'csLineNumber.DataAlignment = HorizontalAlignment.Center
      'csLineNumber.ReadOnly = True
      'csLineNumber.TextBox.Name = "LineNumber"

      Dim csCode As New TreeTextColumn
      csCode.MappingName = "acct_code"
      csCode.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.DescriptionHeaderText}")
      csCode.NullText = ""
      csCode.Width = 100
      csCode.DataAlignment = HorizontalAlignment.Left
      csCode.Alignment = HorizontalAlignment.Left
      csCode.TextBox.Name = "acct_code"
      csCode.ReadOnly = True

      Dim csName As New PlusMinusTreeTextColumn
      csName.MappingName = "acct_name"
      csName.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitHeaderText}")
      csName.NullText = ""
      csName.Width = 350
      csName.DataAlignment = HorizontalAlignment.Left
      csName.Alignment = HorizontalAlignment.Left
      csName.TextBox.Name = "acct_name"
      csName.ReadOnly = True

      Dim csOpenningDocDr As New TreeTextColumn
      csOpenningDocDr.MappingName = "openningdocdr"
      csOpenningDocDr.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitPriceHeaderText}")
      csOpenningDocDr.NullText = ""
      csOpenningDocDr.Width = 100
      csOpenningDocDr.DataAlignment = HorizontalAlignment.Right
      csOpenningDocDr.Format = "#,###.##"
      csOpenningDocDr.TextBox.Name = "openningdocdr"
      csOpenningDocDr.ReadOnly = True

      Dim csOpenningDocCr As New TreeTextColumn
      csOpenningDocCr.MappingName = "openningdoccr"
      csOpenningDocCr.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitPriceHeaderText}")
      csOpenningDocCr.NullText = ""
      csOpenningDocCr.Width = 100
      csOpenningDocCr.DataAlignment = HorizontalAlignment.Right
      csOpenningDocCr.Format = "#,###.##"
      csOpenningDocCr.TextBox.Name = "openningdoccr"
      csOpenningDocCr.ReadOnly = True

      Dim csDocDr As New TreeTextColumn
      csDocDr.MappingName = "docdr"
      csDocDr.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitPriceHeaderText}")
      csDocDr.NullText = ""
      csDocDr.Width = 100
      csDocDr.DataAlignment = HorizontalAlignment.Right
      csDocDr.Format = "#,###.##"
      csDocDr.TextBox.Name = "docdr"
      csDocDr.ReadOnly = True

      Dim csDocCr As New TreeTextColumn
      csDocCr.MappingName = "doccr"
      csDocCr.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitPriceHeaderText}")
      csDocCr.NullText = ""
      csDocCr.Width = 100
      csDocCr.DataAlignment = HorizontalAlignment.Right
      csDocCr.Format = "#,###.##"
      csDocCr.TextBox.Name = "doccr"
      csDocCr.ReadOnly = True

      Dim csSumDr As New TreeTextColumn
      csSumDr.MappingName = "sumdr"
      csSumDr.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitPriceHeaderText}")
      csSumDr.NullText = ""
      csSumDr.Width = 100
      csSumDr.DataAlignment = HorizontalAlignment.Right
      csSumDr.Format = "#,###.##"
      csSumDr.TextBox.Name = "sumdr"
      csSumDr.ReadOnly = True

      Dim csSumCr As New TreeTextColumn
      csSumCr.MappingName = "sumcr"
      csSumCr.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitPriceHeaderText}")
      csSumCr.NullText = ""
      csSumCr.Width = 100
      csSumCr.DataAlignment = HorizontalAlignment.Right
      csSumCr.Format = "#,###.##"
      csSumCr.TextBox.Name = "sumcr"
      csSumCr.ReadOnly = True

      'Dim csAmount As New TreeTextColumn
      'csAmount.MappingName = "amount"
      'csAmount.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitPriceHeaderText}")
      'csAmount.NullText = ""
      'csAmount.Width = 100
      'csAmount.DataAlignment = HorizontalAlignment.Right
      'csAmount.Format = "#,###.##"
      'csAmount.TextBox.Name = "amount"
      'csAmount.ReadOnly = True

      'Dim csDescription As New TreeTextColumn
      'csDescription.MappingName = "description"
      'csDescription.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.DescriptionHeaderText}")
      'csDescription.NullText = ""
      'csDescription.Width = 200
      'csDescription.DataAlignment = HorizontalAlignment.Left
      'csDescription.TextBox.Name = "description"
      'csDescription.ReadOnly = True

      'Dim csRowType As New TreeTextColumn
      'csRowType.MappingName = "rowType"
      'csRowType.HeaderText = "" 'myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CostControlReportView.UnitPriceHeaderText}")
      'csRowType.NullText = ""
      'csRowType.Width = 0
      'csRowType.TextBox.Name = "rowType"
      'csRowType.ReadOnly = True

      'dst.GridColumnStyles.Add(csLineNumber)
      dst.GridColumnStyles.Add(csCode)
      dst.GridColumnStyles.Add(csName)
      'dst.GridColumnStyles.Add(csAmount)
      'dst.GridColumnStyles.Add(csDescription)
      'dst.GridColumnStyles.Add(csRowType)
      dst.GridColumnStyles.Add(csOpenningDocDr)
      dst.GridColumnStyles.Add(csOpenningDocCr)
      dst.GridColumnStyles.Add(csDocDr)
      dst.GridColumnStyles.Add(csDocCr)
      dst.GridColumnStyles.Add(csSumDr)
      dst.GridColumnStyles.Add(csSumCr)

      Return dst
    End Function
    Public Shared Function GetSchemaTable() As TreeTable
      Dim myDatatable As New TreeTable("JournalEntryByCCList")
      Dim selectedCol As New DataColumn("Selected", GetType(Boolean))
      selectedCol.DefaultValue = False
      myDatatable.Columns.Add(selectedCol)
      'myDatatable.Columns.Add(New DataColumn("LineNumber", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("acct_code", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("acct_name", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("openningdocdr", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("openningdoccr", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("docdr", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("doccr", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("sumdr", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("sumcr", GetType(String)))
      'myDatatable.Columns.Add(New DataColumn("amount", GetType(String)))
      'myDatatable.Columns.Add(New DataColumn("description", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("rowType", GetType(String)))

      Return myDatatable
    End Function
    Private Sub CreateHeader()
      If Me.m_treemanager Is Nothing Then
        Return
      End If
      'Dim indent As String = Space(5)
      ' Level 1.
      Dim tr As TreeRow = Me.m_treemanager.Treetable.Childs.Add
      tr("acct_code") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.AcctCode}") '"���ʺѭ��"
      tr("acct_name") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.AcctName}") '"���ͺѭ��"
      tr("openningdocdr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.OpenningBalance}") '"�ʹ¡��"
      tr("docdr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.Period}") '"�ʹ��ШӧǴ"
      tr("sumdr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.Summary}") '"�ʹ����"

      tr = Me.m_treemanager.Treetable.Childs.Add
      tr("acct_code") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.CostCenterCode}") '"���� Cost Center"
      tr("acct_name") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.CostCenterName}") '"���� Cost Center"
      tr("openningdocdr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DocDr}") '"�ôԵ"
      tr("openningdoccr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DocCr}") '"ഺԵ"
      tr("docdr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DocDr}") '"�ôԵ"
      tr("doccr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DocCr}") '"ഺԵ"
      tr("sumdr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DocDr}") '"�ôԵ"
      tr("sumcr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DocCr}") '"ഺԵ"

      m_grid.CoveredRanges.AddRange(New Syncfusion.Windows.Forms.Grid.GridRangeInfo() _
                                      {Syncfusion.Windows.Forms.Grid.GridRangeInfo.Cells(1, 4, 1, 5), _
                                       Syncfusion.Windows.Forms.Grid.GridRangeInfo.Cells(1, 6, 1, 7), _
                                       Syncfusion.Windows.Forms.Grid.GridRangeInfo.Cells(1, 8, 1, 9)})

      'tr("openningdocdr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.OpenningDocDr}") '"DR ¡��"
      'tr("openningdoccr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.OpenningDocCr}") '"CR ¡��"
      'tr("docdr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DocDr}") '"DR 㹪�ǧ"
      'tr("doccr") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DocCr}") '"CR 㹪�ǧ"
      ' Level 2.
      'tr = Me.m_treemanager.Treetable.Childs.Add
      'tr("col0") = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.DocDate}") '"�ѹ����͡���"
      'tr("col1") = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.DocCode}") '"�Ţ����͡���"
      'tr("col2") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.RefDocCode}") '"�Ţ����͡�����ҧ�ԧ"
      'tr("col3") = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.PVRVCode}") '"�Ţ�����Ӥѭ�Ѻ/����"
      'tr("col4") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.AcctBookName}") '"��ش����ѹ"
      'tr("col5") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.Detail}") '"��������´/��͸Ժ��"
      'tr("col6") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.Debit}") '"ഺԵ"
      'tr("col7") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.Credit}") '"�ôԵ"
      'tr("col8") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.Balance}") '"�ʹ�������"
      'tr("col9") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.CostCenter}") '"CC"
      'tr("col10") = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptJournalEntry.ItemNote}") '"�����˵���¡��"
      'tr("col11") = "id"
      'tr("col12") = "type"
    End Sub
#End Region

#Region "Overrides"
    Public Overrides Function GetSimpleSchemaTable() As Gui.Components.TreeTable
      Return RptTrialBalanceByCC.GetSchemaTable 'BOQ.GetWBSMonitorSchemaTable
    End Function
    Public Overrides Function CreateSimpleTableStyle() As System.Windows.Forms.DataGridTableStyle
      Return RptTrialBalanceByCC.CreateTableStyle 'BOQ.CreateWBSMonitorTableStyle
    End Function
    Private m_grid As Syncfusion.Windows.Forms.Grid.GridControl
    Public Overrides Sub ListInNewGrid(ByVal grid As Syncfusion.Windows.Forms.Grid.GridControl)
      m_grid = grid

      Dim lkg As Longkong.Pojjaman.Gui.Components.LKGrid = CType(m_grid, Longkong.Pojjaman.Gui.Components.LKGrid)
      RemoveHandler m_grid.CellDoubleClick, AddressOf CellDblClick
      AddHandler m_grid.CellDoubleClick, AddressOf CellDblClick
      lkg.DefaultBehavior = False
      lkg.HilightWhenMinus = True
      lkg.Init()
      lkg.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.SystemTheme
      Dim tm As New TreeManager(GetSimpleSchemaTable, New TreeGrid)
      ListInGrid(tm)
      lkg.TreeTableStyle = CreateSimpleTableStyle()
      lkg.TreeTable = tm.Treetable
      'lkg.HideHead = True
      'lkg.Cols.FrozenCount = 2
      'm_grid.Model.Cols.Hidden(m_grid.ColCount) = True
      lkg.Rows.HeaderCount = 2
      lkg.Rows.FrozenCount = 2
      lkg.HilightGroupParentText = True
      lkg.RefreshHeights()
      lkg.Refresh()
    End Sub
    Public Overrides Sub ListInGrid(ByVal tm As Gui.Components.TreeManager)
      Me.m_treemanager = tm
      'If m_cc Is Nothing OrElse Not m_cc.Originated Then
      '  Dim dt As TreeTable = CType(tm.Treetable.Clone, TreeTable)
      '  dt.Clear()
      '  tm.Treetable = dt
      '  Return
      'End If
      'If m_cc.BoqId = 0 Then
      '  Dim dt As TreeTable = CType(tm.Treetable.Clone, TreeTable)
      '  dt.Clear()
      '  tm.Treetable = dt
      '  Return
      'End If
      'If TypeOf Me.Filters(1).Value Is Date Then
      'Dim nodigit As Boolean = False
      'If Me.Filters(5).Name.ToLower = "nodigit" Then
      '  nodigit = CBool(Me.Filters(5).Value)
      'End If
      CreateHeader()
      PopulateData()
      'End If
    End Sub
    Private Sub CellDblClick(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs)
      Dim tr As Object = m_hashData(e.RowIndex)
      If tr Is Nothing Then
        Return
      End If

      'If TypeOf tr Is DataRowHelper Then
      '  Dim drh As DataRowHelper = CType(tr, DataRowHelper)

      '  Dim docId As Integer = drh.GetValue(Of Integer)("gl_refid")
      '  Dim docType As Integer = drh.GetValue(Of Integer)("gl_refdoctype")

      '  If docId > 0 AndAlso docType > 0 Then
      '    Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      '    Dim en As SimpleBusinessEntityBase = SimpleBusinessEntityBase.GetEntity(Entity.GetFullClassName(docType), docId)
      '    myEntityPanelService.OpenDetailPanel(en)
      '  End If
      'End If
    End Sub

    Public Sub PopulateData()
      If Me.m_treemanager Is Nothing Then
        Return
      End If
      Dim dt As TreeTable = Me.m_treemanager.Treetable

      Dim dgt As DigitConfig = DigitConfig.Price
      'If noDigit Then
      '  dgt = DigitConfig.Int
      'End If
      'dt.Clear()
      Dim showTreeParent As Boolean = False
      Dim showAllAccount As Boolean = False
      'Dim showDocument As Boolean = False
      Dim accountCodeStartFilter As String = ""
      Dim accountCodeEndFilter As String = ""

      If Not Me.Filters(8).Value.Equals(DBNull.Value) Then
        showTreeParent = CType(Me.Filters(8).Value, Boolean)
      End If
      'If Not Me.Filters(10).Value.Equals(DBNull.Value) Then
      '  showDocument = CType(Me.Filters(10).Value, Boolean)
      'End If
      If Not Me.Filters(6).Value.Equals(DBNull.Value) Then
        accountCodeStartFilter = CType(Me.Filters(6).Value, String)
      End If

      If Not Me.Filters(7).Value.Equals(DBNull.Value) Then
        accountCodeEndFilter = CType(Me.Filters(7).Value, String)
      End If

      Dim dt2 As DataTable = Me.DataSet.Tables(0)
      Dim dt3 As DataTable = Me.DataSet.Tables(2)
      Dim dt4 As DataTable = CostCenter.GetCostCenterSet
      Dim ccDataSource As DataTable = SetDataSourceFiltered(dt4, "cc_code", accountCodeStartFilter, accountCodeEndFilter)
      Dim newCChash As New Hashtable

      For Each ccRow As DataRow In ccDataSource.Rows
        Dim drh As New DataRowHelper(ccRow)
        newCChash(drh.GetValue(Of Integer)("cc_id")) = drh
      Next

      If dt2.Rows.Count <= 0 Then
        Return
      End If

      '#######################################################################################################
      '#######################################################################################################
      Dim Nodes As New Dictionary(Of String, SumControlAccount)
      Dim myParent As String = ""
      Dim parentNode As TreeRow = Nothing
      Dim childNode As TreeRow = Nothing
      Dim docNode As TreeRow = Nothing
      Dim acctId As Integer = 0
      Dim ccId As Integer = 0
      Dim indent As String = Space(2)
      Dim tr As TreeRow

      Dim ccHash As New Hashtable
      Dim key As String = ""

      m_hashData = New Hashtable
      Dim trIndex As Integer = 0

      Try

        If Not showTreeParent Then
          Dim TotalopacctDr As Decimal
          Dim TotalopacctCr As Decimal
          Dim TotalacctDr As Decimal
          Dim TotalacctCr As Decimal
          Dim TotalSumacctDr As Decimal
          Dim TotalSumacctCr As Decimal
          For Each acctRow As DataRow In dt2.Rows
            tr = dt.Childs.Add
            tr("acct_code") = acctRow("acct_code")            '
            tr("acct_name") = acctRow("acct_name")
            tr("rowType") = "account"
            If Not acctRow.IsNull("acct_id") Then
              acctId = CInt(acctRow("acct_id"))
            End If
            tr.State = RowExpandState.Expanded
            Dim opacctDr As Decimal = 0
            Dim opacctCr As Decimal = 0
            Dim acctDr As Decimal = 0
            Dim acctCr As Decimal = 0
            Dim SumacctDr As Decimal = 0
            Dim SumacctCr As Decimal = 0
            For Each glirow As DataRow In dt3.Select("gli_acct = " & acctId.ToString)
              Dim drh As New DataRowHelper(glirow)
              ccId = drh.GetValue(Of Integer)("gli_cc")
              If ccId > 0 Then
                childNode = tr.Childs.Add
                childNode("acct_code") = indent & CType(newCChash(ccId), DataRowHelper).GetValue(Of String)("cc_code")
                childNode("acct_name") = CType(newCChash(ccId), DataRowHelper).GetValue(Of String)("cc_name")
                childNode("openningdocdr") = Configuration.FormatToString(drh.GetValue(Of Decimal)("openningdocdr"), DigitConfig.Price)
                opacctDr += drh.GetValue(Of Decimal)("openningdocdr")
                childNode("openningdoccr") = Configuration.FormatToString(drh.GetValue(Of Decimal)("openningdoccr"), DigitConfig.Price)
                opacctCr += drh.GetValue(Of Decimal)("openningdoccr")
                childNode("docdr") = Configuration.FormatToString(drh.GetValue(Of Decimal)("docdr"), DigitConfig.Price)
                acctDr += drh.GetValue(Of Decimal)("docdr")
                childNode("doccr") = Configuration.FormatToString(drh.GetValue(Of Decimal)("doccr"), DigitConfig.Price)
                acctCr += drh.GetValue(Of Decimal)("doccr")
                Dim sumdrcr As Decimal = drh.GetValue(Of Decimal)("sumcr") - drh.GetValue(Of Decimal)("sumdr")
                If sumdrcr < 0 Then
                  childNode("sumdr") = Configuration.FormatToString(Math.Abs(sumdrcr), DigitConfig.Price)
                  SumacctDr += Math.Abs(sumdrcr)
                ElseIf sumdrcr > 0 Then
                  childNode("sumcr") = Configuration.FormatToString(Math.Abs(sumdrcr), DigitConfig.Price)
                  SumacctCr += Math.Abs(sumdrcr)
                End If
                childNode("rowType") = "costcenter"
              End If
            Next
            tr("openningdocdr") = Configuration.FormatToString(opacctDr, DigitConfig.Price)
            tr("openningdoccr") = Configuration.FormatToString(opacctCr, DigitConfig.Price)
            tr("docdr") = Configuration.FormatToString(acctDr, DigitConfig.Price)
            tr("doccr") = Configuration.FormatToString(acctCr, DigitConfig.Price)
            TotalopacctDr += opacctDr
            TotalopacctCr += opacctCr
            TotalacctDr += acctDr
            TotalacctCr += acctCr

            Dim sumacctdrcr As Decimal = SumacctCr - SumacctDr
            If sumacctdrcr < 0 Then
              tr("sumdr") = Configuration.FormatToString(Math.Abs(sumacctdrcr), DigitConfig.Price)
              TotalSumacctDr = SumacctDr
            ElseIf sumacctdrcr > 0 Then
              tr("sumcr") = Configuration.FormatToString(Math.Abs(sumacctdrcr), DigitConfig.Price)
              TotalSumacctCr = SumacctCr
            End If
          Next
          tr = dt.Childs.Add
          tr("acct_name") = "���������"
          tr("openningdocdr") = Configuration.FormatToString(TotalopacctDr, DigitConfig.Price)
          tr("openningdoccr") = Configuration.FormatToString(TotalopacctCr, DigitConfig.Price)
          tr("docdr") = Configuration.FormatToString(TotalacctDr, DigitConfig.Price)
          tr("doccr") = Configuration.FormatToString(TotalacctCr, DigitConfig.Price)
          Dim totalsumacctdrcr As Decimal = TotalSumacctCr - TotalSumacctDr
          If totalsumacctdrcr < 0 Then
            tr("sumdr") = Configuration.FormatToString(Math.Abs(totalsumacctdrcr), DigitConfig.Price)
          ElseIf totalsumacctdrcr > 0 Then
            tr("sumcr") = Configuration.FormatToString(Math.Abs(totalsumacctdrcr), DigitConfig.Price)
          End If
        Else
          Dim TotalopacctDr As Decimal
          Dim TotalopacctCr As Decimal
          Dim TotalacctDr As Decimal
          Dim TotalacctCr As Decimal
          Dim TotalSumacctDr As Decimal
          Dim TotalSumacctCr As Decimal
          For Each acctRow As DataRow In dt2.Rows

            If CInt(acctRow("acct_level")) = 0 Then
              parentNode = dt.Childs.Add
            Else
              myParent = acctRow("Parent")
              Try
                parentNode = Nodes.Item(myParent).ParentNode.Childs.Add
              Catch ex As Exception

              End Try
            End If

            If Not parentNode Is Nothing Then
              Dim path As String = (CStr(acctRow("acct_path")))
              Nodes.Add(path, New SumControlAccount(parentNode, myParent))
              tr = parentNode
              tr("acct_code") = acctRow("acct_code")            '
              tr("acct_name") = acctRow("acct_name")
              tr("rowType") = "account"
              If Not acctRow.IsNull("acct_id") Then
                acctId = CInt(acctRow("acct_id"))
              End If
              tr.State = RowExpandState.Expanded
              Dim opacctDr As Decimal = 0
              Dim opacctCr As Decimal = 0
              Dim acctDr As Decimal = 0
              Dim acctCr As Decimal = 0
              Dim SumacctDr As Decimal = 0
              Dim SumacctCr As Decimal = 0
              Dim isControl As Boolean = True
              For Each glirow As DataRow In dt3.Select("gli_acct = " & acctId.ToString)
                Dim drh As New DataRowHelper(glirow)
                ccId = drh.GetValue(Of Integer)("gli_cc")
                isControl = False
                If ccId > 0 Then
                  childNode = tr.Childs.Add
                  childNode("acct_code") = indent & CType(newCChash(ccId), DataRowHelper).GetValue(Of String)("cc_code")
                  childNode("acct_name") = CType(newCChash(ccId), DataRowHelper).GetValue(Of String)("cc_name")
                  childNode("openningdocdr") = Configuration.FormatToString(drh.GetValue(Of Decimal)("openningdocdr"), DigitConfig.Price)
                  opacctDr += drh.GetValue(Of Decimal)("openningdocdr")
                  childNode("openningdoccr") = Configuration.FormatToString(drh.GetValue(Of Decimal)("openningdoccr"), DigitConfig.Price)
                  opacctCr += drh.GetValue(Of Decimal)("openningdoccr")
                  childNode("docdr") = Configuration.FormatToString(drh.GetValue(Of Decimal)("docdr"), DigitConfig.Price)
                  acctDr += drh.GetValue(Of Decimal)("docdr")
                  childNode("doccr") = Configuration.FormatToString(drh.GetValue(Of Decimal)("doccr"), DigitConfig.Price)
                  acctCr += drh.GetValue(Of Decimal)("doccr")
                  Dim sumdrcr As Decimal = drh.GetValue(Of Decimal)("sumcr") - drh.GetValue(Of Decimal)("sumdr")
                  If sumdrcr < 0 Then
                    childNode("sumdr") = Configuration.FormatToString(Math.Abs(sumdrcr), DigitConfig.Price)
                    SumacctDr += Math.Abs(sumdrcr)
                  ElseIf sumdrcr > 0 Then
                    childNode("sumcr") = Configuration.FormatToString(Math.Abs(sumdrcr), DigitConfig.Price)
                    SumacctCr += Math.Abs(sumdrcr)
                  End If
                  childNode("rowType") = "costcenter"
                End If
                tr("openningdocdr") = Configuration.FormatToString(opacctDr, DigitConfig.Price)
                tr("openningdoccr") = Configuration.FormatToString(opacctCr, DigitConfig.Price)
                tr("docdr") = Configuration.FormatToString(acctDr, DigitConfig.Price)
                tr("doccr") = Configuration.FormatToString(acctCr, DigitConfig.Price)
                TotalopacctDr += opacctDr
                TotalopacctCr += opacctCr
                TotalacctDr += acctDr
                TotalacctCr += acctCr

                Dim sumacctdrcr As Decimal = SumacctCr - SumacctDr
                If sumacctdrcr < 0 Then
                  tr("sumdr") = Configuration.FormatToString(Math.Abs(sumacctdrcr), DigitConfig.Price)
                  TotalSumacctDr = SumacctDr
                ElseIf sumacctdrcr > 0 Then
                  tr("sumcr") = Configuration.FormatToString(Math.Abs(sumacctdrcr), DigitConfig.Price)
                  TotalSumacctCr = SumacctCr
                End If
              Next
              Dim sca As SumControlAccount = Nodes.Item(path)
              sca.IsControl = isControl
              sca.OpDr += opacctDr
              sca.OpCr += opacctCr
              sca.DocDr += acctDr
              sca.DocCr += acctCr
              sca.SumDr += SumacctDr
              sca.SumCr += SumacctCr
              If myParent <> "|0|" AndAlso myParent.Length > 0 Then
                Dim parPath As String = myParent
                While parPath <> "|0|"
                  Dim psca As SumControlAccount = Nodes.Item(parPath)
                  psca.OpDr += sca.OpDr
                  psca.OpCr += sca.OpCr
                  psca.DocDr += sca.DocDr
                  psca.DocCr += sca.DocCr
                  psca.SumDr += sca.SumDr
                  psca.SumCr += sca.SumCr

                  parPath = parPath.Remove(parPath.LastIndexOf("||")) & "|"

                End While
              End If

            End If
          Next
          ''������
          'For Each kv As KeyValuePair(Of String, SumControlAccount) In Nodes
          '  Dim sca As SumControlAccount = kv.Value
          '  Do While sca.parent.Length > 0 AndAlso sca.parent <> "|0|"

          '  Loop
          '  If sca.parent.Length > 0 AndAlso sca.parent <> "|0|" Then
          '    Dim psca As SumControlAccount = Nodes.Item(sca.parent)
          '    psca.OpDr += sca.OpDr
          '    psca.OpCr += sca.OpCr
          '    psca.DocDr += sca.DocDr
          '    psca.DocCr += sca.DocCr
          '    psca.SumDr += sca.SumDr
          '    psca.SumCr += sca.SumCr
          '  End If
          'Next
          '�����
          For Each kv As KeyValuePair(Of String, SumControlAccount) In Nodes
            Dim sca As SumControlAccount = kv.Value
            tr = sca.ParentNode
            tr("openningdocdr") = Configuration.FormatToString(sca.OpDr, DigitConfig.Price)
            tr("openningdoccr") = Configuration.FormatToString(sca.OpCr, DigitConfig.Price)
            tr("docdr") = Configuration.FormatToString(sca.DocDr, DigitConfig.Price)
            tr("doccr") = Configuration.FormatToString(sca.DocCr, DigitConfig.Price)
            Dim sumCacctdrcr As Decimal = sca.SumCr - sca.SumDr
            If sumCacctdrcr < 0 Then
              tr("sumdr") = Configuration.FormatToString(Math.Abs(sumCacctdrcr), DigitConfig.Price)
            ElseIf sumCacctdrcr > 0 Then
              tr("sumcr") = Configuration.FormatToString(Math.Abs(sumCacctdrcr), DigitConfig.Price)
            End If
          Next

          tr = dt.Childs.Add
          tr("acct_name") = "���������"
          tr("openningdocdr") = Configuration.FormatToString(TotalopacctDr, DigitConfig.Price)
          tr("openningdoccr") = Configuration.FormatToString(TotalopacctCr, DigitConfig.Price)
          tr("docdr") = Configuration.FormatToString(TotalacctDr, DigitConfig.Price)
          tr("doccr") = Configuration.FormatToString(TotalacctCr, DigitConfig.Price)
          Dim totalsumacctdrcr As Decimal = TotalSumacctCr - TotalSumacctDr
          If totalsumacctdrcr < 0 Then
            tr("sumdr") = Configuration.FormatToString(Math.Abs(totalsumacctdrcr), DigitConfig.Price)
          ElseIf totalsumacctdrcr > 0 Then
            tr("sumcr") = Configuration.FormatToString(Math.Abs(totalsumacctdrcr), DigitConfig.Price)
          End If
        End If

        Dim i As Integer = 0
        'For Each row As DataRow In dt.Rows
        '  i += 1
        '  row("boqi_linenumber") = i
        'Next
        'm_hashData = New Hashtable
        For Each row As TreeRow In dt.Rows
          i += 1
          'row("linenumber") = i
          'If Not row.Tag Is Nothing Then
          '  m_hashData(i) = row.Tag
          'End If
        Next

        If i > 0 Then
          dt.AcceptChanges()
        End If
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try

    End Sub
    Private Function SetDataSourceFiltered(ByVal dt As DataTable, ByVal columnSource As String, ByVal codestart As String, ByVal codeend As String) As DataTable
      Dim newdt As New DataTable
      Dim filterString As String = ""

      If codestart.Length = 0 AndAlso codeend.Length = 0 Then
        Return dt
      ElseIf codestart.Length > 0 AndAlso codeend.Length = 0 Then
        filterString = columnSource & " >= '" & codestart & "'"
      ElseIf codestart.Length = 0 AndAlso codeend.Length < 0 Then
        filterString = columnSource & " <= '" & codeend & "'"
      Else
        filterString = columnSource & " >= '" & codestart & "' and " & columnSource & " <='" & codeend & "'"
      End If

      For Each dcol As DataColumn In dt.Columns
        newdt.Columns.Add(New DataColumn(dcol.ColumnName))
      Next

      For Each drow As DataRow In dt.Select(filterString)
        Dim newDrow As DataRow = newdt.NewRow

        For Each dcol As DataColumn In dt.Columns
          newDrow(dcol.ColumnName) = drow(dcol.ColumnName)
        Next
        newdt.Rows.Add(newDrow)
      Next

      Return newdt
    End Function

#End Region

#Region "Select Distinct From DataTable"
    'Public Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
    '    Dim lastValues() As Object
    '    Dim newTable As DataTable

    '    If FieldNames Is Nothing OrElse FieldNames.Length = 0 Then
    '        Throw New ArgumentNullException("FieldNames")
    '    End If

    '    lastValues = New Object(FieldNames.Length - 1) {}
    '    newTable = New DataTable

    '    For Each field As String In FieldNames
    '        newTable.Columns.Add(field, SourceTable.Columns(field).DataType)
    '    Next

    '    For Each Row As DataRow In SourceTable.Select("", String.Join(", ", FieldNames))
    '        If Not fieldValuesAreEqual(lastValues, Row, FieldNames) Then
    '            newTable.Rows.Add(createRowClone(Row, newTable.NewRow(), FieldNames))

    '            setLastValues(lastValues, Row, FieldNames)
    '        End If
    '    Next

    '    Return newTable
    'End Function
    Private Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
      Dim areEqual As Boolean = True

      For i As Integer = 0 To fieldNames.Length - 1
        If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
          areEqual = False
          Exit For
        End If
      Next

      Return areEqual
    End Function
    Private Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
      For Each field As String In fieldNames
        newRow(field) = sourceRow(field)
      Next

      Return newRow
    End Function
    Private Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
      For i As Integer = 0 To fieldNames.Length - 1
        lastValues(i) = sourceRow(fieldNames(i))
      Next
    End Sub
#End Region

#Region "Shared"
#End Region

#Region "Properties"
    Public Overrides ReadOnly Property ClassName() As String
      Get
        Return "RptTrialBalanceByCC"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.RptTrialBalanceByCC"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.RptTrialBalanceByCC"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.RptTrialBalanceByCC.ListLabel}"
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
#End Region

#Region "IPrintableEntity"
    Public Overrides Function GetDefaultFormPath() As String
      Return "RptWBSBudgetUsage"
    End Function
    Public Overrides Function GetDefaultForm() As String
      Return "RptWBSBudgetUsage"
    End Function

    Public Overrides Function GetDocPrintingEntries() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      Dim i As Integer = 0
      Dim r As Integer = 0
      For Each itemRow As DataRow In Me.Treemanager.Treetable.Rows
        For j As Integer = 1 To Me.Treemanager.Treetable.Columns.Count - 1
          dpi = New DocPrintingItem
          dpi.Mapping = "col" & j
          dpi.Value = itemRow(Me.Treemanager.Treetable.Columns(j))
          dpi.DataType = "System.String"
          dpi.Row = i + 1
          dpi.Table = "Item"
          dpiColl.Add(dpi)
        Next

        i += 1
      Next

      Return dpiColl
    End Function
#End Region

  End Class
End Namespace

