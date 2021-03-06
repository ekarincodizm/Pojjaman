Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports Longkong.Core.Services

Namespace Longkong.Pojjaman.BusinessLogic
    Public Class CashWithdraw
        Inherits Banking

#Region "Member"
        
#End Region

#Region "Constructs"
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal id As Integer)
            MyBase.New(id)
        End Sub
        Public Sub New(ByVal code As String)
            MyBase.New(code)
        End Sub
        Protected Overloads Overrides Sub Construct()
            MyBase.Construct()
            Me.BankingTransType = New BankingTransType(2)
        End Sub
        Protected Overloads Overrides Sub Construct(ByVal ds As System.Data.DataSet, ByVal aliasPrefix As String)
            MyBase.Construct(ds, aliasPrefix)
        End Sub
        Protected Overloads Overrides Sub Construct(ByVal dr As System.Data.DataRow, ByVal aliasPrefix As String)
            MyBase.Construct(dr, aliasPrefix)
        End Sub
#End Region

#Region "Overrides"
        Public Overrides ReadOnly Property ClassName() As String
            Get
                Return "cashwithdraw"
            End Get
        End Property
        
        Public Overrides ReadOnly Property GetSprocName() As String
            Get
                Return "Get" & Me.ClassName
            End Get
        End Property

        Public Overrides ReadOnly Property DetailPanelTitle() As String
            Get
                Return "${res:Longkong.Pojjaman.BusinessLogic.CashWithdraw.DetailLabel}"
            End Get
        End Property
        Public Overrides ReadOnly Property DetailPanelIcon() As String
            Get
                Return "Icons.16x16.CashWithdraw"
            End Get
        End Property
        Public Overrides ReadOnly Property ListPanelIcon() As String
            Get
                Return "Icons.16x16.CashWithdraw"
            End Get
        End Property
        Public Overrides ReadOnly Property ListPanelTitle() As String
            Get
                Return "${res:Longkong.Pojjaman.BusinessLogic.CashWithdraw.ListLabel}"
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

#Region " IGLAble Overrides "
        
        Public Overrides Function GetJournalEntries() As JournalEntryItemCollection
            Dim jiColl As New JournalEntryItemCollection
            Dim ji As New JournalEntryItem
            '  DR. �Թʴ                  ji.Mapping = "G4.1"
            SetGLG4_1(jiColl)

            '  CR. �Թ�ҡ��Ҥ��     ji.Mapping = "G4.2"
            SetGLG4_2(jiColl)
            
            Return jiColl
        End Function
        ' DR. �Թʴ
        Private Sub SetGLG4_1(ByVal jiColl As JournalEntryItemCollection)
            If Me.Amount > 0 Then
                Dim ji As New JournalEntryItem
                ji = New JournalEntryItem
                ji.Amount = Me.Amount
                ji.Mapping = "G4.1"
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                jiColl.Add(ji)
            End If
        End Sub
        ' CR. �Թ�ҡ��Ҥ��
        Private Sub SetGLG4_2(ByVal jiColl As JournalEntryItemCollection)
            If Me.Amount > 0 Then
                Dim ji As New JournalEntryItem
                ji = New JournalEntryItem
                ji.Amount = Me.Amount
                ji.Mapping = "G4.2"
                If Me.Bankacct.Account.Originated Then
                    ji.Account = Me.Bankacct.Account
                End If
                ji.CostCenter = CostCenter.GetDefaultCostCenter(CostCenter.DefaultCostCenterType.HQ)
                jiColl.Add(ji)
            End If
        End Sub

#End Region

#Region "Delete"
        Public Overrides ReadOnly Property CanDelete() As Boolean
            Get
                If Me.Originated Then
                    Return Me.Status.Value <= 2
                Else
                    Return False
                End If
            End Get
        End Property
        Public Overrides Function Delete() As SaveErrorException
            If Not Me.Originated Then
                Return New SaveErrorException("${res:Global.Error.NoIdError}")
            End If
            Dim myMessage As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
            Dim format(0) As String
            format(0) = Me.Code
            If Not myMessage.AskQuestionFormatted("${res:Global.ConfirmDeleteCashWithdraw}", format) Then
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
                SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "DeleteCashWithdraw", New SqlParameter() {New SqlParameter("@" & Me.Prefix & "_id", Me.Id), returnVal})
                If IsNumeric(returnVal.Value) Then
                    Select Case CInt(returnVal.Value)
                        Case -1
                            trans.Rollback()
                            Return New SaveErrorException("${res:Global.CashWithdrawIsReferencedCannotBeDeleted}")
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
End Namespace
