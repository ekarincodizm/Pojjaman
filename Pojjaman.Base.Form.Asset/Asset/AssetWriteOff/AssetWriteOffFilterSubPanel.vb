Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.Services
Imports Longkong.Core.Services
Namespace Longkong.Pojjaman.Gui.Panels
  Public Class AssetWriteOffFilterSubPanel
    'Inherits UserControl
    Inherits AbstractFilterSubPanel

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
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents grbMainDetail As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Validator As Longkong.Pojjaman.Gui.Components.PJMTextboxValidator
    Friend WithEvents txtFromCCPersonCode As System.Windows.Forms.TextBox
    Friend WithEvents txtFromCostCenterCode As System.Windows.Forms.TextBox
    Friend WithEvents txtFromCCPersonName As System.Windows.Forms.TextBox
    Friend WithEvents lblFromCCPerson As System.Windows.Forms.Label
    Friend WithEvents txtFromCostCenterName As System.Windows.Forms.TextBox
    Friend WithEvents lblFromCC As System.Windows.Forms.Label
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents btnCustomerDialog As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents btnFromCostCenterDialog As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents grbDocDate As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents txtDocDateStart As System.Windows.Forms.TextBox
    Friend WithEvents txtDocDateEnd As System.Windows.Forms.TextBox
    Friend WithEvents lblDocDateStart As System.Windows.Forms.Label
    Friend WithEvents lblDocDateEnd As System.Windows.Forms.Label
    Friend WithEvents dtpDocDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDocDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnFromCCPersonDialog As Longkong.Pojjaman.Gui.Components.ImageButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AssetWriteOffFilterSubPanel))
      Me.lblCode = New System.Windows.Forms.Label()
      Me.txtCode = New System.Windows.Forms.TextBox()
      Me.grbDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.btnReset = New System.Windows.Forms.Button()
      Me.grbMainDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.txtCustomerCode = New System.Windows.Forms.TextBox()
      Me.txtCustomerName = New System.Windows.Forms.TextBox()
      Me.btnCustomerDialog = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.lblCustomer = New System.Windows.Forms.Label()
      Me.txtFromCCPersonCode = New System.Windows.Forms.TextBox()
      Me.txtFromCostCenterCode = New System.Windows.Forms.TextBox()
      Me.txtFromCCPersonName = New System.Windows.Forms.TextBox()
      Me.lblFromCCPerson = New System.Windows.Forms.Label()
      Me.txtFromCostCenterName = New System.Windows.Forms.TextBox()
      Me.btnFromCostCenterDialog = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.lblFromCC = New System.Windows.Forms.Label()
      Me.btnFromCCPersonDialog = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
      Me.Validator = New Longkong.Pojjaman.Gui.Components.PJMTextboxValidator(Me.components)
      Me.btnSearch = New System.Windows.Forms.Button()
      Me.dtpDocDateEnd = New System.Windows.Forms.DateTimePicker()
      Me.dtpDocDateStart = New System.Windows.Forms.DateTimePicker()
      Me.lblDocDateEnd = New System.Windows.Forms.Label()
      Me.lblDocDateStart = New System.Windows.Forms.Label()
      Me.txtDocDateEnd = New System.Windows.Forms.TextBox()
      Me.txtDocDateStart = New System.Windows.Forms.TextBox()
      Me.grbDocDate = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.grbDetail.SuspendLayout()
      Me.grbMainDetail.SuspendLayout()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.grbDocDate.SuspendLayout()
      Me.SuspendLayout()
      '
      'lblCode
      '
      Me.lblCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCode.ForeColor = System.Drawing.Color.Black
      Me.lblCode.Location = New System.Drawing.Point(8, 30)
      Me.lblCode.Name = "lblCode"
      Me.lblCode.Size = New System.Drawing.Size(104, 18)
      Me.lblCode.TabIndex = 6
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
      Me.txtCode.Location = New System.Drawing.Point(112, 29)
      Me.Validator.SetMinValue(Me.txtCode, "")
      Me.txtCode.Name = "txtCode"
      Me.Validator.SetRegularExpression(Me.txtCode, "")
      Me.Validator.SetRequired(Me.txtCode, False)
      Me.txtCode.Size = New System.Drawing.Size(271, 21)
      Me.txtCode.TabIndex = 0
      '
      'grbDetail
      '
      Me.grbDetail.Controls.Add(Me.txtCustomerCode)
      Me.grbDetail.Controls.Add(Me.grbDocDate)
      Me.grbDetail.Controls.Add(Me.txtCustomerName)
      Me.grbDetail.Controls.Add(Me.btnSearch)
      Me.grbDetail.Controls.Add(Me.btnCustomerDialog)
      Me.grbDetail.Controls.Add(Me.btnReset)
      Me.grbDetail.Controls.Add(Me.lblCustomer)
      Me.grbDetail.Controls.Add(Me.grbMainDetail)
      Me.grbDetail.Controls.Add(Me.txtFromCCPersonCode)
      Me.grbDetail.Controls.Add(Me.txtFromCostCenterName)
      Me.grbDetail.Controls.Add(Me.txtFromCostCenterCode)
      Me.grbDetail.Controls.Add(Me.btnFromCCPersonDialog)
      Me.grbDetail.Controls.Add(Me.txtFromCCPersonName)
      Me.grbDetail.Controls.Add(Me.lblFromCC)
      Me.grbDetail.Controls.Add(Me.btnFromCostCenterDialog)
      Me.grbDetail.Controls.Add(Me.lblFromCCPerson)
      Me.grbDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbDetail.Location = New System.Drawing.Point(8, 8)
      Me.grbDetail.Name = "grbDetail"
      Me.grbDetail.Size = New System.Drawing.Size(647, 175)
      Me.grbDetail.TabIndex = 0
      Me.grbDetail.TabStop = False
      '
      'btnReset
      '
      Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnReset.Location = New System.Drawing.Point(487, 143)
      Me.btnReset.Name = "btnReset"
      Me.btnReset.Size = New System.Drawing.Size(75, 23)
      Me.btnReset.TabIndex = 4
      Me.btnReset.Text = "Reset"
      '
      'grbMainDetail
      '
      Me.grbMainDetail.Controls.Add(Me.txtCode)
      Me.grbMainDetail.Controls.Add(Me.lblCode)
      Me.grbMainDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbMainDetail.Location = New System.Drawing.Point(8, 16)
      Me.grbMainDetail.Name = "grbMainDetail"
      Me.grbMainDetail.Size = New System.Drawing.Size(400, 72)
      Me.grbMainDetail.TabIndex = 0
      Me.grbMainDetail.TabStop = False
      Me.grbMainDetail.Text = "��������´�����"
      '
      'txtCustomerCode
      '
      Me.Validator.SetDataType(Me.txtCustomerCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCustomerCode, "")
      Me.Validator.SetGotFocusBackColor(Me.txtCustomerCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCustomerCode, System.Drawing.Color.Empty)
      Me.txtCustomerCode.Location = New System.Drawing.Point(121, 145)
      Me.Validator.SetMinValue(Me.txtCustomerCode, "")
      Me.txtCustomerCode.Name = "txtCustomerCode"
      Me.Validator.SetRegularExpression(Me.txtCustomerCode, "")
      Me.Validator.SetRequired(Me.txtCustomerCode, False)
      Me.txtCustomerCode.Size = New System.Drawing.Size(80, 20)
      Me.txtCustomerCode.TabIndex = 4
      '
      'txtCustomerName
      '
      Me.Validator.SetDataType(Me.txtCustomerName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCustomerName, "")
      Me.Validator.SetGotFocusBackColor(Me.txtCustomerName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCustomerName, System.Drawing.Color.Empty)
      Me.txtCustomerName.Location = New System.Drawing.Point(201, 144)
      Me.Validator.SetMinValue(Me.txtCustomerName, "")
      Me.txtCustomerName.Name = "txtCustomerName"
      Me.txtCustomerName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtCustomerName, "")
      Me.Validator.SetRequired(Me.txtCustomerName, False)
      Me.txtCustomerName.Size = New System.Drawing.Size(168, 20)
      Me.txtCustomerName.TabIndex = 205
      Me.txtCustomerName.TabStop = False
      '
      'btnCustomerDialog
      '
      Me.btnCustomerDialog.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnCustomerDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnCustomerDialog.ForeColor = System.Drawing.SystemColors.Control
      Me.btnCustomerDialog.Location = New System.Drawing.Point(367, 142)
      Me.btnCustomerDialog.Name = "btnCustomerDialog"
      Me.btnCustomerDialog.Size = New System.Drawing.Size(24, 23)
      Me.btnCustomerDialog.TabIndex = 207
      Me.btnCustomerDialog.TabStop = False
      Me.btnCustomerDialog.ThemedImage = CType(resources.GetObject("btnCustomerDialog.ThemedImage"), System.Drawing.Bitmap)
      '
      'lblCustomer
      '
      Me.lblCustomer.BackColor = System.Drawing.Color.Transparent
      Me.lblCustomer.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCustomer.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblCustomer.Location = New System.Drawing.Point(17, 145)
      Me.lblCustomer.Name = "lblCustomer"
      Me.lblCustomer.Size = New System.Drawing.Size(104, 18)
      Me.lblCustomer.TabIndex = 204
      Me.lblCustomer.Text = "�١���:"
      Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtFromCCPersonCode
      '
      Me.Validator.SetDataType(Me.txtFromCCPersonCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtFromCCPersonCode, "")
      Me.Validator.SetGotFocusBackColor(Me.txtFromCCPersonCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtFromCCPersonCode, System.Drawing.Color.Empty)
      Me.txtFromCCPersonCode.Location = New System.Drawing.Point(121, 96)
      Me.Validator.SetMinValue(Me.txtFromCCPersonCode, "")
      Me.txtFromCCPersonCode.Name = "txtFromCCPersonCode"
      Me.Validator.SetRegularExpression(Me.txtFromCCPersonCode, "")
      Me.Validator.SetRequired(Me.txtFromCCPersonCode, False)
      Me.txtFromCCPersonCode.Size = New System.Drawing.Size(80, 20)
      Me.txtFromCCPersonCode.TabIndex = 1
      '
      'txtFromCostCenterCode
      '
      Me.Validator.SetDataType(Me.txtFromCostCenterCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtFromCostCenterCode, "")
      Me.Validator.SetGotFocusBackColor(Me.txtFromCostCenterCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtFromCostCenterCode, System.Drawing.Color.Empty)
      Me.txtFromCostCenterCode.Location = New System.Drawing.Point(121, 120)
      Me.Validator.SetMinValue(Me.txtFromCostCenterCode, "")
      Me.txtFromCostCenterCode.Name = "txtFromCostCenterCode"
      Me.Validator.SetRegularExpression(Me.txtFromCostCenterCode, "")
      Me.Validator.SetRequired(Me.txtFromCostCenterCode, False)
      Me.txtFromCostCenterCode.Size = New System.Drawing.Size(80, 20)
      Me.txtFromCostCenterCode.TabIndex = 2
      '
      'txtFromCCPersonName
      '
      Me.Validator.SetDataType(Me.txtFromCCPersonName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtFromCCPersonName, "")
      Me.Validator.SetGotFocusBackColor(Me.txtFromCCPersonName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtFromCCPersonName, System.Drawing.Color.Empty)
      Me.txtFromCCPersonName.Location = New System.Drawing.Point(201, 96)
      Me.Validator.SetMinValue(Me.txtFromCCPersonName, "")
      Me.txtFromCCPersonName.Name = "txtFromCCPersonName"
      Me.txtFromCCPersonName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtFromCCPersonName, "")
      Me.Validator.SetRequired(Me.txtFromCCPersonName, False)
      Me.txtFromCCPersonName.Size = New System.Drawing.Size(168, 20)
      Me.txtFromCCPersonName.TabIndex = 196
      Me.txtFromCCPersonName.TabStop = False
      '
      'lblFromCCPerson
      '
      Me.lblFromCCPerson.BackColor = System.Drawing.Color.Transparent
      Me.lblFromCCPerson.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblFromCCPerson.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblFromCCPerson.Location = New System.Drawing.Point(17, 96)
      Me.lblFromCCPerson.Name = "lblFromCCPerson"
      Me.lblFromCCPerson.Size = New System.Drawing.Size(104, 18)
      Me.lblFromCCPerson.TabIndex = 191
      Me.lblFromCCPerson.Text = "����Ѻ:"
      Me.lblFromCCPerson.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtFromCostCenterName
      '
      Me.Validator.SetDataType(Me.txtFromCostCenterName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtFromCostCenterName, "")
      Me.Validator.SetGotFocusBackColor(Me.txtFromCostCenterName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtFromCostCenterName, System.Drawing.Color.Empty)
      Me.txtFromCostCenterName.Location = New System.Drawing.Point(201, 120)
      Me.Validator.SetMinValue(Me.txtFromCostCenterName, "")
      Me.txtFromCostCenterName.Name = "txtFromCostCenterName"
      Me.txtFromCostCenterName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtFromCostCenterName, "")
      Me.Validator.SetRequired(Me.txtFromCostCenterName, False)
      Me.txtFromCostCenterName.Size = New System.Drawing.Size(168, 20)
      Me.txtFromCostCenterName.TabIndex = 196
      Me.txtFromCostCenterName.TabStop = False
      '
      'btnFromCostCenterDialog
      '
      Me.btnFromCostCenterDialog.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnFromCostCenterDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnFromCostCenterDialog.ForeColor = System.Drawing.SystemColors.Control
      Me.btnFromCostCenterDialog.Location = New System.Drawing.Point(367, 119)
      Me.btnFromCostCenterDialog.Name = "btnFromCostCenterDialog"
      Me.btnFromCostCenterDialog.Size = New System.Drawing.Size(24, 23)
      Me.btnFromCostCenterDialog.TabIndex = 201
      Me.btnFromCostCenterDialog.TabStop = False
      Me.btnFromCostCenterDialog.ThemedImage = CType(resources.GetObject("btnFromCostCenterDialog.ThemedImage"), System.Drawing.Bitmap)
      '
      'lblFromCC
      '
      Me.lblFromCC.BackColor = System.Drawing.Color.Transparent
      Me.lblFromCC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblFromCC.ForeColor = System.Drawing.SystemColors.WindowText
      Me.lblFromCC.Location = New System.Drawing.Point(17, 120)
      Me.lblFromCC.Name = "lblFromCC"
      Me.lblFromCC.Size = New System.Drawing.Size(104, 18)
      Me.lblFromCC.TabIndex = 192
      Me.lblFromCC.Text = "CostCenter:"
      Me.lblFromCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'btnFromCCPersonDialog
      '
      Me.btnFromCCPersonDialog.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnFromCCPersonDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnFromCCPersonDialog.ForeColor = System.Drawing.SystemColors.Control
      Me.btnFromCCPersonDialog.Location = New System.Drawing.Point(367, 95)
      Me.btnFromCCPersonDialog.Name = "btnFromCCPersonDialog"
      Me.btnFromCCPersonDialog.Size = New System.Drawing.Size(24, 23)
      Me.btnFromCCPersonDialog.TabIndex = 202
      Me.btnFromCCPersonDialog.TabStop = False
      Me.btnFromCCPersonDialog.ThemedImage = CType(resources.GetObject("btnFromCCPersonDialog.ThemedImage"), System.Drawing.Bitmap)
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
      'btnSearch
      '
      Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnSearch.Location = New System.Drawing.Point(567, 143)
      Me.btnSearch.Name = "btnSearch"
      Me.btnSearch.Size = New System.Drawing.Size(75, 23)
      Me.btnSearch.TabIndex = 5
      Me.btnSearch.Text = "Search"
      '
      'dtpDocDateEnd
      '
      Me.dtpDocDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
      Me.dtpDocDateEnd.Location = New System.Drawing.Point(80, 38)
      Me.dtpDocDateEnd.Name = "dtpDocDateEnd"
      Me.dtpDocDateEnd.Size = New System.Drawing.Size(136, 20)
      Me.dtpDocDateEnd.TabIndex = 11
      Me.dtpDocDateEnd.TabStop = False
      '
      'dtpDocDateStart
      '
      Me.dtpDocDateStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
      Me.dtpDocDateStart.Location = New System.Drawing.Point(80, 14)
      Me.dtpDocDateStart.Name = "dtpDocDateStart"
      Me.dtpDocDateStart.Size = New System.Drawing.Size(136, 20)
      Me.dtpDocDateStart.TabIndex = 10
      Me.dtpDocDateStart.TabStop = False
      '
      'lblDocDateEnd
      '
      Me.lblDocDateEnd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblDocDateEnd.ForeColor = System.Drawing.Color.Black
      Me.lblDocDateEnd.Location = New System.Drawing.Point(24, 39)
      Me.lblDocDateEnd.Name = "lblDocDateEnd"
      Me.lblDocDateEnd.Size = New System.Drawing.Size(56, 18)
      Me.lblDocDateEnd.TabIndex = 9
      Me.lblDocDateEnd.Text = "�֧"
      Me.lblDocDateEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblDocDateStart
      '
      Me.lblDocDateStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblDocDateStart.ForeColor = System.Drawing.Color.Black
      Me.lblDocDateStart.Location = New System.Drawing.Point(24, 15)
      Me.lblDocDateStart.Name = "lblDocDateStart"
      Me.lblDocDateStart.Size = New System.Drawing.Size(56, 18)
      Me.lblDocDateStart.TabIndex = 8
      Me.lblDocDateStart.Text = "�����"
      Me.lblDocDateStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtDocDateEnd
      '
      Me.txtDocDateEnd.BackColor = System.Drawing.SystemColors.Window
      Me.Validator.SetDataType(Me.txtDocDateEnd, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
      Me.Validator.SetDisplayName(Me.txtDocDateEnd, "")
      Me.Validator.SetGotFocusBackColor(Me.txtDocDateEnd, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtDocDateEnd, System.Drawing.Color.Empty)
      Me.txtDocDateEnd.Location = New System.Drawing.Point(80, 38)
      Me.Validator.SetMinValue(Me.txtDocDateEnd, "")
      Me.txtDocDateEnd.Name = "txtDocDateEnd"
      Me.Validator.SetRegularExpression(Me.txtDocDateEnd, "")
      Me.Validator.SetRequired(Me.txtDocDateEnd, False)
      Me.txtDocDateEnd.Size = New System.Drawing.Size(118, 20)
      Me.txtDocDateEnd.TabIndex = 7
      '
      'txtDocDateStart
      '
      Me.txtDocDateStart.BackColor = System.Drawing.SystemColors.Window
      Me.Validator.SetDataType(Me.txtDocDateStart, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
      Me.Validator.SetDisplayName(Me.txtDocDateStart, "")
      Me.Validator.SetGotFocusBackColor(Me.txtDocDateStart, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtDocDateStart, System.Drawing.Color.Empty)
      Me.txtDocDateStart.Location = New System.Drawing.Point(80, 14)
      Me.Validator.SetMinValue(Me.txtDocDateStart, "")
      Me.txtDocDateStart.Name = "txtDocDateStart"
      Me.Validator.SetRegularExpression(Me.txtDocDateStart, "")
      Me.Validator.SetRequired(Me.txtDocDateStart, False)
      Me.txtDocDateStart.Size = New System.Drawing.Size(118, 20)
      Me.txtDocDateStart.TabIndex = 6
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
      Me.grbDocDate.Location = New System.Drawing.Point(414, 16)
      Me.grbDocDate.Name = "grbDocDate"
      Me.grbDocDate.Size = New System.Drawing.Size(224, 72)
      Me.grbDocDate.TabIndex = 2
      Me.grbDocDate.TabStop = False
      Me.grbDocDate.Text = "�ѹ����͡���"
      '
      'AssetWriteOffFilterSubPanel
      '
      Me.Controls.Add(Me.grbDetail)
      Me.Name = "AssetWriteOffFilterSubPanel"
      Me.Size = New System.Drawing.Size(665, 191)
      Me.grbDetail.ResumeLayout(False)
      Me.grbDetail.PerformLayout()
      Me.grbMainDetail.ResumeLayout(False)
      Me.grbMainDetail.PerformLayout()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.grbDocDate.ResumeLayout(False)
      Me.grbDocDate.PerformLayout()
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

#Region " SetLabelText "
    Public Sub SetLabelText()
      Me.grbDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.grbDetail}")
      Me.grbMainDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.grbMainDetail}")
      Me.lblCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.lblCode}")
      Me.btnSearch.Text = Me.StringParserService.Parse("${res:Global.SearchButtonText}")
      Me.btnReset.Text = Me.StringParserService.Parse("${res:Global.ResetButtonText}")
      Me.grbDocDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.grbDocDate}")
      Me.lblDocDateStart.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.lblDocDateStart}")
      Me.lblDocDateEnd.Text = Me.StringParserService.Parse("${res:Global.FilterPanelTo}")
      Me.lblFromCCPerson.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.lblFromCCPerson}")
      Me.lblFromCC.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.lblFromCC}")
      Me.lblCustomer.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.lblCustomer}")
      Me.grbMainDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetWriteOffFilterSubPanel.grbMainDetail}")
    End Sub
#End Region

#Region "Members"
    Private m_fromcc As CostCenter
    Private m_customer As Customer
    Private m_employee As Employee
    Private docDateStart As Date
    Private docDateEnd As Date
#End Region

#Region "Methods"
    Public Sub Initialize()
      AddHandler txtDocDateStart.Validated, AddressOf Me.ChangeProperty
      AddHandler dtpDocDateStart.ValueChanged, AddressOf Me.ChangeProperty
      AddHandler txtDocDateEnd.Validated, AddressOf Me.ChangeProperty
      AddHandler dtpDocDateEnd.ValueChanged, AddressOf Me.ChangeProperty

      ClearCriterias()
    End Sub
    Private m_dateSetting As Boolean
    Public Sub ChangeProperty(ByVal sender As Object, ByVal e As EventArgs)
      Dim dirtyFlag As Boolean = False
      Select Case CType(sender, Control).Name.ToLower
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
      Me.txtFromCostCenterCode.Text = ""
      Me.txtFromCostCenterName.Text = ""
      Me.m_fromcc = New CostCenter

      Me.txtFromCCPersonCode.Text = ""
      Me.txtFromCCPersonName.Text = ""

      Me.txtCustomerCode.Text = ""
      Me.txtCustomerName.Text = ""

      Me.m_customer = New Customer
      Me.m_employee = New Employee

      Me.dtpDocDateStart.Value = DateAdd(DateInterval.Month, -1, Now.Date)
      Me.dtpDocDateEnd.Value = Now.Date

      Me.txtDocDateStart.Text = DateAdd(DateInterval.Month, -1, Now.Date)
      Me.txtDocDateEnd.Text = Now.Date

      Me.docDateStart = DateAdd(DateInterval.Month, -1, Now.Date)
      Me.docDateEnd = Now.Date

      EntityRefresh()
    End Sub

    Public Overrides Function GetFilterArray() As Filter()
      Dim arr(5) As Filter
      arr(0) = New Filter("code", IIf(Me.txtCode.Text.Length = 0, DBNull.Value, Me.txtCode.Text))
      arr(1) = New Filter("fromcc_id", IIf(Me.m_fromcc.Originated, Me.m_fromcc.Id, DBNull.Value))
      arr(2) = New Filter("docdatestart", ValidDateOrDBNull(docDateStart))
      arr(3) = New Filter("docdateend", ValidDateOrDBNull(docDateEnd))
      arr(4) = New Filter("cust_id", IIf(Me.m_customer.Originated, Me.m_customer.Id, DBNull.Value))
      arr(5) = New Filter("emp_id", IIf(Me.m_employee.Originated, Me.m_employee.Id, DBNull.Value))
      Return arr
    End Function
    Public Overrides ReadOnly Property SearchButton() As System.Windows.Forms.Button
      Get
        Return Me.btnSearch
      End Get
    End Property
#End Region

#Region "Event Handlers"
    Private Sub txtFromCostCenterCode_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFromCostCenterCode.Validated
      CostCenter.GetCostCenter(txtFromCostCenterCode, txtFromCostCenterName, Me.m_fromcc)
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
      ClearCriterias()
      Me.btnSearch.PerformClick()
    End Sub
    Private Sub btnFromCostCenterDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFromCostCenterDialog.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenTreeDialog(New CostCenter, AddressOf SetFromCostCenter)
    End Sub
    Private Sub SetFromCostCenter(ByVal e As ISimpleEntity)
      Me.txtFromCostCenterCode.Text = e.Code
      CostCenter.GetCostCenter(txtFromCostCenterCode, txtFromCostCenterName, Me.m_fromcc)
    End Sub
    Private Sub txtCustomerCode_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerCode.Validated
      Customer.GetCustomer(txtCustomerCode, txtCustomerName, Me.m_customer)
    End Sub
    Private Sub btnCustomerDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerDialog.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New Customer, AddressOf SetCustomer)
    End Sub
    Private Sub SetCustomer(ByVal e As ISimpleEntity)
      Me.txtCustomerCode.Text = e.Code
      Customer.GetCustomer(txtCustomerCode, txtCustomerName, Me.m_customer)
    End Sub

    Private Sub txtFromCCPersonCode_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFromCCPersonCode.Validated
      Employee.GetEmployee(txtFromCCPersonCode, txtFromCCPersonName, Me.m_employee)
    End Sub
    Private Sub btnFromCCPersonDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFromCCPersonDialog.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New Employee, AddressOf SetFromCCPersonCode)
    End Sub
    Private Sub SetFromCCPersonCode(ByVal e As ISimpleEntity)
      Me.txtFromCCPersonCode.Text = e.Code
      Employee.GetEmployee(txtFromCCPersonCode, txtFromCCPersonName, Me.m_employee)
    End Sub
#End Region

#Region "IClipboardHandler Overrides" 'Undone
    Public Overrides ReadOnly Property EnablePaste() As Boolean
      Get
        If Me.ActiveControl Is Nothing Then
          Return False
        End If
        Dim data As IDataObject = Clipboard.GetDataObject

        If data.GetDataPresent((New CostCenter).FullClassName) Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtfromcostcentercode", "txtfromcostcentername"
              Return True
          End Select
        End If
        If data.GetDataPresent((New Employee).FullClassName) Then
          Select Case Me.ActiveControl.Name.ToLower
            Case "txtfromccpersoncode", "txtfromccpersonname"
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
      If data.GetDataPresent((New CostCenter).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New CostCenter).FullClassName))
        Dim entity As New CostCenter(id)
        Select Case Me.ActiveControl.Name.ToLower
          Case "txtfromcostcentercode", "txtfromcostcentername"
            Me.SetFromCostCenter(entity)
        End Select
      End If
      'If data.GetDataPresent((New Employee).FullClassName) Then
      '  Dim id As Integer = CInt(data.GetData((New Employee).FullClassName))
      '  Dim entity As New Employee(id)
      '  Select Case Me.ActiveControl.Name.ToLower
      '    Case "txtfromccpersoncode", "txtfromccpersonname"
      '      Me.SetFromCCPerson(entity)
      '  End Select
      'End If
      'If data.GetDataPresent((New LCIItem).FullClassName) Then
      '  Dim id As Integer = CInt(data.GetData((New LCIItem).FullClassName))
      '  Dim entity As New LCIItem(id)
      '  Select Case Me.ActiveControl.Name.ToLower
      '    Case "txtlci", "txtlciname"
      '      Me.SetLCi(entity)
      '  End Select
      'End If
      'If data.GetDataPresent((New Tool).FullClassName) Then
      '  Dim id As Integer = CInt(data.GetData((New Tool).FullClassName))
      '  Dim entity As New Tool(id)
      '  Select Case Me.ActiveControl.Name.ToLower
      '    Case "txttool", "txttoolname"
      '      Me.SetTool(entity)
      '  End Select
      'End If
    End Sub
#End Region

    Public Overrides Property Entities() As System.Collections.ArrayList
      Get
        Return MyBase.Entities
      End Get
      Set(ByVal Value As System.Collections.ArrayList)
        MyBase.Entities = Value
        EntityRefresh()
      End Set
    End Property
    Private Sub EntityRefresh()
      If Entities Is Nothing Then
        Return
      End If
      For Each entity As ISimpleEntity In Entities

        'If TypeOf entity Is AssetSold Then
        '  Dim obj As AssetSold
        '  obj = CType(entity, AssetSold)
        '   Customer ...
        '  If obj.Customer.Originated Then
        '    Me.SetCustomer(obj.Customer)
        '    Me.txtCustomerCode.Enabled = False
        '    Me.txtCustomerName.Enabled = False
        '    Me.btnCustomerDialog.Enabled = False
        '  End If
        'End If
        'If TypeOf entity Is Customer Then
        '  Me.SetCustomer(entity)
        '  Me.txtCustomerCode.Enabled = False
        '  Me.txtCustomerName.Enabled = False
        '  Me.btnCustomerDialog.Enabled = False
        'End If
      Next
    End Sub

  End Class
End Namespace

