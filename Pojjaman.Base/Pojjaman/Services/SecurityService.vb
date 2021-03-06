Imports System.Windows.Forms
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.Internal.Parser
Imports Longkong.SecureCredential
Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.Gui.Dialogs
Imports System.Data.SqlClient
Imports Longkong.Pojjaman.DataAccessLayer
Imports Longkong.Pojjaman.Gui
Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports Longkong.Core.Properties
Imports System.IO
Imports System.Reflection
Namespace Longkong.Pojjaman.Services
  Public Class SecurityService
    Inherits AbstractService

#Region "Members"
    Private m_curentUser As User
    Private m_accessTable As DataTable
#End Region

#Region "Methods"
    Public Sub OpenStartUpPage(Optional ByVal FullClassName As String = "")
      Dim secSrv As SecurityService = CType(ServiceManager.Services.GetService(GetType(SecurityService)), SecurityService)
      Dim CurrentStartUpPage As String = ""
      'CurrentStartUpPage = Configuration.GetConfig("CurrentClassNameOfStartUpPage").ToString
      If Not ConfigurationUser.GetConfig(secSrv.CurrentUser.Id, "CurrentClassNameOfStartUpPage") Is Nothing Then
        CurrentStartUpPage = (ConfigurationUser.GetConfig(secSrv.CurrentUser.Id, "CurrentClassNameOfStartUpPage")).ToString
      End If

      If CurrentStartUpPage Is Nothing OrElse CurrentStartUpPage.Length = 0 Then
        Return
      End If
      If FullClassName.Length > 0 Then
        CurrentStartUpPage = FullClassName
      End If
      'Dim simpleentity As SimpleBusinessEntityBase = SimpleBusinessEntityBase.GetEntity(FullClassName)
      Dim simpleentity As SimpleBusinessEntityBase = SimpleBusinessEntityBase.GetEntity(CurrentStartUpPage)

      Dim accessId As Integer = Entity.GetAccessIdFromFullClassName(simpleentity.FullClassName)

      Dim level As Integer = secSrv.GetAccess(accessId)
      Dim checkString As String = BinaryHelper.DecToBin(level, 5)
      checkString = BinaryHelper.RevertString(checkString)

      'Dim config As Boolean = CBool(ConfigurationUser.GetConfig(secSrv.CurrentUser.Id, "AlwaysShowMultiApprovePage"))
      Dim config As Boolean = CBool(ConfigurationUser.GetConfig(secSrv.CurrentUser.Id, "AlwaysShowStartUpPage"))
      If CBool(checkString.Substring(0, 1)) Then
        If config Then
          Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
          'myEntityPanelService.OpenPanel(Me.Entity, Me.Args, Me.Label)
          myEntityPanelService.OpenPanel(simpleentity.FullClassName, "", "")
        End If
      End If

    End Sub
    Public Sub LogOff()
      Me.m_curentUser = New User
      WorkbenchSingleton.Workbench.RedrawAllComponents()
      Dim ret As DialogResult
      Dim loginUser As User
      Do While ret <> DialogResult.Cancel And (loginUser Is Nothing OrElse Not loginUser.Originated)
        ret = Me.Login
        loginUser = Me.CurrentUser
      Loop
      If ret = DialogResult.Cancel Then
        Application.ExitThread()
        Return
      End If
    End Sub
    Public Function Login(ByVal name As String) As Boolean
      Dim value As Boolean = False
      Try
        Dim myResourceService As ResourceService = CType(ServiceManager.Services.GetService(GetType(IResourceService)), ResourceService)
        Dim dialog As New CredentialsDialog(myResourceService.GetString("MainWindow.DialogName"))
        If Not (name Is Nothing) Then
          dialog.AlwaysDisplay = True ' prevent an infinite loop
        End If
        dialog.Banner = myResourceService.GetBitmap("Login.Banner")
        dialog.Persist = False
        Dim loginUser As New User(dialog.Name, dialog.Password)
        MessageBox.Show(dialog.Name & ":" & dialog.Password)
        If dialog.Show(name) = System.Windows.Forms.DialogResult.OK Then
          If loginUser.Originated AndAlso Not loginUser.Canceled Then
            Me.SetCurrentUser(loginUser)
            value = True
            If dialog.SaveChecked Then
              dialog.Confirm(True)
            End If
          Else
            Try
              dialog.Confirm(False)
            Catch applicationException As ApplicationException
            End Try
            ' exception handling ...
            value = Login(dialog.Name) ' need to find a way to display 'Logon unsuccessful'
          End If
        End If
      Catch applicationException As ApplicationException
      End Try
      ' exception handling ...
      Return value
    End Function 'Login
    Public Shared NoPassword As Boolean = False
    Public Function Login() As DialogResult
      Dim dlg As New LoginDialog
      Dim ret As DialogResult
      ret = dlg.ShowDialog
      If ret = DialogResult.OK Then
        Dim loginUser As User
        User.RefreshUserTable()
        User.CurrentUserName = dlg.UserName
        If NoPassword Then  'Hack
          loginUser = New User(dlg.UserName)
        Else
          loginUser = New User(dlg.UserName, dlg.Password)
        End If
        If loginUser.Originated AndAlso Not loginUser.Canceled Then
          If Not CheckLicense() Then
            Return ret.Cancel
          End If
          Me.m_curentUser = loginUser

          '========================================SETUP===========================================
          SqlHelper.CurrentConnString = RecentCompanies.CurrentCompany.ConnectionString
          UpdateAccessTable()
          Configuration.RefreshConfigurationList()
          CodeDescription.RefreshCodeList()
          PJMModule.RefreshPJMModuleList()
          Access.RefreshFormAccessTable()
          ColumnCollection.RefreshColumnList()
          ReportColumnCollection.RefreshReportColumnList()
          Province.RefreshProvinceList()
          Account.RefreshEntityTable()
          GeneralAccount.RefreshGATable()
          CostCenter.RefreshDefaultCC()
          CBS.RefreshTree()
          AdvanceFindField.RefreshCodeList()

          Unit.DestroyCachUnit()
          CostCenter.DestroyCachCC()
          Employee.DestroyEmployee()
          LCIItem.DestroyLCI()
          '========================================SETUP===========================================
        End If
      End If
      Return ret
    End Function
    Private Function CheckLicense() As Boolean
      Dim validLicense As Boolean = False
      Dim availableLicense As Integer = 0
      Dim licenseCount As Integer = 0
      Dim ds As DataSet = User.GetLicenseInfo
      Dim isDemo As Boolean = False
      CheckLicense = True

      'If ds.Tables(0).Rows(0).IsNull("machineCode") Then
      If Not ds.Tables(0).Rows(0).IsNull("licenseday") Then
        'No View licenserregister
        isDemo = True
        Dim remainingDay As Integer = CInt(ds.Tables(2).Rows(0)("remainingday"))
        If remainingDay <= 0 Then
          availableLicense = 0
        Else
          Dim machineCode As String = User.BytesToHexSmall(CType(ds.Tables(0).Rows(0)("machineCode"), Byte()))
          Dim licenseday As String = ds.Tables(0).Rows(0)("licenseday").ToString
          availableLicense = CInt(ds.Tables(0).Rows(0)("license"))
          Dim s As String = availableLicense.ToString() + User.SALT + licenseday '+machineCode
          s = User.GetMD5Hash(s)
          Dim checkSum As String = ds.Tables(0).Rows(0)("pepper").ToString.ToLower
          If checkSum.Length > 0 Then
            '�� view demoregister
            If s <> checkSum Then
              MessageBox.Show("Demo License is Changed")
              availableLicense = -1
            Else
              MessageBox.Show(String.Format("Demo time Left:{0} Days", remainingDay))
              availableLicense = CInt(ds.Tables(0).Rows(0)("license"))
            End If
          Else
            'No view demoregister
            MessageBox.Show(String.Format("Demo time Left:{0} Days", remainingDay))
            availableLicense = CInt(ds.Tables(0).Rows(0)("license"))
          End If

        End If
      Else
        '�� view licenseregister
        Dim machineCode As String = User.BytesToHexSmall(CType(ds.Tables(0).Rows(0)("machineCode"), Byte())) 'ds.Tables(0).Rows(0)("machineCode").ToString()
        availableLicense = CInt(ds.Tables(0).Rows(0)("license"))
        Dim s As String = availableLicense.ToString() + User.SALT + machineCode
        s = User.GetMD5Hash(s)
        Dim checkSum As String = ds.Tables(0).Rows(0)("pepper").ToString.ToLower 'User.BytesToHexSmall(CType(ds.Tables(0).Rows(0)("pepper"), Byte()))
        If s <> checkSum Then
          MessageBox.Show(String.Format("License is Changed: machineCode = {0}, available = {1} , checksum = {2}", machineCode, availableLicense, checkSum))
          availableLicense = -1
        End If
      End If

      'If isDemo Then
      '  validLicense = False
      'Else
      licenseCount = CInt(ds.Tables(1).Rows(0)("hostnumber"))
      validLicense = licenseCount < availableLicense
      'End If

      If Not validLicense Then
        CheckLicense = False
        MessageBox.Show(String.Format("License used : {0}/{1}", licenseCount, availableLicense))
        Application.ExitThread()
        Application.Exit()
      Else
        If Not isDemo Then
          User.HitDB()
          Dim t As New Timer
          t.Interval = 360000
          AddHandler t.Tick, AddressOf TimerEvent
          t.Start()
        End If
      End If
    End Function
    Private Sub TimerEvent(ByVal sender As Object, ByVal e As EventArgs)
      User.HitDB()
    End Sub
    Public Sub UpdateAccessTable()
      Try
        Dim connString As String = RecentCompanies.CurrentCompany.ConnectionString
        Dim ds As DataSet = SqlHelper.ExecuteDataset( _
                connString _
                , CommandType.StoredProcedure _
                , "GetAccessTableForUser" _
                , New SqlParameter("@user_id", m_curentUser.Id) _
                )
        m_accessTable = ds.Tables(0)
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End Sub
    Public Sub SetCurrentUser(ByVal user As User)
      Me.m_curentUser = user
    End Sub
    Public Function GetAccess(ByVal accessId As Integer) As Integer
      If NoPassword Then 'Hack
        Return 31
      End If
      If Me.m_accessTable Is Nothing Then
        Return 0
      End If
      Dim rows As DataRow() = m_accessTable.Select("useraccess_access=" & accessId)
      If rows.Length = 1 Then
        If Not rows(0).IsNull("useraccess_accessvalue") Then
          Return CInt(rows(0)("useraccess_accessvalue"))
        End If
      End If
      Return 0
        End Function

        Public Function GetAccessCode(ByVal accessId As Integer) As String

            If Me.m_accessTable Is Nothing Then
                Return ""
            End If

            Dim rows As DataRow() = m_accessTable.Select("useraccess_access=" & accessId)

            If rows.Length = 1 Then
                If Not rows(0).IsNull("access_code") Then
                    Return CStr(rows(0)("access_code"))
                End If
            End If
            Return ""

        End Function

#End Region

#Region "Properties"
    Public ReadOnly Property CurrentUser() As User
      Get
        Return m_curentUser
      End Get
    End Property
    Public ReadOnly Property CurrentAccessTable() As DataTable
      Get
        Return m_accessTable
      End Get
    End Property
#End Region

    Public Overrides Sub UnloadService()
      MyBase.UnloadService()
      'Disconnect()
    End Sub
  End Class
End Namespace



