Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.Gui.Components
Imports System.Data.SqlClient
Imports System.IO
Imports System.ComponentModel
Imports System.Configuration
Imports System.Reflection
Imports Longkong.Core.Services
Namespace Longkong.Pojjaman.BusinessLogic
    Public Class FormFormat
        Inherits SimpleBusinessEntityBase
        Implements IHasName

#Region "Members"
        Private m_name As String
        Private m_note As String

        Private m_form As LinkForm
        Private m_isDefault As Boolean

        Private m_itemTable As TreeTable

#End Region

#Region "Constructors"
        Public Sub New()
            MyBase.New()
            ReLoadItems()
            Wire()
        End Sub
        Public Sub New(ByVal lgl As LinkForm)
            MyBase.New()
            Me.LinkForm = lgl
            ReLoadItems()
            Wire()
        End Sub
        Public Sub New(ByVal id As Integer)
            MyBase.New(id)
            ReLoadItems()
            Wire()
        End Sub
        Public Sub New(ByVal code As String)
            MyBase.New(code)
            ReLoadItems()
            Wire()
        End Sub
        Public Sub New(ByVal dr As DataRow, ByVal aliasPrefix As String)
            Me.Construct(dr, aliasPrefix)
            ReLoadItems()
            Wire()
        End Sub
        Public Sub UnWire()
            RemoveHandler m_itemTable.ColumnChanging, AddressOf Treetable_ColumnChanging
            RemoveHandler m_itemTable.ColumnChanged, AddressOf Treetable_ColumnChanged
            RemoveHandler m_itemTable.RowDeleted, AddressOf ItemDelete
        End Sub
        Public Sub Wire()
      'WrapperArrayList.AddItemAddedHandler(m_itemTable, AddressOf ItemAdded)
            AddHandler m_itemTable.ColumnChanging, AddressOf Treetable_ColumnChanging
            AddHandler m_itemTable.ColumnChanged, AddressOf Treetable_ColumnChanged
            AddHandler m_itemTable.RowDeleted, AddressOf ItemDelete
        End Sub
        Protected Overloads Overrides Sub Construct()
            m_form = New LinkForm
        End Sub
        Protected Overloads Overrides Sub Construct(ByVal dr As System.Data.DataRow, ByVal aliasPrefix As String)
            MyBase.Construct(dr, aliasPrefix)
            With Me
                If dr.Table.Columns.Contains(aliasPrefix & Me.Prefix & "_name") AndAlso Not dr.IsNull(aliasPrefix & Me.Prefix & "_name") Then
                    .m_name = CStr(dr(aliasPrefix & Me.Prefix & "_name"))
                End If
                If dr.Table.Columns.Contains(aliasPrefix & Me.Prefix & "_note") AndAlso Not dr.IsNull(aliasPrefix & Me.Prefix & "_note") Then
                    .m_note = CStr(dr(aliasPrefix & Me.Prefix & "_note"))
                End If
                If dr.Table.Columns.Contains(aliasPrefix & "form_id") Then
                    If Not dr.IsNull(aliasPrefix & "form_id") Then
                        .m_form = New LinkForm(dr, "")
                    End If
                Else
                    If dr.Table.Columns.Contains(aliasPrefix & Me.Prefix & "_form") AndAlso Not dr.IsNull(aliasPrefix & Me.Prefix & "_form") Then
                        .m_form = New LinkForm(CInt(dr(aliasPrefix & Me.Prefix & "_form")))
                    End If
                End If
                If dr.Table.Columns.Contains(aliasPrefix & Me.Prefix & "_isdefault") AndAlso Not dr.IsNull(aliasPrefix & Me.Prefix & "_isdefault") Then
                    .m_isDefault = CBool(dr(aliasPrefix & Me.Prefix & "_isdefault"))
                End If
            End With
        End Sub
#End Region

#Region "Properties"
        Public Property ItemTable() As TreeTable            Get                Return m_itemTable            End Get            Set(ByVal Value As TreeTable)                m_itemTable = Value            End Set        End Property
        Public Property LinkForm() As LinkForm            Get                Return m_form            End Get            Set(ByVal Value As LinkForm)                m_form = Value            End Set        End Property
        Public Property IsDefault() As Boolean            Get                Return m_isDefault            End Get            Set(ByVal Value As Boolean)                m_isDefault = Value            End Set        End Property        Public Property Note() As String            Get                Return m_note            End Get            Set(ByVal Value As String)                m_note = Value            End Set        End Property        Public Overrides ReadOnly Property ClassName() As String
            Get
                Return "FormFormat"
            End Get
        End Property
        Public Overrides ReadOnly Property DetailPanelTitle() As String
            Get
                Return "${res:Longkong.Pojjaman.BusinessLogic.FormFormat.DetailLabel}"
            End Get
        End Property
        Public Overrides ReadOnly Property DetailPanelIcon() As String
            Get
                Return "Icons.16x16.FormFormat"
            End Get
        End Property
        Public Overrides ReadOnly Property ListPanelIcon() As String
            Get
                Return "Icons.16x16.FormFormat"
            End Get
        End Property
        Public Overrides ReadOnly Property ListPanelTitle() As String
            Get
                Return "${res:Longkong.Pojjaman.BusinessLogic.FormFormat.ListLabel}"
            End Get
        End Property
        Public Overrides ReadOnly Property Prefix() As String
            Get
                Return "formf"
            End Get
        End Property
        Public Overrides ReadOnly Property TabPageText() As String
            Get
                Dim tpt As String = Me.StringParserService.Parse(Me.DetailPanelTitle) & " (" & Me.Name & ")"
                If tpt.EndsWith("()") Then
                    tpt = tpt.TrimEnd("()".ToCharArray)
                End If
                Return tpt
            End Get
        End Property
#End Region

#Region "Shared"
        Public Shared Function CreateTableStyle() As DataGridTableStyle
            Dim dst As New DataGridTableStyle
            dst.MappingName = "FormFormat"
            Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)

            Dim csLineNumber As New TreeTextColumn
            csLineNumber.MappingName = "formfi_linenumber"
            csLineNumber.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.LinkFormDetail.LineNumberHeaderText}")
            csLineNumber.NullText = ""
            csLineNumber.Width = 30
            csLineNumber.DataAlignment = HorizontalAlignment.Center
            csLineNumber.ReadOnly = True
            csLineNumber.TextBox.Name = "formfi_linenumber"

            Dim csName As New TreeTextColumn
            csName.MappingName = "Name"
            csName.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.LinkFormDetail.DescriptionHeaderText}")
            csName.NullText = ""
            csName.Width = 180
            csName.TextBox.Name = "Description"
            csName.ReadOnly = True

            Dim csNote As New TreeTextColumn
            csNote.MappingName = "Note"
            csNote.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.LinkFormDetail.NoteHeaderText}")
            csNote.NullText = ""
            csNote.Width = 180
            csNote.TextBox.Name = "Note"
            csNote.ReadOnly = True

            Dim csMapping As New TreeTextColumn
            csMapping.MappingName = "formfi_mapping"
            csMapping.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.LinkFormDetail.MappingHeaderText}")
            csMapping.NullText = ""
            csMapping.Width = 180
            csMapping.TextBox.Name = "formfi_mapping"
            csMapping.ReadOnly = True

            Dim csTable As New TreeTextColumn
            csTable.MappingName = "formfi_table"
            csTable.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.LinkFormDetail.TableHeaderText}")
            csTable.NullText = ""
            csTable.Width = 180
            csTable.TextBox.Name = "formfi_table"
            csTable.ReadOnly = True

            dst.GridColumnStyles.Add(csLineNumber)
            dst.GridColumnStyles.Add(csName)
            dst.GridColumnStyles.Add(csNote)
            dst.GridColumnStyles.Add(csMapping)
            dst.GridColumnStyles.Add(csTable)

            Return dst
        End Function
        Public Shared Function GetSchemaTable() As TreeTable
            Dim myDatatable As New TreeTable("FormFormat")
            myDatatable.Columns.Add(New DataColumn("formfi_linenumber", GetType(String)))
            myDatatable.Columns.Add(New DataColumn("Name", GetType(String)))
            myDatatable.Columns.Add(New DataColumn("Note", GetType(String)))
            myDatatable.Columns.Add(New DataColumn("formfi_mapping", GetType(String)))
            myDatatable.Columns.Add(New DataColumn("formfi_table", GetType(String)))
            Return myDatatable
        End Function
#End Region

#Region "Methods"
        Public Function GetFieldRows() As DataRow()
            Return Me.ItemTable.Select("formfi_table is null", "formfi_linenumber")
        End Function
        Public Function GetFieldRows(ByVal tbn As String) As DataRow()
            Return Me.ItemTable.Select("formfi_table='" & tbn & "'", "formfi_linenumber")
        End Function
        Public Overrides Function ToString() As String
            Return m_name
        End Function
        Public Overrides Function Delete() As SaveErrorException
            If Not Me.Originated Then
                Return New SaveErrorException("Error No id")
            End If
            Dim trans As SqlTransaction
            Dim conn As New SqlConnection(Me.ConnectionString)
            conn.Open()
            trans = conn.BeginTransaction()
            Try
                Dim cmd As New SqlCommand("delete formformatitem where formfi_formf=" & Me.Id)
                cmd.Connection = conn
                cmd.Transaction = trans
                cmd.ExecuteNonQuery()
                cmd.CommandText = "delete formformat where formf_id=" & Me.Id
                cmd.ExecuteNonQuery()
                trans.Commit()
                Return New SaveErrorException("1")
            Catch ex As Exception
                trans.Rollback()
                Return New SaveErrorException(ex.Message)
            Finally
                conn.Close()
            End Try
        End Function
        Public Overloads Overrides Function Save(ByVal currentUserId As Integer) As SaveErrorException
            With Me

                Dim returnVal As System.Data.SqlClient.SqlParameter = New SqlParameter
                returnVal.ParameterName = "RETURN_VALUE"
                returnVal.DbType = DbType.Int32
                returnVal.Direction = ParameterDirection.ReturnValue
                returnVal.SourceVersion = DataRowVersion.Current

                ' ���ҧ ArrayList �ҡ Item �ͧ  SqlParameter ...
                Dim paramArrayList As New ArrayList

                paramArrayList.Add(returnVal)
                If Me.Originated Then
                    paramArrayList.Add(New SqlParameter("@formf_id", Me.Id))
                End If

                Dim theTime As Date = Now
                Dim theUser As New User(currentUserId)

                paramArrayList.Add(New SqlParameter("@formf_code", Me.Code))
                paramArrayList.Add(New SqlParameter("@formf_form", Me.ValidIdOrDBNull(Me.LinkForm)))
                paramArrayList.Add(New SqlParameter("@formf_name", Me.Name))
                paramArrayList.Add(New SqlParameter("@formf_note", Me.Note))
                paramArrayList.Add(New SqlParameter("@formf_isdefault", Me.IsDefault))

                SetOriginEditCancelStatus(paramArrayList, currentUserId, theTime)

                ' ���ҧ SqlParameter �ҡ ArrayList ...
                Dim sqlparams() As SqlParameter
                sqlparams = CType(paramArrayList.ToArray(GetType(SqlParameter)), SqlParameter())
                Dim trans As SqlTransaction
                Dim conn As New SqlConnection(Me.ConnectionString)
                conn.Open()
                trans = conn.BeginTransaction()
                Try
                    Me.ExecuteSaveSproc(conn, trans, returnVal, sqlparams, theTime, theUser)
                    SaveDetail(Me.Id, conn, trans)
                    trans.Commit()
                    Return New SaveErrorException(returnVal.Value.ToString)
                Catch ex As SqlException
                    trans.Rollback()
                    Return New SaveErrorException(ex.ToString)
                Catch ex As Exception
                    trans.Rollback()
                    Return New SaveErrorException(ex.ToString)
                Finally
                    conn.Close()
                End Try
            End With
        End Function
        Private Function SaveDetail(ByVal parentID As Integer, ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As Integer

            Dim da As New SqlDataAdapter("Select * from formformatitem where formfi_formf=" & Me.Id, conn)
            Dim cmdBuilder As New SqlCommandBuilder(da)

            Dim ds As New DataSet

            da.SelectCommand.Transaction = trans

            '��ͧ�����ͨҡ da.SelectCommand.Transaction = trans
            cmdBuilder.GetDeleteCommand.Transaction = trans
            cmdBuilder.GetInsertCommand.Transaction = trans
            cmdBuilder.GetUpdateCommand.Transaction = trans

            da.Fill(ds, "FormFormatitem")
            With ds.Tables("FormFormatitem")
                For Each row As DataRow In .Rows
                    row.Delete()
                Next
                Dim defaultDS As DataSet = SqlHelper.ExecuteDataset(Me.ConnectionString _
                , CommandType.StoredProcedure _
                , "GetDefaultFormFormatItems" _
                , New SqlParameter("@form_id", Me.LinkForm.Id) _
                )
                Dim i As Integer = 0
                For Each row As DataRow In defaultDS.Tables(0).Rows
                    Dim item As New FormFormatItem(row, "")
                    item.FormFormat = Me

                    Dim dr As DataRow = .NewRow
                    dr("formfi_formf") = Me.Id
                    dr("formfi_linenumber") = i + 1 'item("gli_linenumber")
                    dr("formfi_fieldName") = item.FieldName
                    dr("formfi_mapping") = item.Mapping
                    dr("formfi_note") = item.Note
                    dr("formfi_table") = item.TableName
                    .Rows.Add(dr)
                    i += 1
                Next
            End With
            Dim dt As DataTable = ds.Tables("FormFormatitem")
            ' First process deletes.
            da.Update(dt.Select(Nothing, Nothing, DataViewRowState.Deleted))
            ' Next process updates.
            da.Update(dt.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent))
            ' Finally process inserts.
            da.Update(dt.Select(Nothing, Nothing, DataViewRowState.Added))
        End Function
#End Region

#Region "IHasName"
        Public Property Name() As String Implements IHasName.Name
            Get
                Return m_name
            End Get
            Set(ByVal Value As String)
                m_name = Value
                OnTabPageTextChanged(Me, New EventArgs)
            End Set
        End Property
#End Region

#Region "Items"
        Public Overloads Sub ReLoadItems()
            Me.IsInitialized = False
            m_itemTable = GetSchemaTable()
            LoadItems()
            Me.IsInitialized = True
        End Sub
        Private Sub LoadItems()
            If Not Me.Originated Then
                If Not Me.LinkForm.Originated Then
                    Return
                End If
                Dim ds As DataSet = SqlHelper.ExecuteDataset(Me.ConnectionString _
                , CommandType.StoredProcedure _
                , "GetDefaultFormFormatItems" _
                , New SqlParameter("@form_id", Me.LinkForm.Id) _
                )

                For Each row As DataRow In ds.Tables(0).Rows
                    Dim item As New FormFormatItem(row, "")
                    item.FormFormat = Me
                    Me.Add(item)
                Next
            Else
                Dim ds2 As DataSet = SqlHelper.ExecuteDataset(Me.ConnectionString _
                , CommandType.StoredProcedure _
                , "GetFormFormatItems" _
                , New SqlParameter("@formf_id", Me.Id) _
                )

                For Each row As DataRow In ds2.Tables(0).Rows
                    Dim item As New FormFormatItem(row, "")
                    item.FormFormat = Me
                    Me.Add(item)
                Next
            End If

        End Sub
        Public Function Add(ByVal item As FormFormatItem) As TreeRow
            Dim myRow As TreeRow = Me.ItemTable.Childs.Add
            item.LineNumber = Me.ItemTable.Childs.Count
            item.CopyToDataRow(myRow)
            Return myRow
        End Function
#End Region

#Region "TreeTable Handlers"
        Private Sub Treetable_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
            If Not Me.IsInitialized Then
                Return
            End If
            'Hack: hard-coded
            If IsDBNull(e.Row("formfi_fieldtype")) OrElse CInt(e.Row("formfi_fieldtype")) = 2 OrElse CInt(e.Row("formfi_fieldtype")) = 3 Then
                Return
            End If
            Dim index As Integer = Me.m_itemTable.Childs.IndexOf(CType(e.Row, TreeRow))
            If ValidateRow(CType(e.Row, TreeRow)) Then
                Dim pe As New PropertyChangedEventArgs("ItemChanged", "", "")
                Me.OnPropertyChanged(Me, pe)
                Me.m_itemTable.AcceptChanges()
            End If
        End Sub
        Private Sub Treetable_ColumnChanging(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
            If Not Me.IsInitialized Then
                Return
            End If
            Try
                Select Case e.Column.ColumnName.ToLower
                    Case "acctcode"
                        'Hack: hard-coded
                        If IsDBNull(e.Row("formfi_fieldtype")) OrElse CInt(e.Row("formfi_fieldtype")) = 2 OrElse CInt(e.Row("formfi_fieldtype")) = 3 Then
                            e.ProposedValue = e.Row("acctcode")
                            Return
                        End If
                        SetAccountValue(e)
                End Select
                ValidateRow(e)
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        Public Sub ValidateRow(ByVal e As DataColumnChangeEventArgs)
            Dim proposedAccount As Object = e.Row("AcctCode")
            Select Case e.Column.ColumnName.ToLower
                Case "acctcode"
                    proposedAccount = e.ProposedValue
                Case Else
                    Return
            End Select

            If IsDBNull(proposedAccount) Then
                e.Row.SetColumnError("AcctCode", Me.StringParserService.Parse("${res:Global.Error.AccountMissing}"))
            Else
                e.Row.SetColumnError("AcctCode", "")
            End If
        End Sub
        Public Function ValidateRow(ByVal row As TreeRow) As Boolean
            Return True
        End Function
        Public Sub SetAccountValue(ByVal e As System.Data.DataColumnChangeEventArgs)
            Dim entity As New Account(e.ProposedValue.ToString)
            If entity.Originated Then
                e.Row("formfi_acct") = entity.Id
                e.ProposedValue = entity.Code
                e.Row("AcctName") = entity.Name
            Else
                e.Row("formfi_acct") = DBNull.Value
                e.ProposedValue = DBNull.Value
                e.Row("AcctName") = DBNull.Value
            End If
        End Sub
        Private Sub ItemAdded(ByVal sender As Object, ByVal e As ITemAddedEventArgs)
            Try
                If Not Me.IsInitialized Then
                    Return
                End If
                Dim pe As New PropertyChangedEventArgs("ItemChanged", "", "")
                Me.OnPropertyChanged(Me, pe)
                e.Row.SetColumnError("Name", "")
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        Private Sub ItemDelete(ByVal sender As Object, ByVal e As System.Data.DataRowChangeEventArgs)
            'Dim row As DataRow = e.Row
            'Me.m_itemTable.Childs.Remove(CType(row, TreeRow))
            'Try
            '    If Not Me.Isinitialized Then
            '        Return
            '    End If

            '    Dim index As TreeRow = CType(e.Row, TreeRow)
            '    Me.m_itemTable.Childs.Remove(index)
            'Catch ex As Exception
            '    MessageBox.Show(ex.ToString)
            'End Try
        End Sub
#End Region

    End Class

    Public Class FormFormatItemType
        Inherits CodeDescription

#Region "Constructors"
        Public Sub New(ByVal value As Integer)
            MyBase.New(value)
        End Sub
#End Region

#Region "Properties"
        Public Overrides ReadOnly Property CodeName() As String
            Get
                Return "formfi_fieldtype"
            End Get
        End Property
#End Region

    End Class

    Public Class FormFormatItem

#Region "Members"
        Private m_formFormat As FormFormat

        Private m_lineNumber As Integer
        Private m_fieldName As String

        Private m_mapping As String
        Private m_note As String
        Private m_tableName As String
#End Region

#Region "Constructors"
        Public Sub New()
        End Sub
        Public Sub New(ByVal dr As DataRow, ByVal aliasPrefix As String)
            Construct(dr, aliasPrefix)
        End Sub
        Protected Sub Construct(ByVal dr As DataRow, ByVal aliasPrefix As String)
            With Me
                If dr.Table.Columns.Contains(aliasPrefix & "formfi_lineNumber") AndAlso Not dr.IsNull(aliasPrefix & "formfi_lineNumber") Then
                    .m_lineNumber = CInt(dr(aliasPrefix & "formfi_lineNumber"))
                End If
                Dim fieldId As Integer
                If dr.Table.Columns.Contains(aliasPrefix & "formfi_fieldName") AndAlso Not dr.IsNull(aliasPrefix & "formfi_fieldName") Then
                    .m_fieldName = CStr(dr(aliasPrefix & "formfi_fieldName"))
                End If
                If dr.Table.Columns.Contains(aliasPrefix & "formfi_mapping") AndAlso Not dr.IsNull(aliasPrefix & "formfi_mapping") Then
                    .m_mapping = CStr(dr(aliasPrefix & "formfi_mapping"))
                End If
                If dr.Table.Columns.Contains(aliasPrefix & "formfi_note") AndAlso Not dr.IsNull(aliasPrefix & "formfi_note") Then
                    .m_note = CStr(dr(aliasPrefix & "formfi_note"))
                End If
                If dr.Table.Columns.Contains(aliasPrefix & "formfi_table") AndAlso Not dr.IsNull(aliasPrefix & "formfi_table") Then
                    .m_tableName = CStr(dr(aliasPrefix & "formfi_table"))
                End If
            End With
        End Sub
#End Region

#Region "Properties"
        Public Property FormFormat() As FormFormat            Get                Return m_FormFormat            End Get            Set(ByVal Value As FormFormat)                m_FormFormat = Value            End Set        End Property
        Public Property TableName() As String            Get                Return m_tableName            End Get            Set(ByVal Value As String)                m_tableName = Value            End Set        End Property
        Public Property LineNumber() As Integer            Get                Return m_lineNumber            End Get            Set(ByVal Value As Integer)                m_lineNumber = Value            End Set        End Property        Public Property FieldName() As String            Get                Return m_fieldName            End Get            Set(ByVal Value As String)                m_fieldName = Value            End Set        End Property        Public Property Mapping() As String            Get                Return m_mapping            End Get            Set(ByVal Value As String)                m_mapping = Value            End Set        End Property
        Public Property Note() As String
            Get
                Return m_note
            End Get
            Set(ByVal Value As String)
                m_note = Value
            End Set
        End Property
#End Region

#Region "Methods"
        Public Sub CopyToDataRow(ByVal row As TreeRow)
            If row Is Nothing Then
                Return
            End If
            Me.FormFormat.IsInitialized = False
            row("formfi_linenumber") = Me.LineNumber
            row("Name") = Me.FieldName
            row("formfi_mapping") = Me.Mapping
            row("Note") = Me.Note
            row("formfi_table") = Me.TableName
            Me.FormFormat.IsInitialized = True
        End Sub
        Public Sub CopyFromDataRow(ByVal row As TreeRow)
            If row Is Nothing Then
                Return
            End If
            Try
                If Not row.IsNull(("formfi_linenumber")) Then
                    Me.LineNumber = CInt(row("formfi_linenumber"))
                End If
                If Not IsDBNull(row("Name")) Then
                    Me.FieldName = CStr(row("Name"))
                End If
                If Not row.IsNull(("formfi_mapping")) Then
                    Me.Mapping = CStr(row("formfi_mapping"))
                End If
                If Not row.IsNull(("Note")) Then
                    Me.Note = CStr(row("Note"))
                End If
                If Not row.IsNull(("formfi_table")) Then
                    Me.TableName = CStr(row("formfi_table"))
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message & "::" & ex.StackTrace)
            End Try

        End Sub
#End Region

    End Class
End Namespace
