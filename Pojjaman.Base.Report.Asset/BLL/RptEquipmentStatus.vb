Option Explicit On
Option Strict On
Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Reflection
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.TextHelper
Imports Telerik.WinControls.UI
Imports System.Collections.Generic

Namespace Longkong.Pojjaman.BusinessLogic
  Public Class RptEquipmentStatus
    Inherits Report
    Implements IUseTelerikGridReport

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
    Dim viewDef As ColumnGroupsViewDefinition
    Private m_grid As RadGridView
    Public Overrides Sub ListInNewGrid(ByVal grid As RadGridView)
      m_grid = grid
      m_grid.GridElement.BeginUpdate()
      m_grid.Rows.Clear()
      m_grid.MasterGridViewTemplate.AllowAddNewRow = False
      m_grid.MasterGridViewTemplate.AllowDeleteRow = False
      m_grid.MasterGridViewTemplate.AllowCellContextMenu = False
      'm_grid.MasterGridViewTemplate.AllowColumnReorder = False
      CreateHeader()
      PopulateData()
      m_grid.GridElement.EndUpdate()
    End Sub
    Private Sub CreateHeader()
      viewDef = New ColumnGroupsViewDefinition

      Dim headerTextList As New List(Of String)
      headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.EquipmentTypeCode}")) '"���ʪ�Դ����ͧ�ѡ�"
      headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.EquipmentTypeName}")) '"���ͪ�Դ����ͧ�ѡ�"

      headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.EquipmentCode}")) '"��������ͧ�ѡ�"
            headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.EquipmentName}")) '"��������ͧ�ѡ�"

      headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.OwnerCC}")) '"CC��Ңͧ"
      headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.currentStatus}")) '"ʶҹлѨ�غѹ"
      headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.currentCC}")) '"CC�������"
      headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.Rentalrate}")) '"�����ҵ���ѹ"
            headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.asset}")) '"�Թ��Ѿ��"
            headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.BuyDocCode}")) '"�͡��ë���"
            headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.Buydate}")) '"�ѹ�������"
            headerTextList.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.buycost}")) '"�Ҥҫ���"
      m_grid.Columns.Clear()

      For i As Integer = 0 To 11
        Dim gridColumn As New GridViewTextBoxColumn("Col" & i.ToString)
        If i = 7 OrElse i = 11 Then
          gridColumn.TextAlignment = ContentAlignment.MiddleRight
        Else
          gridColumn.TextAlignment = ContentAlignment.MiddleLeft
        End If
        gridColumn.HeaderText = headerTextList(i)
        gridColumn.Width = 100
        gridColumn.ReadOnly = True
        m_grid.Columns.Add(gridColumn)
      Next

    End Sub
    Private Sub PopulateData()
      Dim dt As DataTable = Me.DataSet.Tables(0)
      Dim currentEQTypeCode As String = ""
      Dim currentEQTypeCodeIndex As Integer = -1
      Dim currentEqiTypeCode As String = ""
      Dim currentEqiTypeCodeIndex As Integer = -1

      '  'Dim indent As String = Space(3)

      For Each row As DataRow In dt.Rows
        Dim currentGridRow As GridViewDataRowInfo = m_grid.Rows.AddNew()
        Dim deh As New DataRowHelper(row)
        'If deh.GetValue(Of String)("eq_code") <> currentEQTypeCode then
        currentGridRow.Cells(0).Value = deh.GetValue(Of String)("eq_code")
        currentGridRow.Cells(1).Value = deh.GetValue(Of String)("eq_name")
        currentGridRow.Cells(2).Value = deh.GetValue(Of String)("eqi_code")
        currentGridRow.Cells(3).Value = deh.GetValue(Of String)("eqi_name")
        currentGridRow.Cells(4).Value = deh.GetValue(Of String)("ownerCC")
        currentGridRow.Cells(5).Value = deh.GetValue(Of String)("Status")
        currentGridRow.Cells(6).Value = deh.GetValue(Of String)("currentCC")
        currentGridRow.Cells(7).Value = Configuration.FormatToString(deh.GetValue(Of Decimal)("Rentalrate"), DigitConfig.Price)
        currentGridRow.Cells(8).Value = deh.GetValue(Of String)("Asset")
        currentGridRow.Cells(9).Value = deh.GetValue(Of String)("eqi_buydoccode")
        currentGridRow.Cells(10).Value = deh.GetValue(Of String)("eqi_buydate")
        currentGridRow.Cells(11).Value = Configuration.FormatToString(deh.GetValue(Of Decimal)("eqi_buycost"), DigitConfig.Price)

      Next
    End Sub
#End Region#Region "Shared"
#End Region#Region "Properties"    Public Overrides ReadOnly Property ClassName() As String
      Get
        Return "RptEquipmentStatus"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.RptEquipmentStatus"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.RptEquipmentStatus"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.RptEquipmentStatus.ListLabel}"
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
      Return "RptEquipmentStatus"
    End Function
    Public Overrides Function GetDefaultForm() As String
      Return "RptEquipmentStatus"
    End Function
    Public Overrides Function GetDocPrintingEntries() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      For Each fixDpi As DocPrintingItem In Me.FixValueCollection
        dpiColl.Add(fixDpi)
      Next

      Dim n As Integer = 0
      For rowIndex As Integer = 0 To m_grid.RowCount - 1
        dpi = New DocPrintingItem
        dpi.Mapping = "col0"
        dpi.Value = m_grid.Rows(rowIndex).Cells(0).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col1"
        dpi.Value = m_grid.Rows(rowIndex).Cells(1).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col2"
        dpi.Value = m_grid.Rows(rowIndex).Cells(2).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col3"
        dpi.Value = m_grid.Rows(rowIndex).Cells(3).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col4"
        dpi.Value = m_grid.Rows(rowIndex).Cells(4).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col5"
        dpi.Value = m_grid.Rows(rowIndex).Cells(5).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col6"
        dpi.Value = m_grid.Rows(rowIndex).Cells(6).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col7"
        dpi.Value = m_grid.Rows(rowIndex).Cells(7).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col8"
        dpi.Value = m_grid.Rows(rowIndex).Cells(8).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col9"
        dpi.Value = m_grid.Rows(rowIndex).Cells(9).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        dpi = New DocPrintingItem
        dpi.Mapping = "col10"
        dpi.Value = m_grid.Rows(rowIndex).Cells(10).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)


        dpi = New DocPrintingItem
        dpi.Mapping = "col11"
        dpi.Value = m_grid.Rows(rowIndex).Cells(11).Value
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        n += 1
      Next

      Return dpiColl
    End Function
#End Region
  End Class
End Namespace

