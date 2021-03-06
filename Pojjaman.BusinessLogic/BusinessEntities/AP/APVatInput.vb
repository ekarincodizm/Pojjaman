Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.TextHelper

Namespace Longkong.Pojjaman.BusinessLogic
  Public Class APVatInputStatus
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
  Public Class APVatInput
    Inherits SimpleBusinessEntityBase
    Implements IGLAble, IPrintableEntity, IHasIBillablePerson, ICancelable, IVatable, IGLCheckingBeforeRefresh, INewPrintableEntity

#Region "Members"
    Private m_supplier As Supplier
    Private m_docDate As Date

    Private m_note As String
    Private m_creditPeriod As Integer

    Private m_status As APVatInputStatus

    Private m_je As JournalEntry

    Private m_itemCollection As BillAcceptanceItemCollection

    Private m_vat As Vat
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
        .m_vat = New Vat
        .m_vat.Direction.Value = 1
        .m_supplier = New Supplier
        .m_creditPeriod = 0
        .m_note = ""
        .m_docDate = Date.Now.Date
        .m_status = New APVatInputStatus(-1)
        .m_je = New JournalEntry(Me)
        .m_je.DocDate = Me.m_docDate
        .m_itemCollection = New BillAcceptanceItemCollection(Me)
        .AutoCodeFormat = New AutoCodeFormat(Me)
      End With
    End Sub
    Protected Overloads Overrides Sub Construct(ByVal dr As System.Data.DataRow, ByVal aliasPrefix As String)
      MyBase.Construct(dr, aliasPrefix)
      With Me

        If dr.Table.Columns.Contains("supplier.supplier_id") Then
          If Not dr.IsNull("supplier.supplier_id") Then
            .m_supplier = New Supplier(dr, "supplier.")
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
        End If

        If dr.Table.Columns.Contains("pays_note") AndAlso Not dr.IsNull(aliasPrefix & "pays_note") Then
          .m_note = CStr(dr(aliasPrefix & "pays_note"))
        End If

        If dr.Table.Columns.Contains("pays_status") AndAlso Not dr.IsNull(aliasPrefix & "pays_status") Then
          .m_status = New APVatInputStatus(CInt(dr(aliasPrefix & "pays_status")))
        End If

        .m_je = New JournalEntry(Me)

        .m_vat = New Vat(Me)
        .m_vat.Direction.Value = 1

        m_itemCollection = New BillAcceptanceItemCollection(Me)
      End With
      Me.AutoCodeFormat = New AutoCodeFormat(Me)
    End Sub
#End Region

#Region "Properties"
    Public Property ItemCollection() As BillAcceptanceItemCollection
      Get
        Return m_itemCollection
      End Get
      Set(ByVal Value As BillAcceptanceItemCollection)
        m_itemCollection = Value
      End Set
    End Property
    Public Property Supplier() As Supplier
      Get
        Return m_supplier
      End Get
      Set(ByVal Value As Supplier)
        m_supplier = Value
      End Set
    End Property
    Public Property DocDate() As Date Implements IGLAble.Date, IVatable.Date
      Get
        Return m_docDate
      End Get
      Set(ByVal Value As Date)
        m_docDate = Value
        Me.m_je.DocDate = Value
      End Set
    End Property
    Public Property Note() As String Implements IGLAble.Note
      Get
        Return m_note
      End Get
      Set(ByVal Value As String)
        m_note = Value
      End Set
    End Property
    Public Property CreditPeriod() As Integer
      Get
        Return m_creditPeriod
      End Get
      Set(ByVal Value As Integer)
        m_creditPeriod = Value
      End Set
    End Property

    Public Overrides Property Status() As CodeDescription
      Get
        Return m_status
      End Get
      Set(ByVal Value As CodeDescription)
        m_status = CType(Value, APVatInputStatus)
      End Set
    End Property
    Public ReadOnly Property Gross() As Decimal
      Get
        Return Me.ItemCollection.Amount
      End Get
    End Property
    Public ReadOnly Property GrossVatAmt() As Decimal
      Get
        Return Me.ItemCollection.Vatamt
      End Get
    End Property
    Public ReadOnly Property Retention() As Decimal
      Get
        Return Me.ItemCollection.GetRetention
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
        Return "APVatInput"
      End Get
    End Property
    Public Overrides ReadOnly Property Prefix() As String
      Get
        Return "pays"
      End Get
    End Property
    Public Overrides ReadOnly Property TableName() As String
      Get
        Return "APVatInput"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.APVatInput.DetailLabel}"
      End Get
    End Property
    Public Overrides ReadOnly Property DetailPanelIcon() As String
      Get
        Return "Icons.16x16.APVatInput"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelIcon() As String
      Get
        Return "Icons.16x16.APVatInput"
      End Get
    End Property
    Public Overrides ReadOnly Property ListPanelTitle() As String
      Get
        Return "${res:Longkong.Pojjaman.BusinessLogic.APVatInput.ListLabel}"
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
      Dim myDatatable As New TreeTable("APVatInput")

      myDatatable.Columns.Add(New DataColumn("paysi_linenumber", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("paysi_entityType", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("Code", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("Button", GetType(String)))

      Dim dateCol As New DataColumn("DocDate", GetType(Date))
      dateCol.DefaultValue = Date.MinValue
      myDatatable.Columns.Add(dateCol)

      dateCol = New DataColumn("DueDate", GetType(Date))
      dateCol.DefaultValue = Date.MinValue
      myDatatable.Columns.Add(dateCol)

      myDatatable.Columns.Add(New DataColumn("RemainAmount", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("TaxBase", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("VatAmt", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("paysi_note", GetType(String)))

      '��������ʴ� error ���������������ҷ���ͧ���
      Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
      myDatatable.Columns("Code").Caption = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.APVatInputDetail.CodeHeaderText}")
      Return myDatatable
    End Function
    Public Shared Function GetRemainingVatAmount(ByVal id As Integer, ByVal entityId As Integer) As Decimal
      Try
        Dim dsr As DataSet = SqlHelper.ExecuteDataset( _
        ConnectionString _
        , CommandType.StoredProcedure _
        , "GetRemainingVatAmount" _
        , New SqlParameter("@docId", id) _
        , New SqlParameter("@docTypeId", entityId) _
        )
        If dsr.Tables(0).Rows.Count > 0 Then
          Return Configuration.Format(CDec(dsr.Tables(0).Rows(0)(0)), DigitConfig.Price)
        End If
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try

      Return 0
    End Function
#End Region

#Region "Methods"
    Private Sub ResetID(ByVal oldid As Integer, ByVal oldje As Integer, ByVal oldVatId As Integer)
      Me.Id = oldid
      Me.m_je.Id = oldje
      Me.m_vat.Id = oldVatId
    End Sub
    Private Sub ResetCode(ByVal oldCode As String, ByVal oldautogen As Boolean, ByVal oldJecode As String, ByVal oldjeautogen As Boolean)
      Me.Code = oldCode
      Me.AutoGen = oldautogen
      Me.m_je.Code = oldJecode
      Me.m_je.AutoGen = oldjeautogen
    End Sub
    Public Function BeforeSave(ByVal currentUserId As Integer) As SaveErrorException

      Dim ValidateError As SaveErrorException

      ValidateError = Me.Vat.BeforeSave(currentUserId)
      If Not IsNumeric(ValidateError.Message) Then
        Return ValidateError
      End If

      ValidateError = Me.JournalEntry.BeforeSave(currentUserId)
      If Not IsNumeric(ValidateError.Message) Then
        Return ValidateError
      End If

      Dim cc As CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
      'If Not cc Is Nothing Then
      '  Me.m_vat.SetCCId(cc.Id)
      'End If
      For Each vitem As VatItem In Me.m_vat.ItemCollection
        If Not vitem Is Nothing AndAlso vitem.CcId = 0 Then
          vitem.CcId = cc.Id
        End If
      Next

      Return New SaveErrorException("0")

    End Function
    Public Overloads Overrides Function Save(ByVal currentUserId As Integer) As SaveErrorException
      With Me

        If Originated Then
          If Not Supplier Is Nothing Then
            If Supplier.Canceled Then
              Return New SaveErrorException(StringParserService.Parse("${res:Global.Error.CanceledSupplier}"), New String() {Supplier.Code})
            End If
          End If
        End If

        If Me.TaxBase <> Vat.TaxBase Then
          Dim obj As Object = Configuration.GetConfig("VatAcceptDiffAmount")
          Dim myMessage As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
          If Me.TaxBase > Vat.TaxBase AndAlso Me.TaxBase - Vat.TaxBase < CDec(obj) Then
            If Me.TaxBase - Vat.TaxBase > 0.01 Then
              If Not myMessage.AskQuestionFormatted(StringParserService.Parse("${res:Global.Error.DiffTaxBaseAndVatTaxBase}"), _
                                          New String() {Configuration.FormatToString(Me.TaxBase, DigitConfig.Price), _
                                                        Configuration.FormatToString(Vat.TaxBase, DigitConfig.Price)}) Then
                Return New SaveErrorException("${res:Global.Error.SaveCanceled}")
              End If
            End If

          ElseIf Me.TaxBase < Vat.TaxBase AndAlso Vat.TaxBase - Me.TaxBase < CDec(obj) Then
            If Not myMessage.AskQuestionFormatted(StringParserService.Parse("${res:Global.Error.DiffTaxBaseAndVatTaxBase}"), _
                                       New String() {Configuration.FormatToString(Me.TaxBase, DigitConfig.Price), _
                                                     Configuration.FormatToString(Vat.TaxBase, DigitConfig.Price)}) Then
              Return New SaveErrorException("${res:Global.Error.SaveCanceled}")
            End If
          Else
            Return New SaveErrorException(StringParserService.Parse("${res:Global.Error.RefTaxBaseAndVatTaxBase}"), _
                                          New String() {Configuration.FormatToString(Me.TaxBase, DigitConfig.Price), _
                                                        Configuration.FormatToString(Vat.TaxBase, DigitConfig.Price)})
          End If
        End If

        'If Me.MaxRowIndex < 0 Then '.ItemTable.Childs.Count = 0 Then
        '    Return New SaveErrorException(Me.StringParserService.Parse("${res:Global.Error.NoItem}"))
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
          paramArrayList.Add(New SqlParameter("@pays_id", Me.Id))
        End If

        Dim theTime As Date = Now
        Dim theUser As New User(currentUserId)

        If Me.m_je.Status.Value = 4 Then
          Me.Status.Value = 4
        End If
        If Me.Status.Value = 0 Then
          Me.m_vat.Status.Value = 0
          Me.m_je.Status.Value = 0
        End If
        If Me.Status.Value = -1 Then
          Me.Status.Value = 2
        End If
        '---- AutoCode Format --------
        Dim oldcode As String
        Dim oldautogen As Boolean
        Dim oldjecode As String
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
              '��� entity
              If Me.AutoGen Then 'And Me.Code.Length = 0 Then
                Me.Code = Me.GetNextCode
              End If
              Me.m_je.Code = Me.Code
            Case 2
              '��� gl
              If Me.m_je.AutoGen Then
                Me.m_je.Code = m_je.GetNextCode
              End If
              Me.Code = Me.m_je.Code
            Case Else
              '�¡
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
        Me.m_je.DocDate = Me.DocDate
        Me.AutoGen = False
        Me.m_je.AutoGen = False
        paramArrayList.Add(New SqlParameter("@pays_code", Me.Code))
        paramArrayList.Add(New SqlParameter("@pays_docDate", Me.ValidDateOrDBNull(Me.DocDate)))
        paramArrayList.Add(New SqlParameter("@pays_supplier", Me.ValidIdOrDBNull(Me.Supplier)))
        paramArrayList.Add(New SqlParameter("@pays_creditPeriod", Me.CreditPeriod))
        paramArrayList.Add(New SqlParameter("@pays_note", Me.Note))
        paramArrayList.Add(New SqlParameter("@pays_gross", Me.Gross))
        paramArrayList.Add(New SqlParameter("@pays_status", Me.Status.Value))

        SetOriginEditCancelStatus(paramArrayList, currentUserId, theTime)

        '---==Validated ��÷� before save �ͧ˹���������� ====
        Dim ValidateError2 As SaveErrorException = Me.BeforeSave(currentUserId)
        If Not IsNumeric(ValidateError2.Message) Then
          ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
          Return ValidateError2
        End If
        '---==Validated ��÷� before save �ͧ˹���������� ====

        ' ���ҧ SqlParameter �ҡ ArrayList ...
        Dim sqlparams() As SqlParameter
        sqlparams = CType(paramArrayList.ToArray(GetType(SqlParameter)), SqlParameter())
        Dim trans As SqlTransaction
        Dim conn As New SqlConnection(Me.ConnectionString)
        conn.Open()
        trans = conn.BeginTransaction()

        Dim oldid As Integer = Me.Id
        Dim oldVatId As Integer = Me.m_vat.Id
        Dim oldje As Integer = Me.m_je.Id

        Try

          Try
            Me.ExecuteSaveSproc(conn, trans, returnVal, sqlparams, theTime, theUser)
            If IsNumeric(returnVal.Value) Then
              Select Case CInt(returnVal.Value)
                Case -1, -2, -5
                  trans.Rollback()
                  Me.ResetID(oldid, oldje, oldVatId)
                  ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                  Return New SaveErrorException(returnVal.Value.ToString)
                Case Else
              End Select
            ElseIf IsDBNull(returnVal.Value) OrElse Not IsNumeric(returnVal.Value) Then
              trans.Rollback()
              Me.ResetID(oldid, oldje, oldVatId)
              ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
              Return New SaveErrorException(returnVal.Value.ToString)
            End If

            SaveDetail(Me.Id, conn, trans)

            'Dim cc As CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
            'If Not cc Is Nothing Then
            '  Me.m_vat.SetCCId(cc.Id)
            'End If
            If Not Me.NoVat Then
              Dim saveVatError As SaveErrorException = Me.m_vat.Save(currentUserId, conn, trans)
              If Not IsNumeric(saveVatError.Message) Then
                trans.Rollback()
                Me.ResetID(oldid, oldje, oldVatId)
                ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                Return saveVatError
              Else
                Select Case CInt(saveVatError.Message)
                  Case -1, -2, -5
                    trans.Rollback()
                    Me.ResetID(oldid, oldje, oldVatId)
                    ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                    Return saveVatError
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
              Me.ResetID(oldid, oldje, oldVatId)
              ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
              Return saveJeError
            Else
              Select Case CInt(saveJeError.Message)
                Case -1, -5
                  trans.Rollback()
                  Me.ResetID(oldid, oldje, oldVatId)
                  ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                  Return saveJeError
                Case -2
                  'Post �����
                  Return saveJeError
                Case Else
              End Select
            End If

            'Me.DeleteRef(conn, trans)
            'SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateGR_APVIRef" _
            ', New SqlParameter("@pays_id", Me.Id)) '�����Թ���
            'SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateAPO_APVIRef" _
            ', New SqlParameter("@pays_id", Me.Id)) '�Ѵ�Ө���
            'SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateEQMaint_APVIRef" _
            ', New SqlParameter("@pays_id", Me.Id)) '�������ا�Թ��Ѿ��
            'SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateADVP_APVIRef" _
            ' , New SqlParameter("@pays_id", Me.Id)) '�Ѵ�Ө���
            'SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePA_APVIRef" _
            ' , New SqlParameter("@pays_id", Me.Id)) '�Ѻ�ҹ


            ''SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePCN_APVIRef" _
            '', New SqlParameter("@pays_id", Me.Id))
            ''SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateBillA_APVIRef" _
            '', New SqlParameter("@pays_id", Me.Id))


            'If Me.Status.Value = 0 Then
            '  Me.CancelRef(conn, trans)
            'End If
            '==============================AUTOGEN==========================================
            Dim saveAutoCodeError As SaveErrorException = SaveAutoCode(conn, trans)
            If Not IsNumeric(saveAutoCodeError.Message) Then
              trans.Rollback()
              ResetID(oldid, oldje, oldVatId)
              ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
              Return saveAutoCodeError
            Else
              Select Case CInt(saveAutoCodeError.Message)
                Case -1, -2, -5
                  trans.Rollback()
                  ResetID(oldid, oldje, oldVatId)
                  ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
                  Return saveAutoCodeError
                Case Else
              End Select
            End If
            '==============================AUTOGEN==========================================

            trans.Commit()
            'Return New SaveErrorException(returnVal.Value.ToString)
          Catch ex As SqlException
            trans.Rollback()
            Me.ResetID(oldid, oldje, oldVatId)
            ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
            Return New SaveErrorException(ex.ToString)
          Catch ex As Exception
            trans.Rollback()
            Me.ResetID(oldid, oldje, oldVatId)
            ResetCode(oldcode, oldautogen, oldjecode, oldjeautogen)
            Return New SaveErrorException(ex.ToString)
            'Finally
            '  conn.Close()
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
          '--Sub Save Block-- ============================================================

        Catch ex As Exception
          Return New SaveErrorException(ex.ToString)
        Finally
          conn.Close()
        End Try

      End With
    End Function
    Private Function SubSave(ByVal conn As SqlConnection) As SaveErrorException

      '======����� trans 2 �ͧ�Դ��� save ���� ========
      Dim trans As SqlTransaction = conn.BeginTransaction

      Try
        Me.DeleteRef(conn, trans)
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateGR_APVIRef" _
        , New SqlParameter("@pays_id", Me.Id)) '�����Թ���
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateAPO_APVIRef" _
        , New SqlParameter("@pays_id", Me.Id)) '�Ѵ�Ө���
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateEQMaint_APVIRef" _
        , New SqlParameter("@pays_id", Me.Id)) '�������ا�Թ��Ѿ��
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdateADVP_APVIRef" _
         , New SqlParameter("@pays_id", Me.Id)) '�Ѵ�Ө���
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "UpdatePA_APVIRef" _
         , New SqlParameter("@pays_id", Me.Id)) '�Ѻ�ҹ

        If Me.Status.Value = 0 Then
          Me.CancelRef(conn, trans)
        End If
      Catch ex As Exception
        trans.Rollback()
        Return New SaveErrorException(ex.ToString)
      End Try

      trans.Commit()
      Return New SaveErrorException("0")
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
      Dim da As New SqlDataAdapter("Select * from APVatInputItem where paysi_pays=" & Me.Id, conn)
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
      da.FillSchema(ds, SchemaType.Mapped, "APVatInputItem")
      da.Fill(ds, "APVatInputItem")

      Dim dt As DataTable = ds.Tables("APVatInputItem")

      Dim i As Integer = 0
      With ds.Tables("APVatInputItem")
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
          dr("paysi_billedamt") = billi.BilledAmount
          dr("paysi_unpaidamt") = billi.UnpaidAmount
          dr("stock_beforetax") = billi.BeforeTax
          dr("stock_aftertax") = billi.AfterTax
          dr("stock_taxBase") = billi.TaxBase
          dr("stock_note") = billi.Note
          dr("stock_status") = Me.Status.Value
          dr("stock_retention") = billi.Retention
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
      Dim dummyCCId As Integer = 0
      For Each item As BillAcceptanceItem In Me.ItemCollection
        Dim ccId As Integer = item.CostCenterId
        If dummyCCId <> 0 AndAlso dummyCCId <> ccId Then
          Return CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
        End If
        dummyCCId = ccId
      Next
      Return New CostCenter(dummyCCId)
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
      , New SqlParameter("@entity_id", 73), New SqlParameter("@default", 1))
      Dim glf As New GLFormat(ds.Tables(0).Rows(0), "")
      Return glf
    End Function
    Public Function GetJournalEntries() As JournalEntryItemCollection Implements IGLAble.GetJournalEntries
      Dim jiColl As New JournalEntryItemCollection
      Dim ji As JournalEntryItem
      Dim myCC As CostCenter = GetCCFromItem()
      If myCC Is Nothing OrElse Not myCC.Originated Then
        myCC = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
      End If
      '���ի���
      If Me.Vat.Amount > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "B8.4"
        ji.Amount = Configuration.Format(Me.Vat.Amount, DigitConfig.Price)
        ji.CostCenter = myCC
        jiColl.Add(ji)
      End If

      '���ի������֧��˹�
      If Me.GrossVatAmt > 0 Then
        ji = New JournalEntryItem
        ji.Mapping = "B8.5"
        ji.Amount = Configuration.Format(Me.GrossVatAmt, DigitConfig.Price)
        ji.CostCenter = myCC
        ji.EntityItem = Me.Id
        ji.EntityItemType = Me.EntityId
        jiColl.Add(ji)
      End If
      '----�¡��������´
      For Each vati As VatItem In Vat.ItemCollection
        If vati.Amount > 0 Then
          ji = New JournalEntryItem
          ji.Mapping = "B8.4D"
          ji.Amount = Configuration.Format(vati.Amount, DigitConfig.Price)
          'If vati.CcId <= 0 Then
          ji.CostCenter = myCC
          'Else
          '  ji.CostCenter = vati.CostCenter
          'End If
          ji.EntityItem = Me.Id
          ji.EntityItemType = Me.EntityId
          ji.Note = vati.Code & ":" & vati.Runnumber & ":" & vati.PrintName
          jiColl.Add(ji)
        End If
      Next

      For Each apvi As BillAcceptanceItem In Me.ItemCollection
        If apvi.Amount > 0 Then
          ji = New JournalEntryItem
          ji.Mapping = "B8.5D"
          'ji.Amount = Configuration.Format(apvi.TaxAmountDeducted, DigitConfig.Price)
          '����¹�繵ç��� ���е�ͧ��
          ji.Amount = Configuration.Format(apvi.VatAmt, DigitConfig.Price)
          'If apvi.CostCenterId <= 0 Then
          ji.CostCenter = myCC
          'Else
          '  ji.CostCenter = New CostCenter(apvi.CostCenterId)
          'End If
          ji.EntityItem = apvi.Id
          ji.EntityItemType = apvi.EntityId
          ji.Note = apvi.Code & ":" & apvi.itemType & "/" & Me.Supplier.Name
          jiColl.Add(ji)
        End If
      Next

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

#Region "IPrintableEntity"
    Public Function GetDefaultFormPath() As String Implements IPrintableEntity.GetDefaultFormPath
      Return "APVatInput"
    End Function
    Public Function GetDefaultForm() As String Implements IPrintableEntity.GetDefaultForm
      Return "APVatInput"
    End Function
    Public Function GetDocPrintingEntries() As DocPrintingItemCollection Implements IPrintableEntity.GetDocPrintingEntries
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      'Pays_id
      dpi = New DocPrintingItem
      dpi.Mapping = "pays_id"
      dpi.Value = Me.Id
      dpi.DataType = "System.Integer"
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
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      If Me.Supplier IsNot Nothing Then
        'Supplier Code
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierCode"
        dpi.Value = Me.Supplier.Code
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'Supplier Name
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierName"
        dpi.Value = Me.Supplier.Name
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)

        'Supplier Info
        dpi = New DocPrintingItem
        dpi.Mapping = "SupplierInfo"
        dpi.Value = String.Format("{0}:{1}", Me.Supplier.Code, Me.Supplier.Name)
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)
      End If

      Dim line As Integer
      Dim sumRemainingAmount As Decimal
      Dim sumTaxBase As Decimal
      Dim sumVatAmount As Decimal
      Dim itemCodeList As New ArrayList
      For Each bi As BillAcceptanceItem In Me.ItemCollection
        'Paysi_pays
        dpi = New DocPrintingItem
        dpi.Mapping = "Paysi_pays"
        dpi.Value = Me.Id
        dpi.DataType = "System.Integer"
        dpiColl.Add(dpi)

        'LineNumber
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.LineNumber"
        dpi.Value = line + 1
        dpi.DataType = "System.Integer"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        'DocType
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.DocType"
        dpi.Value = bi.itemType
        dpi.DataType = "System.String"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        'DocCode
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.Code"
        dpi.Value = bi.Code
        dpi.DataType = "System.String"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        'DocDate
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.DocDate"
        dpi.Value = bi.Date.ToShortDateString
        dpi.DataType = "System.String"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        'DueDate
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.DueDate"
        dpi.Value = bi.DueDate.ToShortDateString
        dpi.DataType = "System.String"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        'RemainingAmount
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.RemainingAmount"
        dpi.Value = Configuration.FormatToString(bi.UnpaidAmount, DigitConfig.Price)
        dpi.DataType = "System.String"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        'TaxBase
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.TaxBase"
        dpi.Value = Configuration.FormatToString(bi.Amount, DigitConfig.Price)
        dpi.DataType = "System.String"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        'VatAmount
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.VatAmount"
        dpi.Value = Configuration.FormatToString(bi.VatAmt, DigitConfig.Price)
        dpi.DataType = "System.String"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        'Note
        dpi = New DocPrintingItem
        dpi.Mapping = "Item.Note"
        dpi.Value = bi.Note
        dpi.DataType = "System.String"
        dpi.Table = "Item"
        dpiColl.Add(dpi)

        sumRemainingAmount += bi.UnpaidAmount
        sumTaxBase += bi.Amount
        sumVatAmount += bi.VatAmt

        itemCodeList.Add(bi.Code)

        line += 1
      Next

      'ItemCount
      dpi = New DocPrintingItem
      dpi.Mapping = "ItemCount"
      dpi.Value = line
      dpi.DataType = "System.Integer"
      dpiColl.Add(dpi)

      'SummaryRemainingAmount
      dpi = New DocPrintingItem
      dpi.Mapping = "SummaryRemainingAmount"
      dpi.Value = Configuration.FormatToString(sumRemainingAmount, DigitConfig.Price)
      dpi.DataType = "System.Decimal"
      dpiColl.Add(dpi)

      'SummaryTaxbase
      dpi = New DocPrintingItem
      dpi.Mapping = "SummaryTaxbase"
      dpi.Value = Configuration.FormatToString(sumTaxBase, DigitConfig.Price)
      dpi.DataType = "System.Decimal"
      dpiColl.Add(dpi)

      'SummaryVatAmount
      dpi = New DocPrintingItem
      dpi.Mapping = "SummaryVatAmount"
      dpi.Value = Configuration.FormatToString(sumVatAmount, DigitConfig.Price)
      dpi.DataType = "System.Decimal"
      dpiColl.Add(dpi)

      'ItemCodeList
      dpi = New DocPrintingItem
      dpi.Mapping = "ItemCodeList"
      dpi.Value = String.Join(", ", itemCodeList.ToArray)
      dpi.DataType = "System.Decimal"
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
    Public Overrides Function Delete() As SaveErrorException
      If Not Me.Originated Then
        Return New SaveErrorException("${res:Global.Error.NoIdError}")
      End If
      Dim myMessage As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      Dim format(0) As String
      format(0) = Me.Code
      If Not myMessage.AskQuestionFormatted("${res:Global.ConfirmDeleteAPVatInput}", format) Then
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
        SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "DeleteAPVatInput", New SqlParameter() {New SqlParameter("@pays_id", Me.Id), returnVal})
        If IsNumeric(returnVal.Value) Then
          Select Case CInt(returnVal.Value)
            Case -1
              trans.Rollback()
              Return New SaveErrorException("${res:Global.APVatInputIsReferencedCannotBeDeleted}")
            Case Else
          End Select
        ElseIf IsDBNull(returnVal.Value) OrElse Not IsNumeric(returnVal.Value) Then
          trans.Rollback()
          Return New SaveErrorException(returnVal.Value.ToString)
        End If
        Me.DeleteRef(conn, trans)
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
    Public Property Person() As IBillablePerson Implements IVatable.Person
      Get
        Return Me.Supplier
      End Get
      Set(ByVal Value As IBillablePerson)
        Me.Supplier = CType(Value, Supplier)
      End Set
    End Property
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
          If item.EntityId = 59 Then
            Dim tb As Decimal = 0
            If item.TaxBase > 0 AndAlso item.BeforeTax <> item.TaxBase Then
              tb = item.TaxBase
            Else
              tb = item.BeforeTax
            End If
            vitem.TaxBase = tb - item.DeductTaxBase
            vitem.Amount = item.TaxAmount - item.DeductVatAmt
          ElseIf item.EntityId <> 46 Then
            vitem.TaxBase = item.TaxBase - item.DeductTaxBase
            vitem.Amount = item.TaxAmount - item.DeductVatAmt
          ElseIf item.EntityId = 46 Then
            vitem.TaxBase = -(item.TaxBase - item.DeductTaxBase)
            vitem.Amount = -(item.TaxAmount - item.DeductVatAmt)
          End If
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
    Private Function GetTaxBase() As Decimal
      Dim amt As Decimal
      For Each item As BillAcceptanceItem In Me.ItemCollection
        If item.TaxType.Value <> 0 Then
          If item.EntityId <> 46 Then
            amt += item.Amount
          Else
            amt -= item.Amount
          End If
          'amt += item.TaxBase - Vat.GetTaxBaseDeductedWithoutThisRefDoc(item.Id, item.EntityId, Me.Id, Me.EntityId)
        End If
      Next
      Return amt
    End Function
    Public Function GetMaximumTaxBase(Optional ByVal conn As SqlConnection = Nothing, Optional ByVal trans As SqlTransaction = Nothing) As Decimal Implements IVatable.GetMaximumTaxBase
      Return GetTaxBase()
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

    Public Function GetDocPrintingColumnsEntries() As DocPrintingItemCollection Implements INewPrintableEntity.GetDocPrintingColumnsEntries
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      dpiColl.RelationList.Add("general>pays_id>Item>paysi_pay")

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("pays_id", "System.Integer"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Code", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("DocDate", "System.DateTime"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierCode", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierName", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SupplierInfo", "System.String"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("ItemCount", "System.Integer"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SummaryRemainingAmount", "System.Decimal"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SummaryTaxbase", "System.Decimal"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("SummaryVatAmount", "System.Decimal"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("ItemCodeList", "System.String"))

      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("paysi_pay", "System.Integer", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.LineNumber", "System.Integer", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.DocType", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.Code", "System.String", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.DocDate", "System.DateTime", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.DueDate", "System.DateTime", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.RemainingAmount", "System.Decimal", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.TaxBase", "System.Decimal", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.VatAmount", "System.Decimal", "Item"))
      dpiColl.Add(EntitySimpleSchema.NewDocPrintingItem("Item.Note", "System.String", "Item"))

      Return dpiColl
    End Function

    Public Function GetDocPrintingDataEntries() As DocPrintingItemCollection Implements INewPrintableEntity.GetDocPrintingDataEntries
      Return Me.GetDocPrintingEntries
    End Function
  End Class

  Public Class GoodsReceiptForVat
    Inherits GoodsReceipt

    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "GoodsReceiptForVat"
      End Get
    End Property
    Public Overrides ReadOnly Property Columns() As ColumnCollection
      Get
        'If m_columns Is Nothing OrElse m_columns.Count <= 0 Then
        'm_columns = New ColumnCollection(Me.ClassName, 0)
        'End If
        'Return m_columns
        Return New ColumnCollection(Me.ClassName, 0)
      End Get
    End Property
  End Class
  Public Class PurchaseCNForVat
    Inherits PurchaseCN

    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "PurchaseCNForVat"
      End Get
    End Property
  End Class
  Public Class APOpeningBalanceForVat
    Inherits APOpeningBalance

    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "APOpeningBalanceForVat"
      End Get
    End Property
  End Class
  Public Class EqMaintenanceForVat
    Inherits EqMaintenance

    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "EqMaintenanceForVat"
      End Get
    End Property
  End Class
  Public Class AdvancePayForVat
    Inherits AdvancePay

    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "AdvancePayForVat"
      End Get
    End Property

  End Class
  Public Class PAForVat
    Inherits PA

    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "PAForVat"
      End Get
    End Property
    Public Overrides ReadOnly Property Prefix As String
      Get
        Return "stock"
      End Get
    End Property
  End Class
  Public Class AdvancePayClosedForVat
    Inherits AdvancePayClosed

    Public Overrides ReadOnly Property ClassName As String
      Get
        Return "AdvancePayClosedForVat"
      End Get
    End Property

  End Class
End Namespace

