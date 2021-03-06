Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.Services
Imports Longkong.Core.Services
Namespace Longkong.Pojjaman.Gui.Panels
    Public Class PurchaseDNFilterSubPanel
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
        Friend WithEvents btnSearch As System.Windows.Forms.Button
        Friend WithEvents btnReset As System.Windows.Forms.Button
        Friend WithEvents grbDocDate As Longkong.Pojjaman.Gui.Components.FixedGroupBox
        Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents grbItem As Longkong.Pojjaman.Gui.Components.FixedGroupBox
        Friend WithEvents ibtnShowLCIDialog As Longkong.Pojjaman.Gui.Components.ImageButton
        Friend WithEvents ibtnShowLCI As Longkong.Pojjaman.Gui.Components.ImageButton
        Friend WithEvents lblLCI As System.Windows.Forms.Label
        Friend WithEvents txtLCI As System.Windows.Forms.TextBox
        Friend WithEvents txtLCIName As System.Windows.Forms.TextBox
        Friend WithEvents txtToolName As System.Windows.Forms.TextBox
        Friend WithEvents ibtnShowTool As Longkong.Pojjaman.Gui.Components.ImageButton
        Friend WithEvents lblTool As System.Windows.Forms.Label
        Friend WithEvents ibtnShowToolDialog As Longkong.Pojjaman.Gui.Components.ImageButton
        Friend WithEvents txtTool As System.Windows.Forms.TextBox
        Friend WithEvents lblBlank As System.Windows.Forms.Label
        Friend WithEvents txtBlank As System.Windows.Forms.TextBox
        Friend WithEvents grbMainDetail As Longkong.Pojjaman.Gui.Components.FixedGroupBox
        Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
        Friend WithEvents Validator As Longkong.Pojjaman.Gui.Components.PJMTextboxValidator
        Friend WithEvents txtDocDateStart As System.Windows.Forms.TextBox
        Friend WithEvents txtDocDateEnd As System.Windows.Forms.TextBox
        Friend WithEvents lblDocDateStart As System.Windows.Forms.Label
        Friend WithEvents lblDocDateEnd As System.Windows.Forms.Label
        Friend WithEvents dtpDocDateStart As System.Windows.Forms.DateTimePicker
        Friend WithEvents dtpDocDateEnd As System.Windows.Forms.DateTimePicker
        Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
        Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
        Friend WithEvents lblSupplier As System.Windows.Forms.Label
        Friend WithEvents btnSupplierPanel As Longkong.Pojjaman.Gui.Components.ImageButton
        Friend WithEvents btnSupplierDialog As Longkong.Pojjaman.Gui.Components.ImageButton
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(PurchaseDNFilterSubPanel))
            Me.lblCode = New System.Windows.Forms.Label
            Me.txtCode = New System.Windows.Forms.TextBox
            Me.grbDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox
            Me.grbDocDate = New Longkong.Pojjaman.Gui.Components.FixedGroupBox
            Me.txtDocDateStart = New System.Windows.Forms.TextBox
            Me.txtDocDateEnd = New System.Windows.Forms.TextBox
            Me.lblDocDateStart = New System.Windows.Forms.Label
            Me.lblDocDateEnd = New System.Windows.Forms.Label
            Me.dtpDocDateStart = New System.Windows.Forms.DateTimePicker
            Me.dtpDocDateEnd = New System.Windows.Forms.DateTimePicker
            Me.btnSearch = New System.Windows.Forms.Button
            Me.btnReset = New System.Windows.Forms.Button
            Me.grbItem = New Longkong.Pojjaman.Gui.Components.FixedGroupBox
            Me.lblBlank = New System.Windows.Forms.Label
            Me.txtBlank = New System.Windows.Forms.TextBox
            Me.ibtnShowLCIDialog = New Longkong.Pojjaman.Gui.Components.ImageButton
            Me.ibtnShowLCI = New Longkong.Pojjaman.Gui.Components.ImageButton
            Me.lblLCI = New System.Windows.Forms.Label
            Me.txtLCI = New System.Windows.Forms.TextBox
            Me.txtLCIName = New System.Windows.Forms.TextBox
            Me.txtToolName = New System.Windows.Forms.TextBox
            Me.ibtnShowTool = New Longkong.Pojjaman.Gui.Components.ImageButton
            Me.lblTool = New System.Windows.Forms.Label
            Me.ibtnShowToolDialog = New Longkong.Pojjaman.Gui.Components.ImageButton
            Me.txtTool = New System.Windows.Forms.TextBox
            Me.grbMainDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox
            Me.btnSupplierPanel = New Longkong.Pojjaman.Gui.Components.ImageButton
            Me.txtSupplierCode = New System.Windows.Forms.TextBox
            Me.txtSupplierName = New System.Windows.Forms.TextBox
            Me.btnSupplierDialog = New Longkong.Pojjaman.Gui.Components.ImageButton
            Me.lblSupplier = New System.Windows.Forms.Label
            Me.cmbStatus = New System.Windows.Forms.ComboBox
            Me.lblStatus = New System.Windows.Forms.Label
            Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider
            Me.Validator = New Longkong.Pojjaman.Gui.Components.PJMTextboxValidator(Me.components)
            Me.grbDetail.SuspendLayout()
            Me.grbDocDate.SuspendLayout()
            Me.grbItem.SuspendLayout()
            Me.grbMainDetail.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblCode
            '
            Me.lblCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblCode.ForeColor = System.Drawing.Color.Black
            Me.lblCode.Location = New System.Drawing.Point(8, 16)
            Me.lblCode.Name = "lblCode"
            Me.lblCode.Size = New System.Drawing.Size(104, 18)
            Me.lblCode.TabIndex = 6
            Me.lblCode.Text = "�Ţ����͡���:"
            Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtCode
            '
            Me.Validator.SetDataType(Me.txtCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtCode, "")
            Me.txtCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.Validator.SetGotFocusBackColor(Me.txtCode, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtCode, System.Drawing.Color.Empty)
            Me.txtCode.Location = New System.Drawing.Point(112, 16)
            Me.Validator.SetMaxValue(Me.txtCode, "")
            Me.Validator.SetMinValue(Me.txtCode, "")
            Me.txtCode.Name = "txtCode"
            Me.Validator.SetRegularExpression(Me.txtCode, "")
            Me.Validator.SetRequired(Me.txtCode, False)
            Me.txtCode.Size = New System.Drawing.Size(224, 21)
            Me.txtCode.TabIndex = 0
            Me.txtCode.Text = ""
            '
            'grbDetail
            '
            Me.grbDetail.Controls.Add(Me.grbDocDate)
            Me.grbDetail.Controls.Add(Me.btnSearch)
            Me.grbDetail.Controls.Add(Me.btnReset)
            Me.grbDetail.Controls.Add(Me.grbItem)
            Me.grbDetail.Controls.Add(Me.grbMainDetail)
            Me.grbDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.grbDetail.Location = New System.Drawing.Point(8, 8)
            Me.grbDetail.Name = "grbDetail"
            Me.grbDetail.Size = New System.Drawing.Size(728, 192)
            Me.grbDetail.TabIndex = 0
            Me.grbDetail.TabStop = False
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
            Me.grbDocDate.Location = New System.Drawing.Point(8, 112)
            Me.grbDocDate.Name = "grbDocDate"
            Me.grbDocDate.Size = New System.Drawing.Size(224, 72)
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
            Me.txtDocDateStart.Location = New System.Drawing.Point(68, 14)
            Me.Validator.SetMaxValue(Me.txtDocDateStart, "")
            Me.Validator.SetMinValue(Me.txtDocDateStart, "")
            Me.txtDocDateStart.Name = "txtDocDateStart"
            Me.Validator.SetRegularExpression(Me.txtDocDateStart, "")
            Me.Validator.SetRequired(Me.txtDocDateStart, False)
            Me.txtDocDateStart.Size = New System.Drawing.Size(118, 20)
            Me.txtDocDateStart.TabIndex = 6
            Me.txtDocDateStart.Text = ""
            '
            'txtDocDateEnd
            '
            Me.txtDocDateEnd.BackColor = System.Drawing.SystemColors.Window
            Me.Validator.SetDataType(Me.txtDocDateEnd, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
            Me.Validator.SetDisplayName(Me.txtDocDateEnd, "")
            Me.Validator.SetGotFocusBackColor(Me.txtDocDateEnd, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtDocDateEnd, System.Drawing.Color.Empty)
            Me.txtDocDateEnd.Location = New System.Drawing.Point(68, 38)
            Me.Validator.SetMaxValue(Me.txtDocDateEnd, "")
            Me.Validator.SetMinValue(Me.txtDocDateEnd, "")
            Me.txtDocDateEnd.Name = "txtDocDateEnd"
            Me.Validator.SetRegularExpression(Me.txtDocDateEnd, "")
            Me.Validator.SetRequired(Me.txtDocDateEnd, False)
            Me.txtDocDateEnd.Size = New System.Drawing.Size(118, 20)
            Me.txtDocDateEnd.TabIndex = 7
            Me.txtDocDateEnd.Text = ""
            '
            'lblDocDateStart
            '
            Me.lblDocDateStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblDocDateStart.ForeColor = System.Drawing.Color.Black
            Me.lblDocDateStart.Location = New System.Drawing.Point(20, 15)
            Me.lblDocDateStart.Name = "lblDocDateStart"
            Me.lblDocDateStart.Size = New System.Drawing.Size(40, 18)
            Me.lblDocDateStart.TabIndex = 8
            Me.lblDocDateStart.Text = "�����"
            Me.lblDocDateStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblDocDateEnd
            '
            Me.lblDocDateEnd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblDocDateEnd.ForeColor = System.Drawing.Color.Black
            Me.lblDocDateEnd.Location = New System.Drawing.Point(20, 39)
            Me.lblDocDateEnd.Name = "lblDocDateEnd"
            Me.lblDocDateEnd.Size = New System.Drawing.Size(40, 18)
            Me.lblDocDateEnd.TabIndex = 9
            Me.lblDocDateEnd.Text = "�֧"
            Me.lblDocDateEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'dtpDocDateStart
            '
            Me.dtpDocDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpDocDateStart.Location = New System.Drawing.Point(68, 14)
            Me.dtpDocDateStart.Name = "dtpDocDateStart"
            Me.dtpDocDateStart.Size = New System.Drawing.Size(136, 20)
            Me.dtpDocDateStart.TabIndex = 10
            Me.dtpDocDateStart.TabStop = False
            '
            'dtpDocDateEnd
            '
            Me.dtpDocDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpDocDateEnd.Location = New System.Drawing.Point(68, 38)
            Me.dtpDocDateEnd.Name = "dtpDocDateEnd"
            Me.dtpDocDateEnd.Size = New System.Drawing.Size(136, 20)
            Me.dtpDocDateEnd.TabIndex = 11
            Me.dtpDocDateEnd.TabStop = False
            '
            'btnSearch
            '
            Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnSearch.Location = New System.Drawing.Point(648, 160)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.TabIndex = 5
            Me.btnSearch.Text = "Search"
            '
            'btnReset
            '
            Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnReset.Location = New System.Drawing.Point(568, 160)
            Me.btnReset.Name = "btnReset"
            Me.btnReset.TabIndex = 4
            Me.btnReset.Text = "Reset"
            '
            'grbItem
            '
            Me.grbItem.Controls.Add(Me.lblBlank)
            Me.grbItem.Controls.Add(Me.txtBlank)
            Me.grbItem.Controls.Add(Me.ibtnShowLCIDialog)
            Me.grbItem.Controls.Add(Me.ibtnShowLCI)
            Me.grbItem.Controls.Add(Me.lblLCI)
            Me.grbItem.Controls.Add(Me.txtLCI)
            Me.grbItem.Controls.Add(Me.txtLCIName)
            Me.grbItem.Controls.Add(Me.txtToolName)
            Me.grbItem.Controls.Add(Me.ibtnShowTool)
            Me.grbItem.Controls.Add(Me.lblTool)
            Me.grbItem.Controls.Add(Me.ibtnShowToolDialog)
            Me.grbItem.Controls.Add(Me.txtTool)
            Me.grbItem.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.grbItem.Location = New System.Drawing.Point(368, 16)
            Me.grbItem.Name = "grbItem"
            Me.grbItem.Size = New System.Drawing.Size(352, 96)
            Me.grbItem.TabIndex = 1
            Me.grbItem.TabStop = False
            Me.grbItem.Text = "��觷��׹"
            '
            'lblBlank
            '
            Me.lblBlank.BackColor = System.Drawing.Color.Transparent
            Me.lblBlank.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblBlank.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblBlank.Location = New System.Drawing.Point(8, 64)
            Me.lblBlank.Name = "lblBlank"
            Me.lblBlank.Size = New System.Drawing.Size(104, 18)
            Me.lblBlank.TabIndex = 208
            Me.lblBlank.Text = "����:"
            Me.lblBlank.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtBlank
            '
            Me.Validator.SetDataType(Me.txtBlank, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtBlank, "")
            Me.Validator.SetGotFocusBackColor(Me.txtBlank, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtBlank, System.Drawing.Color.Empty)
            Me.txtBlank.Location = New System.Drawing.Point(112, 64)
            Me.Validator.SetMaxValue(Me.txtBlank, "")
            Me.Validator.SetMinValue(Me.txtBlank, "")
            Me.txtBlank.Name = "txtBlank"
            Me.Validator.SetRegularExpression(Me.txtBlank, "")
            Me.Validator.SetRequired(Me.txtBlank, False)
            Me.txtBlank.Size = New System.Drawing.Size(224, 20)
            Me.txtBlank.TabIndex = 2
            Me.txtBlank.Text = ""
            '
            'ibtnShowLCIDialog
            '
            Me.ibtnShowLCIDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.ibtnShowLCIDialog.ForeColor = System.Drawing.SystemColors.Control
            Me.ibtnShowLCIDialog.Image = CType(resources.GetObject("ibtnShowLCIDialog.Image"), System.Drawing.Image)
            Me.ibtnShowLCIDialog.Location = New System.Drawing.Point(288, 16)
            Me.ibtnShowLCIDialog.Name = "ibtnShowLCIDialog"
            Me.ibtnShowLCIDialog.Size = New System.Drawing.Size(24, 23)
            Me.ibtnShowLCIDialog.TabIndex = 206
            Me.ibtnShowLCIDialog.TabStop = False
            Me.ibtnShowLCIDialog.ThemedImage = CType(resources.GetObject("ibtnShowLCIDialog.ThemedImage"), System.Drawing.Bitmap)
            '
            'ibtnShowLCI
            '
            Me.ibtnShowLCI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.ibtnShowLCI.Image = CType(resources.GetObject("ibtnShowLCI.Image"), System.Drawing.Image)
            Me.ibtnShowLCI.Location = New System.Drawing.Point(312, 16)
            Me.ibtnShowLCI.Name = "ibtnShowLCI"
            Me.ibtnShowLCI.Size = New System.Drawing.Size(24, 23)
            Me.ibtnShowLCI.TabIndex = 205
            Me.ibtnShowLCI.TabStop = False
            Me.ibtnShowLCI.ThemedImage = CType(resources.GetObject("ibtnShowLCI.ThemedImage"), System.Drawing.Bitmap)
            '
            'lblLCI
            '
            Me.lblLCI.BackColor = System.Drawing.Color.Transparent
            Me.lblLCI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblLCI.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblLCI.Location = New System.Drawing.Point(8, 16)
            Me.lblLCI.Name = "lblLCI"
            Me.lblLCI.Size = New System.Drawing.Size(104, 18)
            Me.lblLCI.TabIndex = 203
            Me.lblLCI.Text = "��ʴ�:"
            Me.lblLCI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'txtLCI
            '
            Me.Validator.SetDataType(Me.txtLCI, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtLCI, "")
            Me.Validator.SetGotFocusBackColor(Me.txtLCI, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtLCI, System.Drawing.Color.Empty)
            Me.txtLCI.Location = New System.Drawing.Point(112, 16)
            Me.Validator.SetMaxValue(Me.txtLCI, "")
            Me.Validator.SetMinValue(Me.txtLCI, "")
            Me.txtLCI.Name = "txtLCI"
            Me.Validator.SetRegularExpression(Me.txtLCI, "")
            Me.Validator.SetRequired(Me.txtLCI, False)
            Me.txtLCI.Size = New System.Drawing.Size(80, 20)
            Me.txtLCI.TabIndex = 0
            Me.txtLCI.Text = ""
            '
            'txtLCIName
            '
            Me.Validator.SetDataType(Me.txtLCIName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtLCIName, "")
            Me.Validator.SetGotFocusBackColor(Me.txtLCIName, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtLCIName, System.Drawing.Color.Empty)
            Me.txtLCIName.Location = New System.Drawing.Point(192, 16)
            Me.Validator.SetMaxValue(Me.txtLCIName, "")
            Me.Validator.SetMinValue(Me.txtLCIName, "")
            Me.txtLCIName.Name = "txtLCIName"
            Me.txtLCIName.ReadOnly = True
            Me.Validator.SetRegularExpression(Me.txtLCIName, "")
            Me.Validator.SetRequired(Me.txtLCIName, False)
            Me.txtLCIName.Size = New System.Drawing.Size(96, 20)
            Me.txtLCIName.TabIndex = 204
            Me.txtLCIName.TabStop = False
            Me.txtLCIName.Text = ""
            '
            'txtToolName
            '
            Me.Validator.SetDataType(Me.txtToolName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtToolName, "")
            Me.Validator.SetGotFocusBackColor(Me.txtToolName, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtToolName, System.Drawing.Color.Empty)
            Me.txtToolName.Location = New System.Drawing.Point(192, 40)
            Me.Validator.SetMaxValue(Me.txtToolName, "")
            Me.Validator.SetMinValue(Me.txtToolName, "")
            Me.txtToolName.Name = "txtToolName"
            Me.txtToolName.ReadOnly = True
            Me.Validator.SetRegularExpression(Me.txtToolName, "")
            Me.Validator.SetRequired(Me.txtToolName, False)
            Me.txtToolName.Size = New System.Drawing.Size(96, 20)
            Me.txtToolName.TabIndex = 204
            Me.txtToolName.TabStop = False
            Me.txtToolName.Text = ""
            '
            'ibtnShowTool
            '
            Me.ibtnShowTool.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.ibtnShowTool.Image = CType(resources.GetObject("ibtnShowTool.Image"), System.Drawing.Image)
            Me.ibtnShowTool.Location = New System.Drawing.Point(312, 40)
            Me.ibtnShowTool.Name = "ibtnShowTool"
            Me.ibtnShowTool.Size = New System.Drawing.Size(24, 23)
            Me.ibtnShowTool.TabIndex = 205
            Me.ibtnShowTool.TabStop = False
            Me.ibtnShowTool.ThemedImage = CType(resources.GetObject("ibtnShowTool.ThemedImage"), System.Drawing.Bitmap)
            '
            'lblTool
            '
            Me.lblTool.BackColor = System.Drawing.Color.Transparent
            Me.lblTool.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblTool.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblTool.Location = New System.Drawing.Point(8, 40)
            Me.lblTool.Name = "lblTool"
            Me.lblTool.Size = New System.Drawing.Size(104, 18)
            Me.lblTool.TabIndex = 203
            Me.lblTool.Text = "����ͧ���:"
            Me.lblTool.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ibtnShowToolDialog
            '
            Me.ibtnShowToolDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.ibtnShowToolDialog.ForeColor = System.Drawing.SystemColors.Control
            Me.ibtnShowToolDialog.Image = CType(resources.GetObject("ibtnShowToolDialog.Image"), System.Drawing.Image)
            Me.ibtnShowToolDialog.Location = New System.Drawing.Point(288, 40)
            Me.ibtnShowToolDialog.Name = "ibtnShowToolDialog"
            Me.ibtnShowToolDialog.Size = New System.Drawing.Size(24, 23)
            Me.ibtnShowToolDialog.TabIndex = 206
            Me.ibtnShowToolDialog.TabStop = False
            Me.ibtnShowToolDialog.ThemedImage = CType(resources.GetObject("ibtnShowToolDialog.ThemedImage"), System.Drawing.Bitmap)
            '
            'txtTool
            '
            Me.Validator.SetDataType(Me.txtTool, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtTool, "")
            Me.Validator.SetGotFocusBackColor(Me.txtTool, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtTool, System.Drawing.Color.Empty)
            Me.txtTool.Location = New System.Drawing.Point(112, 40)
            Me.Validator.SetMaxValue(Me.txtTool, "")
            Me.Validator.SetMinValue(Me.txtTool, "")
            Me.txtTool.Name = "txtTool"
            Me.Validator.SetRegularExpression(Me.txtTool, "")
            Me.Validator.SetRequired(Me.txtTool, False)
            Me.txtTool.Size = New System.Drawing.Size(80, 20)
            Me.txtTool.TabIndex = 1
            Me.txtTool.Text = ""
            '
            'grbMainDetail
            '
            Me.grbMainDetail.Controls.Add(Me.btnSupplierPanel)
            Me.grbMainDetail.Controls.Add(Me.txtSupplierCode)
            Me.grbMainDetail.Controls.Add(Me.txtSupplierName)
            Me.grbMainDetail.Controls.Add(Me.btnSupplierDialog)
            Me.grbMainDetail.Controls.Add(Me.lblSupplier)
            Me.grbMainDetail.Controls.Add(Me.cmbStatus)
            Me.grbMainDetail.Controls.Add(Me.lblStatus)
            Me.grbMainDetail.Controls.Add(Me.txtCode)
            Me.grbMainDetail.Controls.Add(Me.lblCode)
            Me.grbMainDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.grbMainDetail.Location = New System.Drawing.Point(8, 16)
            Me.grbMainDetail.Name = "grbMainDetail"
            Me.grbMainDetail.Size = New System.Drawing.Size(352, 96)
            Me.grbMainDetail.TabIndex = 0
            Me.grbMainDetail.TabStop = False
            Me.grbMainDetail.Text = "��������´�����"
            '
            'btnSupplierPanel
            '
            Me.btnSupplierPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.btnSupplierPanel.Image = CType(resources.GetObject("btnSupplierPanel.Image"), System.Drawing.Image)
            Me.btnSupplierPanel.Location = New System.Drawing.Point(312, 64)
            Me.btnSupplierPanel.Name = "btnSupplierPanel"
            Me.btnSupplierPanel.Size = New System.Drawing.Size(24, 23)
            Me.btnSupplierPanel.TabIndex = 206
            Me.btnSupplierPanel.TabStop = False
            Me.btnSupplierPanel.ThemedImage = CType(resources.GetObject("btnSupplierPanel.ThemedImage"), System.Drawing.Bitmap)
            '
            'txtSupplierCode
            '
            Me.Validator.SetDataType(Me.txtSupplierCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtSupplierCode, "")
            Me.Validator.SetGotFocusBackColor(Me.txtSupplierCode, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtSupplierCode, System.Drawing.Color.Empty)
            Me.txtSupplierCode.Location = New System.Drawing.Point(112, 64)
            Me.Validator.SetMaxValue(Me.txtSupplierCode, "")
            Me.Validator.SetMinValue(Me.txtSupplierCode, "")
            Me.txtSupplierCode.Name = "txtSupplierCode"
            Me.Validator.SetRegularExpression(Me.txtSupplierCode, "")
            Me.Validator.SetRequired(Me.txtSupplierCode, False)
            Me.txtSupplierCode.Size = New System.Drawing.Size(80, 20)
            Me.txtSupplierCode.TabIndex = 4
            Me.txtSupplierCode.Text = ""
            '
            'txtSupplierName
            '
            Me.Validator.SetDataType(Me.txtSupplierName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
            Me.Validator.SetDisplayName(Me.txtSupplierName, "")
            Me.Validator.SetGotFocusBackColor(Me.txtSupplierName, System.Drawing.Color.Empty)
            Me.Validator.SetInvalidBackColor(Me.txtSupplierName, System.Drawing.Color.Empty)
            Me.txtSupplierName.Location = New System.Drawing.Point(192, 64)
            Me.Validator.SetMaxValue(Me.txtSupplierName, "")
            Me.Validator.SetMinValue(Me.txtSupplierName, "")
            Me.txtSupplierName.Name = "txtSupplierName"
            Me.txtSupplierName.ReadOnly = True
            Me.Validator.SetRegularExpression(Me.txtSupplierName, "")
            Me.Validator.SetRequired(Me.txtSupplierName, False)
            Me.txtSupplierName.Size = New System.Drawing.Size(96, 20)
            Me.txtSupplierName.TabIndex = 205
            Me.txtSupplierName.TabStop = False
            Me.txtSupplierName.Text = ""
            '
            'btnSupplierDialog
            '
            Me.btnSupplierDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.btnSupplierDialog.ForeColor = System.Drawing.SystemColors.Control
            Me.btnSupplierDialog.Image = CType(resources.GetObject("btnSupplierDialog.Image"), System.Drawing.Image)
            Me.btnSupplierDialog.Location = New System.Drawing.Point(288, 64)
            Me.btnSupplierDialog.Name = "btnSupplierDialog"
            Me.btnSupplierDialog.Size = New System.Drawing.Size(24, 23)
            Me.btnSupplierDialog.TabIndex = 207
            Me.btnSupplierDialog.TabStop = False
            Me.btnSupplierDialog.ThemedImage = CType(resources.GetObject("btnSupplierDialog.ThemedImage"), System.Drawing.Bitmap)
            '
            'lblSupplier
            '
            Me.lblSupplier.BackColor = System.Drawing.Color.Transparent
            Me.lblSupplier.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblSupplier.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblSupplier.Location = New System.Drawing.Point(8, 64)
            Me.lblSupplier.Name = "lblSupplier"
            Me.lblSupplier.Size = New System.Drawing.Size(104, 18)
            Me.lblSupplier.TabIndex = 204
            Me.lblSupplier.Text = "�����:"
            Me.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'cmbStatus
            '
            Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbStatus.Location = New System.Drawing.Point(112, 40)
            Me.cmbStatus.Name = "cmbStatus"
            Me.cmbStatus.Size = New System.Drawing.Size(224, 21)
            Me.cmbStatus.TabIndex = 3
            '
            'lblStatus
            '
            Me.lblStatus.BackColor = System.Drawing.Color.Transparent
            Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.lblStatus.ForeColor = System.Drawing.SystemColors.WindowText
            Me.lblStatus.Location = New System.Drawing.Point(8, 40)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(104, 18)
            Me.lblStatus.TabIndex = 197
            Me.lblStatus.Text = "ʶҹ�:"
            Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
            Me.Validator.GotFocusBackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(255, Byte), CType(255, Byte))
            Me.Validator.HasNewRow = False
            Me.Validator.InvalidBackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
            '
            'PurchaseDNFilterSubPanel
            '
            Me.Controls.Add(Me.grbDetail)
            Me.Name = "PurchaseDNFilterSubPanel"
            Me.Size = New System.Drawing.Size(744, 208)
            Me.grbDetail.ResumeLayout(False)
            Me.grbDocDate.ResumeLayout(False)
            Me.grbItem.ResumeLayout(False)
            Me.grbMainDetail.ResumeLayout(False)
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
        Private m_lci As LCIItem
        Private m_tool As Tool
        Private m_supplier As Supplier
        Private docDateStart As Date
        Private docDateEnd As Date
        Private receivingDateStart As Date
        Private receivingDateEnd As Date
#End Region

#Region "Methods"
        Public Sub Initialize()
            AddHandler txtDocDateStart.Validated, AddressOf Me.ChangeProperty
            AddHandler dtpDocDateStart.ValueChanged, AddressOf Me.ChangeProperty
            AddHandler txtDocDateEnd.Validated, AddressOf Me.ChangeProperty
            AddHandler dtpDocDateEnd.ValueChanged, AddressOf Me.ChangeProperty

            PopulateStatus()
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

            Me.txtSupplierCode.Text = ""
            Me.txtSupplierName.Text = ""
            Me.m_supplier = New Supplier

            Me.txtLCI.Text = ""
            Me.txtLCIName.Text = ""
            Me.m_lci = New LCIItem

            Me.txtTool.Text = ""
            Me.txtToolName.Text = ""
            Me.m_tool = New Tool

            Me.txtBlank.Text = ""

            Me.dtpDocDateStart.Value = Now.Date
            Me.dtpDocDateEnd.Value = Now.Date

            Me.txtDocDateStart.Text = ""
            Me.txtDocDateEnd.Text = ""

            Me.docDateStart = Date.MinValue
            Me.docDateEnd = Date.MinValue

            cmbStatus.SelectedIndex = 0
            EntityRefresh()
        End Sub
        Private Sub PopulateStatus()
            CodeDescription.ListCodeDescriptionInComboBox(cmbStatus, "goodsreceipt_status", True)
        End Sub
        Public Sub SetLabelText()
            Me.grbDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.grbDetail}")
            Me.lblCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.lblCode}")
            Me.btnSearch.Text = Me.StringParserService.Parse("${res:Global.SearchButtonText}")
            Me.btnReset.Text = Me.StringParserService.Parse("${res:Global.ResetButtonText}")
            Me.grbDocDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.grbDocDate}")
            Me.lblDocDateStart.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.lblDocDateStart}")
            Me.lblDocDateEnd.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.lblDocDateEnd}")
            Me.lblStatus.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.lblStatus}")
            Me.grbItem.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.grbItem}")
            Me.lblLCI.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.lblLCI}")
            Me.lblTool.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.lblTool}")
            Me.lblBlank.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.lblBlank}")
            Me.lblSupplier.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.lblSupplier}")
            Me.grbMainDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.PurchaseDNFilterSubPanel.grbMainDetail}")
        End Sub
        Public Overrides Function GetFilterArray() As Filter()
            Dim arr(7) As Filter
            arr(0) = New Filter("code", IIf(Me.txtCode.Text.Length = 0, DBNull.Value, Me.txtCode.Text))
            arr(1) = New Filter("docdatestart", ValidDateOrDBNull(docDateStart))
            arr(2) = New Filter("docdateend", ValidDateOrDBNull(docDateEnd))
            arr(3) = New Filter("status", IIf(cmbStatus.SelectedItem Is Nothing, DBNull.Value, CType(cmbStatus.SelectedItem, IdValuePair).Id))
            arr(4) = New Filter("lci_id", IIf(Me.m_lci.Originated, Me.m_lci.Id, DBNull.Value))
            arr(5) = New Filter("tool_id", IIf(Me.m_tool.Originated, Me.m_tool.Id, DBNull.Value))
            arr(6) = New Filter("stocki_itemName", IIf(Me.txtBlank.Text.Length = 0, DBNull.Value, Me.txtBlank.Text))
            arr(7) = New Filter("supplier_id", IIf(Me.m_supplier.Originated, Me.m_supplier.Id, DBNull.Value))
            Return arr
        End Function
        Public Overrides ReadOnly Property SearchButton() As System.Windows.Forms.Button
            Get
                Return Me.btnSearch
            End Get
        End Property
#End Region

#Region "Event Handlers"
        Private Sub ibtnShowLCI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnShowLCI.Click
            Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
            myEntityPanelService.OpenPanel(New LCIItem)
        End Sub

        Private Sub ibtnShowTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnShowTool.Click
            Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
            myEntityPanelService.OpenPanel(New Tool)
        End Sub
        Private Sub txtLCI_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLCI.Validated
            LCIItem.GetLCIItem(txtLCI, txtLCIName, Me.m_lci)
        End Sub
        Private Sub SetLCi(ByVal e As ISimpleEntity)
            Me.txtLCI.Text = e.Code
            LCIItem.GetLCIItem(txtLCI, txtLCIName, Me.m_lci)
        End Sub
        Private Sub txtTool_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTool.Validated
            Tool.GetTool(txtTool, txtToolName, Me.m_tool)
        End Sub
        Private Sub SetTool(ByVal e As ISimpleEntity)
            Me.txtTool.Text = e.Code
            Tool.GetTool(txtTool, txtToolName, Me.m_tool)
        End Sub
        Private Sub ibtnShowLCIDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnShowLCIDialog.Click
            Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
            myEntityPanelService.OpenListDialog(New LCIItem, AddressOf SetLCi)
        End Sub
        Private Sub ibtnShowToolDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnShowToolDialog.Click
            Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
            myEntityPanelService.OpenListDialog(New Tool, AddressOf SetTool)
        End Sub
        Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
            ClearCriterias()
            Me.btnSearch.PerformClick()
        End Sub
        Private Sub txtSupplierCode_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplierCode.Validated
            Supplier.GetSupplier(txtSupplierCode, txtSupplierName, Me.m_supplier)
        End Sub
        Private Sub btnSupplierDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierDialog.Click
            Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
            myEntityPanelService.OpenListDialog(New Supplier, AddressOf SetSupplier)
        End Sub
        Private Sub btnSupplierPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierPanel.Click
            Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
            myEntityPanelService.OpenPanel(New Supplier)
        End Sub
        Private Sub SetSupplier(ByVal e As ISimpleEntity)
            Me.txtSupplierCode.Text = e.Code
            Supplier.GetSupplier(txtSupplierCode, txtSupplierName, Me.m_supplier)
        End Sub
#End Region

#Region "IClipboardHandler Overrides" 'Undone
        Public Overrides ReadOnly Property EnablePaste() As Boolean
            Get
                If Me.ActiveControl Is Nothing Then
                    Return False
                End If
                Dim data As IDataObject = Clipboard.GetDataObject

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
            If data.GetDataPresent((New LCIItem).FullClassName) Then
                Dim id As Integer = CInt(data.GetData((New LCIItem).FullClassName))
                Dim entity As New LCIItem(id)
                Select Case Me.ActiveControl.Name.ToLower
                    Case "txtlci", "txtlciname"
                        Me.SetLCi(entity)
                End Select
            End If
            If data.GetDataPresent((New Tool).FullClassName) Then
                Dim id As Integer = CInt(data.GetData((New Tool).FullClassName))
                Dim entity As New Tool(id)
                Select Case Me.ActiveControl.Name.ToLower
                    Case "txttool", "txttoolname"
                        Me.SetTool(entity)
                End Select
            End If
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

                If TypeOf entity Is GoodsReceipt Then
                    Dim obj As GoodsReceipt
                    obj = CType(entity, GoodsReceipt)
                    ' Supplier ...
                    If obj.Supplier.Originated Then
                        Me.SetSupplier(obj.Supplier)
                        Me.txtSupplierCode.Enabled = False
                        Me.txtSupplierName.Enabled = False
                        Me.btnSupplierDialog.Enabled = False
                        Me.btnSupplierPanel.Enabled = False
                    End If
                End If
                If TypeOf entity Is Supplier Then
                    Me.SetSupplier(entity)
                    Me.txtSupplierCode.Enabled = False
                    Me.txtSupplierName.Enabled = False
                    Me.btnSupplierDialog.Enabled = False
                    Me.btnSupplierPanel.Enabled = False
                End If
            Next
        End Sub
    End Class
End Namespace

