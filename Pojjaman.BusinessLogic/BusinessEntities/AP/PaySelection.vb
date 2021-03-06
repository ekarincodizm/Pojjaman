﻿Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.TextHelper
Imports System.Collections.Generic
Imports Longkong.Core.AddIns
Imports System.Threading.Tasks

Namespace Longkong.Pojjaman.BusinessLogic
  Public Class Stock
    Public Property VatCodes As List(Of String)
    Public Property GLCode As String
    Public Property GLNote As String
    Public Property Note As String
    Public Property Id As Integer
    Public Property Type As Integer

    Function GetVatCodes() As String
      Dim ret As String = ""
      For Each vc As String In VatCodes
        ret &= "," & vc
      Next
      ret = ret.TrimStart(","c)
      Return ret
    End Function
    Public Shared Function GetListDatatableForEquipmenttoolChangestatus(ByVal ParamArray filters() As Filter) As DataTable

      Dim sqlConString As String = SimpleBusinessEntityBase.ConnectionString
      Dim params() As SqlParameter
      If Not filters Is Nothing AndAlso filters.Length > 0 Then
        ReDim params(filters.Length - 1)
        For i As Integer = 0 To filters.Length - 1
          params(i) = New SqlParameter("@" & filters(i).Name, filters(i).Value)
        Next
      End If
      Dim ds As DataSet = SqlHelper.ExecuteDataset(sqlConString, CommandType.StoredProcedure, "GetStockItemsForECList", params)

      Return ds.Tables(0)

    End Function
  End Class

  Public Class PaySelectionStatus
    Inherits CodeDescription

#Region "Constructors"
    Public Sub New(ByVal value As Integer)
      MyBase.New(value)
    End Sub
#End Region

#Region "Properties"
    Public Overrides ReadOnly Property CodeName() As String
      Get
        Return "pays_status"
      End Get
    End Property
#End Region

  End Class
  Public Class PaySelection
    Inherits SimpleBusinessEntityBase
    Implements IGLAble, IWitholdingTaxable, IPrintableEntity, IPayable, IHasIBillablePerson, ICancelable, IVatable, IGLCheckingBeforeRefresh, IHasCurrency, IPurchaseCNItemAble _
      , INewGLAble, ICheckPeriod, INewPrintableEntity, IDocStatus

#Region "Members"
    Private m_supplier As Supplier
    Private m_docDate As Date
    Private m_olddocDate As Date

    Private m_note As String
    Private m_creditPeriod As Long

    Private m_status As PaySelectionStatus

    Private m_payment As Payment
    Private m_je As JournalEntry
    Private m_whtcol As WitholdingTaxCollection

    Private m_itemCollection As BillAcceptanceItemCollection

    Private m_vat As Vat

    Private m_refWHTCollection As New ArrayList
    Private m_realTaxBaes As Decimal
    Private m_purchaseCNItemColl As PurchaseCNItemCollection
    Private m_taxType As TaxType
    Private m_discount As Discount
#End Region

#Region "Constructors"
    Private _getCCFromItem As CostCenter
    Public Sub New()
      MyBase.New()
    End Sub
    Public Sub New(ByVal code As String)
      MyBase.New(code)
    End Sub
    Public Sub New(ByVal id As Integer)
      MyBase.New(id)
    End Sub
    Public Sub New(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Me.Construct(ds, aliasPrefix)
    End Sub
    Public Sub New(ByVal dr As System.Data.DataRow, ByVal aliasPrefix As String)
      Me.Construct(dr, aliasPrefix)
    End Sub
    Protected Overloads Overrides Sub Construct(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Dim dr As DataRow = ds.Tables(0).Rows(0)
      Construct(dr, aliasPrefix)
    End Sub
    Protected Overloads Overrides Sub Construct()
      MyBase.Construct()
      With Me
        .m_vat = New Vat(Me)
        .m_vat.Direction.Value = 1
        .m_supplier = New Supplier
        .m_creditPeriod = 0
        .m_note = ""
        .m_docDate = Date.Now.Date
        .m_olddocDate = Date.Now.Date
        .m_status = New PaySelectionStatus(-1)
        .m_payment = New Payment(Me)

        '==Checking for addin for วิศวพัฒน์
        Dim hasExport As Boolean = False
        For Each a As AddIn In AddInTreeSingleton.AddInTree.AddIns
          If a.FileName.ToLower.Contains("textexport") Then
            hasExport = True
          End If
        Next
        .m_payment.OnHold = hasExport

        .m_je = New JournalEntry(Me)
        .m_je.DocDate = Me.m_docDate
        .m_whtcol = New WitholdingTaxCollection
        .m_whtcol.Direction = New WitholdingTaxDirection(1)
        .m_payment.DocDate = .m_je.DocDate
        m_itemCollection = New BillAcceptanceItemCollection(Me)
        .AutoCodeFormat = New AutoCodeFormat(Me)
      End With
    End Sub
    Protected Overloads Overrides Sub Construct(ByVal dr As System.Data.DataRow, ByVal aliasPrefix As String)
      MyBase.Construct(dr, aliasPrefix)
      With Me

        If dr.Table.Columns.Contains("supplier_id") Then
          If Not dr.IsNull("supplier_id") Then
            '.m_supplier = New Supplier(dr, "supplier.")
            .m_supplier = New Supplier(CInt(dr("supplier_id"))) ' Supplier.GetSupplierbyDataRow(dr)
          End If
        Else
          If Not dr.IsNull(aliasPrefix & "pays_supplier") Then
            .m_supplier = New Supplier(CInt(dr(aliasPrefix & "pays_supplier")))
          End If
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "pays_creditperiod") AndAlso Not dr.IsNull(aliasPrefix & "pays_creditperiod") Then
          .m_creditPeriod = CInt(dr(aliasPrefix & "pays_creditperiod"))
        End If

        If dr.Table.Columns.Contains("pays_docDate") AndAlso Not dr.IsNull(aliasPrefix & "pays_docDate") Then
          .m_docDate = CDate(dr(aliasPrefix & "pays_docDate"))
          .m_olddocDate = CDate(dr(aliasPrefix & "pays_docDate"))
        End If

        If dr.Table.Columns.Contains("pays_note") AndAlso Not dr.IsNull(aliasPrefix & "pays_note") Then
          .m_note = CStr(dr(aliasPrefix & "pays_note"))
        End If

        If dr.Table.Columns.Contains("pays_status") AndAlso Not dr.IsNull(aliasPrefix & "pays_status") Then
          .m_status = New PaySelectionStatus(CInt(dr(aliasPrefix & "pays_status")))
        End If

        'CreateRefDocs()
      End With

      Parallel.Invoke(Sub()
                        m_payment = New Payment(Me)
                      End Sub,
                      Sub()
                        m_je = New JournalEntry(Me)
                      End Sub,
                      Sub()
                        m_whtcol = New WitholdingTaxCollection(Me)
                        m_whtcol.Direction = New WitholdingTaxDirection(1)
                      End Sub,
                      Sub()
                        m_vat = New Vat(Me)
                        m_vat.Direction.Value = 1
                      End Sub,
                      Sub()
                        m_itemCollection = New BillAcceptanceItemCollection(Me)
                      End Sub,
                      Sub()
                        GetPaySelectionRefDocFromDB()
                      End Sub,
                      Sub()
                        Me.AutoCodeFormat = New AutoCodeFormat(Me)
                      End Sub)

      '==============CURRENCY=================================
      'BusinessLogic.Currency.SetCurrencyFromDB(Me)
      '==============CURRENCY=================================
    End Sub
    'Public Function FindStock(ByVal id As Integer, ByVal type As Integer) As Stock
    '  For Each s As Stock In Refinform
    '    If s.Id = id AndAlso s.Type = type Then
    '      Return s
    '    End If
    '  Next
    'End Function
    'Private Sub CreateRefDocs()
    '  Dim ds As DataSet = SqlHelper.ExecuteDataset(Me.ConnectionString _
    '             , CommandType.StoredProcedure _
    '             , "GetPayselectionItemRefDoc" _
    '             , New SqlParameter("@pays_id", Me.Id))

    '  For Each row As DataRow In ds.Tables(0).Rows
    '    Dim dh As New DataRowHelper(row)
    '    Dim id, type As Integer
    '    id = dh.GetValue(Of Integer)("stock_id")
    '    type = dh.GetValue(Of Integer)("stock_type")
    '    Dim s As Stock = FindStock(id, type)
    '    If s Is Nothing Then
    '      s = New Stock
    '      s.Id = id
    '      s.Type = type
    '      s.GLCode = dh.GetValue(Of String)("GLCode")
    '      s.GLNote = dh.GetValue(Of String)("GLNote")
    '      s.Note = dh.GetValue(Of String)("Note")
    '      s.VatCodes = New List(Of String)
    '      Refinform.Add(s)
    '    End If
    '    Dim vatcode As String = dh.GetValue(Of String)("VatCode")
    '    s.VatCodes.Add(vatcode)
    '  Next
    'End Sub
#End Region

#Region "Properties"
    'Private m_RefDocs As List(Of Stock)
    'Public ReadOnly Property Refinform As List(Of Stock)
    '  Get
    '    If m_RefDocs Is Nothing Then
    '      m_RefDocs = New List(Of Stock)
    '    End If
    '    Return m_RefDocs
    '  End Get
    'End Property
    Public Property RefWHTCollection() As ArrayList      Get        Return m_refWHTCollection      End Get      Set(ByVal Value As ArrayList)        m_refWHTCollection = Value      End Set    End Property
    Public Property ItemCollection() As BillAcceptanceItemCollection
      Get
        Return m_itemCollection
      End Get
      Set(ByVal Value As BillAcceptanceItemCollection)
        m_itemCollection = Value
      End Set
    End Property
    Public Property RealTaxBase As Decimal
      Get
        Return m_realTaxBaes
      End Get
      Set(ByVal value As Decimal)
        m_realTaxBaes = value
      End Set
    End Property
    Public Property Supplier() As Supplier Implements IPurchaseCNItemAble.Supplier
      Get
        Return m_supplier
      End Get
      Set(ByVal Value As Supplier)
        Dim oldPerson As IBillablePerson = m_supplier
        If (oldPerson Is Nothing AndAlso Not Value Is Nothing) _          OrElse (Not oldPerson Is Nothing AndAlso Not Value Is Nothing AndAlso oldPerson.Id <> Value.Id) Then          If Not Me.m_whtcol Is Nothing Then
            For Each wht As WitholdingTax In m_whtcol
              wht.UpdateRefDoc(Value, True)
            Next
          End If
        End If
        m_supplier = Value
      End Set
    End Property
    Public Property DocDate() As Date Implements IPayable.Date, IGLAble.Date, IWitholdingTaxable.Date, IVatable.Date, IPurchaseCNItemAble.Docdate, ICheckPeriod.DocDate
      Get
        Return m_docDate
      End Get
      Set(ByVal Value As Date)
        m_docDate = Value
        Me.m_je.DocDate = Value
      End Set
    End Property
    Public ReadOnly Property OldDocDate As Date Implements ICheckPeriod.OldDocDate
      Get
        Return m_olddocDate
      End Get
    End Property
    Public Property Note() As String Implements IPayable.Note, IGLAble.Note, IPurchaseCNItemAble.Note
      Get
        Return m_note
      End Get
      Set(ByVal Value As String)
        m_note = Value
        'Dim config As Object = Configuration.GetConfig("PutGRNoteInOtherTabs")
        'Dim putit As Boolean = False
        'If Not config Is Nothing Then
        'putit = CBool(config)
        'End If        'If putit Then        'If Not Me.Payment Is Nothing Then        Me.Payment.Note = m_note
        'End If        'If Not Me.JournalEntry Is Nothing Then        Me.JournalEntry.Note = m_note
        'End If        'End If
      End Set
    End Property
    Public Property Discount() As Discount Implements IPurchaseCNItemAble.Discount
      Get
        Return m_discount
      End Get
      Set(ByVal value As Discount)
        m_discount = value
        OnPropertyChanged(Me, New PropertyChangedEventArgs)
      End Set
    End Property
    Public Property CreditPeriod() As Long
      Get
        Return m_creditPeriod
      End Get
      Set(ByVal Value As Long)
        m_creditPeriod = Value
      End Set
    End Property
    Public Property PurchaseCNItemCollection() As PurchaseCNItemCollection Implements IPurchaseCNItemAble.PurchaseCNItemCollection
      Get
        Return m_purchaseCNItemColl
      End Get
      Set(ByVal value As PurchaseCNItemCollection)
        m_purchaseCNItemColl = value
      End Set
    End Property
    Public Property TaxType() As TaxType Implements IPurchaseCNItemAble.TaxType
      Get
        Return m_taxType
      End Get
      Set(ByVal Value As TaxType)
        m_taxType = Value
        OnPropertyChanged(Me, New PropertyChangedEventArgs)
      End Set
    End Property
    Public Overrides Property Status() As CodeDescription
      Get
        Return m_status
      End Get
      Set(ByVal Value As CodeDescription)
        m_status = CType(Value, PaySelectionStatus)
      End Set
    End Property
    Public ReadOnly Property Gross() As Decimal
      Get
        Return Me.ItemCollection.Amount
      End Get
    End Property
    Public ReadOnly Property Retention() As Decimal
      Get
        Return Me.ItemCollection.GetRetention
      End Get
    End Property
    Public ReadOnly Property BeforeTax As Decimal
      Get
        'Return Me.ItemCollection.GetBeforeTax
        Dim ret As Decimal = 0
        For Each doc As BillAcceptanceItem In Me.ItemCollection
          ret += doc.BeforeTax
        Next
        Return ret
      End Get
    End Property
    Public ReadOnly Property RemainingAmountAfter() As Decimal
      Get
        Return Me.ItemCollection.RemainingAmount
      End Get
    End Property
    Public ReadOnly Property ItemCount() As Integer
      Get
        Return Me.ItemCollection.Count
      End Get
    End Property
    Public Overrides ReadOnly Property ClassName() As String
      Get
        Return "PaySelection"
      End Get
    End Property
    Public Overrides ReadOnly Property Prefix() As String
      Get
        Return "pays"
      End Get
    End Property
    Public Overrides ReadOnly Property TableName() As String
      Get
        Return "PaySelection"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.PaySelection.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.PaySelection"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.PaySelection"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.PaySelection.ListLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property TabPageText() As String
      Get
        Dim tpt As String = Me.StringParserService.Parse(Me.DetailPanelTitle) & " (" & Me.Code & ")"
        Dim blankSuffix As String = "()"
        If tpt.EndsWith(blankSuffix) Then
          tpt = tpt.Remove(tpt.Length - blankSuffix.Length, blankSuffix.Length)
        End If
        Return tpt
      End Get
    End Property
#End Region

#Region "Shared"
    Public Shared Function GetSchemaTable() As TreeTable
      Dim myDatatable As New TreeTable("PaySelection")

      myDatatable.Columns.Add(New DataColumn("paysi_parentEntityType", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("paysi_parentEntity", GetType(Integer)))

      myDatatable.Columns.Add(New DataColumn("paysi_linenumber", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("paysi_entity", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("paysi_entityType", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("Code", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("Button", GetType(String)))

      Dim dateCol As New DataColumn("DocDate", GetType(Date))
      dateCol.DefaultValue = Date.MinValue
      myDatatable.Columns.Add(dateCol)

      dateCol = New DataColumn("DueDate", GetType(Date))
      dateCol.DefaultValue = Date.MinValue
      myDatatable.Columns.Add(dateCol)

      myDatatable.Columns.Add(New DataColumn("paysi_amt", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("paysi_vatamt", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("UnpaidAmount", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("RemainingAmount", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("RealAmount", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("paysi_note", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("Retention", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("PlusRetention", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("RemainningBalance", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("paysi_retentionType", GetType(Integer)))

      'เพื่อให้แสดง error ตามคอลัมน์เป็นภาษาที่ต้องการ
      Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
      myDatatable.Columns("Code").Caption = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PaySelectionDetail.CodeHeaderText}")
      myDatatable.Columns("paysi_amt").Caption = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PaySelectionDetail.AmountHeaderText}")
      Return myDatatable
    End Function
#End Region

#Region "Methods"
    Private m_ccHash As Hashtable
    Public Sub GetPaySelectionRefDocFromDB()
      Dim ds As DataSet = SqlHelper.ExecuteDataset(SimpleBusinessEntityBase.ConnectionString,
                                                   CommandType.StoredProcedure,
                                                   "GetPaySelectionRefDocList",
                                                   New SqlParameter("@pays_id", Me.Id)
      )
      m_ccHash = New Hashtable
      For Each row As DataRow In ds.Tables(0).Rows
        Dim drh As New DataRowHelper(row)
        Dim icc As New PaySelectionRefDoc
        icc.ListOfRefDocVatCode = New List(Of RefDocVatCode)
        icc.CCID = drh.GetValue(Of Integer)("cc_id")
        icc.CCCode = drh.GetValue(Of String)("cc_code")
        icc.CCName = drh.GetValue(Of String)("cc_name")
        icc.StockId = drh.GetValue(Of Integer)("stock_id")
        icc.StockType = drh.GetValue(Of Integer)("stock_type")
        icc.StockCode = drh.GetValue(Of String)("stock_code")
        icc.StockRetention = drh.GetValue(Of Decimal)("refstock_retention")
        icc.GLCode = drh.GetValue(Of String)("glcode")
        icc.GLNote = drh.GetValue(Of String)("glnote")
        icc.StockNote = drh.GetValue(Of String)("refstock_note")
        icc.RetentionType = drh.GetValue(Of Integer)("retention_type")
        icc.IDType = drh.GetValue(Of String)("idtype")

        For Each vrow As DataRow In ds.Tables(1).Select("stock_id=" & icc.StockId.ToString & " and stock_type=" & icc.StockType.ToString)
          Dim vdrh As New DataRowHelper(vrow)
          Dim ivc As New RefDocVatCode
          ivc.StockId = vdrh.GetValue(Of Integer)("stock_id")
          ivc.StockType = vdrh.GetValue(Of Integer)("stock_type")
          ivc.VatCode = vdrh.GetValue(Of String)("vatcode")
          icc.ListOfRefDocVatCode.Add(ivc)
        Next

        m_ccHash(icc.IDType) = icc
      Next
    End Sub
    Public Function GetPaySelectionRefDocFromHsIDType(ByVal _id As Integer, ByVal _type As Integer, ByVal _retType As Integer) As PaySelectionRefDoc
      If m_ccHash Is Nothing OrElse m_ccHash.Count = 0 Then
        Return New PaySelectionRefDoc
      End If
      Dim _idType As String = _id.ToString & "|" & _type.ToString & "|" & _retType.ToString
      Return CType(m_ccHash(_idType), PaySelectionRefDoc)
    End Function
    Public Function ValidateReferenceDocDate(ByVal bi As BillAcceptanceItem) As Boolean
      If Not bi Is Nothing Then
        If bi.Date <= Me.DocDate Then
          Return True
        End If
      End If
      Return False
    End Function
    Private Sub ResetID(ByVal oldid As Integer, ByVal oldpay As Integer, ByVal oldje As Integer, ByVal oldVatId As Integer)
      Me.Id = oldid
      Me.m_payment.Id = oldpay
      Me.m_je.Id = oldje
      Me.m_vat.Id = oldVatId
      If Not Me.WitholdingTaxCollection Is Nothing Then
        Me.WitholdingTaxCollection.ResetId()
      End If
    End Sub
    Private Sub ResetCode(ByVal oldCode As String, ByVal oldautogen As Boolean, ByVal oldJecode As String, ByVal oldjeautogen As Boolean)
      Me.Code = oldCode
      Me.AutoGen = oldautogen
      Me.m_payment.Code = oldJecode
      Me.m_payment.AutoGen = oldjeautogen
      Me.m_je.Code = oldJecode
      Me.m_je.AutoGen = oldjeautogen
    End Sub

    Public Function OnlyGenGlAtom() As SaveErrorException Implements INewGLAble.OnlyGenGLAtom
      Dim conn As New SqlConnection(Me.ConnectionString)
      conn.Open()
      SubSaveJeAtom(conn)
      conn.Close()
    End Function

    Public Function BeforeSave(ByVal currentUserId As Integer) As SaveErrorException

      Dim ValidateError As SaveErrorException

      ValidateError = Me.Payment.BeforeSave(currentUserId)
      If Not IsNumeric(ValidateError.Message) Then
        Return ValidateError
      End If

      ValidateError = Me.Vat.BeforeSave(currentUserId)
      If Not IsNumeric(ValidateError.Message) Then
        Return ValidateError
      End If

      ValidateError = Me.WitholdingTaxCollection.BeforeSave(currentUserId)
      If Not IsNumeric(ValidateError.Message) Then
        Return ValidateError
      End If

      ValidateError = Me.JournalEntry.BeforeSave(currentUserId)
      If Not IsNumeric(ValidateError.Message) Then
        Return ValidateError
      End If

      Dim cc As CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
      Dim mycc As CostCenter = GetCCFromItem()
      If Not mycc Is Nothing Then
        Me.m_payment.CCId = mycc.Id
        Me.m_whtcol.SetCCId(mycc.Id)
        Me.m_vat.SetCCId(mycc.Id)
      ElseIf Not cc Is Nothing Then
        Me.m_payment.CCId = cc.Id
        Me.m_whtcol.SetCCId(cc.Id)
        Me.m_vat.SetCCId(cc.Id)
      End If

      Return New SaveErrorException("0")

    End Function

    Dim grIdArrayList As ArrayList
    Public Overloads Overrides Function Save(ByVal currentUserId As Integer) As SaveErrorException
      With Me
        Dim oldcode As String
        Dim oldjecode As String
        'Hack by pui ใช้แล้วไปเปลี่ยนตอน save vat, wht ให้ดึง Me.RealTaxBase แทน ไม่งั้นมัน Lock =============
        Me.RealTaxBase = Me.GetTaxBase
        'Hack by pui =========================================================================

        If Originated Then
          If Not Supplier Is Nothing Then
            If Supplier.Canceled Then
              Return New SaveErrorException(StringParserService.Parse("${res:Global.Error.CanceledSupplier}"), New String() {Supplier.Code})
            End If
          End If
        End If

        ''---------------------- เช็คว่ามีเอกสารลดหนี้หลือหรือป่าว 
        If Me.PurchaseCNItemCollection Is Nothing OrElse Me.PurchaseCNItemCollection.Count = 0 Then
          If havePurchaseCN() Then
            Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
            If msgServ.AskQuestion("${res:Global.Question.WantAddPurchaseCN}") Then
              RaiseEvent PurchaseCNClick(Nothing)
              'Return New SaveErrorException(Me.StringParserService.Parse("${res:Global.Error.SaveCanceled}"))
            End If
          End If
        End If                '------------------

        If Me.ItemCollection.Count <= 0 Then
          Return New SaveErrorException(Me.StringParserService.Parse("${res:Global.Error.NoItem}"))
        End If

        grIdArrayList = New ArrayList
        grIdArrayList.AddRange(Me.GetGoodsRecriptIdList())

        'If Me.MaxRowIndex < 0 Then '.ItemTable.Childs.Count = 0 Then
        '    Return New SaveErrorException(Me.StringParserService.Parse("${res:Global.Error.NoItem}"))
        'End If
        Dim returnVal As System.Data.SqlClient.SqlParameter = New SqlParameter
        returnVal.ParameterName = "RETURN_VALUE"
        returnVal.DbType = DbType.Int32
        returnVal.Direction = ParameterDirection.ReturnValue
        returnVal.SourceVersion = DataRowVersion.Current

        ' สร้าง ArrayList จาก Item ของ  SqlParameter ...
        Dim paramArrayList As New ArrayList

        paramArrayList.Add(returnVal)
        If Me.Originated Then
          paramArrayList.Add(New SqlParameter("@pays_id", Me.Id))
        End If

        Dim theTime As Date = Now
        Dim theUser As New User(currentUserId)

        If Me.m_je.Status.Value = 4 Then
          Me.Status.Value = 4
          Me.m_payment.Status.Value = 4
          Me.m_whtcol.SetStatus(4)
          Me.m_vat.Status.Value = 4
        End If
        If Me.Status.Value = 0 Then
          Me.m_payment.Status.Value = 0
          Me.m_whtcol.SetStatus(0)
          Me.m_vat.Status.Value = 0
          Me.m_je.Status.Value = 0
        End If
        If Me.Status.Value = -1 Then
          Me.Status.Value = 2
        End If

        '---- AutoCode Format --------

        Dim oldautogen As Boolean
        Dim oldjeautogen As Boolean

        oldcode = Me.Code
        oldautogen = Me.AutoGen
        oldjecode = Me.m_je.Code
        oldjeautogen = Me.m_je.AutoGen

        Me.m_je.RefreshGLFormat()

        If Not AutoCodeFormat Is Nothing Then


          Select Case Me.AutoCodeFormat.CodeConfig.Value
            Case 0
              If Me.AutoGen Then 'And Me.Code.Length = 0 Then
                Me.Code = Me.GetNextCode
              End If
              Me.m_je.DontSave = True
              Me.m_je.Code = ""
              Me.m_je.DocDate = Me.DocDate
            Case 1
              'ตาม entity
              If Me.AutoGen Then 'And Me.Code.Length = 0 Then
                Me.Code = Me.GetNextCode
              End If
              Me.m_je.Code = Me.Code
            Case 2
              'ตาม gl
              If Me.m_je.AutoGen Then
                Me.m_je.Code = m_je.GetNextCode
              End If
              Me.Code = Me.m_je.Code
            Case Else
              'แยก
              If Me.AutoGen Then 'And Me.Code.Length = 0 Then
                Me.Code = Me.GetNextCode
              End If
              If Me.m_je.AutoGen Then
                Me.m_je.Code = m_je.GetNextCode
              End If
          End Select
        Else
          If Me.AutoGen Then 'And Me.Code.Length = 0 Then
            Me.Code = Me.GetNextCode
          End If
          If Me.m_je.AutoGen Then
            Me.m_je.Code = m_je.GetNextCode
          End If
        End If
        Me.m_payment.Code = m_je.Code
        Me.m_payment.DocDate = m_je.DocDate
        If Me.AutoCodeFormat.CodeConfig.Value = 0 Then
          Me.m_payment.Code = Me.Code
          Me.m_payment.DocDate = Me.DocDate
        End If
        Me.AutoGen = False
        Me.m_payment.AutoGen = False
        Me.m_je.AutoGen = False
        paramArrayList.Add(New SqlParameter("@pays_code", Me.Code))
        paramArrayList.Add(New SqlParameter("@pays_docDate", Me.ValidDateOrDBNull(Me.DocDate)))
        paramArrayList.Add(New SqlParameter("@pays_supplier", Me.ValidIdOrDBNull(Me.Supplier)))
        paramArrayList.Add(New SqlParameter("@pays_creditPeriod", Me.CreditPeriod))
        paramArrayList.Add(New SqlParameter("@pays_note", Me.Note))
        paramArrayList.Add(New SqlParameter("@pays_gross", Me.Gross))
        paramArrayList.Add(New SqlParameter("@pays_status", Me.Status.Value))

        SetOriginEditCancelStatus(paramArrayList, currentUserId, theTime)

        '---==Validated การทำ before save ของหน้าย่อยอื่นๆ ====
        Dim ValidateError2 As SaveErrorException = Me.BeforeSave(currentUserId)
        If Not IsNumeric(ValidateError2.Message) Then
          ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
          Return ValidateError2
        End If
        '---==Validated การทำ before save ของหน้าย่อยอื่นๆ ====

        ' สร้าง SqlParameter จาก ArrayList ...
        Dim sqlparams() As SqlParameter
        sqlparams = CType(paramArrayList.ToArray(GetType(SqlParameter)), SqlParameter())
        Dim trans As SqlTransaction
        Dim conn As New SqlConnection(Me.ConnectionString)
        conn.Open()
        trans = conn.BeginTransaction()

        'HACK================================
        SimpleBusinessEntityBase.Connection = conn
        SimpleBusinessEntityBase.Transaction = trans
        'HACK================================

        Dim oldid As Integer = Me.Id
        Dim oldpay As Integer = Me.m_payment.Id
        Dim oldVatId As Integer = Me.m_vat.Id
        If Not Me.WitholdingTaxCollection Is Nothing Then
          Me.WitholdingTaxCollection.SaveOldID()
        End If
        Dim oldje As Integer = Me.m_je.Id

        Try

          Try
            Me.ExecuteSaveSproc(conn, trans, returnVal, sqlparams, theTime, theUser)
            If IsNumeric(returnVal.Value) Then
              Select Case CInt(returnVal.Value)
                Case -1, -2, -5
                  trans.Rollback()
                  Me.ResetID(oldid, oldpay, oldje, oldVatId)
                  ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)

                  Return New SaveErrorException(returnVal.Value.ToString)
                Case Else
              End Select
            ElseIf IsDBNull(returnVal.Value) OrElse Not IsNumeric(returnVal.Value) Then
              trans.Rollback()
              Me.ResetID(oldid, oldpay, oldje, oldVatId)
              ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
              Return New SaveErrorException(returnVal.Value.ToString)
            End If

            SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "ClearPayselectionItemPVList", New SqlParameter("@pays_id", Me.Id))

            SaveDetail(Me.Id, conn, trans)

            '==============CURRENCY=================================
            'Save Currency
            If Me.Originated Then
              BusinessLogic.Currency.SaveCurrency(Me, conn, trans)
            End If
            '==============CURRENCY=================================

            'Dim cc As CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            'Dim mycc As CostCenter = GetCCFromItem()
            'If Not mycc Is Nothing Then
            '  Me.m_payment.CCId = mycc.Id
            '  Me.m_whtcol.SetCCId(mycc.Id)
            '  Me.m_vat.SetCCId(mycc.Id)
            'ElseIf Not cc Is Nothing Then
            '  Me.m_payment.CCId = cc.Id
            '  Me.m_whtcol.SetCCId(cc.Id)
            '  Me.m_vat.SetCCId(cc.Id)
            'End If
            Dim savePaymentError As SaveErrorException = Me.m_payment.Save(currentUserId, conn, trans)
            If Not IsNumeric(savePaymentError.Message) Then
              trans.Rollback()
              Me.Payment.ResetDetail()
              Me.ResetID(oldid, oldpay, oldje, oldVatId)
              ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
              Return savePaymentError
            Else
              Select Case CInt(savePaymentError.Message)
                Case -1, -2, -5
                  trans.Rollback()
                  Me.Payment.ResetDetail()
                  Me.ResetID(oldid, oldpay, oldje, oldVatId)
                  ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                  Return savePaymentError
                Case Else
              End Select
            End If

            If Not Me.NoVat Then
              Dim saveVatError As SaveErrorException = Me.m_vat.Save(currentUserId, conn, trans)
              If Not IsNumeric(saveVatError.Message) Then
                trans.Rollback()
                Me.Payment.ResetDetail()
                Me.ResetID(oldid, oldpay, oldje, oldVatId)
                ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                Return saveVatError
              Else
                Select Case CInt(saveVatError.Message)
                  Case -1, -2, -5
                    trans.Rollback()
                    Me.Payment.ResetDetail()
                    Me.ResetID(oldid, oldpay, oldje, oldVatId)
                    ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                    Return saveVatError
                  Case Else
                End Select
              End If
            End If

            If Not Me.m_whtcol Is Nothing AndAlso Me.m_whtcol.Count >= 0 Then
              Dim saveWhtError As SaveErrorException = Me.m_whtcol.Save(currentUserId, conn, trans)
              If Not IsNumeric(saveWhtError.Message) Then
                trans.Rollback()
                Me.Payment.ResetDetail()
                Me.ResetID(oldid, oldpay, oldje, oldVatId)
                ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                Return saveWhtError
              Else
                Select Case CInt(saveWhtError.Message)
                  Case -1, -2, -5
                    trans.Rollback()
                    Me.Payment.ResetDetail()
                    Me.ResetID(oldid, oldpay, oldje, oldVatId)
                    ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                    Return saveWhtError
                  Case Else
                End Select
              End If
            End If

            If Me.m_je.Status.Value = -1 Then
              m_je.Status.Value = 3
            End If
            Dim saveJeError As SaveErrorException = Me.m_je.Save(currentUserId, conn, trans)
            If Not IsNumeric(saveJeError.Message) Then
              trans.Rollback()
              Me.Payment.ResetDetail()
              Me.ResetID(oldid, oldpay, oldje, oldVatId)
              ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
              Return saveJeError
            Else
              Select Case CInt(saveJeError.Message)
                Case -1, -5
                  trans.Rollback()
                  Me.Payment.ResetDetail()
                  Me.ResetID(oldid, oldpay, oldje, oldVatId)
                  ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                  Return saveJeError
                Case -2
                  'Post ไปแล้ว
                  Return saveJeError
                Case Else
              End Select
            End If

            '  Me.DeleteRef(conn, trans)
            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateGR_PaySRef" _
            '  , New SqlParameter("@pays_id", Me.Id))
            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateGR_Retention_PaySRef" _
            '  , New SqlParameter("@pays_id", Me.Id))
            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePCN_PaySRef" _
            '  , New SqlParameter("@pays_id", Me.Id))
            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateBillA_PaySRef" _
            '  , New SqlParameter("@pays_id", Me.Id))
            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateEQMaint_PaySRef" _
            '  , New SqlParameter("@pays_id", Me.Id))
            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateAPO_PaySRef" _
            '  , New SqlParameter("@pays_id", Me.Id))
            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePA_PaySRef" _
            ', New SqlParameter("@pays_id", Me.Id))
            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePA_Retention_PaySRef" _
            ', New SqlParameter("@pays_id", Me.Id))
            '  If Me.Status.Value = 0 Then
            '    Me.CancelRef(conn, trans)
            '  End If

            '  SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePayselectionItemPVList", New SqlParameter("@pays_id", Me.Id))

            '==============================AUTOGEN==========================================
            Dim saveAutoCodeError As SaveErrorException = SaveAutoCode(conn, trans)
            If Not IsNumeric(saveAutoCodeError.Message) Then
              trans.Rollback()
              Me.Payment.ResetDetail()
              ResetID(oldid, oldpay, oldVatId, oldje)
              ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
              Return saveAutoCodeError
            Else
              Select Case CInt(saveAutoCodeError.Message)
                Case -1, -2, -5
                  trans.Rollback()
                  Me.Payment.ResetDetail()
                  ResetID(oldid, oldpay, oldVatId, oldje)
                  ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                  Return saveAutoCodeError
                Case Else
              End Select
            End If
            '==============================AUTOGEN==========================================

            trans.Commit()

            'trans = conn.BeginTransaction()
            'For Each refwhtcoll As WitholdingTaxCollection In m_refWHTCollection
            '  For Each refw As WitholdingTax In refwhtcoll
            '    refw.AutoGen = False
            '  Next
            '  Dim saverefWHTError As SaveErrorException = refwhtcoll.Save(currentUserId, conn, trans)
            '  If Not IsNumeric(saverefWHTError.Message) Then
            '    trans.Rollback()
            '    Me.ResetID(oldid, oldpay, oldje, oldVatId)
            '    Return saverefWHTError
            '  Else
            '    Select Case CInt(saverefWHTError.Message)
            '      Case -1, -2, -5
            '        trans.Rollback()
            '        Me.ResetID(oldid, oldpay, oldje, oldVatId)
            '        Return saverefWHTError
            '      Case Else
            '    End Select
            '  End If
            'Next
            'trans.Commit()
            'Return New SaveErrorException(returnVal.Value.ToString)
          Catch ex As SqlException
            trans.Rollback()
            Me.Payment.ResetDetail()
            Me.ResetID(oldid, oldpay, oldje, oldVatId)
            ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
            Return New SaveErrorException(ex.ToString)
          Catch ex As Exception
            trans.Rollback()
            Me.Payment.ResetDetail()
            Me.ResetID(oldid, oldpay, oldje, oldVatId)
            ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
            Return New SaveErrorException(ex.ToString)
            'Finally
            '  conn.Close()
            '  'HACK================================
            '  SimpleBusinessEntityBase.Connection = Nothing
            '  SimpleBusinessEntityBase.Transaction = Nothing
            '  'HACK================================
          End Try

          '--Sub Save Block-- ============================================================
          Try
            Dim subsaveerror As SaveErrorException = SubSave(conn)
            If Not IsNumeric(subsaveerror.Message) Then
              Return New SaveErrorException(" Save Incomplete Please Save Again")
            End If
            Return New SaveErrorException(returnVal.Value.ToString)
            'Complete Save
          Catch ex As Exception
            Return New SaveErrorException(ex.ToString)
          End Try

          Try
            Dim subsaveerror3 As SaveErrorException = SubSaveJeAtom(conn)
            If Not IsNumeric(subsaveerror3.Message) Then
              Return New SaveErrorException(" Save Incomplete Please Save Again")
            End If
          Catch ex As Exception
            Return New SaveErrorException(ex.ToString)
          End Try
          '--Sub Save Block-- ============================================================



        Catch ex As Exception
          Return New SaveErrorException(ex.ToString)
        Finally
          conn.Close()
          'HACK================================
          SimpleBusinessEntityBase.Connection = Nothing
          SimpleBusinessEntityBase.Transaction = Nothing
          'HACK================================
        End Try
      End With
    End Function
    Private Function SubSave(ByVal conn As SqlConnection) As SaveErrorException

      '======เริ่ม trans 2 ลองผิดให้ save ใหม่ ========
      Dim trans As SqlTransaction = conn.BeginTransaction

      Try
        Me.DeleteRef(conn, trans)
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateGR_PaySRef" _
        , New SqlParameter("@pays_id", Me.Id))
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateGR_Retention_PaySRef" _
        , New SqlParameter("@pays_id", Me.Id))
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePCN_PaySRef" _
        , New SqlParameter("@pays_id", Me.Id))
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateBillA_PaySRef" _
        , New SqlParameter("@pays_id", Me.Id))
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateEQMaint_PaySRef" _
        , New SqlParameter("@pays_id", Me.Id))
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateAPO_PaySRef" _
        , New SqlParameter("@pays_id", Me.Id))
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePA_PaySRef" _
      , New SqlParameter("@pays_id", Me.Id))
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePA_Retention_PaySRef" _
      , New SqlParameter("@pays_id", Me.Id))

        If Me.Status.Value = 0 Then
          Me.CancelRef(conn, trans)
        End If

      Catch ex As Exception
        trans.Rollback()
        Return New SaveErrorException(ex.ToString)
      End Try

      trans.Commit()

      Try
        grIdArrayList.AddRange(Me.GetGoodsRecriptIdList())
        Dim grIdList As String = String.Join(",", grIdArrayList.ToArray)

        Parallel.Invoke(Sub()
                          Me.UpdatePayselectionItemPVList(conn)
                        End Sub,
                        Sub()
                          Me.UpdatePaySelectionReferencePOCode(conn, grIdList)
                        End Sub,
                        Sub()
                          GoodsReceipt.UpdateRemainGoodsReceiptList(conn, grIdList)
                        End Sub,
                        Sub()
                          GoodsReceipt.UpdateReferenceGoodsReceiptList(conn, grIdList)
                        End Sub)

      Catch ex As Exception
        Return New SaveErrorException(ex.Message.ToString())
      End Try

      Return New SaveErrorException("0")
    End Function

    Private Function UpdatePayselectionItemPVList(ByVal oldconn As SqlConnection) As SaveErrorException
      Dim conn As New SqlConnection(oldconn.ConnectionString)
      conn.Open()

      Dim trans As SqlTransaction = conn.BeginTransaction
      Try
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePayselectionItemPVList", New SqlParameter("@pays_id", Me.Id))
      Catch ex As Exception
        trans.Rollback()
        conn.Close()
        Return New SaveErrorException(ex.ToString)
      End Try
      trans.Commit()
      conn.Close()
      Return New SaveErrorException("0")
    End Function

    Private Function UpdatePaySelectionReferencePOCode(ByVal oldconn As SqlConnection, grIdList As String) As SaveErrorException
      Dim conn As New SqlConnection(oldconn.ConnectionString)
      conn.Open()

      Dim trans As SqlTransaction = conn.BeginTransaction
      Try
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePaySelectionReferencePOCode", New SqlParameter("@paysId", Me.Id), New SqlParameter("@grIdList", grIdList))
      Catch ex As Exception
        trans.Rollback()
        conn.Close()
        Return New SaveErrorException(ex.ToString)
      End Try
      trans.Commit()
      conn.Close()
      Return New SaveErrorException("0")
    End Function

    Private Function SubSaveJeAtom(ByVal conn As SqlConnection) As SaveErrorException Implements INewGLAble.SubSaveJeAtom
      Me.JournalEntry.RefreshOnlyGLAtom()
      Dim trans As SqlTransaction = conn.BeginTransaction
      Try
        Me.JournalEntry.SaveAutoMateDetail(Me.JournalEntry.Id, conn, trans)
      Catch ex As Exception
        trans.Rollback()
        Return New SaveErrorException(ex.ToString)
      End Try
      trans.Commit()
      Return New SaveErrorException("0")
    End Function
    Public Overrides Function GetNextCode() As String
      Dim autoCodeFormat As String = Me.Code 'Entity.GetAutoCodeFormat(Me.EntityId)
      Dim pattern As String = CodeGenerator.GetPattern(autoCodeFormat, Me)

      pattern = CodeGenerator.GetPattern(pattern)

      Dim lastCode As String = Me.GetLastCode(pattern)
      Dim newCode As String = _
      CodeGenerator.Generate(autoCodeFormat, lastCode, Me)
      While DuplicateCode(newCode)
        newCode = CodeGenerator.Generate(autoCodeFormat, newCode, Me)
      End While
      Return newCode
    End Function
    Private Function GetRefIdString() As String
      Dim ret As String = ""
      For Each billi As BillAcceptanceItem In Me.ItemCollection
        ret &= billi.Id.ToString & ","
      Next
      If ret.EndsWith(",") Then
        ret = ret.Substring(0, Len(ret) - 1)
      End If
      Return ret
    End Function
    Private Function GetRefBAIdString() As String
      Dim ret As String = ""
      For Each billi As BillAcceptanceItem In Me.ItemCollection
        If billi.ParentId <> 0 And billi.ParentType = 60 Then
          ret &= billi.ParentId.ToString & ","
        End If
      Next
      If ret.EndsWith(",") Then
        ret = ret.Substring(0, Len(ret) - 1)
      End If
      Return ret
    End Function
    Private Function SaveDetail(ByVal parentID As Integer, ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As Integer
      Dim da As New SqlDataAdapter("Select * from PayselectionItem where paysi_pays=" & Me.Id, conn)
      Dim ds As New DataSet

      Dim cmdBuilder As New SqlCommandBuilder(da)
      da.SelectCommand.Transaction = trans
      da.DeleteCommand = cmdBuilder.GetDeleteCommand
      da.DeleteCommand.Transaction = trans
      da.InsertCommand = cmdBuilder.GetInsertCommand
      da.InsertCommand.Transaction = trans
      da.UpdateCommand = cmdBuilder.GetUpdateCommand
      da.UpdateCommand.Transaction = trans
      cmdBuilder = Nothing
      da.FillSchema(ds, SchemaType.Mapped, "PayselectionItem")
      da.Fill(ds, "PayselectionItem")

      Dim dt As DataTable = ds.Tables("PayselectionItem")

      Dim i As Integer = 0
      With ds.Tables("PayselectionItem")
        For Each row As DataRow In .Rows
          row.Delete()
        Next
        For Each billi As BillAcceptanceItem In Me.ItemCollection
          i += 1
          Dim dr As DataRow = .NewRow
          dr("paysi_pays") = Me.Id
          dr("paysi_linenumber") = i
          dr("paysi_parententity") = billi.ParentId
          dr("paysi_parententityType") = billi.ParentType
          dr("paysi_parententityCode") = billi.ParentCode
          dr("stock_id") = billi.Id
          dr("stock_type") = billi.EntityId
          dr("stock_code") = billi.Code
          dr("stock_docdate") = billi.Date
          dr("stock_creditprd") = billi.CreditPeriod
          dr("paysi_amt") = billi.Amount
          dr("paysi_unpaidvatamt") = billi.UnpaidVatAmt
          dr("paysi_vatamt") = billi.VatAmt
          dr("paysi_duevatbase") = billi.DueTaxBase
          dr("paysi_billedamt") = billi.BilledAmount
          dr("paysi_unpaidamt") = billi.UnpaidAmount
          dr("stock_beforetax") = billi.BeforeTax
          dr("stock_aftertax") = billi.AfterTax
          dr("stock_taxBase") = billi.TaxBase
          dr("stock_note") = billi.Note
          dr("stock_status") = Me.Status.Value
          dr("stock_retention") = billi.Retention
          dr("paysi_retentiontype") = billi.RetentionType
          .Rows.Add(dr)
        Next
      End With

      AddHandler da.RowUpdated, AddressOf tmpDa_MyRowUpdated
      da.Update(GetDeletedRows(dt))
      da.Update(dt.Select("", "", DataViewRowState.ModifiedCurrent))
      da.Update(dt.Select("", "", DataViewRowState.Added))

    End Function
    Private Sub tmpDa_MyRowUpdated(ByVal sender As Object, ByVal e As System.Data.SqlClient.SqlRowUpdatedEventArgs)
      If e.StatementType = StatementType.Insert Then e.Status = UpdateStatus.SkipCurrentRow
      If e.StatementType = StatementType.Delete Then e.Status = UpdateStatus.SkipCurrentRow
    End Sub
    Private Function GetDeletedRows(ByVal dt As DataTable) As DataRow()
      Dim Rows() As DataRow
      If dt Is Nothing Then Return Rows
      Rows = dt.Select("", "", DataViewRowState.Deleted)
      If Rows.Length = 0 OrElse Not (Rows(0) Is Nothing) Then Return Rows
      '
      ' Workaround:
      ' With a remoted DataSet, Select returns the array elements
      ' filled with Nothing/null, instead of DataRow objects.
      '
      Dim r As DataRow, I As Integer = 0
      For Each r In dt.Rows
        If r.RowState = DataRowState.Deleted Then
          Rows(I) = r
          I += 1
        End If
      Next
      Return Rows
    End Function
    Public Function GetCCFromItem() As CostCenter
      Dim dummyCC As CostCenter
      For Each item As BillAcceptanceItem In Me.ItemCollection
        Dim docType As Integer = item.EntityId
        If docType = 199 Then
          docType = item.RetentionType
        End If
        'Dim thisCC As CostCenter = GetCCFromDocTypeAndId(docType, item.Id)
        If item.CostCenterId = 0 Then
          Return Nothing
        End If
        'Dim thisCC As CostCenter = CostCenter.GetCostCenter(item.CostCenterId, ViewType.PaySelection)
        Dim thisCC As CostCenter = CostCenter.GetCCMinDataById(item.CostCenterId)
        If dummyCC IsNot Nothing AndAlso dummyCC.Id <> thisCC.Id Then
          Return CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        dummyCC = thisCC
      Next
      Return dummyCC
    End Function

    Public Function GetCostCenterFromRefDoc(ByVal stock_id As Integer, ByVal stock_type As Integer, ByVal retention_type As Integer) As CostCenter
      Try
        Dim dsr As DataSet = SqlHelper.ExecuteDataset( _
            Me.ConnectionString _
            , CommandType.StoredProcedure _
            , "GetCCForBillAccaptance" _
            , New SqlParameter("@stock_id", stock_id) _
            , New SqlParameter("@stock_type", stock_type) _
            , New SqlParameter("@retention_type", retention_type) _
            )
        If dsr.Tables(0).Rows.Count > 0 Then
          If Not dsr.Tables(0).Rows(0).IsNull("cc_id") Then
            'Return New CostCenter(dsr.Tables(0).Rows(0), "")
            Return CostCenter.GetCostCenter(dsr.Tables(0).Rows(0), ViewType.PaySelection)
          End If
        End If
        Return New CostCenter
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End Function
    Public Function GetRetentionItem(ByVal entityid As Integer, ByVal entityType As Integer, ByVal retention_type As Integer) As Decimal
      Dim ds As DataSet
      If retention_type = 45 Then
        ds = SqlHelper.ExecuteDataset(ConnectionString _
        , CommandType.Text _
        , "SELECT isnull(stock_retention,0) [stock_retention] FROM stock " & _
          "WHERE stock_type = 45 " & _
          "AND stock_id = " & entityid)
        If ds.Tables(0).Rows.Count > 0 Then
          If IsNumeric(ds.Tables(0).Rows(0)(0)) Then
            Return CDec(ds.Tables(0).Rows(0)(0))
          End If
        End If
      ElseIf retention_type = 292 Then
        ds = SqlHelper.ExecuteDataset(ConnectionString _
        , CommandType.Text _
        , "SELECT isnull(pa_retention,0) [stock_retention] FROM pa " & _
          "WHERE pa_id = " & entityid)
        If ds.Tables(0).Rows.Count > 0 Then
          If IsNumeric(ds.Tables(0).Rows(0)(0)) Then
            Return CDec(ds.Tables(0).Rows(0)(0))
          End If
        End If

      End If

      Return 0
    End Function
    Private Function PurchaseCNIDList() As String
      Dim Arr As New ArrayList
      For Each item As BillAcceptanceItem In Me.ItemCollection
        If item.EntityId = 46 Then
          Arr.Add(item.Id)
        End If
      Next
      Return String.Join(","c, Arr.ToArray)
    End Function
    Private Function havePurchaseCN() As Boolean
      Dim ds As DataSet = SqlHelper.ExecuteDataset( _
               Me.ConnectionString _
               , CommandType.StoredProcedure _
               , "GethavePurchaseCNFromSupandEntity" _
               , New SqlParameter("@supplier_id", Me.Supplier.Id) _
               , New SqlParameter("@entity_type", Me.EntityId) _
               , New SqlParameter("@PurchaseCNIDList", Me.PurchaseCNIDList) _
               )
      If ds.Tables(0).Rows.Count > 0 Then
        Return True
      End If
      Return False
    End Function
#End Region

#Region "IPayable"
    Public Function AmountToPay() As Decimal Implements IPayable.AmountToPay
      Return Configuration.Format(Me.Gross, DigitConfig.Price)
    End Function
    Public Property DueDate() As Date Implements IPayable.DueDate
      Get
        Try
          Return Me.DocDate.AddDays(Me.CreditPeriod)
        Catch ex As Exception
          Return Me.DocDate
        End Try
      End Get
      Set(ByVal Value As Date)

      End Set
    End Property
    Public Property Payment() As Payment Implements IPayable.Payment
      Get
        Return m_payment
      End Get
      Set(ByVal Value As Payment)
        m_payment = Value
      End Set
    End Property
    Public ReadOnly Property Recipient() As IBillablePerson Implements IPayable.Recipient
      Get
        Return Me.Supplier
      End Get
    End Property
    Public Function RemainingAmount() As Decimal Implements IPayable.RemainingAmount
      'Undone
      Return AmountToPay()
    End Function
#End Region

#Region "IGLAble"
    Public Function GetDefaultGLFormat() As GLFormat Implements IGLAble.GetDefaultGLFormat
      If Not Me.AutoCodeFormat.GLFormat Is Nothing AndAlso Me.AutoCodeFormat.GLFormat.Originated Then
        Return Me.AutoCodeFormat.GLFormat
      End If
      Dim ds As DataSet = SqlHelper.ExecuteDataset(Me.ConnectionString _
      , CommandType.StoredProcedure _
      , "GetGLFormatForEntity" _
      , New SqlParameter("@entity_id", Me.EntityId), New SqlParameter("@default", 1))
      Dim glf As New GLFormat(ds.Tables(0).Rows(0), "")
      Return glf
    End Function
    Public Function GetJournalEntries() As JournalEntryItemCollection Implements IGLAble.GetJournalEntries
      Dim jiColl As New JournalEntryItemCollection
      Dim ji As JournalEntryItem
      Dim myCC As CostCenter
      myCC = GetCCFromItem()
      If myCC Is Nothing OrElse Not myCC.Originated Then
        myCC = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
      End If
      Dim tmp As Object = Configuration.GetConfig("APRetentionPoint")
      Dim apRetentionPoint As Integer = 0
      If IsNumeric(tmp) Then
        apRetentionPoint = CInt(tmp)
      End If
      Dim retentionHere As Boolean = (apRetentionPoint = 1)

      Dim ccList As New Dictionary(Of Integer, CostCenter)

      For Each doc As BillAcceptanceItem In Me.ItemCollection
        Dim docType As Integer = doc.EntityId
        If docType = 199 Then
          docType = doc.RetentionType
        End If
        'Dim itemCC As CostCenter = GetCCFromDocTypeAndId(docType, doc.Id)
        'Dim itemCC As CostCenter = CostCenter.GetCostCenter(doc.CostCenterId, ViewType.PaySelection)
        Dim itemCC As CostCenter = CostCenter.GetCCMinDataById(doc.CostCenterId)
        Dim itemCode As String = doc.Code.ToString
        Dim itemType As String = GetTypeNameFromDocType(doc.EntityId)
        If itemCC Is Nothing Then
          itemCC = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        Dim myGross As Decimal = doc.AmountForGL
        Dim myRetention As Decimal = doc.RetentionForGL
        Dim myDebt As Decimal = myGross - myRetention

        If myDebt <> 0 Then
          'เจ้าหนี้การค้า
          ji = New JournalEntryItem
          ji.Mapping = "B8.1"
          If retentionHere Then
            ji.Amount = myDebt + doc.Retention
          Else
            ji.Amount = myDebt
          End If
          If Not Me.Supplier.Account Is Nothing AndAlso Me.Supplier.Account.Originated Then
            ji.Account = Me.Supplier.Account
          End If

          ji.CostCenter = itemCC
          ji.Note = itemType
          jiColl.Add(ji)
        End If

        If myDebt <> 0 Then
          'เจ้าหนี้การค้า แบบแยก D
          ji = New JournalEntryItem
          ji.Mapping = "B8.1D"
          If retentionHere Then
            ji.Amount = myDebt + doc.Retention
          Else
            ji.Amount = myDebt
          End If
          If Not Me.Supplier.Account Is Nothing AndAlso Me.Supplier.Account.Originated Then
            ji.Account = Me.Supplier.Account
          End If

          ji.CostCenter = itemCC
          ji.EntityItem = doc.Id
          ji.EntityItemType = doc.EntityId
          ji.Note = itemCode & ":" & itemType
          jiColl.Add(ji)
        End If

        If myDebt <> 0 Then
          'เจ้าหนี้การค้า แบบแยก W
          ji = New JournalEntryItem
          ji.Mapping = "B8.1W"
          If retentionHere Then
            ji.Amount = myDebt + doc.Retention
          Else
            ji.Amount = myDebt
          End If
          If Not Me.Supplier.Account Is Nothing AndAlso Me.Supplier.Account.Originated Then
            ji.Account = Me.Supplier.Account
          End If

          ji.CostCenter = itemCC
          ji.Note = itemCode & ":" & itemType
          jiColl.Add(ji)
        End If

        If myRetention <> 0 Then
          'เจ้าหนี้เงินประกันผลงาน
          ji = New JournalEntryItem
          ji.Mapping = "B8.3"
          ji.Amount = myRetention
          ji.CostCenter = itemCC
          ji.Note = itemCode & ":" & itemType
          jiColl.Add(ji)
        End If

        If myRetention <> 0 Then
          'เจ้าหนี้เงินประกันผลงาน W
          ji = New JournalEntryItem
          ji.Mapping = "B8.3D"
          ji.Amount = myRetention
          ji.CostCenter = itemCC
          ji.EntityItem = doc.Id
          ji.EntityItemType = doc.EntityId
          ji.Note = itemCode & ":" & itemType
          jiColl.Add(ji)
        End If

        If myRetention <> 0 Then
          'เจ้าหนี้เงินประกันผลงาน W
          ji = New JournalEntryItem
          ji.Mapping = "B8.3W"
          ji.Amount = myRetention
          ji.CostCenter = itemCC
          ji.Note = itemCode & ":" & itemType
          jiColl.Add(ji)
        End If

        '===CURRENCY===
        Dim myGrossPaysCurrency As Decimal = doc.AmountPaysConversionForGL
        Dim myRetentionPaysCurrency As Decimal = doc.RetentionPaysConversionForGL
        Dim myDebtPaysCurrency As Decimal = myGrossPaysCurrency - myRetentionPaysCurrency
        Dim diffCurrency As Decimal = myDebt - myDebtPaysCurrency
        If diffCurrency <> 0 Then
          'กำไร/ขาดทุน
          ji = New JournalEntryItem
          ji.Mapping = "THROUGH"
          ji.Amount = diffCurrency
          ji.IsDebit = False
          ji.Account = GeneralAccount.GetDefaultGA(GeneralAccount.DefaultGAType.CurrencyProfitLoss).Account
          ji.CostCenter = itemCC
          ji.Note = itemType
          jiColl.Add(ji)
        End If
        '===CURRENCY===

        'Retention หัก
        If retentionHere AndAlso doc.Retention <> 0 Then
          ji = New JournalEntryItem
          ji.Mapping = "E3.16"
          ji.Amount = doc.Retention
          ji.CostCenter = itemCC
          ji.Note = Me.Recipient.Name & ":" & itemCode & ":" & itemType
          jiColl.Add(ji)
        End If

        'Retention หัก
        If retentionHere AndAlso doc.Retention <> 0 Then
          ji = New JournalEntryItem
          ji.Mapping = "E3.16D"
          ji.Amount = doc.Retention
          ji.CostCenter = itemCC
          ji.EntityItem = doc.Id
          ji.EntityItemType = doc.EntityId
          ji.Note = Me.Recipient.Name & ":" & itemCode & ":" & itemType
          jiColl.Add(ji)
        End If

        'Retention หัก
        If retentionHere AndAlso doc.Retention <> 0 Then
          ji = New JournalEntryItem
          ji.Mapping = "E3.16W"
          ji.Amount = doc.Retention
          ji.CostCenter = itemCC
          ji.Note = Me.Recipient.Name & ":" & itemCode & ":" & itemType
          jiColl.Add(ji)
        End If

      Next

      'ภาษีซื้อ
      If Me.Vat.Amount <> 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "B8.4"
        ji.Amount = Configuration.Format(Me.Vat.Amount, DigitConfig.Price)
        ji.CostCenter = myCC
        jiColl.Add(ji)
      End If

      For Each vi As VatItem In Me.Vat.ItemCollection
        ji = New JournalEntryItem
        ji.Mapping = "B8.4D"
        ji.Amount = Configuration.Format(vi.Amount, DigitConfig.Price)
        ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        ji.EntityItem = Me.Id
        ji.EntityItemType = Me.EntityId
        ji.Note = vi.Code & "/" & vi.PrintName
        jiColl.Add(ji)

      Next

      'ภาษีซื้อไม่ถึงกำหนด
      If Me.GetVatAmt <> 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "B8.5"
        ji.Amount = Configuration.Format(Me.GetVatAmt, DigitConfig.Price)
        ji.CostCenter = myCC
        jiColl.Add(ji)

        'ภาษีซื้อไม่ถึงกำหนด
        For Each pi As BillAcceptanceItem In Me.ItemCollection
          If pi.EntityId <> 46 AndAlso pi.EntityId <> 199 Then
            If pi.Amount <> 0 Then
              ji = New JournalEntryItem
              ji.Mapping = "B8.5D"
              ji.Amount = Configuration.Format(pi.VatAmt, DigitConfig.Price)
              ji.CostCenter = myCC
              ji.EntityItem = pi.Id
              ji.EntityItemType = pi.EntityId
              ji.Note = pi.Code & "/" & pi.itemType
              jiColl.Add(ji)
            End If
          End If
        Next

      End If


      'ภาษีหัก ณ ที่จ่าย
      If Not Me.WitholdingTaxCollection Is Nothing AndAlso Me.WitholdingTaxCollection.Amount > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "B8.2"
        ji.Amount = Me.WitholdingTaxCollection.Amount
        ji.CostCenter = myCC 'CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        jiColl.Add(ji)
      End If
      Dim WHTTypeSum As New Hashtable

      For Each wht As WitholdingTax In Me.WitholdingTaxCollection
        If WHTTypeSum.Contains(wht.Type.Value) Then
          WHTTypeSum(wht.Type.Value) = CDec(WHTTypeSum(wht.Type.Value)) + wht.Amount
        Else
          WHTTypeSum(wht.Type.Value) = wht.Amount
        End If
      Next
      Dim typeNum As String
      For Each obj As Object In WHTTypeSum.Keys
        typeNum = CStr(obj)
        If Not (typeNum.Length > 1) Then
          typeNum = "0" & typeNum
        End If
        If Not IsDBNull(Configuration.GetConfig("WHTAcc" & typeNum)) Then
          ji = New JournalEntryItem
          ji.Mapping = "E3.18"
          ji.Amount = CDec(WHTTypeSum(obj))
          ji.Account = New Account(CStr(Configuration.GetConfig("WHTAcc" & typeNum)))
          ji.CostCenter = myCC
          ji.Note = Me.Recipient.Name
          jiColl.Add(ji)
        End If
      Next
      For Each wht As WitholdingTax In Me.WitholdingTaxCollection
        typeNum = CStr(wht.Type.Value)
        If Not (typeNum.Length > 1) Then
          typeNum = "0" & typeNum
        End If
        If Not IsDBNull(Configuration.GetConfig("WHTAcc" & typeNum)) Then
          ji = New JournalEntryItem
          ji.Mapping = "E3.18D"
          ji.Amount = wht.Amount
          ji.Account = New Account(CStr(Configuration.GetConfig("WHTAcc" & typeNum)))
          ji.CostCenter = myCC
          ji.Note = wht.Code & ":" & Me.Recipient.Name
          jiColl.Add(ji)
        End If
      Next
      For Each wht As WitholdingTax In Me.WitholdingTaxCollection
        typeNum = CStr(wht.Type.Value)
        If Not (typeNum.Length > 1) Then
          typeNum = "0" & typeNum
        End If
        If Not IsDBNull(Configuration.GetConfig("WHTAcc" & typeNum)) Then
          ji = New JournalEntryItem
          ji.Mapping = "E3.18W"
          ji.Amount = wht.Amount
          ji.Account = New Account(CStr(Configuration.GetConfig("WHTAcc" & typeNum)))
          ji.CostCenter = myCC
          ji.Note = Me.Recipient.Name
          jiColl.Add(ji)
        End If
      Next
      Me.Payment.CCId = myCC.Id

      If Not Me.Payment Is Nothing Then
        jiColl.AddRange(Me.Payment.GetJournalEntries)
      End If
      Return jiColl
    End Function
    Public Function NewGetJournalEntries() As JournalEntryItemCollection Implements INewGLAble.NewGetJournalEntries

      Dim jiColl As New JournalEntryItemCollection
      Dim ji As JournalEntryItem
      Dim myCC As CostCenter
      myCC = GetCCFromItem()
      If myCC Is Nothing OrElse Not myCC.Originated Then
        myCC = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
      End If
      Dim tmp As Object = Configuration.GetConfig("APRetentionPoint")
      Dim apRetentionPoint As Integer = 0
      If IsNumeric(tmp) Then
        apRetentionPoint = CInt(tmp)
      End If
      Dim retentionHere As Boolean = (apRetentionPoint = 1)

      Dim ccList As New Dictionary(Of Integer, CostCenter)

      For Each doc As BillAcceptanceItem In Me.ItemCollection
        Dim docType As Integer = doc.EntityId
        If docType = 199 Then
          docType = doc.RetentionType
        End If
        'Dim itemCC As CostCenter = GetCCFromDocTypeAndId(docType, doc.Id)
        'Dim itemCC As CostCenter = CostCenter.GetCostCenter(doc.CostCenterId, ViewType.PaySelection)
        Dim itemCC As CostCenter = CostCenter.GetCCMinDataById(doc.CostCenterId)
        Dim itemCode As String = doc.Code.ToString
        Dim itemType As String = GetTypeNameFromDocType(doc.EntityId)
        If itemCC Is Nothing Then
          itemCC = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        Dim myGross As Decimal = doc.AmountForGL
        Dim myRetention As Decimal = doc.RetentionForGL
        Dim myDebt As Decimal = myGross - myRetention

        If myDebt <> 0 Then
          'เจ้าหนี้การค้า แบบแยก D
          ji = New JournalEntryItem
          ji.Mapping = "B8.1"
          If retentionHere Then
            ji.Amount = myDebt + doc.Retention
          Else
            ji.Amount = myDebt
          End If
          If Not Me.Supplier.Account Is Nothing AndAlso Me.Supplier.Account.Originated Then
            ji.Account = Me.Supplier.Account
          End If

          ji.CostCenter = itemCC
          ji.Note = itemCode & ":" & itemType
          ji.EntityItem = doc.Id
          ji.EntityItemType = doc.EntityId
          ji.table = Me.TableName & "item"
          jiColl.Add(ji)
        End If





        If myRetention <> 0 Then
          'เจ้าหนี้เงินประกันผลงาน W
          ji = New JournalEntryItem
          ji.Mapping = "B8.3"
          ji.Amount = myRetention
          ji.CostCenter = itemCC
          ji.Note = itemCode & ":" & itemType

          ji.EntityItem = doc.Id
          ji.EntityItemType = doc.EntityId
          ji.table = Me.TableName & "item"
          ji.CustomRefstr = doc.RetentionType.ToString
          ji.CustomRefType = "RetentionType"
          ji.AtomNote = "ล้างหนี้ retention"
          jiColl.Add(ji)
        End If



        '===CURRENCY===
        Dim myGrossPaysCurrency As Decimal = doc.AmountPaysConversionForGL
        Dim myRetentionPaysCurrency As Decimal = doc.RetentionPaysConversionForGL
        Dim myDebtPaysCurrency As Decimal = myGrossPaysCurrency - myRetentionPaysCurrency
        Dim diffCurrency As Decimal = myDebt - myDebtPaysCurrency
        If diffCurrency <> 0 Then
          'กำไร/ขาดทุน
          ji = New JournalEntryItem
          ji.Mapping = "THROUGH"
          ji.Amount = diffCurrency
          ji.IsDebit = False
          ji.Account = GeneralAccount.GetDefaultGA(GeneralAccount.DefaultGAType.CurrencyProfitLoss).Account
          ji.CostCenter = itemCC
          ji.Note = itemType

          ji.EntityItemType = Entity.GetIdFromClassName("CurrencyProfitLoss")
          ji.table = Me.TableName & "item"
          ji.ga = GeneralAccount.GetDefaultGA(GeneralAccount.DefaultGAType.CurrencyProfitLoss)
          ji.AtomNote = "กำไร/ขาดทุนจากอัตราแลกเปลี่ยน"
          jiColl.Add(ji)
        End If
        '===CURRENCY===



        'Retention หัก
        If retentionHere AndAlso doc.Retention <> 0 Then
          ji = New JournalEntryItem
          ji.Mapping = "E3.16"
          ji.Amount = doc.Retention
          ji.CostCenter = itemCC
          ji.Note = Me.Recipient.Name & ":" & itemCode & ":" & itemType

          ji.EntityItem = doc.Id
          ji.EntityItemType = doc.EntityId
          ji.table = Me.TableName & "item"
          ji.CustomRefstr = "199"
          ji.CustomRefType = "stock_rentention"
          ji.AtomNote = "เจ้าหนี้ Retention"
          jiColl.Add(ji)
        End If



      Next

      'ภาษีซื้อ

      For Each vi As VatItem In Me.Vat.ItemCollection
        ji = New JournalEntryItem
        ji.Mapping = "B8.4"
        ji.Amount = Configuration.Format(vi.Amount, DigitConfig.Price)
        'อาจต้องระบุ Cc ของภาษีให้ได้
        ji.CostCenter = myCC

        ji.Note = vi.Code & "/" & vi.PrintName
        ji.EntityItem = Me.Vat.Id
        ji.EntityItemType = 97 'ใช้ Incomingvat 
        ji.table = Me.Vat.TableName & "item"
        ji.CustomRefstr = vi.LineNumber.ToString
        ji.CustomRefType = "vati_linenumber"
        jiColl.Add(ji)

      Next

      'ภาษีซื้อไม่ถึงกำหนด
      If Me.GetVatAmt <> 0 Then


        'ล้างภาษีซื้อไม่ถึงกำหนด
        For Each pi As BillAcceptanceItem In Me.ItemCollection
          If pi.EntityId <> 46 AndAlso pi.EntityId <> 199 Then
            If pi.Amount <> 0 Then
              ji = New JournalEntryItem
              ji.Mapping = "B8.5"
              ji.Amount = Configuration.Format(pi.VatAmt, DigitConfig.Price)
              ji.CostCenter = myCC
              ji.Note = pi.Code & "/" & pi.itemType
              'ตอนตั้งกับล้างอ้างอันเดียวกัน
              ji.EntityItem = pi.Id
              ji.EntityItemType = Entity.GetIdFromClassName("VatInNotDue")
              ji.table = Me.TableName & "item"
              ji.CustomRefstr = pi.EntityId.ToString
              ji.CustomRefType = "entity"
              ji.AtomNote = "ล้าง vat not due"
              jiColl.Add(ji)
            End If
          End If
        Next

      End If


      'ภาษีหัก ณ ที่จ่าย
      If Not Me.WitholdingTaxCollection Is Nothing AndAlso Me.WitholdingTaxCollection.Amount > 0 Then

        For Each wht As WitholdingTax In Me.WitholdingTaxCollection

          ji = New JournalEntryItem
          ji.Mapping = "B8.2"
          ji.Amount = wht.Amount
          ji.CostCenter = myCC
          ji.Note = wht.Code & ":" & Me.Recipient.Name
          ji.EntityItem = wht.Id
          ji.EntityItemType = Entity.GetIdFromClassName("WitholdingTax")
          ji.table = wht.TableName
          jiColl.Add(ji)
        Next
      End If

      Dim WHTTypeSum As New Hashtable

      For Each wht As WitholdingTax In Me.WitholdingTaxCollection
        If WHTTypeSum.Contains(wht.Type.Value) Then
          WHTTypeSum(wht.Type.Value) = CDec(WHTTypeSum(wht.Type.Value)) + wht.Amount
        Else
          WHTTypeSum(wht.Type.Value) = wht.Amount
        End If
      Next
      Dim typeNum As String
      'For Each obj As Object In WHTTypeSum.Keys
      '  typeNum = CStr(obj)
      '  If Not (typeNum.Length > 1) Then
      '    typeNum = "0" & typeNum
      '  End If
      '  If Not IsDBNull(Configuration.GetConfig("WHTAcc" & typeNum)) Then
      '    ji = New JournalEntryItem
      '    ji.Mapping = "E3.18"
      '    ji.Amount = CDec(WHTTypeSum(obj))
      '    ji.Account = New Account(CStr(Configuration.GetConfig("WHTAcc" & typeNum)))
      '    ji.CostCenter = myCC
      '    ji.Note = Me.Recipient.Name
      '    jiColl.Add(ji)
      '  End If
      'Next
      For Each wht As WitholdingTax In Me.WitholdingTaxCollection
        typeNum = CStr(wht.Type.Value)
        If Not (typeNum.Length > 1) Then
          typeNum = "0" & typeNum
        End If
        If Not IsDBNull(Configuration.GetConfig("WHTAcc" & typeNum)) Then
          ji = New JournalEntryItem
          ji.Mapping = "E3.18"
          ji.Amount = wht.Amount
          ji.Account = New Account(CStr(Configuration.GetConfig("WHTAcc" & typeNum)))
          ji.CostCenter = myCC
          ji.Note = wht.Code & ":" & Me.Recipient.Name
          ji.EntityItem = wht.Id
          ji.EntityItemType = Entity.GetIdFromClassName("WitholdingTax")
          ji.table = wht.TableName
          jiColl.Add(ji)
        End If
      Next

      Me.Payment.CCId = myCC.Id

      If Not Me.Payment Is Nothing Then
        jiColl.AddRange(Me.Payment.GetNewJournalEntries)
      End If
      Return jiColl
    End Function
    Public Property JournalEntry() As JournalEntry Implements IGLAble.JournalEntry
      Get
        Return Me.m_je
      End Get
      Set(ByVal Value As JournalEntry)
        m_je = Value
      End Set
    End Property
#End Region

#Region "IWitholdingTaxable"
    Public Function GetMaximumWitholdingTaxBase() As Decimal Implements IWitholdingTaxable.GetMaximumWitholdingTaxBase
      Return Me.GetWitholdingTaxBase() 'Me.BeforeTax
    End Function
    Public Property Person() As IBillablePerson Implements IWitholdingTaxable.Person, IVatable.Person
      Get
        Return Me.Supplier
      End Get
      Set(ByVal Value As IBillablePerson)
        Me.Supplier = CType(Value, Supplier)
      End Set
    End Property
    Public Property WitholdingTaxCollection() As WitholdingTaxCollection Implements IWitholdingTaxable.WitholdingTaxCollection
      Get
        Return m_whtcol
      End Get
      Set(ByVal Value As WitholdingTaxCollection)
        m_whtcol = Value
      End Set
    End Property
#End Region

#Region "IPrintableEntity"
    Public Function GetDefaultFormPath() As String Implements IPrintableEntity.GetDefaultFormPath
      Return "PaySelection"
    End Function
    Public Function GetDefaultForm() As String Implements IPrintableEntity.GetDefaultForm
      Return "PaySelection"
    End Function
    Public Function GetDocPrintingEntries() As DocPrintingItemCollection Implements IPrintableEntity.GetDocPrintingEntries
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      'Code
      dpi = New DocPrintingItem
      dpi.Mapping = "Code"
      dpi.Value = Me.Code
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      Return dpiColl
    End Function
#End Region

#Region "Delete"
    Public Overrides ReadOnly Property CanDelete() As Boolean
      Get
        Return Me.Status.Value <= 2 AndAlso Not Me.IsReferenced
      End Get
    End Property
    Private Function GetGoodsRecriptIdList() As ArrayList
      Dim arrList As New ArrayList
      Dim ds As DataSet = SqlHelper.ExecuteDataset(SimpleBusinessEntityBase.ConnectionString, CommandType.StoredProcedure, "GetGoodsRecriptIdList", New SqlParameter("@pays_id", Me.Id))
      For Each row As DataRow In ds.Tables(0).Rows
        arrList.Add(row("stock_id"))
      Next
      'For Each itm As BillAcceptanceItem In Me.ItemCollection
      '  If itm.EntityId = 45 Then
      '    arrList.Add(itm.Id)
      '  End If
      'Next
      Return arrList ' String.Join(",", arrList.ToArray)
    End Function
    Public Overrides Function Delete() As SaveErrorException
      If Not Me.Originated Then
        Return New SaveErrorException("${res:Global.Error.NoIdError}")
      End If
      Dim myMessage As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      Dim format(0) As String
      format(0) = Me.Code
      If Not myMessage.AskQuestionFormatted("${res:Global.ConfirmDeletePaySelection}", format) Then
        Return New SaveErrorException("${res:Global.CencelDelete}")
      End If

      Dim grIdList As String = String.Join(",", Me.GetGoodsRecriptIdList().ToArray)

      Dim trans As SqlTransaction
      Dim conn As New SqlConnection(Me.ConnectionString)
      conn.Open()
      trans = conn.BeginTransaction()
      Try
        Dim returnVal As System.Data.SqlClient.SqlParameter = New SqlParameter
        returnVal.ParameterName = "RETURN_VALUE"
        returnVal.DbType = DbType.Int32
        returnVal.Direction = ParameterDirection.ReturnValue
        returnVal.SourceVersion = DataRowVersion.Current
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "DeletePaySelection", New SqlParameter() {New SqlParameter("@pays_id", Me.Id), returnVal})
        'SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePaymentItemEntityStatus", New SqlParameter("@payment_id", Me.Payment.Id))
        If Not Me.Payment Is Nothing Then
          Me.Payment.UpdateItemEntityStatus(conn, trans)
        End If
        If IsNumeric(returnVal.Value) Then
          Select Case CInt(returnVal.Value)
            Case -1
              trans.Rollback()
              Return New SaveErrorException("${res:Global.PaySelectionIsReferencedCannotBeDeleted}")
            Case Else
          End Select
        ElseIf IsDBNull(returnVal.Value) OrElse Not IsNumeric(returnVal.Value) Then
          trans.Rollback()
          Return New SaveErrorException(returnVal.Value.ToString)
        End If
        Me.DeleteRef(conn, trans)
        trans.Commit()
      Catch ex As SqlException
        trans.Rollback()
        Return New SaveErrorException(ex.Message)
      Catch ex As Exception
        trans.Rollback()
        Return New SaveErrorException(ex.Message)
      Finally
        conn.Close()
      End Try

      Try
        GoodsReceipt.UpdateRemainGoodsReceiptList(conn, grIdList)

        GoodsReceipt.UpdateReferenceGoodsReceiptList(conn, grIdList)
      Catch ex As Exception
        Return New SaveErrorException(ex.Message)
      End Try

      Return New SaveErrorException("1")
    End Function
#End Region

#Region "ICancelable"
    Public ReadOnly Property CanCancel() As Boolean Implements ICancelable.CanCancel
      Get
        Return Me.Status.Value > 1 AndAlso Me.IsCancelable
      End Get
    End Property
    Public Function CancelEntity(ByVal currentUserId As Integer, ByVal theTime As Date) As SaveErrorException Implements ICancelable.CancelEntity
      Me.Status.Value = 0
      Return Me.Save(currentUserId)
    End Function
#End Region

#Region "IHasIBillablePerson"
    Public Property BillablePerson() As IBillablePerson Implements IHasIBillablePerson.BillablePerson
      Get
        Return Me.Supplier
      End Get
      Set(ByVal Value As IBillablePerson)
        If TypeOf Value Is Supplier Then
          Me.Supplier = CType(Value, Supplier)
        End If
      End Set
    End Property
#End Region

#Region "IVatable"
    Public Sub GenVatItems()
      Me.Vat.ItemCollection.Clear()
      Dim i As Integer = 0
      Dim vi As New VatItem
      Dim ptn As String = Entity.GetAutoCodeFormat(vi.EntityId)
      Dim pattern As String = CodeGenerator.GetPattern(ptn, Me)
      pattern = CodeGenerator.GetPattern(pattern)
      Dim lastCode As String = vi.GetLastCode(pattern)
      For Each item As BillAcceptanceItem In Me.ItemCollection
        If item.TaxType.Value <> 0 Then
          i += 1
          Dim vitem As New VatItem
          vitem.LineNumber = i
          Dim newCode As String = CodeGenerator.Generate(ptn, lastCode, Me)
          vitem.Code = newCode
          lastCode = newCode
          vitem.DocDate = Me.DocDate
          vitem.PrintName = Me.Supplier.Name
                    vitem.PrintAddress = Me.Supplier.BillingAddress
                    vitem.TaxId = Me.Supplier.TaxId
                    vitem.BranchId = Me.Supplier.BranchId
          vitem.TaxBase = item.TaxBase - item.DeductTaxBase
          vitem.Amount = item.VatAmt '- item.DeductVatAmt
          'vitem.TaxBase = item.TaxBase - Vat.GetTaxBaseDeductedWithoutThisRefDoc(item.Id, item.EntityId, Me.Id, Me.EntityId)
          vitem.TaxRate = CDec(Configuration.GetConfig("CompanyTaxRate"))
          vitem.CcId = item.CostCenterId
          vitem.Refdoc = item.Id
          vitem.RefdocType = item.EntityId
          vitem.BillAcceptanceItem = item
          Me.Vat.ItemCollection.Add(vitem)
        End If
      Next
    End Sub
    Public Sub GenSingleVatItem()
      Me.Vat.ItemCollection.Clear()
      Dim vitem As New VatItem
      vitem.LineNumber = 1
      Dim ptn As String = Entity.GetAutoCodeFormat(vitem.EntityId)
      Dim pattern As String = CodeGenerator.GetPattern(ptn, Me)
      pattern = CodeGenerator.GetPattern(pattern)
      vitem.Code = CodeGenerator.Generate(ptn, vitem.GetLastCode(pattern), Me.DocDate)
      vitem.DocDate = Me.DocDate
      vitem.PrintName = Me.Supplier.Name
            vitem.PrintAddress = Me.Supplier.BillingAddress
            vitem.TaxId = Me.Supplier.TaxId
            vitem.BranchId = Me.Supplier.BranchId
      vitem.TaxBase = Me.GetMaximumTaxBase
      vitem.Amount = Me.GetVatAmt
      vitem.TaxRate = CDec(Configuration.GetConfig("CompanyTaxRate"))
      Me.Vat.ItemCollection.Add(vitem)
    End Sub

    Public Function GetAfterTax() As Decimal Implements IVatable.GetAfterTax
      Return Me.ItemCollection.GetAfterTax
    End Function

    Public Function GetBeforeTax() As Decimal Implements IVatable.GetBeforeTax
      Return Me.ItemCollection.GetBeforeTax
    End Function
    Public Property TaxBase() As Decimal Implements IVatable.TaxBase
      Get
        Return Me.GetTaxBase
      End Get
      Set(ByVal Value As Decimal)

      End Set
    End Property
    Public ReadOnly Property paysTaxbase As Decimal
      Get
        Return GetTaxBase()
        Dim ptb As Decimal
        For Each item As BillAcceptanceItem In Me.ItemCollection
          If item.TaxType.Value <> 0 AndAlso item.EntityId <> 46 AndAlso item.EntityId <> 199 Then
            'ptb += (item.Amount / item.BilledAmount) * (item.TaxBase - item.DeductTaxBase)
            'ถ้าจ่ายไม่เต็ม ค่า taxbase ต้องเท่ากับค่าจ่ายเท่านั้น
            ptb += item.TaxBaseDeducted
          End If
        Next
        Return ptb
      End Get
    End Property
    Private Function GetTaxBase(Optional ByVal conn As SqlConnection = Nothing, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
      Dim amt As Decimal
      For Each item As BillAcceptanceItem In Me.ItemCollection
        'ถ้าไม่มี vatฟทะ ก็ไม่มี taxbase
        If item.VatAmt <> 0 Then
          If item.TaxType.Value <> 0 AndAlso item.EntityId <> 46 AndAlso item.EntityId <> 199 Then
            amt += item.TaxBaseDeducted
          ElseIf item.EntityId = 46 Then
            amt -= item.TaxBaseDeducted
          End If
        End If

        'If item.TaxType.Value <> 0 AndAlso item.EntityId <> 46 AndAlso item.EntityId <> 199 Then
        '  amt += (item.TaxBase - item.DeductTaxBase(conn, trans))
        'ElseIf item.EntityId = 46 Then
        '  amt -= (item.TaxBase - item.DeductTaxBase(conn, trans))
        'End If
      Next
      Return amt
    End Function
    Public Function GetTaxBaseDeducted(Optional ByVal conn As SqlConnection = Nothing, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
      Dim amt As Decimal
      For Each item As BillAcceptanceItem In Me.ItemCollection
        If item.TaxType.Value <> 0 AndAlso item.EntityId <> 46 AndAlso item.EntityId <> 199 Then
          amt += item.DueTaxBase
        ElseIf item.EntityId = 46 Then
          amt -= item.DueTaxBase
        End If
        'If item.TaxType.Value <> 0 AndAlso item.EntityId <> 46 AndAlso item.EntityId <> 199 Then
        '  amt += item.TaxBaseDeducted
        'ElseIf item.EntityId = 46 Then
        '  amt -= item.TaxBaseDeducted
        'End If
      Next
      Return amt
    End Function
    Public Function GetVatAmt() As Decimal
      Dim amt As Decimal
      For Each item As BillAcceptanceItem In Me.ItemCollection
        'Dim d As Decimal '= item.TaxBaseDeducted
        ''If d = Decimal.MinValue Then
        'd = Vat.GetTaxBaseDeductedWithoutThisRefDoc(item.Id, item.EntityId, Me.Id, Me.EntityId)
        ''d = Vat.GetTaxBaseDeductedWithoutThisRefDoc(item.Id, item.EntityId, Me.Id, Me.EntityId)
        'item.TaxBaseDeducted = d
        'End If
        If item.TaxType.Value <> 0 AndAlso item.EntityId <> 46 AndAlso item.EntityId <> 199 Then
          amt += item.VatAmt
        ElseIf item.EntityId = 46 Then
          amt -= item.VatAmt
        End If
      Next
      Return amt
    End Function
    Private Function GetWitholdingTaxBase() As Decimal
      Dim amt As Decimal
      Dim itemamt As Decimal
      For Each item As BillAcceptanceItem In Me.ItemCollection

        If item.TaxType.Value <> 0 Then
          itemamt = CDec(item.UnpaidAmount / (107 / 100))
        Else
          itemamt = item.UnpaidAmount
        End If
        amt += itemamt
      Next
      Return amt
    End Function
    Public Function GetMaximumTaxBase(Optional ByVal conn As SqlConnection = Nothing, Optional ByVal trans As SqlTransaction = Nothing) As Decimal Implements IVatable.GetMaximumTaxBase
      Return GetTaxBase(conn, trans)
    End Function

    Public ReadOnly Property NoVat() As Boolean Implements IVatable.NoVat
      Get
        Return False
      End Get
    End Property
    Public Property Vat() As Vat Implements IVatable.Vat
      Get
        Return Me.m_vat
      End Get
      Set(ByVal Value As Vat)
        Me.m_vat = Value
      End Set
    End Property
#End Region

#Region "Event"
    Public Event PurchaseCNClick(ByVal e As ButtonColumnEventArgs)
#End Region

    '==============CURRENCY=================================
#Region "IHasCurrency"
    Private m_currency As Currency
    Public Property Currency As Currency Implements IHasCurrency.Currency
      Get
        If m_currency Is Nothing Then
          m_currency = Currency.DefaultCurrency.Clone
        End If
        Return m_currency
      End Get
      Set(ByVal value As Currency)
        m_currency = value
      End Set
    End Property
#End Region
    '==============CURRENCY=================================

#Region "IDocStatus"
    Public ReadOnly Property DocStatus As String Implements IDocStatus.DocStatus
      Get

      End Get
    End Property
#End Region

#Region "INewPrintableEntity"
    Public Function GetDocPrintingColumnsEntries() As DocPrintingItemCollection Implements INewPrintableEntity.GetDocPrintingColumnsEntries
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Code", "System.String"))
      Return dpiColl
    End Function

    Public Function GetDocPrintingDataEntries() As DocPrintingItemCollection Implements INewPrintableEntity.GetDocPrintingDataEntries
      Return Me.GetDocPrintingEntries
    End Function
#End Region

  End Class

  Public Class GoodsReceiptForPaySelection
    Inherits GoodsReceipt
    Implements IVisibleButtonShowColorListAble
    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "GoodsReceiptForPaySelection"
      End Get
    End Property
    Public Overrides ReadOnly Property Columns() As ColumnCollection
      Get
        Return New ColumnCollection(Me.ClassName, 0)
      End Get
    End Property

  End Class
  Public Class APOpeningBalanceForPaySelection
    Inherits APOpeningBalance
    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "APOpeningBalanceForPaySelection"
      End Get
    End Property
  End Class
  Public Class EqMaintenanceForPaySelection
    Inherits EqMaintenance
    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "EqMaintenanceForPaySelection"
      End Get
    End Property
  End Class
  Public Class PurchaseCNForPaySelection
    Inherits PurchaseCN
    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "PurchaseCNForPaySelection"
      End Get
    End Property
  End Class
  Public Class PurchaseRetentionForPaySelection
    Inherits PurchaseRetention
    Implements IVisibleButtonShowColorListAble
    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "PurchaseRetentionForPaySelection"
      End Get
    End Property
    Public Overrides ReadOnly Property Columns() As ColumnCollection
      Get
        Return New ColumnCollection(Me.ClassName, 0)
      End Get
    End Property
  End Class
  Public Class PARetentionForPaySelection
    Inherits PurchaseRetention
    Implements IVisibleButtonShowColorListAble
    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "PARetentionForPaySelection"
      End Get
    End Property
    Public Overrides ReadOnly Property Columns() As ColumnCollection
      Get
        Return New ColumnCollection(Me.ClassName, 0)
      End Get
    End Property
    'Public Overrides ReadOnly Property Prefix() As String
    '  Get
    '    Return "pa"
    '  End Get
    'End Property
    'Public Overrides ReadOnly Property TableName() As String
    '  Get
    '    Return "pa"
    '  End Get
    'End Property
    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.PARetention.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.PARetention"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.PARetention"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.PARetention.ListLabel}"
      End Get
    End Property
  End Class

  Public Class PAForPaySelection
    Inherits PA
    Implements IVisibleButtonShowColorListAble
    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "PAForPaySelection"
      End Get
    End Property
  End Class

  Public Class PaySelectionRefDoc
    Public Property CCID As Integer
    Public Property CCCode As String
    Public Property CCName As String
    Public Property StockId As Integer
    Public Property StockType As Integer
    Public Property StockCode As String
    Public Property StockRetention As Decimal
    Public Property GLCode As String
    Public Property GLNote As String
    Public Property StockNote As String
    Public Property RetentionType As Integer
    Public Property ListOfRefDocVatCode As List(Of RefDocVatCode)
    Public Property IDType As String
    Public Sub New()
      'ListOfRefDocVatCode = New List(Of RefDocVatCode)
    End Sub
    Public Function GetVatCodes() As String
      If ListOfRefDocVatCode Is Nothing Then
        Return ""
      End If
      Dim arr As New ArrayList
      For Each refvat As RefDocVatCode In ListOfRefDocVatCode
        arr.Add(refvat.VatCode)
      Next
      Return String.Join(",", arr.ToArray)
    End Function
  End Class
  Public Class RefDocVatCode
    Public Property StockId As Integer
    Public Property StockType As Integer
    Public Property VatCode As String
  End Class
End Namespace

