Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.Services
Imports Longkong.Core.Services
Namespace Longkong.Pojjaman.Gui.Panels
    Public Class PAFilterSubPanel
    Inherits AbstractFilterSubPanel
    'Inherits UserControl

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
    Friend WithEvents grbDetail As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents grbDocDate As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents lblDocDateStart As System.Windows.Forms.Label
    Friend WithEvents lblDocDateEnd As System.Windows.Forms.Label
    Friend WithEvents dtpDocDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDocDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents grbMainDetail As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Validator As Longkong.Pojjaman.Gui.Components.PJMTextboxValidator
    Friend WithEvents btnSupplierPanel As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierDialog As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents lblSupplier As System.Windows.Forms.Label
    Friend WithEvents txtDocDateEnd As System.Windows.Forms.TextBox
    Friend WithEvents txtDocDateStart As System.Windows.Forms.TextBox
    Friend WithEvents btnCostCenterPanel As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtCostCenterCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCostCenterName As System.Windows.Forms.TextBox
    Friend WithEvents btnCostCenterDialog As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents grbApprove As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents cmbApproveLevel As System.Windows.Forms.ComboBox
    Friend WithEvents lblApproveLevel As System.Windows.Forms.Label
    Friend WithEvents txtApprovePerson As System.Windows.Forms.TextBox
    Friend WithEvents txtApprovePersonName As System.Windows.Forms.TextBox
    Friend WithEvents lblApprovePerson As System.Windows.Forms.Label
    Friend WithEvents btnFineApprove As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents grbSC As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents txtSCCode As System.Windows.Forms.TextBox
    Friend WithEvents lblSC As System.Windows.Forms.Label
    Friend WithEvents btnSCDialog As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents lblCC As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PAFilterSubPanel))
      Me.lblCode = New System.Windows.Forms.Label()
      Me.txtCode = New System.Windows.Forms.TextBox()
      Me.grbDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.grbApprove = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.cmbApproveLevel = New System.Windows.Forms.ComboBox()
      Me.lblApproveLevel = New System.Windows.Forms.Label()
      Me.txtApprovePerson = New System.Windows.Forms.TextBox()
      Me.txtApprovePersonName = New System.Windows.Forms.TextBox()
      Me.lblApprovePerson = New System.Windows.Forms.Label()
      Me.btnFineApprove = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.grbDocDate = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.txtDocDateStart = New System.Windows.Forms.TextBox()
      Me.txtDocDateEnd = New System.Windows.Forms.TextBox()
      Me.lblDocDateStart = New System.Windows.Forms.Label()
      Me.lblDocDateEnd = New System.Windows.Forms.Label()
      Me.dtpDocDateStart = New System.Windows.Forms.DateTimePicker()
      Me.dtpDocDateEnd = New System.Windows.Forms.DateTimePicker()
      Me.btnSearch = New System.Windows.Forms.Button()
      Me.btnReset = New System.Windows.Forms.Button()
      Me.grbMainDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.btnCostCenterPanel = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtCostCenterCode = New System.Windows.Forms.TextBox()
      Me.txtCostCenterName = New System.Windows.Forms.TextBox()
      Me.btnCostCenterDialog = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.lblCC = New System.Windows.Forms.Label()
      Me.cmbStatus = New System.Windows.Forms.ComboBox()
      Me.lblStatus = New System.Windows.Forms.Label()
      Me.btnSupplierPanel = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtSupplierCode = New System.Windows.Forms.TextBox()
      Me.txtSupplierName = New System.Windows.Forms.TextBox()
      Me.btnSupplierDialog = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.lblSupplier = New System.Windows.Forms.Label()
      Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
      Me.Validator = New Longkong.Pojjaman.Gui.Components.PJMTextboxValidator(Me.components)
      Me.grbSC = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.txtSCCode = New System.Windows.Forms.TextBox()
      Me.lblSC = New System.Windows.Forms.Label()
      Me.btnSCDialog = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.grbDetail.SuspendLayout()
      Me.grbApprove.SuspendLayout()
      Me.grbDocDate.SuspendLayout()
      Me.grbMainDetail.SuspendLayout()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.grbSC.SuspendLayout()
      Me.SuspendLayout()
      '
      'lblCode
      '
      Me.lblCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCode.ForeColor = System.Drawing.Color.Black
      Me.lblCode.Location = New System.Drawing.Point(9, 16)
      Me.lblCode.Name = "lblCode"
      Me.lblCode.Size = New System.Drawing.Size(88, 18)
      Me.lblCode.TabIndex = 3
      Me.lblCode.Text = "����:"
      Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtCode
      '
      Me.Validator.SetDataType(Me.txtCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCode, "")
      Me.txtCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCode, System.Drawing.Color.Empty)
      Me.txtCode.Location = New System.Drawing.Point(97, 16)
      Me.Validator.SetMinValue(Me.txtCode, "")
      Me.txtCode.Name = "txtCode"
      Me.Validator.SetRegularExpression(Me.txtCode, "")
      Me.Validator.SetRequired(Me.txtCode, False)
      Me.txtCode.Size = New System.Drawing.Size(232, 21)
      Me.txtCode.TabIndex = 0
      '
      'grbDetail
      '
      Me.grbDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.grbDetail.Controls.Add(Me.grbSC)
      Me.grbDetail.Controls.Add(Me.grbApprove)
      Me.grbDetail.Controls.Add(Me.grbDocDate)
      Me.grbDetail.Controls.Add(Me.btnSearch)
      Me.grbDetail.Controls.Add(Me.btnReset)
      Me.grbDetail.Controls.Add(Me.grbMainDetail)
      Me.grbDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbDetail.Location = New System.Drawing.Point(8, 4)
      Me.grbDetail.Name = "grbDetail"
      Me.grbDetail.Size = New System.Drawing.Size(683, 207)
      Me.grbDetail.TabIndex = 0
      Me.grbDetail.TabStop = False
      '
      'grbApprove
      '
      Me.grbApprove.Controls.Add(Me.cmbApproveLevel)
      Me.grbApprove.Controls.Add(Me.lblApproveLevel)
      Me.grbApprove.Controls.Add(Me.txtApprovePerson)
      Me.grbApprove.Controls.Add(Me.txtApprovePersonName)
      Me.grbApprove.Controls.Add(Me.lblApprovePerson)
      Me.grbApprove.Controls.Add(Me.btnFineApprove)
      Me.grbApprove.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbApprove.Location = New System.Drawing.Point(8, 129)
      Me.grbApprove.Name = "grbApprove"
      Me.grbApprove.Size = New System.Drawing.Size(385, 68)
      Me.grbApprove.TabIndex = 1
      Me.grbApprove.TabStop = False
      Me.grbApprove.Text = "���͹��ѵ�"
      '
      'cmbApproveLevel
      '
      Me.cmbApproveLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.cmbApproveLevel.Location = New System.Drawing.Point(97, 38)
      Me.cmbApproveLevel.Name = "cmbApproveLevel"
      Me.cmbApproveLevel.Size = New System.Drawing.Size(232, 21)
      Me.cmbApproveLevel.TabIndex = 1
      '
      'lblApproveLevel
      '
      Me.lblApproveLevel.BackColor = System.Drawing.Color.Transparent
      Me.lblApproveLevel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblApproveLevel.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblApproveLevel.Location = New System.Drawing.Point(6, 38)
      Me.lblApproveLevel.Name = "lblApproveLevel"
      Me.lblApproveLevel.Size = New System.Drawing.Size(91, 18)
      Me.lblApproveLevel.TabIndex = 13
      Me.lblApproveLevel.Text = "�дѺ���͹��ѵ�:"
      Me.lblApproveLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtApprovePerson
      '
      Me.Validator.SetDataType(Me.txtApprovePerson, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtApprovePerson, "")
      Me.Validator.SetGotFocusBackColor(Me.txtApprovePerson, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtApprovePerson, System.Drawing.Color.Empty)
      Me.txtApprovePerson.Location = New System.Drawing.Point(97, 15)
      Me.Validator.SetMinValue(Me.txtApprovePerson, "")
      Me.txtApprovePerson.Name = "txtApprovePerson"
      Me.Validator.SetRegularExpression(Me.txtApprovePerson, "")
      Me.Validator.SetRequired(Me.txtApprovePerson, False)
      Me.txtApprovePerson.Size = New System.Drawing.Size(80, 20)
      Me.txtApprovePerson.TabIndex = 0
      '
      'txtApprovePersonName
      '
      Me.Validator.SetDataType(Me.txtApprovePersonName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtApprovePersonName, "")
      Me.Validator.SetGotFocusBackColor(Me.txtApprovePersonName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtApprovePersonName, System.Drawing.Color.Empty)
      Me.txtApprovePersonName.Location = New System.Drawing.Point(178, 15)
      Me.Validator.SetMinValue(Me.txtApprovePersonName, "")
      Me.txtApprovePersonName.Name = "txtApprovePersonName"
      Me.txtApprovePersonName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtApprovePersonName, "")
      Me.Validator.SetRequired(Me.txtApprovePersonName, False)
      Me.txtApprovePersonName.Size = New System.Drawing.Size(150, 20)
      Me.txtApprovePersonName.TabIndex = 8
      Me.txtApprovePersonName.TabStop = False
      '
      'lblApprovePerson
      '
      Me.lblApprovePerson.BackColor = System.Drawing.Color.Transparent
      Me.lblApprovePerson.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblApprovePerson.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblApprovePerson.Location = New System.Drawing.Point(5, 15)
      Me.lblApprovePerson.Name = "lblApprovePerson"
      Me.lblApprovePerson.Size = New System.Drawing.Size(94, 18)
      Me.lblApprovePerson.TabIndex = 5
      Me.lblApprovePerson.Text = "���͹��ѵ�:"
      Me.lblApprovePerson.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'btnFineApprove
      '
      Me.btnFineApprove.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnFineApprove.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnFineApprove.ForeColor = System.Drawing.SystemColors.Control
      Me.btnFineApprove.Location = New System.Drawing.Point(328, 15)
      Me.btnFineApprove.Name = "btnFineApprove"
      Me.btnFineApprove.Size = New System.Drawing.Size(24, 23)
      Me.btnFineApprove.TabIndex = 2
      Me.btnFineApprove.TabStop = False
      Me.btnFineApprove.ThemedImage = CType(resources.GetObject("btnFineApprove.ThemedImage"), System.Drawing.Bitmap)
      '
      'grbDocDate
      '
      Me.grbDocDate.Controls.Add(Me.txtDocDateStart)
      Me.grbDocDate.Controls.Add(Me.txtDocDateEnd)
      Me.grbDocDate.Controls.Add(Me.lblDocDateStart)
      Me.grbDocDate.Controls.Add(Me.lblDocDateEnd)
      Me.grbDocDate.Controls.Add(Me.dtpDocDateStart)
      Me.grbDocDate.Controls.Add(Me.dtpDocDateEnd)
      Me.grbDocDate.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbDocDate.Location = New System.Drawing.Point(399, 10)
      Me.grbDocDate.Name = "grbDocDate"
      Me.grbDocDate.Size = New System.Drawing.Size(274, 70)
      Me.grbDocDate.TabIndex = 2
      Me.grbDocDate.TabStop = False
      Me.grbDocDate.Text = "�ѹ����͡���"
      '
      'txtDocDateStart
      '
      Me.txtDocDateStart.BackColor = System.Drawing.SystemColors.Window
      Me.Validator.SetDataType(Me.txtDocDateStart, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
      Me.Validator.SetDisplayName(Me.txtDocDateStart, "")
      Me.Validator.SetGotFocusBackColor(Me.txtDocDateStart, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtDocDateStart, System.Drawing.Color.Empty)
      Me.txtDocDateStart.Location = New System.Drawing.Point(101, 17)
      Me.Validator.SetMinValue(Me.txtDocDateStart, "")
      Me.txtDocDateStart.Name = "txtDocDateStart"
      Me.Validator.SetRegularExpression(Me.txtDocDateStart, "")
      Me.Validator.SetRequired(Me.txtDocDateStart, False)
      Me.txtDocDateStart.Size = New System.Drawing.Size(80, 20)
      Me.txtDocDateStart.TabIndex = 0
      '
      'txtDocDateEnd
      '
      Me.txtDocDateEnd.BackColor = System.Drawing.SystemColors.Window
      Me.Validator.SetDataType(Me.txtDocDateEnd, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
      Me.Validator.SetDisplayName(Me.txtDocDateEnd, "")
      Me.Validator.SetGotFocusBackColor(Me.txtDocDateEnd, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtDocDateEnd, System.Drawing.Color.Empty)
      Me.txtDocDateEnd.Location = New System.Drawing.Point(101, 41)
      Me.Validator.SetMinValue(Me.txtDocDateEnd, "")
      Me.txtDocDateEnd.Name = "txtDocDateEnd"
      Me.Validator.SetRegularExpression(Me.txtDocDateEnd, "")
      Me.Validator.SetRequired(Me.txtDocDateEnd, False)
      Me.txtDocDateEnd.Size = New System.Drawing.Size(80, 20)
      Me.txtDocDateEnd.TabIndex = 2
      '
      'lblDocDateStart
      '
      Me.lblDocDateStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblDocDateStart.ForeColor = System.Drawing.Color.Black
      Me.lblDocDateStart.Location = New System.Drawing.Point(13, 18)
      Me.lblDocDateStart.Name = "lblDocDateStart"
      Me.lblDocDateStart.Size = New System.Drawing.Size(88, 18)
      Me.lblDocDateStart.TabIndex = 2
      Me.lblDocDateStart.Text = "�����"
      Me.lblDocDateStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblDocDateEnd
      '
      Me.lblDocDateEnd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblDocDateEnd.ForeColor = System.Drawing.Color.Black
      Me.lblDocDateEnd.Location = New System.Drawing.Point(13, 42)
      Me.lblDocDateEnd.Name = "lblDocDateEnd"
      Me.lblDocDateEnd.Size = New System.Drawing.Size(88, 18)
      Me.lblDocDateEnd.TabIndex = 3
      Me.lblDocDateEnd.Text = "�֧"
      Me.lblDocDateEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'dtpDocDateStart
      '
      Me.dtpDocDateStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
      Me.dtpDocDateStart.Location = New System.Drawing.Point(101, 17)
      Me.dtpDocDateStart.Name = "dtpDocDateStart"
      Me.dtpDocDateStart.Size = New System.Drawing.Size(98, 20)
      Me.dtpDocDateStart.TabIndex = 1
      Me.dtpDocDateStart.TabStop = False
      '
      'dtpDocDateEnd
      '
      Me.dtpDocDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
      Me.dtpDocDateEnd.Location = New System.Drawing.Point(101, 41)
      Me.dtpDocDateEnd.Name = "dtpDocDateEnd"
      Me.dtpDocDateEnd.Size = New System.Drawing.Size(98, 20)
      Me.dtpDocDateEnd.TabIndex = 3
      Me.dtpDocDateEnd.TabStop = False
      '
      'btnSearch
      '
      Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnSearch.Location = New System.Drawing.Point(598, 175)
      Me.btnSearch.Name = "btnSearch"
      Me.btnSearch.Size = New System.Drawing.Size(75, 23)
      Me.btnSearch.TabIndex = 5
      Me.btnSearch.Text = "Search"
      '
      'btnReset
      '
      Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnReset.Location = New System.Drawing.Point(518, 175)
      Me.btnReset.Name = "btnReset"
      Me.btnReset.Size = New System.Drawing.Size(75, 23)
      Me.btnReset.TabIndex = 4
      Me.btnReset.Text = "Reset"
      '
      'grbMainDetail
      '
      Me.grbMainDetail.Controls.Add(Me.btnCostCenterPanel)
      Me.grbMainDetail.Controls.Add(Me.txtCostCenterCode)
      Me.grbMainDetail.Controls.Add(Me.txtCostCenterName)
      Me.grbMainDetail.Controls.Add(Me.btnCostCenterDialog)
      Me.grbMainDetail.Controls.Add(Me.lblCC)
      Me.grbMainDetail.Controls.Add(Me.cmbStatus)
      Me.grbMainDetail.Controls.Add(Me.lblStatus)
      Me.grbMainDetail.Controls.Add(Me.btnSupplierPanel)
      Me.grbMainDetail.Controls.Add(Me.txtSupplierCode)
      Me.grbMainDetail.Controls.Add(Me.txtCode)
      Me.grbMainDetail.Controls.Add(Me.txtSupplierName)
      Me.grbMainDetail.Controls.Add(Me.lblCode)
      Me.grbMainDetail.Controls.Add(Me.btnSupplierDialog)
      Me.grbMainDetail.Controls.Add(Me.lblSupplier)
      Me.grbMainDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbMainDetail.Location = New System.Drawing.Point(8, 9)
      Me.grbMainDetail.Name = "grbMainDetail"
      Me.grbMainDetail.Size = New System.Drawing.Size(385, 120)
      Me.grbMainDetail.TabIndex = 0
      Me.grbMainDetail.TabStop = False
      Me.grbMainDetail.Text = "��������´�����"
      '
      'btnCostCenterPanel
      '
      Me.btnCostCenterPanel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnCostCenterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnCostCenterPanel.Location = New System.Drawing.Point(352, 64)
      Me.btnCostCenterPanel.Name = "btnCostCenterPanel"
      Me.btnCostCenterPanel.Size = New System.Drawing.Size(24, 23)
      Me.btnCostCenterPanel.TabIndex = 7
      Me.btnCostCenterPanel.TabStop = False
      Me.btnCostCenterPanel.ThemedImage = CType(resources.GetObject("btnCostCenterPanel.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtCostCenterCode
      '
      Me.Validator.SetDataType(Me.txtCostCenterCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCostCenterCode, "")
      Me.Validator.SetGotFocusBackColor(Me.txtCostCenterCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCostCenterCode, System.Drawing.Color.Empty)
      Me.txtCostCenterCode.Location = New System.Drawing.Point(97, 64)
      Me.Validator.SetMinValue(Me.txtCostCenterCode, "")
      Me.txtCostCenterCode.Name = "txtCostCenterCode"
      Me.Validator.SetRegularExpression(Me.txtCostCenterCode, "")
      Me.Validator.SetRequired(Me.txtCostCenterCode, False)
      Me.txtCostCenterCode.Size = New System.Drawing.Size(80, 20)
      Me.txtCostCenterCode.TabIndex = 2
      '
      'txtCostCenterName
      '
      Me.Validator.SetDataType(Me.txtCostCenterName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCostCenterName, "")
      Me.Validator.SetGotFocusBackColor(Me.txtCostCenterName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCostCenterName, System.Drawing.Color.Empty)
      Me.txtCostCenterName.Location = New System.Drawing.Point(177, 64)
      Me.Validator.SetMinValue(Me.txtCostCenterName, "")
      Me.txtCostCenterName.Name = "txtCostCenterName"
      Me.txtCostCenterName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtCostCenterName, "")
      Me.Validator.SetRequired(Me.txtCostCenterName, False)
      Me.txtCostCenterName.Size = New System.Drawing.Size(152, 20)
      Me.txtCostCenterName.TabIndex = 16
      Me.txtCostCenterName.TabStop = False
      '
      'btnCostCenterDialog
      '
      Me.btnCostCenterDialog.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnCostCenterDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnCostCenterDialog.ForeColor = System.Drawing.SystemColors.Control
      Me.btnCostCenterDialog.Location = New System.Drawing.Point(328, 64)
      Me.btnCostCenterDialog.Name = "btnCostCenterDialog"
      Me.btnCostCenterDialog.Size = New System.Drawing.Size(24, 23)
      Me.btnCostCenterDialog.TabIndex = 5
      Me.btnCostCenterDialog.TabStop = False
      Me.btnCostCenterDialog.ThemedImage = CType(resources.GetObject("btnCostCenterDialog.ThemedImage"), System.Drawing.Bitmap)
      '
      'lblCC
      '
      Me.lblCC.BackColor = System.Drawing.Color.Transparent
      Me.lblCC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCC.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblCC.Location = New System.Drawing.Point(9, 64)
      Me.lblCC.Name = "lblCC"
      Me.lblCC.Size = New System.Drawing.Size(88, 18)
      Me.lblCC.TabIndex = 15
      Me.lblCC.Text = "CostCenter:"
      Me.lblCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'cmbStatus
      '
      Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.cmbStatus.Location = New System.Drawing.Point(97, 88)
      Me.cmbStatus.Name = "cmbStatus"
      Me.cmbStatus.Size = New System.Drawing.Size(232, 21)
      Me.cmbStatus.TabIndex = 3
      '
      'lblStatus
      '
      Me.lblStatus.BackColor = System.Drawing.Color.Transparent
      Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblStatus.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblStatus.Location = New System.Drawing.Point(9, 88)
      Me.lblStatus.Name = "lblStatus"
      Me.lblStatus.Size = New System.Drawing.Size(88, 18)
      Me.lblStatus.TabIndex = 5
      Me.lblStatus.Text = "ʶҹ�:"
      Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'btnSupplierPanel
      '
      Me.btnSupplierPanel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnSupplierPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnSupplierPanel.Location = New System.Drawing.Point(352, 40)
      Me.btnSupplierPanel.Name = "btnSupplierPanel"
      Me.btnSupplierPanel.Size = New System.Drawing.Size(24, 23)
      Me.btnSupplierPanel.TabIndex = 6
      Me.btnSupplierPanel.TabStop = False
      Me.btnSupplierPanel.ThemedImage = CType(resources.GetObject("btnSupplierPanel.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtSupplierCode
      '
      Me.Validator.SetDataType(Me.txtSupplierCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtSupplierCode, "")
      Me.Validator.SetGotFocusBackColor(Me.txtSupplierCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtSupplierCode, System.Drawing.Color.Empty)
      Me.txtSupplierCode.Location = New System.Drawing.Point(97, 40)
      Me.Validator.SetMinValue(Me.txtSupplierCode, "")
      Me.txtSupplierCode.Name = "txtSupplierCode"
      Me.Validator.SetRegularExpression(Me.txtSupplierCode, "")
      Me.Validator.SetRequired(Me.txtSupplierCode, False)
      Me.txtSupplierCode.Size = New System.Drawing.Size(80, 20)
      Me.txtSupplierCode.TabIndex = 1
      '
      'txtSupplierName
      '
      Me.Validator.SetDataType(Me.txtSupplierName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtSupplierName, "")
      Me.Validator.SetGotFocusBackColor(Me.txtSupplierName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtSupplierName, System.Drawing.Color.Empty)
      Me.txtSupplierName.Location = New System.Drawing.Point(177, 40)
      Me.Validator.SetMinValue(Me.txtSupplierName, "")
      Me.txtSupplierName.Name = "txtSupplierName"
      Me.txtSupplierName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtSupplierName, "")
      Me.Validator.SetRequired(Me.txtSupplierName, False)
      Me.txtSupplierName.Size = New System.Drawing.Size(152, 20)
      Me.txtSupplierName.TabIndex = 6
      Me.txtSupplierName.TabStop = False
      '
      'btnSupplierDialog
      '
      Me.btnSupplierDialog.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnSupplierDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnSupplierDialog.ForeColor = System.Drawing.SystemColors.Control
      Me.btnSupplierDialog.Location = New System.Drawing.Point(328, 40)
      Me.btnSupplierDialog.Name = "btnSupplierDialog"
      Me.btnSupplierDialog.Size = New System.Drawing.Size(24, 23)
      Me.btnSupplierDialog.TabIndex = 4
      Me.btnSupplierDialog.TabStop = False
      Me.btnSupplierDialog.ThemedImage = CType(resources.GetObject("btnSupplierDialog.ThemedImage"), System.Drawing.Bitmap)
      '
      'lblSupplier
      '
      Me.lblSupplier.BackColor = System.Drawing.Color.Transparent
      Me.lblSupplier.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblSupplier.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblSupplier.Location = New System.Drawing.Point(9, 40)
      Me.lblSupplier.Name = "lblSupplier"
      Me.lblSupplier.Size = New System.Drawing.Size(88, 18)
      Me.lblSupplier.TabIndex = 4
      Me.lblSupplier.Text = "Supplier:"
      Me.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'ErrorProvider1
      '
      Me.ErrorProvider1.ContainerControl = Me
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
      'grbSC
      '
      Me.grbSC.Controls.Add(Me.txtSCCode)
      Me.grbSC.Controls.Add(Me.lblSC)
      Me.grbSC.Controls.Add(Me.btnSCDialog)
      Me.grbSC.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbSC.Location = New System.Drawing.Point(399, 81)
      Me.grbSC.Name = "grbSC"
      Me.grbSC.Size = New System.Drawing.Size(274, 48)
      Me.grbSC.TabIndex = 3
      Me.grbSC.TabStop = False
      Me.grbSC.Text = "���觨�ҧ"
      '
      'txtSCCode
      '
      Me.Validator.SetDataType(Me.txtSCCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtSCCode, "")
      Me.Validator.SetGotFocusBackColor(Me.txtSCCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtSCCode, System.Drawing.Color.Empty)
      Me.txtSCCode.Location = New System.Drawing.Point(101, 16)
      Me.Validator.SetMinValue(Me.txtSCCode, "")
      Me.txtSCCode.Name = "txtSCCode"
      Me.Validator.SetRegularExpression(Me.txtSCCode, "")
      Me.Validator.SetRequired(Me.txtSCCode, False)
      Me.txtSCCode.Size = New System.Drawing.Size(139, 20)
      Me.txtSCCode.TabIndex = 0
      '
      'lblSC
      '
      Me.lblSC.BackColor = System.Drawing.Color.Transparent
      Me.lblSC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblSC.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblSC.Location = New System.Drawing.Point(9, 16)
      Me.lblSC.Name = "lblSC"
      Me.lblSC.Size = New System.Drawing.Size(94, 18)
      Me.lblSC.TabIndex = 12
      Me.lblSC.Text = "�͡��èѴ��ҧ:"
      Me.lblSC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'btnSCDialog
      '
      Me.btnSCDialog.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnSCDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnSCDialog.ForeColor = System.Drawing.SystemColors.Control
      Me.btnSCDialog.Location = New System.Drawing.Point(240, 16)
      Me.btnSCDialog.Name = "btnSCDialog"
      Me.btnSCDialog.Size = New System.Drawing.Size(24, 23)
      Me.btnSCDialog.TabIndex = 1
      Me.btnSCDialog.TabStop = False
      Me.btnSCDialog.ThemedImage = CType(resources.GetObject("btnSCDialog.ThemedImage"), System.Drawing.Bitmap)
      '
      'PAFilterSubPanel
      '
      Me.Controls.Add(Me.grbDetail)
      Me.Name = "PAFilterSubPanel"
      Me.Size = New System.Drawing.Size(699, 218)
      Me.grbDetail.ResumeLayout(False)
      Me.grbApprove.ResumeLayout(False)
      Me.grbApprove.PerformLayout()
      Me.grbDocDate.ResumeLayout(False)
      Me.grbDocDate.PerformLayout()
      Me.grbMainDetail.ResumeLayout(False)
      Me.grbMainDetail.PerformLayout()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.grbSC.ResumeLayout(False)
      Me.grbSC.PerformLayout()
      Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Constructors"
    Public Sub New()
      MyBase.New()

      InitializeComponent()
      Initialize()
      SetLabelText()
      Me.LoopControl(Me)
    End Sub
#End Region

#Region "Members"
    Private m_supplier As Supplier
    Private m_lci As LCIItem
    Private m_tool As Tool
    Private dummyCC As New CostCenter
    Private m_cc As CostCenter
    Private docDateStart As Date
    Private docDateEnd As Date
    Private receivingDateStart As Date
    Private receivingDateEnd As Date
    Private m_user As New User
    Private m_sc As SC
#End Region

#Region "Methods"
    Public Sub Initialize()
      AddHandler txtDocDateStart.Validated, AddressOf Me.ChangeProperty
      AddHandler dtpDocDateStart.ValueChanged, AddressOf Me.ChangeProperty
      AddHandler txtDocDateEnd.Validated, AddressOf Me.ChangeProperty
      AddHandler dtpDocDateEnd.ValueChanged, AddressOf Me.ChangeProperty
      AddHandler txtSCCode.Validated, AddressOf Me.ChangeProperty

      'AddHandler txtReceivingDateStart.Validated, AddressOf Me.ChangeProperty
      'AddHandler dtpReceivingDateStart.ValueChanged, AddressOf Me.ChangeProperty
      'AddHandler txtReceivingdateEnd.Validated, AddressOf Me.ChangeProperty
      'AddHandler dtpReceivingDateEnd.ValueChanged, AddressOf Me.ChangeProperty

      PopulateStatus()
      ClearCriterias()
    End Sub
    Private m_dateSetting As Boolean
    Public Sub ChangeProperty(ByVal sender As Object, ByVal e As EventArgs)
      Dim dirtyFlag As Boolean = False
      Select Case CType(sender, Control).Name.ToLower
        Case txtSCCode.Name.ToLower
          dirtyFlag = SC.GetSC(txtSCCode, Me.m_sc, True)
          If dirtyFlag AndAlso txtSCCode.Text.Trim.Length = 0 Then
            Me.m_sc = New SC
          End If
        Case "dtpdocdatestart"
          If Not Me.docDateStart.Equals(dtpDocDateStart.Value) Then
            If Not m_dateSetting Then
              Me.txtDocDateStart.Text = MinDateToNull(dtpDocDateStart.Value, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
              Me.docDateStart = dtpDocDateStart.Value
            End If
            dirtyFlag = True
          End If
        Case "txtdocdatestart"
          m_dateSetting = True
          If Not Me.txtDocDateStart.Text.Length = 0 AndAlso Me.Validator.GetErrorMessage(Me.txtDocDateStart) = "" Then
            Dim theDate As Date = CDate(Me.txtDocDateStart.Text)
            If Not Me.docDateStart.Equals(theDate) Then
              dtpDocDateStart.Value = theDate
              Me.docDateStart = dtpDocDateStart.Value
              dirtyFlag = True
            End If
          Else
            Me.dtpDocDateStart.Value = Date.Now
            Me.docDateStart = Date.MinValue
            dirtyFlag = True
          End If
          m_dateSetting = False
        Case "dtpdocdateend"
          If Not Me.docDateEnd.Equals(dtpDocDateEnd.Value) Then
            If Not m_dateSetting Then
              Me.txtDocDateEnd.Text = MinDateToNull(dtpDocDateEnd.Value, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
              Me.docDateEnd = dtpDocDateEnd.Value
            End If
            dirtyFlag = True
          End If
        Case "txtdocdateend"
          m_dateSetting = True
          If Not Me.txtDocDateEnd.Text.Length = 0 AndAlso Me.Validator.GetErrorMessage(Me.txtDocDateEnd) = "" Then
            Dim theDate As Date = CDate(Me.txtDocDateEnd.Text)
            If Not Me.docDateEnd.Equals(theDate) Then
              dtpDocDateEnd.Value = theDate
              Me.docDateEnd = dtpDocDateEnd.Value
              dirtyFlag = True
            End If
          Else
            Me.dtpDocDateEnd.Value = Date.Now
            Me.docDateEnd = Date.MinValue
            dirtyFlag = True
          End If
          m_dateSetting = False
        Case Else
      End Select
    End Sub
    Private Sub ClearCriterias()
      Me.txtCode.Text = ""

      Me.txtCostCenterCode.Text = ""
      Me.txtCostCenterName.Text = ""
      Me.m_cc = New CostCenter

      Me.txtSupplierCode.Text = ""
      Me.txtSupplierName.Text = ""

      Me.txtCostCenterCode.Text = ""
      Me.txtCostCenterName.Text = ""
      Me.txtSCCode.Text = ""
      Me.m_cc = New CostCenter
      Me.m_sc = New SC

      Me.m_supplier = New Supplier


      'Me.txtLCI.Text = ""
      'Me.txtLCIName.Text = ""
      'Me.m_lci = New LCIItem

      'Me.txtTool.Text = ""
      'Me.txtToolName.Text = ""
      'Me.m_tool = New Tool

      'Me.txtBlank.Text = ""

      Dim poDocDateStartBeforeToday As Long = Configuration.GetConfig("PODocDateStartBeforeToday")
      Dim poDocDateEndAfterToday As Long = Configuration.GetConfig("PODocDateEndAfterToday")
      Dim poReceiveDateStartBeforeToday As Long = Configuration.GetConfig("POReceiveDateStartBeforeToday")
      Dim poReceiveDateEndAfterToday As Long = Configuration.GetConfig("POReceiveDateEndAfterToday")

      Me.dtpDocDateStart.Value = DateAdd(DateInterval.Day, poDocDateEndAfterToday, Now.Subtract(New TimeSpan(7, 0, 0, 0)))
      Me.dtpDocDateEnd.Value = DateAdd(DateInterval.Day, poReceiveDateEndAfterToday, Now.Date)

      Me.txtDocDateStart.Text = Me.MinDateToNull(DateAdd(DateInterval.Day, poDocDateStartBeforeToday, Now.Subtract(New TimeSpan(7, 0, 0, 0))), "")
      Me.txtDocDateEnd.Text = Me.MinDateToNull(DateAdd(DateInterval.Day, poDocDateEndAfterToday, Now.Date), "")

      Me.docDateStart = DateAdd(DateInterval.Day, poDocDateStartBeforeToday, Now.Subtract(New TimeSpan(7, 0, 0, 0)))
      Me.docDateEnd = DateAdd(DateInterval.Day, poDocDateEndAfterToday, Now.Date)

      'Me.dtpReceivingDateStart.Value = DateAdd(DateInterval.Day, poReceiveDateStartBeforeToday, Now.Date)
      'Me.dtpReceivingDateEnd.Value = DateAdd(DateInterval.Day, poReceiveDateEndAfterToday, Now.Date)

      'Me.txtReceivingDateStart.Text = Me.MinDateToNull(DateAdd(DateInterval.Day, poReceiveDateStartBeforeToday, Now.Date), "")
      'Me.txtReceivingdateEnd.Text = Me.MinDateToNull(DateAdd(DateInterval.Day, poReceiveDateEndAfterToday, Now.Date), "")

      'Me.receivingDateStart = DateAdd(DateInterval.Day, poReceiveDateStartBeforeToday, Now.Date)
      'Me.receivingDateEnd = DateAdd(DateInterval.Day, poReceiveDateEndAfterToday, Now.Date)

      cmbStatus.SelectedIndex = 0
      Me.cmbApproveLevel.SelectedIndex = 0

      Me.txtApprovePerson.Text = ""
      Me.txtApprovePersonName.Text = ""
      Me.m_user = New User
    End Sub
    Private Sub PopulateStatus()
      Dim myService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
      'Dim lvString As String = Me.StringParserService.Parse("${res:Global.Level}")
      Dim waitLVSApprove As String = Me.StringParserService.Parse("${res:Global.WaitForOtherLevelApprove}")
      Dim notAppear As String = Me.StringParserService.Parse("${res:Global.Unspecified}")
      Dim maxGRApproveLevel As Integer = CType(Configuration.GetConfig("MaxLevelApprovePA"), Integer)

      Dim dt1 As DataTable

      CodeDescription.ListCodeDescriptionInComboBox(cmbStatus, "po_status", True)
      dt1 = CodeDescription.GetCodeList("reference_status")
      For Each row As DataRow In dt1.Rows
        Dim item As New IdValuePair(CInt(row("code_value")), myService.Parse(CStr(row("code_description"))))
        cmbStatus.Items.Add(item)
      Next

      dt1 = CodeDescription.GetCodeList("approve_status")
      Dim itemApprove1 As IdValuePair = Nothing
      Dim itemApprove2 As IdValuePair = Nothing
      Dim itemApprove3 As IdValuePair = Nothing

      For Each row As DataRow In dt1.Rows
        If Not row.IsNull("code_value") Then
          If CInt(row("code_value")) = 201 Then
            itemApprove1 = New IdValuePair(CInt(row("code_value")), myService.Parse(CStr(row("code_description"))))
          End If
          'If CInt(row("code_value")) = "202" Then
          '  itemApprove2 = New IdValuePair(CInt(row("code_value")), myService.Parse(CStr(row("code_description"))))
          'End If
          If CInt(row("code_value")) = 203 Then
            itemApprove3 = New IdValuePair(CInt(row("code_value")), myService.Parse(CStr(row("code_description"))))
          End If
        End If
      Next

      cmbApproveLevel.Items.Clear()
      cmbApproveLevel.Items.Insert(0, New IdValuePair(-1, notAppear))
      For i As Integer = 1 To maxGRApproveLevel 'User.MaxLevel
        Dim item As New IdValuePair(i - 1, String.Format(waitLVSApprove, i))
        cmbApproveLevel.Items.Add(item)
      Next
      If Not itemApprove1 Is Nothing Then
        cmbApproveLevel.Items.Insert(maxGRApproveLevel + 1, itemApprove1)
      End If
      If Not itemApprove3 Is Nothing Then
        cmbApproveLevel.Items.Insert(maxGRApproveLevel + 2, itemApprove3)
      End If
    End Sub
    Public Sub SetLabelText()
      Me.grbDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.grbDetail}")
      Me.lblCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblCode}")
      Me.btnSearch.Text = Me.StringParserService.Parse("${res:Global.SearchButtonText}")
      Me.btnReset.Text = Me.StringParserService.Parse("${res:Global.ResetButtonText}")
      Me.grbDocDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.grbDocDate}")
      Me.lblDocDateStart.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblDocDateStart}")
      Me.lblDocDateEnd.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblDocDateEnd}")
      Me.lblSupplier.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblSupplier}")
      Me.lblCC.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblCC}")
      Me.lblStatus.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblStatus}")
      Me.grbMainDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.grbMainDetail}")

      Me.grbApprove.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.grbApprove}")
      Me.lblApprovePerson.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblApprovePerson}")
      Me.lblApproveLevel.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblApproveLevel}")
      Me.grbSC.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.grbSC}")
      Me.lblSC.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PAFilterSubPanel.lblSC}")
    End Sub
    Public Overrides Function GetFilterArray() As Filter()
      Dim arr(9) As Filter
      arr(0) = New Filter("code", IIf(Me.txtCode.Text.Length = 0, DBNull.Value, Me.txtCode.Text))
      arr(1) = New Filter("supplier_id", IIf(Me.m_supplier.Valid, Me.m_supplier.Id, DBNull.Value))
      arr(2) = New Filter("docdatestart", ValidDateOrDBNull(docDateStart))
      arr(3) = New Filter("docdateend", ValidDateOrDBNull(docDateEnd))
      arr(4) = New Filter("costcenter", IIf(Me.m_cc.Valid, Me.m_cc.Id, DBNull.Value))
      arr(5) = New Filter("status", IIf(cmbStatus.SelectedItem Is Nothing, DBNull.Value, CType(cmbStatus.SelectedItem, IdValuePair).Id))
      arr(6) = New Filter("userRight", CType(ServiceManager.Services.GetService(GetType(SecurityService)), SecurityService).CurrentUser.Id)
      arr(7) = New Filter("ApprovePerson", ValidIdOrDBNull(m_user))
      arr(8) = New Filter("ApproveLevel", IIf(cmbApproveLevel.SelectedItem Is Nothing, DBNull.Value, CType(cmbApproveLevel.SelectedItem, IdValuePair).Id))
      arr(9) = New Filter("sc_id", Me.ValidIdOrDBNull(Me.m_sc))
      Return arr
    End Function
    Public Overrides ReadOnly Property SearchButton() As System.Windows.Forms.Button
      Get
        Return Me.btnSearch
      End Get
    End Property
#End Region

#Region "Event Handlers"
    Private Sub txtApprovePerson_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtApprovePerson.Validated
      User.GetUser(txtApprovePerson, txtApprovePersonName, Me.m_user)
    End Sub
    Private Sub txtSupplierCode_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplierCode.Validated
      Supplier.GetSupplier(txtSupplierCode, txtSupplierName, Me.m_supplier)
    End Sub
    Private Sub txtCostCenterCode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostCenterCode.Validated
      CostCenter.GetCostCenter(txtCostCenterCode, txtCostCenterName, Me.m_cc, CType(ServiceManager.Services.GetService(GetType(SecurityService)), SecurityService).CurrentUser.Id)
    End Sub
    Private Sub ibtnShowLCI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New LCIItem)
    End Sub
    Private Sub ibtnShowTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New Tool)
    End Sub
    Private Sub btnSCDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSCDialog.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New SC, AddressOf SetSC)
    End Sub
    Private Sub SetSC(ByVal e As ISimpleEntity)
      Me.txtSCCode.Text = e.Code
      SC.GetSC(txtSCCode, Me.m_sc, True)
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
      ClearCriterias()
      Me.btnSearch.PerformClick()
    End Sub
    Private Sub btnSupplierDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierDialog.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New Supplier, AddressOf SetSupplier)
    End Sub
    Private Sub btnCostCenterDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCostCenterDialog.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenTreeDialog(New CostCenter, AddressOf SetCostCenter)
    End Sub
    Private Sub SetCostCenter(ByVal e As ISimpleEntity)
      Me.txtCostCenterCode.Text = e.Code
      CostCenter.GetCostCenter(txtCostCenterCode, txtCostCenterName, Me.m_cc, CType(ServiceManager.Services.GetService(GetType(SecurityService)), SecurityService).CurrentUser.Id)
    End Sub
    Private Sub btnCostCenterPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCostCenterPanel.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New CostCenter)
    End Sub
    Private Sub SetSupplier(ByVal e As ISimpleEntity)
      Me.txtSupplierCode.Text = e.Code
      Supplier.GetSupplier(txtSupplierCode, txtSupplierName, Me.m_supplier)
    End Sub
    Private Sub btnSupplierPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierPanel.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New Supplier)
    End Sub
    Private Sub SetUser(ByVal e As ISimpleEntity)
      Me.txtApprovePerson.Text = e.Code
      User.GetUser(txtApprovePerson, txtApprovePersonName, Me.m_user)
    End Sub
    Private Sub btnFineApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFineApprove.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New User, AddressOf SetUser)
    End Sub
#End Region

#Region "IClipboardHandler Overrides"   'Undone
    Public Overrides ReadOnly Property EnablePaste() As Boolean
      Get
        If Me.ActiveControl Is Nothing Then
          Return False
        End If
        Dim data As IDataObject = Clipboard.GetDataObject

        If data.GetDataPresent((dummyCC).FullClassName) Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtcostcentercode", "txtcostcentername"
              Return True
          End Select
        End If

        If data.GetDataPresent((New Supplier).FullClassName) Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtsuppliercode", "txtsuppliername"
              Return True
          End Select
        End If
        If data.GetDataPresent((New LCIItem).FullClassName) Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtlci", "txtlciname"
              Return True
          End Select
        End If
        If data.GetDataPresent((New Tool).FullClassName) Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txttool", "txttoolname"
              Return True
          End Select
        End If
      End Get
    End Property
    Public Overrides Sub Paste(ByVal sender As Object, ByVal e As System.EventArgs)
      If Me.ActiveControl Is Nothing Then
        Return
      End If
      Dim data As IDataObject = Clipboard.GetDataObject
      If data.GetDataPresent((dummyCC).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New CostCenter).FullClassName))
        Dim entity As New CostCenter(id)
        Select Case Me.ActiveControl.Name.ToLower
          Case "txtcostcentercode", "txtcostcentername"
            Me.SetCostCenter(entity)
        End Select
      End If
      If data.GetDataPresent((New Supplier).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New Supplier).FullClassName))
        Dim entity As New Supplier(id)
        Select Case Me.ActiveControl.Name.ToLower
          Case "txtsuppliercode", "txtsuppliername"
            Me.SetSupplier(entity)
        End Select
      End If
      If data.GetDataPresent((New LCIItem).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New LCIItem).FullClassName))
        Dim entity As New LCIItem(id)
        Select Case Me.ActiveControl.Name.ToLower
          Case "txtlci", "txtlciname"
            'Me.SetLCi(entity)
        End Select
      End If
      If data.GetDataPresent((New Tool).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New Tool).FullClassName))
        Dim entity As New Tool(id)
        Select Case Me.ActiveControl.Name.ToLower
          Case "txttool", "txttoolname"
            'Me.SetTool(entity)
        End Select
      End If
    End Sub
#End Region

#Region "Properties"
    Public Overrides Property Entities() As System.Collections.ArrayList
      Get
        Return MyBase.Entities
      End Get
      Set(ByVal Value As System.Collections.ArrayList)
        MyBase.Entities = Value
        For Each entity As ISimpleEntity In Value
          If TypeOf entity Is Supplier Then
            Me.SetSupplier(entity)
            Me.txtSupplierCode.Enabled = False
            Me.txtSupplierName.Enabled = False
            Me.btnSupplierDialog.Enabled = False
            Me.btnSupplierPanel.Enabled = False
          End If
          If TypeOf entity Is PO Then
            If entity.Status.Value <> -1 Then
              CodeDescription.ComboSelect(Me.cmbStatus, entity.Status)
              Me.cmbStatus.Enabled = False
            End If
          End If
          If TypeOf entity Is CostCenter Then
            Me.SetCostCenter(CType(entity, CostCenter))
            Me.txtCostCenterCode.Enabled = False
            Me.txtCostCenterName.Enabled = False
            Me.btnCostCenterDialog.Enabled = False
            Me.btnCostCenterPanel.Enabled = False
          End If
        Next
      End Set
    End Property
#End Region

  End Class
End Namespace

