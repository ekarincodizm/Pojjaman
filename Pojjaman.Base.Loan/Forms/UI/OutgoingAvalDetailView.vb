Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.TextHelper
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.Services

Namespace Longkong.Pojjaman.Gui.Panels
  Public Class OutgoingAvalDetailView
    Inherits AbstractEntityDetailPanelView
    Implements IValidatable, IReversibleEntityProperty

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
    Friend WithEvents Validator As Longkong.Pojjaman.Gui.Components.PJMTextboxValidator
    Friend WithEvents grbOutgoingAval As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents lblIssueDate As System.Windows.Forms.Label
    Friend WithEvents lblDueDate As System.Windows.Forms.Label
    Friend WithEvents dtpIssueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblBankAccount As System.Windows.Forms.Label
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents txtNote As MultiLineTextBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtrecipient As System.Windows.Forms.TextBox
    Friend WithEvents lblrecipient As System.Windows.Forms.Label
    Friend WithEvents lblCheckStatus As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCqCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtIssueDate As System.Windows.Forms.TextBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtDueDate As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents txtBankAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBankAccountName As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtbankbranch As System.Windows.Forms.TextBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents lblCqCode As System.Windows.Forms.Label
    Friend WithEvents lblSupplier As System.Windows.Forms.Label
    Friend WithEvents btnSupplierFind As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents btnSupplierEdit As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents chkAutorun As System.Windows.Forms.CheckBox
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents lblBaht3 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents tgItem As Longkong.Pojjaman.Gui.Components.TreeGrid
    Friend WithEvents btnLoanFind As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents lblLoan As System.Windows.Forms.Label
    Friend WithEvents txtLoanCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLoanName As System.Windows.Forms.TextBox
    Friend WithEvents btnLoanEdit As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents lblItem As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Protected Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OutgoingAvalDetailView))
      Me.grbOutgoingAval = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.txtTotal = New System.Windows.Forms.TextBox()
      Me.lblBaht3 = New System.Windows.Forms.Label()
      Me.lblTotal = New System.Windows.Forms.Label()
      Me.tgItem = New Longkong.Pojjaman.Gui.Components.TreeGrid()
      Me.lblItem = New System.Windows.Forms.Label()
      Me.chkAutorun = New System.Windows.Forms.CheckBox()
      Me.txtDueDate = New System.Windows.Forms.TextBox()
      Me.btnSupplierFind = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtIssueDate = New System.Windows.Forms.TextBox()
      Me.txtSupplierCode = New System.Windows.Forms.TextBox()
      Me.txtAmount = New System.Windows.Forms.TextBox()
      Me.lblCode = New System.Windows.Forms.Label()
      Me.btnSupplierEdit = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.cmbStatus = New System.Windows.Forms.ComboBox()
      Me.dtpIssueDate = New System.Windows.Forms.DateTimePicker()
      Me.lblSupplier = New System.Windows.Forms.Label()
      Me.lblCqCode = New System.Windows.Forms.Label()
      Me.txtCqCode = New System.Windows.Forms.TextBox()
      Me.lblIssueDate = New System.Windows.Forms.Label()
      Me.lblDueDate = New System.Windows.Forms.Label()
      Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
      Me.lblBankAccount = New System.Windows.Forms.Label()
      Me.lblAmount = New System.Windows.Forms.Label()
      Me.lblCurrency = New System.Windows.Forms.Label()
      Me.lblNote = New System.Windows.Forms.Label()
      Me.txtNote = New Longkong.Pojjaman.Gui.Components.MultiLineTextBox()
      Me.lblCheckStatus = New System.Windows.Forms.Label()
      Me.txtrecipient = New System.Windows.Forms.TextBox()
      Me.lblrecipient = New System.Windows.Forms.Label()
      Me.lblStatus = New System.Windows.Forms.Label()
      Me.lblBank = New System.Windows.Forms.Label()
      Me.txtbankbranch = New System.Windows.Forms.TextBox()
      Me.txtBankAccountCode = New System.Windows.Forms.TextBox()
      Me.txtSupplierName = New System.Windows.Forms.TextBox()
      Me.txtBankAccountName = New System.Windows.Forms.TextBox()
      Me.txtCode = New System.Windows.Forms.TextBox()
      Me.Validator = New Longkong.Pojjaman.Gui.Components.PJMTextboxValidator()
      Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider()
      Me.btnLoanFind = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.lblLoan = New System.Windows.Forms.Label()
      Me.txtLoanCode = New System.Windows.Forms.TextBox()
      Me.txtLoanName = New System.Windows.Forms.TextBox()
      Me.btnLoanEdit = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.grbOutgoingAval.SuspendLayout()
      Me.SuspendLayout()
      '
      'grbOutgoingAval
      '
      Me.grbOutgoingAval.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.grbOutgoingAval.Controls.Add(Me.btnLoanEdit)
      Me.grbOutgoingAval.Controls.Add(Me.btnLoanFind)
      Me.grbOutgoingAval.Controls.Add(Me.lblLoan)
      Me.grbOutgoingAval.Controls.Add(Me.txtLoanCode)
      Me.grbOutgoingAval.Controls.Add(Me.txtLoanName)
      Me.grbOutgoingAval.Controls.Add(Me.txtTotal)
      Me.grbOutgoingAval.Controls.Add(Me.lblBaht3)
      Me.grbOutgoingAval.Controls.Add(Me.lblTotal)
      Me.grbOutgoingAval.Controls.Add(Me.tgItem)
      Me.grbOutgoingAval.Controls.Add(Me.lblItem)
      Me.grbOutgoingAval.Controls.Add(Me.chkAutorun)
      Me.grbOutgoingAval.Controls.Add(Me.txtDueDate)
      Me.grbOutgoingAval.Controls.Add(Me.btnSupplierFind)
      Me.grbOutgoingAval.Controls.Add(Me.txtIssueDate)
      Me.grbOutgoingAval.Controls.Add(Me.txtSupplierCode)
      Me.grbOutgoingAval.Controls.Add(Me.txtAmount)
      Me.grbOutgoingAval.Controls.Add(Me.lblCode)
      Me.grbOutgoingAval.Controls.Add(Me.btnSupplierEdit)
      Me.grbOutgoingAval.Controls.Add(Me.cmbStatus)
      Me.grbOutgoingAval.Controls.Add(Me.dtpIssueDate)
      Me.grbOutgoingAval.Controls.Add(Me.lblSupplier)
      Me.grbOutgoingAval.Controls.Add(Me.lblCqCode)
      Me.grbOutgoingAval.Controls.Add(Me.txtCqCode)
      Me.grbOutgoingAval.Controls.Add(Me.lblIssueDate)
      Me.grbOutgoingAval.Controls.Add(Me.lblDueDate)
      Me.grbOutgoingAval.Controls.Add(Me.dtpDueDate)
      Me.grbOutgoingAval.Controls.Add(Me.lblBankAccount)
      Me.grbOutgoingAval.Controls.Add(Me.lblAmount)
      Me.grbOutgoingAval.Controls.Add(Me.lblCurrency)
      Me.grbOutgoingAval.Controls.Add(Me.lblNote)
      Me.grbOutgoingAval.Controls.Add(Me.txtNote)
      Me.grbOutgoingAval.Controls.Add(Me.lblCheckStatus)
      Me.grbOutgoingAval.Controls.Add(Me.txtrecipient)
      Me.grbOutgoingAval.Controls.Add(Me.lblrecipient)
      Me.grbOutgoingAval.Controls.Add(Me.lblStatus)
      Me.grbOutgoingAval.Controls.Add(Me.lblBank)
      Me.grbOutgoingAval.Controls.Add(Me.txtbankbranch)
      Me.grbOutgoingAval.Controls.Add(Me.txtBankAccountCode)
      Me.grbOutgoingAval.Controls.Add(Me.txtSupplierName)
      Me.grbOutgoingAval.Controls.Add(Me.txtBankAccountName)
      Me.grbOutgoingAval.Controls.Add(Me.txtCode)
      Me.grbOutgoingAval.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbOutgoingAval.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.grbOutgoingAval.ForeColor = System.Drawing.Color.Blue
      Me.grbOutgoingAval.Location = New System.Drawing.Point(8, 8)
      Me.grbOutgoingAval.Name = "grbOutgoingAval"
      Me.grbOutgoingAval.Size = New System.Drawing.Size(624, 520)
      Me.grbOutgoingAval.TabIndex = 0
      Me.grbOutgoingAval.TabStop = False
      Me.grbOutgoingAval.Text = "�����ŵ�������� : "
      '
      'txtTotal
      '
      Me.Validator.SetDataType(Me.txtTotal, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtTotal, "")
      Me.Validator.SetGotFocusBackColor(Me.txtTotal, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtTotal, System.Drawing.Color.Empty)
      Me.txtTotal.Location = New System.Drawing.Point(368, 271)
      Me.Validator.SetMinValue(Me.txtTotal, "")
      Me.txtTotal.Name = "txtTotal"
      Me.txtTotal.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtTotal, "")
      Me.Validator.SetRequired(Me.txtTotal, False)
      Me.txtTotal.Size = New System.Drawing.Size(136, 21)
      Me.txtTotal.TabIndex = 201
      '
      'lblBaht3
      '
      Me.lblBaht3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblBaht3.ForeColor = System.Drawing.Color.Black
      Me.lblBaht3.Location = New System.Drawing.Point(512, 271)
      Me.lblBaht3.Name = "lblBaht3"
      Me.lblBaht3.Size = New System.Drawing.Size(32, 16)
      Me.lblBaht3.TabIndex = 198
      Me.lblBaht3.Text = "�ҷ"
      Me.lblBaht3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblTotal
      '
      Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblTotal.ForeColor = System.Drawing.Color.Black
      Me.lblTotal.Location = New System.Drawing.Point(255, 271)
      Me.lblTotal.Name = "lblTotal"
      Me.lblTotal.Size = New System.Drawing.Size(105, 18)
      Me.lblTotal.TabIndex = 200
      Me.lblTotal.Text = "�ʹ��������Ť������:"
      Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'tgItem
      '
      Me.tgItem.AllowNew = False
      Me.tgItem.AllowSorting = False
      Me.tgItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.tgItem.AutoColumnResize = True
      Me.tgItem.CaptionVisible = False
      Me.tgItem.Cellchanged = False
      Me.tgItem.ColorList.AddRange(New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))})
      Me.tgItem.DataMember = ""
      Me.tgItem.HeaderForeColor = System.Drawing.SystemColors.ControlText
      Me.tgItem.Location = New System.Drawing.Point(16, 295)
      Me.tgItem.Name = "tgItem"
      Me.tgItem.Size = New System.Drawing.Size(592, 177)
      Me.tgItem.SortingArrowColor = System.Drawing.Color.Red
      Me.tgItem.TabIndex = 202
      Me.tgItem.TreeManager = Nothing
      '
      'lblItem
      '
      Me.lblItem.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblItem.ForeColor = System.Drawing.Color.Black
      Me.lblItem.Location = New System.Drawing.Point(16, 276)
      Me.lblItem.Name = "lblItem"
      Me.lblItem.Size = New System.Drawing.Size(136, 18)
      Me.lblItem.TabIndex = 199
      Me.lblItem.Text = "�ѹ�ա�ʹ�Ѵ����"
      Me.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'chkAutorun
      '
      Me.chkAutorun.Appearance = System.Windows.Forms.Appearance.Button
      Me.chkAutorun.Image = CType(resources.GetObject("chkAutorun.Image"), System.Drawing.Image)
      Me.chkAutorun.Location = New System.Drawing.Point(272, 24)
      Me.chkAutorun.Name = "chkAutorun"
      Me.chkAutorun.Size = New System.Drawing.Size(21, 21)
      Me.chkAutorun.TabIndex = 2
      Me.chkAutorun.TabStop = False
      '
      'txtDueDate
      '
      Me.Validator.SetDataType(Me.txtDueDate, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
      Me.Validator.SetDisplayName(Me.txtDueDate, "")
      Me.Validator.SetGotFocusBackColor(Me.txtDueDate, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtDueDate, -15)
      Me.Validator.SetInvalidBackColor(Me.txtDueDate, System.Drawing.Color.Empty)
      Me.txtDueDate.Location = New System.Drawing.Point(400, 48)
      Me.Validator.SetMinValue(Me.txtDueDate, "")
      Me.txtDueDate.Name = "txtDueDate"
      Me.Validator.SetRegularExpression(Me.txtDueDate, "")
      Me.Validator.SetRequired(Me.txtDueDate, False)
      Me.txtDueDate.Size = New System.Drawing.Size(123, 21)
      Me.txtDueDate.TabIndex = 9
      '
      'btnSupplierFind
      '
      Me.btnSupplierFind.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnSupplierFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnSupplierFind.ForeColor = System.Drawing.SystemColors.Control
      Me.btnSupplierFind.Location = New System.Drawing.Point(496, 96)
      Me.btnSupplierFind.Name = "btnSupplierFind"
      Me.btnSupplierFind.Size = New System.Drawing.Size(24, 23)
      Me.btnSupplierFind.TabIndex = 16
      Me.btnSupplierFind.TabStop = False
      Me.btnSupplierFind.ThemedImage = CType(resources.GetObject("btnSupplierFind.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtIssueDate
      '
      Me.Validator.SetDataType(Me.txtIssueDate, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
      Me.Validator.SetDisplayName(Me.txtIssueDate, "")
      Me.ErrorProvider1.SetError(Me.txtIssueDate, "��˹��ѹ����͡���")
      Me.Validator.SetGotFocusBackColor(Me.txtIssueDate, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtIssueDate, -15)
      Me.Validator.SetInvalidBackColor(Me.txtIssueDate, System.Drawing.Color.Empty)
      Me.txtIssueDate.Location = New System.Drawing.Point(400, 24)
      Me.Validator.SetMinValue(Me.txtIssueDate, "")
      Me.txtIssueDate.Name = "txtIssueDate"
      Me.Validator.SetRegularExpression(Me.txtIssueDate, "")
      Me.Validator.SetRequired(Me.txtIssueDate, True)
      Me.txtIssueDate.Size = New System.Drawing.Size(123, 21)
      Me.txtIssueDate.TabIndex = 4
      '
      'txtSupplierCode
      '
      Me.txtSupplierCode.BackColor = System.Drawing.SystemColors.Window
      Me.Validator.SetDataType(Me.txtSupplierCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtSupplierCode, "")
      Me.Validator.SetGotFocusBackColor(Me.txtSupplierCode, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtSupplierCode, -15)
      Me.Validator.SetInvalidBackColor(Me.txtSupplierCode, System.Drawing.Color.Empty)
      Me.txtSupplierCode.Location = New System.Drawing.Point(144, 96)
      Me.Validator.SetMinValue(Me.txtSupplierCode, "")
      Me.txtSupplierCode.Name = "txtSupplierCode"
      Me.Validator.SetRegularExpression(Me.txtSupplierCode, "")
      Me.Validator.SetRequired(Me.txtSupplierCode, False)
      Me.txtSupplierCode.Size = New System.Drawing.Size(128, 21)
      Me.txtSupplierCode.TabIndex = 14
      '
      'txtAmount
      '
      Me.Validator.SetDataType(Me.txtAmount, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DecimalType)
      Me.Validator.SetDisplayName(Me.txtAmount, "")
      Me.txtAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtAmount, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtAmount, -15)
      Me.Validator.SetInvalidBackColor(Me.txtAmount, System.Drawing.Color.Empty)
      Me.txtAmount.Location = New System.Drawing.Point(144, 191)
      Me.Validator.SetMinValue(Me.txtAmount, "")
      Me.txtAmount.Name = "txtAmount"
      Me.Validator.SetRegularExpression(Me.txtAmount, "")
      Me.Validator.SetRequired(Me.txtAmount, True)
      Me.txtAmount.Size = New System.Drawing.Size(128, 21)
      Me.txtAmount.TabIndex = 26
      Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'lblCode
      '
      Me.lblCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCode.ForeColor = System.Drawing.Color.Black
      Me.lblCode.Location = New System.Drawing.Point(8, 25)
      Me.lblCode.Name = "lblCode"
      Me.lblCode.Size = New System.Drawing.Size(128, 18)
      Me.lblCode.TabIndex = 0
      Me.lblCode.Text = "�Ţ����͡���:"
      Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'btnSupplierEdit
      '
      Me.btnSupplierEdit.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnSupplierEdit.Location = New System.Drawing.Point(520, 96)
      Me.btnSupplierEdit.Name = "btnSupplierEdit"
      Me.btnSupplierEdit.Size = New System.Drawing.Size(24, 23)
      Me.btnSupplierEdit.TabIndex = 17
      Me.btnSupplierEdit.TabStop = False
      Me.btnSupplierEdit.ThemedImage = CType(resources.GetObject("btnSupplierEdit.ThemedImage"), System.Drawing.Bitmap)
      '
      'cmbStatus
      '
      Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
      Me.cmbStatus.Enabled = False
      Me.ErrorProvider1.SetIconPadding(Me.cmbStatus, -15)
      Me.cmbStatus.Location = New System.Drawing.Point(144, 215)
      Me.cmbStatus.MaxDropDownItems = 5
      Me.cmbStatus.Name = "cmbStatus"
      Me.cmbStatus.Size = New System.Drawing.Size(128, 21)
      Me.cmbStatus.TabIndex = 29
      Me.cmbStatus.TabStop = False
      '
      'dtpIssueDate
      '
      Me.dtpIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
      Me.dtpIssueDate.Location = New System.Drawing.Point(400, 24)
      Me.dtpIssueDate.Name = "dtpIssueDate"
      Me.dtpIssueDate.Size = New System.Drawing.Size(144, 21)
      Me.dtpIssueDate.TabIndex = 5
      Me.dtpIssueDate.TabStop = False
      '
      'lblSupplier
      '
      Me.lblSupplier.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblSupplier.ForeColor = System.Drawing.Color.Black
      Me.lblSupplier.Location = New System.Drawing.Point(8, 96)
      Me.lblSupplier.Name = "lblSupplier"
      Me.lblSupplier.Size = New System.Drawing.Size(128, 18)
      Me.lblSupplier.TabIndex = 13
      Me.lblSupplier.Text = "�����:"
      Me.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblCqCode
      '
      Me.lblCqCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCqCode.ForeColor = System.Drawing.Color.Black
      Me.lblCqCode.Location = New System.Drawing.Point(8, 49)
      Me.lblCqCode.Name = "lblCqCode"
      Me.lblCqCode.Size = New System.Drawing.Size(128, 18)
      Me.lblCqCode.TabIndex = 6
      Me.lblCqCode.Text = "�Ţ�����:"
      Me.lblCqCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtCqCode
      '
      Me.Validator.SetDataType(Me.txtCqCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCqCode, "")
      Me.txtCqCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtCqCode, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtCqCode, -15)
      Me.Validator.SetInvalidBackColor(Me.txtCqCode, System.Drawing.Color.Empty)
      Me.txtCqCode.Location = New System.Drawing.Point(144, 48)
      Me.Validator.SetMinValue(Me.txtCqCode, "")
      Me.txtCqCode.Name = "txtCqCode"
      Me.Validator.SetRegularExpression(Me.txtCqCode, "")
      Me.Validator.SetRequired(Me.txtCqCode, True)
      Me.txtCqCode.Size = New System.Drawing.Size(128, 21)
      Me.txtCqCode.TabIndex = 7
      '
      'lblIssueDate
      '
      Me.lblIssueDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblIssueDate.ForeColor = System.Drawing.Color.Black
      Me.lblIssueDate.Location = New System.Drawing.Point(296, 25)
      Me.lblIssueDate.Name = "lblIssueDate"
      Me.lblIssueDate.Size = New System.Drawing.Size(96, 18)
      Me.lblIssueDate.TabIndex = 3
      Me.lblIssueDate.Text = "�ѹ�͡��:"
      Me.lblIssueDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblDueDate
      '
      Me.lblDueDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblDueDate.ForeColor = System.Drawing.Color.Black
      Me.lblDueDate.Location = New System.Drawing.Point(280, 48)
      Me.lblDueDate.Name = "lblDueDate"
      Me.lblDueDate.Size = New System.Drawing.Size(112, 18)
      Me.lblDueDate.TabIndex = 8
      Me.lblDueDate.Text = "�ѹ���ú��˹�:"
      Me.lblDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'dtpDueDate
      '
      Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
      Me.dtpDueDate.Location = New System.Drawing.Point(400, 48)
      Me.dtpDueDate.Name = "dtpDueDate"
      Me.dtpDueDate.Size = New System.Drawing.Size(144, 21)
      Me.dtpDueDate.TabIndex = 10
      Me.dtpDueDate.TabStop = False
      '
      'lblBankAccount
      '
      Me.lblBankAccount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblBankAccount.ForeColor = System.Drawing.Color.Black
      Me.lblBankAccount.Location = New System.Drawing.Point(8, 143)
      Me.lblBankAccount.Name = "lblBankAccount"
      Me.lblBankAccount.Size = New System.Drawing.Size(128, 18)
      Me.lblBankAccount.TabIndex = 18
      Me.lblBankAccount.Text = "��ش�Թ�ҡ��Ҥ��:"
      Me.lblBankAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblAmount
      '
      Me.lblAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblAmount.ForeColor = System.Drawing.Color.Black
      Me.lblAmount.Location = New System.Drawing.Point(8, 191)
      Me.lblAmount.Name = "lblAmount"
      Me.lblAmount.Size = New System.Drawing.Size(128, 18)
      Me.lblAmount.TabIndex = 25
      Me.lblAmount.Text = "�ӹǹ�Թ:"
      Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblCurrency
      '
      Me.lblCurrency.AutoSize = True
      Me.lblCurrency.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCurrency.ForeColor = System.Drawing.Color.Black
      Me.lblCurrency.Location = New System.Drawing.Point(280, 191)
      Me.lblCurrency.Name = "lblCurrency"
      Me.lblCurrency.Size = New System.Drawing.Size(27, 13)
      Me.lblCurrency.TabIndex = 27
      Me.lblCurrency.Text = "�ҷ"
      Me.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblNote
      '
      Me.lblNote.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblNote.ForeColor = System.Drawing.Color.Black
      Me.lblNote.Location = New System.Drawing.Point(8, 239)
      Me.lblNote.Name = "lblNote"
      Me.lblNote.Size = New System.Drawing.Size(128, 18)
      Me.lblNote.TabIndex = 30
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
      Me.txtNote.Location = New System.Drawing.Point(144, 239)
      Me.Validator.SetMinValue(Me.txtNote, "")
      Me.txtNote.Name = "txtNote"
      Me.Validator.SetRegularExpression(Me.txtNote, "")
      Me.Validator.SetRequired(Me.txtNote, False)
      Me.txtNote.Size = New System.Drawing.Size(400, 21)
      Me.txtNote.TabIndex = 31
      '
      'lblCheckStatus
      '
      Me.lblCheckStatus.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblCheckStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCheckStatus.ForeColor = System.Drawing.Color.Black
      Me.lblCheckStatus.Location = New System.Drawing.Point(8, 215)
      Me.lblCheckStatus.Name = "lblCheckStatus"
      Me.lblCheckStatus.Size = New System.Drawing.Size(128, 18)
      Me.lblCheckStatus.TabIndex = 28
      Me.lblCheckStatus.Text = "ʶҹ��礨���:"
      Me.lblCheckStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtrecipient
      '
      Me.Validator.SetDataType(Me.txtrecipient, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtrecipient, "")
      Me.txtrecipient.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtrecipient, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtrecipient, -15)
      Me.Validator.SetInvalidBackColor(Me.txtrecipient, System.Drawing.Color.Empty)
      Me.txtrecipient.Location = New System.Drawing.Point(144, 72)
      Me.Validator.SetMinValue(Me.txtrecipient, "")
      Me.txtrecipient.Name = "txtrecipient"
      Me.Validator.SetRegularExpression(Me.txtrecipient, "")
      Me.Validator.SetRequired(Me.txtrecipient, False)
      Me.txtrecipient.Size = New System.Drawing.Size(400, 21)
      Me.txtrecipient.TabIndex = 12
      '
      'lblrecipient
      '
      Me.lblrecipient.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblrecipient.ForeColor = System.Drawing.Color.Black
      Me.lblrecipient.Location = New System.Drawing.Point(8, 72)
      Me.lblrecipient.Name = "lblrecipient"
      Me.lblrecipient.Size = New System.Drawing.Size(128, 18)
      Me.lblrecipient.TabIndex = 11
      Me.lblrecipient.Text = "����Ѻ����:"
      Me.lblrecipient.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblStatus
      '
      Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.lblStatus.AutoSize = True
      Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblStatus.ForeColor = System.Drawing.Color.Black
      Me.lblStatus.Location = New System.Drawing.Point(8, 496)
      Me.lblStatus.Name = "lblStatus"
      Me.lblStatus.Size = New System.Drawing.Size(69, 13)
      Me.lblStatus.TabIndex = 32
      Me.lblStatus.Text = "Status ��꺼�"
      Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblBank
      '
      Me.lblBank.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblBank.ForeColor = System.Drawing.Color.Black
      Me.lblBank.Location = New System.Drawing.Point(8, 167)
      Me.lblBank.Name = "lblBank"
      Me.lblBank.Size = New System.Drawing.Size(128, 18)
      Me.lblBank.TabIndex = 23
      Me.lblBank.Text = "��Ҥ��/�Ң�:"
      Me.lblBank.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtbankbranch
      '
      Me.Validator.SetDataType(Me.txtbankbranch, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtbankbranch, "")
      Me.txtbankbranch.Enabled = False
      Me.txtbankbranch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtbankbranch, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtbankbranch, -15)
      Me.Validator.SetInvalidBackColor(Me.txtbankbranch, System.Drawing.Color.Empty)
      Me.txtbankbranch.Location = New System.Drawing.Point(144, 167)
      Me.Validator.SetMinValue(Me.txtbankbranch, "")
      Me.txtbankbranch.Name = "txtbankbranch"
      Me.txtbankbranch.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtbankbranch, "")
      Me.Validator.SetRequired(Me.txtbankbranch, False)
      Me.txtbankbranch.Size = New System.Drawing.Size(400, 21)
      Me.txtbankbranch.TabIndex = 24
      Me.txtbankbranch.TabStop = False
      '
      'txtBankAccountCode
      '
      Me.txtBankAccountCode.BackColor = System.Drawing.SystemColors.Window
      Me.Validator.SetDataType(Me.txtBankAccountCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtBankAccountCode, "")
      Me.txtBankAccountCode.Enabled = False
      Me.Validator.SetGotFocusBackColor(Me.txtBankAccountCode, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtBankAccountCode, -15)
      Me.Validator.SetInvalidBackColor(Me.txtBankAccountCode, System.Drawing.Color.Empty)
      Me.txtBankAccountCode.Location = New System.Drawing.Point(144, 143)
      Me.Validator.SetMinValue(Me.txtBankAccountCode, "")
      Me.txtBankAccountCode.Name = "txtBankAccountCode"
      Me.Validator.SetRegularExpression(Me.txtBankAccountCode, "")
      Me.Validator.SetRequired(Me.txtBankAccountCode, True)
      Me.txtBankAccountCode.Size = New System.Drawing.Size(128, 21)
      Me.txtBankAccountCode.TabIndex = 19
      '
      'txtSupplierName
      '
      Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Control
      Me.Validator.SetDataType(Me.txtSupplierName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtSupplierName, "")
      Me.txtSupplierName.Enabled = False
      Me.Validator.SetGotFocusBackColor(Me.txtSupplierName, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtSupplierName, -15)
      Me.Validator.SetInvalidBackColor(Me.txtSupplierName, System.Drawing.Color.Empty)
      Me.txtSupplierName.Location = New System.Drawing.Point(272, 96)
      Me.Validator.SetMinValue(Me.txtSupplierName, "")
      Me.txtSupplierName.Name = "txtSupplierName"
      Me.txtSupplierName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtSupplierName, "")
      Me.Validator.SetRequired(Me.txtSupplierName, False)
      Me.txtSupplierName.Size = New System.Drawing.Size(224, 21)
      Me.txtSupplierName.TabIndex = 15
      Me.txtSupplierName.TabStop = False
      '
      'txtBankAccountName
      '
      Me.txtBankAccountName.BackColor = System.Drawing.SystemColors.Control
      Me.Validator.SetDataType(Me.txtBankAccountName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtBankAccountName, "")
      Me.txtBankAccountName.Enabled = False
      Me.Validator.SetGotFocusBackColor(Me.txtBankAccountName, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtBankAccountName, -15)
      Me.Validator.SetInvalidBackColor(Me.txtBankAccountName, System.Drawing.Color.Empty)
      Me.txtBankAccountName.Location = New System.Drawing.Point(272, 143)
      Me.Validator.SetMinValue(Me.txtBankAccountName, "")
      Me.txtBankAccountName.Name = "txtBankAccountName"
      Me.txtBankAccountName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtBankAccountName, "")
      Me.Validator.SetRequired(Me.txtBankAccountName, False)
      Me.txtBankAccountName.Size = New System.Drawing.Size(272, 21)
      Me.txtBankAccountName.TabIndex = 20
      Me.txtBankAccountName.TabStop = False
      '
      'txtCode
      '
      Me.Validator.SetDataType(Me.txtCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCode, "")
      Me.ErrorProvider1.SetError(Me.txtCode, "��˹��Ţ����͡���")
      Me.txtCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtCode, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtCode, -15)
      Me.Validator.SetInvalidBackColor(Me.txtCode, System.Drawing.Color.Empty)
      Me.txtCode.Location = New System.Drawing.Point(144, 24)
      Me.Validator.SetMinValue(Me.txtCode, "")
      Me.txtCode.Name = "txtCode"
      Me.Validator.SetRegularExpression(Me.txtCode, "")
      Me.Validator.SetRequired(Me.txtCode, True)
      Me.txtCode.Size = New System.Drawing.Size(128, 21)
      Me.txtCode.TabIndex = 1
      '
      'Validator
      '
      Me.Validator.BackcolorChanging = False
      Me.Validator.DataTable = Nothing
      Me.Validator.ErrorProvider = Me.ErrorProvider1
      Me.Validator.GotFocusBackColor = System.Drawing.Color.Empty
      Me.Validator.HasNewRow = False
      Me.Validator.InvalidBackColor = System.Drawing.Color.Empty
      '
      'ErrorProvider1
      '
      Me.ErrorProvider1.ContainerControl = Me
      '
      'btnLoanFind
      '
      Me.btnLoanFind.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnLoanFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnLoanFind.ForeColor = System.Drawing.SystemColors.Control
      Me.btnLoanFind.Location = New System.Drawing.Point(496, 119)
      Me.btnLoanFind.Name = "btnLoanFind"
      Me.btnLoanFind.Size = New System.Drawing.Size(24, 23)
      Me.btnLoanFind.TabIndex = 206
      Me.btnLoanFind.TabStop = False
      Me.btnLoanFind.ThemedImage = CType(resources.GetObject("btnLoanFind.ThemedImage"), System.Drawing.Bitmap)
      '
      'lblLoan
      '
      Me.lblLoan.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblLoan.ForeColor = System.Drawing.Color.Black
      Me.lblLoan.Location = New System.Drawing.Point(8, 119)
      Me.lblLoan.Name = "lblLoan"
      Me.lblLoan.Size = New System.Drawing.Size(128, 18)
      Me.lblLoan.TabIndex = 203
      Me.lblLoan.Text = "��Թ���:"
      Me.lblLoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtLoanCode
      '
      Me.txtLoanCode.BackColor = System.Drawing.SystemColors.Window
      Me.Validator.SetDataType(Me.txtLoanCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtLoanCode, "")
      Me.Validator.SetGotFocusBackColor(Me.txtLoanCode, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtLoanCode, -15)
      Me.Validator.SetInvalidBackColor(Me.txtLoanCode, System.Drawing.Color.Empty)
      Me.txtLoanCode.Location = New System.Drawing.Point(144, 119)
      Me.Validator.SetMinValue(Me.txtLoanCode, "")
      Me.txtLoanCode.Name = "txtLoanCode"
      Me.Validator.SetRegularExpression(Me.txtLoanCode, "")
      Me.Validator.SetRequired(Me.txtLoanCode, True)
      Me.txtLoanCode.Size = New System.Drawing.Size(128, 21)
      Me.txtLoanCode.TabIndex = 204
      '
      'txtLoanName
      '
      Me.txtLoanName.BackColor = System.Drawing.SystemColors.Control
      Me.Validator.SetDataType(Me.txtLoanName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtLoanName, "")
      Me.txtLoanName.Enabled = False
      Me.Validator.SetGotFocusBackColor(Me.txtLoanName, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtLoanName, -15)
      Me.Validator.SetInvalidBackColor(Me.txtLoanName, System.Drawing.Color.Empty)
      Me.txtLoanName.Location = New System.Drawing.Point(272, 119)
      Me.Validator.SetMinValue(Me.txtLoanName, "")
      Me.txtLoanName.Name = "txtLoanName"
      Me.txtLoanName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtLoanName, "")
      Me.Validator.SetRequired(Me.txtLoanName, False)
      Me.txtLoanName.Size = New System.Drawing.Size(224, 21)
      Me.txtLoanName.TabIndex = 205
      Me.txtLoanName.TabStop = False
      '
      'btnLoanEdit
      '
      Me.btnLoanEdit.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnLoanEdit.Location = New System.Drawing.Point(520, 119)
      Me.btnLoanEdit.Name = "btnLoanEdit"
      Me.btnLoanEdit.Size = New System.Drawing.Size(24, 23)
      Me.btnLoanEdit.TabIndex = 207
      Me.btnLoanEdit.TabStop = False
      Me.btnLoanEdit.ThemedImage = CType(resources.GetObject("btnLoanEdit.ThemedImage"), System.Drawing.Bitmap)
      '
      'OutgoingAvalDetailView
      '
      Me.Controls.Add(Me.grbOutgoingAval)
      Me.Name = "OutgoingAvalDetailView"
      Me.Size = New System.Drawing.Size(640, 504)
      Me.grbOutgoingAval.ResumeLayout(False)
      Me.grbOutgoingAval.PerformLayout()
      Me.ResumeLayout(False)

    End Sub

#End Region

#Region " SetLabelText "
    Public Overrides Sub SetLabelText()
      If Not Me.m_entity Is Nothing Then Me.Text = Me.StringParserService.Parse(Me.m_entity.TabPageText)
      Me.lblCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblCode}")
      Me.Validator.SetDisplayName(txtCode, lblCode.Text)

      Me.lblCqCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblCqCode}")
      Me.Validator.SetDisplayName(txtCqCode, lblCqCode.Text)

      Me.lblIssueDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblIssueDate}")
      Me.Validator.SetDisplayName(txtIssueDate, lblIssueDate.Text)

      Me.lblDueDate.Text = Me.StringParserService.Parse("${res:Global.DueDateText}")
      Me.Validator.SetDisplayName(txtDueDate, lblDueDate.Text)

      Me.lblrecipient.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblrecipient}")
      Me.Validator.SetDisplayName(txtrecipient, lblrecipient.Text)

      Me.lblSupplier.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblSupplier}")
      Me.Validator.SetDisplayName(txtSupplierCode, lblSupplier.Text)

      Me.lblBankAccount.Text = Me.StringParserService.Parse("${res:Global.BankAccountText}")
      Me.lblBank.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblBank}")
      Me.Validator.SetDisplayName(txtBankAccountCode, lblBankAccount.Text)

      Me.lblAmount.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblAmount}")
      Me.Validator.SetDisplayName(txtAmount, lblAmount.Text)

      Me.lblNote.Text = Me.StringParserService.Parse("${res:Global.NoteText}")
      Me.Validator.SetDisplayName(txtNote, lblNote.Text)

      Me.lblCurrency.Text = Me.StringParserService.Parse("${res:Global.CurrencyUnit}")
      Me.lblCheckStatus.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.OutgoingCheckDetailView.lblCheckStatus}")
    End Sub
#End Region

#Region "Member"
    Private m_entity As New OutgoingAval
    Private m_isInitialized As Boolean = False
    Private m_treeManager As TreeManager
#End Region

#Region "Constructor"
    Public Sub New()
      MyBase.New()
      InitializeComponent()
      Initialize()

      Dim dt As TreeTable = OutgoingAval.GetSchemaTable()
      Dim dst As DataGridTableStyle = OutgoingAval.CreateTableStyle()
      m_treeManager = New TreeManager(dt, tgItem)
      m_treeManager.SetTableStyle(dst)
      m_treeManager.AllowSorting = False
      m_treeManager.AllowDelete = False

      For Each colStyle As DataGridColumnStyle In Me.m_treeManager.GridTableStyle.GridColumnStyles
        colStyle.ReadOnly = True
      Next

      If CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
        Me.Validator.SetRequired(txtCqCode, False)
        Me.Validator.SetRequired(txtDueDate, False)
      End If

      Me.SetLabelText()
      Me.UpdateEntityProperties()
      Me.EventWiring()
    End Sub
#End Region

#Region "Method"
    Private Sub SetBankBranch()
      Dim oldstatus As Boolean = Me.m_isInitialized
      Me.m_isInitialized = False
      If m_entity.Loan.BankAccount Is Nothing _
      OrElse Not Me.m_entity.Loan.BankAccount.Originated Then
        txtbankbranch.Text = ""
      Else
        txtbankbranch.Text = Me.m_entity.Loan.BankAccount.BankBranch.Bank.Name & " : " & Me.m_entity.Loan.BankAccount.BankBranch.Name
      End If
      Me.m_isInitialized = oldstatus
    End Sub
    Private Sub SetBankAccount()
      Dim oldstatus As Boolean = Me.m_isInitialized
      Me.m_isInitialized = False
      If m_entity.Loan.BankAccount Is Nothing _
      OrElse Not Me.m_entity.Loan.BankAccount.Originated Then
        txtBankAccountCode.Text = ""
        txtBankAccountName.Text = ""
      Else
        txtBankAccountCode.Text = Me.m_entity.Loan.BankAccount.BankCode
        txtbankbranch.Text = Me.m_entity.Loan.BankAccount.Name
      End If
      Me.m_isInitialized = oldstatus
    End Sub
#End Region

#Region "ISimpleEntityPanel"
    Public Overrides Sub Initialize()
      OutgoingCheckDocStatus.ListCodeDescriptionInComboBox(cmbStatus, "outgoingcheck_docstatus")
    End Sub

    Protected Overrides Sub EventWiring()
      AddHandler txtCode.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtCqCode.TextChanged, AddressOf Me.ChangeProperty

      AddHandler txtIssueDate.Validated, AddressOf Me.ChangeProperty
      AddHandler dtpIssueDate.ValueChanged, AddressOf Me.ChangeProperty

      AddHandler txtDueDate.Validated, AddressOf Me.ChangeProperty
      AddHandler dtpDueDate.ValueChanged, AddressOf Me.ChangeProperty

      AddHandler txtrecipient.TextChanged, AddressOf Me.ChangeProperty

      AddHandler txtSupplierCode.Validated, AddressOf Me.ChangeProperty
      AddHandler txtBankAccountCode.Validated, AddressOf Me.ChangeProperty

      AddHandler txtAmount.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtAmount.Validated, AddressOf Me.NumberTextBoxChange

      AddHandler txtNote.TextChanged, AddressOf Me.ChangeProperty

      AddHandler cmbStatus.SelectedIndexChanged, AddressOf Me.ChangeProperty
    End Sub
    ' ��Ǩ�ͺʶҹТͧ�����
    Public Overrides Sub CheckFormEnable()
      If Me.m_entity Is Nothing Then
        Return
      End If
      If Me.m_entity.Status.Value = 0 _
          OrElse Me.m_entity.Status.Value >= 3 _
          OrElse Me.m_entity.DocStatus.Value = 0 _
          OrElse Me.m_entity.DocStatus.Value = 2 Then   '{-1 �ѧ���ѹ�֡, 0 ¡��ԡ  , 1 ����  , 2 �礼�ҹ }
        If Not CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
          grbOutgoingAval.Enabled = False
        Else
          For Each ctrl As Control In grbOutgoingAval.Controls
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is Button Then
              'MessageBox.Show(Me.m_entity.Supplier.Id)
              If ctrl.Name = "txtrecipient" And Me.m_entity.Supplier.invisible = True Then   '��������Թʴ���¶֧�������¹���ͤ��Ѻ�� 
                'MessageBox.Show(ctrl.Name & " isTrue")
              Else
                ctrl.Enabled = False
                'MessageBox.Show(ctrl.Name & " isFalse")
              End If
            End If
          Next
          dtpIssueDate.Enabled = False
          txtBankAccountCode.Enabled = False
          If txtCqCode.Text.Length = 0 OrElse txtDueDate.Text.Length = 0 Then
            txtCqCode.Enabled = True
            txtDueDate.Enabled = True
            dtpDueDate.Enabled = True
            'txtBankAccountCode.Enabled = True
            'btnBankAccountFind.Enabled = True
            'btnBankAccountEdit.Enabled = True
          Else
            txtCqCode.Enabled = False
            txtDueDate.Enabled = False
            dtpDueDate.Enabled = False
            'txtBankAccountCode.Enabled = False
            'btnBankAccountFind.Enabled = False
            'btnBankAccountEdit.Enabled = False
          End If
        End If

      Else

        If Not CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
          grbOutgoingAval.Enabled = True
        Else
          For Each ctrl As Control In grbOutgoingAval.Controls
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is Button Then
              ctrl.Enabled = True
            End If
          Next
          dtpIssueDate.Enabled = True
          txtCqCode.Enabled = True
          txtDueDate.Enabled = True
          dtpDueDate.Enabled = True
          txtBankAccountCode.Enabled = True
        End If
      End If
      If txtrecipient.Text.Length = 0 Then
        txtrecipient.Enabled = True
      End If
      Me.cmbStatus.Enabled = False
    End Sub

    ' ������������ control
    Public Overrides Sub ClearDetail()
      For Each ctrl As Control In grbOutgoingAval.Controls
        If TypeOf ctrl Is TextBox Then
          ctrl.Text = ""
        End If
      Next

      txtIssueDate.Text = Me.StringParserService.Parse("${res:Global.BlankDateText}")
      txtDueDate.Text = Me.StringParserService.Parse("${res:Global.BlankDateText}")

      dtpIssueDate.Value = Date.Now
      dtpDueDate.Value = Date.Now

      cmbStatus.SelectedIndex = 0
      cmbStatus.SelectedIndex = 0
    End Sub

    ' �ʴ���Ң�����ŧ� control ������躹�����
    Public Overrides Sub UpdateEntityProperties()
      m_isInitialized = False
      ClearDetail()

      If m_entity Is Nothing Then
        Return
      End If

      ' �ӡ�ü١ Property ��ҧ � ��ҡѺ control
      With Me
        .txtCode.Text = .m_entity.Code
        .txtCqCode.Text = .m_entity.CqCode
        ' autogencode 
        m_oldCode = m_entity.Code
        Me.chkAutorun.Checked = Me.m_entity.AutoGen
        Me.UpdateAutogenStatus()

        dtpIssueDate.Value = MinDateToNow(Me.m_entity.IssueDate)
        txtIssueDate.Text = MinDateToNull(Me.m_entity.IssueDate, Me.StringParserService.Parse("${res:Global.BlankDateText}"))

        dtpDueDate.Value = MinDateToNow(Me.m_entity.DueDate)
        If Me.m_entity.Originated Or CBool(Configuration.GetConfig("AllowNoCqCodeDate")) Then
          txtDueDate.Text = MinDateToNull(Me.m_entity.DueDate, "")
        Else
          txtDueDate.Text = MinDateToNull(Me.m_entity.DueDate, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
        End If


        txtrecipient.Text = .m_entity.Recipient

        txtAmount.Text = Configuration.FormatToString(Me.m_entity.Amount, DigitConfig.Price)

        txtNote.Text = .m_entity.Note

        If Not .m_entity.Supplier Is Nothing Then
          txtSupplierCode.Text = .m_entity.Supplier.Code
          txtSupplierName.Text = .m_entity.Supplier.Name
        End If

        If Not .m_entity.Loan Is Nothing Then
          txtLoanCode.Text = .m_entity.Loan.Code
          txtLoanName.Text = .m_entity.Loan.Name

          If Not .m_entity.Loan.BankAccount Is Nothing Then
            txtBankAccountCode.Text = .m_entity.Loan.BankAccount.Code
            txtBankAccountName.Text = .m_entity.Loan.BankAccount.Name
            SetBankBranch()
          End If
        End If

        

        If Not .m_entity.DocStatus Is Nothing Then
          Dim desc As String = Me.m_entity.DocStatus.GetDescription("outgoingcheck_docstatus", Me.m_entity.DocStatus.Value)
          cmbStatus.FindStringExact(desc)
          cmbStatus.SelectedIndex = cmbStatus.FindStringExact(desc)
        End If

      End With

      Me.m_entity.ReLoadItems()

      'Load Items**********************************************************
      Me.m_treeManager.Treetable = Me.m_entity.ItemTable
      Me.Validator.DataTable = m_treeManager.Treetable
      '********************************************************************
      UpdateAmount()

      SetStatus()
      CheckFormEnable()
      SetLabelText()

      m_isInitialized = True
    End Sub
    Private Sub SetStatus()
      If Not IsNothing(m_entity.CancelDate) And Not m_entity.CancelDate.Equals(Date.MinValue) Then
        lblStatus.Text = "¡��ԡ: " & m_entity.CancelDate.ToShortDateString & _
        " " & m_entity.CancelDate.ToShortTimeString & _
        "  ��:" & m_entity.CancelPerson.Name
      ElseIf Not IsNothing(m_entity.LastEditDate) And Not m_entity.LastEditDate.Equals(Date.MinValue) Then
        lblStatus.Text = "�������ش: " & m_entity.LastEditDate.ToShortDateString & _
        " " & m_entity.LastEditDate.ToShortTimeString & _
        "  ��:" & m_entity.LastEditor.Name
      ElseIf Not IsNothing(m_entity.OriginDate) And Not m_entity.OriginDate.Equals(Date.MinValue) Then
        lblStatus.Text = "�����������к�: " & m_entity.OriginDate.ToShortDateString & _
        " " & m_entity.OriginDate.ToShortTimeString & _
        "  ��:" & m_entity.Originator.Name
      Else
        lblStatus.Text = "�ѧ�����ѹ�֡"
      End If
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
    Public Sub ChangeProperty(ByVal sender As Object, ByVal e As EventArgs)
      If Me.m_entity Is Nothing Or Not m_isInitialized Then
        Return
      End If
      Dim dirtyFlag As Boolean
      Select Case CType(sender, Control).Name.ToLower
        Case "txtcode"
          Me.m_entity.Code = Me.txtCode.Text
          dirtyFlag = True

        Case "txtcqcode"
          dirtyFlag = True
          Me.m_entity.CqCode = Me.txtCqCode.Text

        Case "dtpduedate"
          txtDueDate.Text = MinDateToNull(dtpDueDate.Value, "")
          Me.m_entity.DueDate = Me.dtpDueDate.Value
          dirtyFlag = True

        Case "txtduedate"
          If Me.txtDueDate.Text.Length > 0 Then
            Dim theDate As Date = CDate(Me.txtDueDate.Text)
            If Not Me.m_entity.DueDate.Equals(theDate) Then
              Dim dt As DateTime = StringToDate(txtDueDate, dtpDueDate)
              Me.m_entity.DueDate = dt
              dirtyFlag = True
            End If
          Else
            Me.m_entity.DueDate = DateTime.MinValue
            dirtyFlag = True
          End If

        Case "dtpissuedate"
          txtIssueDate.Text = MinDateToNull(dtpIssueDate.Value, "")
          Me.m_entity.IssueDate = Me.dtpIssueDate.Value
          dirtyFlag = True

        Case "txtissuedate"
          Dim dt As DateTime = StringToDate(txtIssueDate, dtpIssueDate)
          Me.m_entity.DueDate = dt
          dirtyFlag = True

        Case "txtrecipient"
          Me.m_entity.Recipient = Me.txtrecipient.Text
          dirtyFlag = True

        Case "txtamount"
          If txtAmount.TextLength > 0 Then
            Me.m_entity.Amount = CDec(Me.txtAmount.Text)
          Else
            Me.m_entity.Amount = Nothing
          End If
          'UpdateAmount()
          dirtyFlag = True

        Case "txtnote"
          Me.m_entity.Note = Me.txtNote.Text
          dirtyFlag = True

        Case "cmbstatus"
          Me.m_entity.DocStatus = New OutgoingCheckDocStatus(Me.cmbStatus.SelectedIndex)
          dirtyFlag = True

        Case "txtloancode"
          dirtyFlag = Loan.GetLoan(txtBankAccountCode, txtBankAccountName, Me.m_entity.Loan)
          SetBankBranch()

        Case "txtsuppliercode"
          dirtyFlag = Supplier.GetSupplier(txtSupplierCode, txtSupplierName, Me.m_entity.Supplier, True)
          Dim tmp As Boolean = m_isInitialized
          m_isInitialized = False
          Me.txtrecipient.Text = Me.m_entity.Recipient
          m_isInitialized = tmp
      End Select

      Me.WorkbenchWindow.ViewContent.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty Or dirtyFlag

      SetStatus()
      'CheckFormEnable()

    End Sub

    Public Overrides Property Entity() As ISimpleEntity
      Get
        Return Me.m_entity
      End Get
      Set(ByVal Value As ISimpleEntity)
        Me.m_entity = CType(Value, OutgoingAval)
        Me.m_entity.OnTabPageTextChanged(m_entity, EventArgs.Empty)
        UpdateEntityProperties()
        EventWiring()
      End Set
    End Property

#End Region

#Region "IReversibleEntityProperty"
    Public Sub RevertProperties() Implements IReversibleEntityProperty.RevertProperties

    End Sub

    Public Sub SaveProperties() Implements IReversibleEntityProperty.SaveProperties

    End Sub
#End Region

#Region "IValidatable"
    Public ReadOnly Property FormValidator() As Components.PJMTextboxValidator Implements IValidatable.FormValidator
      Get
        Return Me.Validator
      End Get
    End Property
#End Region

#Region "IClipboardHandler Overrides"
    Public Overrides ReadOnly Property EnablePaste() As Boolean
      Get
        Dim data As IDataObject = Clipboard.GetDataObject
        If data.GetDataPresent((New Supplier).FullClassName) Then
          If Not Me.ActiveControl Is Nothing Then
            Select Case Me.ActiveControl.Name.ToLower
              Case "txtsuppliercode", "txtsuppliername"
                Return True
            End Select
          End If
        End If
        If data.GetDataPresent((New BankAccount).FullClassName) Then
          If Not Me.ActiveControl Is Nothing Then
            Select Case Me.ActiveControl.Name.ToLower
              Case "txtloancode", "txtloanname"
                Return True
            End Select
          End If
        End If
        Return False
      End Get
    End Property
    Public Overrides Sub Paste(ByVal sender As Object, ByVal e As System.EventArgs)
      Dim data As IDataObject = Clipboard.GetDataObject
      If data.GetDataPresent((New Supplier).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New Supplier).FullClassName))
        Dim entity As New Supplier(id)
        If Not Me.ActiveControl Is Nothing Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtsuppliercode", "txtsuppliername"
              Me.SetSupplierDialog(entity)
          End Select
        End If
      End If
      If data.GetDataPresent((New BankAccount).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New BankAccount).FullClassName))
        Dim entity As New BankAccount(id)
        If Not Me.ActiveControl Is Nothing Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtloancode", "txtloanname"
              Me.SetLoanDialog(entity)
          End Select
        End If
      End If
    End Sub
#End Region

#Region " Autogencode"
    Private Sub chkAutorun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutorun.CheckedChanged
      UpdateAutogenStatus()
    End Sub
    Private m_oldCode As String = ""
    Private Sub UpdateAutogenStatus()
      If Me.chkAutorun.Checked Then
        Me.Validator.SetRequired(Me.txtCode, False)
        Me.ErrorProvider1.SetError(Me.txtCode, "")
        Me.txtCode.ReadOnly = True
        m_oldCode = Me.txtCode.Text
        Me.txtCode.Text = BusinessLogic.Entity.GetAutoCodeFormat(Me.m_entity.EntityId)
        'Hack: set Code �� "" �ͧ
        Me.m_entity.Code = ""
        Me.m_entity.AutoGen = True
      Else
        Me.Validator.SetRequired(Me.txtCode, True)
        Me.txtCode.Text = m_oldCode
        Me.txtCode.ReadOnly = False
        Me.m_entity.AutoGen = False
      End If
    End Sub
#End Region

#Region " Event of Button controls "
    Private Sub btnSupplierFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierFind.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New Supplier, AddressOf SetSupplierDialog)
    End Sub

    Private Sub SetSupplierDialog(ByVal e As ISimpleEntity)
      Me.txtSupplierCode.Text = e.Code
      Me.WorkbenchWindow.ViewContent.IsDirty = _
          Me.WorkbenchWindow.ViewContent.IsDirty Or _
          Supplier.GetSupplier(txtSupplierCode, txtSupplierName, Me.m_entity.Supplier, True)
      Dim tmp As Boolean = m_isInitialized
      m_isInitialized = False
      Me.txtrecipient.Text = Me.m_entity.Recipient
      m_isInitialized = tmp
    End Sub

    'Private Sub btnBankAccountFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
    'myEntityPanelService.OpenListDialog(New BankAccount, AddressOf SetBankAccountDialog)
    'End Sub

    'Private Sub SetBankAccountDialog(ByVal e As ISimpleEntity)
    'Me.txtBankAccountCode.Text = e.Code
    'Me.WorkbenchWindow.ViewContent.IsDirty = _
    'Me.WorkbenchWindow.ViewContent.IsDirty Or _
    'BankAccount.GetBankAccount(txtBankAccountCode, txtBankAccountName, Me.m_entity.Bankacct)
    'SetBankBranch()
    'End Sub

    Private Sub btnSupplierEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierEdit.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New Supplier)
    End Sub

    Private Sub btnBankAccountEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New BankAccount)
    End Sub
    Private Sub btnLoanFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoanFind.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New Loan, AddressOf SetLoanDialog)
    End Sub
    Private Sub btnLoanEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoanEdit.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New Loan)
    End Sub
    Private Sub SetLoanDialog(ByVal e As ISimpleEntity)
      Me.txtLoanCode.Text = e.Code
      Me.WorkbenchWindow.ViewContent.IsDirty = _
          Me.WorkbenchWindow.ViewContent.IsDirty Or _
      Loan.GetLoan(txtLoanCode, txtLoanName, Me.m_entity.Loan)
      'BankAccount.GetBankAccount(txtLoanCode, txtLoanName, Me.m_entity.Bankacct)
      SetBankAccount()
      SetBankBranch()
    End Sub
#End Region

    Private Sub UpdateAmount()
      txtTotal.Text = Configuration.FormatToString(m_entity.GetRemainingAmount, DigitConfig.Price)
    End Sub


    Private Sub lblItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    
  End Class

End Namespace
