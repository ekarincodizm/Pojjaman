Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Reflection
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.TextHelper
Imports System.Collections.Generic
Namespace Longkong.Pojjaman.BusinessLogic
  Public Class RptAssetDepreciation
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
      m_grid.BeginUpdate()
      m_grid.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.SystemTheme
      m_grid.Model.Options.NumberedColHeaders = False
      m_grid.Model.Options.WrapCellBehavior = Syncfusion.Windows.Forms.Grid.GridWrapCellBehavior.WrapRow
      CreateHeader()
      PopulateData()
      m_grid.EndUpdate()
    End Sub
    Private Sub CreateHeader()
      m_grid.RowCount = 1
      m_grid.ColCount = 18

      m_grid.ColWidths(1) = 100
      m_grid.ColWidths(2) = 120
      m_grid.ColWidths(3) = 200
      m_grid.ColWidths(4) = 100
      m_grid.ColWidths(5) = 110
      m_grid.ColWidths(6) = 100
      m_grid.ColWidths(7) = 100
      m_grid.ColWidths(8) = 100
      m_grid.ColWidths(9) = 60
      m_grid.ColWidths(10) = 100
      m_grid.ColWidths(11) = 5
      m_grid.ColWidths(12) = 100

      m_grid.ColWidths(13) = 100

      m_grid.ColWidths(14) = 100
      m_grid.ColWidths(15) = 70
      m_grid.ColWidths(16) = 100
      m_grid.ColWidths(17) = 100
      m_grid.ColWidths(18) = 100

      m_grid.ColStyles(1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid.ColStyles(2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid.ColStyles(3).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid.ColStyles(4).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid.ColStyles(5).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid.ColStyles(6).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid.ColStyles(7).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid.ColStyles(8).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid.ColStyles(9).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid.ColStyles(10).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid.ColStyles(11).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid.ColStyles(12).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right

      m_grid.ColStyles(13).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right

      m_grid.ColStyles(14).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid.ColStyles(15).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid.ColStyles(16).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid.ColStyles(17).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid.ColStyles(18).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right

      m_grid.Rows.HeaderCount = 1
      m_grid.Rows.FrozenCount = 1

      Dim indent As String = Space(3)
      m_grid(0, 1).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.AssetAcctCode}")              '"���ʺѭ��"
      m_grid(0, 2).Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.AssetAcctName}")            '"���ͺѭ���Թ��Ѿ��"

      m_grid(1, 1).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.No}")  '"No."
      m_grid(1, 2).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.AssetCode}")  '"�����Թ��Ѿ��"
      m_grid(1, 3).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.AssetName}")  '"�����Թ��Ѿ��"
      m_grid(1, 4).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.BuyDate}")   '"�ѹ������"
      m_grid(1, 5).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.BuyDocCode}")   '"�Ţ����͡��ë���"
      m_grid(1, 6).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.cc_code}")   '"���� costcenter"
      m_grid(1, 7).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.cc_name}")   '"���� costcenter"
      m_grid(1, 8).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.BuyPrice}")  '"�Ҥ�"
      m_grid(1, 9).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.Salvage}")  '"�ҤҤ�ҫҡ"
      m_grid(1, 10).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.DepreBase}")  '"�ҹ�Դ���������"
      m_grid(1, 11).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.Openning}")   '"��Ť���Թ��Ѿ��¡��"
      m_grid(1, 12).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.AccDpOpen}")   '"�������������¡��"

      m_grid(1, 13).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.StartCalcAmount}")   '"������������ͧ��"

      m_grid(1, 14).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.CalcRate}")   '"�ѵ�� (%)"
      m_grid(1, 15).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.DaysOfDepre}")   '"�ӹǹ�ѹ���Դ���������"
      m_grid(1, 16).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.Depreciation}")    '"����֡���"
      m_grid(1, 17).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.DepreciationOpenning}")   '"����������Ҥ�����"
      m_grid(1, 18).Text = indent & Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.OnGoing}")   '"�ʹ¡�"

      m_grid(0, 1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid(0, 2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left

      m_grid(1, 1).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid(1, 2).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid(1, 3).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid(1, 4).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid(1, 5).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid(1, 6).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid(1, 7).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
      m_grid(1, 8).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 9).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 10).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 11).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 12).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 13).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 14).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 15).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 16).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 17).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right
      m_grid(1, 18).HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right

    End Sub
    Private Sub PopulateData()
      Dim dt As DataTable = Me.DataSet.Tables(0)

      Dim currAccountIndex As Integer = -1
      Dim currAssetIndex As Integer = -1
      Dim indent As String = Space(3)
      Dim no As Integer = 0
      Dim currAccountCode As String = ""

      Dim sumPrice As Decimal = 0
      Dim sumOpeningBalance As Decimal = 0
      Dim sumDeprebase As Decimal = 0
      Dim sumAccDepre As Decimal = 0
      Dim sumDepre As Decimal = 0
      Dim sumDepreAmount As Decimal = 0
      Dim sumEndingBalance As Decimal = 0

      Dim sumAllPrice As Decimal = 0
      Dim sumAllOpeningBalance As Decimal = 0
      Dim sumAllDeprebase As Decimal = 0
      Dim sumAllAccDepre As Decimal = 0
      Dim sumAllDepre As Decimal = 0
      Dim sumAllDepreAmount As Decimal = 0
      Dim sumAllEndingBalance As Decimal = 0

      For Each row As DataRow In dt.Rows
        Dim darow As New DataRowHelper(row)

        no += 1

        If currAccountCode <> darow.GetValue(Of String)("AcctCode") Then
          If no > 1 Then
            m_grid(currAccountIndex, 8).CellValue = indent & Configuration.FormatToString(sumPrice, DigitConfig.Price)
            m_grid(currAccountIndex, 10).CellValue = Configuration.FormatToString(sumDeprebase, DigitConfig.Price)
            m_grid(currAccountIndex, 11).CellValue = Configuration.FormatToString(sumOpeningBalance, DigitConfig.Price)
            m_grid(currAccountIndex, 12).CellValue = Configuration.FormatToString(sumAccDepre, DigitConfig.Price)
            m_grid(currAccountIndex, 16).CellValue = Configuration.FormatToString(sumDepre, DigitConfig.Price)
            m_grid(currAccountIndex, 17).CellValue = Configuration.FormatToString(sumDepreAmount, DigitConfig.Price)
            m_grid(currAccountIndex, 18).CellValue = Configuration.FormatToString(sumEndingBalance, DigitConfig.Price)
            sumPrice = 0
            sumOpeningBalance = 0
            sumDeprebase = 0
            sumAccDepre = 0
            sumDepre = 0
            sumDepreAmount = 0
            sumEndingBalance = 0
          End If

          m_grid.RowCount += 1
          currAccountIndex = m_grid.RowCount
          m_grid.RowStyles(currAccountIndex).BackColor = Color.FromArgb(128, 255, 128)
          m_grid.RowStyles(currAccountIndex).Font.Bold = True
          m_grid.RowStyles(currAccountIndex).ReadOnly = True
          m_grid(currAccountIndex, 1).CellValue = darow.GetValue(Of String)("AcctCode")
          m_grid(currAccountIndex, 2).CellValue = darow.GetValue(Of String)("AcctName")
          m_grid(currAccountIndex, 1).Tag = "Font.Bold"
          currAccountCode = darow.GetValue(Of String)("AcctCode")
        End If

        m_grid.RowCount += 1
        currAssetIndex = m_grid.RowCount
        m_grid.RowStyles(currAssetIndex).ReadOnly = True
        m_grid(currAssetIndex, 1).CellValue = indent & no.ToString
        m_grid(currAssetIndex, 2).CellValue = indent & darow.GetValue(Of String)("asset_code")
        m_grid(currAssetIndex, 3).CellValue = indent & darow.GetValue(Of String)("asset_name")
        m_grid(currAssetIndex, 4).CellValue = indent & darow.GetValue(Of DateTime)("asset_buyDate").ToShortDateString
        m_grid(currAssetIndex, 5).CellValue = indent & darow.GetValue(Of String)("asset_buyDocCode")
        m_grid(currAssetIndex, 6).CellValue = indent & darow.GetValue(Of String)("cc_code")
        m_grid(currAssetIndex, 7).CellValue = indent & darow.GetValue(Of String)("cc_name")
        m_grid(currAssetIndex, 8).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("asset_buyPrice"), DigitConfig.Price)
        m_grid(currAssetIndex, 9).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("asset_salvage"), DigitConfig.Price)
        m_grid(currAssetIndex, 10).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("asset_Deprebase"), DigitConfig.Price)
        m_grid(currAssetIndex, 11).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("OpeningBalance"), DigitConfig.Price)
        m_grid(currAssetIndex, 12).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("OpeningDepre"), DigitConfig.Price)

        m_grid(currAssetIndex, 13).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("asset_startCalcAmt"), DigitConfig.Price)

        m_grid(currAssetIndex, 14).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("asset_calcRate"), DigitConfig.Price)
        m_grid(currAssetIndex, 15).CellValue = Configuration.FormatToString(darow.GetValue(Of Integer)("SumDayOfDepre"), DigitConfig.Int)
        m_grid(currAssetIndex, 16).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("Depre"), DigitConfig.Price)
        m_grid(currAssetIndex, 17).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("DepreAmount"), DigitConfig.Price)
        m_grid(currAssetIndex, 18).CellValue = Configuration.FormatToString(darow.GetValue(Of Decimal)("EndingBalance"), DigitConfig.Price)

        sumPrice += darow.GetValue(Of Decimal)("asset_buyPrice")
        sumOpeningBalance += darow.GetValue(Of Decimal)("OpeningBalance")
        sumDeprebase += darow.GetValue(Of Decimal)("asset_Deprebase")
        sumAccDepre += darow.GetValue(Of Decimal)("OpeningDepre")
        sumDepre += darow.GetValue(Of Decimal)("Depre")
        sumDepreAmount += darow.GetValue(Of Decimal)("DepreAmount")
        sumEndingBalance += darow.GetValue(Of Decimal)("EndingBalance")

        sumAllPrice += darow.GetValue(Of Decimal)("asset_buyPrice")
        sumAllOpeningBalance += darow.GetValue(Of Decimal)("OpeningBalance")
        sumAllDeprebase += darow.GetValue(Of Decimal)("asset_Deprebase")
        sumAllAccDepre += darow.GetValue(Of Decimal)("OpeningDepre")
        sumAllDepre += darow.GetValue(Of Decimal)("Depre")
        sumAllDepreAmount += darow.GetValue(Of Decimal)("DepreAmount")
        sumAllEndingBalance += darow.GetValue(Of Decimal)("EndingBalance")

      Next

      '����Ѻ�Թ��Ѿ�����ش����
      m_grid(currAccountIndex, 8).CellValue = indent & Configuration.FormatToString(sumPrice, DigitConfig.Price)
      m_grid(currAccountIndex, 10).CellValue = Configuration.FormatToString(sumDeprebase, DigitConfig.Price)
      m_grid(currAccountIndex, 11).CellValue = Configuration.FormatToString(sumOpeningBalance, DigitConfig.Price)
      m_grid(currAccountIndex, 12).CellValue = Configuration.FormatToString(sumAccDepre, DigitConfig.Price)
      m_grid(currAccountIndex, 16).CellValue = Configuration.FormatToString(sumDepre, DigitConfig.Price)
      m_grid(currAccountIndex, 17).CellValue = Configuration.FormatToString(sumDepreAmount, DigitConfig.Price)
      m_grid(currAccountIndex, 18).CellValue = Configuration.FormatToString(sumEndingBalance, DigitConfig.Price)

      m_grid.RowCount += 1
      currAssetIndex = m_grid.RowCount
      m_grid.RowStyles(currAssetIndex).Font.Bold = True
      m_grid.RowStyles(currAssetIndex).ReadOnly = True
      m_grid(currAssetIndex, 3).CellValue = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.Total}")   '"���"
      m_grid(currAssetIndex, 8).CellValue = Configuration.FormatToString(sumAllPrice, DigitConfig.Price)
      m_grid(currAssetIndex, 10).CellValue = Configuration.FormatToString(sumAllDeprebase, DigitConfig.Price)
      m_grid(currAssetIndex, 11).CellValue = Configuration.FormatToString(sumAllOpeningBalance, DigitConfig.Price)
      m_grid(currAssetIndex, 12).CellValue = Configuration.FormatToString(sumAllAccDepre, DigitConfig.Price)
      m_grid(currAssetIndex, 16).CellValue = Configuration.FormatToString(sumAllDepre, DigitConfig.Price)
      m_grid(currAssetIndex, 17).CellValue = Configuration.FormatToString(sumAllDepreAmount, DigitConfig.Price)
      m_grid(currAssetIndex, 18).CellValue = Configuration.FormatToString(sumAllEndingBalance, DigitConfig.Price) 'sumEdbNetAmt, DigitConfig.Price)
      m_grid(currAssetIndex, 1).Tag = "Font.Bold"


    End Sub
#End Region#Region "Shared"
#End Region#Region "Properties"    Public Overrides ReadOnly Property ClassName() As String
      Get
        Return "RptAssetDepreciation"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.RptAssetDepreciation"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.RptAssetDepreciation"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.RptAssetDepreciation.ListLabel}"
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
      Return "RptAssetDepreciation"
    End Function
    Public Overrides Function GetDefaultForm() As String
      Return "RptAssetDepreciation"
    End Function
    Public Overrides Function GetDocPrintingEntries() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      For Each fixDpi As DocPrintingItem In Me.FixValueCollection
        dpiColl.Add(fixDpi)
      Next

      Dim n As Integer = 0
      Dim fn As Font
      For rowIndex As Integer = 2 To m_grid.RowCount
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

        dpi = New DocPrintingItem
        dpi.Mapping = "col10"
        dpi.Value = m_grid(rowIndex, 11).CellValue
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpi.Font = fn
        dpiColl.Add(dpi)


        dpi = New DocPrintingItem
        dpi.Mapping = "col11"
        dpi.Value = m_grid(rowIndex, 12).CellValue
        dpi.DataType = "System.String"
        dpi.Row = n + 1
        dpi.Table = "Item"
        dpi.Font = fn
        dpiColl.Add(dpi)

        For i As Integer = 12 To m_grid.ColCount - 1

          dpi = New DocPrintingItem
          dpi.Mapping = "col" & i.ToString
          dpi.Value = m_grid(rowIndex, i + 1).CellValue
          dpi.DataType = "System.String"
          dpi.Row = n + 1
          dpi.Table = "Item"
          dpi.Font = fn
          dpiColl.Add(dpi)
        Next

        n += 1
      Next

      Return dpiColl
    End Function
#End Region
  End Class
End Namespace

