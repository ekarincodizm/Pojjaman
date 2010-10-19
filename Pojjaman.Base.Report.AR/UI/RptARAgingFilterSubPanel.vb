Imports Longkong.Pojjaman.BusinessLogic
Imports longkong.Pojjaman.Services
Imports Longkong.Core.Services

Namespace Longkong.Pojjaman.Gui.Panels
  Public Class RptARAgingFilterSubPanel
    Inherits AbstractFilterSubPanel
    Implements IReportFilterSubPanel

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
    Friend WithEvents grbMaster As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents lblDocDateStart As System.Windows.Forms.Label
    Friend WithEvents Validator As Longkong.Pojjaman.Gui.Components.PJMTextboxValidator
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtDocDateStart As System.Windows.Forms.TextBox
    Friend WithEvents dtpDocDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents grbDetail As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents txtTemp As System.Windows.Forms.TextBox
    Friend WithEvents btnCustEndFind As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtCustCodeEnd As System.Windows.Forms.TextBox
    Friend WithEvents lblCustEnd As System.Windows.Forms.Label
    Friend WithEvents btnCustStartFind As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtCustCodeStart As System.Windows.Forms.TextBox
    Friend WithEvents lblCustStart As System.Windows.Forms.Label
    Friend WithEvents chkIncludeChildren As System.Windows.Forms.CheckBox
    Friend WithEvents btnCCCodeStart As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtCCCodeStart As System.Windows.Forms.TextBox
    Friend WithEvents lblCCStart As System.Windows.Forms.Label
    Friend WithEvents txtCostCenterName As System.Windows.Forms.TextBox
    Friend WithEvents grbDisplay As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents cmbShowPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents lblTerm As System.Windows.Forms.Label
    Friend WithEvents chkDetail As System.Windows.Forms.CheckBox
    Friend WithEvents cmbDueDateType As System.Windows.Forms.ComboBox
    Friend WithEvents lblDueDate As System.Windows.Forms.Label
        Friend WithEvents rdbBillissue As System.Windows.Forms.RadioButton
        Friend WithEvents KeepKeyCombo1 As Longkong.Pojjaman.Gui.Components.KeepKeyCombo
		Friend WithEvents rdbPaySelection As System.Windows.Forms.RadioButton
		<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptARAgingFilterSubPanel))
            Me.grbMaster = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
            Me.grbDisplay = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
            Me.rdbBillissue = New System.Windows.Forms.RadioButton()
            Me.rdbPaySelection = New System.Windows.Forms.RadioButton()
            Me.chkDetail = New System.Windows.Forms.CheckBox()
            Me.cmbDueDateType = New System.Windows.Forms.ComboBox()
            Me.lblDueDate = New System.Windows.Forms.Label()
            Me.cmbShowPeriod = New System.Windows.Forms.ComboBox()
            Me.lblTerm = New System.Windows.Forms.Label()
            Me.txtTemp = New System.Windows.Forms.TextBox()
            Me.grbDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
            Me.chkIncludeChildren = New System.Windows.Forms.CheckBox()
            Me.btnCCCodeStart = New Longkong.Pojjaman.Gui.Components.ImageButton()
            Me.txtCCCodeStart = New System.Windows.Forms.TextBox()
            Me.lblCCStart = New System.Windows.Forms.Label()
            Me.txtCostCenterName = New System.Windows.Forms.TextBox()
            Me.btnCustEndFind = New Longkong.Pojjaman.Gui.Components.ImageButton()
            Me.txtCustCodeEnd = New System.Windows.Forms.TextBox()
            Me.lblCustEnd = New System.Windows.Forms.Label()
            Me.btnCustStartFind = New Longkong.Pojjaman.Gui.Components.ImageButton()
            Me.txtCustCodeStart = New System.Windows.Forms.TextBox()
            Me.lblCustStart = New System.Windows.Forms.Label()
            Me.txtDocDateStart = New System.Windows.Forms.TextBox()
            Me.dtpDocDateStart = New System.Windows.Forms.DateTimePicker()
            Me.lblDocDateStart = New System.Windows.Forms.Label()
            Me.btnSearch = New System.Windows.Forms.Button()
            Me.btnReset = New System.Windows.Forms.Button()
            Me.Validator = New Longkong.Pojjaman.Gui.Components.PJMTextboxValidator(Me.components)
            Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
            Me.KeepKeyCombo1 = New Longkong.Pojjaman.Gui.Components.KeepKeyCombo()
            Me.grbMaster.SuspendLayout()
            Me.grbDisplay.SuspendLayout()
            Me.grbDetail.SuspendLayout()
            CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'grbMaster
            '
            Me.grbMaster.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grbMaster.Controls.Add(Me.grbDisplay)
            Me.grbMaster.Controls.Add(Me.txtTemp)
            Me.grbMaster.Controls.Add(Me.grbDetail)
            Me.grbMaster.Controls.Add(Me.btnSearch)
            Me.grbMaster.Controls.Add(Me.btnReset)
            Me.grbMaster.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.grbMaster.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.grbMaster.Location = New System.Drawing.Point(8, 8)
            Me.grbMaster.Name = "grbMaster"
            Me.grbMaster.Size = New System.Drawing.Size(696, 200)
            Me.grbMaster.TabIndex = 0
            Me.grbMaster.TabStop = False
            Me.grbMaster.Text = "���Ѻ"
            '
            'grbDisplay
            '
            Me.grbDisplay.Controls.Add(Me.KeepKeyCombo1)
            Me.grbDisplay.Controls.Add(Me.rdbBillissue)
            Me.grbDisplay.Controls.Add(Me.rdbPaySelection)
            Me.grbDisplay.Controls.Add(Me.chkDetail)
            Me.grbDisplay.Controls.Add(Me.cmbDueDateType)
            Me.grbDisplay.Controls.Add(Me.lblDueDate)
            Me.grbDisplay.Controls.Add(Me.cmbShowPeriod)
            Me.grbDisplay.Controls.Add(Me.lblTerm)
            Me.grbDisplay.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.grbDisplay.Location = New System.Drawing.Point(416, 16)
            Me.grbDisplay.Name = "grbDisplay"
            Me.grbDisplay.Size = New System.Drawing.Size(272, 144)
            Me.grbDisplay.TabIndex = 4
            Me.grbDisplay.TabStop = False
            Me.grbDisplay.Text = "�ٻẺ����ʴ���"
            '
            'rdbBillissue
            '
            Me.rdbBillissue.Checked = True
            Me.rdbBillissue.Location = New System.Drawing.Point(88, 88)
            Me.rdbBillissue.Name = "rdbBillissue"
            Me.rdbBillissue.Size = New System.Drawing.Size(168, 24)
            Me.rdbBillissue.TabIndex = 59
            Me.rdbBillissue.TabStop = True
            Me.rdbBillissue.Text = "�ʴ� Retention �͹�ҧ���"
            Me.rdbBillissue.Visible = False
            '
            'rdbPaySelection
            '
            Me.rdbPaySelection.Location = New System.Drawing.Point(88, 112)
            Me.rdbPaySelection.Name = "rdbPaySelection"
            Me.rdbPaySelection.Size = New System.Drawing.Size(168, 24)
            Me.rdbPaySelection.TabIndex = 58
            Me.rdbPaySelection.Text = "�ʴ� Retention �͹�Ѻ����"
            Me.rdbPaySelection.Visible = False
            '
            'chkDetail
            '
            Me.chkDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.chkDetail.Location = New System.Drawing.Point(128, 64)
            Me.chkDetail.Name = "chkDetail"
            Me.chkDetail.Size = New System.Drawing.Size(128, 24)
            Me.chkDetail.TabIndex = 37
            Me.chkDetail.Text = "�ʴ���������´"
            '
            'cmbDueDateType
            '
            Me.cmbDueDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbDueDateType.Location = New System.Drawing.Point(128, 40)
            Me.cmbDueDateType.Name = "cmbDueDateType"
            Me.cmbDueDateType.Size = New System.Drawing.Size(136, 21)
            Me.cmbDueDateType.TabIndex = 36
            '
            'lblDueDate
            '
            Me.lblDueDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblDueDate.ForeColor = System.Drawing.Color.Black
            Me.lblDueDate.Location = New System.Drawing.Point(16, 40)
            Me.lblDueDate.Name = "lblDueDate"
            Me.lblDueDate.Size = New System.Drawing.Size(104, 18)
            Me.lblDueDate.TabIndex = 35
            Me.lblDueDate.Text = "�ѹ����˹����е��"
            Me.lblDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'cmbShowPeriod
            '
            Me.cmbShowPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbShowPeriod.Location = New System.Drawing.Point(128, 16)
            Me.cmbShowPeriod.Name = "cmbShowPeriod"
            Me.cmbShowPeriod.Size = New System.Drawing.Size(136, 21)
            Me.cmbShowPeriod.TabIndex = 1
            '
            'lblTerm
            '
            Me.lblTerm.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblTerm.ForeColor = System.Drawing.Color.Black
            Me.lblTerm.Location = New System.Drawing.Point(40, 16)
            Me.lblTerm.Name = "lblTerm"
            Me.lblTerm.Size = New System.Drawing.Size(80, 18)
            Me.lblTerm.TabIndex = 0
            Me.lblTerm.Text = "��ǧ����ʴ�"
            Me.lblTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtTemp
            '
            Me.Validator.SetDataType(Me.txtTemp, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtTemp, "")
            Me.Validator.SetGotFocusBackColor(Me.txtTemp, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtTemp, System.Drawing.Color.Empty)
            Me.txtTemp.Location = New System.Drawing.Point(728, 32)
            Me.txtTemp.MaxLength = 255
            Me.Validator.SetMinValue(Me.txtTemp, "")
            Me.txtTemp.Name = "txtTemp"
            Me.txtTemp.ReadOnly = True
            Me.Validator.SetRegularExpression(Me.txtTemp, "")
            Me.Validator.SetRequired(Me.txtTemp, False)
            Me.txtTemp.Size = New System.Drawing.Size(72, 21)
            Me.txtTemp.TabIndex = 3
            Me.txtTemp.Visible = False
            '
            'grbDetail
            '
            Me.grbDetail.Controls.Add(Me.chkIncludeChildren)
            Me.grbDetail.Controls.Add(Me.btnCCCodeStart)
            Me.grbDetail.Controls.Add(Me.txtCCCodeStart)
            Me.grbDetail.Controls.Add(Me.lblCCStart)
            Me.grbDetail.Controls.Add(Me.txtCostCenterName)
            Me.grbDetail.Controls.Add(Me.btnCustEndFind)
            Me.grbDetail.Controls.Add(Me.txtCustCodeEnd)
            Me.grbDetail.Controls.Add(Me.lblCustEnd)
            Me.grbDetail.Controls.Add(Me.btnCustStartFind)
            Me.grbDetail.Controls.Add(Me.txtCustCodeStart)
            Me.grbDetail.Controls.Add(Me.lblCustStart)
            Me.grbDetail.Controls.Add(Me.txtDocDateStart)
            Me.grbDetail.Controls.Add(Me.dtpDocDateStart)
            Me.grbDetail.Controls.Add(Me.lblDocDateStart)
            Me.grbDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.grbDetail.Location = New System.Drawing.Point(8, 16)
            Me.grbDetail.Name = "grbDetail"
            Me.grbDetail.Size = New System.Drawing.Size(400, 144)
            Me.grbDetail.TabIndex = 0
            Me.grbDetail.TabStop = False
            Me.grbDetail.Text = "�����ŷ����"
            '
            'chkIncludeChildren
            '
            Me.chkIncludeChildren.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.chkIncludeChildren.Location = New System.Drawing.Point(104, 88)
            Me.chkIncludeChildren.Name = "chkIncludeChildren"
            Me.chkIncludeChildren.Size = New System.Drawing.Size(128, 24)
            Me.chkIncludeChildren.TabIndex = 38
            Me.chkIncludeChildren.Text = "��� Cost Center �١"
            '
            'btnCCCodeStart
            '
            Me.btnCCCodeStart.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnCCCodeStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.btnCCCodeStart.ForeColor = System.Drawing.SystemColors.Control
            Me.btnCCCodeStart.Location = New System.Drawing.Point(200, 64)
            Me.btnCCCodeStart.Name = "btnCCCodeStart"
            Me.btnCCCodeStart.Size = New System.Drawing.Size(24, 22)
            Me.btnCCCodeStart.TabIndex = 37
            Me.btnCCCodeStart.TabStop = False
            Me.btnCCCodeStart.ThemedImage = CType(resources.GetObject("btnCCCodeStart.ThemedImage"), System.Drawing.Bitmap)
            '
            'txtCCCodeStart
            '
            Me.Validator.SetDataType(Me.txtCCCodeStart, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtCCCodeStart, "")
            Me.txtCCCodeStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.Validator.SetGotFocusBackColor(Me.txtCCCodeStart, System.Drawing.Color.Empty)
            Me.ErrorProvider1.SetIconPadding(Me.txtCCCodeStart, -15)
            Me.Validator.SetInvalidBackColor(Me.txtCCCodeStart, System.Drawing.Color.Empty)
            Me.txtCCCodeStart.Location = New System.Drawing.Point(104, 64)
            Me.txtCCCodeStart.MaxLength = 50
            Me.Validator.SetMinValue(Me.txtCCCodeStart, "")
            Me.txtCCCodeStart.Name = "txtCCCodeStart"
            Me.Validator.SetRegularExpression(Me.txtCCCodeStart, "")
            Me.Validator.SetRequired(Me.txtCCCodeStart, False)
            Me.txtCCCodeStart.Size = New System.Drawing.Size(96, 21)
            Me.txtCCCodeStart.TabIndex = 36
            '
            'lblCCStart
            '
            Me.lblCCStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblCCStart.ForeColor = System.Drawing.Color.Black
            Me.lblCCStart.Location = New System.Drawing.Point(24, 64)
            Me.lblCCStart.Name = "lblCCStart"
            Me.lblCCStart.Size = New System.Drawing.Size(72, 18)
            Me.lblCCStart.TabIndex = 34
            Me.lblCCStart.Text = "Cost Center"
            Me.lblCCStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtCostCenterName
            '
            Me.Validator.SetDataType(Me.txtCostCenterName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtCostCenterName, "")
            Me.txtCostCenterName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.Validator.SetGotFocusBackColor(Me.txtCostCenterName, System.Drawing.Color.Empty)
            Me.ErrorProvider1.SetIconPadding(Me.txtCostCenterName, -15)
            Me.Validator.SetInvalidBackColor(Me.txtCostCenterName, System.Drawing.Color.Empty)
            Me.txtCostCenterName.Location = New System.Drawing.Point(224, 64)
            Me.txtCostCenterName.MaxLength = 50
            Me.Validator.SetMinValue(Me.txtCostCenterName, "")
            Me.txtCostCenterName.Name = "txtCostCenterName"
            Me.txtCostCenterName.ReadOnly = True
            Me.Validator.SetRegularExpression(Me.txtCostCenterName, "")
            Me.Validator.SetRequired(Me.txtCostCenterName, False)
            Me.txtCostCenterName.Size = New System.Drawing.Size(160, 21)
            Me.txtCostCenterName.TabIndex = 35
            '
            'btnCustEndFind
            '
            Me.btnCustEndFind.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnCustEndFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.btnCustEndFind.ForeColor = System.Drawing.SystemColors.Control
            Me.btnCustEndFind.Location = New System.Drawing.Point(360, 40)
            Me.btnCustEndFind.Name = "btnCustEndFind"
            Me.btnCustEndFind.Size = New System.Drawing.Size(24, 22)
            Me.btnCustEndFind.TabIndex = 11
            Me.btnCustEndFind.TabStop = False
            Me.btnCustEndFind.ThemedImage = CType(resources.GetObject("btnCustEndFind.ThemedImage"), System.Drawing.Bitmap)
            '
            'txtCustCodeEnd
            '
            Me.Validator.SetDataType(Me.txtCustCodeEnd, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtCustCodeEnd, "")
            Me.txtCustCodeEnd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.Validator.SetGotFocusBackColor(Me.txtCustCodeEnd, System.Drawing.Color.Empty)
            Me.ErrorProvider1.SetIconPadding(Me.txtCustCodeEnd, -15)
            Me.Validator.SetInvalidBackColor(Me.txtCustCodeEnd, System.Drawing.Color.Empty)
            Me.txtCustCodeEnd.Location = New System.Drawing.Point(264, 40)
            Me.Validator.SetMinValue(Me.txtCustCodeEnd, "")
            Me.txtCustCodeEnd.Name = "txtCustCodeEnd"
            Me.Validator.SetRegularExpression(Me.txtCustCodeEnd, "")
            Me.Validator.SetRequired(Me.txtCustCodeEnd, False)
            Me.txtCustCodeEnd.Size = New System.Drawing.Size(96, 21)
            Me.txtCustCodeEnd.TabIndex = 10
            '
            'lblCustEnd
            '
            Me.lblCustEnd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblCustEnd.ForeColor = System.Drawing.Color.Black
            Me.lblCustEnd.Location = New System.Drawing.Point(232, 40)
            Me.lblCustEnd.Name = "lblCustEnd"
            Me.lblCustEnd.Size = New System.Drawing.Size(24, 18)
            Me.lblCustEnd.TabIndex = 9
            Me.lblCustEnd.Text = "�֧"
            Me.lblCustEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnCustStartFind
            '
            Me.btnCustStartFind.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnCustStartFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.btnCustStartFind.ForeColor = System.Drawing.SystemColors.Control
            Me.btnCustStartFind.Location = New System.Drawing.Point(200, 40)
            Me.btnCustStartFind.Name = "btnCustStartFind"
            Me.btnCustStartFind.Size = New System.Drawing.Size(24, 22)
            Me.btnCustStartFind.TabIndex = 8
            Me.btnCustStartFind.TabStop = False
            Me.btnCustStartFind.ThemedImage = CType(resources.GetObject("btnCustStartFind.ThemedImage"), System.Drawing.Bitmap)
            '
            'txtCustCodeStart
            '
            Me.Validator.SetDataType(Me.txtCustCodeStart, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtCustCodeStart, "")
            Me.txtCustCodeStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.Validator.SetGotFocusBackColor(Me.txtCustCodeStart, System.Drawing.Color.Empty)
            Me.ErrorProvider1.SetIconPadding(Me.txtCustCodeStart, -15)
            Me.Validator.SetInvalidBackColor(Me.txtCustCodeStart, System.Drawing.Color.Empty)
            Me.txtCustCodeStart.Location = New System.Drawing.Point(104, 40)
            Me.Validator.SetMinValue(Me.txtCustCodeStart, "")
            Me.txtCustCodeStart.Name = "txtCustCodeStart"
            Me.Validator.SetRegularExpression(Me.txtCustCodeStart, "")
            Me.Validator.SetRequired(Me.txtCustCodeStart, False)
            Me.txtCustCodeStart.Size = New System.Drawing.Size(96, 21)
            Me.txtCustCodeStart.TabIndex = 7
            '
            'lblCustStart
            '
            Me.lblCustStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblCustStart.ForeColor = System.Drawing.Color.Black
            Me.lblCustStart.Location = New System.Drawing.Point(8, 40)
            Me.lblCustStart.Name = "lblCustStart"
            Me.lblCustStart.Size = New System.Drawing.Size(88, 18)
            Me.lblCustStart.TabIndex = 6
            Me.lblCustStart.Text = "�١���"
            Me.lblCustStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtDocDateStart
            '
            Me.Validator.SetDataType(Me.txtDocDateStart, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
            Me.Validator.SetDisplayName(Me.txtDocDateStart, "")
            Me.Validator.SetGotFocusBackColor(Me.txtDocDateStart, System.Drawing.Color.Empty)
            Me.ErrorProvider1.SetIconPadding(Me.txtDocDateStart, -15)
            Me.Validator.SetInvalidBackColor(Me.txtDocDateStart, System.Drawing.Color.Empty)
            Me.txtDocDateStart.Location = New System.Drawing.Point(104, 16)
            Me.txtDocDateStart.MaxLength = 10
            Me.Validator.SetMinValue(Me.txtDocDateStart, "")
            Me.txtDocDateStart.Name = "txtDocDateStart"
            Me.Validator.SetRegularExpression(Me.txtDocDateStart, "")
            Me.Validator.SetRequired(Me.txtDocDateStart, False)
            Me.txtDocDateStart.Size = New System.Drawing.Size(99, 21)
            Me.txtDocDateStart.TabIndex = 1
            '
            'dtpDocDateStart
            '
            Me.dtpDocDateStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpDocDateStart.Location = New System.Drawing.Point(104, 16)
            Me.dtpDocDateStart.Name = "dtpDocDateStart"
            Me.dtpDocDateStart.Size = New System.Drawing.Size(120, 21)
            Me.dtpDocDateStart.TabIndex = 2
            Me.dtpDocDateStart.TabStop = False
            '
            'lblDocDateStart
            '
            Me.lblDocDateStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblDocDateStart.ForeColor = System.Drawing.Color.Black
            Me.lblDocDateStart.Location = New System.Drawing.Point(8, 16)
            Me.lblDocDateStart.Name = "lblDocDateStart"
            Me.lblDocDateStart.Size = New System.Drawing.Size(88, 18)
            Me.lblDocDateStart.TabIndex = 0
            Me.lblDocDateStart.Text = "�����"
            Me.lblDocDateStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'btnSearch
            '
            Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnSearch.Location = New System.Drawing.Point(605, 168)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(75, 23)
            Me.btnSearch.TabIndex = 2
            Me.btnSearch.Text = "����"
            '
            'btnReset
            '
            Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnReset.Location = New System.Drawing.Point(525, 168)
            Me.btnReset.Name = "btnReset"
            Me.btnReset.Size = New System.Drawing.Size(75, 23)
            Me.btnReset.TabIndex = 1
            Me.btnReset.TabStop = False
            Me.btnReset.Text = "������"
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
            'KeepKeyCombo1
            '
            Me.KeepKeyCombo1.FormattingEnabled = True
            Me.KeepKeyCombo1.Location = New System.Drawing.Point(150, 106)
            Me.KeepKeyCombo1.Name = "KeepKeyCombo1"
            Me.KeepKeyCombo1.Size = New System.Drawing.Size(121, 21)
            Me.KeepKeyCombo1.TabIndex = 60
            Me.KeepKeyCombo1.Visible = False
            '
            'RptARAgingFilterSubPanel
            '
            Me.Controls.Add(Me.grbMaster)
            Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.Name = "RptARAgingFilterSubPanel"
            Me.Size = New System.Drawing.Size(712, 216)
            Me.grbMaster.ResumeLayout(False)
            Me.grbMaster.PerformLayout()
            Me.grbDisplay.ResumeLayout(False)
            Me.grbDetail.ResumeLayout(False)
            Me.grbDetail.PerformLayout()
            CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region " SetLabelText "
    Public Sub SetLabelText()
      'If Not m_entity Is Nothing Then Me.Text = Me.StringParserService.Parse(Me.m_entity.TabPageText)
      Me.lblCustStart.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.lblCusStart}")
      Me.Validator.SetDisplayName(txtCustCodeStart, lblCustStart.Text)

      Me.lblDocDateStart.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.lblDocDateStart}")
      Me.Validator.SetDisplayName(txtDocDateStart, lblDocDateStart.Text)

      Me.lblCCStart.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.lblCCStart}")
      Me.Validator.SetDisplayName(txtCCCodeStart, lblCCStart.Text)

      ' Global {�֧}
      Me.lblCustEnd.Text = Me.StringParserService.Parse("${res:Global.FilterPanelTo}")
      Me.Validator.SetDisplayName(txtCustCodeEnd, lblCustEnd.Text)

      ' Button
      Me.btnSearch.Text = Me.StringParserService.Parse("${res:Global.SearchButtonText}")
      Me.btnReset.Text = Me.StringParserService.Parse("${res:Global.ResetButtonText}")

      ' GroupBox
      Me.grbMaster.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.grbMaster}")
      Me.grbDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.grbDetail}")

      'Checkbox
      Me.chkIncludeChildren.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.chkIncludeChildren}")

      Me.cmbShowPeriod.Items.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.cmbShowPeriod_Month}"))
      Me.cmbShowPeriod.Items.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.cmbShowPeriod_6Month}"))
      Me.cmbShowPeriod.Items.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.cmbShowPeriod_Year}"))
      Me.cmbShowPeriod.SelectedIndex = 0

      Me.cmbDueDateType.Items.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.cmbNotSpecialize}")) '������к�
            Me.cmbDueDateType.Items.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.cmbDuebyPUR}")) '㺢���Թ���/��ԡ��
      Me.cmbDueDateType.Items.Add(Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.cmbDuebyBA}")) '��ҧ���
      Me.cmbDueDateType.SelectedIndex = 0

      Me.lblDueDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.lblDueDate}")
      Me.chkDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.RptARAgingFilterSubPanel.chkDetail}")
    End Sub
#End Region

#Region "Member"
    Private m_customerstart As Customer
    Private m_customerend As Customer

    Private m_DocDateStart As Date
    Private m_cc As Costcenter
#End Region

#Region "Constructors"
    Public Sub New()
      MyBase.New()
      InitializeComponent()
      EventWiring()
      Initialize()

      SetLabelText()
      LoopControl(Me)
    End Sub
#End Region

#Region "Properties"
    Public Property CustomerStart() As Customer
      Get
        Return m_customerstart
      End Get
      Set(ByVal Value As Customer)
        m_customerstart = Value
      End Set
    End Property
    Public Property CustomerEnd() As Customer
      Get
        Return m_customerend
      End Get
      Set(ByVal Value As Customer)
        m_customerend = Value
      End Set
    End Property
    Public Property DocDateStart() As Date      Get        Return m_DocDateStart      End Get      Set(ByVal Value As Date)        m_DocDateStart = Value      End Set    End Property
    Public Property Costcenter() As Costcenter
      Get
        Return m_cc
      End Get
      Set(ByVal Value As Costcenter)
        m_cc = Value
      End Set
    End Property
#End Region

#Region "Methods"

    Private Sub Initialize()
      ClearCriterias()
    End Sub

    Private Sub ClearCriterias()
      For Each grbCtrl As Control In grbMaster.Controls
        If TypeOf grbCtrl Is Longkong.Pojjaman.Gui.Components.FixedGroupBox Then
          For Each Ctrl As Control In grbCtrl.Controls
            If TypeOf Ctrl Is TextBox Then
              Ctrl.Text = ""
            End If
          Next
        End If
      Next

      Me.Costcenter = New Costcenter

      Me.CustomerStart = New Customer
      Me.CustomerEnd = New Customer

      Dim dtStart As Date = Date.Now
      Me.DocDateStart = dtStart
      Me.txtDocDateStart.Text = MinDateToNull(Me.DocDateStart, "")
      Me.dtpDocDateStart.Value = Me.DocDateStart

      If Me.cmbShowPeriod.Items.Count > 0 Then
        Me.cmbShowPeriod.SelectedIndex = 0
      End If
      If Me.cmbDueDateType.Items.Count > 0 Then
        Me.cmbDueDateType.SelectedIndex = 0
      End If
      Me.chkDetail.Checked = False

    End Sub
    Public Overrides Function GetFilterString() As String

    End Function
    Public Overrides Function GetFilterArray() As Filter()
			Dim arr(9) As Filter
      arr(0) = New Filter("DocDateStart", IIf(Me.DocDateStart.Equals(Date.MinValue), DBNull.Value, Me.DocDateStart))
      arr(1) = New Filter("CustCodeStart", IIf(txtCustCodeStart.TextLength > 0, txtCustCodeStart.Text, DBNull.Value))
      arr(2) = New Filter("CustCodeEnd", IIf(txtCustCodeEnd.TextLength > 0, txtCustCodeEnd.Text, DBNull.Value))
      arr(3) = New Filter("cc_id", Me.ValidIdOrDBNull(m_cc))
      arr(4) = New Filter("IncludeChildCC", Me.chkIncludeChildren.Checked)
      arr(5) = New Filter("ShowPeriod", Me.cmbShowPeriod.SelectedIndex)
      arr(6) = New Filter("userRight", CType(ServiceManager.Services.GetService(GetType(SecurityService)), SecurityService).CurrentUser.Id)
      arr(7) = New Filter("DueDateBy", Me.cmbDueDateType.SelectedIndex)
			arr(8) = New Filter("ShowDetail", IIf(Me.chkDetail.Checked, 1, 0))
			arr(9) = New Filter("retentionview", IIf(rdbBillissue.Checked, 0, 1))
			Return arr
    End Function
    Public Overrides ReadOnly Property SearchButton() As System.Windows.Forms.Button
      Get
        Return Me.btnSearch
      End Get
    End Property

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
      ClearCriterias()
      Me.btnSearch.PerformClick()
    End Sub
#End Region

#Region "IReportFilterSubPanel"
    Public Function GetFixValueCollection() As BusinessLogic.DocPrintingItemCollection Implements IReportFilterSubPanel.GetFixValueCollection
      Dim dpiColl As New DocPrintingItemCollection
      Dim dpi As DocPrintingItem

      'Month
      dpi = New DocPrintingItem
      dpi.Mapping = "Month"
      dpi.Value = "" 'Me.cmbMonth.Text
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'Year
      dpi = New DocPrintingItem
      dpi.Mapping = "Year"
      dpi.Value = "" 'Me.cmbYear.Text
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'Docdate Start
      dpi = New DocPrintingItem
      dpi.Mapping = "DocdateStart"
      dpi.Value = Me.txtDocDateStart.Text
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'Customerer Start
      dpi = New DocPrintingItem
      dpi.Mapping = "CustomerStart"
      dpi.Value = Me.txtCustCodeStart.Text
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'Customer End
      dpi = New DocPrintingItem
      dpi.Mapping = "CustomerEnd"
      dpi.Value = Me.txtCustCodeEnd.Text
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'CostCenterStart
      dpi = New DocPrintingItem
      dpi.Mapping = "CostCenterStart"
      dpi.Value = Me.txtCostCenterName.Text
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      'CheckBox ChildInclude
      If Me.chkIncludeChildren.Checked Then
        dpi = New DocPrintingItem
        dpi.Mapping = "childincluded"
        dpi.Value = "(�����ѧ�Ѵ)"
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)
      End If

      'DueDateBy
      dpi = New DocPrintingItem
      dpi.Mapping = "DueDateBy"
      dpi.Value = Me.cmbDueDateType.Text
      dpi.DataType = "System.String"
      dpiColl.Add(dpi)

      If Me.chkDetail.Checked Then
        'Show Detail
        dpi = New DocPrintingItem
        dpi.Mapping = "ShowDetail"
        dpi.Value = Me.StringParserService.Parse("${res:Longkong.Pojjaman.BusinessLogic.RptARAging.ShowDetail}")        '"�ʴ������´"
        dpi.DataType = "System.String"
        dpiColl.Add(dpi)
      End If

      Return dpiColl
    End Function
#End Region

#Region " ChangeProperty "
    Private Sub EventWiring()
      AddHandler btnCustStartFind.Click, AddressOf Me.btnCustomerFind_Click
      AddHandler btnCustEndFind.Click, AddressOf Me.btnCustomerFind_Click

      AddHandler btnCCCodeStart.Click, AddressOf Me.btnCostcenterFind_Click
      AddHandler txtCCCodeStart.Validated, AddressOf Me.ChangeProperty

      AddHandler txtDocDateStart.Validated, AddressOf Me.ChangeProperty
      AddHandler dtpDocDateStart.ValueChanged, AddressOf Me.ChangeProperty
    End Sub

    Private m_dateSetting As Boolean
    Private Sub ChangeProperty(ByVal sender As Object, ByVal e As EventArgs)

      Select Case CType(sender, Control).Name.ToLower
        Case "txtcccodestart"
          Costcenter.GetCostCenter(txtCCCodeStart, Me.txtCostCenterName, m_cc, CType(ServiceManager.Services.GetService(GetType(SecurityService)), SecurityService).CurrentUser.Id)

        Case "dtpdocdatestart"
          If Not Me.DocDateStart.Equals(dtpDocDateStart.Value) Then
            If Not m_dateSetting Then
              Me.txtDocDateStart.Text = MinDateToNull(dtpDocDateStart.Value, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
              Me.DocDateStart = dtpDocDateStart.Value
            End If
          End If
        Case "txtdocdatestart"
          m_dateSetting = True
          If Not Me.txtDocDateStart.Text.Length = 0 AndAlso Me.Validator.GetErrorMessage(Me.txtDocDateStart) = "" Then
            Dim theDate As Date = CDate(Me.txtDocDateStart.Text)
            If Not Me.DocDateStart.Equals(theDate) Then
              dtpDocDateStart.Value = theDate
              Me.DocDateStart = dtpDocDateStart.Value
            End If
          Else
            Me.dtpDocDateStart.Value = Date.Now
            Me.DocDateStart = Date.MinValue
          End If
          m_dateSetting = False

          'Case "dtpdocdateend"
          '    If Not Me.DocDateEnd.Equals(dtpDocDateEnd.Value) Then
          '        If Not m_dateSetting Then
          '            Me.txtDocDateEnd.Text = MinDateToNull(dtpDocDateEnd.Value, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
          '            Me.DocDateEnd = dtpDocDateEnd.Value
          '        End If
          '    End If
          'Case "txtdocdateend"
          '    m_dateSetting = True
          '    If Not Me.txtDocDateEnd.Text.Length = 0 AndAlso Me.Validator.GetErrorMessage(Me.txtDocDateEnd) = "" Then
          '        Dim theDate As Date = CDate(Me.txtDocDateEnd.Text)
          '        If Not Me.DocDateEnd.Equals(theDate) Then
          '            dtpDocDateEnd.Value = theDate
          '            Me.DocDateEnd = dtpDocDateEnd.Value
          '        End If
          '    Else
          '        Me.dtpDocDateEnd.Value = Date.Now
          '        Me.DocDateEnd = Date.MinValue
          '    End If
          m_dateSetting = False

        Case Else

      End Select
    End Sub
#End Region

#Region "IClipboardHandler Overrides"
    Public Overrides ReadOnly Property EnablePaste() As Boolean
      Get
        Dim data As IDataObject = Clipboard.GetDataObject
        If data.GetDataPresent((New Customer).FullClassName) Then
          If Not Me.ActiveControl Is Nothing Then
            Select Case Me.ActiveControl.Name.ToLower
              Case "txtcustcodestart", "txtcustcodeend"
                Return True
            End Select
          End If
        End If
        ' Costcenter
        If data.GetDataPresent((New Costcenter).FullClassName) Then
          If Not Me.ActiveControl Is Nothing Then
            Select Case Me.ActiveControl.Name.ToLower
              Case "txtcccodestart", "txtcccodeend"
                Return True
            End Select
          End If
        End If
      End Get
    End Property
    Public Overrides Sub Paste(ByVal sender As Object, ByVal e As System.EventArgs)
      Dim data As IDataObject = Clipboard.GetDataObject
      If data.GetDataPresent((New Customer).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New Customer).FullClassName))
        Dim entity As New Customer(id)
        If Not Me.ActiveControl Is Nothing Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtcustcodestart"
              Me.SetCustomerStartDialog(entity)

            Case "txtcustcodeend"
              Me.SetCustomerEndDialog(entity)

          End Select
        End If
      End If
      ' Costcenter
      If data.GetDataPresent((New Costcenter).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New Costcenter).FullClassName))
        Dim entity As New Costcenter(id)
        If Not Me.ActiveControl Is Nothing Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtcostcentercodestart"
              Me.SetCCCodeStartDialog(entity)

            Case "txtcostcentercodeend"
              Me.SetCCCodeStartDialog(entity)

          End Select
        End If
      End If
    End Sub
#End Region

#Region " Event Handlers "
    Private Sub btnCustomerFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      Select Case CType(sender, Control).Name.ToLower
        Case "btncuststartfind"
          myEntityPanelService.OpenListDialog(New Customer, AddressOf SetCustomerStartDialog)
        Case "btncustendfind"
          myEntityPanelService.OpenListDialog(New Customer, AddressOf SetCustomerEndDialog)
      End Select
    End Sub
    ' Costcenter
    Private Sub btnCostcenterFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      Select Case CType(sender, Control).Name.ToLower
        Case "btncccodestart"
          myEntityPanelService.OpenTreeDialog(New Costcenter, AddressOf SetCCCodeStartDialog)
      End Select
    End Sub
    Private Sub SetCustomerStartDialog(ByVal e As ISimpleEntity)
      Me.txtCustCodeStart.Text = e.Code
      Customer.GetCustomer(txtCustCodeStart, txtTemp, Me.CustomerStart)
    End Sub
    Private Sub SetCustomerEndDialog(ByVal e As ISimpleEntity)
      Me.txtCustCodeEnd.Text = e.Code
      Customer.GetCustomer(txtCustCodeEnd, txtTemp, Me.CustomerEnd)
    End Sub
    Private Sub SetCCCodeStartDialog(ByVal e As ISimpleEntity)
      Me.txtCCCodeStart.Text = e.Code
      Costcenter.GetCostCenter(txtCCCodeStart, txtCostCenterName, m_cc, CType(ServiceManager.Services.GetService(GetType(SecurityService)), SecurityService).CurrentUser.Id)
    End Sub
#End Region

  End Class
End Namespace

