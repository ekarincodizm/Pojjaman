Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Reflection
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.Services

Namespace Longkong.Pojjaman.BusinessLogic
  Public Class MatTransferItem
    Implements IWBSAllocatableItem, IAllowWBSAllocatableItem

#Region "Members"
    Private m_matTransfer As MatTransfer
    Private m_lineNumber As Integer
    Private m_entity As IHasName
    Private m_defaultunit As Unit
    Private m_unit As Unit
    Private m_qty As Decimal
    Private m_unitCost As Decimal
    Private m_note As String
    Private m_stockqty As Decimal
    Private m_oldStockQty As Decimal
    Private m_oldStockQty2 As Decimal
    Private m_transferAmount As Decimal = 0

    Private m_matTransferId As Integer

    Private m_sequence As Integer

    'Private m_inWbsdColl As WBSDistributeCollection
    Private m_WBSDistributeCollection As WBSDistributeCollection

    Private m_itemCostCollection As StockCostItemCollection

    Private m_pritem As PRItem
#End Region

#Region "Constructors"
    Public Sub New()
      MyBase.New()
      'm_inWbsdColl = New WBSDistributeCollection
      m_WBSDistributeCollection = New WBSDistributeCollection
    End Sub
    Public Sub New(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Me.Construct(ds, aliasPrefix)
    End Sub
    Public Sub New(ByVal dr As DataRow, ByVal aliasPrefix As String)
      Me.Construct(dr, aliasPrefix)
    End Sub
    Public Sub New(ByVal stockid As Integer, ByVal line As Integer)

      Dim connString As String = RecentCompanies.CurrentCompany.ConnectionString
      Dim ds As DataSet = SqlHelper.ExecuteDataset(connString _
      , CommandType.StoredProcedure _
      , "GetmatTransferItems" _
      , New SqlParameter("@stock_id", stockid) _
      , New SqlParameter("@stocki_linenumber", line) _
      )
      Me.Construct(ds.Tables(0).Rows(0), "")
      'm_inWbsdColl = New WBSDistributeCollection
      'For Each wbsRow As DataRow In ds.Tables(1).Select("stockiw_sequence=" & Me.Sequence & "and stockiw_direction=0")
      '  Dim wbsd As New WBSDistribute(wbsRow, "")
      '  m_inWbsdColl.Add(wbsd)
      '  Me.matTransfer.SetActual(wbsd.WBS, 0, wbsd.TransferAmount, False)
      'Next
      m_WBSDistributeCollection = New WBSDistributeCollection
      For Each wbsRow As DataRow In ds.Tables(1).Select("stockiw_sequence=" & Me.Sequence & "and stockiw_direction=1")
        Dim wbsd As New WBSDistribute(wbsRow, "")
        m_WBSDistributeCollection.Add(wbsd)
        Me.MatTransfer.SetActual(wbsd.WBS, 0, wbsd.TransferAmount, False)
      Next
    End Sub
    Protected Sub Construct(ByVal dr As DataRow, ByVal aliasPrefix As String)
      With Me

        If dr.Table.Columns.Contains(aliasPrefix & "pri_entityType") AndAlso Not dr.IsNull("pri_entityType") Then
          Me.m_pritem = New PRItem(dr, aliasPrefix)
          Dim myPR As New PR

          If dr.Table.Columns.Contains(aliasPrefix & "pri_pr") AndAlso Not dr.IsNull(aliasPrefix & "pri_pr") Then
            myPR.Id = CInt(dr(aliasPrefix & "pri_pr"))
          End If
          If dr.Table.Columns.Contains(aliasPrefix & "pr_code") AndAlso Not dr.IsNull(aliasPrefix & "pr_code") Then
            myPR.Code = CStr(dr(aliasPrefix & "pr_code"))
          End If
          If dr.Table.Columns.Contains(aliasPrefix & "pr_ApproveStorePerson") AndAlso Not dr.IsNull(aliasPrefix & "pr_ApproveStorePerson") Then
            myPR.ApproveStorePerson = New User(CInt(dr(aliasPrefix & "pr_ApproveStorePerson")))
          End If

          Me.m_pritem.Pr = myPR
        End If

        If dr.Table.Columns.Contains("lci_id") AndAlso Not dr.IsNull("lci_id") Then
          If Not dr.IsNull("lci_id") Then
            .m_entity = New LCIItem(dr, "")
          End If
        ElseIf dr.Table.Columns.Contains(aliasPrefix & "stocki_entity") AndAlso Not dr.IsNull(aliasPrefix & "stocki_entity") Then
          .m_entity = New LCIItem(CInt(dr(aliasPrefix & "stocki_entity")))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "stocki_stock") AndAlso Not dr.IsNull(aliasPrefix & "stocki_stock") Then
          m_matTransferId = CInt(dr(aliasPrefix & "stocki_stock"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "stocki_lineNumber") AndAlso Not dr.IsNull(aliasPrefix & "stocki_lineNumber") Then
          .m_lineNumber = CInt(dr(aliasPrefix & "stocki_lineNumber"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "stocki_sequence") AndAlso Not dr.IsNull(aliasPrefix & "stocki_sequence") Then
          .m_sequence = CInt(dr(aliasPrefix & "stocki_sequence"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "stocki_qty") AndAlso Not dr.IsNull(aliasPrefix & "stocki_qty") Then
          .m_qty = CDec(dr(aliasPrefix & "stocki_qty"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "stocki_unitcost") AndAlso Not dr.IsNull(aliasPrefix & "stocki_unitcost") Then
          .m_unitCost = CDec(dr(aliasPrefix & "stocki_unitcost"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "stocki_transferUnitPrice") AndAlso Not dr.IsNull(aliasPrefix & "stocki_transferUnitPrice") Then
          .m_transferUnitPrice = CDec(dr(aliasPrefix & "stocki_transferUnitPrice"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "stocki_transferAmt") AndAlso Not dr.IsNull(aliasPrefix & "stocki_transferAmt") Then
          .m_transferAmount = CDec(dr(aliasPrefix & "stocki_transferAmt"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "stocki_note") AndAlso Not dr.IsNull(aliasPrefix & "stocki_note") Then
          .m_note = CStr(dr(aliasPrefix & "stocki_note"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "unit_name") AndAlso Not dr.IsNull(aliasPrefix & "unit_name") Then
          .m_defaultunit = New Unit(dr, "")
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "unit_id") AndAlso Not dr.IsNull(aliasPrefix & "unit_id") Then
          If Not dr.IsNull("unit_id") Then
            .m_unit = New Unit(dr, "")
          End If
        Else
          If dr.Table.Columns.Contains(aliasPrefix & "stocki_unit") AndAlso Not dr.IsNull(aliasPrefix & "stocki_unit") Then
            .m_unit = New Unit(CInt(dr(aliasPrefix & "stocki_unit")))
          End If
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "stocki_stockqty") AndAlso Not dr.IsNull(aliasPrefix & "stocki_stockqty") Then
          Me.m_stockqty = CDec(dr(aliasPrefix & "stocki_stockqty"))
        End If

        If Not Me.Unit Is Nothing AndAlso Me.Unit.Originated Then
          If TypeOf Me.Entity Is LCIItem Then
            Dim lci As LCIItem = CType(Me.Entity, LCIItem)
            Try
              Me.Conversion = lci.GetConversion(Me.Unit)
            Catch ex As NoConversionException
              Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
              msgServ.ShowErrorFormatted("��ʴ� {0} �����˹��¹Ѻ {1} �к����", New String() {ex.Lci.Code, ex.Unit.Name})
            End Try
          Else
            Me.Conversion = 1
          End If
        End If

      End With
    End Sub
    Protected Sub Construct(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Dim dr As DataRow = ds.Tables(0).Rows(0)
      Me.Construct(dr, aliasPrefix)
    End Sub
#End Region

#Region "Properties"
    Public Property ItemCostCollection As StockCostItemCollection
      Get
        If m_itemCostCollection Is Nothing Then
          If Not Me.MatTransfer Is Nothing AndAlso Not Me.MatTransfer.FromCostCenter Is Nothing Then
          End If
        End If
        Return m_itemCostCollection
      End Get
      Set(ByVal value As StockCostItemCollection)
        m_itemCostCollection = value
        'm_transferAmount = m_itemCollectionPrePareCost.CostAmount
      End Set
    End Property
    Public Property WBSDistributeCollection() As WBSDistributeCollection Implements IWBSAllocatableItem.WBSDistributeCollection
    Public Property WBSDistributeCollection2() As WBSDistributeCollection Implements IWBSAllocatableItem.WBSDistributeCollection2
    Public Property Sequence() As Integer
    Public ReadOnly Property MatTransferId() As Integer
      Get
        Return m_matTransferId
      End Get
    End Property
    Public Property MatTransfer() As MatTransfer
          m_matTransferId = 0
          Return
        End If
        m_matTransferId = Value.Id
          Me.m_unit = CType(m_entity, IHasUnit).DefaultUnit
        End If
        End If
    '    If Me.ItemCollectionPrePareCost Is Nothing OrElse Me.ItemCollectionPrePareCost.Count = 0 Then
    '      If Not Me.matTransfer Is Nothing AndAlso Not Me.matTransfer.FromCostCenter Is Nothing Then
    '      End If
    '    End If
    '    Dim amt As Decimal = 0
    '    For Each itm As StockCostItem In Me.ItemCollectionPrePareCost
    '      amt += (itm.UnitCost * itm.StockQty)
    '    Next
    '    Return amt
    '  End Get
    'End Property
      Try
        Dim ds As DataSet = SqlHelper.ExecuteDataset( _
                RecentCompanies.CurrentCompany.SiteConnectionString _
                , CommandType.StoredProcedure _
                , "GetRemainLCIItemListForCC" _
                , New SqlParameter("@cc_id", cc) _
                , New SqlParameter("@FromacctType", 3) _
                , New SqlParameter("@EntityId", 31) _
                , New SqlParameter("@lci_id", lci_id) _
                , New SqlParameter("@stock_id", Me.MatTransfer.Id) _
                )
        If ds.Tables(0).Rows(0).IsNull("remain") Then
          Return 0
        End If
        Return CDec(ds.Tables(0).Rows(0)("remain"))
      Catch ex As Exception
      End Try
    End Function
        If Not itm.Entity Is Nothing Then
          If itm.Entity.Code = theCode Then
            Return True
          End If
        End If
      Next
      Return False
    End Function
      Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
      Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      If theCode Is Nothing OrElse theCode.Length = 0 Then
        If Me.Entity.Code.Length <> 0 Then
          If msgServ.AskQuestionFormatted("${res:Global.Question.DeleteLCIDetail}", New String() {Me.Entity.Code}) Then
            Me.Clear()
          End If
        End If
        Return
      End If

      Dim lci As New LCIItem(theCode)
      If CheckDupplicateLciCode(theCode) Then
        msgServ.ShowMessageFormatted("${res:Global.Validate.DupplicateLciCode}", New String() {theCode})
        Return
      End If

      If Not lci.Originated Then
        msgServ.ShowMessageFormatted("${res:Global.Error.NoLCI}", New String() {theCode})
        Return
      Else
        Dim myUnit As Unit = lci.DefaultUnit
        Me.m_unit = myUnit
        Me.m_entity = lci

        If Not cc = 0 Then
          Dim remainQty As Decimal = 0
          remainQty = GetAmountFromSproc(lci.Id, cc)
          If remainQty > 0 Then
            Me.m_qty = remainQty
            Me.OldQty2 = Me.m_qty
            Me.m_unit = lci.DefaultUnit
            Me.m_entity = lci
            If Not Me.MatTransfer Is Nothing AndAlso Not Me.MatTransfer.FromCostCenter Is Nothing Then
            End If
          Else
            msgServ.ShowMessageFormatted("${res:Global.Error.NoLCI}", New String() {theCode})
            Return
          End If
          'msgServ.ShowMessageFormatted("no!!!!!!", New String() {theCode})
        End If
      End If
      'Me.m_qty = 1
    End Sub
        Return m_defaultunit
      End Get
      Set(ByVal Value As Unit)
        m_defaultunit = Value
      End Set
    End Property
        Dim oldConversion As Decimal = Me.Conversion
        Dim newConversion As Decimal = 1
        Dim err As String = ""
        If Not Value Is Nothing AndAlso Value.Originated Then
          If TypeOf Me.Entity Is LCIItem Then
            If CType(Me.Entity, LCIItem).Level < 5 Then
              newConversion = 1
            Else
              If Not CType(Me.Entity, LCIItem).ValidUnit(Value) Then
                err = "${res:Global.Error.NoUnitConversion}"
              Else
                newConversion = CType(Me.Entity, LCIItem).GetConversion(Value)
              End If
            End If
          ElseIf TypeOf Me.Entity Is Tool Then
            If Not (Not CType(Me.Entity, Tool).Unit Is Nothing AndAlso CType(Me.Entity, Tool).Unit.Id = Value.Id) Then
              err = "${res:Global.Error.NoUnitConversion}"
            End If
          End If
        Else
          err = "${res:Global.Error.InvalidUnit}"
        End If
        If err.Length = 0 Then
          If Me.Qty <> 0 Then
            Me.Qty = (Me.Qty * oldConversion) / newConversion
          End If
          'If Not (Me.TransferUnitPrice = Decimal.MinValue) AndAlso Me.TransferUnitPrice <> 0 Then
          '  Me.TransferUnitPrice = (Me.TransferUnitPrice / oldConversion) * newConversion
          'End If
          'If Me.TransferUnitPrice <> 0 AndAlso Me.TransferUnitPrice <> Decimal.MinValue Then
          '  Me.TransferUnitPrice = (newConversion / oldConversion) * Me.m_transferUnitPrice
          'End If
          m_unit = Value
          msgServ.ShowMessage(err)
        End If
        End If
          End If
        End If
      Get
        Return Me.Qty * Me.Conversion
      End Get
    End Property
        If Me.UnitCost = Decimal.MinValue Then
          Return 0
        End If
        Return (Me.Qty * Me.Conversion) * Me.UnitCost
      End Get
    End Property
      Get
        Return m_transferUnitPrice
      End Get
      Set(ByVal Value As Decimal)
        'm_transferUnitPrice = Configuration.Format(Value, DigitConfig.UnitPrice)
        m_transferUnitPrice = Value
      End Set
    End Property
      Get
        'If Me.TransferUnitPrice = Decimal.MinValue Then
        '  Return 0
        'End If
        'Return Me.Qty * Me.TransferUnitPrice    
        If Not Me.ItemCostCollection Is Nothing Then
          Return Me.ItemCostCollection.CostAmount
        End If
        Return 0
      End Get
    End Property
    Public Property OldQty() As Decimal '෨ҡ�С���
      Get
        Return m_oldStockQty
      End Get
      Set(ByVal Value As Decimal)
        m_oldStockQty = Value
      End Set
    End Property
    Public Property OldQty2() As Decimal '�������ͧ���� enter
      Get
        Return m_oldStockQty2
      End Get
      Set(ByVal Value As Decimal)
        m_oldStockQty2 = Value
      End Set
    End Property
#End Region

#Region "Methods"
    'Private Function GetRemainLCIItem(ByVal lci_id As Integer) As Decimal
    '  Try
    '    Dim ds As DataSet = SqlHelper.ExecuteDataset( _
    '            Me.matTransfer.ConnectionString _
    '            , CommandType.StoredProcedure _
    '            , "GetRemainLCIItemForCC" _
    '            , New SqlParameter("@lci_id", lci_id) _
    '            , New SqlParameter("@cc_id", Me.matTransfer.ValidIdOrDBNull(Me.matTransfer.FromCC)) _
    '            )
    '    Dim tableIndex As Integer = 0
    '    If ds.Tables.Count > tableIndex Then
    '      If ds.Tables(tableIndex).Rows.Count > 0 Then
    '        If ds.Tables(tableIndex).Rows(0).IsNull(0) Then
    '          Return 0
    '        End If
    '        Return CDec(ds.Tables(tableIndex).Rows(0)("remain"))
    '      End If
    '    End If
    '  Catch ex As Exception
    '    MessageBox.Show(ex.ToString)
    '  End Try
    '  Return 0
    'End Function
    Public Function AllowWithdrawFromPR() As Decimal
      Dim qty As Decimal = Math.Max(Pritem.Qty - Pritem.WithdrawnQty, 0)
      Dim remainstock As Decimal = Me.MatTransfer.GetRemainLCIItem(Me.m_entity.Id)
      Dim allowWithdrawn As Decimal = Math.Min(remainstock, qty * Pritem.Conversion)
      Return remainstock
    End Function
    Public Sub CopyFromPRItem(ByVal prItem As PRItem, ByVal cumWithdrawn As Decimal)
      Me.m_pritem = prItem
      Me.m_entity = prItem.Entity
      Me.m_unit = prItem.Unit
      Me.m_qty = Math.Max(prItem.Qty - prItem.WithdrawnQty, 0)

      Dim allowWithdrawn As Decimal
      Dim remainstock As Decimal
      remainstock = Me.MatTransfer.GetRemainLCIItem(Me.m_entity.Id)
      allowWithdrawn = Math.Min(remainstock - cumWithdrawn, Me.m_qty * prItem.Conversion)

      If allowWithdrawn <= 0 Then
        Me.m_qty = 0
      Else
        If prItem.Conversion = 0 Then
          Me.m_qty = 0
        Else
          Me.m_qty = allowWithdrawn / prItem.Conversion
        End If
      End If

      Me.m_note = prItem.Note
      If Not prItem.WBSDistributeCollection Is Nothing Then
        'Me.OutWbsdColl = prItem.WBSDistributeCollection.Clone(Me)
        Me.WBSDistributeCollection = prItem.WBSDistributeCollection.Clone(Me)
      End If
    End Sub
    Public Sub CopyFromPRItem(ByVal prItem As PRItem)
      Me.m_pritem = prItem
      Me.m_entity = prItem.Entity
      Me.m_unit = prItem.Unit
      Me.m_qty = Math.Max(prItem.Qty - prItem.WithdrawnQty, 0)
      Me.m_qty = Math.Min(Me.MatTransfer.GetRemainLCIItem(Me.m_entity.Id), Me.m_qty)
      Me.m_note = prItem.Note
      If Not prItem.WBSDistributeCollection Is Nothing Then
        'Me.OutWbsdColl = prItem.WBSDistributeCollection.Clone(Me)
        Me.WBSDistributeCollection = prItem.WBSDistributeCollection.Clone(Me)
      End If
    End Sub
    'Public Sub SetTransferAmount(ByVal amt As Decimal)
    'End Sub

    Public Sub Clear()
      Me.m_pritem = Nothing
      Me.m_entity = New BlankItem("")
      Me.TransferUnitPrice = 0
      Me.m_qty = 0
      Me.m_unit = New Unit
      Me.m_transferUnitPrice = 0
      Me.m_unitCost = 0
      Me.m_note = ""
    End Sub
    Public Sub ItemValidateRow(ByVal row As DataRow)
      Dim proposedUnit As Object = row("stocki_unit")
      Dim proposedCode As Object = row("Code")
      Dim proposedDescription As Object = row("stocki_itemName")
      Dim proposedQty As Object = row("stocki_qty")
      Dim stocki_transferUnitPrice As Object = row("stocki_transferUnitPrice")

      Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
      Dim isBlankRow As Boolean = False
      If (IsDBNull(proposedUnit) OrElse CStr(proposedUnit).Length = 0) _
          And (IsDBNull(proposedCode) OrElse CStr(proposedCode).Length = 0) _
          And (IsDBNull(proposedDescription) OrElse CStr(proposedDescription).Length = 0) _
          And (IsDBNull(proposedQty) OrElse Not IsNumeric(proposedQty) OrElse CDec(proposedQty) = 0) _
          Then
        isBlankRow = True
      End If

      If Not isBlankRow Then
        If IsDBNull(proposedUnit) Then
          row.SetColumnError("Unit", myStringParserService.Parse("${res:Global.Error.UnitMissing}"))
        Else
          row.SetColumnError("Unit", "")
        End If

        If IsDBNull(proposedDescription) OrElse CStr(proposedDescription).Length = 0 Then
          row.SetColumnError("stocki_itemName", myStringParserService.Parse("${res:Global.Error.ItemMissing}"))
        Else
          row.SetColumnError("stocki_itemName", "")
        End If

        If IsDBNull(proposedQty) OrElse Not IsNumeric(proposedQty) OrElse CDec(proposedQty) = 0 Then
          row.SetColumnError("stocki_qty", myStringParserService.Parse("${res:Global.Error.QtyMissing}"))
        Else
          row.SetColumnError("stocki_qty", "")
        End If
      End If
    End Sub
    Public Sub CopyToDataRow(ByVal row As TreeRow)
      If row Is Nothing Then
        Return
      End If
      Me.MatTransfer.IsInitialized = False

      If Not Me.Pritem Is Nothing Then
        row("pri_linenumber") = Me.Pritem.LineNumber
        If Not Me.Pritem.Entity Is Nothing Then
          row("pri_entity") = Me.Pritem.Entity.Id
          row("pri_entityType") = Me.Pritem.ItemType.Value
          row("PRItemCode") = Me.Pritem.Entity.Code
          row("PRItemName") = Me.Pritem.Entity.Name
        End If

        If Not Me.Pritem.Unit Is Nothing Then
          row("PRItemUnit") = Me.Pritem.Unit.Name
        End If
        row("pri_qty") = Me.Pritem.Qty
        row("PRItemRemainingQty") = Me.Pritem.Qty
        If Not Me.Pritem.Pr Is Nothing Then
          row("pr_code") = Me.Pritem.Pr.Code
          row("pri_pr") = Me.Pritem.Pr.Id
        End If
      End If

      row("stocki_linenumber") = Me.LineNumber
      row("stocki_sequence") = Me.Sequence
      If Not Me.Entity Is Nothing Then
        row("stocki_entity") = Me.Entity.Id
        row("Code") = Me.Entity.Code
        row("stocki_itemName") = Me.Entity.Name
      End If
      If Not Me.DefaultUnit Is Nothing Then
        row("defaultunit") = Me.DefaultUnit.Name
      End If
      If Not Me.Unit Is Nothing Then
        row("stocki_unit") = Me.Unit.Id
        row("Unit") = Me.Unit.Name
      End If
      Me.Conversion = 1

      If Not Me.Unit Is Nothing AndAlso Me.Unit.Originated Then
        If TypeOf Me.Entity Is LCIItem Then
          Dim lci As LCIItem = CType(Me.Entity, LCIItem)
          Me.Conversion = lci.GetConversion(Me.Unit)
        Else
          Me.Conversion = 1
        End If
      End If
      If Me.Qty <> 0 Then
        row("stocki_qty") = Configuration.FormatToString(Me.Qty, DigitConfig.Qty)
      Else
        row("stocki_qty") = ""
      End If
      If Me.UnitCost <> 0 Then
        row("stocki_unitcost") = Configuration.FormatToString(Me.UnitCost, DigitConfig.Cost)
      Else
        row("stocki_unitcost") = ""
      End If
      If Me.StockQty <> 0 Then
        row("StockQty") = Configuration.FormatToString(Me.StockQty, DigitConfig.Qty)
      Else
        row("StockQty") = ""
      End If
      If Me.TransferUnitPrice = Decimal.MinValue Then
        row("stocki_transferUnitPrice") = DBNull.Value
      Else
        row("stocki_transferUnitPrice") = Configuration.FormatToString(Me.TransferUnitPrice, DigitConfig.UnitPrice)
      End If
      If Me.TransferAmount <> 0 Then
        row("stocki_transferamt") = Configuration.FormatToString(Me.TransferAmount, DigitConfig.Price)
      Else
        row("stocki_transferamt") = DBNull.Value
      End If
      row("stocki_note") = Me.Note
      Me.MatTransfer.IsInitialized = True
    End Sub
    Public Sub UpdateWBSQty()
      'For Each wbsd As WBSDistribute In Me.InWbsdColl
      '  'Dim bfTax As Decimal = 0
      '  'Dim oldVal As Decimal = wbsd.TransferAmount
      '  'Dim transferAmt As Decimal = Me.Amount
      '  'wbsd.BaseCost = bfTax
      '  'wbsd.TransferBaseCost = transferAmt
      '  Dim boqConversion As Decimal = wbsd.WBS.GetBoqItemConversion(Me.Entity.Id, Me.Unit.Id)
      '  If boqConversion = 0 Then
      '    wbsd.BaseQty = Me.Qty
      '  Else
      '    wbsd.BaseQty = Me.Qty * (Me.Conversion / boqConversion)
      '  End If

      '  'Me.WBSChangedHandler(wbsd, New PropertyChangedEventArgs("Percent", wbsd.TransferAmount, oldVal))
      'Next
      For Each wbsd As WBSDistribute In Me.WBSDistributeCollection
        'Dim bfTax As Decimal = 0
        'Dim oldVal As Decimal = wbsd.TransferAmount
        'Dim transferAmt As Decimal = Me.Amount
        'wbsd.BaseCost = bfTax
        'wbsd.TransferBaseCost = transferAmt
        Dim boqConversion As Decimal = wbsd.WBS.GetBoqItemConversion(Me.Entity.Id, Me.Unit.Id)
        If boqConversion = 0 Then
          wbsd.BaseQty = Me.Qty
        Else
          wbsd.BaseQty = Me.Qty * (Me.Conversion / boqConversion)
        End If

        'Me.WBSChangedHandler(wbsd, New PropertyChangedEventArgs("Percent", wbsd.TransferAmount, oldVal))
      Next
    End Sub
#End Region

#Region "IAllowWBSAllocatableItem"
    Public ReadOnly Property AllowWBSAllocateFrom As Boolean Implements IAllowWBSAllocatableItem.AllowWBSAllocateFrom
      Get
        Return True
      End Get
    End Property

    Public ReadOnly Property AllowWBSAllocateTo As Boolean Implements IAllowWBSAllocatableItem.AllowWBSAllocateTo
      Get
        Return False
      End Get
    End Property
#End Region

#Region "IWBSAllocatableItem"

    Public ReadOnly Property AllocationErrorMessage As String Implements IWBSAllocatableItem.AllocationErrorMessage
      Get
        Return ""
      End Get
    End Property

    Public ReadOnly Property AllocationType As String Implements IWBSAllocatableItem.AllocationType
      Get
        Return "mat"
      End Get
    End Property

    Public ReadOnly Property Description As String Implements IWBSAllocatableItem.Description
      Get
        Return Me.Entity.Code & " : " & Trim(Me.Entity.Name)
      End Get
    End Property

    Public ReadOnly Property Type As String Implements IWBSAllocatableItem.Type
      Get
        Dim strType As String = CodeDescription.GetDescription("stocki_enitytype", 42)
        Return strType
      End Get
    End Property

#End Region

  End Class

  <Serializable(), DefaultMember("Item")> _
  Public Class MatTransferItemCollection
    Inherits CollectionBase

#Region "Members"
    Private m_matTransfer As MatTransfer
#End Region

#Region "Events"
    Public Event StoreApprove As StoreApproveHandler
    Public Delegate Sub StoreApproveHandler(ByVal sender As Object, ByVal e As StoreApproveEventArgs)
    Public Class StoreApproveEventArgs
      Inherits EventArgs

#Region "Members"
      Private m_approveHash As Hashtable
#End Region

#Region "Constructors"
      Public Sub New()
        MyBase.New()
      End Sub
      Public Sub New(ByVal approveHash As Hashtable)
        MyBase.New()
        m_approveHash = approveHash
      End Sub
#End Region

#Region "Properties"
      Public Property ApproveHash() As Hashtable
        Get
          Return m_approveHash
        End Get
        Set(ByVal Value As Hashtable)
          m_approveHash = Value
        End Set
      End Property

#End Region

    End Class

#End Region

#Region "Constructors"
    Public Sub New()
    End Sub
    Public Sub New(ByVal owner As MatTransfer, ByVal group As Boolean)
      Me.m_matTransfer = owner
      If Not Me.m_matTransfer.Originated Then
        Return
      End If

      Dim sqlConString As String = RecentCompanies.CurrentCompany.ConnectionString

      Dim ds As DataSet = SqlHelper.ExecuteDataset(sqlConString _
      , CommandType.StoredProcedure _
      , "GetmatTransferItems" _
      , New SqlParameter("@stock_id", Me.m_matTransfer.Id) _
      , New SqlParameter("@grouping", group) _
      )

      For Each row As DataRow In ds.Tables(0).Rows
        Dim item As New MatTransferItem(row, "")
        item.matTransfer = m_matTransfer
        Me.Add(item)

        'Dim inWbsdColl As WBSDistributeCollection = New WBSDistributeCollection
        'item.InWbsdColl = inWbsdColl
        'For Each wbsRow As DataRow In ds.Tables(1).Select("stockiw_sequence=" & row("stocki_sequence").ToString & "and stockiw_direction=0")
        '  Dim wbsd As New WBSDistribute(wbsRow, "")
        '  inWbsdColl.Add(wbsd)
        'Next
        Dim outWbsdColl As WBSDistributeCollection = New WBSDistributeCollection
        item.WBSDistributeCollection = outWbsdColl
        For Each wbsRow As DataRow In ds.Tables(1).Select("stockiw_sequence=" & row("stocki_sequence").ToString & "and stockiw_direction=1")
          Dim wbsd As New WBSDistribute(wbsRow, "")
          outWbsdColl.Add(wbsd)
        Next

        Dim icCol As StockCostItemCollection = New StockCostItemCollection
        item.ItemCostCollection = icCol
        For Each icRow As DataRow In ds.Tables(2).Select("stockic_stockisequence=" & row("stocki_sequence").ToString)
          Dim itmcost As New StockCostItem(icRow, "")
          icCol.Add(itmcost)
        Next

      Next
    End Sub
#End Region

#Region "Properties"
    Public Property MatTransfer() As MatTransfer
      Get
        Return CType(MyBase.List.Item(index), MatTransferItem)
      End Get
      Set(ByVal value As MatTransferItem)
        MyBase.List.Item(index) = value
      End Set
    End Property
#End Region

#Region "Class Methods"
    Public Sub CheckPRForStoreApprove()
      Dim approveHash As New Hashtable
      For Each item As MatTransferItem In Me
        If Not item.Pritem Is Nothing AndAlso Not item.Pritem.Pr Is Nothing Then
          'MessageBox.Show(String.Format("Qty:{0}, PRQty:{1}, Qty:{2}", item.Qty, item.Pritem.Qty, item.Pritem.WithdrawnQty))
          'If item.Qty > 0 AndAlso (item.Pritem.Qty - item.Pritem.WithdrawnQty) > 0 AndAlso IsDBNull(item.Pritem.Pr.ApproveStoreDate) Then
          If item.Pritem.Pr.ApproveStorePerson Is Nothing OrElse item.Pritem.Pr.ApproveStorePerson.Code = "" Then
            'If Not (approveHash.Contains(item.Pritem.Pr.Id)) Then
            approveHash(item.Pritem.Pr.Id) = item.Pritem.Pr
            'End If
          End If
        End If
      Next
      RaiseEvent StoreApprove(Me.m_matTransfer, New StoreApproveEventArgs(approveHash))
    End Sub
    Public Sub SetItems(ByVal items As BasketItemCollection)
      Dim cumWithdrawn As New Hashtable
      For i As Integer = 0 To items.Count - 1
        If TypeOf items(i) Is StockBasketItem Then
          Dim item As StockBasketItem = CType(items(i), StockBasketItem)
          'If Not TypeOf item.Tag Is BoqItem Then
          Dim pri As PRItem = CType(item.Tag, PRItem)
          If pri.ItemType.Value = 42 Then
            Dim p As New PR
            p.Id = item.Id
            p.Code = item.StockCode
            pri.Pr = p
            Dim mwi As New MatTransferItem
            Me.Add(mwi)
            If Not (cumWithdrawn.Contains(pri.Entity.Id)) Then
              cumWithdrawn(pri.Entity.Id) = 0
            End If
            mwi.CopyFromPRItem(pri, CType(cumWithdrawn(pri.Entity.Id), Decimal))

            cumWithdrawn(pri.Entity.Id) = CType(cumWithdrawn(pri.Entity.Id), Decimal) + (pri.Qty * pri.Conversion)

          End If
          'End If
        End If
      Next
    End Sub
    Public Sub Populate(ByVal dt As TreeTable, ByVal tg As DataGrid)
      dt.Clear()
      Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
      Dim noPRText As String = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.POPanelView.BlankPRText}")
      Dim prRowHash As New Hashtable
      Dim parRow As TreeRow

      Dim isgroupping As Boolean = Me.matTransfer.Grouping
      For Each mwi As MatTransferItem In Me
        parRow = Nothing
        If Not mwi.Pritem Is Nothing _
        AndAlso Not mwi.Pritem.Pr Is Nothing AndAlso mwi.Pritem.Pr.Originated Then
          If Not prRowHash.Contains(mwi.Pritem.Pr.Id) Then
            parRow = dt.Childs.Add
            parRow("PRItemCode") = mwi.Pritem.Pr.Code
            parRow("Button") = "invisible"
            parRow("UnitButton") = "invisible"
            parRow.State = RowExpandState.Expanded
            prRowHash(mwi.Pritem.Pr.Id) = parRow
          Else
            parRow = CType(prRowHash(mwi.Pritem.Pr.Id), TreeRow)
          End If
        Else
          'Ẻ����� PR
          If Not prRowHash.Contains(0) Then
            parRow = dt.Childs.Add
            parRow("PRItemCode") = noPRText
            parRow("Button") = "invisible"
            parRow("UnitButton") = "invisible"
            parRow.State = RowExpandState.Expanded
            prRowHash(0) = parRow
          Else
            parRow = CType(prRowHash(0), TreeRow)
          End If
        End If

        Dim newRow As TreeRow = parRow.Childs.Add()
        mwi.CopyToDataRow(newRow)
        mwi.ItemValidateRow(newRow)
        If Not Me.matTransfer.Grouping Then
          For Each mwci As StockCostItem In mwi.ItemCostCollection
            Dim newCost As TreeRow = newRow.Childs.Add
            newCost("stocki_qty") = Configuration.FormatToString(mwci.StockQty / mwi.Conversion, DigitConfig.Qty)
            If mwci.IsNoCost Then
              newCost("stocki_unitcost") = "No Cost"
              newCost("stocki_transferUnitPrice") = "No Cost"
              newCost("stocki_transferamt") = "No Cost"
            Else
              newCost("stocki_unitcost") = Configuration.FormatToString(mwci.UnitCost, DigitConfig.Cost)
              newCost("stocki_transferUnitPrice") = Configuration.FormatToString(mwci.UnitCost, DigitConfig.UnitPrice)
              newCost("stocki_transferamt") = Configuration.FormatToString(mwci.UnitCost * mwci.StockQty, DigitConfig.Price)
            End If
            newCost("StockQty") = Configuration.FormatToString(mwci.StockQty, DigitConfig.Qty)
            If Not mwci.UnitDefault Is Nothing Then
              newCost("defaultunit") = mwci.UnitDefault.Name
            End If
            newCost.FixLevel = -1
            newCost("Button") = "invisible"
            newCost("UnitButton") = "invisible"
          Next
        End If

        newRow.Tag = mwi
      Next
      If Not prRowHash.Contains(0) Then
        parRow = dt.Childs.Add
        parRow("PRItemCode") = noPRText
        parRow("Button") = "invisible"
        parRow("UnitButton") = "invisible"
        parRow.State = RowExpandState.Expanded
        prRowHash(0) = parRow
      End If



      dt.AcceptChanges()

      Do Until dt.Rows.Count > tg.VisibleRowCount
        '�����Ǩ����
        dt.Childs.Add()
      Loop

      Try
        If (Not dt.Rows(dt.Rows.Count - 1).IsNull("PRItemCode")) OrElse (Not CType(dt.Rows(dt.Rows.Count - 1), TreeRow).Tag Is Nothing) Then
          '  '�����ա 1 �� ����բ����Ũ��֧���ش����
          dt.Childs.Add()
        End If
      Catch ex As Exception

      End Try

      dt.AcceptChanges()
    End Sub
#End Region

#Region "Collection Methods"
    Public Overridable Function Add(ByVal value As MatTransferItem) As Integer
      If Not m_matTransfer Is Nothing Then
        value.matTransfer = m_matTransfer
      End If
      Return MyBase.List.Add(value)
    End Function
    Public Sub AddRange(ByVal value As MatTransferItemCollection)
      For i As Integer = 0 To value.Count - 1
        Me.Add(value(i))
      Next
    End Sub
    Public Sub AddRange(ByVal value As MatTransferItem())
      For i As Integer = 0 To value.Length - 1
        Me.Add(value(i))
      Next
    End Sub
    Public Function Contains(ByVal value As MatTransferItem) As Boolean
      Return MyBase.List.Contains(value)
    End Function
    Public Sub CopyTo(ByVal array As MatTransferItem(), ByVal index As Integer)
      MyBase.List.CopyTo(array, index)
    End Sub
    Public Shadows Function GetEnumerator() As matTransferItemEnumerator
      Return New matTransferItemEnumerator(Me)
    End Function
    Public Function IndexOf(ByVal value As MatTransferItem) As Integer
      Return MyBase.List.IndexOf(value)
    End Function
    Public Overridable Sub Insert(ByVal index As Integer, ByVal value As MatTransferItem)
      If Not m_matTransfer Is Nothing Then
        value.matTransfer = m_matTransfer
      End If
      MyBase.List.Insert(index, value)
    End Sub
    Public Sub Remove(ByVal value As MatTransferItem)
      MyBase.List.Remove(value)
    End Sub
    Public Sub Remove(ByVal value As MatTransferItemCollection)
      For i As Integer = 0 To value.Count - 1
        Me.Remove(value(i))
      Next
    End Sub
    Public Sub Remove(ByVal index As Integer)
      MyBase.List.RemoveAt(index)
    End Sub
#End Region

    Public Class MatTransferItemEnumerator
      Implements IEnumerator

#Region "Members"
      Private m_baseEnumerator As IEnumerator
      Private m_temp As IEnumerable
#End Region

#Region "Construtor"
      Public Sub New(ByVal mappings As MatTransferItemCollection)
        Me.m_temp = mappings
        Me.m_baseEnumerator = Me.m_temp.GetEnumerator
      End Sub
#End Region

      Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
        Get
          Return CType(Me.m_baseEnumerator.Current, MatTransferItem)
        End Get
      End Property

      Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
        Return Me.m_baseEnumerator.MoveNext
      End Function

      Public Sub Reset() Implements System.Collections.IEnumerator.Reset
        Me.m_baseEnumerator.Reset()
      End Sub
    End Class
  End Class

End Namespace
