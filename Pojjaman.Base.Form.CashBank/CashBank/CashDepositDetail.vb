Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.TextHelper
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.Services

Namespace Longkong.Pojjaman.Gui.Panels
  Public Class CashDepositDetail
    Inherits AbstractEntityDetailPanelView
    Implements IValidatable

#Region " Windows Form Designer generated code "
    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
        If Not (components Is Nothing) Then
          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents lblBankAccount As System.Windows.Forms.Label
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblDocDate As System.Windows.Forms.Label
    Friend WithEvents grbMain As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents Validator As Longkong.Pojjaman.Gui.Components.PJMTextboxValidator
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnBankAccountEdit As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtBankAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBankAccountName As System.Windows.Forms.TextBox
    Friend WithEvents btnBankAccountFind As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtBankBranch As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtDocDate As System.Windows.Forms.TextBox
    Friend WithEvents chkAutorun As System.Windows.Forms.CheckBox
    Friend WithEvents cmbCode As System.Windows.Forms.ComboBox
    Friend WithEvents lblCurrencyUnit As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Protected Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CashDepositDetail))
      Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
      Me.lblCode = New System.Windows.Forms.Label()
      Me.lblDocDate = New System.Windows.Forms.Label()
      Me.lblBankAccount = New System.Windows.Forms.Label()
      Me.lblNote = New System.Windows.Forms.Label()
      Me.txtNote = New System.Windows.Forms.TextBox()
      Me.lblBank = New System.Windows.Forms.Label()
      Me.txtBankBranch = New System.Windows.Forms.TextBox()
      Me.txtAmount = New System.Windows.Forms.TextBox()
      Me.lblAmount = New System.Windows.Forms.Label()
      Me.grbMain = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.cmbCode = New System.Windows.Forms.ComboBox()
      Me.chkAutorun = New System.Windows.Forms.CheckBox()
      Me.txtDocDate = New System.Windows.Forms.TextBox()
      Me.lblStatus = New System.Windows.Forms.Label()
      Me.btnBankAccountFind = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.btnBankAccountEdit = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtBankAccountCode = New System.Windows.Forms.TextBox()
      Me.txtBankAccountName = New System.Windows.Forms.TextBox()
      Me.lblCurrencyUnit = New System.Windows.Forms.Label()
      Me.Validator = New Longkong.Pojjaman.Gui.Components.PJMTextboxValidator(Me.components)
      Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
      Me.grbMain.SuspendLayout()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'dtpDocDate
      '
      Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
      Me.dtpDocDate.Location = New System.Drawing.Point(400, 24)
      Me.dtpDocDate.Name = "dtpDocDate"
      Me.dtpDocDate.Size = New System.Drawing.Size(144, 20)
      Me.dtpDocDate.TabIndex = 5
      '
      'lblCode
      '
      Me.lblCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCode.ForeColor = System.Drawing.Color.Black
      Me.lblCode.Location = New System.Drawing.Point(8, 24)
      Me.lblCode.Name = "lblCode"
      Me.lblCode.Size = New System.Drawing.Size(120, 18)
      Me.lblCode.TabIndex = 0
      Me.lblCode.Text = "�����͡���:"
      Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblDocDate
      '
      Me.lblDocDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblDocDate.ForeColor = System.Drawing.Color.Black
      Me.lblDocDate.Location = New System.Drawing.Point(304, 24)
      Me.lblDocDate.Name = "lblDocDate"
      Me.lblDocDate.Size = New System.Drawing.Size(88, 18)
      Me.lblDocDate.TabIndex = 3
      Me.lblDocDate.Text = "�ѹ����͡���:"
      Me.lblDocDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblBankAccount
      '
      Me.lblBankAccount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblBankAccount.ForeColor = System.Drawing.Color.Black
      Me.lblBankAccount.Location = New System.Drawing.Point(8, 72)
      Me.lblBankAccount.Name = "lblBankAccount"
      Me.lblBankAccount.Size = New System.Drawing.Size(120, 18)
      Me.lblBankAccount.TabIndex = 9
      Me.lblBankAccount.Text = "��ش�Թ�ҡ��Ҥ��:"
      Me.lblBankAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblNote
      '
      Me.lblNote.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblNote.ForeColor = System.Drawing.Color.Black
      Me.lblNote.Location = New System.Drawing.Point(8, 120)
      Me.lblNote.Name = "lblNote"
      Me.lblNote.Size = New System.Drawing.Size(120, 18)
      Me.lblNote.TabIndex = 16
      Me.lblNote.Text = "�����˵�:"
      Me.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtNote
      '
      Me.Validator.SetDataType(Me.txtNote, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtNote, "")
      Me.txtNote.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtNote, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtNote, -15)
      Me.Validator.SetInvalidBackColor(Me.txtNote, System.Drawing.Color.Empty)
      Me.txtNote.Location = New System.Drawing.Point(136, 120)
      Me.txtNote.MaxLength = 255
      Me.Validator.SetMaxValue(Me.txtNote, "")
      Me.Validator.SetMinValue(Me.txtNote, "")
      Me.txtNote.Name = "txtNote"
      Me.Validator.SetRegularExpression(Me.txtNote, "")
      Me.Validator.SetRequired(Me.txtNote, False)
      Me.txtNote.Size = New System.Drawing.Size(408, 21)
      Me.txtNote.TabIndex = 17
      '
      'lblBank
      '
      Me.lblBank.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblBank.ForeColor = System.Drawing.Color.Black
      Me.lblBank.Location = New System.Drawing.Point(8, 96)
      Me.lblBank.Name = "lblBank"
      Me.lblBank.Size = New System.Drawing.Size(120, 16)
      Me.lblBank.TabIndex = 14
      Me.lblBank.Text = "��Ҥ��/�Ң�:"
      Me.lblBank.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtBankBranch
      '
      Me.Validator.SetDataType(Me.txtBankBranch, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtBankBranch, "")
      Me.txtBankBranch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtBankBranch, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtBankBranch, -15)
      Me.Validator.SetInvalidBackColor(Me.txtBankBranch, System.Drawing.Color.Empty)
      Me.txtBankBranch.Location = New System.Drawing.Point(136, 96)
      Me.txtBankBranch.MaxLength = 255
      Me.Validator.SetMaxValue(Me.txtBankBranch, "")
      Me.Validator.SetMinValue(Me.txtBankBranch, "")
      Me.txtBankBranch.Name = "txtBankBranch"
      Me.txtBankBranch.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtBankBranch, "")
      Me.Validator.SetRequired(Me.txtBankBranch, False)
      Me.txtBankBranch.Size = New System.Drawing.Size(408, 21)
      Me.txtBankBranch.TabIndex = 15
      '
      'txtAmount
      '
      Me.Validator.SetDataType(Me.txtAmount, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DecimalType)
      Me.Validator.SetDisplayName(Me.txtAmount, "")
      Me.txtAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtAmount, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtAmount, -15)
      Me.Validator.SetInvalidBackColor(Me.txtAmount, System.Drawing.Color.Empty)
      Me.txtAmount.Location = New System.Drawing.Point(136, 48)
      Me.txtAmount.MaxLength = 15
      Me.Validator.SetMaxValue(Me.txtAmount, "")
      Me.Validator.SetMinValue(Me.txtAmount, "")
      Me.txtAmount.Name = "txtAmount"
      Me.Validator.SetRegularExpression(Me.txtAmount, "")
      Me.Validator.SetRequired(Me.txtAmount, True)
      Me.txtAmount.Size = New System.Drawing.Size(144, 21)
      Me.txtAmount.TabIndex = 7
      Me.txtAmount.TabStop = False
      Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'lblAmount
      '
      Me.lblAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblAmount.ForeColor = System.Drawing.Color.Black
      Me.lblAmount.Location = New System.Drawing.Point(8, 48)
      Me.lblAmount.Name = "lblAmount"
      Me.lblAmount.Size = New System.Drawing.Size(120, 18)
      Me.lblAmount.TabIndex = 6
      Me.lblAmount.Text = "�ӹǹ�ӽҡ:"
      Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'grbMain
      '
      Me.grbMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
              Or System.Windows.Forms.AnchorStyles.Left) _
              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.grbMain.Controls.Add(Me.cmbCode)
      Me.grbMain.Controls.Add(Me.chkAutorun)
      Me.grbMain.Controls.Add(Me.txtDocDate)
      Me.grbMain.Controls.Add(Me.lblStatus)
      Me.grbMain.Controls.Add(Me.btnBankAccountFind)
      Me.grbMain.Controls.Add(Me.btnBankAccountEdit)
      Me.grbMain.Controls.Add(Me.txtBankAccountCode)
      Me.grbMain.Controls.Add(Me.txtBankAccountName)
      Me.grbMain.Controls.Add(Me.dtpDocDate)
      Me.grbMain.Controls.Add(Me.txtAmount)
      Me.grbMain.Controls.Add(Me.lblAmount)
      Me.grbMain.Controls.Add(Me.lblDocDate)
      Me.grbMain.Controls.Add(Me.txtBankBranch)
      Me.grbMain.Controls.Add(Me.lblBank)
      Me.grbMain.Controls.Add(Me.txtNote)
      Me.grbMain.Controls.Add(Me.lblNote)
      Me.grbMain.Controls.Add(Me.lblBankAccount)
      Me.grbMain.Controls.Add(Me.lblCode)
      Me.grbMain.Controls.Add(Me.lblCurrencyUnit)
      Me.grbMain.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbMain.Location = New System.Drawing.Point(8, 8)
      Me.grbMain.Name = "grbMain"
      Me.grbMain.Size = New System.Drawing.Size(672, 216)
      Me.grbMain.TabIndex = 0
      Me.grbMain.TabStop = False
      Me.grbMain.Text = "�ӽҡ�Թʴ"
      '
      'cmbCode
      '
      Me.cmbCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.ErrorProvider1.SetIconPadding(Me.cmbCode, -15)
      Me.cmbCode.Location = New System.Drawing.Point(136, 25)
      Me.cmbCode.Name = "cmbCode"
      Me.cmbCode.Size = New System.Drawing.Size(144, 21)
      Me.cmbCode.TabIndex = 215
      '
      'chkAutorun
      '
      Me.chkAutorun.Appearance = System.Windows.Forms.Appearance.Button
      Me.chkAutorun.Image = CType(resources.GetObject("chkAutorun.Image"), System.Drawing.Image)
      Me.chkAutorun.Location = New System.Drawing.Point(280, 24)
      Me.chkAutorun.Name = "chkAutorun"
      Me.chkAutorun.Size = New System.Drawing.Size(21, 21)
      Me.chkAutorun.TabIndex = 2
      Me.chkAutorun.TabStop = False
      '
      'txtDocDate
      '
      Me.Validator.SetDataType(Me.txtDocDate, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
      Me.Validator.SetDisplayName(Me.txtDocDate, "")
      Me.txtDocDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtDocDate, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtDocDate, -15)
      Me.Validator.SetInvalidBackColor(Me.txtDocDate, System.Drawing.Color.Empty)
      Me.txtDocDate.Location = New System.Drawing.Point(400, 24)
      Me.txtDocDate.MaxLength = 10
      Me.Validator.SetMaxValue(Me.txtDocDate, "")
      Me.Validator.SetMinValue(Me.txtDocDate, "")
      Me.txtDocDate.Name = "txtDocDate"
      Me.Validator.SetRegularExpression(Me.txtDocDate, "")
      Me.Validator.SetRequired(Me.txtDocDate, True)
      Me.txtDocDate.Size = New System.Drawing.Size(123, 21)
      Me.txtDocDate.TabIndex = 4
      Me.txtDocDate.TabStop = False
      '
      'lblStatus
      '
      Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.lblStatus.AutoSize = True
      Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblStatus.Location = New System.Drawing.Point(8, 192)
      Me.lblStatus.Name = "lblStatus"
      Me.lblStatus.Size = New System.Drawing.Size(48, 13)
      Me.lblStatus.TabIndex = 18
      Me.lblStatus.Text = "lblStatus"
      Me.lblStatus.Visible = False
      '
      'btnBankAccountFind
      '
      Me.btnBankAccountFind.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnBankAccountFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnBankAccountFind.ForeColor = System.Drawing.SystemColors.Control
      Me.btnBankAccountFind.Location = New System.Drawing.Point(496, 72)
      Me.btnBankAccountFind.Name = "btnBankAccountFind"
      Me.btnBankAccountFind.Size = New System.Drawing.Size(24, 23)
      Me.btnBankAccountFind.TabIndex = 12
      Me.btnBankAccountFind.TabStop = False
      Me.btnBankAccountFind.ThemedImage = CType(resources.GetObject("btnBankAccountFind.ThemedImage"), System.Drawing.Bitmap)
      '
      'btnBankAccountEdit
      '
      Me.btnBankAccountEdit.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnBankAccountEdit.Location = New System.Drawing.Point(520, 72)
      Me.btnBankAccountEdit.Name = "btnBankAccountEdit"
      Me.btnBankAccountEdit.Size = New System.Drawing.Size(24, 23)
      Me.btnBankAccountEdit.TabIndex = 13
      Me.btnBankAccountEdit.ThemedImage = CType(resources.GetObject("btnBankAccountEdit.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtBankAccountCode
      '
      Me.Validator.SetDataType(Me.txtBankAccountCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtBankAccountCode, "")
      Me.txtBankAccountCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtBankAccountCode, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtBankAccountCode, -15)
      Me.Validator.SetInvalidBackColor(Me.txtBankAccountCode, System.Drawing.Color.Empty)
      Me.txtBankAccountCode.Location = New System.Drawing.Point(136, 72)
      Me.txtBankAccountCode.MaxLength = 20
      Me.Validator.SetMaxValue(Me.txtBankAccountCode, "")
      Me.Validator.SetMinValue(Me.txtBankAccountCode, "")
      Me.txtBankAccountCode.Name = "txtBankAccountCode"
      Me.Validator.SetRegularExpression(Me.txtBankAccountCode, "")
      Me.Validator.SetRequired(Me.txtBankAccountCode, True)
      Me.txtBankAccountCode.Size = New System.Drawing.Size(144, 21)
      Me.txtBankAccountCode.TabIndex = 10
      Me.txtBankAccountCode.TabStop = False
      '
      'txtBankAccountName
      '
      Me.Validator.SetDataType(Me.txtBankAccountName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtBankAccountName, "")
      Me.txtBankAccountName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtBankAccountName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtBankAccountName, System.Drawing.Color.Empty)
      Me.txtBankAccountName.Location = New System.Drawing.Point(280, 72)
      Me.txtBankAccountName.MaxLength = 255
      Me.Validator.SetMaxValue(Me.txtBankAccountName, "")
      Me.Validator.SetMinValue(Me.txtBankAccountName, "")
      Me.txtBankAccountName.Name = "txtBankAccountName"
      Me.txtBankAccountName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtBankAccountName, "")
      Me.Validator.SetRequired(Me.txtBankAccountName, False)
      Me.txtBankAccountName.Size = New System.Drawing.Size(216, 21)
      Me.txtBankAccountName.TabIndex = 11
      '
      'lblCurrencyUnit
      '
      Me.lblCurrencyUnit.AutoSize = True
      Me.lblCurrencyUnit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCurrencyUnit.ForeColor = System.Drawing.Color.Black
      Me.lblCurrencyUnit.Location = New System.Drawing.Point(288, 48)
      Me.lblCurrencyUnit.Name = "lblCurrencyUnit"
      Me.lblCurrencyUnit.Size = New System.Drawing.Size(27, 13)
      Me.lblCurrencyUnit.TabIndex = 8
      Me.lblCurrencyUnit.Text = "�ҷ"
      Me.lblCurrencyUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'Validator
      '
      Me.Validator.BackcolorChanging = False
      Me.Validator.DataTable = Nothing
      Me.Validator.ErrorProvider = Me.ErrorProvider1
      Me.Validator.GotFocusBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
      Me.Validator.HasNewRow = False
      Me.Validator.InvalidBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
      '
      'ErrorProvider1
      '
      Me.ErrorProvider1.ContainerControl = Me
      '
      'CashDepositDetail
      '
      Me.Controls.Add(Me.grbMain)
      Me.Name = "CashDepositDetail"
      Me.Size = New System.Drawing.Size(688, 232)
      Me.grbMain.ResumeLayout(False)
      Me.grbMain.PerformLayout()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)

    End Sub

#End Region

#Region " SetLabelText "
    Public Overrides Sub SetLabelText()
      If Not m_entity Is Nothing Then Me.Text = Me.StringParserService.Parse(Me.m_entity.TabPageText)
      Me.lblCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CashDepositDetail.lblCode}")
      Me.Validator.SetDisplayName(Me.cmbCode, StringHelper.GetRidOfAtEnd(Me.lblCode.Text, ":"))


      Me.lblBankAccount.Text = Me.StringParserService.Parse("${res:Global.BankAccountText}")
      Me.Validator.SetDisplayName(txtBankAccountCode, lblBankAccount.Text)
      Me.lblBank.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CashDepositDetail.lblBank}")

      Me.lblNote.Text = Me.StringParserService.Parse("${res:Global.NoteText}")
      Me.Validator.SetDisplayName(txtNote, lblNote.Text)

      Me.lblAmount.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CashDepositDetail.lblAmount}")
      Me.Validator.SetDisplayName(txtAmount, lblAmount.Text)

      Me.lblDocDate.Text = Me.StringParserService.Parse("${res:Global.DocDateText}")
      Me.Validator.SetDisplayName(txtDocDate, lblDocDate.Text)

      Me.grbMain.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.CashDepositDetail.grpMain}")
      Me.lblCurrencyUnit.Text = Me.StringParserService.Parse("${res:Global.CurrencyUnit}")
    End Sub
#End Region

#Region "Members"
    Private m_entity As CashDeposite
    Private m_isInitialized As Boolean = False
#End Region

#Region "Constructs"
    Public Sub New()
      MyBase.New()
      InitializeComponent()

      Initialize()
      EventWiring()
      SetLabelText()
    End Sub
#End Region

#Region "Methods"
#End Region

#Region "IListDetail"
    Public Overrides Sub CheckFormEnable()
      If Me.m_entity.Canceled _
      OrElse Me.m_entity.Status.Value = 0 _
      OrElse Me.m_entity.Status.Value >= 3 Then
        For Each ctrl As Control In grbMain.Controls
          ctrl.Enabled = False
        Next
      Else
        For Each ctrl As Control In grbMain.Controls
          ctrl.Enabled = True
        Next
      End If
    End Sub
    Public Overrides Sub Initialize()

    End Sub
    Protected Overrides Sub EventWiring()
      AddHandler cmbCode.TextChanged, AddressOf Me.ChangeProperty
      AddHandler cmbCode.SelectedIndexChanged, AddressOf Me.ChangeProperty

      AddHandler txtDocDate.Validated, AddressOf Me.ChangeProperty
      AddHandler dtpDocDate.ValueChanged, AddressOf Me.ChangeProperty

      AddHandler txtAmount.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtAmount.Validated, AddressOf Me.NumberTextBoxChange

      AddHandler txtBankAccountCode.Validated, AddressOf Me.ChangeProperty

      AddHandler txtNote.TextChanged, AddressOf Me.ChangeProperty
    End Sub
    Public Sub NumberTextBoxChange(ByVal sender As Object, ByVal e As EventArgs)
      If Me.m_entity Is Nothing Or Not m_isInitialized Then
        Return
      End If
      Select Case CType(sender, Control).Name.ToLower
        Case "txtamount"
          txtAmount.Text = Configuration.FormatToString(Me.m_entity.Amount, DigitConfig.Price)
      End Select
    End Sub
    Private m_dateSetting As Boolean
    Public Sub ChangeProperty(ByVal sender As Object, ByVal e As EventArgs)
      If Me.m_entity Is Nothing Or Not m_isInitialized Then
        Return
      End If
      Dim dirtyFlag As Boolean = False
      Select Case CType(sender, Control).Name.ToLower
        Case "cmbcode"
          Me.m_entity.Code = cmbCode.Text
          '���� AutoCode
          If TypeOf cmbCode.SelectedItem Is AutoCodeFormat Then
            Me.m_entity.AutoCodeFormat = CType(cmbCode.SelectedItem, AutoCodeFormat)
            Me.m_entity.OnGlChanged()
          End If
          dirtyFlag = True

        Case "dtpdocdate"
          If Not Me.m_entity.Docdate.Equals(dtpDocDate.Value) Then
            If Not m_dateSetting Then
              Me.txtDocDate.Text = MinDateToNull(dtpDocDate.Value, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
              Me.m_entity.Docdate = dtpDocDate.Value
            End If
            dirtyFlag = True
          End If
        Case "txtdocdate"
          m_dateSetting = True
          If Not Me.txtDocDate.Text.Length = 0 AndAlso Me.Validator.GetErrorMessage(Me.txtDocDate) = "" Then
            Dim theDate As Date = CDate(Me.txtDocDate.Text)
            If Not Me.m_entity.Docdate.Equals(theDate) Then
              dtpDocDate.Value = theDate
              Me.m_entity.Docdate = dtpDocDate.Value
              dirtyFlag = True
            End If
          Else
            Me.dtpDocDate.Value = Date.Now
            Me.m_entity.Docdate = Date.MinValue
            dirtyFlag = True
          End If
          m_dateSetting = False

        Case "txtamount"
          dirtyFlag = True
          If txtAmount.TextLength > 0 Then
            Me.m_entity.Amount = CDec(txtAmount.Text)
          Else
            Me.m_entity.Amount = Nothing
          End If

        Case "txtbankaccountcode"
          dirtyFlag = BankAccount.GetBankAccountBankBranch(txtBankAccountCode, txtBankAccountName, txtBankBranch, Me.m_entity.Bankacct)

        Case "txtnote"
          dirtyFlag = True
          Me.m_entity.Note = txtNote.Text

        Case "cmbtype"
          dirtyFlag = True

      End Select
      Me.WorkbenchWindow.ViewContent.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty Or dirtyFlag

      SetStatus()
      CheckFormEnable()
    End Sub
    Public Overrides Sub UpdateEntityProperties()
      m_isInitialized = False
      ClearDetail()
      If m_entity Is Nothing Then
        Return
      End If
      m_oldCode = m_entity.Code
      Me.chkAutorun.Checked = Me.m_entity.AutoGen
      Me.UpdateAutogenStatus()

      dtpDocDate.Value = MinDateToNow(Me.m_entity.Docdate)
      txtDocDate.Text = MinDateToNull(Me.m_entity.Docdate, Me.StringParserService.Parse("${res:Global.BlankDateText}"))

      txtAmount.Text = Configuration.FormatToString(Me.m_entity.Amount, DigitConfig.Price)

      If Not Me.m_entity.Bankacct Is Nothing Then
        txtBankAccountCode.Text = Me.m_entity.Bankacct.Code
        txtBankAccountName.Text = Me.m_entity.Bankacct.Name
        txtBankBranch.Text = Me.m_entity.Bankacct.BankBranchName
      End If

      txtNote.Text = Me.m_entity.Note

      SetStatus()
      SetLabelText()
      CheckFormEnable()

      m_isInitialized = True
    End Sub
    Public Overrides Sub ClearDetail()
      lblStatus.Text = ""
      For Each crlt As Control In grbMain.Controls
        If TypeOf crlt Is TextBox Then
          crlt.Text = ""
        End If
      Next

      txtDocDate.Text = Me.StringParserService.Parse("${res:Global.BlankDateText}")
      dtpDocDate.Value = Date.Now

    End Sub
    Public Overrides Property Entity() As ISimpleEntity
      Get
        Return Me.m_entity
      End Get
      Set(ByVal Value As ISimpleEntity)
        Me.m_entity = Nothing
        Me.m_entity = CType(Value, CashDeposite)
        'Hack:
        Me.m_entity.OnTabPageTextChanged(m_entity, EventArgs.Empty)
        UpdateEntityProperties()
      End Set
    End Property

    Public Sub SetStatus()
      MyBase.SetStatusBarMessage()
      'If Not IsNothing(m_entity.CancelDate) And Not m_entity.CancelDate.Equals(Date.MinValue) Then
      '  lblStatus.Text = "¡��ԡ: " & m_entity.CancelDate.ToShortDateString & _
      '  " " & m_entity.CancelDate.ToShortTimeString & _
      '  "  ��:" & m_entity.CancelPerson.Name
      'ElseIf Not IsNothing(m_entity.LastEditDate) And Not m_entity.LastEditDate.Equals(Date.MinValue) Then
      '  lblStatus.Text = "�������ش: " & m_entity.LastEditDate.ToShortDateString & _
      '  " " & m_entity.LastEditDate.ToShortTimeString & _
      '  "  ��:" & m_entity.LastEditor.Name
      'ElseIf Not IsNothing(m_entity.OriginDate) And Not m_entity.OriginDate.Equals(Date.MinValue) Then
      '  lblStatus.Text = "�����������к�: " & m_entity.OriginDate.ToShortDateString & _
      '  " " & m_entity.OriginDate.ToShortTimeString & _
      '  "  ��:" & m_entity.Originator.Name
      'Else
      '  lblStatus.Text = "�ѧ�����ѹ�֡"
      'End If
    End Sub
#End Region

#Region "IValidatable"
    Public ReadOnly Property FormValidator() As components.PJMTextboxValidator Implements IValidatable.FormValidator
      Get
        Return Me.Validator
      End Get
    End Property
#End Region

#Region "Overrides"

#End Region

#Region "Event of Button controls"
    ' Bank Account
    Private Sub btnBankAccountEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBankAccountEdit.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New BankAccount)
    End Sub
    Private Sub btnBankAccountFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBankAccountFind.Click
      Dim myEntityPanelService As IEntityPanelService = _
       CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New BankAccount, AddressOf SetBankAccountDialog)
    End Sub
    Private Sub SetBankAccountDialog(ByVal e As ISimpleEntity)
      Me.txtBankAccountCode.Text = e.Code
      Me.WorkbenchWindow.ViewContent.IsDirty = _
          Me.WorkbenchWindow.ViewContent.IsDirty _
          Or BankAccount.GetBankAccountBankBranch(txtBankAccountCode, txtBankAccountName, txtBankBranch, Me.m_entity.Bankacct)
    End Sub
#End Region

#Region "IClipboardHandler Overrides"
    Public Overrides ReadOnly Property EnablePaste() As Boolean
      Get
        Dim data As IDataObject = Clipboard.GetDataObject
        If data.GetDataPresent((New BankAccount).FullClassName) Then
          If Not Me.ActiveControl Is Nothing Then
            Select Case Me.ActiveControl.Name.ToLower
              Case "txtbankaccountcode", "txtbankaccountname"
                Return True
            End Select
          End If
        End If
        Return False
      End Get
    End Property
    Public Overrides Sub Paste(ByVal sender As Object, ByVal e As System.EventArgs)
      Dim data As IDataObject = Clipboard.GetDataObject
      If data.GetDataPresent((New BankAccount).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New BankAccount).FullClassName))
        Dim entity As New BankAccount(id)
        If Not Me.ActiveControl Is Nothing Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtbankaccountcode", "txtbankaccountname"
              Me.SetBankAccountDialog(entity)
          End Select
        End If
      End If
    End Sub
#End Region

#Region " Autogencode "
    Private Sub chkAutorun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutorun.CheckedChanged
      UpdateAutogenStatus()
    End Sub
    Private m_oldCode As String = ""
    Private Sub UpdateAutogenStatus()
      If Me.chkAutorun.Checked Then
        Me.cmbCode.DropDownStyle = ComboBoxStyle.DropDownList 'ComboBoxStyle.DropDown
        Dim currentUserId As Integer = Me.SecurityService.CurrentUser.Id
        BusinessLogic.Entity.NewPopulateCodeCombo(Me.cmbCode, Me.m_entity.EntityId, currentUserId)
        If Me.m_entity.Code Is Nothing OrElse Me.m_entity.Code.Length = 0 Then
          If Me.cmbCode.Items.Count > 0 Then
            Me.m_entity.Code = CType(Me.cmbCode.Items(0), AutoCodeFormat).Format
            Me.cmbCode.SelectedIndex = 0
            Me.m_entity.AutoCodeFormat = CType(Me.cmbCode.Items(0), AutoCodeFormat)
          End If
        Else
          Me.cmbCode.SelectedIndex = Me.cmbCode.FindStringExact(Me.m_entity.Code)
          If TypeOf Me.cmbCode.SelectedItem Is AutoCodeFormat Then
            Me.m_entity.AutoCodeFormat = CType(Me.cmbCode.SelectedItem, AutoCodeFormat)
          End If
        End If
        m_oldCode = Me.cmbCode.Text
        Me.m_entity.Code = m_oldCode
        Me.m_entity.AutoGen = True
      Else
        Me.cmbCode.DropDownStyle = ComboBoxStyle.Simple
        Me.cmbCode.Text = m_oldCode
        Me.m_entity.AutoGen = False
      End If
    End Sub
#End Region

  End Class
End Namespace