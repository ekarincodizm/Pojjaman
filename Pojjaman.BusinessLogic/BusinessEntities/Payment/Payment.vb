Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.TextHelper
Imports System.Reflection
Imports Longkong.Pojjaman.Services
Imports System.Collections.Generic
Imports System.Linq

Namespace Longkong.Pojjaman.BusinessLogic
  Public Class PaymentStatus
    Inherits CodeDescription

#Region "Constructors"
    Public Sub New(ByVal value As Integer)
      MyBase.New(value)
    End Sub
#End Region

#Region "Properties"
    Public Overrides ReadOnly Property CodeName() As String
      Get
        Return "payment_status"
      End Get
    End Property
#End Region

  End Class
  Public Class Payment
    Inherits SimpleBusinessEntityBase
    Implements IPrintableEntity, IHasToCostCenter, IHasFromCostCenter, IHasMainDoc, INewPrintableEntity, IDocStatus

#Region "Members"
    Private payment_docDate As Date
    Private payment_note As String

    Private payment_status As PaymentStatus

    Private payment_refDoc As IPayable
    Private payment_refdoctype As Integer

    Private payment_discountAmount As Decimal  '��ǹŴ�Ѻ
    Private payment_otherRevenue As Decimal  '���������
    Private payment_interest As Decimal  '�͡���¨���
    Private payment_bankcharge As Decimal  '��Ҹ���������Ҥ��
    Private payment_otherExpense As Decimal  '������������
    Private payment_ccId As Integer  'Id �ͧ CostCenter

    Private m_debitCollection As PaymentAccountItemCollection
    Private m_creditCollection As PaymentAccountItemCollection

    Private m_itemCollection As PaymentItemCollection
    Private m_oldListOfPaymentItem As List(Of PaymentItem)

    Public StandAlone As Boolean = False
#End Region

#Region "Constructors"
    Public Sub New()
      MyBase.New()
    End Sub
    Public Sub New(ByVal code As String)
      MyBase.New(code)
    End Sub
    Public Sub New(ByVal id As Integer)
      MyBase.New(id)
    End Sub
    Public Sub New(ByVal refDoc As IPayable)
      Me.New(refDoc.Id, CType(refDoc, IObjectReflectable).EntityId)
      Me.RefDoc = refDoc
      'Me.DocDate = refDoc.Date '����͡��͹ ���С�꡺͡��� Gigasite ��ͧ�� 
    End Sub
    Private Sub New(ByVal refId As Integer, ByVal refType As Integer)
      If refId = 0 Then
        Return
      End If
      Dim ds As DataSet = SqlHelper.ExecuteDataset(Me.ConnectionString _
      , CommandType.StoredProcedure _
      , "GetPayment" _
      , New SqlParameter("@payment_refDoc", refId), New SqlParameter("@payment_refDocType", refType))
      If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count = 1 Then
        Construct(ds.Tables(0).Rows(0), "")
      End If
    End Sub
    Public Sub New(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Me.Construct(ds, aliasPrefix)
    End Sub
    Public Sub New(ByVal dr As System.Data.DataRow, ByVal aliasPrefix As String)
      Me.Construct(dr, aliasPrefix)
    End Sub
    Protected Overloads Overrides Sub Construct()
      MyBase.Construct()
      With Me
        .payment_refDoc = New GenericPayable
        .payment_refDoc.Id = 0
        .payment_refDoc.Date = Date.MinValue
        .payment_refDoc.Code = ""
        .payment_status = New PaymentStatus(-1)
        .OnHold = False
      End With
      m_itemCollection = New PaymentItemCollection(Me)
      m_oldListOfPaymentItem = New List(Of PaymentItem)
      m_debitCollection = New PaymentAccountItemCollection(Me, True)
      m_creditCollection = New PaymentAccountItemCollection(Me, False)
    End Sub
    Protected Overloads Overrides Sub Construct(ByVal dr As System.Data.DataRow, ByVal aliasPrefix As String)
      MyBase.Construct(dr, aliasPrefix)
      With Me

        If Not dr.IsNull(aliasPrefix & "payment_docDate") Then
          .payment_docDate = CDate(dr(aliasPrefix & "payment_docDate"))
        End If

        If Not dr.IsNull(aliasPrefix & "payment_note") Then
          .payment_note = CStr(dr(aliasPrefix & "payment_note"))
        End If

        Dim refDocId As Integer
        Dim refDocCode As String
        Dim refDocDate As Date
        If dr.Table.Columns.Contains(aliasPrefix & "payment_refDocType") AndAlso Not dr.IsNull(aliasPrefix & "payment_refDocType") Then
          payment_refdoctype = CInt(dr(aliasPrefix & "payment_refDocType"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "payment_refdoc") AndAlso Not dr.IsNull(aliasPrefix & "payment_refdoc") Then
          refDocId = CInt(dr(aliasPrefix & "payment_refdoc"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "payment_refdoccode") AndAlso Not dr.IsNull(aliasPrefix & "payment_refdoccode") Then
          refDocCode = CStr(dr(aliasPrefix & "payment_refdoccode"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "payment_refdocdate") AndAlso Not dr.IsNull(aliasPrefix & "payment_refdocdate") Then
          refDocDate = CDate(dr(aliasPrefix & "payment_refdocdate"))
        End If
        .payment_refDoc = New GenericPayable
        .payment_refDoc.Id = refDocId
        .payment_refDoc.Code = refDocCode
        .payment_refDoc.Date = refDocDate

        If dr.Table.Columns.Contains(aliasPrefix & "payment_status") AndAlso dr.Table.Columns.Contains(aliasPrefix & "payment_status") AndAlso Not dr.IsNull(aliasPrefix & "payment_status") Then
          .payment_status = New PaymentStatus(CInt(dr(aliasPrefix & "payment_status")))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "payment_discount") AndAlso Not dr.IsNull(aliasPrefix & "payment_discount") Then
          .payment_discountAmount = CDec(dr(aliasPrefix & "payment_discount"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "payment_otherRevenue") AndAlso Not dr.IsNull(aliasPrefix & "payment_otherRevenue") Then
          .payment_otherRevenue = CDec(dr(aliasPrefix & "payment_otherRevenue"))
        End If
        'If Not dr.IsNull(aliasPrefix & "payment_witholdingTax") Then
        '    .payment_witholdingTax = CDec(dr(aliasPrefix & "payment_witholdingTax"))
        'End If
        If dr.Table.Columns.Contains(aliasPrefix & "payment_interest") AndAlso Not dr.IsNull(aliasPrefix & "payment_interest") Then
          .payment_interest = CDec(dr(aliasPrefix & "payment_interest"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "payment_bankcharge") AndAlso Not dr.IsNull(aliasPrefix & "payment_bankcharge") Then
          .payment_bankcharge = CDec(dr(aliasPrefix & "payment_bankcharge"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "payment_otherExpense") AndAlso Not dr.IsNull(aliasPrefix & "payment_otherExpense") Then
          .payment_otherExpense = CDec(dr(aliasPrefix & "payment_otherExpense"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "payment_cc") AndAlso Not dr.IsNull(aliasPrefix & "payment_cc") Then
          .payment_ccId = CInt(dr(aliasPrefix & "payment_cc"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "payment_onhold") AndAlso Not dr.IsNull(aliasPrefix & "payment_onhold") Then
          OnHold = CBool(dr(aliasPrefix & "payment_onhold"))
        End If
      End With
      m_itemCollection = New PaymentItemCollection(Me)
      m_oldListOfPaymentItem = m_itemCollection.ListOfPaymentItem ' New List(Of PaymentItem)

      m_debitCollection = New PaymentAccountItemCollection(Me, True)
      m_creditCollection = New PaymentAccountItemCollection(Me, False)
    End Sub
    Protected Overloads Overrides Sub Construct(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Dim dr As DataRow = ds.Tables(0).Rows(0)
      Construct(dr, aliasPrefix)
    End Sub
#End Region

#Region "Properties"
    Public Property OnHold As Boolean
    Public ReadOnly Property Maindoc() As ISimpleEntity Implements IHasMainDoc.MainDoc
      Get
        Return CType(payment_refDoc, ISimpleEntity)
      End Get
    End Property
    Public ReadOnly Property Refdoctype() As Integer
      Get
        Return payment_refdoctype
      End Get
    End Property
    Public Property ItemCollection() As PaymentItemCollection
      Get
        Return m_itemCollection
      End Get
      Set(ByVal Value As PaymentItemCollection)
        m_itemCollection = Value
      End Set
    End Property
    Public Property DebitCollection() As PaymentAccountItemCollection
      Get
        Return m_debitCollection
      End Get
      Set(ByVal Value As PaymentAccountItemCollection)
        m_debitCollection = Value
      End Set
    End Property
    Public Property CreditCollection() As PaymentAccountItemCollection
      Get
        Return m_creditCollection
      End Get
      Set(ByVal Value As PaymentAccountItemCollection)
        m_creditCollection = Value
      End Set
    End Property
    Public ReadOnly Property DebitAmount() As Decimal
      Get
        Return Me.DebitCollection.GetAmount
      End Get
    End Property
    Public ReadOnly Property CreditAmount() As Decimal
      Get
        Return Me.CreditCollection.GetAmount
      End Get
    End Property
    Public ReadOnly Property Gross() As Decimal
      Get
        If Me.ItemCollection Is Nothing Then
          Return 0
        End If
        Dim amt As Decimal = 0
        For Each item As PaymentItem In Me.ItemCollection
          amt += item.Amount
        Next
        Return amt
      End Get
    End Property
    Public ReadOnly Property GrossWithNoCreditDebitAmount As Decimal
      Get
        Return Me.Gross - Me.SumCreditAmount + Me.SumDebitAmount
      End Get
    End Property
    'Public Sub UpdateGross()
    '    If Me.ItemTable Is Nothing OrElse Me.ItemTable.Rows.Count = 0 Then
    '        m_gross = 0
    '    Else
    '        Dim amt As Decimal = 0
    '        For Each row As TreeRow In Me.ItemTable.Rows
    '            If Not row.IsNull("paymenti_amt") AndAlso IsNumeric(row("paymenti_amt")) Then
    '                amt += CDec(row("paymenti_amt"))
    '            End If
    '        Next
    '        m_gross = amt
    '    End If
    'End Sub
    Public ReadOnly Property SumCreditAmount() As Decimal
      Get
        Return CreditAmount + Me.OtherExpense + Me.BankCharge + Me.Interest
      End Get
    End Property
    Public ReadOnly Property SumDebitAmount() As Decimal
      Get
        Return DebitAmount + Me.OtherRevenue + Me.DiscountAmount + Me.WitholdingTax
      End Get
    End Property
    Private Function GetCurrencyConversion() As Decimal
      If TypeOf Me.RefDoc Is IHasCurrency Then
        Return CType(Me.RefDoc, IHasCurrency).Currency.Conversion
      End If
      Return 1
    End Function
    Public ReadOnly Property AmountToPay As Decimal
      Get
        Return Me.RefDoc.AmountToPay * GetCurrencyConversion()
      End Get
    End Property
    Public ReadOnly Property Amount() As Decimal
      Get
        Return AmountToPay + Me.SumCreditAmount - Me.SumDebitAmount
      End Get
    End Property
    Public Property CCId() As Integer
      Get
        Return Me.payment_ccId
      End Get
      Set(ByVal Value As Integer)
        payment_ccId = Value
      End Set
    End Property
    Public ReadOnly Property CostCenter() As CostCenter
      Get
        Return CostCenter.GetCCMinDataById(payment_ccId)
        'Return New CostCenter(payment_ccId)
      End Get
    End Property
    Public Property DocDate() As Date
      Get
        Return payment_docDate
      End Get
      Set(ByVal Value As Date)
        payment_docDate = Value
      End Set
    End Property
    Public Property Note() As String
      Get
        Return payment_note
      End Get
      Set(ByVal Value As String)
        payment_note = Value
      End Set
    End Property
    Public Property DiscountAmount() As Decimal
      Get
        Return payment_discountAmount
      End Get
      Set(ByVal Value As Decimal)
        payment_discountAmount = Value
      End Set
    End Property

    Public Property OtherRevenue() As Decimal
      Get
        Return payment_otherRevenue
      End Get
      Set(ByVal Value As Decimal)
        payment_otherRevenue = Value
      End Set
    End Property
    Public ReadOnly Property WitholdingTax() As Decimal
      Get
        If Me.RefDoc Is Nothing Then
          Return 0
        End If
        If Not TypeOf Me.RefDoc Is IWitholdingTaxable Then
          Return 0
        End If
        If CType(Me.RefDoc, IWitholdingTaxable).WitholdingTaxCollection Is Nothing Then
          Return 0
        End If
        If CType(Me.RefDoc, IWitholdingTaxable).WitholdingTaxCollection.IsBeforePay Then
          Return 0
        End If
        Return CType(Me.RefDoc, IWitholdingTaxable).WitholdingTaxCollection.Amount
      End Get
    End Property
    Public Property Interest() As Decimal
      Get
        Return payment_interest
      End Get
      Set(ByVal Value As Decimal)
        payment_interest = Value
      End Set
    End Property
    Public Property BankCharge() As Decimal
      Get
        Return payment_bankcharge
      End Get
      Set(ByVal Value As Decimal)
        payment_bankcharge = Value
      End Set
    End Property
    Public Property OtherExpense() As Decimal
      Get
        Return payment_otherExpense
      End Get
      Set(ByVal Value As Decimal)
        payment_otherExpense = Value
      End Set
    End Property
    Public Overrides Property Status() As CodeDescription
      Get
        Return payment_status
      End Get
      Set(ByVal Value As CodeDescription)
        payment_status = CType(Value, PaymentStatus)
      End Set
    End Property
    Public Property RefDoc() As IPayable
      Get
        If StandAlone AndAlso Not TypeOf payment_refDoc Is ISimpleEntity Then
          payment_refDoc = CType(SimpleBusinessEntityBase.GetEntity(Longkong.Pojjaman.BusinessLogic.Entity.GetFullClassName(payment_refdoctype), payment_refDoc.Id), IPayable)
        End If
        Return payment_refDoc
      End Get
      Set(ByVal Value As IPayable)
        payment_refDoc = Nothing
        payment_refDoc = Value
      End Set
    End Property
    Public Overrides ReadOnly Property ClassName() As String
      Get
        Return "Payment"
      End Get
    End Property
    Public Overrides ReadOnly Property TableName() As String
      Get
        Return "Payment"
      End Get
    End Property
    Public Overrides ReadOnly Property Prefix() As String
      Get
        Return "payment"
      End Get
    End Property

    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.Payment.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.Payment"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.Payment"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.Payment.ListLabel}"
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
      Dim myDatatable As New TreeTable("Payment")
      myDatatable.Columns.Add(New DataColumn("paymenti_linenumber", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("paymenti_entityType", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("Code", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("Button", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("BACode", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("BAButton", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("BAName", GetType(String)))

      Dim dateCol As New DataColumn("DueDate", GetType(Date))
      dateCol.DefaultValue = Date.MinValue
      myDatatable.Columns.Add(dateCol)
      myDatatable.Columns.Add(New DataColumn("RealAmount", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("paymenti_amt", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("paymenti_note", GetType(String)))
      Return myDatatable
    End Function
#End Region

#Region "Methods"
    Private Sub ResetID(ByVal oldid As Integer)

      Me.Id = oldid

    End Sub
    Public Sub ResetDetail()
      For Each pi As PaymentItem In ItemCollection
        If TypeOf pi.Entity Is OutgoingCheck OrElse TypeOf pi.Entity Is OutgoingAval Then
          pi.Entity.Id = pi.oldEntityId
        End If
      Next
    End Sub
    Public Function MultipleCheck() As Boolean
      Dim i As Integer = 0
      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is OutgoingCheck Then
          i += 1
        End If
        If i > 1 Then
          Return True
        End If
      Next
      Return False
    End Function
    Public Overloads Overrides Function Save(ByVal currentUserId As Integer) As SaveErrorException

      Dim trans As SqlTransaction
      Dim conn As New SqlConnection(Me.ConnectionString)
      conn.Open()
      trans = conn.BeginTransaction()
      With Me

        Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)

        For Each item As PaymentItem In Me.ItemCollection
          If item.Entity.Id = 0 Then
            If item.Amount <= 0 Then
              Return New SaveErrorException("${res:Global.Error.AmountMissing}")
            End If
          End If
        Next

        If CBool(Configuration.GetConfig("OneCheckPerPV")) AndAlso MultipleCheck() Then
          Return New SaveErrorException("${res:Global.Error.PaymentHasMultipleCheck}")
        End If

        Dim myGross As Decimal = Me.Gross

        Dim cmp As Integer = Configuration.Compare(myGross, Me.Amount, DigitConfig.Price)
        If cmp > 0 Then
          Return New SaveErrorException("${res:Global.Error.PaymentGrossExceedAmount}", Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price))
        ElseIf cmp < 0 Then
          If Not TypeOf Me.RefDoc Is AdvancePay AndAlso Not TypeOf Me.RefDoc Is PaySelection _
          AndAlso Not TypeOf Me.RefDoc Is AdvanceMoney Then
            If Not Me.Status.Value = 4 Then
              '���˹��
              If Not TypeOf Me.RefDoc Is PurchaseDN Then
                If Not TypeOf Me.RefDoc Is APOpeningBalance Then
                  'If Not msgServ.AskQuestionFormatted("${res:Global.Question.PaymentAmountExceedGross}", New String() {Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price), Configuration.FormatToString(Me.Amount - myGross, DigitConfig.Price)}) Then
                  '    Return New SaveErrorException("${res:Global.Error.SaveCanceled}")
                  'End If
                End If
              End If
            End If
          ElseIf Not OnHold Then
            Return New SaveErrorException("${res:Global.Error.PaymentAmountExceedGross}", New String() {Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price)})
          End If
        End If
        Dim returnVal As System.Data.SqlClient.SqlParameter = New SqlParameter
        returnVal.ParameterName = "RETURN_VALUE"
        returnVal.DbType = DbType.Int32
        returnVal.Direction = ParameterDirection.ReturnValue
        returnVal.SourceVersion = DataRowVersion.Current

        ' ���ҧ ArrayList �ҡ Item �ͧ  SqlParameter ...
        Dim paramArrayList As New ArrayList

        paramArrayList.Add(returnVal)

        If Me.Originated Then
          paramArrayList.Add(New SqlParameter("@payment_id", Me.Id))
        End If


        Dim theTime As Date = Now
        Dim theUser As New User(currentUserId)

        If Me.Status.Value = -1 Then
          Me.Status.Value = 2
        End If

        If Me.AutoGen AndAlso Me.Code.Length > 0 Then
          Me.Code = Me.GetNextCode
        End If
        Me.AutoGen = False
        If IsDBNull(Me.ValidDateOrDBNull(Me.DocDate)) Then
          Me.DocDate = Me.RefDoc.Date
        End If
        If Not TypeOf Me.RefDoc Is PettyCash AndAlso myGross > 0 Then
          paramArrayList.Add(New SqlParameter("@payment_code", Me.Code))
        Else
          paramArrayList.Add(New SqlParameter("@payment_code", DBNull.Value))
        End If
        paramArrayList.Add(New SqlParameter("@payment_docDate", Me.ValidDateOrDBNull(Me.DocDate)))

        If TypeOf Me.RefDoc Is SimpleBusinessEntityBase Then
          paramArrayList.Add(New SqlParameter("@payment_refDocType", CType(Me.RefDoc, SimpleBusinessEntityBase).EntityId))
        End If

        paramArrayList.Add(New SqlParameter("@payment_refDoc", IIf(Me.RefDoc.Id <> 0, Me.RefDoc.Id, DBNull.Value)))
        paramArrayList.Add(New SqlParameter("@payment_refDocDate", IIf(Me.RefDoc.Id <> 0, Me.ValidDateOrDBNull(Me.RefDoc.Date), DBNull.Value)))
        paramArrayList.Add(New SqlParameter("@payment_refDocCode", IIf(Me.RefDoc.Id <> 0, Me.RefDoc.Code, DBNull.Value)))
        paramArrayList.Add(New SqlParameter("@payment_refDocNote", IIf(Me.RefDoc.Id <> 0, Me.RefDoc.Note, DBNull.Value)))
        If Not Me.RefDoc.Recipient Is Nothing AndAlso TypeOf Me.RefDoc.Recipient Is SimpleBusinessEntityBase Then
          Dim payee As SimpleBusinessEntityBase = CType(Me.RefDoc.Recipient, SimpleBusinessEntityBase)
          paramArrayList.Add(New SqlParameter("@payment_refDocEntity", ValidIdOrDBNull(payee)))
          paramArrayList.Add(New SqlParameter("@payment_refDocEntityType", payee.EntityId))
        End If
        Dim due As Date = Me.RefDoc.DueDate
        Dim creditPrd As Integer = 0
        If Not due.Equals(Date.MinValue) Then
          creditPrd = due.Subtract(Me.RefDoc.Date).Days
        End If
        paramArrayList.Add(New SqlParameter("@payment_refDocCreditPeriod", creditPrd))
        paramArrayList.Add(New SqlParameter("@payment_gross", myGross))
        paramArrayList.Add(New SqlParameter("@payment_discount", Me.DiscountAmount))
        paramArrayList.Add(New SqlParameter("@payment_otherRevenue", Me.OtherRevenue))
        paramArrayList.Add(New SqlParameter("@payment_witholdingTax", Me.WitholdingTax))
        paramArrayList.Add(New SqlParameter("@payment_interest", Me.Interest))
        paramArrayList.Add(New SqlParameter("@payment_bankcharge", Me.BankCharge))
        paramArrayList.Add(New SqlParameter("@payment_otherExpense", Me.OtherExpense))
        paramArrayList.Add(New SqlParameter("@payment_amt", Me.Amount))
        paramArrayList.Add(New SqlParameter("@payment_debitamt", Me.DebitAmount))
        paramArrayList.Add(New SqlParameter("@payment_creditamt", Me.CreditAmount))
        paramArrayList.Add(New SqlParameter("@payment_note", Me.Note))
        paramArrayList.Add(New SqlParameter("@payment_status", Me.Status.Value))
        paramArrayList.Add(New SqlParameter("@payment_cc", Me.CCId))
        paramArrayList.Add(New SqlParameter("@payment_onhold", Me.OnHold))

        SetOriginEditCancelStatus(paramArrayList, currentUserId, theTime)

        ' ���ҧ SqlParameter �ҡ ArrayList ...
        Dim sqlparams() As SqlParameter
        sqlparams = CType(paramArrayList.ToArray(GetType(SqlParameter)), SqlParameter())
        Dim oldid As Integer = Me.Id
        Try
          Me.ExecuteSaveSproc(conn, trans, returnVal, sqlparams, theTime, theUser)
          If IsNumeric(returnVal.Value) Then
            Select Case CInt(returnVal.Value)
              Case -1
                Me.ResetID(oldid)
                trans.Rollback()
                Return New SaveErrorException("${res:Global.Error.DuplicatedPaymentCode}", Me.Code)
              Case -2, -5
                Me.ResetID(oldid)
                trans.Rollback()
                Return New SaveErrorException(returnVal.Value.ToString)
              Case Else
            End Select
          ElseIf IsDBNull(returnVal.Value) OrElse Not IsNumeric(returnVal.Value) Then
            Me.ResetID(oldid)
            trans.Rollback()
            Return New SaveErrorException(returnVal.Value.ToString)
          End If
          Dim detailError As SaveErrorException = SaveDetail(Me.Id, conn, trans, currentUserId)
          If Not IsNumeric(detailError.Message) Then
            Me.ResetID(oldid)
            trans.Rollback()
            Return detailError
          Else
            Select Case CInt(detailError.Message)
              Case -1, -5
                Me.ResetID(oldid)
                trans.Rollback()
                Return New SaveErrorException(returnVal.Value.ToString)
              Case -2
                Me.ResetID(oldid)
                trans.Rollback()
                Return New SaveErrorException(returnVal.Value.ToString)
              Case Else
            End Select
          End If
          UpdateItemEntityStatus(conn, trans)
          trans.Commit()
          Return New SaveErrorException(returnVal.Value.ToString)
        Catch ex As SqlException
          Me.ResetID(oldid)
          trans.Rollback()
          Return New SaveErrorException(ex.ToString)
        Catch ex As Exception
          Me.ResetID(oldid)
          trans.Rollback()
          Return New SaveErrorException(ex.ToString)
        Finally
          conn.Close()
        End Try
      End With
    End Function
    Public Function BeforeSave(ByVal currentUserId As Integer) As SaveErrorException

      Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)

      For Each item As PaymentItem In Me.ItemCollection
        item.oldEntityId = item.Entity.Id
        If item.Entity.Id = 0 Then
          If item.Amount <= 0 Then
            Return New SaveErrorException("${res:Global.Error.AmountMissing}")
          End If
        End If
        If (TypeOf item.Entity Is OutgoingCheck OrElse TypeOf item.Entity Is BankTransferOut) _
                       AndAlso item.Entity.CreateDate.HasValue _
                       AndAlso Me.RefDoc.Date.Month <> item.Entity.CreateDate.Value.Month _
                       AndAlso Me.RefDoc.Date.Year <> item.Entity.CreateDate.Value.Year Then
          If Not msgServ.AskQuestion("${res:Global.Error.DifferentMonthCheckAndPayment}" & " " & item.Entity.Code _
                                  & vbCrLf & "${res:Global.Error.DifferentMonthCheckAndPayment2}") Then
            Return New SaveErrorException(item.Entity.Code & " " & "${res:Global.Error.DifferentMonthCheckAndPayment}")
          End If
        End If


        '          ''��ͧ���ѧ�����������ѹ�����������º�ѹ���«�觶�����ѹ���ǡѹ�ҧ case ������ҹ validate ����� ??? ���Ҿѡ�֧����͹�ѹ ��ͧ���ѧ����ͧ��º�ѹ���
        'If Not Date.MinValue.Equals(item.Entity.DueDate) AndAlso CDate(Me.RefDoc.Date.ToShortDateString) < CDate(item.Entity.DueDate.ToShortDateString) Then
        '  Return New SaveErrorException("${res:Global.Error.BeforeCreateDate}")
        'End If
      Next

      If CBool(Configuration.GetConfig("OneCheckPerPV")) AndAlso MultipleCheck() Then
        Return New SaveErrorException("${res:Global.Error.PaymentHasMultipleCheck}")
      End If

      Dim myGross As Decimal = Me.Gross

      Dim cmp As Integer = Configuration.Compare(myGross, Me.Amount, DigitConfig.Price)
      If cmp > 0 Then
        Return New SaveErrorException("${res:Global.Error.PaymentGrossExceedAmount}", Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price))
      ElseIf cmp < 0 Then
        If Not TypeOf Me.RefDoc Is AdvancePay AndAlso Not TypeOf Me.RefDoc Is PaySelection _
        AndAlso Not TypeOf Me.RefDoc Is AdvanceMoney Then
          If Not Me.Status.Value = 4 Then
            '���˹��
            If Not TypeOf Me.RefDoc Is PurchaseDN Then
              If Not TypeOf Me.RefDoc Is APOpeningBalance Then
                'If Not msgServ.AskQuestionFormatted("${res:Global.Question.PaymentAmountExceedGross}", New String() {Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price), Configuration.FormatToString(Me.Amount - myGross, DigitConfig.Price)}) Then
                '    Return New SaveErrorException("${res:Global.Error.SaveCanceled}")
                'End If
              End If
            End If
          End If
        ElseIf Not OnHold Then
          Return New SaveErrorException("${res:Global.Error.PaymentAmountExceedGross}", New String() {Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price)})
        End If
      End If


      Return New SaveErrorException("0")
    End Function

    Public Overloads Overrides Function Save(ByVal currentUserId As Integer, ByVal conn As System.Data.SqlClient.SqlConnection, ByVal trans As System.Data.SqlClient.SqlTransaction) As SaveErrorException
      With Me

        'MessageBox.Show("itemCollection : " & m_itemCollection.Count.ToString)
        'MessageBox.Show("m_oldListOfPaymentItem" & m_oldListOfPaymentItem.Count.ToString)

        'Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)

        'For Each item As PaymentItem In Me.ItemCollection
        '  If item.Entity.Id = 0 Then
        '    If item.Amount <= 0 Then
        '      Return New SaveErrorException("${res:Global.Error.AmountMissing}")
        '    End If
        '  End If
        '  If (TypeOf item.Entity Is OutgoingCheck OrElse TypeOf item.Entity Is BankTransferOut) _
        '                 AndAlso item.Entity.CreateDate.HasValue _
        '                 AndAlso Me.RefDoc.Date.Month <> item.Entity.CreateDate.Value.Month _
        '                 AndAlso Me.RefDoc.Date.Year <> item.Entity.CreateDate.Value.Year Then
        '    If Not msgServ.AskQuestion("${res:Global.Error.DifferentMonthCheckAndPayment}" & " " & item.Entity.Code _
        '                            & vbCrLf & "${res:Global.Error.DifferentMonthCheckAndPayment2}") Then
        '      Return New SaveErrorException(item.Entity.Code & " " & "${res:Global.Error.DifferentMonthCheckAndPayment}")
        '    End If
        '  End If


        '  '          ''��ͧ���ѧ�����������ѹ�����������º�ѹ���«�觶�����ѹ���ǡѹ�ҧ case ������ҹ validate ����� ??? ���Ҿѡ�֧����͹�ѹ ��ͧ���ѧ����ͧ��º�ѹ���
        '  'If Not Date.MinValue.Equals(item.Entity.DueDate) AndAlso CDate(Me.RefDoc.Date.ToShortDateString) < CDate(item.Entity.DueDate.ToShortDateString) Then
        '  '  Return New SaveErrorException("${res:Global.Error.BeforeCreateDate}")
        '  'End If
        'Next

        'If CBool(Configuration.GetConfig("OneCheckPerPV")) AndAlso MultipleCheck() Then
        '  Return New SaveErrorException("${res:Global.Error.PaymentHasMultipleCheck}")
        'End If

        Dim myGross As Decimal = Me.Gross

        'Dim cmp As Integer = Configuration.Compare(myGross, Me.Amount, DigitConfig.Price)
        'If cmp > 0 Then
        '  Return New SaveErrorException("${res:Global.Error.PaymentGrossExceedAmount}", Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price))
        'ElseIf cmp < 0 Then
        '  If Not TypeOf Me.RefDoc Is AdvancePay AndAlso Not TypeOf Me.RefDoc Is PaySelection _
        '  AndAlso Not TypeOf Me.RefDoc Is AdvanceMoney Then
        '    If Not Me.Status.Value = 4 Then
        '      '���˹��
        '      If Not TypeOf Me.RefDoc Is PurchaseDN Then
        '        If Not TypeOf Me.RefDoc Is APOpeningBalance Then
        '          'If Not msgServ.AskQuestionFormatted("${res:Global.Question.PaymentAmountExceedGross}", New String() {Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price), Configuration.FormatToString(Me.Amount - myGross, DigitConfig.Price)}) Then
        '          '    Return New SaveErrorException("${res:Global.Error.SaveCanceled}")
        '          'End If
        '        End If
        '      End If
        '    End If
        '  ElseIf Not OnHold Then
        '    Return New SaveErrorException("${res:Global.Error.PaymentAmountExceedGross}", New String() {Configuration.FormatToString(myGross, DigitConfig.Price), Configuration.FormatToString(Me.Amount, DigitConfig.Price)})
        '  End If
        'End If

        Dim returnVal As System.Data.SqlClient.SqlParameter = New SqlParameter
        returnVal.ParameterName = "RETURN_VALUE"
        returnVal.DbType = DbType.Int32
        returnVal.Direction = ParameterDirection.ReturnValue
        returnVal.SourceVersion = DataRowVersion.Current

        ' ���ҧ ArrayList �ҡ Item �ͧ  SqlParameter ...
        Dim paramArrayList As New ArrayList

        paramArrayList.Add(returnVal)

        If Me.Originated Then
          paramArrayList.Add(New SqlParameter("@payment_id", Me.Id))
        End If


        Dim theTime As Date = Now
        Dim theUser As New User(currentUserId)

        If Me.Status.Value = -1 Then
          Me.Status.Value = 2
        End If

        If Me.AutoGen AndAlso Me.Code.Length > 0 Then
          Me.Code = Me.GetNextCode
        End If
        Me.AutoGen = False
        If IsDBNull(Me.ValidDateOrDBNull(Me.DocDate)) Then
          Me.DocDate = Me.RefDoc.Date
        End If
        If Not TypeOf Me.RefDoc Is PettyCash AndAlso myGross > 0 Then
          paramArrayList.Add(New SqlParameter("@payment_code", Me.Code))
        Else
          paramArrayList.Add(New SqlParameter("@payment_code", DBNull.Value))
        End If
        paramArrayList.Add(New SqlParameter("@payment_docDate", Me.ValidDateOrDBNull(Me.DocDate)))

        If TypeOf Me.RefDoc Is SimpleBusinessEntityBase Then
          paramArrayList.Add(New SqlParameter("@payment_refDocType", CType(Me.RefDoc, SimpleBusinessEntityBase).EntityId))
        End If

        paramArrayList.Add(New SqlParameter("@payment_refDoc", IIf(Me.RefDoc.Id <> 0, Me.RefDoc.Id, DBNull.Value)))
        paramArrayList.Add(New SqlParameter("@payment_refDocDate", IIf(Me.RefDoc.Id <> 0, Me.ValidDateOrDBNull(Me.RefDoc.Date), DBNull.Value)))
        paramArrayList.Add(New SqlParameter("@payment_refDocCode", IIf(Me.RefDoc.Id <> 0, Me.RefDoc.Code, DBNull.Value)))
        paramArrayList.Add(New SqlParameter("@payment_refDocNote", IIf(Me.RefDoc.Id <> 0, Me.RefDoc.Note, DBNull.Value)))
        If Not Me.RefDoc.Recipient Is Nothing AndAlso TypeOf Me.RefDoc.Recipient Is SimpleBusinessEntityBase Then
          Dim payee As SimpleBusinessEntityBase = CType(Me.RefDoc.Recipient, SimpleBusinessEntityBase)
          paramArrayList.Add(New SqlParameter("@payment_refDocEntity", ValidIdOrDBNull(payee)))
          paramArrayList.Add(New SqlParameter("@payment_refDocEntityType", payee.EntityId))
        End If
        Dim due As Date = Me.RefDoc.DueDate
        Dim creditPrd As Integer = 0
        If Not due.Equals(Date.MinValue) Then
          creditPrd = due.Subtract(Me.RefDoc.Date).Days
        End If
        paramArrayList.Add(New SqlParameter("@payment_refDocCreditPeriod", creditPrd))
        paramArrayList.Add(New SqlParameter("@payment_gross", myGross))
        paramArrayList.Add(New SqlParameter("@payment_discount", Me.DiscountAmount))
        paramArrayList.Add(New SqlParameter("@payment_otherRevenue", Me.OtherRevenue))
        paramArrayList.Add(New SqlParameter("@payment_witholdingTax", Me.WitholdingTax))
        paramArrayList.Add(New SqlParameter("@payment_interest", Me.Interest))
        paramArrayList.Add(New SqlParameter("@payment_bankcharge", Me.BankCharge))
        paramArrayList.Add(New SqlParameter("@payment_otherExpense", Me.OtherExpense))
        paramArrayList.Add(New SqlParameter("@payment_amt", Me.Amount))
        paramArrayList.Add(New SqlParameter("@payment_debitamt", Me.DebitAmount))
        paramArrayList.Add(New SqlParameter("@payment_creditamt", Me.CreditAmount))
        paramArrayList.Add(New SqlParameter("@payment_note", Me.Note))
        paramArrayList.Add(New SqlParameter("@payment_status", Me.Status.Value))
        paramArrayList.Add(New SqlParameter("@payment_cc", Me.CCId))
        paramArrayList.Add(New SqlParameter("@payment_onhold", Me.OnHold))

        SetOriginEditCancelStatus(paramArrayList, currentUserId, theTime)

        ' ���ҧ SqlParameter �ҡ ArrayList ...
        Dim sqlparams() As SqlParameter
        sqlparams = CType(paramArrayList.ToArray(GetType(SqlParameter)), SqlParameter())
        Dim oldid As Integer = Me.Id
        Try
          Me.ExecuteSaveSproc(conn, trans, returnVal, sqlparams, theTime, theUser)
          If IsNumeric(returnVal.Value) Then
            Select Case CInt(returnVal.Value)
              Case -1
                Me.ResetID(oldid)
                Return New SaveErrorException("${res:Global.Error.DuplicatedPaymentCode}", Me.Code)
              Case -2, -5
                Me.ResetID(oldid)
                Return New SaveErrorException(returnVal.Value.ToString)
              Case Else
            End Select
          ElseIf IsDBNull(returnVal.Value) OrElse Not IsNumeric(returnVal.Value) Then
            Me.ResetID(oldid)
            Return New SaveErrorException(returnVal.Value.ToString)
          End If

          ' ''============ Update Old Payment item 

          'SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateOldPaymentItemEntityStatus" _
          '                          , New SqlParameter("@payment_id", Me.Id))


          ' ''=====================================

          Dim detailError As SaveErrorException = SaveDetail(Me.Id, conn, trans, currentUserId)
          If Not IsNumeric(detailError.Message) Then
            Me.ResetID(oldid)
            Return detailError
          Else
            Select Case CInt(detailError.Message)
              Case -1, -5
                Me.ResetID(oldid)
                Return New SaveErrorException(returnVal.Value.ToString)
              Case -2
                Me.ResetID(oldid)
                Return New SaveErrorException(returnVal.Value.ToString)
              Case Else
            End Select
          End If

          UpdateItemEntityStatus(conn, trans)

          UpdatePayment_RefCheckPass(conn, trans)

          Return New SaveErrorException(returnVal.Value.ToString)
        Catch ex As SqlException
          Me.ResetID(oldid)
          Return New SaveErrorException(ex.ToString)
        Catch ex As Exception
          Me.ResetID(oldid)
          Return New SaveErrorException(ex.ToString)
        End Try
      End With
    End Function

    Public Overrides Function GetNextCode() As String
      Dim autoCodeFormat As String = Me.Code     'Entity.GetAutoCodeFormat(Me.EntityId)
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

    Private Sub UpdatePayment_RefCheckPass(ByVal conn As SqlConnection, ByVal trans As SqlTransaction)
      '@payment_refdoc numeric(18,0)
      '@payment_refdoctype numeric(18,0)
      SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePayment_RefCheckPass",
                                New SqlParameter("@payment_refdoc", RefDoc.Id), New SqlParameter("@payment_refdoctype", CType(Me.RefDoc, SimpleBusinessEntityBase).EntityId))
    End Sub

    Private Function GetOldOutGoingCheckIdList() As String
      Dim arrId As New ArrayList
      For Each item As PaymentItem In Me.m_oldListOfPaymentItem
        If item.EntityType.Value = 22 Then
          arrId.Add(item.Entity.Id)
        End If
      Next
      If arrId.Count > 0 Then
        Return String.Join(",", arrId.ToArray)
      End If
      Return ""
    End Function
    Private Function GetOldAdvancePayIdList() As String
      Dim arrId As New ArrayList
      For Each item As PaymentItem In Me.m_oldListOfPaymentItem
        If item.EntityType.Value = 59 Then
          arrId.Add(item.Entity.Id)
        End If
      Next
      If arrId.Count > 0 Then
        Return String.Join(",", arrId.ToArray)
      End If
      Return ""
    End Function
    Private Function GetOldPettyCashIdList() As String
      Dim arrId As New ArrayList
      For Each item As PaymentItem In Me.m_oldListOfPaymentItem
        If item.EntityType.Value = 65 Then
          arrId.Add(item.Entity.Id)
        End If
      Next
      If arrId.Count > 0 Then
        Return String.Join(",", arrId.ToArray)
      End If
      Return ""
    End Function
    Private Function GetOldAdvanceMoneyList() As String
      Dim arrId As New ArrayList
      For Each item As PaymentItem In Me.m_oldListOfPaymentItem
        If item.EntityType.Value = 174 Then
          arrId.Add(item.Entity.Id)
        End If
      Next
      If arrId.Count > 0 Then
        Return String.Join(",", arrId.ToArray)
      End If
      Return ""
    End Function
    Public Sub UpdateItemEntityStatus(ByVal conn As SqlConnection, ByVal trans As SqlTransaction)
      If Not Me.Originated Then
        Return
      End If
      Dim oldCheck As String = Me.GetOldOutGoingCheckIdList
      Dim oldAdvancePay As String = Me.GetOldAdvancePayIdList
      Dim oldPettyCash As String = Me.GetOldPettyCashIdList
      Dim oldAdvanceMoney As String = Me.GetOldAdvanceMoneyList
      SqlHelper.ExecuteNonQuery(conn,
                                trans,
                                CommandType.StoredProcedure,
                                "UpdatePaymentItemEntityStatus",
                                New SqlParameter("@payment_id", Me.Id),
                                New SqlParameter("@OldOutGoingCheckIdList", oldCheck),
                                New SqlParameter("@OldAdvancePayIdList", oldAdvancePay),
                                New SqlParameter("@OldPettyCashIdList", oldPettyCash),
                                New SqlParameter("@OldAdvanceMoneyIdList", oldAdvanceMoney)
                                )
    End Sub
    Private Function GetBAFromSproc(ByVal sproc As String, ByVal paymentRefDoc As Integer) As DataTable
      Try
        Dim ds As DataSet = SqlHelper.ExecuteDataset( _
         Me.ConnectionString _
         , CommandType.StoredProcedure _
         , sproc _
         , New SqlParameter("@payment_refDoc", paymentRefDoc) _
         )
        Return ds.Tables(0)
      Catch ex As Exception
      End Try
    End Function
    Private Function GetAmountFromSproc(ByVal sproc As String, ByVal toDate As Date, ByVal view As Integer) As Decimal
      Try
        Dim ds As DataSet = SqlHelper.ExecuteDataset( _
         Me.ConnectionString _
         , CommandType.StoredProcedure _
         , sproc _
         , New SqlParameter("@boq_id", Me.Id) _
         , New SqlParameter("@toDate", toDate) _
         , New SqlParameter("@view", view) _
         )
        Dim tableIndex As Integer = 0
        'Select Case m_WBSReportType
        '    Case WBSReportType.GoodsReceipt, WBSReportType.MatWithdraw
        '        tableIndex = 0
        '    Case WBSReportType.PR
        '        tableIndex = 1
        '    Case WBSReportType.PO
        '        tableIndex = 2
        'End Select
        If ds.Tables.Count > tableIndex Then
          If ds.Tables(tableIndex).Rows.Count > 0 Then
            If ds.Tables(tableIndex).Rows(0).IsNull(0) Then
              Return 0
            End If
            Return CDec(ds.Tables(tableIndex).Rows(0)(0))
          End If
        End If
      Catch ex As Exception
      End Try
    End Function
    Private Function SaveDetail(ByVal parentID As Integer, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal currentUserId As Integer) As SaveErrorException
      Try
        Dim da As New SqlDataAdapter("Select * from paymentitem where paymenti_payment=" & Me.Id, conn)
        Dim cmdBuilder As New SqlCommandBuilder(da)

        Dim ds As New DataSet

        da.SelectCommand.Transaction = trans

        '��ͧ�����ͨҡ da.SelectCommand.Transaction = trans
        cmdBuilder.GetDeleteCommand.Transaction = trans
        cmdBuilder.GetInsertCommand.Transaction = trans
        cmdBuilder.GetUpdateCommand.Transaction = trans

        da.Fill(ds, "paymentitem")

        Dim i As Integer = 0
        With ds.Tables("paymentitem")
          For Each row As DataRow In .Rows
            row.Delete()
          Next
          Dim lastCheckCode As String = ""
          For Each item As PaymentItem In Me.ItemCollection
            If Not item.Entity Is Nothing Then
              i += 1
              Dim dr As DataRow = .NewRow
              If TypeOf item.Entity Is OutgoingCheck AndAlso Not OnHold Then
                Dim check As OutgoingCheck = CType(item.Entity, OutgoingCheck)
                If Not check.Originated Then
                  check.IssueDate = Me.DocDate
                  check.AutoGen = True
                  If Not Me.RefDoc Is Nothing Then
                    If Not Me.RefDoc.Recipient Is Nothing Then
                      If TypeOf Me.RefDoc.Recipient Is Supplier Then
                        check.Supplier = CType(Me.RefDoc.Recipient, Supplier)
                        If TypeOf Me.RefDoc Is PettyCash Then
                          check.Recipient = ""
                        Else
                          check.Recipient = Me.RefDoc.Recipient.Name
                        End If
                      Else
                        check.Recipient = Me.RefDoc.Recipient.Name
                      End If
                    End If
                  End If
                  If lastCheckCode.Length <> 0 Then
                    check.Code = CodeGenerator.Generate(Entity.GetAutoCodeFormat(check.EntityId), lastCheckCode, check)
                  Else
                    check.Code = check.GetNextCode(conn, trans)
                  End If
                  check.AutoGen = False
                  check.DocStatus = New OutgoingCheckDocStatus(-1)

                  Dim checkSaveError As SaveErrorException = check.Save(currentUserId, conn, trans)
                  If Not IsNumeric(checkSaveError.Message) Then
                    Return checkSaveError
                  Else
                    Select Case CInt(checkSaveError.Message)
                      Case -1, -5
                        Return checkSaveError
                      Case -2
                        Return checkSaveError
                      Case Else
                    End Select
                  End If
                  lastCheckCode = check.Code
                End If
                If Not check.Originated AndAlso Not OnHold Then
                  Return New SaveErrorException("Check Saving Error")
                End If
              End If
              ' save Aval
              If TypeOf item.Entity Is OutgoingAval Then
                Dim Aval As OutgoingAval = CType(item.Entity, OutgoingAval)
                If Not Aval.Originated Then
                  Aval.IssueDate = Me.DocDate
                  Aval.AutoGen = True
                  If Not Me.RefDoc Is Nothing Then
                    If Not Me.RefDoc.Recipient Is Nothing Then
                      If TypeOf Me.RefDoc.Recipient Is Supplier Then
                        Aval.Supplier = CType(Me.RefDoc.Recipient, Supplier)
                        If TypeOf Me.RefDoc Is PettyCash Then
                          Aval.Recipient = ""
                        Else
                          Aval.Recipient = Me.RefDoc.Recipient.Name
                        End If
                      Else
                        Aval.Recipient = Me.RefDoc.Recipient.Name
                      End If
                    End If
                  End If
                  If lastCheckCode.Length <> 0 Then
                    Aval.Code = CodeGenerator.Generate(Entity.GetAutoCodeFormat(Aval.EntityId), lastCheckCode, Aval)
                  Else
                    Aval.Code = Aval.GetNextCode
                  End If
                  Aval.AutoGen = False
                  Aval.DocStatus = New OutgoingCheckDocStatus(-1)

                  Dim checkSaveError As SaveErrorException = Aval.Save(currentUserId, conn, trans)
                  If Not IsNumeric(checkSaveError.Message) Then
                    Return checkSaveError
                  Else
                    Select Case CInt(checkSaveError.Message)
                      Case -1, -5
                        Return checkSaveError
                      Case -2
                        Return checkSaveError
                      Case Else
                    End Select
                  End If
                  lastCheckCode = Aval.Code
                End If
                If Not Aval.Originated Then
                  Return New SaveErrorException("Aval Saving Error")
                End If
              End If
              dr("paymenti_payment") = Me.Id
              dr("paymenti_linenumber") = i
              dr("paymenti_duedate") = Me.ValidDateOrDBNull(item.DueDate)
              If OnHold Then
                dr("paymenti_entity") = 0
              Else
                dr("paymenti_entity") = item.Entity.Id
              End If
              dr("paymenti_entitycode") = item.Entity.Code
              If TypeOf item.Entity Is IHasBankAccount Then
                dr("paymenti_bankacct") = Me.ValidIdOrDBNull(CType(item.Entity, IHasBankAccount).BankAccount)
              End If
              dr("paymenti_entityType") = item.EntityType.Value
              dr("paymenti_amt") = item.Amount
              dr("paymenti_refamt") = item.RealAmount
              dr("paymenti_note") = item.Note
              dr("paymenti_status") = Me.Status.Value
              .Rows.Add(dr)
            End If
          Next
        End With
        Dim dt As DataTable = ds.Tables("paymentitem")
        ' First process deletes.
        da.Update(dt.Select(Nothing, Nothing, DataViewRowState.Deleted))
        ' Next process updates.
        da.Update(dt.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent))
        ' Finally process inserts.
        da.Update(dt.Select(Nothing, Nothing, DataViewRowState.Added))

        'Update PCSuplier ��Ѻ价�� Check.
        If TypeOf Me.RefDoc Is PettyCashClaim Then
          For Each item As PaymentItem In Me.ItemCollection
            If item.EntityType.Value = 22 Then
              SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateCheckFromPettyCashClaim" _
              , New SqlParameter("@check_id", item.Entity.Id))
            End If
          Next
        End If

        Dim daDrCr As New SqlDataAdapter("Select * from paymentaccount where paymenta_payment=" & Me.Id, conn)
        cmdBuilder = New SqlCommandBuilder(daDrCr)

        daDrCr.SelectCommand.Transaction = trans

        '��ͧ�����ͨҡ da.SelectCommand.Transaction = trans
        cmdBuilder.GetDeleteCommand.Transaction = trans
        cmdBuilder.GetInsertCommand.Transaction = trans
        cmdBuilder.GetUpdateCommand.Transaction = trans

        daDrCr.Fill(ds, "paymentaccount")

        With ds.Tables("paymentaccount")
          For Each row As DataRow In .Rows
            row.Delete()
          Next
          For Each item As PaymentAccountItem In Me.DebitCollection
            Dim dr As DataRow = .NewRow
            dr("paymenta_payment") = Me.Id
            dr("paymenta_acct") = Me.ValidIdOrDBNull(item.Account)
            dr("paymenta_isdebit") = item.IsDebit
            dr("paymenta_amt") = item.Amount
            .Rows.Add(dr)
          Next
          For Each item As PaymentAccountItem In Me.CreditCollection
            Dim dr As DataRow = .NewRow
            dr("paymenta_payment") = Me.Id
            dr("paymenta_acct") = Me.ValidIdOrDBNull(item.Account)
            dr("paymenta_isdebit") = item.IsDebit
            dr("paymenta_amt") = item.Amount
            .Rows.Add(dr)
          Next
        End With
        dt = ds.Tables("paymentaccount")
        ' First process deletes.
        daDrCr.Update(dt.Select(Nothing, Nothing, DataViewRowState.Deleted))
        ' Next process updates.
        daDrCr.Update(dt.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent))
        ' Finally process inserts.
        daDrCr.Update(dt.Select(Nothing, Nothing, DataViewRowState.Added))
      Catch ex As Exception
        Return New SaveErrorException(ex.ToString)
      Finally

      End Try
      Return New SaveErrorException("0")
    End Function
    Private Function SaveAccountItemDetail(ByVal parentID As Integer, ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As Integer

    End Function
#End Region

#Region "Shared AccTable"
    Public Shared Function GetDebitCreditSchemaTable() As TreeTable
      Dim myDatatable As New TreeTable("OtherPayment")
      myDatatable.Columns.Add(New DataColumn("Linenumber", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("Code", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("Button", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("Name", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("paymenta_amt", GetType(String)))
      Return myDatatable
    End Function
#End Region

#Region "GetJournalEntries"
    Public Function GetJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection

      '�͡����
      Dim ji As JournalEntryItem
      If Me.Interest > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.1"
        ji.Amount = Me.Interest
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("PayInterest")
        jiColl.Add(ji)
      End If

      '��¨�������
      If Me.OtherExpense > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.2"
        ji.Amount = Me.OtherExpense
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("OtherExpense")
        jiColl.Add(ji)
      End If

      If Me.CreditCollection.Count > 0 Then
        For Each item As PaymentAccountItem In Me.CreditCollection
          ji = New JournalEntryItem
          ji.Mapping = "Through"
          ji.Amount = item.Amount
          ji.Account = item.Account
          ji.IsDebit = True
          ji.Note = StringParserService.Parse("${res:Global.OtherCredit}")
          If Me.CostCenter.Originated Then
            ji.CostCenter = Me.CostCenter
          Else
            ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
          End If
          ji.EntityItem = Me.Id
          ji.EntityItemType = Entity.GetIdFromClassName("OtherCredit")
          jiColl.Add(ji)
        Next
      End If

      '��Ҹ���������Ҥ��
      If Me.BankCharge > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.3"
        ji.Amount = Me.BankCharge
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("PayCharge")
        jiColl.Add(ji)
      End If

      '���������
      If Me.OtherRevenue > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.8"
        ji.Amount = Me.OtherRevenue
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("OtherIncome")
        jiColl.Add(ji)
      End If

      If Me.DebitCollection.Count > 0 Then
        For Each item As PaymentAccountItem In Me.DebitCollection
          ji = New JournalEntryItem
          ji.Mapping = "Through"
          ji.Amount = item.Amount
          ji.Account = item.Account
          ji.IsDebit = False
          ji.Note = StringParserService.Parse("${res:Global.OtherDebit}")
          If Me.CostCenter.Originated Then
            ji.CostCenter = Me.CostCenter
          Else
            ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
          End If
          ji.EntityItem = Me.Id
          ji.EntityItemType = Entity.GetIdFromClassName("OtherDebit")
          jiColl.Add(ji)
        Next
      End If

      '��ǹŴ�Ѻ
      If Me.DiscountAmount > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.9"
        ji.Amount = Me.DiscountAmount
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("DiscountReceive")
        jiColl.Add(ji)
      End If

      jiColl.AddRange(GetPettyCashJournalEntries)
      jiColl.AddRange(GetPettyCashDetailJournalEntries)
      jiColl.AddRange(GetCashCheckJournalEntries)
      jiColl.AddRange(GetCashCheckDetailJournalEntries)
      jiColl.AddRange(GetBankTransferJournalEntries)
      jiColl.AddRange(GetBankTransferDetailJournalEntries)
      jiColl.AddRange(GetAdvanceMoneyJournalEntries)
      jiColl.AddRange(GetAdvanceMoneyDetailJournalEntries)
      Return jiColl
    End Function
    Private Function GetCashCheckJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection
      Dim sumCheck As Decimal = 0
      Dim sumAval As Decimal = 0
      Dim sumCash As Decimal = 0
      Dim sumAvp As Decimal = 0
      Dim ji As JournalEntryItem
      Dim pm15note As String = ""
      Dim pm15Dnote As String = ""
      Dim pm110note As String = ""

      For Each item As PaymentItem In Me.ItemCollection
        Select Case item.EntityType.Value
          Case 22       'Check
            sumCheck += item.Amount
            If pm15note = "" Then
              Dim theNote As String = ""
              theNote &= "���� " & Me.RefDoc.Recipient.Code
              theNote &= CType(item.Entity, OutgoingCheck).CqCode
              theNote &= "/" & CType(item.Entity, OutgoingCheck).Bankacct.BankBranch.Bank.Code
              pm15note = theNote
              pm15Dnote = theNote
            Else
              Dim theNote As String = ""
              theNote &= CType(item.Entity, OutgoingCheck).CqCode
              theNote &= "/" & CType(item.Entity, OutgoingCheck).Bankacct.BankBranch.Bank.Code
              pm15note = pm15note & " " & theNote
              pm15Dnote = theNote
            End If
            'If item.Amount > 0 Then
            '  ji = New JournalEntryItem
            '  ji.Mapping = "PM1.5D"
            '  ji.Amount = item.Amount
            '  If Me.CostCenter.Originated Then
            '    ji.CostCenter = Me.CostCenter
            '  Else
            '    ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            '  End If
            '  ji.EntityItem = item.Entity.Id
            '  ji.EntityItemType = 22
            '  ji.Note = pm15Dnote
            '  jiColl.Add(ji)
            'End If
          Case 336       'Aval
            sumAval += item.Amount
            If pm15note = "" Then
              pm15note = "���� " & Me.RefDoc.Recipient.Code & " ���µ�������� " & CType(item.Entity, OutgoingAval).CqCode & "/" & CType(item.Entity, OutgoingAval).Loan.Name
              pm15Dnote = "���� " & Me.RefDoc.Recipient.Code & " ������ " & CType(item.Entity, OutgoingCheck).CqCode & "/" & CType(item.Entity, OutgoingCheck).Bankacct.BankBranch.Bank.Code
            Else
              pm15note = pm15note & " " & CType(item.Entity, OutgoingAval).CqCode & "/" & CType(item.Entity, OutgoingAval).Loan.Name
              pm15Dnote = CType(item.Entity, OutgoingAval).CqCode & "/" & CType(item.Entity, OutgoingAval).Loan.Name
            End If
            'If item.Amount > 0 Then
            '  ji = New JournalEntryItem
            '  ji.Mapping = "PM1.5D"
            '  ji.Amount = item.Amount
            '  If Me.CostCenter.Originated Then
            '    ji.CostCenter = Me.CostCenter
            '  Else
            '    ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            '  End If
            '  ji.EntityItem = item.Entity.Id
            '  ji.EntityItemType = 336
            '  ji.Note = pm15Dnote
            '  jiColl.Add(ji)
            'End If
          Case 0          'Cash
            sumCash += item.Amount
          Case 59         'AdvancePayment
            sumAvp += item.Amount
            If pm110note = "" Then
              pm110note = "�Ѵ�Ѵ�Ӣͧ " & Me.RefDoc.Recipient.Code & "(" & CType(item.Entity, AdvancePayItem).AdvancePay.Code & ")"

            Else
              pm110note = pm110note & "," & Me.RefDoc.Recipient.Code & "(" & CType(item.Entity, AdvancePayItem).AdvancePay.Code & ")"
            End If
        End Select
      Next
      If sumCash > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.4"
        ji.Amount = sumCash
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        jiColl.Add(ji)
      End If
      If sumAval > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.5"
        ji.Amount = sumAval
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.Note = pm15note
        jiColl.Add(ji)
      End If
      If sumCheck > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.5"
        ji.Amount = sumCheck
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.Note = pm15note
        jiColl.Add(ji)
      End If
      If sumAvp > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.10"
        ji.Amount = sumAvp
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.Note = pm110note
        jiColl.Add(ji)
      End If
      Return jiColl
    End Function
    Private Function GetCashCheckDetailJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection
      Dim sumCheck As Decimal = 0
      Dim sumAval As Decimal = 0
      Dim sumCash As Decimal = 0
      Dim sumAvp As Decimal = 0
      Dim ji As JournalEntryItem

      For Each item As PaymentItem In Me.ItemCollection
        Select Case item.EntityType.Value
          Case 22         'Check
            ji = New JournalEntryItem
            ji.Mapping = "PM1.5D"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, OutgoingCheck).CqCode _
            & " " & CType(item.Entity, OutgoingCheck).DueDate.ToShortDateString _
            & " " & CType(item.Entity, OutgoingCheck).Bankacct.Code _
            & "/" & Me.RefDoc.Recipient.Name
            ji.EntityItem = item.Entity.Id
            ji.EntityItemType = 22
            jiColl.Add(ji)

            ji = New JournalEntryItem
            ji.Mapping = "PM1.5W"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, OutgoingCheck).CqCode _
            & " " & CType(item.Entity, OutgoingCheck).DueDate.ToShortDateString _
            & " " & CType(item.Entity, OutgoingCheck).Bankacct.Code _
            & "/" & Me.RefDoc.Recipient.Name
            jiColl.Add(ji)
          Case 336         'Aval
            ji = New JournalEntryItem
            ji.Mapping = "PM1.5D"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, OutgoingAval).CqCode _
            & " " & CType(item.Entity, OutgoingAval).DueDate.ToShortDateString _
            & " " & CType(item.Entity, OutgoingAval).Loan.Code _
            & "/" & Me.RefDoc.Recipient.Name
            ji.EntityItem = item.Entity.Id
            ji.EntityItemType = 336
            jiColl.Add(ji)

            ji = New JournalEntryItem
            ji.Mapping = "PM1.5W"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, OutgoingAval).CqCode _
            & " " & CType(item.Entity, OutgoingAval).DueDate.ToShortDateString _
            & " " & CType(item.Entity, OutgoingAval).Loan.Code _
            & "/" & Me.RefDoc.Recipient.Name
            jiColl.Add(ji)

          Case 0          'Cash
            ji = New JournalEntryItem
            ji.Mapping = "PM1.4D"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            jiColl.Add(ji)

            ji = New JournalEntryItem
            ji.Mapping = "PM1.4W"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            jiColl.Add(ji)

          Case 59         'AdvancePayment
            ji = New JournalEntryItem
            ji.Mapping = "PM1.10D"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, AdvancePayItem).AdvancePay.Code & "/" & Me.RefDoc.Recipient.Name
            jiColl.Add(ji)

            ji = New JournalEntryItem
            ji.Mapping = "PM1.10W"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, AdvancePayItem).AdvancePay.Code & "/" & Me.RefDoc.Recipient.Name
            jiColl.Add(ji)

        End Select
      Next
      Return jiColl
    End Function
    Private Function GetAdvanceMoneyJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection

      Dim ji As New JournalEntryItem
      Dim pm111note As String = ""
      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is AdvanceMoney Then
          Dim advm As AdvanceMoney = CType(item.Entity, AdvanceMoney)
          If Not advm Is Nothing Then

            If pm111note = "" Then
              pm111note = advm.Name & "(" & advm.Code & ")"
            Else
              pm111note = pm111note & "," & advm.Name & "(" & advm.Code & ")"
            End If

            If Not advm.Account Is Nothing AndAlso advm.Account.Originated Then
              Dim matched As Boolean = False
              For Each addedJi As JournalEntryItem In jiColl
                If addedJi.Account.Id = advm.Account.Id Then
                  '�� Account ���ǡѹ
                  addedJi.Amount += item.Amount
                  addedJi.Note = pm111note
                  matched = True
                End If
              Next
              If Not matched Then
                ji = New JournalEntryItem
                ji.Account = advm.Account
                ji.Mapping = "PM1.11"
                ji.Amount = item.Amount
                If Me.CostCenter.Originated Then
                  ji.CostCenter = Me.CostCenter
                Else
                  ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                End If
                ji.Note = pm111note
                jiColl.Add(ji)
              End If
            Else
              '����� Account --- �������ҧ�������Ẻ Mix
              Dim matched As Boolean = False
              For Each addedJi As JournalEntryItem In jiColl
                If addedJi.Account Is Nothing OrElse Not addedJi.Account.Originated Then
                  If ji.Mapping = "PM1.11" Then
                    addedJi.Amount += item.Amount
                    addedJi.Note = pm111note
                    matched = True
                  End If
                End If
              Next
              If Not matched Then
                ji = New JournalEntryItem
                ji.Mapping = "PM1.11"
                ji.Amount = item.Amount
                If Me.CostCenter.Originated Then
                  ji.CostCenter = Me.CostCenter
                Else
                  ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                End If
                ji.Note = pm111note
                jiColl.Add(ji)
              End If
            End If
          End If
        End If
      Next
      Return jiColl
    End Function
    Private Function GetAdvanceMoneyDetailJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection
      Dim ji As New JournalEntryItem
      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is AdvanceMoney Then
          Dim advm As AdvanceMoney = CType(item.Entity, AdvanceMoney)
          If Not advm Is Nothing Then
            If Not advm.Account Is Nothing AndAlso advm.Account.Originated Then
              ji = New JournalEntryItem
              ji.Account = advm.Account
              ji.Mapping = "PM1.11D"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = advm.Name & "(" & advm.Code & ")"
              ji.EntityItem = item.Entity.Id
              ji.EntityItemType = item.EntityType.Value
              jiColl.Add(ji)

              ji = New JournalEntryItem
              ji.Account = advm.Account
              ji.Mapping = "PM1.11W"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = advm.Name & "(" & advm.Code & ")"
              jiColl.Add(ji)

            Else
              '����� Account --- �������ҧ�������Ẻ Mix
              ji = New JournalEntryItem
              ji.Mapping = "PM1.11D"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = advm.Name & "(" & advm.Code & ")"
              ji.EntityItem = item.Entity.Id
              ji.EntityItemType = item.EntityType.Value
              jiColl.Add(ji)

              ji = New JournalEntryItem
              ji.Mapping = "PM1.11W"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = advm.Name & "(" & advm.Code & ")"
              jiColl.Add(ji)

            End If
          End If
        End If
      Next
      Return jiColl
    End Function
    Private Function GetPettyCashJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection

      Dim ji As New JournalEntryItem
      Dim pm17note As String = ""

      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is PettyCash Then
          Dim ptc As PettyCash = CType(item.Entity, PettyCash)
          If Not ptc Is Nothing Then
            If pm17note = "" Then
              pm17note = ptc.Name & "(" & ptc.Code & ")"
            Else
              pm17note = pm17note & "," & ptc.Name & "(" & ptc.Code & ")"
            End If
            If Not ptc.Account Is Nothing AndAlso ptc.Account.Originated Then
              Dim matched As Boolean = False
              For Each addedJi As JournalEntryItem In jiColl
                If addedJi.Account.Id = ptc.Account.Id Then
                  '�� Account ���ǡѹ
                  addedJi.Amount += item.Amount
                  addedJi.Note = pm17note
                  matched = True
                End If
              Next
              If Not matched Then
                ji = New JournalEntryItem
                ji.Account = ptc.Account
                ji.Mapping = "PM1.7"
                ji.Amount = item.Amount
                If ptc.ToCC IsNot Nothing Then
                  ji.CostCenter = ptc.ToCC
                Else
                  If Me.CostCenter.Originated Then
                    ji.CostCenter = Me.CostCenter
                  Else
                    ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                  End If
                End If
                ji.Note = pm17note
                jiColl.Add(ji)
              End If
            Else
              '����� Account --- �������ҧ�������Ẻ Mix
              Dim matched As Boolean = False
              For Each addedJi As JournalEntryItem In jiColl
                If addedJi.Account Is Nothing OrElse Not addedJi.Account.Originated Then
                  If ji.Mapping = "PM1.7" Then
                    addedJi.Amount += item.Amount
                    addedJi.Note = pm17note
                    matched = True
                  End If
                End If
              Next
              If Not matched Then
                ji = New JournalEntryItem
                ji.Mapping = "PM1.7"
                ji.Amount = item.Amount
                If ptc.ToCC IsNot Nothing Then
                  ji.CostCenter = ptc.ToCC
                Else
                  If Me.CostCenter.Originated Then
                    ji.CostCenter = Me.CostCenter
                  Else
                    ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                  End If
                End If
                ji.Note = pm17note
                jiColl.Add(ji)
              End If
            End If
          End If
        End If
      Next
      Return jiColl
    End Function
    Private Function GetPettyCashDetailJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection

      Dim ji As New JournalEntryItem
      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is PettyCash Then
          Dim ptc As PettyCash = CType(item.Entity, PettyCash)
          If Not ptc Is Nothing Then
            If Not ptc.Account Is Nothing AndAlso ptc.Account.Originated Then
              ji = New JournalEntryItem
              ji.Account = ptc.Account
              ji.Mapping = "PM1.7D"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = ptc.Name & "(" & ptc.Code & ")"
              jiColl.Add(ji)

              ji = New JournalEntryItem
              ji.Account = ptc.Account
              ji.Mapping = "PM1.7W"
              ji.Amount = item.Amount
              If ptc.ToCC IsNot Nothing Then
                ji.CostCenter = ptc.ToCC
              Else
                If Me.CostCenter.Originated Then
                  ji.CostCenter = Me.CostCenter
                Else
                  ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                End If
              End If
              ji.Note = ptc.Name & "(" & ptc.Code & ")"
              ji.EntityItem = item.Entity.Id
              ji.EntityItemType = item.EntityType.Value
              jiColl.Add(ji)

            Else
              '����� Account --- �������ҧ�������Ẻ Mix
              ji = New JournalEntryItem
              ji.Mapping = "PM1.7D"
              ji.Amount = item.Amount
              If ptc.ToCC IsNot Nothing Then
                ji.CostCenter = ptc.ToCC
              Else
                If Me.CostCenter.Originated Then
                  ji.CostCenter = Me.CostCenter
                Else
                  ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                End If
              End If
              ji.Note = ptc.Name & "(" & ptc.Code & ")"
              ji.EntityItem = item.Entity.Id
              ji.EntityItemType = item.EntityType.Value
              jiColl.Add(ji)

              ji = New JournalEntryItem
              ji.Mapping = "PM1.7W"
              ji.Amount = item.Amount
              If ptc.ToCC IsNot Nothing Then
                ji.CostCenter = ptc.ToCC
              Else
                If Me.CostCenter.Originated Then
                  ji.CostCenter = Me.CostCenter
                Else
                  ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                End If
              End If
              ji.Note = ptc.Name & "(" & ptc.Code & ")"
              jiColl.Add(ji)

            End If
          End If
        End If
      Next
      Return jiColl
    End Function
    Private Function GetBankTransferJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection

      Dim ji As New JournalEntryItem
      Dim pm16note As String = ""

      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is BankTransferOut Then
          Dim bto As BankTransferOut = CType(item.Entity, BankTransferOut)
          If pm16note = "" Then
            pm16note = "�͹��� " & Me.RefDoc.Recipient.Code & " �ҡ " & bto.BankAccount.BankBranch.Bank.Code
          Else
            pm16note = pm16note & "," & bto.BankAccount.BankBranch.Bank.Code
          End If
          If Not bto Is Nothing AndAlso Not bto.BankAccount Is Nothing _
          AndAlso Not bto.BankAccount.Account Is Nothing AndAlso bto.BankAccount.Account.Originated Then
            Dim matched As Boolean = False
            For Each addedJi As JournalEntryItem In jiColl
              If addedJi.Account.Id = bto.BankAccount.Account.Id And addedJi.Mapping = "PM1.6" Then
                '�� Account ���ǡѹ
                addedJi.Amount += item.Amount
                addedJi.Note = pm16note
                matched = True
              End If
            Next
            If Not matched Then
              ji = New JournalEntryItem
              ji.Account = bto.BankAccount.Account
              ji.Mapping = "PM1.6"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = pm16note
              jiColl.Add(ji)
            End If
          End If
        End If
      Next
      Return jiColl
    End Function
    Private Function GetBankTransferDetailJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection
      Dim ji As New JournalEntryItem
      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is BankTransferOut Then
          Dim bto As BankTransferOut = CType(item.Entity, BankTransferOut)
          If Not bto Is Nothing AndAlso Not bto.BankAccount Is Nothing _
          AndAlso Not bto.BankAccount.Account Is Nothing AndAlso bto.BankAccount.Account.Originated Then
            ji = New JournalEntryItem
            ji.Account = bto.BankAccount.Account
            ji.Mapping = "PM1.6D"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = bto.DocDate.ToShortDateString & " " & bto.BankAccount.BankBranch.Bank.Name & "/" & Me.RefDoc.Recipient.Name
            jiColl.Add(ji)

            ji = New JournalEntryItem
            ji.Account = bto.BankAccount.Account
            ji.Mapping = "PM1.6W"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = bto.DocDate.ToShortDateString & " " & bto.BankAccount.BankBranch.Bank.Name & "/" & Me.RefDoc.Recipient.Name
            ji.EntityItem = bto.BankAccount.Id
            ji.EntityItemType = bto.BankAccount.EntityId
            jiColl.Add(ji)
          End If
        End If
      Next
      Return jiColl
    End Function
#End Region

#Region "GetNewJournalEntries"
    Public Function GetNewJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection

      '�͡����
      Dim ji As JournalEntryItem
      If Me.Interest > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.1"
        ji.Amount = Me.Interest
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("PayInterest")
        ji.table = Me.TableName
        ji.AtomNote = "�͡���¨���"
        jiColl.Add(ji)
      End If

      '��¨�������
      If Me.OtherExpense > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.2"
        ji.Amount = Me.OtherExpense
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("OtherExpense")
        ji.table = Me.TableName
        ji.AtomNote = "��¨�������"

        jiColl.Add(ji)
      End If

      If Me.CreditCollection.Count > 0 Then
        For Each item As PaymentAccountItem In Me.CreditCollection
          ji = New JournalEntryItem
          ji.Mapping = "Through"
          ji.Amount = item.Amount
          ji.Account = item.Account
          ji.IsDebit = True
          ji.Note = StringParserService.Parse("${res:Global.OtherCredit}")
          If Me.CostCenter.Originated Then
            ji.CostCenter = Me.CostCenter
          Else
            ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
          End If
          ji.EntityItem = Me.Id
          ji.EntityItemType = Entity.GetIdFromClassName("OtherCredit")
          ji.table = "paymentaccount"
          ji.AtomNote = "����� GA"
          jiColl.Add(ji)
        Next
      End If

      '��Ҹ���������Ҥ��
      If Me.BankCharge > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.3"
        ji.Amount = Me.BankCharge
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("PayCharge")
        ji.table = Me.TableName
        ji.AtomNote = "��Ҹ���������Ҥ��"
        jiColl.Add(ji)
      End If

      '���������
      If Me.OtherRevenue > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.8"
        ji.Amount = Me.OtherRevenue
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("OtherIncome")
        ji.table = Me.TableName
        ji.AtomNote = "���������"
        jiColl.Add(ji)
      End If

      If Me.DebitCollection.Count > 0 Then
        For Each item As PaymentAccountItem In Me.DebitCollection
          ji = New JournalEntryItem
          ji.Mapping = "Through"
          ji.Amount = item.Amount
          ji.Account = item.Account
          ji.IsDebit = False
          ji.Note = StringParserService.Parse("${res:Global.OtherDebit}")
          If Me.CostCenter.Originated Then
            ji.CostCenter = Me.CostCenter
          Else
            ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
          End If
          ji.EntityItem = Me.Id
          ji.EntityItemType = Entity.GetIdFromClassName("OtherDebit")
          ji.table = "paymentaccount"
          ji.AtomNote = "����� GA"
          jiColl.Add(ji)
        Next
      End If

      '��ǹŴ�Ѻ
      If Me.DiscountAmount > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "PM1.9"
        ji.Amount = Me.DiscountAmount
        If Me.CostCenter.Originated Then
          ji.CostCenter = Me.CostCenter
        Else
          ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        ji.EntityItem = Me.Id
        ji.EntityItemType = Entity.GetIdFromClassName("DiscountReceive")
        ji.table = Me.TableName
        ji.AtomNote = "��ǹŴ�Ѻ"
        jiColl.Add(ji)
      End If

      jiColl.AddRange(GetNewPettyCashDetailJournalEntries)
      jiColl.AddRange(GetNewCashCheckDetailJournalEntries)
      jiColl.AddRange(GetNewBankTransferDetailJournalEntries)
      jiColl.AddRange(GetNewAdvanceMoneyDetailJournalEntries)
      Return jiColl
    End Function

    Private Function GetNewCashCheckDetailJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection
      Dim sumCheck As Decimal = 0
      Dim sumAval As Decimal = 0
      Dim sumCash As Decimal = 0
      Dim sumAvp As Decimal = 0
      Dim ji As JournalEntryItem

      For Each item As PaymentItem In Me.ItemCollection
        Select Case item.EntityType.Value
          Case 22         'Check
            ji = New JournalEntryItem
            ji.Mapping = "PM1.5"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, OutgoingCheck).CqCode _
            & " " & CType(item.Entity, OutgoingCheck).DueDate.ToShortDateString _
            & " " & CType(item.Entity, OutgoingCheck).Bankacct.Code _
            & "/" & Me.RefDoc.Recipient.Name
            ji.EntityItem = item.Entity.Id
            ji.EntityItemType = 22
            ji.table = Me.TableName & "item"
            ji.CustomRefstr = Me.Id.ToString
            ji.CustomRefType = Me.ClassName
            ji.AtomNote = "���´�����"
            jiColl.Add(ji)

          Case 336         'Aval
            ji = New JournalEntryItem
            ji.Mapping = "PM1.5"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, OutgoingAval).CqCode _
            & " " & CType(item.Entity, OutgoingAval).DueDate.ToShortDateString _
            & " " & CType(item.Entity, OutgoingAval).Loan.Code _
            & "/" & Me.RefDoc.Recipient.Name
            ji.EntityItem = item.Entity.Id
            ji.EntityItemType = 336
            ji.table = Me.TableName & "item"
            ji.CustomRefstr = Me.Id.ToString
            ji.CustomRefType = Me.ClassName
            ji.AtomNote = "���´��� Aval"
            jiColl.Add(ji)


          Case 0          'Cash
            ji = New JournalEntryItem
            ji.Mapping = "PM1.4"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.EntityItemType = 421
            ji.table = Me.TableName & "item"
            ji.CustomRefstr = Me.Id.ToString
            ji.CustomRefType = Me.ClassName
            ji.AtomNote = "���´��� �Թʴ"
            jiColl.Add(ji)



          Case 59         'AdvancePayment
            ji = New JournalEntryItem
            ji.Mapping = "PM1.10"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = CType(item.Entity, AdvancePayItem).AdvancePay.Code & "/" & Me.RefDoc.Recipient.Name
            ji.EntityItem = item.Entity.Id
            ji.EntityItemType = 59
            ji.table = Me.TableName & "item"
            ji.CustomRefstr = Me.Id.ToString
            ji.CustomRefType = Me.ClassName
            ji.AtomNote = "���´��� �Ѵ��"
            jiColl.Add(ji)



        End Select
      Next
      Return jiColl
    End Function

    Private Function GetNewAdvanceMoneyDetailJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection
      Dim ji As New JournalEntryItem
      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is AdvanceMoney Then
          Dim advm As AdvanceMoney = CType(item.Entity, AdvanceMoney)
          If Not advm Is Nothing Then
            If Not advm.Account Is Nothing AndAlso advm.Account.Originated Then
              ji = New JournalEntryItem
              ji.Account = advm.Account
              ji.Mapping = "PM1.11"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = advm.Name & "(" & advm.Code & ")"
              ji.EntityItem = item.Entity.Id
              ji.EntityItemType = item.EntityType.Value
              ji.table = Me.TableName & "item"
              ji.CustomRefstr = Me.Id.ToString
              ji.CustomRefType = Me.ClassName
              ji.AtomNote = "���´��� �Թ���ͧ����"
              jiColl.Add(ji)



            Else
              '����� Account --- �������ҧ�������Ẻ Mix
              ji = New JournalEntryItem
              ji.Mapping = "PM1.11"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = advm.Name & "(" & advm.Code & ")"
              ji.EntityItem = item.Entity.Id
              ji.EntityItemType = item.EntityType.Value
              ji.table = Me.TableName & "item"
              ji.CustomRefstr = Me.Id.ToString
              ji.CustomRefType = Me.ClassName
              ji.AtomNote = "���´��� �Թ���ͧ����"
              jiColl.Add(ji)

            End If
          End If
        End If
      Next
      Return jiColl
    End Function

    Private Function GetNewPettyCashDetailJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection

      Dim ji As New JournalEntryItem
      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is PettyCash Then
          Dim ptc As PettyCash = CType(item.Entity, PettyCash)
          If Not ptc Is Nothing Then
            If Not ptc.Account Is Nothing AndAlso ptc.Account.Originated Then
              ji = New JournalEntryItem
              ji.Account = ptc.Account
              ji.Mapping = "PM1.7"
              ji.Amount = item.Amount
              If Me.CostCenter.Originated Then
                ji.CostCenter = Me.CostCenter
              Else
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
              End If
              ji.Note = ptc.Name & "(" & ptc.Code & ")"
              ji.EntityItem = item.Entity.Id
              ji.EntityItemType = item.EntityType.Value
              ji.table = Me.TableName & "item"
              ji.CustomRefstr = Me.Id.ToString
              ji.CustomRefType = Me.ClassName
              ji.AtomNote = "���´��� �Թʴ����"
              jiColl.Add(ji)


            Else
              '����� Account --- �������ҧ�������Ẻ Mix
              ji = New JournalEntryItem
              ji.Mapping = "PM1.7"
              ji.Amount = item.Amount
              If ptc.ToCC IsNot Nothing Then
                ji.CostCenter = ptc.ToCC
              Else
                If Me.CostCenter.Originated Then
                  ji.CostCenter = Me.CostCenter
                Else
                  ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                End If
              End If
              ji.Note = ptc.Name & "(" & ptc.Code & ")"
              ji.EntityItem = item.Entity.Id
              ji.EntityItemType = item.EntityType.Value
              ji.table = Me.TableName & "item"
              ji.CustomRefstr = Me.Id.ToString
              ji.CustomRefType = Me.ClassName
              ji.AtomNote = "���´��� �Թʴ����"
              jiColl.Add(ji)


            End If
          End If
        End If
      Next
      Return jiColl
    End Function

    Private Function GetNewBankTransferDetailJournalEntries() As JournalEntryItemCollection
      Dim jiColl As New JournalEntryItemCollection
      Dim ji As New JournalEntryItem
      For Each item As PaymentItem In Me.ItemCollection
        If TypeOf item.Entity Is BankTransferOut Then
          Dim bto As BankTransferOut = CType(item.Entity, BankTransferOut)
          If Not bto Is Nothing AndAlso Not bto.BankAccount Is Nothing _
          AndAlso Not bto.BankAccount.Account Is Nothing AndAlso bto.BankAccount.Account.Originated Then
            ji = New JournalEntryItem
            ji.Account = bto.BankAccount.Account
            ji.Mapping = "PM1.6"
            ji.Amount = item.Amount
            If Me.CostCenter.Originated Then
              ji.CostCenter = Me.CostCenter
            Else
              ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            End If
            ji.Note = bto.DocDate.ToShortDateString & " " & bto.BankAccount.BankBranch.Bank.Name & "/" & Me.RefDoc.Recipient.Name
            ji.EntityItem = bto.BankAccount.Id
            ji.EntityItemType = Entity.GetIdFromClassName("BankAccount")
            ji.table = Me.TableName & "item"
            ji.CustomRefstr = Me.Id.ToString
            ji.CustomRefType = Me.ClassName
            ji.AtomNote = "���´��� �Թ�͹"
            jiColl.Add(ji)

          End If
        End If
      Next
      Return jiColl
    End Function
#End Region

#Region "IPrintableEntity"
    Public Function GetDefaultFormPath() As String Implements IPrintableEntity.GetDefaultFormPath
      Return "C:\Documents and Settings\Administrator\Desktop\Forms\Documents\PV.dfm"
    End Function
    Public Function GetDefaultForm() As String Implements IPrintableEntity.GetDefaultForm
      Return "PV"
    End Function
    Private Enum TableType
      PaymentItem
      PaymentItemBTO
      PaymentItemAll
      PaymentItemAllGeneric
    End Enum

    Private Enum PaymentItemType
      itIsCheck
      itIsAval
      itIsBto
      itIsCash
      itIsPC
      itIsADVP
      itIsADVM
    End Enum
    Public Function GetDocPrintingEntries() As DocPrintingItemCollection Implements IPrintableEntity.GetDocPrintingEntries
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      'Payment_id
      dpi = New DocPrintingItem
      dpi.Mapping = "payment_id"
      dpi.Value = Me.Id
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      dpi = New DocPrintingItem
      dpi.Mapping = "payment_refdoc"
      dpi.Value = Me.RefDoc.Id.ToString
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      dpi = New DocPrintingItem
      dpi.Mapping = "payment_refdoctype"
      If TypeOf Me.RefDoc Is ISimpleEntity Then
        dpi.Value = CType(Me.RefDoc, ISimpleEntity).EntityId
      End If
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'Code
      dpi = New DocPrintingItem
      dpi.Mapping = "Code"
      dpi.Value = Me.Code
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'DocDate
      dpi = New DocPrintingItem
      dpi.Mapping = "DocDate"
      dpi.Value = Me.DocDate.ToShortDateString
      dpi.DataType = "System.DateTime"
      dpiColl.Add(dpi)

      'RefCode
      dpi = New DocPrintingItem
      dpi.Mapping = "RefCode"
      dpi.Value = Me.RefDoc.Code
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'RefDocDate
      dpi = New DocPrintingItem
      dpi.Mapping = "RefDocDate"
      dpi.Value = Me.RefDoc.Date.ToShortDateString
      dpi.DataType = "System.DateTime"
      dpiColl.Add(dpi)

      'Gross
      dpi = New DocPrintingItem
      dpi.Mapping = "Gross"
      dpi.Value = Configuration.FormatToString(Me.Gross, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      If TypeOf (Me.RefDoc) Is AdvanceMoney Then
        Dim advm As AdvanceMoney = CType(Me.RefDoc, AdvanceMoney)

        'Employee Code
        dpi = New DocPrintingItem
        dpi.Mapping = "RefEmployeeCode"
        dpi.Value = advm.Employee.Code
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'Employee Name
        dpi = New DocPrintingItem
        dpi.Mapping = "RefEmployeeName"
        dpi.Value = advm.Employee.Name
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'Employee 
        dpi = New DocPrintingItem
        dpi.Mapping = "RefEmployeeInfo"
        dpi.Value = advm.Employee.Code & ":" & advm.Employee.Name
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'Note
        dpi = New DocPrintingItem
        dpi.Mapping = "RefNote"
        dpi.Value = advm.Note
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)
      End If


      'RefDueDate
      If Not Me.RefDoc.DueDate.Equals(Date.MinValue) Then
        dpi = New DocPrintingItem
        dpi.Mapping = "RefDueDate"
        dpi.Value = Me.RefDoc.DueDate.ToShortDateString
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)
      End If

      'Note
      dpi = New DocPrintingItem
      dpi.Mapping = "Note"
      dpi.Value = Me.Note
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      If Not Me.CostCenter Is Nothing Then
        'CostCenterInfo
        dpi = New DocPrintingItem
        dpi.Mapping = "CostCenterInfo"
        dpi.Value = Me.CostCenter.Code & ":" & Me.CostCenter.Name
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'CostCenterCode
        dpi = New DocPrintingItem
        dpi.Mapping = "CostCenterCode"
        dpi.Value = Me.CostCenter.Code
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'CostCenterName
        dpi = New DocPrintingItem
        dpi.Mapping = "CostCenterName"
        dpi.Value = Me.CostCenter.Name
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)
      End If

      If Not Me.RefDoc.Recipient Is Nothing Then
        'SupplierInfo
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierInfo"
        dpi.Value = Me.RefDoc.Recipient.Code & ":" & Me.RefDoc.Recipient.Name
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'SupplierCode
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierCode"
        dpi.Value = Me.RefDoc.Recipient.Code
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'SupplierName
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierName"
        If TypeOf Me.RefDoc Is AdvanceMoney Then
          Dim myAdvm As AdvanceMoney = CType(Me.RefDoc, AdvanceMoney)
          If myAdvm.IsForEmployee Then
            dpi.Value = myAdvm.Employee.Name
          Else
            dpi.Value = myAdvm.Costcenter.Name
          End If
        Else
          dpi.Value = Me.RefDoc.Recipient.Name
        End If
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'SupplierAddress
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierAddress"
        dpi.Value = Me.RefDoc.Recipient.BillingAddress
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'SupplierCurrentAddress
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierCurrentAddress"
        dpi.Value = Me.RefDoc.Recipient.Address
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'SupplierPhone
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierPhone"
        dpi.Value = Me.RefDoc.Recipient.Phone
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'SupplierFax
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierFax"
        dpi.Value = Me.RefDoc.Recipient.Fax
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'SupplierTaxId
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierTaxId"
        dpi.Value = Me.RefDoc.Recipient.TaxId
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'SupplierIdNo
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierIdNo"
        dpi.Value = Me.RefDoc.Recipient.IdNo
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)
      End If

      Dim TotalCash As Decimal = 0
      Dim TotalPettyCash As Decimal = 0
      Dim TotalAdvancePay As Decimal = 0
      Dim TotalCheck As Decimal = 0
      Dim TotalTransferOut As Decimal = 0
      Dim TotalAdvanceMoney As Decimal = 0
            Dim CheckCode As String = ""

            Dim DiscountAmount As Decimal = 0
            Dim OtherRevenue As Decimal = 0

      Dim tmpCode As String = ""
      Dim tmpName As String = ""
      Dim tmpEmpCode As String = ""
      Dim tmpEmpName As String = ""

      Dim tt As TableType

      For tableType As Integer = 0 To 3
        'tableType 0 = ੾����
        'tableType 1 = ੾���͹
        'tableType 2 = ������

        Dim tableName As String
        Select Case tableType
          Case 0
            tableName = "PaymentItem"
            tt = Payment.TableType.PaymentItem
          Case 1
            tableName = "PaymentItemBTO"
            tt = Payment.TableType.PaymentItemBTO
          Case 2
            tableName = "PaymentItemAll"
            tt = Payment.TableType.PaymentItemAll
          Case 3
            tableName = "PaymentItemAllGeneric"
            tt = Payment.TableType.PaymentItemAllGeneric
        End Select

        Dim n As Integer = 0
        For Each item As PaymentItem In Me.ItemCollection
          Dim entityType As Integer = item.EntityType.Value

          'PaymentItem.LineNumber
          dpi = New DocPrintingItem
          dpi.Mapping = "paymenti_payment"
          dpi.Value = Me.Id
          dpi.DataType = "System.Int32"
          dpi.Row = n + 1
          dpi.Table = tableName
          dpiColl.Add(dpi)

          'PaymentItem.LineNumber
          dpi = New DocPrintingItem
          dpi.Mapping = tableName & ".LineNumber"
          dpi.Value = n + 1
          dpi.DataType = "System.Int32"
          dpi.Row = n + 1
          dpi.Table = tableName
          dpiColl.Add(dpi)

          Dim pit As PaymentItemType

          Dim itIsCheck As Boolean = (TypeOf item.Entity Is OutgoingCheck)
          Dim itIsAval As Boolean = (TypeOf item.Entity Is OutgoingAval)
          Dim itIsBto As Boolean = (TypeOf item.Entity Is BankTransferOut)
          Dim itIsCash As Boolean = (TypeOf item.Entity Is CashItem)
          Dim itIsPC As Boolean = (TypeOf item.Entity Is PettyCash)
          Dim itIsADVP As Boolean = (TypeOf item.Entity Is AdvancePay)
          Dim itIsADVM As Boolean = (TypeOf item.Entity Is AdvanceMoney)

          Dim PaymentTypeName As String = ""
          If itIsCheck Then
            PaymentTypeName = "itIsCheck"
            pit = PaymentItemType.itIsCheck
          ElseIf itIsAval Then
            PaymentTypeName = "itIsCheck"
            pit = PaymentItemType.itIsCheck
          ElseIf itIsBto Then
            PaymentTypeName = "itIsBto"
            pit = PaymentItemType.itIsBto
          ElseIf itIsCash Then
            PaymentTypeName = "itIsCash"
            pit = PaymentItemType.itIsCash
          ElseIf itIsPC Then
            PaymentTypeName = "itIsPC"
            pit = PaymentItemType.itIsPC
          ElseIf itIsADVP Then
            PaymentTypeName = "itIsADVP"
            pit = PaymentItemType.itIsADVP

          ElseIf itIsADVM Then
            PaymentTypeName = "itIsADVM"
            pit = PaymentItemType.itIsADVM
          End If
          'PaymentItem..PaymentTypeName
          dpi = New DocPrintingItem
          dpi.Mapping = tableName & "." & PaymentTypeName
          dpi.Value = "P"
          dpi.DataType = "System.String"
          dpi.Row = n + 1
          dpi.Table = tableName
          dpiColl.Add(dpi)

          If tt <> Payment.TableType.PaymentItemAllGeneric Then


            If itIsCheck Then
              If CheckCode Is Nothing OrElse CheckCode.Length = 0 Then
                CheckCode = CType(item.Entity, OutgoingCheck).CqCode
              End If

              'PaymentItem.CheckPayAmount
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".CheckPayAmount"
              dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)


            End If


            If tableType = 2 Then 'paymentItemAll
              'PaymentItemAll.DueDate
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".DueDate"
              dpi.Value = item.DueDate.ToShortDateString
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              'PaymentItemAll.LimitAmount
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".LimitAmount"
              dpi.Value = Configuration.FormatToString(item.Entity.Amount, DigitConfig.Price)
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              'PaymentItemAll.payAmount
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".PayAmount"
              dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
              dpi.DataType = "System.Decimal"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

            End If




            If (itIsCash) Then
              'PaymentItem.DueDate
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".CashDueDate"
              dpi.Value = item.DueDate.ToShortDateString
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              'PaymentItem.Amount
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".CashAmount"
              dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
              dpi.DataType = "System.Decimal"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)
            End If

            If (itIsPC) Then
              If Not item.Entity Is Nothing Then
                Dim iPC As New PettyCash(item.Entity.Id)
                tmpCode = iPC.Code
                tmpName = iPC.Name
                tmpEmpCode = iPC.Employee.Code
                tmpEmpName = iPC.Employee.Name
              End If

              'PaymentItem.PettyCashPayAmount
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".PettyCashPayAmount"
              dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              'PaymentItem.PettyCashAmount
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".PettyCashAmount"
              dpi.Value = Configuration.FormatToString(item.Entity.Amount, DigitConfig.Price)
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              'PaymentItem.PettyCashPayDate
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".PettyCashPayDate"
              dpi.Value = item.DueDate.ToShortDateString
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

            End If
            If (itIsADVM) Then
              Dim iADVM As New AdvanceMoney(item.Entity.Id)
              tmpCode = iADVM.Code
              tmpName = iADVM.Name
              tmpEmpCode = iADVM.Employee.Code
              tmpEmpName = iADVM.Employee.Name
            End If
            If (itIsPC Or itIsADVM) And (tableType = 0 Or tableType = 2) Then
              'PaymentItem.SetMoneyCode
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".SetMoneyCode"
              dpi.Value = tmpCode
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              'PaymentItem.SetMoneyName
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".SetMoneyName"
              dpi.Value = tmpName
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              'PaymentItem.EmployeeCode
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".EmployeeCode"
              dpi.Value = tmpEmpCode
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              'PaymentItem.EmployeeName
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".EmployeeName"
              dpi.Value = tmpEmpName
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)
            End If

            If (itIsCheck And tableType = 0) _
             Or (itIsBto And tableType = 1) _
             Or (itIsCheck Or itIsBto) And tableType = 2 _
             Then

              If itIsCheck Then
                'PaymentItem.DueDate
                dpi = New DocPrintingItem
                dpi.Mapping = tableName & ".DueDate"
                If CType(item.Entity, OutgoingCheck).DueDate.Equals(Date.MinValue) Then
                  dpi.Value = ""
                Else
                  dpi.Value = CType(item.Entity, OutgoingCheck).DueDate.ToShortDateString
                End If
                dpi.DataType = "System.DateTime"
                dpi.Row = n + 1
                dpi.Table = tableName
                dpiColl.Add(dpi)
              Else
                'PaymentItem.DueDate
                dpi = New DocPrintingItem
                dpi.Mapping = tableName & ".DueDate"
                If item.DueDate.Equals(Date.MinValue) Then
                  dpi.Value = ""
                Else
                  dpi.Value = item.DueDate.ToShortDateString
                End If
                dpi.DataType = "System.DateTime"
                dpi.Row = n + 1
                dpi.Table = tableName
                dpiColl.Add(dpi)
              End If

              'PaymentItem.CqCode
              dpi = New DocPrintingItem
              dpi.Mapping = tableName & ".CqCode"
              If itIsCheck Then
                dpi.Value = CType(item.Entity, OutgoingCheck).CqCode
              Else
                dpi.Value = item.Entity.Code
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = tableName
              dpiColl.Add(dpi)

              If TypeOf item.Entity Is IHasAmount Then
                'PaymentItem.Amount
                dpi = New DocPrintingItem
                dpi.Mapping = tableName & ".Amount"
                dpi.Value = Configuration.FormatToString(CType(item.Entity, IHasAmount).Amount, DigitConfig.Price)
                dpi.DataType = "System.Decimal"
                dpi.Row = n + 1
                dpi.Table = tableName
                dpiColl.Add(dpi)
              End If

              If TypeOf item.Entity Is IHasBankAccount Then
                Dim hasBankAccount As IHasBankAccount = CType(item.Entity, IHasBankAccount)
                Dim bankAcct As BankAccount = hasBankAccount.BankAccount
                Dim bankBranch As BankBranch
                Dim bank As Bank
                If Not bankAcct Is Nothing Then
                  'PaymentItem.BankAccount
                  dpi = New DocPrintingItem
                  dpi.Mapping = tableName & ".BankAccount"
                  dpi.Value = bankAcct.Name
                  dpi.DataType = "System.String"
                  dpi.Row = n + 1
                  dpi.Table = tableName
                  dpiColl.Add(dpi)

                  'PaymentItem.BankAccountCode
                  dpi = New DocPrintingItem
                  dpi.Mapping = tableName & ".BankAccountCode"
                  dpi.Value = bankAcct.BankCode
                  dpi.DataType = "System.String"
                  dpi.Row = n + 1
                  dpi.Table = tableName
                  dpiColl.Add(dpi)

                  bankBranch = bankAcct.BankBranch
                  If Not bankBranch Is Nothing Then
                    'PaymentItem.BankBranch
                    dpi = New DocPrintingItem
                    dpi.Mapping = tableName & ".BankBranch"
                    dpi.Value = bankBranch.Name
                    dpi.DataType = "System.String"
                    dpi.Row = n + 1
                    dpi.Table = tableName
                    dpiColl.Add(dpi)

                    bank = bankBranch.Bank
                    If Not bank Is Nothing Then
                      'PaymentItem.Bank
                      dpi = New DocPrintingItem
                      dpi.Mapping = tableName & ".Bank"
                      dpi.Value = bank.Name
                      dpi.DataType = "System.String"
                      dpi.Row = n + 1
                      dpi.Table = tableName
                      dpiColl.Add(dpi)
                    End If                'Not bank Is Nothing Then
                  End If               'Not bankBranch Is Nothing Then
                End If             'Not bankAcct Is Nothing Then
              End If           'TypeOf item.Entity Is IHasBankAccount Then
            End If
          Else ' �� payment.TableType.PaymentItemAllGeneric

            If TypeOf item.Entity Is IHasBankAccount Then
              Dim hasBankAccount As IHasBankAccount = CType(item.Entity, IHasBankAccount)
              Dim bankAcct As BankAccount = hasBankAccount.BankAccount
              Dim bankBranch As BankBranch
              Dim bank As Bank
              If Not bankAcct Is Nothing Then
                item.printItemBAName = bankAcct.Name
                item.printItemBACode = bankAcct.BankCode
              End If
            End If

            Select Case pit
              Case PaymentItemType.itIsCash
                item.printItemCode = ""
                item.printItemRealAmount = 0
              Case PaymentItemType.itIsCheck
                item.printItemCode = CType(item.Entity, OutgoingCheck).CqCode
                item.printItemRealAmount = 0
              Case PaymentItemType.itIsBto
                item.printItemCode = ""
                item.printItemRealAmount = 0
              Case PaymentItemType.itIsPC
                Dim iPC As New PettyCash(item.Entity.Id)
                tmpCode = iPC.Code
                tmpName = iPC.Name
                tmpEmpCode = iPC.Employee.Code
                tmpEmpName = iPC.Employee.Name
                item.printItemBAName = tmpEmpName
                item.printItemBACode = tmpEmpCode
                item.printItemCode = tmpCode
                item.printItemRealAmount = 0
              Case PaymentItemType.itIsAval
              Case PaymentItemType.itIsADVP
                item.printItemCode = CType(item.Entity, OutgoingCheck).CqCode
                item.printItemRealAmount = 0
              Case PaymentItemType.itIsADVM
                Dim iADVM As New AdvanceMoney(item.Entity.Id)
                tmpCode = iADVM.Code
                tmpName = iADVM.Name
                tmpEmpCode = iADVM.Employee.Code
                tmpEmpName = iADVM.Employee.Name
                item.printItemBAName = tmpEmpName
                item.printItemBACode = tmpEmpCode
                item.printItemCode = tmpCode
                item.printItemRealAmount = 0

            End Select

            'PaymentItem..PaymentTypeName
            dpi = New DocPrintingItem
            dpi.Mapping = tableName & ".paymentType"
            dpi.Value = item.EntityType.Description
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = tableName
            dpiColl.Add(dpi)


            'PaymentItemAllGeneric.Code
            dpi = New DocPrintingItem
            dpi.Mapping = tableName & ".Code"
            dpi.Value = item.printItemCode
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = tableName
            dpiColl.Add(dpi)


            'PaymentItemAllGeneric.DueDate
            dpi = New DocPrintingItem
            dpi.Mapping = tableName & ".DueDate"
            If item.DueDate > Date.MinValue Then
              dpi.Value = item.DueDate.ToShortDateString
            Else
              dpi.Value = ""
            End If
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = tableName
            dpiColl.Add(dpi)

            'PaymentItemAllGeneric.BankAccountCode
            dpi = New DocPrintingItem
            dpi.Mapping = tableName & ".BankAccountCode"
            dpi.Value = item.printItemBACode
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = tableName
            dpiColl.Add(dpi)

            'PaymentItemAllGeneric.BankAccount
            dpi = New DocPrintingItem
            dpi.Mapping = tableName & ".BankAccount"
            dpi.Value = item.printItemBAName
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = tableName
            dpiColl.Add(dpi)



            'PaymentItemAllGeneric.LimitAmount
            dpi = New DocPrintingItem
            dpi.Mapping = tableName & ".LimitAmount"
            If item.Entity.Amount > 0 Then
              dpi.Value = Configuration.FormatToString(item.Entity.Amount, DigitConfig.Price)
            Else
              dpi.Value = ""
            End If
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = tableName
            dpiColl.Add(dpi)

            'PaymentItemAllGeneric.payAmount
            dpi = New DocPrintingItem
            dpi.Mapping = tableName & ".PayAmount"
            dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
            dpi.DataType = "System.Decimal"
            dpi.Row = n + 1
            dpi.Table = tableName
            dpiColl.Add(dpi)

          End If

          'PaymentItem.Note
          dpi = New DocPrintingItem
          dpi.Mapping = tableName & ".Note"
          dpi.Value = item.Note
          dpi.DataType = "System.String"
          dpi.Row = n + 1
          dpi.Table = tableName
          dpiColl.Add(dpi)

          n += 1

          If tableType = 0 Then
            If itIsCheck Then
              TotalCheck += item.Amount
            ElseIf itIsBto Then
              TotalTransferOut += item.Amount
            ElseIf itIsCash Then
              TotalCash += item.Amount
            ElseIf itIsPC Then
              TotalPettyCash += item.Amount
            ElseIf itIsADVP Then
              TotalAdvancePay += item.Amount
            ElseIf itIsADVM Then
              TotalAdvanceMoney += item.Amount
            End If
          End If
        Next
      Next

      Dim totalOtherCutPay As Decimal
      totalOtherCutPay = Me.DiscountAmount + Me.OtherRevenue + Me.WitholdingTax + Me.DebitCollection.GetAmount
      'totalOtherCutPay
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalOtherCutPay"
      dpi.Value = Configuration.FormatToString(totalOtherCutPay, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      Dim totalOtherPay As Decimal
      totalOtherPay = Me.Interest + Me.BankCharge + Me.OtherExpense + Me.CreditCollection.GetAmount
      'totalOtherPay
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalOtherPay"
      dpi.Value = Configuration.FormatToString(totalOtherPay, DigitConfig.Price)
      dpi.DataType = "System.String"
            dpiColl.Add(dpi)

            '--------------------------------------
            'payment_discountAmount '��ǹŴ�Ѻ
            dpi = New DocPrintingItem
            dpi.Mapping = "DiscountAmount"
            dpi.Value = Configuration.FormatToString(Me.payment_discountAmount, DigitConfig.Price)
            dpi.DataType = "System.String"
            dpiColl.Add(dpi)

            'payment_otherRevenue ���������
            dpi = New DocPrintingItem
            dpi.Mapping = "OtherRevenue"
            dpi.Value = Configuration.FormatToString(Me.payment_otherRevenue, DigitConfig.Price)
            dpi.DataType = "System.String"
            dpiColl.Add(dpi)
            '--------------------------------------

      'TotalCash
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalCash"
      dpi.Value = Configuration.FormatToString(TotalCash, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'TotalPettyCash
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalPettyCash"
      dpi.Value = Configuration.FormatToString(TotalPettyCash, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'TotalAdvancePay
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalAdvancePay"
      dpi.Value = Configuration.FormatToString(TotalAdvancePay, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'TotalAdvanceMoney
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalAdvanceMoney"
      dpi.Value = Configuration.FormatToString(TotalAdvanceMoney, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'TotalCheck
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalCheck"
      dpi.Value = Configuration.FormatToString(TotalCheck, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'CheckCode
      dpi = New DocPrintingItem
      dpi.Mapping = "CheckCode"
      dpi.Value = CheckCode
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'TotalTransferOut
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalTransferOut"
      dpi.Value = Configuration.FormatToString(TotalTransferOut, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'TotalCheckAndBTO
      dpi = New DocPrintingItem
      dpi.Mapping = "TotalCheckAndBTO"
      dpi.Value = Configuration.FormatToString(TotalCheck + TotalTransferOut, DigitConfig.Price)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      '############################################################################
      dpiColl.AddRange(GetGLDocPrintingEntries)
      dpiColl.AddRange(GetGoodsReceiptDocPrintingEntries)
      dpiColl.AddRange(GetAdvancePayDocPrintingEntries)
      dpiColl.AddRange(GetPettyCashClaimItemDocPrintingEntries)
      dpiColl.AddRange(GetAdvanceMoneyDocPrintingEntries)
      dpiColl.AddRange(GetPaymentSelectionDocPrintingEntries)
      dpiColl.AddRange(GetPADocPrintingEntries)
      '############################################################################

      'RemainingAmount
      dpi = New DocPrintingItem
      dpi.Mapping = "RemainingAmount"
      dpi.Value = Configuration.FormatToString(Me.Amount - Me.Gross, DigitConfig.UnitPrice)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'PaidAmount
      dpi = New DocPrintingItem
      dpi.Mapping = "PaidAmount"
      dpi.Value = Configuration.FormatToString(Me.Gross, DigitConfig.UnitPrice)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'OtherCutPayAmount
      dpi = New DocPrintingItem
      dpi.Mapping = "OtherCutPayAmount"
      dpi.Value = Configuration.FormatToString(Me.DebitAmount, DigitConfig.UnitPrice)
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      Dim r As Integer = 0
      If TypeOf Me.RefDoc Is IVatable Then
        For Each vitem As VatItem In CType(Me.RefDoc, IVatable).Vat.ItemCollection

          'vati_refdoc
          dpi = New DocPrintingItem
          dpi.Mapping = "vati_refdoc"
          dpi.Value = Me.RefDoc.Id
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'vati_refdoc
          dpi = New DocPrintingItem
          dpi.Mapping = "vati_refdoctype"
          If TypeOf Me.RefDoc Is ISimpleEntity Then
            dpi.Value = CType(Me.RefDoc, ISimpleEntity).EntityId
          End If
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem.LineNumber
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem.LineNumber"
          dpi.Value = r + 1
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem.RunNumber
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem.RunNumber"
          dpi.Value = vitem.Runnumber
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem.Code
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem.Code"
          dpi.Value = vitem.Code
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem.DocDate
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem.DocDate"
          dpi.Value = vitem.DocDate.ToShortDateString
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem..PrintName
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem..PrintName"
          dpi.Value = vitem.PrintName
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem..PrintAddress
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem..PrintAddress"
          dpi.Value = vitem.PrintAddress
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem.TaxBase
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem.TaxBase"
          dpi.Value = Configuration.FormatToString(vitem.TaxBase, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem.TaxRate
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem.TaxRate"
          dpi.Value = Configuration.FormatToString(vitem.TaxRate, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem.VatAmount
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem.VatAmount"
          dpi.Value = Configuration.FormatToString(vitem.Amount, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          'VatItem.Note
          dpi = New DocPrintingItem
          dpi.Mapping = "VatItem.Note"
          dpi.Value = vitem.Note
          dpi.DataType = "System.String"
          dpi.Row = r + 1
          dpi.Table = "VatItem"
          dpiColl.Add(dpi)

          r += 1
        Next
      End If

      Return dpiColl
    End Function
    Private Function GetGLDocPrintingEntries() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem
      Dim SumCredit As Decimal = 0
      Dim SumDebit As Decimal = 0
      If TypeOf Me.RefDoc Is IGLAble Then
        Dim je As JournalEntry = CType(Me.RefDoc, IGLAble).JournalEntry
        If Not je Is Nothing Then
          'RefGLCode
          dpi = New DocPrintingItem
          dpi.Mapping = "RefGLCode"
          dpi.Value = je.Code
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefGLDate
          dpi = New DocPrintingItem
          dpi.Mapping = "RefGLDate"
          dpi.Value = je.DocDate.ToShortDateString
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'AccountBook
          dpi = New DocPrintingItem
          dpi.Mapping = "AccountBook"
          If Not je.AccountBook Is Nothing Then
            dpi.Value = je.AccountBook.Name
          End If
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)
          'JournalName
          dpi = New DocPrintingItem
          dpi.Mapping = "JournalName"
          dpi.Value = je.AccountBook.TitleName
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)
          Dim n As Integer = 0
          For Each item As JournalEntryItem In je.ItemCollection
            'paymenti_payment
            dpi = New DocPrintingItem
            dpi.Mapping = "paymenti_payment"
            dpi.Value = Me.Id
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "Item"
            dpiColl.Add(dpi)

            'Item.LineNumber
            dpi = New DocPrintingItem
            dpi.Mapping = "Item.LineNumber"
            dpi.Value = n + 1
            dpi.DataType = "System.Int32"
            dpi.Row = n + 1
            dpi.Table = "Item"
            dpiColl.Add(dpi)

            'Item.AccountCode
            dpi = New DocPrintingItem
            dpi.Mapping = "Item.AccountCode"
            If Not item.Account Is Nothing Then
              dpi.Value = item.Account.Code
            Else
              dpi.Value = ""
            End If
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "Item"
            dpiColl.Add(dpi)

            Dim amt As String = Configuration.FormatToString(item.Amount, DigitConfig.Price)
            Dim Bfpoint As String = Trim(Split(Replace(amt, ",", ""), ".")(0))
            Dim Aftpoint As String = "00"
            If UBound(Split(amt, "."), 1) <> 0 Then
              Aftpoint = Left(Trim(Split(amt, ".")(1)), 2)
            End If
            amt = Configuration.FormatToString(item.Amount, DigitConfig.Price)
            Bfpoint = Trim(Split(Replace(amt, ",", ""), ".")(0))
            Aftpoint = "00"
            If UBound(Split(amt, "."), 1) <> 0 Then
              Aftpoint = Left(Trim(Split(amt, ".")(1)), 2)
            End If
            Dim space As String = ""
            If item.IsDebit Then
              'Item.Debit
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.Debit"
              dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
              SumDebit += item.Amount
              dpi.DataType = "System.Decimal"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)

              'Item.DebitBaht
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.DebitBaht"
              dpi.Value = Configuration.FormatToString(CDec(Bfpoint), DigitConfig.Int)
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)

              'Item.DebitSatang
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.DebitSatang"
              dpi.Value = Aftpoint
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)

              'Item.Amount
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.Amount"
              dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
              dpi.DataType = "System.Decimal"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)
            Else
              'Item.Credit
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.Credit"
              dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
              SumCredit += item.Amount
              dpi.DataType = "System.Decimal"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)

              'Item.CreditBaht
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.CreditBaht"
              dpi.Value = Configuration.FormatToString(CDec(Bfpoint), DigitConfig.Int)
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)

              'Item.CreditSatang
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.CreditSatang"
              dpi.Value = Aftpoint
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)

              'Item.Amount
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.Amount"
              dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
              dpi.DataType = "System.Decimal"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)

              space = "     "
            End If

            'Item.AccountName
            dpi = New DocPrintingItem
            dpi.Mapping = "Item.AccountName"
            If Not item.Account Is Nothing Then
              dpi.Value = space & item.Account.Name
            Else
              dpi.Value = ""
            End If
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "Item"
            dpiColl.Add(dpi)

            'Item.CostCenter
            dpi = New DocPrintingItem
            dpi.Mapping = "Item.CostCenter"
            If Not item.CostCenter Is Nothing Then
              dpi.Value = item.CostCenter.Code
            Else
              dpi.Value = ""
            End If
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "Item"
            dpiColl.Add(dpi)

            'Item.Note
            dpi = New DocPrintingItem
            dpi.Mapping = "Item.Note"
            If Not item.Account Is Nothing Then
              dpi.Value = item.Note
            Else
              dpi.Value = ""
            End If
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "Item"
            dpiColl.Add(dpi)

            n += 1
          Next
          'SumCredit
          dpi = New DocPrintingItem
          dpi.Mapping = "SumCredit"
          dpi.Value = Configuration.FormatToString(SumCredit, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'SumDebit
          dpi = New DocPrintingItem
          dpi.Mapping = "SumDebit"
          dpi.Value = Configuration.FormatToString(SumDebit, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

        End If
      End If
      Return dpiColl
    End Function
    Private Function GetAdvancePayDocPrintingEntries() As DocPrintingItemCollection
      Dim n As Integer
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem
      If TypeOf Me.RefDoc Is AdvancePay Then
        Dim advp As AdvancePay = CType(Me.RefDoc, AdvancePay)
        If Not advp Is Nothing Then
          If Not advp.CostCenter Is Nothing Then
            'ToCostCenterInfo
            dpi = New DocPrintingItem
            dpi.Mapping = "CostCenterInfo"
            dpi.Value = advp.CostCenter.Code & ":" & advp.CostCenter.Name
            dpi.DataType = "System.String"
            dpiColl.Add(dpi)

            'ToCostCenterCode
            dpi = New DocPrintingItem
            dpi.Mapping = "CostCenterCode"
            dpi.Value = advp.CostCenter.Code
            dpi.DataType = "System.String"
            dpiColl.Add(dpi)

            'ToCostCenterName
            dpi = New DocPrintingItem
            dpi.Mapping = "CostCenterName"
            dpi.Value = advp.CostCenter.Name
            dpi.DataType = "System.String"
            dpiColl.Add(dpi)
          End If
          'RefDocCode
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocCode"
          dpi.Value = advp.Code
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocDate
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocDate"
          dpi.Value = advp.DocDate
          dpi.DataType = "System.DateTime"
          dpiColl.Add(dpi)

          'WHTAmount
          dpi = New DocPrintingItem
          dpi.Mapping = "WHTAmount"
          dpi.Value = Configuration.FormatToString(advp.WitholdingTaxCollection.Amount, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocBeforeTax
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocBeforeTax"
          dpi.Value = Configuration.FormatToString(advp.BeforeTax, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDoc Tax Amount
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocTaxAmount"
          dpi.Value = Configuration.FormatToString(advp.TaxAmount, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocAftertax
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocAftertax"
          dpi.Value = Configuration.FormatToString(advp.AfterTax, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)
        End If
      End If

      Return dpiColl
    End Function
    Private Function GetGoodsReceiptDocPrintingEntries() As DocPrintingItemCollection
      Dim n As Integer
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem
      If TypeOf Me.RefDoc Is GoodsReceipt Then
        Dim gr As GoodsReceipt = CType(Me.RefDoc, GoodsReceipt)
        If Not gr Is Nothing Then
          If Not gr.ToCostCenter Is Nothing Then
            'ToCostCenterInfo
            dpi = New DocPrintingItem
            dpi.Mapping = "CostCenterInfo"
            dpi.Value = gr.ToCostCenter.Code & ":" & gr.ToCostCenter.Name
            dpi.DataType = "System.String"
            dpiColl.Add(dpi)

            'ToCostCenterCode
            dpi = New DocPrintingItem
            dpi.Mapping = "CostCenterCode"
            dpi.Value = gr.ToCostCenter.Code
            dpi.DataType = "System.String"
            dpiColl.Add(dpi)

            'ToCostCenterName
            dpi = New DocPrintingItem
            dpi.Mapping = "CostCenterName"
            dpi.Value = gr.ToCostCenter.Name
            dpi.DataType = "System.String"
            dpiColl.Add(dpi)
          End If
          'RefDocCode
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocCode"
          dpi.Value = gr.Code
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocDate
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocDate"
          dpi.Value = gr.DocDate
          dpi.DataType = "System.DateTime"
          dpiColl.Add(dpi)

          'POCode
          dpi = New DocPrintingItem
          dpi.Mapping = "POCode"
          dpi.Value = gr.Po.Code
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'PODocDate
          dpi = New DocPrintingItem
          dpi.Mapping = "PODocDate"
          dpi.Value = gr.Po.DocDate.ToShortDateString
          dpi.DataType = "System.DateTime"
          dpiColl.Add(dpi)

          'PURCode
          dpi = New DocPrintingItem
          dpi.Mapping = "PURCode"
          dpi.Value = gr.Payment.RefDoc.Code
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'PURDate
          dpi = New DocPrintingItem
          dpi.Mapping = "PURDate"
          dpi.Value = gr.Payment.RefDoc.Date.ToShortDateString
          dpi.DataType = "System.DateTime"
          dpiColl.Add(dpi)

          Dim dt As DataTable = GetBAFromSproc("GetBAfromGoodsReceiptCodeList", gr.Payment.RefDoc.Id)
          Dim tmpBillaCode As String = ""
          Dim tmpBillaDocDate As String = ""
          If Not dt Is Nothing Then
            For Each row As DataRow In dt.Rows
              If Not row.IsNull("BillaCode") Then
                tmpBillaCode &= row("BillaCode").ToString & ","
              End If
              If IsDate(row("BillaDocDate")) Then
                tmpBillaDocDate &= CDate(row("BillaDocDate")).ToShortDateString & ","
              End If
            Next
          End If
          If tmpBillaCode.Length > 0 Then
            tmpBillaCode = tmpBillaCode.Substring(0, tmpBillaCode.Length - 1)
          End If
          If tmpBillaDocDate.Length > 0 Then
            tmpBillaDocDate = tmpBillaDocDate.Substring(0, tmpBillaDocDate.Length - 1)
          End If

          'BICode
          dpi = New DocPrintingItem
          dpi.Mapping = "BICode"
          dpi.Value = tmpBillaCode
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'BIDocDate
          dpi = New DocPrintingItem
          dpi.Mapping = "BIDocDate"
          dpi.Value = tmpBillaDocDate
          dpi.DataType = "System.DateTime"
          dpiColl.Add(dpi)


          'RefDocTaxAmount
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocTaxAmount"
          dpi.Value = Configuration.FormatToString(gr.RealTaxAmount, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocBeforeTax
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocBeforeTax"
          dpi.Value = Configuration.FormatToString(gr.BeforeTax, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocDiscountAmount
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocDiscountAmount"
          dpi.Value = Configuration.FormatToString(gr.DiscountAmount, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocAdvanceMoney
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocAdvanceMoney"
          Select Case gr.TaxType.Value
            Case 0, 1 '"�����","�¡"
              dpi.Value = Configuration.FormatToString(gr.AdvancePayItemCollection.GetExcludeVATAmount, DigitConfig.UnitPrice)
            Case 2 '"���"
              dpi.Value = Configuration.FormatToString(gr.AdvancePayItemCollection.GetAmount, DigitConfig.UnitPrice)
          End Select
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocRetention
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocRetention"
          dpi.Value = Configuration.FormatToString(gr.Retention, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocAfterTax
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocAfterTax"
          dpi.Value = Configuration.FormatToString(gr.AfterTax, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          Dim sumRefDocItemAmount As Decimal = 0
          Dim line As Integer = 0

          Dim grColl As New GoodsReceiptItemCollection(gr)
          For Each item As GoodsReceiptItem In grColl

            'paymenti_payment
            dpi = New DocPrintingItem
            dpi.Mapping = "paymenti_payment"
            dpi.Value = Me.Id
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            'RefDocItem.Code
            dpi = New DocPrintingItem
            dpi.Mapping = "RefDocItem.Code"
            dpi.Value = item.Entity.Code
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            ''RefDocItem.Code
            'dpi = New DocPrintingItem
            'dpi.Mapping = "RefDocItem.Code"
            'dpi.Value = item.Entity.Code
            'dpi.DataType = "System.String"
            'dpi.Row = n + 1
            'dpi.Table = "RefDocItem"
            'dpiColl.Add(dpi)

            If (item.ItemType.Value <> 160 And item.ItemType.Value <> 162) Then
              line += 1
              'Item.LineNumber
              '************** �����������ѹ��� 2
              'RefDocItem.LineNumber
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.LineNumber"
              dpi.Value = line
              dpi.DataType = "System.Int32"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.Unit
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.Unit"
              dpi.Value = item.Unit.Name
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.Qty
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.Qty"
              dpi.Value = Configuration.FormatToString(item.Qty, DigitConfig.Qty)
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.UnitPrice
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.UnitPrice"
              If item.UnitPrice = 0 Then
                dpi.Value = ""
              Else
                dpi.Value = Configuration.FormatToString(item.UnitPrice, DigitConfig.UnitPrice)
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.DiscountRate
              dpi = New DocPrintingItem
              dpi.Mapping = "Item.DiscountRate"
              dpi.Value = item.Discount.Rate
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "Item"
              dpiColl.Add(dpi)

              'RefDocItem.DiscountAmount
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.DiscountAmount"
              If item.Discount.Amount = 0 Then
                dpi.Value = ""
              Else
                dpi.Value = Configuration.FormatToString(item.Discount.Amount, DigitConfig.Price)
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.Amount
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.Amount"
              If item.Amount = 0 Then
                dpi.Value = ""
              Else
                If item.ItemType.Value = 46 Then
                  dpi.Value = Configuration.FormatToString(-item.Amount, DigitConfig.Price)
                  sumRefDocItemAmount -= item.Amount
                Else
                  dpi.Value = Configuration.FormatToString(item.Amount, DigitConfig.Price)
                  sumRefDocItemAmount += item.Amount
                End If
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.ZeroVat
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.ZeroVat"
              dpi.Value = item.UnVatable
              dpi.DataType = "System.Boolean"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)
            End If
            'RefDocItem.Description
            dpi = New DocPrintingItem
            dpi.Mapping = "RefDocItem.Description"
            If Not item.EntityName Is Nothing AndAlso item.EntityName.Length > 0 Then
              dpi.Value = item.EntityName
            Else
              dpi.Value = item.Entity.Name
            End If
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            'RefDocItem.Note
            dpi = New DocPrintingItem
            dpi.Mapping = "RefDocItem.Note"
            dpi.Value = item.Note
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            n += 1
          Next
          ''RemainingAmount
          'dpi = New DocPrintingItem
          'dpi.Mapping = "RemainingAmount"
          'dpi.Value = Configuration.FormatToString(Me.Amount - Me.Gross, DigitConfig.UnitPrice)
          'dpi.DataType = "System.String"
          'dpiColl.Add(dpi)

          ''PaidAmount
          'dpi = New DocPrintingItem
          'dpi.Mapping = "PaidAmount"
          'dpi.Value = Configuration.FormatToString(Me.Gross, DigitConfig.UnitPrice)
          'dpi.DataType = "System.String"
          'dpiColl.Add(dpi)

          ''PaidAmount
          'dpi = New DocPrintingItem
          'dpi.Mapping = "OtherCutPayAmount"
          'dpi.Value = Configuration.FormatToString(Me.DebitAmount, DigitConfig.UnitPrice)
          'dpi.DataType = "System.String"
          'dpiColl.Add(dpi)

          'WHTAmount
          dpi = New DocPrintingItem
          dpi.Mapping = "WHTAmount"
          dpi.Value = Configuration.FormatToString(gr.WitholdingTaxCollection.Amount, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

        End If
      End If

      Return dpiColl
    End Function
    Private Function GetAdvanceMoneyDocPrintingEntries() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem
      If TypeOf Me.RefDoc Is AdvanceMoney Then
        Dim am As AdvanceMoney = CType(Me.RefDoc, AdvanceMoney)
        Dim n As Integer = 0

        'AdvanceMoneyName
        dpi = New DocPrintingItem
        dpi.Mapping = "AdvanceMoneyName"
        dpi.Value = am.Name
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        For Each itemRow As TreeRow In am.ItemTable.Rows
          'paymenti_payment
          dpi = New DocPrintingItem
          dpi.Mapping = "paymenti_payment"
          dpi.Value = Me.Id
          dpi.DataType = "System.String"
          dpi.Row = n + 1
          dpi.Table = "RefDocItem"
          dpiColl.Add(dpi)

          'RefDocItem.LineNumber
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocItem.LineNumber"
          dpi.Value = n + 1
          dpi.DataType = "System.Int32"
          dpi.Row = n + 1
          dpi.Table = "RefDocItem"
          dpiColl.Add(dpi)

          'RefDocItem.Description
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocItem.Description"
          dpi.Value = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.GetAdvanceMoneyDocPrintingEntries.AdvanceMoney}")
          dpi.DataType = "System.String"
          dpi.Row = n + 1
          dpi.Table = "RefDocItem"
          dpiColl.Add(dpi)

          'RefDocItem.Amount
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocItem.Amount"
          dpi.Value = Configuration.FormatToString(am.Amount, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpi.Row = n + 1
          dpi.Table = "RefDocItem"
          dpiColl.Add(dpi)

          n += 1
        Next
      End If
      Return dpiColl
    End Function
    Private Function GetPettyCashClaimItemDocPrintingEntries() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem
      If TypeOf Me.RefDoc Is PettyCashClaim Then
        Dim pcc As PettyCashClaim = CType(Me.RefDoc, PettyCashClaim)
        Dim n As Integer = 0

        'RefDocGross
        dpi = New DocPrintingItem
        dpi.Mapping = "RefDocGross"
        dpi.Value = Configuration.FormatToString(pcc.Gross, DigitConfig.Price)
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'RefPettyCashCode
        dpi = New DocPrintingItem
        dpi.Mapping = "RefPettyCashCode"
        dpi.Value = pcc.PettyCash.Code
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'RefPettyCashName
        dpi = New DocPrintingItem
        dpi.Mapping = "RefPettyCashName"
        dpi.Value = pcc.PettyCash.Name
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        Dim refCC As New ArrayList
        Dim refCCName As String = ""
        For Each itemRow As TreeRow In pcc.ItemTable.Rows
          If pcc.ValidateRow(itemRow) Then
            Dim item As New PettyCashClaimItem
            item.CopyFromDataRow(itemRow)

            'paymenti_payment
            dpi = New DocPrintingItem
            dpi.Mapping = "paymenti_payment"
            dpi.Value = Me.Id
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            'RefDocItem.LineNumber
            dpi = New DocPrintingItem
            dpi.Mapping = "RefDocItem.LineNumber"
            dpi.Value = n + 1
            dpi.DataType = "System.Int32"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            'RefDocItem.Description
            dpi = New DocPrintingItem
            dpi.Mapping = "RefDocItem.Description"
            dpi.Value = item.RefDocCode & " : " & item.RefDocType
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            'RefDocItem.Amount
            dpi = New DocPrintingItem
            dpi.Mapping = "RefDocItem.Amount"
            dpi.Value = Configuration.FormatToString(item.RefDocAmount, DigitConfig.Price)
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            'RefDocItem.PaidAmount
            dpi = New DocPrintingItem
            dpi.Mapping = "RefDocItem.PaidAmount"
            dpi.Value = Configuration.FormatToString(item.PaidAmount, DigitConfig.Price)
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            'RefDocItem.Note
            dpi = New DocPrintingItem
            dpi.Mapping = "RefDocItem.Note"
            dpi.Value = item.Note
            dpi.DataType = "System.String"
            dpi.Row = n + 1
            dpi.Table = "RefDocItem"
            dpiColl.Add(dpi)

            Dim py As New Payment(item.Paymentid)
            If Not py Is Nothing Then
              Dim cc As New CostCenter(py.payment_ccId)
              If Not cc Is Nothing Then
                'RefDocItem.CostCenterCode
                dpi = New DocPrintingItem
                dpi.Mapping = "RefDocItem.CostCenterCode"
                dpi.Value = cc.Code
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

                'RefDocItem.CostCenterName
                dpi = New DocPrintingItem
                dpi.Mapping = "RefDocItem.CostCenterName"
                dpi.Value = cc.Name
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

                If Not cc.Name Is Nothing Then
                  If refCC.Count = 0 Then
                    refCC.Add(cc.Name)
                  ElseIf refCC.Count > 0 Then
                    Dim chkDup As Boolean = False
                    For i As Integer = 0 To refCC.Count - 1
                      If refCC(i).ToString = cc.Name Then
                        chkDup = True
                      End If
                    Next
                    If Not chkDup Then
                      refCC.Add(cc.Name)
                    End If
                  End If
                End If

              End If
            End If

            n += 1
          End If
        Next

        For i As Integer = 0 To refCC.Count - 1
          If i < refCC.Count - 1 Then
            refCCName &= refCC(i).ToString & ","
          Else
            refCCName &= refCC(i).ToString
          End If
        Next
        'If refCCName.Length <> 0 Then
        '  refCCName = refCCName.Substring(0, refCCName.Length - 1)
        'End If

        'RefPettyCashItemCostCenter
        dpi = New DocPrintingItem
        dpi.Mapping = "RefPettyCashItemCostCenter"
        dpi.Value = refCCName
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)
      End If
      Return dpiColl
    End Function
    Private Function GetPaymentSelectionDocPrintingEntries() As DocPrintingItemCollection
      Dim n As Integer
      Dim sumAfterTax As Decimal = 0
      Dim sumBeforTax As Decimal = 0
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      If TypeOf Me.RefDoc Is PaySelection Then
        Dim ps As PaySelection = CType(Me.RefDoc, PaySelection)
        'RefDocCode
        dpi = New DocPrintingItem
        dpi.Mapping = "RefDocCode"
        dpi.Value = ps.Code
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'RefDocDate
        dpi = New DocPrintingItem
        dpi.Mapping = "RefDocDate"
        dpi.Value = ps.DocDate
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'PVDocCode
        dpi = New DocPrintingItem
        dpi.Mapping = "PVDocCode"
        dpi.Value = ps.Payment.Code
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'PVDocDate
        dpi = New DocPrintingItem
        dpi.Mapping = "PVDocDate"
        dpi.Value = ps.Payment.DocDate
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        Dim dt As TreeTable = PaySelection.GetSchemaTable()
        ps.ItemCollection.PopulatePaySelectionItem(dt)

        Dim tmpRefItemCode As String = ""
        Dim tmpBACode As String = ""
        Dim tmpDescription As String = ""

        Dim refRetention As Decimal = 0
        Dim refDocRetention As Decimal = 0

        For Each dr As TreeRow In dt.Rows

          If dr.IsNull("paysi_parentEntity") OrElse CDec(dr("paysi_parentEntity")) <> 0 Then
            If Not IsDBNull(dr("paysi_entityType")) Then
              Dim dh As New DataRowHelper(dr)
              Dim stock_id As Integer = dh.GetValue(Of Integer)("paysi_entity")
              Dim stock_type As Integer = dh.GetValue(Of Integer)("paysi_entityType")
              Dim retention_type As Integer = dh.GetValue(Of Integer)("paysi_retentiontype", 0)
              'Trace.WriteLine(retention_type.ToString)
              'Dim s As Stock = ps.FindStock(stock_id, stock_type)
              Dim s As PaySelectionRefDoc = ps.GetPaySelectionRefDocFromHsIDType(stock_id, stock_type, retention_type)
              If s IsNot Nothing Then
                'paymenti_payment
                dpi = New DocPrintingItem
                dpi.Mapping = "paymenti_payment"
                dpi.Value = Me.Id
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

                'RefDocItem.Glnote
                dpi = New DocPrintingItem
                dpi.Mapping = "RefDocItem.glnote"
                dpi.Value = s.GLNote
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

                'RefDocItem.GLCode
                dpi = New DocPrintingItem
                dpi.Mapping = "RefDocItem.GLCode"
                dpi.Value = s.GLCode
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

                'RefDocItem.refnote
                dpi = New DocPrintingItem
                dpi.Mapping = "RefDocItem.refnote"
                dpi.Value = s.StockNote
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

                'RefDocItem.VatCodes
                dpi = New DocPrintingItem
                dpi.Mapping = "RefDocItem.VatCodes"
                dpi.Value = s.GetVatCodes
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

                'refRetention = ps.GetRetentionItem(stock_id, stock_type, retention_type)
                If stock_type <> 199 Then
                  refRetention = s.StockRetention
                Else
                  refRetention = 0
                End If
                refDocRetention += refRetention
                'RefDocItem.Retention
                dpi = New DocPrintingItem
                dpi.Mapping = "RefDocItem.Retention"
                dpi.Value = Configuration.FormatToString(refRetention, DigitConfig.UnitPrice)
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

                'RefDocItem.CostCenterCode
                dpi = New DocPrintingItem
                dpi.Mapping = "RefDocItem.CostCenterCode"
                Trace.WriteLine("stock_type=" & stock_type.ToString)
                'dpi.Value = ps.GetCostCenterFromRefDoc(stock_id, stock_type, retention_type).Code
                dpi.Value = s.CCCode
                dpi.DataType = "System.String"
                dpi.Row = n + 1
                dpi.Table = "RefDocItem"
                dpiColl.Add(dpi)

              End If
              'RefDocItem.LineNumber
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.LineNumber"
              If Not IsDBNull(dr("paysi_linenumber")) Then
                dpi.Value = dr("paysi_linenumber")
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.Description
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.Description"
              If Not IsDBNull(dr("paysi_entityType")) Then
                If Not IsDBNull(dr("Code")) Then
                  dpi.Value = CodeDescription.GetDescription("paysi_entityType", CInt(dr("paysi_entityType"))) & ":" & CStr(dr("Code"))
                  tmpRefItemCode = tmpRefItemCode & CStr(dr("Code")) & ","
                Else
                  dpi.Value = CodeDescription.GetDescription("paysi_entityType", CInt(dr("paysi_entityType")))
                End If
              Else
                If Not IsDBNull(dr("Code")) Then
                  dpi.Value = CStr(dr("Code"))
                Else
                  dpi.Value = ""
                End If
              End If
              tmpDescription = CStr(dpi.Value)
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              If Not IsDBNull(dr("paysi_parentEntity")) Then
                If CInt(dr("paysi_parentEntityType")) = 60 Then
                  tmpBACode = tmpBACode & CStr(dr("Code")) & ","
                End If
              End If

              'RefDocItem.DescriptionWithNote
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.DescriptionWithNote"
              If Not IsDBNull(dr("paysi_note")) Then
                dpi.Value = tmpDescription & " - " & CStr(dr("paysi_note"))
              Else
                dpi.Value = tmpDescription
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.Amount
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.Amount"
              If Not IsDBNull(dr("paysi_amt")) Then
                Dim paysi_amt As Decimal = CDec(dr("paysi_amt"))
                If stock_type = 46 Then
                  paysi_amt = (-1) * paysi_amt
                End If
                dpi.Value = Configuration.FormatToString(paysi_amt, DigitConfig.UnitPrice)
                sumAfterTax += paysi_amt 'CDec(dr("paysi_amt"))
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.Note
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.Note"
              If Not IsDBNull(dr("paysi_note")) Then
                dpi.Value = CStr(dr("paysi_note"))
              Else
                dpi.Value = ""
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.DocDate
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.DocDate"
              If Not IsDBNull(dr("DocDate")) And IsDate(dr("DocDate")) Then
                dpi.Value = CDate(dr("DocDate")).ToShortDateString
              Else
                dpi.Value = ""
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.DueDate
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.DueDate"
              If Not IsDBNull(dr("DueDate")) And IsDate(dr("DueDate")) Then
                dpi.Value = CDate(dr("DueDate")).ToShortDateString
              Else
                dpi.Value = ""
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.RealAmount
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.RealAmount"
              If Not IsDBNull(dr("RealAmount")) Then
                dpi.Value = Configuration.FormatToString(CDec(dr("RealAmount")), DigitConfig.UnitPrice)
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.SignedRealAmount
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.SignedRealAmount"
              If Not IsDBNull(dr("RealAmount")) Then
                If stock_type = 46 Then
                  dpi.Value = Configuration.FormatToString(-CDec(dr("RealAmount")), DigitConfig.UnitPrice)
                Else
                  dpi.Value = Configuration.FormatToString(CDec(dr("RealAmount")), DigitConfig.UnitPrice)
                End If
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              'RefDocItem.UnpaidAmount
              dpi = New DocPrintingItem
              dpi.Mapping = "RefDocItem.UnpaidAmount"

              If Not IsDBNull(dr("UnpaidAmount")) Then
                Dim paysi_unpaidamt As Decimal = CDec(dr("UnpaidAmount"))
                If stock_type = 46 Then
                  paysi_unpaidamt = (-1) * paysi_unpaidamt
                End If
                dpi.Value = Configuration.FormatToString(paysi_unpaidamt, DigitConfig.UnitPrice)
              End If
              dpi.DataType = "System.String"
              dpi.Row = n + 1
              dpi.Table = "RefDocItem"
              dpiColl.Add(dpi)

              n += 1

            End If
          End If

        Next

        'RefDocRetention
        dpi = New DocPrintingItem
        dpi.Mapping = "RefDocRetention"
        dpi.Value = Configuration.FormatToString(refDocRetention, DigitConfig.UnitPrice)
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'RefItemCode
        dpi = New DocPrintingItem
        dpi.Mapping = "RefItemCode"
        If tmpRefItemCode.Length > 1 Then
          dpi.Value = tmpRefItemCode.Substring(0, tmpRefItemCode.Length - 1)
        Else
          dpi.Value = ""
        End If
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'BACode
        dpi = New DocPrintingItem
        dpi.Mapping = "BACode"
        If tmpBACode.Length > 1 Then
          dpi.Value = tmpBACode.Substring(0, tmpBACode.Length - 1)
        Else
          dpi.Value = ""
        End If
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'RefDocTaxAmount
        dpi = New DocPrintingItem
        dpi.Mapping = "RefDocTaxAmount"
        dpi.Value = Configuration.FormatToString(ps.Vat.Amount, DigitConfig.UnitPrice)
        dpi.DataType = "System.Decimal"
        dpiColl.Add(dpi)

        'RefDocBeforeTax
        dpi = New DocPrintingItem
        dpi.Mapping = "RefDocBeforeTax"
        dpi.Value = Configuration.FormatToString(ps.BeforeTax, DigitConfig.UnitPrice)
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'RefDocAfterTax
        dpi = New DocPrintingItem
        dpi.Mapping = "RefDocAfterTax"
        dpi.Value = Configuration.FormatToString(sumAfterTax, DigitConfig.UnitPrice)
        dpi.DataType = "System.Decimal"
        dpiColl.Add(dpi)

        'RefBillCode
        dpi = New DocPrintingItem
        dpi.Mapping = "RefBillCode"
        dpi.Value = ps.ItemCollection.GetRefBillCodeList
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'WHTAmount
        dpi = New DocPrintingItem
        dpi.Mapping = "WHTAmount"
        If ps.WitholdingTaxCollection.Amount > 0 Then
          dpi.Value = Configuration.FormatToString(ps.WitholdingTaxCollection.Amount, DigitConfig.UnitPrice)
        Else
          dpi.Value = ""
        End If
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'PaidAmount
        dpi = New DocPrintingItem
        dpi.Mapping = "PaidAmount"
        dpi.Value = Configuration.FormatToString(Me.Gross, DigitConfig.UnitPrice)
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'DiscountAmount
        dpi = New DocPrintingItem
        dpi.Mapping = "DiscountAmount"
        dpi.Value = Configuration.FormatToString(Me.DiscountAmount, DigitConfig.UnitPrice)
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'WHTAmount
        dpi = New DocPrintingItem
        dpi.Mapping = "WHTAmount"
        dpi.Value = Configuration.FormatToString(ps.WitholdingTaxCollection.Amount, DigitConfig.UnitPrice)
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

      End If
      Return dpiColl
    End Function
    Private Function GetPADocPrintingEntries() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      If Me.RefDoc IsNot Nothing Then
        If TypeOf Me.RefDoc Is PA Then
          Dim newPa As PA = CType(Me.RefDoc, PA)

          'RefDocTaxAmount
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocTaxAmount"
          dpi.Value = Configuration.FormatToString(newPa.TaxAmount, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocBeforeTax
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocBeforeTax"
          dpi.Value = Configuration.FormatToString(newPa.BeforeTax, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocDiscountAmount
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocDiscountAmount"
          dpi.Value = Configuration.FormatToString(newPa.DiscountAmount, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocAdvanceMoney
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocAdvanceMoney"
          Select Case newPa.TaxType.Value
            Case 0, 1 '"�����","�¡"
              dpi.Value = Configuration.FormatToString(newPa.AdvancePayItemCollection.GetExcludeVATAmount, DigitConfig.UnitPrice)
            Case 2 '"���"
              dpi.Value = Configuration.FormatToString(newPa.AdvancePayItemCollection.GetAmount, DigitConfig.UnitPrice)
          End Select
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocRetention
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocRetention"
          dpi.Value = Configuration.FormatToString(newPa.Retention, DigitConfig.UnitPrice)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocAfterTax
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocAfterTax"
          dpi.Value = Configuration.FormatToString(newPa.AfterTax, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          'RefDocWHTAmount
          dpi = New DocPrintingItem
          dpi.Mapping = "RefDocWHTAmount"
          dpi.Value = Configuration.FormatToString(newPa.WitholdingTax, DigitConfig.Price)
          dpi.DataType = "System.String"
          dpiColl.Add(dpi)

          For Each refDpi As DocPrintingItem In newPa.GetDocPrintingItemsEntries
            refDpi.Table = "RefDoc" + refDpi.Table
            refDpi.Mapping = "RefDoc" + refDpi.Mapping
            dpiColl.Add(refDpi)
          Next
        End If
      End If

      Return dpiColl
    End Function

#End Region

    Public Property FromCC() As CostCenter Implements IHasFromCostCenter.FromCC
      Get
        If TypeOf Me.RefDoc Is IHasFromCostCenter Then
          Return CType(Me.RefDoc, IHasFromCostCenter).FromCC
        Else
          Return Me.CostCenter
        End If
        Return Me.CostCenter
      End Get
      Set(ByVal Value As CostCenter)

      End Set
    End Property

    Public Property ToCC() As CostCenter Implements IHasToCostCenter.ToCC
      Get
        If TypeOf Me.RefDoc Is IHasToCostCenter Then
          Return CType(Me.RefDoc, IHasToCostCenter).ToCC
        Else
          Return Me.CostCenter
        End If
        Return Me.CostCenter
      End Get
      Set(ByVal Value As CostCenter)

      End Set
    End Property

#Region "INewPrintableEntity"
    Public Function GetDocPrintingColumnsEntries() As DocPrintingItemCollection Implements INewPrintableEntity.GetDocPrintingColumnsEntries
      Dim dpiColl As New DocPrintingItemCollection

      dpiColl.RelationList.Add("general>payment_refdoc>VatItem>vati_refdoc")
      dpiColl.RelationList.Add("general>payment_refdoctype>VatItem>vati_refdoctype")

      dpiColl.RelationList.Add("general>payment_id>Item>paymenti_payment")
      dpiColl.RelationList.Add("general>payment_id>RefDocItem>paymenti_payment")
      dpiColl.RelationList.Add("general>payment_id>PaymentItem>paymenti_payment")
      dpiColl.RelationList.Add("general>payment_id>PaymentItemBTO>paymenti_payment")
      dpiColl.RelationList.Add("general>payment_id>PaymentItemAll>paymenti_payment")
      dpiColl.RelationList.Add("general>payment_id>PaymentItemAllGeneric>paymenti_payment")

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("payment_id", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("payment_refdoc", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("payment_refdoctype", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Code", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("DocDate", "System.DateTime"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocDate", "System.DateTime"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Gross", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefEmployeeCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefEmployeeName", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefEmployeeInfo", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefNote", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDueDate", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Note", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("CostCenterInfo", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("CostCenterCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("CostCenterName", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierInfo", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierName", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierAddress", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierCurrentAddress", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierPhone", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierFax", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierTaxId", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierIdNo", "System.String"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LineNumber", "System.Int32", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PaymentType", "System.String", "PaymentItem"))
      'dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PaymentTypeName", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CheckPayAmount", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.DueDate", "System.DateTime", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LimitAmount", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PayAmount", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CashDueDate", "System.DateTime", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CashAmount", "System.Decimal", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashPayAmount", "System.Decimal", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashAmount", "System.Decimal", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashPayDate", "System.DateTime", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.SetMoneyCode", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.SetMoneyName", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.EmployeeCode", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.EmployeeName", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CqCode", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Amount", "System.Decimal", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankAccount", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankAccountCode", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankBranch", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Bank", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Code", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LimitAmount", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PayAmount", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Note", "System.String", "PaymentItem"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LineNumber", "System.Int32", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PaymentType", "System.String", "PaymentItemBTO"))
      'dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PaymentTypeName", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CheckPayAmount", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.DueDate", "System.DateTime", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LimitAmount", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PayAmount", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CashDueDate", "System.DateTime", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CashAmount", "System.Decimal", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashPayAmount", "System.Decimal", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashAmount", "System.Decimal", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashPayDate", "System.DateTime", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.SetMoneyCode", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.SetMoneyName", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.EmployeeCode", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.EmployeeName", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CqCode", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Amount", "System.Decimal", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankAccount", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankAccountCode", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankBranch", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Bank", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Code", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LimitAmount", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PayAmount", "System.String", "PaymentItemBTO"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Note", "System.String", "PaymentItemBTO"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LineNumber", "System.Int32", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PaymentType", "System.String", "PaymentItemAll"))
      'dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PaymentTypeName", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CheckPayAmount", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.DueDate", "System.DateTime", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LimitAmount", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PayAmount", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CashDueDate", "System.DateTime", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CashAmount", "System.Decimal", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashPayAmount", "System.Decimal", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashAmount", "System.Decimal", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashPayDate", "System.DateTime", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.SetMoneyCode", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.SetMoneyName", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.EmployeeCode", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.EmployeeName", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CqCode", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Amount", "System.Decimal", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankAccount", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankAccountCode", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankBranch", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Bank", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Code", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LimitAmount", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PayAmount", "System.String", "PaymentItemAll"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Note", "System.String", "PaymentItemAll"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LineNumber", "System.Int32", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PaymentType", "System.String", "PaymentItemAllGeneric"))
      'dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PaymentTypeName", "System.String", "PaymentItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CheckPayAmount", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.DueDate", "System.DateTime", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LimitAmount", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PayAmount", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CashDueDate", "System.DateTime", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CashAmount", "System.Decimal", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashPayAmount", "System.Decimal", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashAmount", "System.Decimal", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PettyCashPayDate", "System.DateTime", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.SetMoneyCode", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.SetMoneyName", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.EmployeeCode", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.EmployeeName", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.CqCode", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Amount", "System.Decimal", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankAccount", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankAccountCode", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.BankBranch", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Bank", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Code", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.LimitAmount", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.PayAmount", "System.String", "PaymentItemAllGeneric"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaymentItem.Note", "System.String", "PaymentItemAllGeneric"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalOtherCutPay", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalOtherPay", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalCash", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalPettyCash", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalCash", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalPettyCash", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalAdvancePay", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalAdvanceMoney", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalCheck", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("CheckCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalTransferOut", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("TotalCheckAndBTO", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RemainingAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaidAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("OtherCutPayAmount", "System.String"))

      '############################################################################
      dpiColl.AddRange(GetGLDocPrintingEntriesColumns)
      dpiColl.AddRange(GetGoodsReceiptDocPrintingEntriesColumns)
      dpiColl.AddRange(GetAdvancePayDocPrintingEntriesColumns)
      dpiColl.AddRange(GetPettyCashClaimItemDocPrintingEntriesColumns)
      dpiColl.AddRange(GetAdvanceMoneyDocPrintingEntriesColumns)
      dpiColl.AddRange(GetPaymentSelectionDocPrintingEntriesColumns)
      dpiColl.AddRange(GetPADocPrintingEntriesColumns)
      '############################################################################

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("vati_refdoc", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("vati_refdoctype", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.LineNumber", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.RunNumber", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.Code", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.DocDate", "System.DateTime", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.PrintName", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.PrintAddress", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.TaxBase", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.TaxRate", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.VatAmount", "System.String", "VatItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("VatItem.Note", "System.String", "VatItem"))

      Return dpiColl

    End Function
    Private Function GetGLDocPrintingEntriesColumns() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefGLCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefGLDate", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("AccountBook", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("JournalName", "System.String"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.LineNumber", "System.Int32", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.AccountCode", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.Debit", "System.Decimal", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.DebitBaht", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.DebitSatang", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.Amount", "System.Decimal", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.Credit", "System.Decimal", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.CreditBaht", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.CreditSatang", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.AccountName", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.CostCenter", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.Note", "System.String", "Item"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SumCredit", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SumDebit", "System.String"))

      Return dpiColl
    End Function
    Private Function GetAdvancePayDocPrintingEntriesColumns() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("CostCenterInfo", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("CostCenterCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("CostCenterName", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocDate", "System.DateTime"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("WHTAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocBeforeTax", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocTaxAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocAftertax", "System.String"))

      Return dpiColl
    End Function
    Private Function GetGoodsReceiptDocPrintingEntriesColumns() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("POCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PODocDate", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PURCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PURDate", "System.DateTime"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("BICode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("BIDocDate", "System.DateTime"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocTaxAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocBeforeTax", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocDiscountAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocAdvanceMoney", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocRetention", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocAfterTax", "System.String"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Code", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.LineNumber", "System.Int32", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Unit", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Qty", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.UnitPrice", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.DiscountRate", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.DiscountAmount", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Amount", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.ZeroVat", "System.Boolean", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Description", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Note", "System.String", "RefDocItem"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("WHTAmount", "System.String"))

      Return dpiColl
    End Function
    Private Function GetAdvanceMoneyDocPrintingEntriesColumns() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("AdvanceMoneyName", "System.String"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.LineNumber", "System.Int32", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Description", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Amount", "System.String", "RefDocItem"))

      Return dpiColl
    End Function

    Private Function GetPettyCashClaimItemDocPrintingEntriesColumns() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocGross", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefPettyCashCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefPettyCashName", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefPettyCashItemCostCenter", "System.String"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.LineNumber", "System.Int32", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Description", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Amount", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.PaidAmount", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Note", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.CostCenterCode", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.CostCenterName", "System.String", "RefDocItem"))

      Return dpiColl
    End Function
    Private Function GetPaymentSelectionDocPrintingEntriesColumns() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocDate", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PVDocCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PVDocDate", "System.String"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paymenti_payment", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.glnote", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.GLCode", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.refnote", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.VatCodes", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Retention", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.CostCenterCode", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.LineNumber", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Description", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.DescriptionWithNote", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.DescriptionWithNote", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Amount", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.Note", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.DocDate", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.DueDate", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.RealAmount", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.SignedRealAmount", "System.String", "RefDocItem"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocItem.UnpaidAmount", "System.String", "RefDocItem"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocRetention", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefItemCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("BACode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocTaxAmount", "System.Decimal"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocBeforeTax", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocAfterTax", "System.Decimal"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefBillCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("WHTAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("PaidAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("DiscountAmount", "System.String"))

      Return dpiColl
    End Function
    Private Function GetPADocPrintingEntriesColumns() As DocPrintingItemCollection
      Dim dpiColl As New DocPrintingItemCollection
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocTaxAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocBeforeTax", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocDiscountAmount", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocAdvanceMoney", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocRetention", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocAfterTax", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("RefDocWHTAmount", "System.String"))

      Return dpiColl
    End Function

    Public Function GetDocPrintingDataEntries() As DocPrintingItemCollection Implements INewPrintableEntity.GetDocPrintingDataEntries
      Return Me.GetDocPrintingEntries
    End Function
#End Region

    Public ReadOnly Property DocStatus As String Implements IDocStatus.DocStatus
      Get
        If Me.Status.Value = 0 Then
          Return "Canceled"
        Else
          'Dim obj As Object = Configuration.GetConfig("ApprovePR")
          'If CBool(obj) Then
          '  If Me.IsAuthorized Then
          '    Return "Authorized"
          '  ElseIf Me.IsLevelApproved Then
          '    Return "Approved"
          '  End If
          'End If
        End If
        Return ""
      End Get
    End Property
  End Class
  Public Class TaxInfoItem

    Property TaxRate As Decimal
    Property Description As String
    Property BeforeVAT As Decimal

    Property WHT As Decimal

    Property AfterVat As Decimal

  End Class
  Public Class TaxInfo

    Property TaxForm As String
    Property TaxCondition As String
    Private m_items As List(Of TaxInfoItem)
    Public ReadOnly Property TaxInfoItems As List(Of TaxInfoItem)
      Get
        If m_items Is Nothing Then
          m_items = New List(Of TaxInfoItem)
        End If
        Return m_items
      End Get
    End Property

    Property ID As Integer

  End Class
  Public Class PaymentForList

    '=========================================
    Private m_TaxInfos As List(Of TaxInfo)
    Public ReadOnly Property TaxInfos As List(Of TaxInfo)
      Get
        If m_TaxInfos Is Nothing Then
          m_TaxInfos = New List(Of TaxInfo)
        End If
        Return m_TaxInfos
      End Get
    End Property
    Public Property KbankMCBank As String
    Public Property KbankMCAccount As String

    Public Property KbankDCBank As String
    Public Property KbankDCAccount As String

    Public Property PayeeName As String

    Property BankName As String

    Public Property PayeeID As String

    Property PersonalID As String
    Property PayeeTaxID As String
    Property PayeeFax As String

    Property DeliveryMethod As String = "CR"
    Property PickupLocation As String = "16"
    Property PickupDocument As String = "R"
    Property AttachmentSubfile As String = ""
    '=========================================

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
      If obj Is Nothing Then
        Return False
      End If
      Return CType(obj, PaymentForList).Id = Me.Id AndAlso CType(obj, PaymentForList).RefCode = Me.RefCode AndAlso CType(obj, PaymentForList).RefTypeId = Me.RefTypeId
      'Return CType
    End Function
    Public Property Id As Integer
    Public Property Selected As Boolean
    Public Property SelectedForDeleted As Boolean
    Public Property Code As String
    Public Property RefId As Integer
    Public Property RefCode As String
    Public Property RefType As String
    Public Property RefTypeId As Integer
    Public Property RefDocDate As Date
    Public Property RefDueDate As Date
    Public Property bankacct As String
    Public Property RefCreditPeriod As Integer
    Public Property RefAmount As Decimal
    Public Property RefPaid As Decimal
    Public Property JustAdded As Boolean = False
    Public Property Note As String
    Public ReadOnly Property RefRemain As Decimal
      Get
        Return RefAmount - RefPaid
      End Get
    End Property
    Public Property Amount As Decimal




    Public Shared Function GetPaymentList(ByVal filters As Filter()) As List(Of PaymentForList)
      Dim params() As SqlParameter
      If Not filters Is Nothing AndAlso filters.Length > 0 Then
        ReDim params(filters.Length - 1)
        For i As Integer = 0 To filters.Length - 1
          params(i) = New SqlParameter("@" & filters(i).Name, filters(i).Value)
        Next
      End If
      Dim sqlConString As String = RecentCompanies.CurrentCompany.ConnectionString
      Dim ds As DataSet = SqlHelper.ExecuteDataset(sqlConString _
      , CommandType.StoredProcedure _
      , "GetPaymentForList" _
      , params _
      )
      Dim ret As New List(Of PaymentForList)

      For Each row As DataRow In ds.Tables(0).Rows
        Dim deh As New DataRowHelper(row)
        Dim p As New PaymentForList
        p.Id = deh.GetValue(Of Integer)("Id")
        p.Code = deh.GetValue(Of String)("Code")
        p.RefId = deh.GetValue(Of Integer)("RefId")
        p.RefCode = deh.GetValue(Of String)("RefCode")
        p.RefType = deh.GetValue(Of String)("RefType")
        p.RefTypeId = deh.GetValue(Of Integer)("RefTypeId")
        p.RefDocDate = deh.GetValue(Of Date)("RefDocDate")
        p.RefDueDate = deh.GetValue(Of Date)("RefDueDate")
        p.bankacct = deh.GetValue(Of String)("bankaccount")
        p.RefAmount = deh.GetValue(Of Decimal)("RefAmount")
        p.RefPaid = deh.GetValue(Of Decimal)("RefPaid")
        p.Note = ""
        ret.Add(p)
      Next
      Return ret
    End Function

    Public Shared Function GetPCCList(ByVal filters As Filter()) As List(Of PaymentForList)
      Dim params() As SqlParameter
      If Not filters Is Nothing AndAlso filters.Length > 0 Then
        ReDim params(filters.Length - 1)
        For i As Integer = 0 To filters.Length - 1
          params(i) = New SqlParameter("@" & filters(i).Name, filters(i).Value)
        Next
      End If
      Dim sqlConString As String = RecentCompanies.CurrentCompany.ConnectionString
      Dim ds As DataSet = SqlHelper.ExecuteDataset(sqlConString _
      , CommandType.StoredProcedure _
      , "GetPCCForList" _
      , params _
      )
      Dim ret As New List(Of PaymentForList)

      For Each row As DataRow In ds.Tables(0).Rows
        Dim deh As New DataRowHelper(row)
        Dim p As New PaymentForList
        p.Id = deh.GetValue(Of Integer)("Id")
        p.Code = deh.GetValue(Of String)("Code")
        p.RefId = deh.GetValue(Of Integer)("RefId")
        p.RefCode = deh.GetValue(Of String)("RefCode")
        p.RefType = deh.GetValue(Of String)("RefType")
        p.RefTypeId = deh.GetValue(Of Integer)("RefTypeId")
        p.RefDocDate = deh.GetValue(Of Date)("RefDocDate")
        p.RefDueDate = deh.GetValue(Of Date)("RefDueDate")
        p.RefAmount = deh.GetValue(Of Decimal)("RefAmount")
        p.RefPaid = deh.GetValue(Of Decimal)("RefPaid")
        p.Note = ""
        ret.Add(p)
      Next
      Return ret
    End Function

  End Class
  Public Class PaymentEntityType
    Inherits CodeDescription

#Region "Constructors"
    Public Sub New(ByVal value As Integer)
      MyBase.New(value)
    End Sub
#End Region

#Region "Properties"
    Public Overrides ReadOnly Property CodeName() As String
      Get
        Return "paymenti_entityType"
      End Get
    End Property
#End Region

  End Class
  Public Class PaymentItem

#Region "Members"
    Private m_payment As Payment
    Private m_lineNumber As Integer
    Private m_entity As IPaymentItem
    Private m_entityType As PaymentEntityType

    Private m_amount As Decimal  '�Թ�ӹǹ����ͧ���¨�ԧ
    Private m_note As String
    Private m_limit As Decimal   '�Թ���������
#End Region

#Region "Constructors"
    Public Sub New()
      MyBase.New()
      m_limit = 0
    End Sub
    Public Sub New(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Me.Construct(ds, aliasPrefix)
    End Sub
    Public Sub New(ByVal dr As DataRow, ByVal aliasPrefix As String)
      Me.Construct(dr, aliasPrefix)
    End Sub
    Protected Sub Construct(ByVal dr As DataRow, ByVal aliasPrefix As String)
      With Me
        If dr.Table.Columns.Contains(aliasPrefix & "paymenti_entityType") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_entityType") Then
          .m_entityType = New PaymentEntityType(CInt(dr(aliasPrefix & "paymenti_entityType")))
        End If
        Dim itemId As Integer
        If dr.Table.Columns.Contains(aliasPrefix & "paymenti_entity") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_entity") Then
          itemId = CInt(dr(aliasPrefix & "paymenti_entity"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "paymenti_payment") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_payment") Then
          .m_payment = New Payment
          .m_payment.Id = CInt(dr(aliasPrefix & "paymenti_payment"))
        End If
        Select Case .m_entityType.Value
          Case 0      'Cash
            If dr.Table.Columns.Contains(aliasPrefix & "paymenti_amt") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_amt") Then
              Dim cash As New CashItem(CDec(dr(aliasPrefix & "paymenti_amt")))
              If dr.Table.Columns.Contains(aliasPrefix & "paymenti_duedate") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_duedate") Then
                cash.DocDate = CDate(dr(aliasPrefix & "paymenti_duedate"))
              End If
              .m_entity = cash
            End If
            'Case 65     'Transfer
            '  If dr.Table.Columns.Contains(aliasPrefix & "paymenti_amt") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_amt") Then
            '    Dim bto As New BankTransferOut()
            '    bto.Amount = CDec(dr(aliasPrefix & "paymenti_amt"))
            '    If dr.Table.Columns.Contains(aliasPrefix & "paymenti_bankacct") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_bankacct") Then
            '      bto.BankAccount = New BankAccount(CInt(dr(aliasPrefix & "paymenti_bankacct")))
            '    End If
            '    If dr.Table.Columns.Contains(aliasPrefix & "paymenti_duedate") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_duedate") Then
            '      bto.DocDate = CDate(dr(aliasPrefix & "paymenti_duedate"))
            '    End If
            '    .m_entity = bto
            '  End If
          Case Else
            Dim entityTypeId As Integer = .m_entityType.Value
            Dim myEntity As SimpleBusinessEntityBase = SimpleBusinessEntityBase.GetEntity(BusinessLogic.Entity.GetFullClassName(entityTypeId), itemId)
            If TypeOf myEntity Is IPaymentItem Then
              .m_entity = CType(myEntity, IPaymentItem)
              If TypeOf m_entity Is IHasBankAccount Then
                If m_entity.Id = 0 Then
                  If dr.Table.Columns.Contains(aliasPrefix & "paymenti_bankacct") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_bankacct") Then
                    CType(m_entity, IHasBankAccount).BankAccount = New BankAccount(CInt(dr(aliasPrefix & "paymenti_bankacct")))
                  End If
                End If
              End If
              If TypeOf m_entity Is IPaymentItem Then
                Dim pi As IPaymentItem = CType(m_entity, IPaymentItem)
                If m_entity.Id = 0 Then
                  If dr.Table.Columns.Contains(aliasPrefix & "paymenti_refamt") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_refamt") Then
                    pi.Amount = CDec(dr(aliasPrefix & "paymenti_refamt"))
                  End If
                  If dr.Table.Columns.Contains(aliasPrefix & "paymenti_duedate") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_duedate") Then
                    pi.DueDate = CDate(dr(aliasPrefix & "paymenti_duedate"))
                  End If
                  If dr.Table.Columns.Contains(aliasPrefix & "paymenti_entitycode") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_entitycode") Then
                    pi.Code = CStr(dr(aliasPrefix & "paymenti_entitycode"))
                  End If
                End If
                If TypeOf pi Is BankTransferOut Then
                  If dr.Table.Columns.Contains(aliasPrefix & "paymenti_duedate") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_duedate") Then
                    pi.DueDate = CDate(dr(aliasPrefix & "paymenti_duedate"))
                  End If
                End If
              End If
            End If

            If TypeOf myEntity Is PettyCash Then
              Dim pc As PettyCash = CType(myEntity, PettyCash)
              If dr.Table.Columns.Contains(aliasPrefix & "paymenti_amt") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_amt") Then
                If Not pc.LimitedOverBudget And pc.AllowOverBudget Then
                  .m_limit = -1
                ElseIf pc.LimitedOverBudget And Not pc.AllowOverBudget Then
                  .m_limit = pc.GetRemainingAmount(Me.Payment.Id) + pc.LimitedOverBudgetAmount
                Else
                  .m_limit = pc.GetRemainingAmount(Me.Payment.Id)
                End If
              End If
              'If Not pc.AllowOverBudget Then
              '  m_limit = 0
              'ElseIf pc.LimitedOverBudget Then
              '  m_limit = pc.LimitedOverBudgetAmount
              'Else
              '  m_limit = -1
              'End If
            End If
        End Select

        If dr.Table.Columns.Contains(aliasPrefix & "paymenti_lineNumber") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_lineNumber") Then
          .m_lineNumber = CInt(dr(aliasPrefix & "paymenti_lineNumber"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "paymenti_amt") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_amt") Then
          .m_amount = CDec(dr(aliasPrefix & "paymenti_amt"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "paymenti_note") AndAlso Not dr.IsNull(aliasPrefix & "paymenti_note") Then
          .m_note = CStr(dr(aliasPrefix & "paymenti_note"))
        End If
      End With
    End Sub
    Protected Sub Construct(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Dim dr As DataRow = ds.Tables(0).Rows(0)
      Me.Construct(dr, aliasPrefix)
    End Sub
#End Region

#Region "Properties"
    Public Property oldEntityId As Integer
    Public Property Payment() As Payment
      Get
        Return m_payment
      End Get
      Set(ByVal Value As Payment)
        m_payment = Value
      End Set
    End Property
    Public Property LineNumber() As Integer
      Get
        Return m_lineNumber
      End Get
      Set(ByVal Value As Integer)
        m_lineNumber = Value
      End Set
    End Property
    Public Property Entity() As IPaymentItem
      Get
        Return m_entity
      End Get
      Set(ByVal Value As IPaymentItem)
        Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
        Dim oldAmount As Decimal = Me.Amount
        Dim parentAmount As Decimal = Me.Payment.Amount
        Dim parentGross As Decimal = Me.Payment.Gross
        Select Case Me.EntityType.Value
          Case 22     '�礨���
            Dim check As OutgoingCheck = CType(Value, OutgoingCheck)
            If check.Originated Then
              If Not TypeOf Me.Payment.RefDoc Is PettyCashClaim AndAlso check.Supplier.Id <> Me.Payment.RefDoc.Recipient.Id Then
                msgServ.ShowMessageFormatted("${res:Global.Error.CheckIssueToOther}", New String() {Value.Code, Me.Payment.RefDoc.Recipient.Name})
                Return
              End If
              If check.DocStatus.Value = 0 Then
                msgServ.ShowMessageFormatted("${res:Global.Error.CheckIsCanceled}", New String() {Value.Code})
                Return
              End If
              Dim remain As Decimal = check.GetRemainingAmount(Me.Payment.Id)
              If remain <= 0 Then
                msgServ.ShowMessageFormatted("${res:Global.Error.ZeroOrLessCheckAmount}", New String() {Value.Code})
                Return
              End If
              If DupCode(Value.Code) Then
                msgServ.ShowMessageFormatted("${res:Global.Error.AlreadyHasCode}", New String() {Me.EntityType.Description, Value.Code})
                Return
              End If
              Me.m_amount = Configuration.Format(Math.Max(Math.Min(parentAmount - parentGross + oldAmount, remain), 0), DigitConfig.Price)
            End If
          Case 36     '�Թʴ����
            If Not TypeOf Me.Payment.RefDoc Is PettyCashClaim AndAlso Not TypeOf Me.Payment.RefDoc Is PettyCash Then
              Dim ptc As PettyCash = CType(Value, PettyCash)
              If ptc.Originated Then
                If ptc.Status.Value = 0 Then
                  msgServ.ShowMessageFormatted("${res:Global.Error.PettyCashIsCanceled}", New String() {Value.Code})
                  Return
                End If
                Dim remain As Decimal = ptc.GetRemainingAmount(Me.Payment.Id)
                Dim limit As Decimal = 0
                'If ptc.LimitedOverBudget Then
                '  limit = ptc.LimitedOverBudgetAmount
                'ElseIf Not ptc.AllowOverBudget Then
                '  limit = 0
                'Else
                '  limit = -1
                'End If
                If Not ptc.LimitedOverBudget AndAlso ptc.AllowOverBudget Then
                  limit = -1
                  Me.m_amount = Configuration.Format((parentAmount - parentGross + oldAmount), DigitConfig.Price)
                Else
                  limit = remain
                  If remain <= 0 Then
                    'If remain <= 0 And limit = 0 Then
                    msgServ.ShowMessageFormatted("${res:Global.Error.ZeroOrLessPettyCashAmount}", New String() {Value.Code})
                    Return
                  End If
                  Me.m_amount = Configuration.Format(Math.Max(Math.Min(parentAmount - parentGross + oldAmount, remain), 0), DigitConfig.Price)
                End If

                Me.Limit = limit

              End If
            End If
          Case 59     '�Ѵ�Ө���
            Dim avp As AdvancePay = CType(Value, AdvancePay)
            If avp.Originated Then
              If avp.Status.Value = 0 Then
                msgServ.ShowMessageFormatted("${res:Global.Error.AdvanePayIsCanceled}", New String() {Value.Code})
                Return
              End If
              Dim remain As Decimal = avp.GetRemainingAmount(Me.Payment.Id)
              If remain <= 0 Then
                msgServ.ShowMessageFormatted("${res:Global.Error.ZeroOrLessAdvanePayAmount}", New String() {Value.Code})
                Return
              End If
              Me.m_amount = Configuration.Format(Math.Max(Math.Min(parentAmount - parentGross + oldAmount, remain), 0), DigitConfig.Price)
            End If
          Case 174      '�Թ���ͧ����
            Dim advm As AdvanceMoney = CType(Value, AdvanceMoney)
            If advm.Originated Then
              If advm.Status.Value = 0 Then
                msgServ.ShowMessageFormatted("${res:Global.Error.AdvanceMoneyIsCanceled}", New String() {Value.Code})
                Return
              End If
              If advm.Closed Then
                msgServ.ShowMessageFormatted("${res:Global.Error.AdvanceMoneyIsClosed}", New String() {Value.Code})
                Return
              End If
              Dim remain As Decimal = advm.GetRemainingAmount(Me.Payment.Id)
              If remain <= 0 Then
                msgServ.ShowMessageFormatted("${res:Global.Error.ZeroOrLessAdvanceMoneyAmount}", New String() {Value.Code})
                Return
              End If
              Me.m_amount = Configuration.Format(Math.Max(Math.Min(parentAmount - parentGross + oldAmount, remain), 0), DigitConfig.Price)
            End If
        End Select

        m_entity = Value
        SetRefDocGLChange()
      End Set
    End Property
    ''' <summary>
    ''' ����¹�ŧ GL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRefDocGLChange()
      If Not m_payment Is Nothing Then
        If TypeOf m_payment.RefDoc Is SimpleBusinessEntityBase Then
          CType(m_payment.RefDoc, SimpleBusinessEntityBase).OnGlChanged()
        End If
      End If
    End Sub
    Private Function HasCash() As Boolean
      For Each item As PaymentItem In Me.Payment.ItemCollection
        If Not item Is Me Then
          If Not item.EntityType Is Nothing AndAlso item.EntityType.Value = 0 Then
            Return True
          End If
        End If
      Next
      Return False
    End Function
    Public Property EntityType() As PaymentEntityType
      Get
        Return m_entityType
      End Get
      Set(ByVal Value As PaymentEntityType)
        Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
        Dim oldAmount As Decimal = Me.Amount
        Dim parentAmount As Decimal = Me.Payment.Amount
        Dim parentGross As Decimal = Me.Payment.Gross

        Dim amt As Decimal = Configuration.Format(Math.Max(parentAmount - parentGross + oldAmount, 0), DigitConfig.Price)

        If Not Value Is Nothing AndAlso Value.Value = 0 Then
          If HasCash() Then
            msgServ.ShowMessage("${res:Global.Error.AlreadyHasCash}")
            Return
          End If
        End If

        If Not Me.m_entityType Is Nothing _
        AndAlso Not Value Is Nothing _
        AndAlso m_entityType.Value = Value.Value Then
          '��ҹ�Ŵ
          Return
        End If

        If (Me.m_entityType Is Nothing AndAlso Not Value Is Nothing) _
        OrElse msgServ.AskQuestion("${res:Global.Question.ChangePaymentEntityType}") Then
          Select Case Value.Value
            Case 0       '�Թʴ
              Me.m_entity = New CashItem(amt)
              Me.Entity.DueDate = Me.Payment.RefDoc.Date
              Me.m_amount = amt
            Case 65      '�͹
              Me.m_entity = New BankTransferOut()
              Me.m_entity.Amount = amt
              Me.Entity.DueDate = Me.Payment.RefDoc.Date
              Me.m_amount = amt
            Case 22      '��
              Dim check As New OutgoingCheck
              Me.m_entity = check
              If CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
                Me.Entity.Amount = amt
                Me.m_amount = amt
              Else
                Me.m_amount = 0
              End If
            Case 336      '��
              Dim check As New OutgoingAval
              Me.m_entity = check
              If CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
                Me.Entity.Amount = amt
                Me.m_amount = amt
              Else
                Me.m_amount = 0
              End If
            Case 59      '�Ѵ��
              Me.m_entity = New AdvancePay
              Me.m_amount = 0
            Case 36      '�Թʴ����
              Me.m_entity = New PettyCash
              Me.m_amount = 0
            Case 174       '�Թ���ͧ����
              Me.m_entity = New AdvanceMoney
              Me.m_amount = 0
          End Select
        Else
          Return
        End If
        m_entityType = Value
        SetRefDocGLChange()
      End Set
    End Property
    Public Property DueDate() As Date
      Get
        If Me.Entity Is Nothing Then
          Return Date.MinValue
        End If
        Return Me.Entity.DueDate
      End Get
      Set(ByVal Value As Date)
        If Me.Entity Is Nothing Then
          Return
        End If
        Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
        If Me.EntityType Is Nothing Then
          '����� Type
          msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
          Return
        End If
        Select Case EntityType.Value
          Case 0      'ʴ
            '��ҹ
          Case 22     '�礨���
            Dim check As OutgoingCheck = CType(Me.Entity, OutgoingCheck)
            If Not check Is Nothing AndAlso check.Originated Then
              msgServ.ShowMessage("${res:Global.Error.CannotChangeOldCheckDate}")
              Return
            End If
          Case 336     '�����
            Dim aval As OutgoingAval = CType(Me.Entity, OutgoingAval)
            If Not aval Is Nothing AndAlso aval.Originated Then
              msgServ.ShowMessage("${res:Global.Error.CannotChangeOldCheckDate}")
              Return
            End If
          Case 36     '�Թʴ����
            msgServ.ShowMessage("${res:Global.Error.CannotChangePettyCashDate}")
            Return
          Case 59     '�Ѵ��
            msgServ.ShowMessage("${res:Global.Error.CannotChangeAdvancePayDate}")
            Return
          Case 174      '���ͧ����
            msgServ.ShowMessage("${res:Global.Error.CannotChangeAdvanceMoneyDate}")
            Return
          Case 65     '�͹
            '��ҹ
          Case Else
            msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
            Return
        End Select
        Me.Entity.DueDate = Value
      End Set
    End Property
    Public Property Amount() As Decimal
      Get
        If Me.Entity Is Nothing Then
          Return 0
        End If
        Return m_amount
      End Get
      Set(ByVal Value As Decimal)
        If Me.Entity Is Nothing Then
          Return
        End If
        Value = Configuration.Format(Value, DigitConfig.Price)
        Dim oldAmount As Decimal = Me.Amount
        Dim parentAmount As Decimal = Me.Payment.Amount
        Dim parentGross As Decimal = Me.Payment.Gross
        Dim oldRealAmount As Decimal = Me.RealAmount
        Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
        If Me.EntityType Is Nothing Then
          '����� Type
          msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
          Return
        End If
        Select Case Me.EntityType.Value
          Case 0      'ʴ
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            Else
              Me.Entity.Amount = Value
            End If
          Case 22    '�礨���
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            Else
              Dim check As OutgoingCheck = CType(Me.Entity, OutgoingCheck)
              Dim remain As Decimal = check.GetRemainingAmount(Me.Payment.Id)
              '��Ǩ�ͺ��� remain ᷹
              If Configuration.Compare(CDec(IIf(remain <= 0, oldRealAmount, remain)), Value) < 0 Then
                msgServ.ShowMessage("${res:Global.Error.RealAmountLessThanAmount}")
                Return
              End If
            End If
          Case 336    '�礨���
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            Else
              Dim aval As OutgoingAval = CType(Me.Entity, OutgoingAval)
              Dim remain As Decimal = aval.GetRemainingAmount(Me.Payment.Id)
              '��Ǩ�ͺ��� remain ᷹
              If Configuration.Compare(CDec(IIf(remain <= 0, oldRealAmount, remain)), Value) < 0 Then
                msgServ.ShowMessage("${res:Global.Error.RealAmountLessThanAmount}")
                Return
              End If
            End If
          Case 36     '�Թʴ����
            If Not TypeOf Me.Payment.RefDoc Is PettyCash Then
              Dim limit As Decimal
              Dim pt As PettyCash = CType(Me.Entity, PettyCash)
              limit = pt.LimitedOverBudgetAmount + pt.Amount  'CDec(IIf(IsNumeric(CType(Me.Entity, PettyCash).LimitedOverBudgetAmount), CType(Me.Entity, PettyCash).LimitedOverBudgetAmount, CType(Me.Entity, PettyCash).Amount))
              If Not (CType(Me.Entity, PettyCash).AllowOverBudget) Then
                If Configuration.Compare(limit, (parentGross + Value - oldAmount)) < 0 Then
                  msgServ.ShowMessageFormatted("${res:Global.Error.PaysRemainingAmountLessThanAmount}", _
                  New String() {Configuration.FormatToString(limit, DigitConfig.Price), _
                  Configuration.FormatToString((parentGross + Value - oldAmount), DigitConfig.Price)})
                  Return
                End If
              End If
            End If
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              MessageBox.Show(String.Format("{0}, ({1} + {2} - {3})", parentAmount, parentGross, Value, oldAmount))
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            ElseIf Limit <> -1 Then
              If Configuration.Compare(oldRealAmount + Limit, Value) < 0 Then
                MessageBox.Show(String.Format("{0} + {1}, {2}", oldRealAmount, Limit, Value))
                msgServ.ShowMessage("${res:Global.Error.RealAmountLessThanAmount}")
                Return
              End If
            End If
          Case 59     '�Ѵ��
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            Else
              If Configuration.Compare(oldRealAmount, Value) < 0 Then
                msgServ.ShowMessage("${res:Global.Error.RealAmountLessThanAmount}")
                Return
              End If
            End If
          Case 174      '���ͧ����
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            Else
              If Configuration.Compare(oldRealAmount, Value) < 0 Then
                msgServ.ShowMessage("${res:Global.Error.RealAmountLessThanAmount}")
                Return
              End If
            End If
          Case 65     '�͹
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            Else
              Me.Entity.Amount = Value
            End If
          Case Else
            msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
            Return
        End Select
        m_amount = Value
        SetRefDocGLChange()
      End Set
    End Property
    Public Property RealAmount() As Decimal
      Get
        If Me.Entity Is Nothing Then
          Return 0
        End If
        Return Me.Entity.Amount
      End Get
      Set(ByVal Value As Decimal)
        If Me.Entity Is Nothing Then
          Return
        End If

        Value = Configuration.Format(Value, DigitConfig.Price)
        Dim oldAmount As Decimal = Me.Amount
        Dim parentAmount As Decimal = Me.Payment.Amount
        Dim parentGross As Decimal = Me.Payment.Gross
        Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
        If Me.EntityType Is Nothing Then
          '����� Type
          msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
          Return
        End If
        Select Case Me.EntityType.Value
          Case 0      'ʴ
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            Else
              Me.m_amount = Value
            End If
          Case 22     '�礨���
            Dim check As OutgoingCheck = CType(Me.Entity, OutgoingCheck)
            If Not check Is Nothing AndAlso check.Originated Then
              msgServ.ShowMessage("${res:Global.Error.CannotChangeOldCheckAmount}")
              Return
            Else
              If Configuration.Compare(Value, oldAmount) < 0 Then
                msgServ.ShowMessage("${res:Global.Error.RealAmountLessThanAmount}")
                Return
              End If
            End If
          Case 36     '�Թʴ����
            msgServ.ShowMessage("${res:Global.Error.CannotChangePettyCashAmount}")
            Return
          Case 59     '�Ѵ��
            msgServ.ShowMessage("${res:Global.Error.CannotChangeAdvancePayAmount}")
            Return
          Case 174      '���ͧ����
            msgServ.ShowMessage("${res:Global.Error.CannotChangeAdvanceMoneyAmount}")
            Return
          Case 65     '�͹
            If Configuration.Compare(parentAmount, (parentGross + Value - oldAmount)) < 0 Then
              msgServ.ShowMessage("${res:Global.Error.AmountExceedPayingAmount}")
              Return
            Else
              Me.m_amount = Value
            End If
          Case Else
            msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
            Return
        End Select
        Me.Entity.Amount = Value
        SetRefDocGLChange()
      End Set
    End Property
    Public Property Note() As String
      Get
        Return m_note
      End Get
      Set(ByVal Value As String)
        m_note = Value
      End Set
    End Property
    Public Property Limit() As Decimal
      Get
        Return m_limit
      End Get
      Set(ByVal Value As Decimal)
        m_limit = Value
      End Set
    End Property
    Private Function DupCode(ByVal theCode As String) As Boolean
      If Me.EntityType Is Nothing Then
        Return False
      End If
      If theCode Is Nothing OrElse theCode.Length = 0 Then
        Return False
      End If
      For Each item As PaymentItem In Me.Payment.ItemCollection
        If Not item Is Me Then
          If item.EntityType Is Nothing Then
            If item.EntityType.Value = Me.EntityType.Value Then
              If theCode.ToLower = item.Entity.Code.ToLower Then
                Return True
              End If
            End If
          End If
        End If
      Next
      Return False
    End Function
    Public Sub SetItemCode(ByVal theCode As String)
      Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      Dim oldAmount As Decimal = Me.Amount
      Dim parentAmount As Decimal = Me.Payment.Amount
      Dim parentGross As Decimal = Me.Payment.Gross
      If Me.EntityType Is Nothing Then
        '����� Type
        msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
        Return
      End If
      If DupCode(theCode) Then
        msgServ.ShowMessageFormatted("${res:Global.Error.AlreadyHasCode}", New String() {Me.EntityType.Description, theCode})
        Return
      End If
      Select Case Me.EntityType.Value
        Case 0     'ʴ
          msgServ.ShowMessage("${res:Global.Error.CashCannotHaveCode}")
          Return
        Case 22    '�礨���
          If theCode Is Nothing OrElse theCode.Length = 0 Then
            If Me.Entity.Code.Length <> 0 Then
              If msgServ.AskQuestionFormatted("${res:Global.Question.DeleteOutGoingCheckDetail}", New String() {Me.Entity.Code}) Then
                Me.Clear()
              End If
            End If
            Return
          End If
          Dim checkInstant As New OutgoingCheck(theCode)
          Dim check As New OutgoingCheck
          If Not checkInstant.Originated Then
            If msgServ.AskQuestionFormatted("${res:Global.Question.CreateNewOutGoingCheck}", New String() {theCode}) Then
              check.CqCode = theCode
              check.Amount = Configuration.Format(Math.Max(parentAmount - parentGross + oldAmount, 0), DigitConfig.Price)
              check.Bankacct = New BankAccount
              If Not CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
                check.DueDate = Me.Payment.RefDoc.Date
                Dim o As Object = Configuration.GetConfig("CheckDateFromWHT")
                If Not o Is Nothing AndAlso CBool(o) Then
                  'CheckDateFromWHT
                  If TypeOf Me.Payment.RefDoc Is IWitholdingTaxable Then
                    Dim whtref As IWitholdingTaxable = CType(Me.Payment.RefDoc, IWitholdingTaxable)
                    If whtref.WitholdingTaxCollection.Count >= 1 Then
                      check.DueDate = whtref.WitholdingTaxCollection(0).DocDate
                    End If
                  End If
                End If
              End If
            Else
              Return
            End If
            Me.Entity = check
          Else
            checkInstant.Amount = Configuration.Format(Math.Max(parentAmount - parentGross + oldAmount, 0), DigitConfig.Price)
            Me.Entity = checkInstant
          End If

          'Case 336    'Aval
          'If theCode Is Nothing OrElse theCode.Length = 0 Then
          'If Me.Entity.Code.Length <> 0 Then
          'If msgServ.AskQuestionFormatted("${res:Global.Question.DeleteOutGoingCheckDetail}", New String() {Me.Entity.Code}) Then
          'Me.Clear()
          'End If
          'End If
          'Return
          'End If
          'Dim AvalInstant As New OutgoingAval(theCode)
          'Dim aval As New OutgoingAval
          'If Not AvalInstant.Originated Then
          'If msgServ.AskQuestionFormatted("${res:Global.Question.CreateNewOutGoingCheck}", New String() {theCode}) Then
          'aval.CqCode = theCode
          'aval.Amount = Configuration.Format(Math.Max(parentAmount - parentGross + oldAmount, 0), DigitConfig.Price)
          'aval.Bankacct = New BankAccount
          'If Not CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
          'aval.DueDate = Me.Payment.RefDoc.Date
          'Dim o As Object = Configuration.GetConfig("CheckDateFromWHT")
          'If Not o Is Nothing AndAlso CBool(o) Then
          ''CheckDateFromWHT
          'If TypeOf Me.Payment.RefDoc Is IWitholdingTaxable Then
          'Dim whtref As IWitholdingTaxable = CType(Me.Payment.RefDoc, IWitholdingTaxable)
          'If whtref.WitholdingTaxCollection.Count >= 1 Then
          'aval.DueDate = whtref.WitholdingTaxCollection(0).DocDate
          'End If
          'End If
          'End If
          'End If
          'Else
          'Return
          'End If
          'End If
          'Me.Entity = aval
        Case 36    '�Թʴ����
          If theCode Is Nothing OrElse theCode.Length = 0 Then
            If Me.Entity.Code.Length <> 0 Then
              If msgServ.AskQuestionFormatted("${res:Global.Question.DeletePettyCashDetail}", New String() {Me.Entity.Code}) Then
                Me.Clear()
              End If
            End If
            Return
          End If
          Dim ptc As New PettyCash(theCode)
          If Not ptc.Originated Then
            msgServ.ShowMessageFormatted("${res:Global.Error.NoPettyCash}", New String() {theCode})
            Return
          Else
            Me.Entity = ptc
          End If
        Case 59    '�Ѵ��
          If theCode Is Nothing OrElse theCode.Length = 0 Then
            If Me.Entity.Code.Length <> 0 Then
              If msgServ.AskQuestionFormatted("${res:Global.Question.DeleteAdvancePayDetail}", New String() {Me.Entity.Code}) Then
                Me.Clear()
              End If
            End If
            Return
          End If
          Dim avp As New AdvancePay(theCode)
          If Not avp.Originated Then
            msgServ.ShowMessageFormatted("${res:Global.Error.NoAdvancePay}", New String() {theCode})
            Return
          Else
            Me.Entity = avp
          End If
        Case 65    '�͹
          msgServ.ShowMessage("${res:Global.Error.BankTransferOutCannotHaveCode}")
          Return
        Case 174     '�Թ���ͧ����
          If theCode Is Nothing OrElse theCode.Length = 0 Then
            If Me.Entity.Code.Length <> 0 Then
              If msgServ.AskQuestionFormatted("${res:Global.Question.DeleteAdvanceMoneyDetail}", New String() {Me.Entity.Code}) Then
                Me.Clear()
              End If
            End If
            Return
          End If
          Dim advm As New AdvanceMoney(theCode)
          If Not advm.Originated Then
            msgServ.ShowMessageFormatted("${res:Global.Error.NoAdvanceMoney}", New String() {theCode})
            Return
          Else
            Me.Entity = advm
          End If
        Case Else
          msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
          Return
      End Select
      SetRefDocGLChange()
    End Sub
    Public Sub SetBankAccount(ByVal theCode As String)
      Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      If Me.EntityType Is Nothing Then
        '����� Type
        msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
        Return
      End If
      Select Case Me.EntityType.Value
        Case 0     'ʴ
          msgServ.ShowMessage("${res:Global.Error.CashCannotHaveBankAccount}")
          Return
        Case 22    '�礨���
          Dim check As OutgoingCheck = CType(Me.Entity, OutgoingCheck)
          If Not check Is Nothing AndAlso check.Originated Then
            msgServ.ShowMessage("${res:Global.Error.CannotChangeOldCheckBankAccount}")
            Return
          Else
            Dim ba As New BankAccount(theCode)
            If ba.Originated Then
              check.Bankacct = ba
            Else
              msgServ.ShowMessageFormatted("${res:Global.Error.BankAccountNotFound}", New String() {theCode})
              Return
            End If
          End If
        Case 36    '�Թʴ����
          msgServ.ShowMessage("${res:Global.Error.PettyCashCannotHaveBankAccount}")
          Return
        Case 59    '�Ѵ��
          msgServ.ShowMessage("${res:Global.Error.AdvancePayCannotHaveBankAccount}")
          Return
        Case 174     '���ͧ����
          msgServ.ShowMessage("${res:Global.Error.AdvanceMoneyCannotHaveBankAccount}")
          Return
        Case 65    '�͹
          Dim bto As BankTransferOut = CType(Me.Entity, BankTransferOut)
          Dim ba As New BankAccount(theCode)
          If ba.Originated Then
            bto.BankAccount = ba
          Else
            msgServ.ShowMessageFormatted("${res:Global.Error.BankAccountNotFound}", New String() {theCode})
            Return
          End If
        Case Else
          msgServ.ShowMessage("${res:Global.Error.NoPaymentType}")
          Return
      End Select
    End Sub
#End Region

#Region "Methods"
    Public Sub Clear()
      Me.m_entity = Nothing
      Me.RealAmount = 0
      Me.Amount = 0
      Me.DueDate = Date.MinValue
    End Sub
    Public Sub ItemValidateRow(ByVal row As DataRow)
      Dim code As Object = row("code")
      Dim paymenti_entitytype As Object = row("paymenti_entitytype")
      Dim bacode As Object = row("bacode")
      Dim duedate As Object = row("duedate")
      Dim realamount As Object = row("realamount")
      Dim paymenti_amt As Object = row("paymenti_amt")

      Dim isBlankRow As Boolean = False
      If IsDBNull(paymenti_entitytype) Then
        isBlankRow = True
      End If

      Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
      If Not isBlankRow Then
        Select Case CInt(paymenti_entitytype)
          Case 0      'ʴ
            If IsDBNull(duedate) OrElse CDate(duedate).Equals(Date.MinValue) Then
              row.SetColumnError("duedate", myStringParserService.Parse("${res:Global.Error.DateMissing}"))
            Else
              row.SetColumnError("duedate", "")
            End If
            If Not IsNumeric(paymenti_amt) OrElse CDec(paymenti_amt) <= 0 Then
              row.SetColumnError("paymenti_amt", myStringParserService.Parse("${res:Global.Error.PayAmountMissing}"))
            Else
              row.SetColumnError("paymenti_amt", "")
            End If
            row.SetColumnError("code", "")
            row.SetColumnError("bacode", "")
            row.SetColumnError("realamount", "")
          Case 22     '�礨���
            If (IsDBNull(code) OrElse code.ToString.Length = 0) And (Not CBool(Configuration.GetConfig("AllowNoCqCodeDate"))) Then       ' OrElse CreateNewEmptyCqCode = False
              row.SetColumnError("code", myStringParserService.Parse("${res:Global.Error.CheckCodeMissing}"))
            Else
              row.SetColumnError("code", "")
            End If
            If (IsDBNull(duedate) OrElse CDate(duedate).Equals(Date.MinValue)) And Not CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
              row.SetColumnError("duedate", myStringParserService.Parse("${res:Global.Error.DateMissing}"))
            Else
              row.SetColumnError("duedate", "")
            End If
            If Not IsNumeric(realamount) OrElse CDec(realamount) <= 0 Then
              row.SetColumnError("realamount", myStringParserService.Parse("${res:Global.Error.RealAmountMissing}"))
            Else
              row.SetColumnError("realamount", "")
            End If
            If Not IsNumeric(paymenti_amt) OrElse CDec(paymenti_amt) <= 0 Then
              row.SetColumnError("paymenti_amt", myStringParserService.Parse("${res:Global.Error.PayAmountMissing}"))
            Else
              row.SetColumnError("paymenti_amt", "")
            End If
            If IsDBNull(bacode) OrElse bacode.ToString.Length = 0 Then
              'row.SetColumnError("bacode", myStringParserService.Parse("${res:Global.Error.BACodeMissing}"))
              row.SetColumnError("bacode", "")
            Else
              row.SetColumnError("bacode", "")
            End If
          Case 336     'Aval
            If (IsDBNull(code) OrElse code.ToString.Length = 0) And (Not CBool(Configuration.GetConfig("AllowNoCqCodeDate"))) Then       ' OrElse CreateNewEmptyCqCode = False
              row.SetColumnError("code", myStringParserService.Parse("${res:Global.Error.CheckCodeMissing}"))
            Else
              row.SetColumnError("code", "")
            End If
            If (IsDBNull(duedate) OrElse CDate(duedate).Equals(Date.MinValue)) And Not CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
              row.SetColumnError("duedate", myStringParserService.Parse("${res:Global.Error.DateMissing}"))
            Else
              row.SetColumnError("duedate", "")
            End If
            If Not IsNumeric(realamount) OrElse CDec(realamount) <= 0 Then
              row.SetColumnError("realamount", myStringParserService.Parse("${res:Global.Error.RealAmountMissing}"))
            Else
              row.SetColumnError("realamount", "")
            End If
            If Not IsNumeric(paymenti_amt) OrElse CDec(paymenti_amt) <= 0 Then
              row.SetColumnError("paymenti_amt", myStringParserService.Parse("${res:Global.Error.PayAmountMissing}"))
            Else
              row.SetColumnError("paymenti_amt", "")
            End If
            If IsDBNull(bacode) OrElse bacode.ToString.Length = 0 Then
              'row.SetColumnError("bacode", myStringParserService.Parse("${res:Global.Error.BACodeMissing}"))
              row.SetColumnError("bacode", "")
            Else
              row.SetColumnError("bacode", "")
            End If

          Case 36     '�Թʴ����
            If IsDBNull(code) OrElse code.ToString.Length = 0 Then
              row.SetColumnError("code", myStringParserService.Parse("${res:Global.Error.PettyCashCodeMissing}"))
            Else
              row.SetColumnError("code", "")
            End If
            If Not IsNumeric(paymenti_amt) OrElse CDec(paymenti_amt) <= 0 Then
              row.SetColumnError("paymenti_amt", myStringParserService.Parse("${res:Global.Error.PayAmountMissing}"))
            Else
              row.SetColumnError("paymenti_amt", "")
            End If
            row.SetColumnError("bacode", "")
            row.SetColumnError("realamount", "")
            row.SetColumnError("duedate", "")
          Case 59     '�Ѵ��
            If IsDBNull(code) OrElse code.ToString.Length = 0 Then
              row.SetColumnError("code", myStringParserService.Parse("${res:Global.Error.AdvancePayCodeMissing}"))
            Else
              row.SetColumnError("code", "")
            End If
            If Not IsNumeric(paymenti_amt) OrElse CDec(paymenti_amt) <= 0 Then
              row.SetColumnError("paymenti_amt", myStringParserService.Parse("${res:Global.Error.PayAmountMissing}"))
            Else
              row.SetColumnError("paymenti_amt", "")
            End If
            row.SetColumnError("bacode", "")
            row.SetColumnError("realamount", "")
            row.SetColumnError("duedate", "")
          Case 174      '���ͧ����
            If IsDBNull(code) OrElse code.ToString.Length = 0 Then
              row.SetColumnError("code", myStringParserService.Parse("${res:Global.Error.AdvanceMoneyCodeMissing}"))
            Else
              row.SetColumnError("code", "")
            End If
            If Not IsNumeric(paymenti_amt) OrElse CDec(paymenti_amt) <= 0 Then
              row.SetColumnError("paymenti_amt", myStringParserService.Parse("${res:Global.Error.PayAmountMissing}"))
            Else
              row.SetColumnError("paymenti_amt", "")
            End If
            row.SetColumnError("bacode", "")
            row.SetColumnError("realamount", "")
            row.SetColumnError("duedate", "")
          Case 65     '�͹
            If IsDBNull(duedate) OrElse CDate(duedate).Equals(Date.MinValue) Then
              row.SetColumnError("duedate", myStringParserService.Parse("${res:Global.Error.DateMissing}"))
            Else
              row.SetColumnError("duedate", "")
            End If
            If Not IsNumeric(paymenti_amt) OrElse CDec(paymenti_amt) <= 0 Then
              row.SetColumnError("paymenti_amt", myStringParserService.Parse("${res:Global.Error.PayAmountMissing}"))
            Else
              row.SetColumnError("paymenti_amt", "")
            End If
            If IsDBNull(bacode) OrElse bacode.ToString.Length = 0 Then
              row.SetColumnError("bacode", myStringParserService.Parse("${res:Global.Error.BACodeMissing}"))
            Else
              row.SetColumnError("bacode", "")
            End If
            row.SetColumnError("code", "")
            row.SetColumnError("realamount", "")
          Case Else
            Return
        End Select
      End If

    End Sub
    Public Sub CopyToDataRow(ByVal row As TreeRow)
      If row Is Nothing Then
        Return
      End If
      Me.Payment.IsInitialized = False
      row("paymenti_linenumber") = Me.LineNumber
      row("paymenti_note") = Me.Note
      If Me.Amount <> 0 Then
        row("paymenti_amt") = Configuration.FormatToString(Me.Amount, DigitConfig.Price)
      Else
        row("paymenti_amt") = ""
      End If

      If Not Me.EntityType Is Nothing Then
        If Not Me.Entity Is Nothing Then
          If TypeOf Me.Entity Is OutgoingCheck Then
            row("code") = CType(Me.Entity, OutgoingCheck).CqCode
          ElseIf TypeOf Me.Entity Is OutgoingAval Then
            row("code") = CType(Me.Entity, OutgoingAval).CqCode
          Else
            row("code") = Me.Entity.Code
          End If
          row("DueDate") = Me.Entity.DueDate
          If TypeOf Me.Entity Is IHasBankAccount Then
            Dim hasb As IHasBankAccount = CType(Me.Entity, IHasBankAccount)
            If Not hasb.BankAccount Is Nothing Then
              row("BACode") = hasb.BankAccount.Code
              row("BAName") = hasb.BankAccount.Name
            End If
          ElseIf TypeOf Me.Entity Is OutgoingAval Then
            Dim hasb As OutgoingAval = CType(Me.Entity, OutgoingAval)
            If Not hasb.Loan Is Nothing Then
              row("BACode") = hasb.Loan.Code
              row("BAName") = hasb.Loan.Name
            End If
          ElseIf TypeOf Me.Entity Is IHasName Then
            Dim hasn As IHasName = CType(Me.Entity, IHasName)
            row("BACode") = hasn.Code
            row("BAName") = hasn.Name
          Else
            row("BACode") = DBNull.Value
            row("BAName") = DBNull.Value
          End If

          If Me.Entity.Amount <> 0 Then
            row("RealAmount") = Configuration.FormatToString(Me.Entity.Amount, DigitConfig.Price)
          Else
            row("RealAmount") = ""
          End If
        End If
        Select Case Me.EntityType.Value
          Case 0      'ʴ
            row("Button") = "invisible"
            row("BAButton") = "invisible"
          Case 22     '�礨���
            row("Button") = ""
            row("BAButton") = ""
          Case 336     'Aval
            row("Button") = ""
            row("BAButton") = ""
          Case 36     '�Թʴ����
            row("Button") = ""
            row("BAButton") = ""       ' "invisible"
          Case 59     '�Ѵ��
            row("Button") = ""
            row("BAButton") = ""       '"invisible"
          Case 174      '���ͧ����
            row("Button") = ""
            row("BAButton") = ""       '"invisible"
          Case 65     '�͹
            row("Button") = ""       '"invisible"
            row("BAButton") = ""
          Case Else
            row("Button") = ""
            row("BAButton") = "invisible"
        End Select

        row("paymenti_entitytype") = Me.EntityType.Value

      Else
        row("Button") = ""
        row("BAButton") = "invisible"
      End If
      Me.Payment.IsInitialized = True
    End Sub
#End Region

#Region "IPrintableEntity"
    Public Property printItemCode As String
    Public Property printItemDuedate As Date
    Public Property printItemBACode As String
    Public Property printItemBAName As String
    Public Property printItemRealAmount As Decimal

#End Region

    Public Shared Function GetNewCheckFromitemRow(ByVal itemRow As TreeRow, ByVal itemPayment As Payment) As OutgoingCheck
      '������
      Dim check As New OutgoingCheck
      If Not itemRow.IsNull("RealAmount") AndAlso IsNumeric(itemRow("RealAmount")) Then
        check.Amount = CDec(itemRow("RealAmount"))
      End If
      If Not itemRow.IsNull("code") AndAlso (itemRow("code").ToString.Length > 0 OrElse (itemRow("code").ToString.Length = 0 AndAlso CBool(Configuration.GetConfig("AllowNoCqCodeDate")))) Then
        check.CqCode = itemRow("code").ToString
      End If
      If Not itemRow.IsNull("paymenti_bankacct") AndAlso IsNumeric(itemRow("paymenti_bankacct")) Then
        check.Bankacct = New BankAccount(CInt(itemRow("paymenti_bankacct")))
      End If
      If Not itemRow.IsNull("duedate") Then
        check.DueDate = CDate(itemRow("duedate"))
      End If
      check.IssueDate = itemPayment.DocDate
      If Not itemPayment.RefDoc Is Nothing Then
        If Not itemPayment.RefDoc.Recipient Is Nothing Then
          If TypeOf itemPayment.RefDoc.Recipient Is Supplier Then
            check.Supplier = CType(itemPayment.RefDoc.Recipient, Supplier)
            check.Recipient = itemPayment.RefDoc.Recipient.Name
          Else
            check.Recipient = itemPayment.RefDoc.Recipient.Name
          End If
        End If
      End If
      check.AutoGen = True
      Return check
    End Function

  End Class

  <Serializable(), DefaultMember("Item")> _
  Public Class PaymentItemCollection
    Inherits CollectionBase

#Region "Members"
    Private m_payment As Payment
    Private m_lPayment As List(Of PaymentItem)
#End Region

#Region "Constructors"
    Public Sub New(ByVal owner As Payment)
      Me.m_payment = owner
      If Not Me.m_payment.Originated Then
        Return
      End If

      m_lPayment = New List(Of PaymentItem)

      Dim sqlConString As String = RecentCompanies.CurrentCompany.ConnectionString

      Dim ds As DataSet = SqlHelper.ExecuteDataset(sqlConString _
      , CommandType.StoredProcedure _
      , "GetPaymentItems" _
      , New SqlParameter("@payment_id", Me.m_payment.Id) _
      )

      For Each row As DataRow In ds.Tables(0).Rows
        Dim item As New PaymentItem(row, "")
        Dim nitem As New PaymentItem(row, "")
        If Not Me.m_payment Is Nothing _
        AndAlso Me.m_payment.Refdoctype = 66 _
        AndAlso Not item.EntityType Is Nothing _
        AndAlso item.EntityType.Value = 36 Then
        Else
          item.Payment = m_payment
          Me.Add(item)

          nitem.Payment = m_payment
          m_lPayment.Add(nitem)
        End If
      Next
    End Sub
#End Region

#Region "Properties"
    Default Public Property Item(ByVal index As Integer) As PaymentItem
      Get
        Return CType(MyBase.List.Item(index), PaymentItem)
      End Get
      Set(ByVal value As PaymentItem)
        MyBase.List.Item(index) = value
      End Set
    End Property
    Public ReadOnly Property Amount() As Decimal
      Get
        Dim amt As Decimal = 0
        For Each item As PaymentItem In Me
          amt += Configuration.Format(item.Amount, DigitConfig.Price)
        Next
        Return amt
      End Get
    End Property
#End Region

#Region "Class Methods"
    Public Function ListOfPaymentItem() As List(Of PaymentItem)
      If m_lPayment Is Nothing Then
        m_lPayment = New List(Of PaymentItem)
      End If
      Return m_lPayment
      'Dim newList As New List(Of PaymentItem)
      'For Each Item As PaymentItem In Me
      '  Dim newItem As New PaymentItem
      '  newList.Add(Item)
      'Next
      'Return newList
    End Function
    Public Sub Populate(ByVal dt As TreeTable)
      dt.Clear()
      Dim i As Integer = 0
      For Each vi As PaymentItem In Me
        If Not Me.m_payment Is Nothing _
        AndAlso TypeOf Me.m_payment.RefDoc Is PettyCashClaim _
        AndAlso Not vi.EntityType Is Nothing _
        AndAlso vi.EntityType.Value = 36 Then
        Else
          i += 1
          Dim newRow As TreeRow = dt.Childs.Add()
          vi.CopyToDataRow(newRow)
          vi.ItemValidateRow(newRow)
          newRow.Tag = vi
        End If
      Next
    End Sub
#End Region

#Region "Collection Methods"
    ''' <summary>
    ''' ����¹�ŧ GL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRefDocGLChange()
      If Not m_payment Is Nothing Then
        If TypeOf m_payment.RefDoc Is SimpleBusinessEntityBase Then
          CType(m_payment.RefDoc, SimpleBusinessEntityBase).OnGlChanged()
        End If
      End If
    End Sub
    Public Function Add(ByVal value As PaymentItem) As Integer
      If Not m_payment Is Nothing Then
        value.Payment = m_payment
      End If
      SetRefDocGLChange()
      Return MyBase.List.Add(value)
    End Function
    Public Sub AddRange(ByVal value As PaymentItemCollection)
      For i As Integer = 0 To value.Count - 1
        Me.Add(value(i))
      Next
    End Sub
    Public Sub AddRange(ByVal value As PaymentItem())
      For i As Integer = 0 To value.Length - 1
        Me.Add(value(i))
      Next
    End Sub
    Public Function Contains(ByVal value As PaymentItem) As Boolean
      Return MyBase.List.Contains(value)
    End Function
    Public Sub CopyTo(ByVal array As PaymentItem(), ByVal index As Integer)
      MyBase.List.CopyTo(array, index)
    End Sub
    Public Shadows Function GetEnumerator() As PaymentItemEnumerator
      Return New PaymentItemEnumerator(Me)
    End Function
    Public Function IndexOf(ByVal value As PaymentItem) As Integer
      Return MyBase.List.IndexOf(value)
    End Function
    Public Sub Insert(ByVal index As Integer, ByVal value As PaymentItem)
      If Not m_payment Is Nothing Then
        value.Payment = m_payment
      End If
      SetRefDocGLChange()
      MyBase.List.Insert(index, value)
    End Sub
    Public Sub Remove(ByVal value As PaymentItem)
      SetRefDocGLChange()
      MyBase.List.Remove(value)
    End Sub
    Public Sub Remove(ByVal index As Integer)
      SetRefDocGLChange()
      MyBase.List.RemoveAt(index)
    End Sub
#End Region


    Public Class PaymentItemEnumerator
      Implements IEnumerator

#Region "Members"
      Private m_baseEnumerator As IEnumerator
      Private m_temp As IEnumerable
#End Region

#Region "Construtor"
      Public Sub New(ByVal mappings As PaymentItemCollection)
        Me.m_temp = mappings
        Me.m_baseEnumerator = Me.m_temp.GetEnumerator
      End Sub
#End Region

      Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
        Get
          Return CType(Me.m_baseEnumerator.Current, PaymentItem)
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

  Public Class CashItem
    Implements IPaymentItem, IReceiveItem

#Region "Members"
    Private m_amount As Decimal
    Private m_docDate As Date
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub
    Public Sub New(ByVal amount As Decimal)
      Me.m_amount = amount
    End Sub
#End Region

#Region "Properties"
    Public Property DocDate() As Date Implements IPaymentItem.DueDate
      Get
        Return m_docDate
      End Get
      Set(ByVal Value As Date)
        m_docDate = Value
      End Set
    End Property

    Public ReadOnly Property CreateDate As Nullable(Of Date) Implements IPaymentItem.CreateDate, IReceiveItem.CreateDate
      Get
        Return Nothing
      End Get
    End Property
#End Region

#Region "IPaymentItem"
    Public Property Code() As String Implements IIdentifiable.Code
      Get
        Return ""
      End Get
      Set(ByVal Value As String)

      End Set
    End Property
    Public Property Id() As Integer Implements IIdentifiable.Id
      Get
        Return 0
      End Get
      Set(ByVal Value As Integer)

      End Set
    End Property
    Public Property Amount() As Decimal Implements IHasAmount.Amount
      Get
        Return m_amount
      End Get
      Set(ByVal Value As Decimal)
        m_amount = Value
      End Set
    End Property
#End Region

  End Class

  Public Class BankTransferOut
    Inherits SimpleBusinessEntityBase
    Implements IPaymentItem, IHasBankAccount, IExportable

#Region "Members"
    Private m_amount As Decimal
    Private m_bankacct As BankAccount
    Private m_docDate As Date

    Private m_supplier As Supplier
    Private m_recipient As String
#End Region

#Region "Constructors"
    Public Sub New()
      MyBase.New()
    End Sub
    Public Sub New(ByVal id As Integer)
      MyBase.New(id)
    End Sub
    Public Sub New(ByVal code As String)
      MyBase.New(code)
    End Sub
    Public Sub New(ByVal dr As DataRow, ByVal aliasPrefix As String)
      MyBase.New(dr, aliasPrefix)
    End Sub
    Private m_exportType As String
    Public Property ExportType As String Implements IExportable.ExportType
      Get
        Return m_exportType
      End Get
      Set(ByVal value As String)
        m_exportType = value
      End Set
    End Property
    Protected Overloads Overrides Sub Construct()
      MyBase.Construct()

      Me.m_bankacct = New BankAccount
      Me.m_supplier = New Supplier
      Me.m_docDate = Now.Date
      m_exportType = "mcl"
      Me.Status = New CheckStatus(-1)
    End Sub
    Protected Overloads Overrides Sub Construct(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      MyBase.Construct(ds, aliasPrefix)
    End Sub
    Protected Overloads Overrides Sub Construct(ByVal dr As System.Data.DataRow, ByVal aliasPrefix As String)
      MyBase.Construct(dr, aliasPrefix)
      With Me

        If dr.Table.Columns.Contains(aliasPrefix & "bto_docDate") AndAlso Not dr.IsNull(aliasPrefix & "bto_docDate") Then
          .m_docDate = CDate(dr(aliasPrefix & "bto_docDate"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "supplier_id") Then
          If Not dr.IsNull(aliasPrefix & "supplier_id") Then
            .m_supplier = New Supplier(CInt(dr("supplier_id"))) 'Supplier.GetSupplierbyDataRow(dr)
            '.m_supplier = New Supplier(dr, aliasPrefix)
          End If
        Else
          If dr.Table.Columns.Contains(aliasPrefix & "bto_supplier") AndAlso Not dr.IsNull(aliasPrefix & "bto_supplier") Then
            Dim filters(0) As Filter
            filters(0) = New Filter("includeInvisible", True) '��������ͧ��� supplier ����͹������ �Թʴ���� ��
            .m_supplier = New Supplier(CInt(dr(aliasPrefix & "bto_supplier")), filters)
          End If
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "bto_recipient") AndAlso Not dr.IsNull(aliasPrefix & "bto_recipient") Then
          .m_recipient = CStr(dr(aliasPrefix & "bto_recipient"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "bankacct_id") Then
          If Not dr.IsNull(aliasPrefix & "bankacct_id") Then
            .m_bankacct = New BankAccount(dr, aliasPrefix)
          End If
        Else
          If dr.Table.Columns.Contains(aliasPrefix & "bto_bankacct") AndAlso Not dr.IsNull(aliasPrefix & "bto_bankacct") Then
            .m_bankacct = New BankAccount(CInt(dr(aliasPrefix & "bto_bankacct")))
          End If
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "bto_amt") AndAlso Not dr.IsNull(aliasPrefix & "bto_amt") Then
          .m_amount = CDec(dr(aliasPrefix & "bto_amt"))
        End If


        If dr.Table.Columns.Contains(aliasPrefix & Me.Prefix & "_status") AndAlso Not dr.IsNull(aliasPrefix & Me.Prefix & "_status") Then
          .Status.Value = CInt(dr(aliasPrefix & Me.Prefix & "_status"))
        End If

        If dr.Table.Columns.Contains(aliasPrefix & "bto_ExportType") AndAlso Not dr.IsNull(aliasPrefix & "bto_ExportType") Then
          .ExportType = CStr(dr(aliasPrefix & "bto_ExportType"))
        End If
      End With
      RefreshPVList()
    End Sub
#End Region

#Region "Properties"
    Public Property BankAccount() As BankAccount Implements IHasBankAccount.BankAccount
      Get
        Return m_bankacct
      End Get
      Set(ByVal Value As BankAccount)
        m_bankacct = Value
      End Set
    End Property
    Public Property DocDate() As Date Implements IPaymentItem.DueDate
      Get
        Return m_docDate
      End Get
      Set(ByVal Value As Date)
        m_docDate = Value
      End Set
    End Property

    Public ReadOnly Property CreateDate As Nullable(Of Date) Implements IPaymentItem.CreateDate
      Get
        Return Nothing
      End Get
    End Property

    Public Property Supplier() As Supplier      Get        Return m_supplier      End Get      Set(ByVal Value As Supplier)        m_supplier = Value        If Me.Recipient Is Nothing OrElse Me.Recipient.Length = 0 Then          Me.Recipient = m_supplier.Name
        End If
      End Set    End Property    Public Property Recipient() As String      Get        Return m_recipient      End Get      Set(ByVal Value As String)        m_recipient = Value      End Set    End Property
#End Region

#Region "IHasAmount"
    Public Property Amount() As Decimal Implements IHasAmount.Amount
      Get
        Return m_amount
      End Get
      Set(ByVal Value As Decimal)
        m_amount = Value
      End Set
    End Property
#End Region

#Region "Methods"
    Private m_paymentList As List(Of PaymentForList)
    Public ReadOnly Property PaymentList As List(Of PaymentForList) Implements IExportable.PaymentList
      Get
        If m_paymentList Is Nothing Then
          m_paymentList = New List(Of PaymentForList)
        End If
        Return m_paymentList
      End Get
    End Property
    Public Sub RefreshPVList()
      Dim ds As DataSet = SqlHelper.ExecuteDataset(Me.ConnectionString _
      , CommandType.StoredProcedure _
      , "GetBanktransferoutPayments" _
      , New SqlParameter("@bto_id", Me.Id) _
      , New SqlParameter("@supplier_id", Me.ValidIdOrDBNull(Me.Supplier)) _
      )

      m_paymentList = New List(Of PaymentForList)
      For Each row As DataRow In ds.Tables(0).Rows
        Dim deh As New DataRowHelper(row)
        Dim p As New PaymentForList
        p.Id = deh.GetValue(Of Integer)("Id")
        p.Code = deh.GetValue(Of String)("Code")
        p.RefId = deh.GetValue(Of Integer)("RefId")
        p.RefCode = deh.GetValue(Of String)("RefCode")
        p.RefType = deh.GetValue(Of String)("RefType")
        p.RefTypeId = deh.GetValue(Of Integer)("RefTypeId")
        p.RefDocDate = deh.GetValue(Of Date)("RefDocDate")
        p.RefDueDate = deh.GetValue(Of Date)("RefDueDate")
        p.RefAmount = deh.GetValue(Of Decimal)("RefAmount")
        p.Amount = deh.GetValue(Of Decimal)("Amount")
        p.RefPaid = deh.GetValue(Of Decimal)("RefPaid")
        p.Note = deh.GetValue(Of String)("Note")
        p.JustAdded = False

        '===========================================================
        p.PayeeFax = deh.GetValue(Of String)("PayeeFaxForExport")
        p.PayeeID = deh.GetValue(Of String)("PayeeID")
        p.PayeeName = deh.GetValue(Of String)("PayeeName")
        p.PayeeTaxID = deh.GetValue(Of String)("PayeeTaxID")
        p.PersonalID = deh.GetValue(Of String)("PersonalID")

        p.KbankDCAccount = deh.GetValue(Of String)("KbankDCAccount")
        p.KbankDCBank = deh.GetValue(Of String)("KbankDCBank")
        p.KbankMCAccount = deh.GetValue(Of String)("KbankMCAccount")
        p.KbankMCBank = deh.GetValue(Of String)("KbankMCBank")
        '===========================================================

        '===========================================================
        Dim drs As DataRow() = ds.Tables(1).Select("ID=" & p.Id.ToString)
        If Not drs Is Nothing AndAlso drs.Length > 0 Then
          Dim currentTaxInfo As New TaxInfo
          For Each r As DataRow In drs
            Dim deh2 As New DataRowHelper(r)
            Dim wid As Integer = deh2.GetValue(Of Integer)("whtid")
            If currentTaxInfo.ID <> wid Then
              currentTaxInfo = New TaxInfo
              currentTaxInfo.ID = wid
              currentTaxInfo.TaxCondition = deh2.GetValue(Of String)("TaxCondition")
              currentTaxInfo.TaxForm = deh2.GetValue(Of String)("TaxForm")
              p.TaxInfos.Add(currentTaxInfo)
            End If
            Dim ti As New TaxInfoItem
            ti.Description = deh2.GetValue(Of String)("Description")
            ti.BeforeVAT = deh2.GetValue(Of Decimal)("BeforeVAT")
            ti.TaxRate = deh2.GetValue(Of Decimal)("TaxRate")
            ti.AfterVat = ti.BeforeVAT + Vat.GetVatAmount(ti.BeforeVAT)
            ti.WHT = deh2.GetValue(Of Decimal)("TaxAmount")
            currentTaxInfo.TaxInfoItems.Add(ti)
          Next
        End If
        '===========================================================
        m_paymentList.Add(p)
      Next
    End Sub
#End Region

#Region "Overrides"
    Public Overrides ReadOnly Property ClassName() As String
      Get
        Return "BankTransferOut"
      End Get
    End Property

    Public Overrides ReadOnly Property TableName() As String
      Get
        Return "BankTransferOut"
      End Get
    End Property

    Public Overrides ReadOnly Property GetSprocName() As String
      Get
        Return "Get" & Me.TableName
      End Get
    End Property

    Public Overrides ReadOnly Property Prefix() As String
      Get
        Return "bto"
      End Get
    End Property

    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.BankTransferOut.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.BankTransferOut"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.BankTransferOut"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.BankTransferOut.ListLabel}"
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

    Private Sub ResetID(ByVal oldid As Integer)
      Me.Id = oldid
    End Sub
    Public Overloads Overrides Function Save(ByVal currentUserId As Integer) As SaveErrorException
      'If Me.Amount <= 0 AndAlso (Me.DocStatus.Value <> 5 OrElse Me.Originated) Then
      '  Return New SaveErrorException("${res:Global.Error.ZeroValueMiss}", "${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblAmount}")
      'End If
      If Me.Originated Then
        If Not Me.Supplier Is Nothing Then
          If Me.Supplier.Canceled Then
            Return New SaveErrorException(Me.StringParserService.Parse("${res:Global.Error.CanceledSupplier}"), New String() {Me.Supplier.Code})
          End If
        End If
      End If
      ' ��˹� SqlParameter ���� return ��ҡ�� Execute procedure ...
      Dim returnVal As System.Data.SqlClient.SqlParameter = New SqlParameter
      returnVal.ParameterName = "RETURN_VALUE"
      returnVal.DbType = DbType.Int32
      returnVal.Direction = ParameterDirection.ReturnValue
      returnVal.SourceVersion = DataRowVersion.Current

      ' ���ҧ ArrayList �ҡ Item �ͧ  SqlParameter ...
      Dim paramArrayList As New ArrayList

      If Me.Originated Then
        paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_id", Me.Id))
      End If

      Dim theTime As Date = Now
      Dim theUser As New User(currentUserId)

      If Me.AutoGen And Me.Code.Length = 0 Then
        Me.Code = Me.GetNextCode
      End If
      Me.AutoGen = False


      If Me.Status.Value = -1 Then
        Me.Status.Value = 2
      End If

      paramArrayList.Add(returnVal)
      paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_code", Me.Code))
      paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_docdate", ValidDateOrDBNull(Me.DocDate)))
      paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_supplier", Me.ValidIdOrDBNull(Me.Supplier)))
      paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_recipient", Me.Recipient))
      paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_bankacct", Me.ValidIdOrDBNull(Me.BankAccount)))
      paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_amt", Me.Amount))
      paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_status", Me.Status.Value))
      paramArrayList.Add(New SqlParameter("@" & Me.Prefix & "_exporttype", Me.ExportType))

      SetOriginEditCancelStatus(paramArrayList, currentUserId, theTime)

      ' ���ҧ SqlParameter �ҡ ArrayList ...
      Dim sqlparams() As SqlParameter
      sqlparams = CType(paramArrayList.ToArray(GetType(SqlParameter)), SqlParameter())

      Dim trans As SqlTransaction
      Dim conn As New SqlConnection(Me.ConnectionString)

      If conn.State = ConnectionState.Open Then conn.Close()
      conn.Open()
      trans = conn.BeginTransaction()

      Dim oldid As Integer = Me.Id

      Try
        Me.ExecuteSaveSproc(conn, trans, returnVal, sqlparams, theTime, theUser)
        If IsNumeric(returnVal.Value) Then
          Select Case CInt(returnVal.Value)
            Case -1
              trans.Rollback()
              Me.ResetID(oldid)
              Return New SaveErrorException("${res:Global.Error.DupBtoCode}", Me.Code)
            Case -2
              trans.Rollback()
              Me.ResetID(oldid)
              Return New SaveErrorException("${res:Global.Error.BtoCodeIsRefed}", Me.Code)
            Case Else
          End Select
        ElseIf IsDBNull(returnVal.Value) OrElse Not IsNumeric(returnVal.Value) Then
          trans.Rollback()
          Me.ResetID(oldid)
          Return New SaveErrorException(returnVal.Value.ToString)
        End If
        Dim detailError As SaveErrorException = SaveDetail(Me.Id, conn, trans, currentUserId)
        If Not IsNumeric(detailError.Message) Then
          Me.ResetID(oldid)
          trans.Rollback()
          Return detailError
        Else
          Select Case CInt(detailError.Message)
            Case -1, -5
              Me.ResetID(oldid)
              trans.Rollback()
              Return New SaveErrorException(returnVal.Value.ToString)
            Case -2
              Me.ResetID(oldid)
              trans.Rollback()
              Return New SaveErrorException(returnVal.Value.ToString)
            Case Else
          End Select
        End If
        trans.Commit()
        Return New SaveErrorException(returnVal.Value.ToString)
      Catch ex As SqlException
        trans.Rollback()
        Me.ResetID(oldid)
        Return New SaveErrorException(ex.ToString)
      Catch ex As Exception
        trans.Rollback()
        Me.ResetID(oldid)
        Return New SaveErrorException(ex.ToString)
      Finally
        conn.Close()
        Try
          For Each item As PaymentForList In Me.PaymentList
            If item.JustAdded Then
              Dim refType As Integer = item.RefTypeId
              Dim refId As Integer = item.RefId
              Dim theEntity As SimpleBusinessEntityBase = SimpleBusinessEntityBase.GetEntity(Entity.GetFullClassName(refType), refId)
              Dim m_whtcol As WitholdingTaxCollection
              Dim m_FirstWht As WitholdingTax
              If TypeOf (theEntity) Is IPayable Then
                Dim payable As IPayable = CType(theEntity, IPayable)
                If payable.Payment IsNot Nothing Then
                  payable.Payment.OnHold = False
                End If
              End If
              '====================WHT=========================
              If TypeOf theEntity Is IWitholdingTaxable Then
                Dim whtRefDoc As IWitholdingTaxable = CType(theEntity, IWitholdingTaxable)
                m_whtcol = whtRefDoc.WitholdingTaxCollection
                If m_whtcol Is Nothing Then
                  m_whtcol = New WitholdingTaxCollection
                  m_whtcol.RefDoc.WitholdingTaxCollection = m_whtcol
                End If
                If m_whtcol.Count > 0 Then
                  For Each witem As WitholdingTax In m_whtcol
                    witem.RefDoc.WitholdingTaxCollection = m_whtcol
                    witem.RefDoc = whtRefDoc
                    witem.Entity = whtRefDoc.Person
                  Next
                  m_FirstWht = m_whtcol(0)
                Else
                  m_FirstWht = New WitholdingTax
                  m_FirstWht.Code = BusinessLogic.Entity.GetAutoCodeFormat(m_FirstWht.EntityId)
                  m_FirstWht.LastestCode = m_FirstWht.Code
                  m_FirstWht.RefDoc.WitholdingTaxCollection = m_whtcol
                  m_FirstWht.RefDoc = whtRefDoc
                  m_FirstWht.Entity = whtRefDoc.Person
                  m_whtcol.Add(m_FirstWht)
                End If

                If whtRefDoc.WitholdingTaxCollection.Count > 0 AndAlso _
                   whtRefDoc.WitholdingTaxCollection.CanBeDelay Then
                  m_whtcol = whtRefDoc.WitholdingTaxCollection
                End If
                m_whtcol.RefDoc = whtRefDoc
              End If
              If m_whtcol.Count > 0 Then
                If Not m_whtcol Is Nothing AndAlso m_whtcol.Contains(m_whtcol(0)) Then
                  m_FirstWht = m_whtcol(0)
                End If
              Else
                Dim whtRefDoc As IWitholdingTaxable = CType(theEntity, IWitholdingTaxable)
                m_whtcol = whtRefDoc.WitholdingTaxCollection

                m_FirstWht = New WitholdingTax
                m_FirstWht.Code = BusinessLogic.Entity.GetAutoCodeFormat(m_FirstWht.EntityId)
                m_FirstWht.LastestCode = m_FirstWht.Code
                m_FirstWht.RefDoc.WitholdingTaxCollection = m_whtcol
                m_FirstWht.RefDoc = whtRefDoc
                m_FirstWht.Entity = whtRefDoc.Person
                m_whtcol.Add(m_FirstWht)
              End If
              '====================WHT=========================

              '======================GL=======================
              'theEntity.GLIsChanged = True
              '======================GL=======================
              theEntity.Save(currentUserId)
            End If
          Next
        Catch ex As Exception
        End Try
      End Try
    End Function
    Private Function GetPaymentIdToSave() As String
      Dim list As New List(Of String)
      list.Add("'0'")
      For Each item As PaymentForList In Me.PaymentList
        If item.JustAdded Then
          list.Add(item.Id.ToString)
        End If
      Next
      Return String.Join(",", list.ToArray)
    End Function
    Private Function SaveDetail(ByVal parentID As Integer, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal currentUserId As Integer) As SaveErrorException
      Try
        Dim da As New SqlDataAdapter("Select * from paymentitem where (paymenti_entitytype = 65 and paymenti_entity=" & Me.Id & ") or paymenti_payment in (" & GetPaymentIdToSave() & ")", conn)
        Dim da2 As New SqlDataAdapter("select * from payment where payment_id in (" & GetPaymentIdToSave() & ")", conn)
        Dim cmdBuilder As New SqlCommandBuilder(da)

        Dim ds As New DataSet

        da.SelectCommand.Transaction = trans

        '��ͧ�����ͨҡ da.SelectCommand.Transaction = trans
        cmdBuilder.GetDeleteCommand.Transaction = trans
        cmdBuilder.GetInsertCommand.Transaction = trans
        cmdBuilder.GetUpdateCommand.Transaction = trans

        da.Fill(ds, "paymentitem")

        '=================================================
        cmdBuilder = New SqlCommandBuilder(da2)

        da2.SelectCommand.Transaction = trans

        '��ͧ�����ͨҡ da2.SelectCommand.Transaction = trans
        cmdBuilder.GetDeleteCommand.Transaction = trans
        cmdBuilder.GetInsertCommand.Transaction = trans
        cmdBuilder.GetUpdateCommand.Transaction = trans

        da2.Fill(ds, "payment")
        '=================================================

        With ds.Tables("paymentitem")
          Dim rowsToDelete As New List(Of DataRow)
          For Each row As DataRow In .Rows
            Dim found As Boolean = False
            For Each item As PaymentForList In Me.PaymentList
              If item.Id = CInt(row("paymenti_payment")) Then
                found = True
                Exit For
              End If
            Next
            If Not found Then
              rowsToDelete.Add(row)
            End If
          Next
          For Each row As DataRow In rowsToDelete
            row.Delete()
          Next
          Dim i As Integer = 0
          For Each item As PaymentForList In Me.PaymentList
            If item.JustAdded Then
              Dim drs1 As DataRow() = ds.Tables("paymentitem").Select("paymenti_payment = " & item.Id.ToString)
              If Not drs1 Is Nothing AndAlso drs1.Length > 0 Then
                Dim dr As DataRow = drs1(0)
                dr("paymenti_entity") = Me.Id
                dr("paymenti_entitycode") = Me.Code
                dr("paymenti_payment") = item.Id
                dr("paymenti_linenumber") = i + 1
                dr("paymenti_entityType") = Me.EntityId
                dr("paymenti_refamt") = Me.Amount
                dr("paymenti_amt") = item.Amount
                dr("paymenti_note") = item.Note
                dr("paymenti_status") = Me.Status.Value
              Else
                Dim dr As DataRow = ds.Tables("paymentitem").NewRow
                dr("paymenti_entity") = Me.Id
                dr("paymenti_entitycode") = Me.Code
                dr("paymenti_payment") = item.Id
                dr("paymenti_linenumber") = i + 1
                dr("paymenti_entityType") = Me.EntityId
                dr("paymenti_refamt") = Me.Amount
                dr("paymenti_amt") = item.Amount
                dr("paymenti_note") = item.Note
                dr("paymenti_status") = Me.Status.Value
                ds.Tables("paymentitem").Rows.Add(dr)
              End If
              i += 1
              Dim drs As DataRow() = ds.Tables("payment").Select("payment_id = " & item.Id.ToString)
              If Not drs Is Nothing AndAlso drs.Length > 0 Then
                Dim paymentDR As DataRow = drs(0)
                Dim deh As New DataRowHelper(paymentDR)
                Dim oldAmount As Decimal = deh.GetValue(Of Decimal)("payment_amt")
                Dim oldGross As Decimal = deh.GetValue(Of Decimal)("payment_gross")
                paymentDR("payment_gross") = oldGross + item.Amount
              End If
            End If
          Next
        End With
        Dim dt As DataTable = ds.Tables("paymentitem")
        ' First process deletes.
        da.Update(dt.Select(Nothing, Nothing, DataViewRowState.Deleted))
        ' Next process updates.
        da.Update(dt.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent))
        ' Finally process inserts.
        da.Update(dt.Select(Nothing, Nothing, DataViewRowState.Added))

        Dim dt2 As DataTable = ds.Tables("payment")
        da2.Update(dt2.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent))
      Catch ex As Exception
        Return New SaveErrorException(ex.ToString)
      End Try
      Return New SaveErrorException("0")
    End Function
#End Region

    Private Function GetList(ByVal list As List(Of PaymentForList)) As IQueryable(Of PaymentForList)
      Dim ret As IQueryable(Of PaymentForList) = (From p In list
                  Select p).AsQueryable
      Return ret
    End Function
    Public Function GetSum() As Decimal
      Return GetList(PaymentList).Sum(Function(p) p.Amount)
    End Function
    Public Function GetRemain() As Decimal
      Return Me.Amount - GetSum()
    End Function


#Region "Delete"
    Public Overrides ReadOnly Property CanDelete() As Boolean
      Get
        If Me.Originated Then
          Return Me.Status.Value = 2
        End If
        Return False
      End Get
    End Property
    Public Overrides Function Delete() As SaveErrorException
      If Not Me.Originated Then
        Return New SaveErrorException("${res:Global.Error.NoIdError}")
      End If
      Dim myMessage As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      Dim format(0) As String
      format(0) = Me.Code
      If Not myMessage.AskQuestionFormatted("${res:Global.ConfirmDeleteBankTransferOut}", format) Then
        Return New SaveErrorException("${res:Global.CencelDelete}")
      End If
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
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "DeleteBankTransferOut", New SqlParameter() {New SqlParameter("@bto_id", Me.Id), returnVal})
        If IsNumeric(returnVal.Value) Then
          Select Case CInt(returnVal.Value)
            Case -1
              trans.Rollback()
              Return New SaveErrorException("${res:Global.DeleteBankTransferOutIsReferencedCannotBeDeleted}")
            Case Else
          End Select
        ElseIf IsDBNull(returnVal.Value) OrElse Not IsNumeric(returnVal.Value) Then
          trans.Rollback()
          Return New SaveErrorException(returnVal.Value.ToString)
        End If
        trans.Commit()
        Return New SaveErrorException("1")
      Catch ex As SqlException
        trans.Rollback()
        Return New SaveErrorException(ex.Message)
      Catch ex As Exception
        trans.Rollback()
        Return New SaveErrorException(ex.Message)
      Finally
        conn.Close()
      End Try
    End Function
#End Region

  End Class

  Public Class PaymentAccountItem
    Implements ICloneable

#Region "Members"
    Private m_payment As Payment
    Private m_acct As Account
    Private m_amount As Decimal
    Private m_isDebit As Boolean
#End Region

#Region "Constructors"
    Public Sub New()
      MyBase.New()
      m_acct = New Account
    End Sub
    Public Sub New(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
      Me.Construct(ds, aliasPrefix)
    End Sub
    Public Sub New(ByVal dr As DataRow, ByVal aliasPrefix As String)
      Me.Construct(dr, aliasPrefix)
    End Sub
    Protected Sub Construct(ByVal dr As DataRow, ByVal aliasPrefix As String)
      With Me
        If dr.Table.Columns.Contains(aliasPrefix & "paymenta_amt") AndAlso Not dr.IsNull(aliasPrefix & "paymenta_amt") Then
          .m_amount = CDec(dr(aliasPrefix & "paymenta_amt"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "paymenta_isdebit") AndAlso Not dr.IsNull(aliasPrefix & "paymenta_isdebit") Then
          .m_isDebit = CBool(dr(aliasPrefix & "paymenta_isdebit"))
        End If
        If dr.Table.Columns.Contains(aliasPrefix & "acct_id") AndAlso Not dr.IsNull(aliasPrefix & "acct_id") Then
          If Not dr.IsNull("acct_id") Then
            .m_acct = New Account(dr, "")
          End If
        Else
          If dr.Table.Columns.Contains(aliasPrefix & "paymenta_acct") AndAlso Not dr.IsNull(aliasPrefix & "paymenta_acct") Then
            .m_acct = New Account(CInt(dr(aliasPrefix & "paymenta_acct")))
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
    Public Property Payment() As Payment
      Get
        Return m_payment
      End Get
      Set(ByVal Value As Payment)
        m_payment = Value
      End Set
    End Property
    Public Property Account() As Account
      Get
        Return m_acct
      End Get
      Set(ByVal Value As Account)
        m_acct = Value
      End Set
    End Property
    Public Property Amount() As Decimal
      Get
        Return m_amount
      End Get
      Set(ByVal Value As Decimal)
        m_amount = Value
      End Set
    End Property
    Public Property IsDebit() As Boolean
      Get
        Return m_isDebit
      End Get
      Set(ByVal Value As Boolean)
        m_isDebit = Value
      End Set
    End Property
#End Region

#Region "ICloneable"
    Public Function Clone() As Object Implements System.ICloneable.Clone
      Dim paymenta As New PaymentAccountItem
      paymenta.m_payment = Me.m_payment
      paymenta.m_isDebit = Me.m_isDebit
      paymenta.m_amount = Me.m_amount
      paymenta.m_acct = Me.m_acct
      Return paymenta
    End Function
#End Region

  End Class

  <Serializable(), DefaultMember("Item")> _
  Public Class PaymentAccountItemCollection
    Inherits CollectionBase
    Implements ICloneable

#Region "Members"
    Private m_payment As Payment
    Private m_isDebit As Boolean
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub
    Public Sub New(ByVal pm As Payment, ByVal isDebit As Boolean)
      m_payment = pm
      m_isDebit = isDebit
      If Not pm.Originated Then
        Return
      End If

      Dim sqlConString As String = RecentCompanies.CurrentCompany.ConnectionString


      Dim ds As DataSet = SqlHelper.ExecuteDataset(sqlConString _
      , CommandType.StoredProcedure _
      , "GetPaymentAccountItems" _
      , New SqlParameter("@payment_id", m_payment.Id) _
      , New SqlParameter("@paymenta_isDebit", m_isDebit) _
      )

      For Each row As DataRow In ds.Tables(0).Rows
        Dim item As New PaymentAccountItem(row, "")
        Me.Add(item)
      Next
    End Sub
#End Region

#Region "Properties"
    Public Property Payment() As Payment
      Get
        Return m_payment
      End Get
      Set(ByVal Value As Payment)
        m_payment = Value
      End Set
    End Property
    Default Public Property Item(ByVal index As Integer) As PaymentAccountItem
      Get
        Return CType(MyBase.List.Item(index), PaymentAccountItem)
      End Get
      Set(ByVal value As PaymentAccountItem)
        MyBase.List.Item(index) = value
      End Set
    End Property
#End Region

#Region "Class Methods"
    Public Function GetAmount() As Decimal
      Dim ret As Decimal = 0
      For Each item As PaymentAccountItem In Me
        ret += item.Amount
      Next
      Return ret
    End Function
    Public Sub Populate(ByVal dt As TreeTable)
      dt.Clear()
      Dim i As Integer = 0
      For Each paymenta As PaymentAccountItem In Me
        i += 1
        Dim newRow As TreeRow = dt.Childs.Add()
        newRow("Linenumber") = i
        If Not paymenta.Account Is Nothing AndAlso paymenta.Account.Originated Then
          newRow("Code") = paymenta.Account.Code
          newRow("Name") = paymenta.Account.Name
        End If
        newRow("paymenta_amt") = Configuration.FormatToString(paymenta.Amount, DigitConfig.Price)
        newRow.Tag = paymenta
      Next
    End Sub
    Public Sub CleanCollection()
      Dim temp As New ArrayList
      For Each item As PaymentAccountItem In Me
        If item.Account Is Nothing OrElse Not item.Account.Originated Then
          temp.Add(item)
        End If
      Next
      For Each item As PaymentAccountItem In temp
        Me.Remove(item)
      Next
    End Sub
#End Region

#Region "Collection Methods"
    Public Function Add(ByVal value As PaymentAccountItem) As Integer
      value.Payment = Me.m_payment
      value.IsDebit = m_isDebit
      Return MyBase.List.Add(value)
    End Function
    Public Sub AddRange(ByVal value As PaymentAccountItemCollection)
      For i As Integer = 0 To value.Count - 1
        Me.Add(value(i))
      Next
    End Sub
    Public Sub AddRange(ByVal value As PaymentAccountItem())
      For i As Integer = 0 To value.Length - 1
        Me.Add(value(i))
      Next
    End Sub
    Public Function Contains(ByVal value As PaymentAccountItem) As Boolean
      Return MyBase.List.Contains(value)
    End Function
    Public Sub CopyTo(ByVal array As PaymentAccountItem(), ByVal index As Integer)
      MyBase.List.CopyTo(array, index)
    End Sub
    Public Shadows Function GetEnumerator() As PaymentAccountItemEnumerator
      Return New PaymentAccountItemEnumerator(Me)
    End Function
    Public Function IndexOf(ByVal value As PaymentAccountItem) As Integer
      Return MyBase.List.IndexOf(value)
    End Function
    Public Sub Insert(ByVal index As Integer, ByVal value As PaymentAccountItem)
      value.Payment = Me.m_payment
      value.IsDebit = m_isDebit
      MyBase.List.Insert(index, value)
    End Sub
    Public Sub Remove(ByVal value As PaymentAccountItem)
      MyBase.List.Remove(value)
    End Sub
    Public Sub Remove(ByVal value As PaymentAccountItemCollection)
      For i As Integer = 0 To value.Count - 1
        Me.Remove(value(i))
      Next
    End Sub
    Public Sub Remove(ByVal index As Integer)
      MyBase.List.RemoveAt(index)
    End Sub
#End Region

#Region "ICloneable"
    Public Function Clone() As Object Implements System.ICloneable.Clone
      Dim newColl As New PaymentAccountItemCollection
      newColl.m_payment = Me.m_payment
      newColl.m_isDebit = Me.m_isDebit
      For Each oldItem As PaymentAccountItem In Me
        newColl.Add(CType(oldItem.Clone, PaymentAccountItem))
      Next
      Return newColl
    End Function
#End Region


    Public Class PaymentAccountItemEnumerator
      Implements IEnumerator

#Region "Members"
      Private m_baseEnumerator As IEnumerator
      Private m_temp As IEnumerable
#End Region

#Region "Construtor"
      Public Sub New(ByVal mappings As PaymentAccountItemCollection)
        Me.m_temp = mappings
        Me.m_baseEnumerator = Me.m_temp.GetEnumerator
      End Sub
#End Region

      Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
        Get
          Return CType(Me.m_baseEnumerator.Current, PaymentAccountItem)
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
