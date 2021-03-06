

Imports System.Collections.Generic
Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.TextHelper
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.Services
Imports Longkong.Core.AddIns
Namespace Longkong.Pojjaman.Gui.Panels
  Public Class EquipmentDetailView
    'Inherits UserControl
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
    Friend WithEvents grbDetail As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents txtEQIName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents Validator As Longkong.Pojjaman.Gui.Components.PJMTextboxValidator
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblEQTCode As System.Windows.Forms.Label
    Friend WithEvents TextEQIBuycost As System.Windows.Forms.TextBox

    Friend WithEvents Grbeqi As System.Windows.Forms.GroupBox
    Friend WithEvents TxtBuyDocDate As System.Windows.Forms.TextBox
    Friend WithEvents txtCostCenterName As System.Windows.Forms.TextBox
    Friend WithEvents ibtnCostcenterDialog As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents ibtnShowcostcenter As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtCostcenterAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtlEQIlicense As System.Windows.Forms.TextBox
    Friend WithEvents lblCostcenterAddress As System.Windows.Forms.Label
    Friend WithEvents txtModel As System.Windows.Forms.TextBox
    Friend WithEvents txtSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtCostcenterCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCostCentername As System.Windows.Forms.Label
    Friend WithEvents lblRefDoc As System.Windows.Forms.Label
    Friend WithEvents txtEQIbuydoccode As System.Windows.Forms.TextBox
    Friend WithEvents lblRefDocDate As System.Windows.Forms.Label
    Friend WithEvents cmbCode As System.Windows.Forms.ComboBox
    Friend WithEvents chkAutorun As System.Windows.Forms.CheckBox
    Public WithEvents lv As Longkong.Pojjaman.Gui.Components.PJMListView
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents tgItem As Longkong.Pojjaman.Gui.Components.TreeGrid
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents ibtnShowUnit1 As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtUnitCode As System.Windows.Forms.TextBox
    Friend WithEvents ibtnShowUnitDialog1 As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents lblFairPriceUnit As System.Windows.Forms.Label
    Friend WithEvents txtRentalRate As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblSerailNo As System.Windows.Forms.Label
    Friend WithEvents lblModel As System.Windows.Forms.Label
    Friend WithEvents lblLicenseNo As System.Windows.Forms.Label
    Friend WithEvents lblBuycost As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblRentalRate As System.Windows.Forms.Label
    Friend WithEvents lblunit As System.Windows.Forms.Label
    Friend WithEvents CmbEQCode As System.Windows.Forms.ComboBox
    Friend WithEvents lblEquipmentCode As System.Windows.Forms.Label
    Friend WithEvents lblEquipmentName As System.Windows.Forms.Label
    Friend WithEvents txtEQName As System.Windows.Forms.TextBox
    Friend WithEvents chkEqAutoRun As System.Windows.Forms.CheckBox
    Friend WithEvents lblPicSize As System.Windows.Forms.Label
    Friend WithEvents btnLoadImage As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents btnClearImage As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents txtAssetName As System.Windows.Forms.TextBox
    Friend WithEvents lblAsset As System.Windows.Forms.Label
    Friend WithEvents btnAssetFind As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents txtAssetCode As System.Windows.Forms.TextBox
    Friend WithEvents txtReference As System.Windows.Forms.TextBox
    Friend WithEvents lblReference As System.Windows.Forms.Label
    Friend WithEvents btnPurchaseFind As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents ibtnDel As System.Windows.Forms.Button
    Friend WithEvents ibtnNewLot As System.Windows.Forms.Button
    Friend WithEvents ibtnSave As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Protected Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EquipmentDetailView))
      Me.grbDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.CmbEQCode = New System.Windows.Forms.ComboBox()
      Me.lblEquipmentCode = New System.Windows.Forms.Label()
      Me.lblEquipmentName = New System.Windows.Forms.Label()
      Me.txtEQName = New System.Windows.Forms.TextBox()
      Me.chkEqAutoRun = New System.Windows.Forms.CheckBox()
      Me.lv = New Longkong.Pojjaman.Gui.Components.PJMListView()
      Me.Grbeqi = New System.Windows.Forms.GroupBox()
      Me.ibtnDel = New System.Windows.Forms.Button()
      Me.ibtnNewLot = New System.Windows.Forms.Button()
      Me.ibtnSave = New System.Windows.Forms.Button()
      Me.btnPurchaseFind = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtReference = New System.Windows.Forms.TextBox()
      Me.lblReference = New System.Windows.Forms.Label()
      Me.lblLicenseNo = New System.Windows.Forms.Label()
      Me.lblModel = New System.Windows.Forms.Label()
      Me.txtAssetName = New System.Windows.Forms.TextBox()
      Me.lblSerailNo = New System.Windows.Forms.Label()
      Me.TxtBuyDocDate = New System.Windows.Forms.TextBox()
      Me.txtDescription = New System.Windows.Forms.TextBox()
      Me.btnLoadImage = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtSerialNumber = New System.Windows.Forms.TextBox()
      Me.lblCostCentername = New System.Windows.Forms.Label()
      Me.lblDescription = New System.Windows.Forms.Label()
      Me.btnClearImage = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtModel = New System.Windows.Forms.TextBox()
      Me.lblBuycost = New System.Windows.Forms.Label()
      Me.txtlEQIlicense = New System.Windows.Forms.TextBox()
      Me.lblPicSize = New System.Windows.Forms.Label()
      Me.lblRefDocDate = New System.Windows.Forms.Label()
      Me.txtCostcenterCode = New System.Windows.Forms.TextBox()
      Me.txtEQIbuydoccode = New System.Windows.Forms.TextBox()
      Me.lblFairPriceUnit = New System.Windows.Forms.Label()
      Me.txtCostCenterName = New System.Windows.Forms.TextBox()
      Me.txtRentalRate = New System.Windows.Forms.TextBox()
      Me.picImage = New System.Windows.Forms.PictureBox()
      Me.lblRefDoc = New System.Windows.Forms.Label()
      Me.cmbCode = New System.Windows.Forms.ComboBox()
      Me.ibtnShowcostcenter = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.chkAutorun = New System.Windows.Forms.CheckBox()
      Me.TextEQIBuycost = New System.Windows.Forms.TextBox()
      Me.lblEQTCode = New System.Windows.Forms.Label()
      Me.ibtnCostcenterDialog = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtStatus = New System.Windows.Forms.TextBox()
      Me.txtCostcenterAddress = New System.Windows.Forms.TextBox()
      Me.txtUnit = New System.Windows.Forms.TextBox()
      Me.lblCostcenterAddress = New System.Windows.Forms.Label()
      Me.lblAsset = New System.Windows.Forms.Label()
      Me.lblStatus = New System.Windows.Forms.Label()
      Me.lblunit = New System.Windows.Forms.Label()
      Me.lblName = New System.Windows.Forms.Label()
      Me.btnAssetFind = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.txtEQIName = New System.Windows.Forms.TextBox()
      Me.ibtnShowUnit1 = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.lblRentalRate = New System.Windows.Forms.Label()
      Me.txtAssetCode = New System.Windows.Forms.TextBox()
      Me.txtUnitCode = New System.Windows.Forms.TextBox()
      Me.ibtnShowUnitDialog1 = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.Validator = New Longkong.Pojjaman.Gui.Components.PJMTextboxValidator(Me.components)
      Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
      Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
      Me.grbDetail.SuspendLayout()
      Me.Grbeqi.SuspendLayout()
      CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'grbDetail
      '
      Me.grbDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.grbDetail.Controls.Add(Me.CmbEQCode)
      Me.grbDetail.Controls.Add(Me.lblEquipmentCode)
      Me.grbDetail.Controls.Add(Me.lblEquipmentName)
      Me.grbDetail.Controls.Add(Me.txtEQName)
      Me.grbDetail.Controls.Add(Me.chkEqAutoRun)
      Me.grbDetail.Controls.Add(Me.lv)
      Me.grbDetail.Controls.Add(Me.Grbeqi)
      Me.grbDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.grbDetail.ForeColor = System.Drawing.Color.Blue
      Me.grbDetail.Location = New System.Drawing.Point(8, 8)
      Me.grbDetail.Name = "grbDetail"
      Me.grbDetail.Size = New System.Drawing.Size(994, 639)
      Me.grbDetail.TabIndex = 0
      Me.grbDetail.TabStop = False
      Me.grbDetail.Text = "��������ͧ�ѡ� :"
      '
      'CmbEQCode
      '
      Me.CmbEQCode.Location = New System.Drawing.Point(126, 28)
      Me.CmbEQCode.Name = "CmbEQCode"
      Me.CmbEQCode.Size = New System.Drawing.Size(139, 21)
      Me.CmbEQCode.TabIndex = 1
      '
      'lblEquipmentCode
      '
      Me.lblEquipmentCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblEquipmentCode.ForeColor = System.Drawing.Color.Black
      Me.lblEquipmentCode.Location = New System.Drawing.Point(19, 25)
      Me.lblEquipmentCode.Name = "lblEquipmentCode"
      Me.lblEquipmentCode.Size = New System.Drawing.Size(106, 23)
      Me.lblEquipmentCode.TabIndex = 334
      Me.lblEquipmentCode.Text = "���� :"
      Me.lblEquipmentCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblEquipmentName
      '
      Me.lblEquipmentName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblEquipmentName.ForeColor = System.Drawing.Color.Black
      Me.lblEquipmentName.Location = New System.Drawing.Point(19, 51)
      Me.lblEquipmentName.Name = "lblEquipmentName"
      Me.lblEquipmentName.Size = New System.Drawing.Size(106, 18)
      Me.lblEquipmentName.TabIndex = 337
      Me.lblEquipmentName.Text = "��Դ :"
      Me.lblEquipmentName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtEQName
      '
      Me.Validator.SetDataType(Me.txtEQName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtEQName, "")
      Me.txtEQName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtEQName, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtEQName, -15)
      Me.Validator.SetInvalidBackColor(Me.txtEQName, System.Drawing.Color.Empty)
      Me.txtEQName.Location = New System.Drawing.Point(126, 51)
      Me.txtEQName.MaxLength = 255
      Me.Validator.SetMinValue(Me.txtEQName, "")
      Me.txtEQName.Name = "txtEQName"
      Me.Validator.SetRegularExpression(Me.txtEQName, "")
      Me.Validator.SetRequired(Me.txtEQName, True)
      Me.txtEQName.Size = New System.Drawing.Size(314, 21)
      Me.txtEQName.TabIndex = 2
      '
      'chkEqAutoRun
      '
      Me.chkEqAutoRun.Appearance = System.Windows.Forms.Appearance.Button
      Me.chkEqAutoRun.Image = CType(resources.GetObject("chkEqAutoRun.Image"), System.Drawing.Image)
      Me.chkEqAutoRun.Location = New System.Drawing.Point(265, 28)
      Me.chkEqAutoRun.Name = "chkEqAutoRun"
      Me.chkEqAutoRun.Size = New System.Drawing.Size(21, 21)
      Me.chkEqAutoRun.TabIndex = 335
      Me.chkEqAutoRun.TabStop = False
      '
      'lv
      '
      Me.lv.AllowSort = True
      Me.lv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.lv.FullRowSelect = True
      Me.lv.GridLines = True
      Me.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
      Me.lv.HideSelection = False
      Me.lv.Location = New System.Drawing.Point(562, 106)
      Me.lv.Name = "lv"
      Me.lv.Size = New System.Drawing.Size(424, 526)
      Me.lv.SortIndex = -1
      Me.lv.SortOrder = System.Windows.Forms.SortOrder.None
      Me.lv.TabIndex = 4
      Me.lv.UseCompatibleStateImageBehavior = False
      Me.lv.View = System.Windows.Forms.View.Details
      '
      'Grbeqi
      '
      Me.Grbeqi.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.Grbeqi.Controls.Add(Me.ibtnDel)
      Me.Grbeqi.Controls.Add(Me.ibtnNewLot)
      Me.Grbeqi.Controls.Add(Me.ibtnSave)
      Me.Grbeqi.Controls.Add(Me.btnPurchaseFind)
      Me.Grbeqi.Controls.Add(Me.txtReference)
      Me.Grbeqi.Controls.Add(Me.lblReference)
      Me.Grbeqi.Controls.Add(Me.lblLicenseNo)
      Me.Grbeqi.Controls.Add(Me.lblModel)
      Me.Grbeqi.Controls.Add(Me.txtAssetName)
      Me.Grbeqi.Controls.Add(Me.lblSerailNo)
      Me.Grbeqi.Controls.Add(Me.TxtBuyDocDate)
      Me.Grbeqi.Controls.Add(Me.txtDescription)
      Me.Grbeqi.Controls.Add(Me.btnLoadImage)
      Me.Grbeqi.Controls.Add(Me.txtSerialNumber)
      Me.Grbeqi.Controls.Add(Me.lblCostCentername)
      Me.Grbeqi.Controls.Add(Me.lblDescription)
      Me.Grbeqi.Controls.Add(Me.btnClearImage)
      Me.Grbeqi.Controls.Add(Me.txtModel)
      Me.Grbeqi.Controls.Add(Me.lblBuycost)
      Me.Grbeqi.Controls.Add(Me.txtlEQIlicense)
      Me.Grbeqi.Controls.Add(Me.lblPicSize)
      Me.Grbeqi.Controls.Add(Me.lblRefDocDate)
      Me.Grbeqi.Controls.Add(Me.txtCostcenterCode)
      Me.Grbeqi.Controls.Add(Me.txtEQIbuydoccode)
      Me.Grbeqi.Controls.Add(Me.lblFairPriceUnit)
      Me.Grbeqi.Controls.Add(Me.txtCostCenterName)
      Me.Grbeqi.Controls.Add(Me.txtRentalRate)
      Me.Grbeqi.Controls.Add(Me.picImage)
      Me.Grbeqi.Controls.Add(Me.lblRefDoc)
      Me.Grbeqi.Controls.Add(Me.cmbCode)
      Me.Grbeqi.Controls.Add(Me.ibtnShowcostcenter)
      Me.Grbeqi.Controls.Add(Me.chkAutorun)
      Me.Grbeqi.Controls.Add(Me.TextEQIBuycost)
      Me.Grbeqi.Controls.Add(Me.lblEQTCode)
      Me.Grbeqi.Controls.Add(Me.ibtnCostcenterDialog)
      Me.Grbeqi.Controls.Add(Me.txtStatus)
      Me.Grbeqi.Controls.Add(Me.txtCostcenterAddress)
      Me.Grbeqi.Controls.Add(Me.txtUnit)
      Me.Grbeqi.Controls.Add(Me.lblCostcenterAddress)
      Me.Grbeqi.Controls.Add(Me.lblAsset)
      Me.Grbeqi.Controls.Add(Me.lblStatus)
      Me.Grbeqi.Controls.Add(Me.lblunit)
      Me.Grbeqi.Controls.Add(Me.lblName)
      Me.Grbeqi.Controls.Add(Me.btnAssetFind)
      Me.Grbeqi.Controls.Add(Me.txtEQIName)
      Me.Grbeqi.Controls.Add(Me.ibtnShowUnit1)
      Me.Grbeqi.Controls.Add(Me.lblRentalRate)
      Me.Grbeqi.Controls.Add(Me.txtAssetCode)
      Me.Grbeqi.Controls.Add(Me.txtUnitCode)
      Me.Grbeqi.Controls.Add(Me.ibtnShowUnitDialog1)
      Me.Grbeqi.Location = New System.Drawing.Point(6, 99)
      Me.Grbeqi.Name = "Grbeqi"
      Me.Grbeqi.Size = New System.Drawing.Size(550, 534)
      Me.Grbeqi.TabIndex = 3
      Me.Grbeqi.TabStop = False
      Me.Grbeqi.Text = "��������´����ͧ�ѡ���µ��"
      '
      'ibtnDel
      '
      Me.ibtnDel.ForeColor = System.Drawing.SystemColors.WindowText
      Me.ibtnDel.Location = New System.Drawing.Point(283, 445)
      Me.ibtnDel.Name = "ibtnDel"
      Me.ibtnDel.Size = New System.Drawing.Size(80, 29)
      Me.ibtnDel.TabIndex = 352
      Me.ibtnDel.Text = "ź"
      Me.ibtnDel.UseVisualStyleBackColor = True
      '
      'ibtnNewLot
      '
      Me.ibtnNewLot.ForeColor = System.Drawing.SystemColors.WindowText
      Me.ibtnNewLot.Location = New System.Drawing.Point(119, 445)
      Me.ibtnNewLot.Name = "ibtnNewLot"
      Me.ibtnNewLot.Size = New System.Drawing.Size(80, 29)
      Me.ibtnNewLot.TabIndex = 353
      Me.ibtnNewLot.Text = "���� "
      Me.ibtnNewLot.UseVisualStyleBackColor = True
      '
      'ibtnSave
      '
      Me.ibtnSave.ForeColor = System.Drawing.SystemColors.WindowText
      Me.ibtnSave.Location = New System.Drawing.Point(201, 445)
      Me.ibtnSave.Name = "ibtnSave"
      Me.ibtnSave.Size = New System.Drawing.Size(80, 29)
      Me.ibtnSave.TabIndex = 16
      Me.ibtnSave.Text = "�ѹ�֡"
      Me.ibtnSave.UseVisualStyleBackColor = True
      '
      'btnPurchaseFind
      '
      Me.btnPurchaseFind.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnPurchaseFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnPurchaseFind.ForeColor = System.Drawing.SystemColors.Control
      Me.btnPurchaseFind.Location = New System.Drawing.Point(233, 114)
      Me.btnPurchaseFind.Name = "btnPurchaseFind"
      Me.btnPurchaseFind.Size = New System.Drawing.Size(24, 23)
      Me.btnPurchaseFind.TabIndex = 21
      Me.btnPurchaseFind.TabStop = False
      Me.btnPurchaseFind.ThemedImage = CType(resources.GetObject("btnPurchaseFind.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtReference
      '
      Me.Validator.SetDataType(Me.txtReference, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtReference, "")
      Me.Validator.SetGotFocusBackColor(Me.txtReference, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtReference, System.Drawing.Color.Empty)
      Me.txtReference.Location = New System.Drawing.Point(120, 408)
      Me.Validator.SetMinValue(Me.txtReference, "")
      Me.txtReference.Name = "txtReference"
      Me.txtReference.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtReference, "")
      Me.Validator.SetRequired(Me.txtReference, False)
      Me.txtReference.Size = New System.Drawing.Size(111, 21)
      Me.txtReference.TabIndex = 15
      '
      'lblReference
      '
      Me.lblReference.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblReference.ForeColor = System.Drawing.Color.Black
      Me.lblReference.Location = New System.Drawing.Point(7, 408)
      Me.lblReference.Name = "lblReference"
      Me.lblReference.Size = New System.Drawing.Size(112, 18)
      Me.lblReference.TabIndex = 349
      Me.lblReference.Text = "Reference :"
      Me.lblReference.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblLicenseNo
      '
      Me.lblLicenseNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblLicenseNo.ForeColor = System.Drawing.Color.Black
      Me.lblLicenseNo.Location = New System.Drawing.Point(6, 237)
      Me.lblLicenseNo.Name = "lblLicenseNo"
      Me.lblLicenseNo.Size = New System.Drawing.Size(112, 18)
      Me.lblLicenseNo.TabIndex = 4
      Me.lblLicenseNo.Text = "license No. :"
      Me.lblLicenseNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblModel
      '
      Me.lblModel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblModel.ForeColor = System.Drawing.Color.Black
      Me.lblModel.Location = New System.Drawing.Point(6, 261)
      Me.lblModel.Name = "lblModel"
      Me.lblModel.Size = New System.Drawing.Size(112, 18)
      Me.lblModel.TabIndex = 4
      Me.lblModel.Text = "Model :"
      Me.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtAssetName
      '
      Me.Validator.SetDataType(Me.txtAssetName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtAssetName, "")
      Me.txtAssetName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtAssetName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtAssetName, System.Drawing.Color.Empty)
      Me.txtAssetName.Location = New System.Drawing.Point(233, 91)
      Me.Validator.SetMinValue(Me.txtAssetName, "")
      Me.txtAssetName.Name = "txtAssetName"
      Me.txtAssetName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtAssetName, "")
      Me.Validator.SetRequired(Me.txtAssetName, False)
      Me.txtAssetName.Size = New System.Drawing.Size(116, 21)
      Me.txtAssetName.TabIndex = 339
      Me.txtAssetName.TabStop = False
      '
      'lblSerailNo
      '
      Me.lblSerailNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblSerailNo.ForeColor = System.Drawing.Color.Black
      Me.lblSerailNo.Location = New System.Drawing.Point(6, 282)
      Me.lblSerailNo.Name = "lblSerailNo"
      Me.lblSerailNo.Size = New System.Drawing.Size(112, 18)
      Me.lblSerailNo.TabIndex = 4
      Me.lblSerailNo.Text = "Serail Number :"
      Me.lblSerailNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'TxtBuyDocDate
      '
      Me.Validator.SetDataType(Me.TxtBuyDocDate, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DateTimeType)
      Me.Validator.SetDisplayName(Me.TxtBuyDocDate, "")
      Me.Validator.SetGotFocusBackColor(Me.TxtBuyDocDate, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.TxtBuyDocDate, System.Drawing.Color.Empty)
      Me.TxtBuyDocDate.Location = New System.Drawing.Point(120, 139)
      Me.Validator.SetMinValue(Me.TxtBuyDocDate, "")
      Me.TxtBuyDocDate.Name = "TxtBuyDocDate"
      Me.TxtBuyDocDate.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.TxtBuyDocDate, "")
      Me.Validator.SetRequired(Me.TxtBuyDocDate, False)
      Me.TxtBuyDocDate.Size = New System.Drawing.Size(112, 21)
      Me.TxtBuyDocDate.TabIndex = 5
      '
      'txtDescription
      '
      Me.Validator.SetDataType(Me.txtDescription, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtDescription, "")
      Me.txtDescription.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtDescription, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtDescription, -15)
      Me.Validator.SetInvalidBackColor(Me.txtDescription, System.Drawing.Color.Empty)
      Me.txtDescription.Location = New System.Drawing.Point(120, 307)
      Me.txtDescription.MaxLength = 255
      Me.Validator.SetMinValue(Me.txtDescription, "")
      Me.txtDescription.Multiline = True
      Me.txtDescription.Name = "txtDescription"
      Me.Validator.SetRegularExpression(Me.txtDescription, "")
      Me.Validator.SetRequired(Me.txtDescription, False)
      Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
      Me.txtDescription.Size = New System.Drawing.Size(230, 47)
      Me.txtDescription.TabIndex = 12
      '
      'btnLoadImage
      '
      Me.btnLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnLoadImage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnLoadImage.ForeColor = System.Drawing.SystemColors.Control
      Me.btnLoadImage.Location = New System.Drawing.Point(495, 148)
      Me.btnLoadImage.Name = "btnLoadImage"
      Me.btnLoadImage.Size = New System.Drawing.Size(24, 23)
      Me.btnLoadImage.TabIndex = 25
      Me.btnLoadImage.TabStop = False
      Me.btnLoadImage.ThemedImage = CType(resources.GetObject("btnLoadImage.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtSerialNumber
      '
      Me.Validator.SetDataType(Me.txtSerialNumber, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtSerialNumber, "")
      Me.Validator.SetGotFocusBackColor(Me.txtSerialNumber, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtSerialNumber, System.Drawing.Color.Empty)
      Me.txtSerialNumber.Location = New System.Drawing.Point(120, 283)
      Me.Validator.SetMinValue(Me.txtSerialNumber, "")
      Me.txtSerialNumber.Name = "txtSerialNumber"
      Me.Validator.SetRegularExpression(Me.txtSerialNumber, "")
      Me.Validator.SetRequired(Me.txtSerialNumber, False)
      Me.txtSerialNumber.Size = New System.Drawing.Size(112, 21)
      Me.txtSerialNumber.TabIndex = 11
      '
      'lblCostCentername
      '
      Me.lblCostCentername.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCostCentername.ForeColor = System.Drawing.Color.Black
      Me.lblCostCentername.Location = New System.Drawing.Point(6, 69)
      Me.lblCostCentername.Name = "lblCostCentername"
      Me.lblCostCentername.Size = New System.Drawing.Size(112, 18)
      Me.lblCostCentername.TabIndex = 21
      Me.lblCostCentername.Text = "Cost center ��Ңͧ :"
      Me.lblCostCentername.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblDescription
      '
      Me.lblDescription.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblDescription.ForeColor = System.Drawing.Color.Black
      Me.lblDescription.Location = New System.Drawing.Point(6, 320)
      Me.lblDescription.Name = "lblDescription"
      Me.lblDescription.Size = New System.Drawing.Size(112, 18)
      Me.lblDescription.TabIndex = 5
      Me.lblDescription.Text = "Description :"
      Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'btnClearImage
      '
      Me.btnClearImage.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnClearImage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnClearImage.Location = New System.Drawing.Point(520, 148)
      Me.btnClearImage.Name = "btnClearImage"
      Me.btnClearImage.Size = New System.Drawing.Size(24, 23)
      Me.btnClearImage.TabIndex = 26
      Me.btnClearImage.TabStop = False
      Me.btnClearImage.ThemedImage = CType(resources.GetObject("btnClearImage.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtModel
      '
      Me.Validator.SetDataType(Me.txtModel, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtModel, "")
      Me.Validator.SetGotFocusBackColor(Me.txtModel, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtModel, System.Drawing.Color.Empty)
      Me.txtModel.Location = New System.Drawing.Point(120, 259)
      Me.Validator.SetMinValue(Me.txtModel, "")
      Me.txtModel.Name = "txtModel"
      Me.Validator.SetRegularExpression(Me.txtModel, "")
      Me.Validator.SetRequired(Me.txtModel, False)
      Me.txtModel.Size = New System.Drawing.Size(112, 21)
      Me.txtModel.TabIndex = 10
      '
      'lblBuycost
      '
      Me.lblBuycost.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblBuycost.ForeColor = System.Drawing.Color.Black
      Me.lblBuycost.Location = New System.Drawing.Point(6, 163)
      Me.lblBuycost.Name = "lblBuycost"
      Me.lblBuycost.Size = New System.Drawing.Size(112, 18)
      Me.lblBuycost.TabIndex = 4
      Me.lblBuycost.Text = "��Ť�ҫ��� :"
      Me.lblBuycost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtlEQIlicense
      '
      Me.Validator.SetDataType(Me.txtlEQIlicense, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtlEQIlicense, "")
      Me.Validator.SetGotFocusBackColor(Me.txtlEQIlicense, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtlEQIlicense, System.Drawing.Color.Empty)
      Me.txtlEQIlicense.Location = New System.Drawing.Point(120, 235)
      Me.Validator.SetMinValue(Me.txtlEQIlicense, "")
      Me.txtlEQIlicense.Name = "txtlEQIlicense"
      Me.Validator.SetRegularExpression(Me.txtlEQIlicense, "")
      Me.Validator.SetRequired(Me.txtlEQIlicense, False)
      Me.txtlEQIlicense.Size = New System.Drawing.Size(112, 21)
      Me.txtlEQIlicense.TabIndex = 9
      '
      'lblPicSize
      '
      Me.lblPicSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.lblPicSize.Location = New System.Drawing.Point(426, 64)
      Me.lblPicSize.Name = "lblPicSize"
      Me.lblPicSize.Size = New System.Drawing.Size(100, 23)
      Me.lblPicSize.TabIndex = 206
      Me.lblPicSize.Text = "120 X 120 pixel"
      Me.lblPicSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'lblRefDocDate
      '
      Me.lblRefDocDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblRefDocDate.ForeColor = System.Drawing.Color.Black
      Me.lblRefDocDate.Location = New System.Drawing.Point(6, 140)
      Me.lblRefDocDate.Name = "lblRefDocDate"
      Me.lblRefDocDate.Size = New System.Drawing.Size(112, 18)
      Me.lblRefDocDate.TabIndex = 5
      Me.lblRefDocDate.Text = "�ѹ��������͡��� :"
      Me.lblRefDocDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtCostcenterCode
      '
      Me.Validator.SetDataType(Me.txtCostcenterCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCostcenterCode, "")
      Me.txtCostcenterCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtCostcenterCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCostcenterCode, System.Drawing.Color.Empty)
      Me.txtCostcenterCode.Location = New System.Drawing.Point(120, 67)
      Me.txtCostcenterCode.MaxLength = 20
      Me.Validator.SetMinValue(Me.txtCostcenterCode, "")
      Me.txtCostcenterCode.Name = "txtCostcenterCode"
      Me.Validator.SetRegularExpression(Me.txtCostcenterCode, "")
      Me.Validator.SetRequired(Me.txtCostcenterCode, True)
      Me.txtCostcenterCode.Size = New System.Drawing.Size(112, 21)
      Me.txtCostcenterCode.TabIndex = 2
      '
      'txtEQIbuydoccode
      '
      Me.Validator.SetDataType(Me.txtEQIbuydoccode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtEQIbuydoccode, "")
      Me.txtEQIbuydoccode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtEQIbuydoccode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtEQIbuydoccode, System.Drawing.Color.Empty)
      Me.txtEQIbuydoccode.Location = New System.Drawing.Point(120, 115)
      Me.Validator.SetMinValue(Me.txtEQIbuydoccode, "")
      Me.txtEQIbuydoccode.Name = "txtEQIbuydoccode"
      Me.txtEQIbuydoccode.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtEQIbuydoccode, "")
      Me.Validator.SetRequired(Me.txtEQIbuydoccode, False)
      Me.txtEQIbuydoccode.Size = New System.Drawing.Size(112, 21)
      Me.txtEQIbuydoccode.TabIndex = 4
      '
      'lblFairPriceUnit
      '
      Me.lblFairPriceUnit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblFairPriceUnit.ForeColor = System.Drawing.Color.Black
      Me.lblFairPriceUnit.Location = New System.Drawing.Point(236, 189)
      Me.lblFairPriceUnit.Name = "lblFairPriceUnit"
      Me.lblFairPriceUnit.Size = New System.Drawing.Size(30, 18)
      Me.lblFairPriceUnit.TabIndex = 22
      Me.lblFairPriceUnit.Text = "�ҷ"
      Me.lblFairPriceUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'txtCostCenterName
      '
      Me.Validator.SetDataType(Me.txtCostCenterName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCostCenterName, "")
      Me.txtCostCenterName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtCostCenterName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCostCenterName, System.Drawing.Color.Empty)
      Me.txtCostCenterName.Location = New System.Drawing.Point(233, 67)
      Me.Validator.SetMinValue(Me.txtCostCenterName, "")
      Me.txtCostCenterName.Name = "txtCostCenterName"
      Me.txtCostCenterName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtCostCenterName, "")
      Me.Validator.SetRequired(Me.txtCostCenterName, False)
      Me.txtCostCenterName.Size = New System.Drawing.Size(116, 21)
      Me.txtCostCenterName.TabIndex = 23
      Me.txtCostCenterName.TabStop = False
      '
      'txtRentalRate
      '
      Me.Validator.SetDataType(Me.txtRentalRate, Longkong.Pojjaman.Gui.Components.DataTypeConstants.DecimalType)
      Me.Validator.SetDisplayName(Me.txtRentalRate, "")
      Me.txtRentalRate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtRentalRate, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtRentalRate, System.Drawing.Color.Empty)
      Me.txtRentalRate.Location = New System.Drawing.Point(120, 187)
      Me.Validator.SetMinValue(Me.txtRentalRate, "0")
      Me.txtRentalRate.Name = "txtRentalRate"
      Me.Validator.SetRegularExpression(Me.txtRentalRate, "")
      Me.Validator.SetRequired(Me.txtRentalRate, True)
      Me.txtRentalRate.Size = New System.Drawing.Size(112, 21)
      Me.txtRentalRate.TabIndex = 7
      Me.txtRentalRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'picImage
      '
      Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.picImage.Location = New System.Drawing.Point(400, 15)
      Me.picImage.Name = "picImage"
      Me.picImage.Size = New System.Drawing.Size(143, 129)
      Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
      Me.picImage.TabIndex = 203
      Me.picImage.TabStop = False
      '
      'lblRefDoc
      '
      Me.lblRefDoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblRefDoc.ForeColor = System.Drawing.Color.Black
      Me.lblRefDoc.Location = New System.Drawing.Point(6, 116)
      Me.lblRefDoc.Name = "lblRefDoc"
      Me.lblRefDoc.Size = New System.Drawing.Size(112, 18)
      Me.lblRefDoc.TabIndex = 3
      Me.lblRefDoc.Text = "�Ţ����͡��ë��� :"
      Me.lblRefDoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'cmbCode
      '
      Me.cmbCode.Location = New System.Drawing.Point(120, 21)
      Me.cmbCode.Name = "cmbCode"
      Me.cmbCode.Size = New System.Drawing.Size(139, 21)
      Me.cmbCode.TabIndex = 0
      '
      'ibtnShowcostcenter
      '
      Me.ibtnShowcostcenter.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ibtnShowcostcenter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.ibtnShowcostcenter.Location = New System.Drawing.Point(372, 67)
      Me.ibtnShowcostcenter.Name = "ibtnShowcostcenter"
      Me.ibtnShowcostcenter.Size = New System.Drawing.Size(24, 23)
      Me.ibtnShowcostcenter.TabIndex = 19
      Me.ibtnShowcostcenter.TabStop = False
      Me.ibtnShowcostcenter.ThemedImage = CType(resources.GetObject("ibtnShowcostcenter.ThemedImage"), System.Drawing.Bitmap)
      '
      'chkAutorun
      '
      Me.chkAutorun.Appearance = System.Windows.Forms.Appearance.Button
      Me.chkAutorun.Image = CType(resources.GetObject("chkAutorun.Image"), System.Drawing.Image)
      Me.chkAutorun.Location = New System.Drawing.Point(259, 20)
      Me.chkAutorun.Name = "chkAutorun"
      Me.chkAutorun.Size = New System.Drawing.Size(21, 21)
      Me.chkAutorun.TabIndex = 17
      Me.chkAutorun.TabStop = False
      '
      'TextEQIBuycost
      '
      Me.Validator.SetDataType(Me.TextEQIBuycost, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.TextEQIBuycost, "")
      Me.Validator.SetGotFocusBackColor(Me.TextEQIBuycost, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.TextEQIBuycost, System.Drawing.Color.Empty)
      Me.TextEQIBuycost.Location = New System.Drawing.Point(120, 163)
      Me.Validator.SetMinValue(Me.TextEQIBuycost, "")
      Me.TextEQIBuycost.Name = "TextEQIBuycost"
      Me.TextEQIBuycost.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.TextEQIBuycost, "")
      Me.Validator.SetRequired(Me.TextEQIBuycost, False)
      Me.TextEQIBuycost.Size = New System.Drawing.Size(112, 21)
      Me.TextEQIBuycost.TabIndex = 6
      Me.TextEQIBuycost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'lblEQTCode
      '
      Me.lblEQTCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblEQTCode.ForeColor = System.Drawing.Color.Black
      Me.lblEQTCode.Location = New System.Drawing.Point(6, 20)
      Me.lblEQTCode.Name = "lblEQTCode"
      Me.lblEQTCode.Size = New System.Drawing.Size(112, 18)
      Me.lblEQTCode.TabIndex = 0
      Me.lblEQTCode.Text = "������µ�� :"
      Me.lblEQTCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'ibtnCostcenterDialog
      '
      Me.ibtnCostcenterDialog.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ibtnCostcenterDialog.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.ibtnCostcenterDialog.ForeColor = System.Drawing.SystemColors.Control
      Me.ibtnCostcenterDialog.Location = New System.Drawing.Point(349, 67)
      Me.ibtnCostcenterDialog.Name = "ibtnCostcenterDialog"
      Me.ibtnCostcenterDialog.Size = New System.Drawing.Size(24, 23)
      Me.ibtnCostcenterDialog.TabIndex = 18
      Me.ibtnCostcenterDialog.TabStop = False
      Me.ibtnCostcenterDialog.ThemedImage = CType(resources.GetObject("ibtnCostcenterDialog.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtStatus
      '
      Me.Validator.SetDataType(Me.txtStatus, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtStatus, "")
      Me.Validator.SetGotFocusBackColor(Me.txtStatus, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtStatus, System.Drawing.Color.Empty)
      Me.txtStatus.Location = New System.Drawing.Point(120, 381)
      Me.Validator.SetMinValue(Me.txtStatus, "")
      Me.txtStatus.Name = "txtStatus"
      Me.txtStatus.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtStatus, "")
      Me.Validator.SetRequired(Me.txtStatus, False)
      Me.txtStatus.Size = New System.Drawing.Size(112, 21)
      Me.txtStatus.TabIndex = 14
      '
      'txtCostcenterAddress
      '
      Me.Validator.SetDataType(Me.txtCostcenterAddress, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCostcenterAddress, "")
      Me.txtCostcenterAddress.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtCostcenterAddress, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCostcenterAddress, System.Drawing.Color.Empty)
      Me.txtCostcenterAddress.Location = New System.Drawing.Point(120, 357)
      Me.txtCostcenterAddress.MaxLength = 255
      Me.Validator.SetMinValue(Me.txtCostcenterAddress, "")
      Me.txtCostcenterAddress.Name = "txtCostcenterAddress"
      Me.txtCostcenterAddress.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtCostcenterAddress, "")
      Me.Validator.SetRequired(Me.txtCostcenterAddress, False)
      Me.txtCostcenterAddress.Size = New System.Drawing.Size(230, 21)
      Me.txtCostcenterAddress.TabIndex = 13
      '
      'txtUnit
      '
      Me.Validator.SetDataType(Me.txtUnit, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtUnit, "")
      Me.txtUnit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtUnit, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtUnit, System.Drawing.Color.Empty)
      Me.txtUnit.Location = New System.Drawing.Point(233, 211)
      Me.Validator.SetMinValue(Me.txtUnit, "")
      Me.txtUnit.Name = "txtUnit"
      Me.txtUnit.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtUnit, "")
      Me.Validator.SetRequired(Me.txtUnit, False)
      Me.txtUnit.Size = New System.Drawing.Size(116, 21)
      Me.txtUnit.TabIndex = 339
      Me.txtUnit.TabStop = False
      '
      'lblCostcenterAddress
      '
      Me.lblCostcenterAddress.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCostcenterAddress.ForeColor = System.Drawing.Color.Black
      Me.lblCostcenterAddress.Location = New System.Drawing.Point(6, 359)
      Me.lblCostcenterAddress.Name = "lblCostcenterAddress"
      Me.lblCostcenterAddress.Size = New System.Drawing.Size(112, 18)
      Me.lblCostcenterAddress.TabIndex = 40
      Me.lblCostcenterAddress.Text = "Cost center ������� :"
      Me.lblCostcenterAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblAsset
      '
      Me.lblAsset.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblAsset.ForeColor = System.Drawing.Color.Black
      Me.lblAsset.Location = New System.Drawing.Point(5, 93)
      Me.lblAsset.Name = "lblAsset"
      Me.lblAsset.Size = New System.Drawing.Size(112, 18)
      Me.lblAsset.TabIndex = 5
      Me.lblAsset.Text = "�Թ��Ѿ�� :"
      Me.lblAsset.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblStatus
      '
      Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblStatus.ForeColor = System.Drawing.Color.Black
      Me.lblStatus.Location = New System.Drawing.Point(6, 384)
      Me.lblStatus.Name = "lblStatus"
      Me.lblStatus.Size = New System.Drawing.Size(112, 18)
      Me.lblStatus.TabIndex = 4
      Me.lblStatus.Text = "ʶҹ� :"
      Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblunit
      '
      Me.lblunit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblunit.ForeColor = System.Drawing.Color.Black
      Me.lblunit.Location = New System.Drawing.Point(6, 212)
      Me.lblunit.Name = "lblunit"
      Me.lblunit.Size = New System.Drawing.Size(112, 18)
      Me.lblunit.TabIndex = 5
      Me.lblunit.Text = "Unit :"
      Me.lblunit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblName
      '
      Me.lblName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblName.ForeColor = System.Drawing.Color.Black
      Me.lblName.Location = New System.Drawing.Point(6, 46)
      Me.lblName.Name = "lblName"
      Me.lblName.Size = New System.Drawing.Size(112, 18)
      Me.lblName.TabIndex = 4
      Me.lblName.Text = "���� :"
      Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'btnAssetFind
      '
      Me.btnAssetFind.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnAssetFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnAssetFind.ForeColor = System.Drawing.SystemColors.Control
      Me.btnAssetFind.Location = New System.Drawing.Point(349, 91)
      Me.btnAssetFind.Name = "btnAssetFind"
      Me.btnAssetFind.Size = New System.Drawing.Size(24, 23)
      Me.btnAssetFind.TabIndex = 20
      Me.btnAssetFind.TabStop = False
      Me.btnAssetFind.ThemedImage = CType(resources.GetObject("btnAssetFind.ThemedImage"), System.Drawing.Bitmap)
      '
      'txtEQIName
      '
      Me.Validator.SetDataType(Me.txtEQIName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtEQIName, "")
      Me.txtEQIName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtEQIName, System.Drawing.Color.Empty)
      Me.ErrorProvider1.SetIconPadding(Me.txtEQIName, -15)
      Me.Validator.SetInvalidBackColor(Me.txtEQIName, System.Drawing.Color.Empty)
      Me.txtEQIName.Location = New System.Drawing.Point(120, 44)
      Me.txtEQIName.MaxLength = 255
      Me.Validator.SetMinValue(Me.txtEQIName, "")
      Me.txtEQIName.Name = "txtEQIName"
      Me.Validator.SetRegularExpression(Me.txtEQIName, "")
      Me.Validator.SetRequired(Me.txtEQIName, True)
      Me.txtEQIName.Size = New System.Drawing.Size(230, 21)
      Me.txtEQIName.TabIndex = 1
      '
      'ibtnShowUnit1
      '
      Me.ibtnShowUnit1.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ibtnShowUnit1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.ibtnShowUnit1.Location = New System.Drawing.Point(372, 210)
      Me.ibtnShowUnit1.Name = "ibtnShowUnit1"
      Me.ibtnShowUnit1.Size = New System.Drawing.Size(24, 23)
      Me.ibtnShowUnit1.TabIndex = 24
      Me.ibtnShowUnit1.TabStop = False
      Me.ibtnShowUnit1.ThemedImage = CType(resources.GetObject("ibtnShowUnit1.ThemedImage"), System.Drawing.Bitmap)
      '
      'lblRentalRate
      '
      Me.lblRentalRate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblRentalRate.ForeColor = System.Drawing.Color.Black
      Me.lblRentalRate.Location = New System.Drawing.Point(6, 189)
      Me.lblRentalRate.Name = "lblRentalRate"
      Me.lblRentalRate.Size = New System.Drawing.Size(112, 18)
      Me.lblRentalRate.TabIndex = 5
      Me.lblRentalRate.Text = "Rental Rate :"
      Me.lblRentalRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtAssetCode
      '
      Me.Validator.SetDataType(Me.txtAssetCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtAssetCode, "")
      Me.txtAssetCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtAssetCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtAssetCode, System.Drawing.Color.Empty)
      Me.txtAssetCode.Location = New System.Drawing.Point(120, 91)
      Me.Validator.SetMinValue(Me.txtAssetCode, "")
      Me.txtAssetCode.Name = "txtAssetCode"
      Me.Validator.SetRegularExpression(Me.txtAssetCode, "")
      Me.Validator.SetRequired(Me.txtAssetCode, False)
      Me.txtAssetCode.Size = New System.Drawing.Size(112, 21)
      Me.txtAssetCode.TabIndex = 3
      '
      'txtUnitCode
      '
      Me.Validator.SetDataType(Me.txtUnitCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtUnitCode, "")
      Me.txtUnitCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtUnitCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtUnitCode, System.Drawing.Color.Empty)
      Me.txtUnitCode.Location = New System.Drawing.Point(120, 211)
      Me.Validator.SetMinValue(Me.txtUnitCode, "")
      Me.txtUnitCode.Name = "txtUnitCode"
      Me.Validator.SetRegularExpression(Me.txtUnitCode, "")
      Me.Validator.SetRequired(Me.txtUnitCode, False)
      Me.txtUnitCode.Size = New System.Drawing.Size(112, 21)
      Me.txtUnitCode.TabIndex = 8
      '
      'ibtnShowUnitDialog1
      '
      Me.ibtnShowUnitDialog1.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ibtnShowUnitDialog1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.ibtnShowUnitDialog1.ForeColor = System.Drawing.SystemColors.Control
      Me.ibtnShowUnitDialog1.Location = New System.Drawing.Point(349, 210)
      Me.ibtnShowUnitDialog1.Name = "ibtnShowUnitDialog1"
      Me.ibtnShowUnitDialog1.Size = New System.Drawing.Size(24, 23)
      Me.ibtnShowUnitDialog1.TabIndex = 23
      Me.ibtnShowUnitDialog1.TabStop = False
      Me.ibtnShowUnitDialog1.ThemedImage = CType(resources.GetObject("ibtnShowUnitDialog1.ThemedImage"), System.Drawing.Bitmap)
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
      'EquipmentDetailView
      '
      Me.Controls.Add(Me.grbDetail)
      Me.Name = "EquipmentDetailView"
      Me.Size = New System.Drawing.Size(1010, 650)
      Me.grbDetail.ResumeLayout(False)
      Me.grbDetail.PerformLayout()
      Me.Grbeqi.ResumeLayout(False)
      Me.Grbeqi.PerformLayout()
      CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)

    End Sub

#End Region

#Region " SetLabelText "
    Public Overrides Sub SetLabelText()
      If Not m_entity Is Nothing Then Me.Text = Me.StringParserService.Parse(Me.m_entity.TabPageText)
      'Me.lblCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblCode}")
      'Me.Validator.SetDisplayName(Me.txtCode, StringHelper.GetRidOfAtEnd(Me.lblCode.Text, ":"))

      'Me.lblName.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblName}")
      'Me.Validator.SetDisplayName(Me.txtEQIName, StringHelper.GetRidOfAtEnd(Me.lblName.Text, ":"))

      'Me.lblDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblDetail}")
      'Me.Validator.SetDisplayName(Me.txtDetail, StringHelper.GetRidOfAtEnd(Me.lblDetail.Text, ":"))

      'Me.Validator.SetDisplayName(Me.txtRentalUnitCode, StringHelper.GetRidOfAtEnd(Me.lblRentalunit.Text, ":"))
      'Me.lblGl.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblGl}")
      'Me.Validator.SetDisplayName(Me.txtGLCode, StringHelper.GetRidOfAtEnd(Me.lblGl.Text, ":"))


      'Me.lblDepreOpeningAcct.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblDepreOpeningAcct}")
      'Me.Validator.SetDisplayName(Me.txtDepreOpeningAcctCode, StringHelper.GetRidOfAtEnd(Me.lblDepreOpeningAcct.Text, ":"))

      'Me.lblDepreAcct.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblDepreAcct}")
      'Me.Validator.SetDisplayName(Me.txtDepreAcctCode, StringHelper.GetRidOfAtEnd(Me.lblDepreAcct.Text, ":"))

      'Me.lblType.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblType}")
      'Me.Validator.SetDisplayName(Me.txtTypeCode, StringHelper.GetRidOfAtEnd(Me.lblType.Text, ":"))

      'Me.lblCostcenter.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblCostcenter}")
      'Me.Validator.SetDisplayName(Me.txtCostcenterCode, StringHelper.GetRidOfAtEnd(Me.lblCostcenter.Text, ":"))

      'Me.lblLocation.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblLocation}")
      'Me.Validator.SetDisplayName(Me.txtLocation, StringHelper.GetRidOfAtEnd(Me.lblLocation.Text, ":"))

      'Me.lblCalcRate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblCalcRate}")
      'Me.Validator.SetDisplayName(Me.txtCalcRate, StringHelper.GetRidOfAtEnd(Me.lblCalcRate.Text, ":"))

      'Me.lblEndCalcDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblEndCalcDate}")
      'Me.Validator.SetDisplayName(Me.txtEndCalcDate, StringHelper.GetRidOfAtEnd(Me.lblEndCalcDate.Text, ":"))

      'Me.lblAge.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblAge}")
      'Me.Validator.SetDisplayName(Me.txtAge, StringHelper.GetRidOfAtEnd(Me.lblAge.Text, ":"))

      'Me.lblStartCalcAmnt.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblStartCalcAmnt}")
      'Me.Validator.SetDisplayName(Me.txtStartCalcAmt, StringHelper.GetRidOfAtEnd(Me.lblStartCalcAmnt.Text, ":"))


      'Me.lblCalcType.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblCalcType}")
      'Me.Validator.SetDisplayName(Me.txtCalcRate, StringHelper.GetRidOfAtEnd(Me.lblCalcType.Text, ":"))



      'Me.lblAge.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblAge}")
      'Me.Validator.SetDisplayName(txtAge, lblAge.Text)

      ''Me.lblRent.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblRent}")
      ''Me.Validator.SetDisplayName(txtRent, lblRent.Text)
      ''Me.lblDateInval.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblDateInval}")

      'Me.lblStartCalcDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblStartCalcDate}")
      'Me.Validator.SetDisplayName(txtStartCalcDate, lblStartCalcDate.Text)

      'Me.lblBuyPrice.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblBuyPrice}")
      'Me.Validator.SetDisplayName(txtBuyPrice, lblBuyPrice.Text)

      'Me.lblBuyDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblBuyDate}")
      'Me.Validator.SetDisplayName(TxtBuyDocDate, "�ѹ������١��ͧ")

      'Me.lblTransferDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblTransferDate}")
      'Me.Validator.SetDisplayName(txtTransferDate, lblTransferDate.Text)

      'Me.lblBuyFrom.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblBuyFrom}")
      'Me.Validator.SetDisplayName(txtBuyFrom, lblBuyFrom.Text)

      'Me.lblBuyDocCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblBuyDocCode}")
      'Me.Validator.SetDisplayName(txtBuyDocCode, lblBuyDocCode.Text)

      'Me.lblBuyDocDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblBuyDocDate}")
      'Me.Validator.SetDisplayName(txtBuyDocDate, lblBuyDocDate.Text)

      'Me.lblYear.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblYear}")
      'Me.lblYear1.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblYear1}")

      'Me.lblSavage.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblSavage}")
      'Me.Validator.SetDisplayName(txtSalvage, lblSavage.Text)

      'Me.lblRemainingValue.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblRemainingValue}")
      'Me.Validator.SetDisplayName(txtRemainingValue, lblRemainingValue.Text)

      'Me.lblDepreOpenning.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblDepreOpenning}")
      'Me.Validator.SetDisplayName(txtDepreOpenning, lblDepreOpenning.Text)

      'Me.lblNote.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblNote}")
      'Me.Validator.SetDisplayName(txtNote, lblNote.Text)

      'Me.ToolTip1.SetToolTip(Me.btnLoadImage, Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.btnLoadImage}"))
      'Me.ToolTip1.SetToolTip(Me.btnClearImage, Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.btnClearImage}"))
      'Me.btnAssetAuxDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.btnAssetAuxDetail}")
      'Me.btnAssetAuxDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.btnAssetAuxDetail}")

      'Me.lblCurrency1.Text = Me.StringParserService.Parse("${res:Global.CurrencyUnit}")
      'Me.lblCurrency2.Text = Me.StringParserService.Parse("${res:Global.CurrencyUnit}")
      'Me.lblCurrency3.Text = Me.StringParserService.Parse("${res:Global.CurrencyUnit}")
      'Me.lblCurrency4.Text = Me.StringParserService.Parse("${res:Global.CurrencyUnit}")
      'Me.lblCurrency5.Text = Me.StringParserService.Parse("${res:Global.CurrencyUnit}")

      'Me.lblAssetStatus.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.lblAssetStatus}")

      'Me.grbStatus.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.grbStatus}")
      'Me.grbDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.grbDetail}")
      'Me.grbCalcDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.grbCalcDetail}")
      'Me.grbBuyDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.grbBuyDetail}")

      'Me.chkCancel.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.chkCancel}")
      'Me.chkDecay.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.AssetDetailView.chkDecay}")

      grbDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.grbDetail}") '�����Ū�Դ����ͧ�ѡ�
      lblEquipmentCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblEquipmentCode}") '���ʪ�Դ����ͧ�ѡ� :
      lblEquipmentName.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblEquipmentName}") '��Դ����ͧ�ѡ� :

      Grbeqi.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.Grbeqi}") '��������´����ͧ�ѡ�
      lblEQTCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblEQTCode}") '��������ͧ�ѡ� : 
      lblName.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblName}") '��������ͧ�ѡ� : 
      lblCostCentername.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblCostCentername}") 'Cost Center : 
      lblRentalRate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblRentalRate}") 'Rental Rate : 
      lblunit.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblunit}") '˹��� : 
      lblAsset.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblAsset}") '�Թ��Ѿ�� : 
      lblRefDoc.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblRefDoc}") '�Ţ����͡��ë��� : 
      lblRefDocDate.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblRefDocDate}") '�ѹ����͡��ë��� : 

      lblBuycost.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblBuycost}") '��Ť�ҵ���͡��ë��� : 

      lblLicenseNo.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblLicenseNo}") 'License No. : 
      lblModel.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblModel}") 'Model : 
      lblSerailNo.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblSerailNo}") 'Serial No. : 
      lblDescription.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblDescription}") '��������´ : 

      lblCostcenterAddress.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblCostcenterAddress}") '������ Cost Center : 
      lblStatus.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblStatus}") 'ʶҹ�����ͧ�ѡ� : 
      lblReference.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.lblReference}") 'ʶҹж١��ҧ�ԧ : 

      Me.Validator.SetDisplayName(Me.txtEQIName, StringHelper.GetRidOfAtEnd(Me.lblName.Text, ":"))
      Me.Validator.SetDisplayName(Me.txtRentalRate, StringHelper.GetRidOfAtEnd(Me.lblRentalRate.Text, ":"))
      Me.Validator.SetDisplayName(Me.txtCostcenterCode, StringHelper.GetRidOfAtEnd(Me.lblCostCentername.Text, ":"))

    End Sub
#End Region

#Region "Member"
    Private m_entity As Equipment  'IHasEquipmentItemCollection 'EquipmentItem
    Private m_refDoc As IHasEquipment
    Private m_isInitialized As Boolean = False
    Private m_treeManager As TreeManager
    Private m_StringParserService As StringParserService

#End Region

#Region "Constructor"

    Public Sub New()
      MyBase.New()
      Try
        Me.InitializeComponent()
      Catch ex As Exception
        MessageBox.Show(ex.InnerException.ToString)
      End Try

      Me.Initialize()

      Me.EventWiring()
      Me.SetLabelText()

      Me.UpdateEntityProperties()

    End Sub

#End Region

#Region "Properties"
    Private ReadOnly Property CurrentTagItem() As EquipmentItem
      Get
        'If lv.SelectedItems.Count = 0 Then
        '  Return Nothing
        'End If
        'Return CType(lv.SelectedItems(0).Tag, EquipmentItem)

        'If lv.Items.Count > 0 Then
        '  Dim lvi As ListViewItem = lv.SelectedItems.
        '  If Not lvi.Tag Is Nothing Then
        '    If TypeOf lvi.Tag Is EquipmentItem Then
        '      Return CType(lvi.Tag, EquipmentItem)
        '    End If
        '  End If
        'End If

        'Return Nothing
        If Not Me.m_entity Is Nothing AndAlso Not Me.m_entity.EquipmentItem Is Nothing Then
          Return Me.m_entity.EquipmentItem
        Else
          If Me.m_entity.ItemCollection.Count > 0 Then
            Me.m_entity.EquipmentItem = Me.m_entity.ItemCollection(0)
            Return Me.m_entity.EquipmentItem
          End If
        End If
        Return Nothing
      End Get
      'Get
      '  Dim row As TreeRow = Me.m_treeManager.SelectedRow
      '  If row Is Nothing Then
      '    Return Nothing
      '  End If
      '  If Not TypeOf row.Tag Is EquipmentItem Then
      '    Return Nothing
      '  End If
      '  Return CType(row.Tag, EquipmentItem)
      'End Get
    End Property
#End Region

#Region "Method"
    Public Overrides Sub Initialize()
      SetLVHeader()
      ' ��˹���äӹǳ����������Ҥ�
      'AssetCalcType.ListCodeDescriptionInComboBox(cmbCalcType, "asset_calctype")
      ' ��˹��ѵ�Ҥ����Ҿ�鹰ҹ
    End Sub
    ' �ʴ���Ң�����ŧ� control ������躹�����
    Public Overloads Function MinDateToNull(ByVal dt As Date, ByVal nullString As String) As String
      If dt.Equals(Date.MinValue) Then
        Return nullString
      End If
      Return dt.ToShortDateString '��������׹���
      'Return dt.ToString("dd/MM/yyyy")  ' �˹�����Ф�Ѻ
    End Function
    Public Overloads Function MinDateToNow(ByVal dt As Date) As Date
      If dt.Equals(Date.MinValue) Then
        dt = Now
      End If
      Return dt
    End Function
    Public Overloads ReadOnly Property StringParserService() As StringParserService
      Get
        If m_StringParserService Is Nothing Then
          m_StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
        End If
        Return m_StringParserService
      End Get
    End Property
    Public Overrides Sub UpdateEntityProperties()
      If Me.m_entity Is Nothing Then
        Return
      End If
      Me.m_isInitialized = False

      'ClearDetail()

      Me.chkEqAutoRun.Checked = Me.m_entity.AutoGen
      Me.UpdateEqAutogenStatus()

      Me.RefreshTextData()
      Me.RefreshListViewData()

      'CmbEQCode.Text = m_entity.Code
      txtEQName.Text = Me.m_entity.Name

      'Trace.WriteLine(Me.m_entity.Name)

      SetLabelText()
      CheckFormEnable()
      Me.m_isInitialized = True
    End Sub
    Private Sub RefreshListViewData()
      If Me.m_entity Is Nothing Then
        Return
      End If
      Me.m_isInitialized = False

      Dim sequence As Integer = 0
      Dim eqitem As String = ""
      Dim asset As String = ""
      Dim cc As String = ""

      lv.Items.Clear()
      For Each eqi As EquipmentItem In Me.m_entity.ItemCollection

        sequence += 1

        Dim lvItem As New ListViewItem(sequence)

        eqitem = eqi.Code & ":" & eqi.Name
        lvItem.SubItems.Add(eqitem)

        If Not eqi.Asset Is Nothing Then
          asset = eqi.Asset.Code & ":" & eqi.Asset.Name
          lvItem.SubItems.Add(asset)
        Else
          lvItem.SubItems.Add("")
        End If

        If Not eqi.Costcenter Is Nothing Then
          cc = eqi.Costcenter.Code & ":" & eqi.Costcenter.Name
          lvItem.SubItems.Add(cc)
        Else
          lvItem.SubItems.Add("")
        End If
        If eqi.IsReferenced Then
          lvItem.SubItems.Add(Me.StringParserService.Parse("${res:Global.Referenced}"))
        Else
          lvItem.SubItems.Add("")
        End If

        lvItem.Tag = eqi
        lv.Items.Add(lvItem).Tag = eqi '�ջѭ�ҵ͹�Դ
      Next
      Me.m_isInitialized = True
    End Sub
    Private Sub RefreshTextData()
      If Not Me.m_entity.EquipmentItem Is Nothing Then
        Me.txtEQIbuydoccode.Text = ""
        Me.TxtBuyDocDate.Text = ""

        m_chkAutorunCheckChanged = True
        Me.chkAutorun.Checked = Me.m_entity.EquipmentItem.Autogen
        Me.UpdateAutogenStatus()
        m_chkAutorunCheckChanged = False

        If Not Me.m_entity.EquipmentItem.Asset Is Nothing Then
          Me.txtAssetCode.Text = Me.m_entity.EquipmentItem.Asset.Code
          Me.txtAssetName.Text = Me.m_entity.EquipmentItem.Asset.Name
        End If

        'If Me.m_entity.EquipmentItem.Buydoc.Code.Trim.Length > 0 Then
        '  Me.txtEQIbuydoccode.Text = Me.m_entity.EquipmentItem.Buydoc.Code
        '  Me.TxtBuyDocDate.Text = Me.m_entity.EquipmentItem.Buydate.ToShortDateString
        'End If

        If Not Me.m_entity.EquipmentItem.Buydoc Is Nothing Then
          Me.txtEQIbuydoccode.Text = Me.m_entity.EquipmentItem.Buydoc.Code
        Else
          Me.txtEQIbuydoccode.Text = ""
        End If
        If Not Me.m_entity.EquipmentItem.Buydate.Equals(Date.MinValue) Then
          Me.TxtBuyDocDate.Text = Me.m_entity.EquipmentItem.Buydate.ToShortDateString
        Else
          Me.TxtBuyDocDate.Text = ""
        End If

        Me.txtEQIName.Text = Me.m_entity.EquipmentItem.Name

        If Not Me.m_entity.EquipmentItem.Costcenter Is Nothing Then
          Me.txtCostcenterCode.Text = Me.m_entity.EquipmentItem.Costcenter.Code
          Me.txtCostCenterName.Text = Me.m_entity.EquipmentItem.Costcenter.Name
        End If

        Me.TextEQIBuycost.Text = Configuration.FormatToString(Me.m_entity.EquipmentItem.Buycost, DigitConfig.Price)
        Me.txtRentalRate.Text = Configuration.FormatToString(Me.m_entity.EquipmentItem.Rentalrate, DigitConfig.Price)

        Me.txtUnitCode.Text = Me.m_entity.EquipmentItem.Unit.Code
        Me.txtUnit.Text = Me.m_entity.EquipmentItem.Unit.Name
        Me.txtlEQIlicense.Text = Me.m_entity.EquipmentItem.License

        Me.txtModel.Text = Me.m_entity.EquipmentItem.Brand
        Me.txtSerialNumber.Text = Me.m_entity.EquipmentItem.Serailnumber

        Me.txtDescription.Text = Me.m_entity.EquipmentItem.Description

        If Not Me.m_entity.EquipmentItem.CurrentCostCenter Is Nothing Then
          Me.txtCostcenterAddress.Text = Me.m_entity.EquipmentItem.CurrentCostCenter.Code & ":" & Me.m_entity.EquipmentItem.CurrentCostCenter.Name
        End If

        Me.txtStatus.Text = Me.m_entity.EquipmentItem.CurrentStatus.Description

        If Me.m_entity.EquipmentItem.IsReferenced Then
          Me.txtReference.Text = Me.StringParserService.Parse("${res:Global.Referenced}")
        Else
          Me.txtReference.Text = ""
        End If

        picImage.Image = Me.m_entity.EquipmentItem.Image
        CheckLabelImgSize()

        'Me.txtToollotBuyQTY.Text = Configuration.FormatToString(Me.m_entity.ToolLot.Buyqty, DigitConfig.Price)
        'Me.txtToollotUnitCost.Text = Configuration.FormatToString(Me.m_entity.ToolLot.UnitCost, DigitConfig.Price)
        'Me.TxtToollotBuycost.Text = Configuration.FormatToString(Me.m_entity.ToolLot.Buycost, DigitConfig.Price)
        'Me.txtToollotWriteOff.Text = Configuration.FormatToString(Me.m_entity.ToolLot.WriteOff, DigitConfig.Price)

        'Me.txtToollotRemainQTY.Text = Configuration.FormatToString(Me.m_entity.ToolLot.RemainQTY, DigitConfig.Price)
        'Me.txtToollotRemainCost.Text = Configuration.FormatToString(Me.m_entity.ToolLot.RemainCost, DigitConfig.Price)

        'Me.TxtToollotbrand.Text = Me.m_entity.ToolLot.Brand
        'Me.txtDescription.Text = Me.m_entity.ToolLot.Description

        'If Me.m_entity.ToolLot.IsReferenced Then
        '  Me.txtReference.Text = Me.StringParserService.Parse("${res:Global.Referenced}")
        'End If

      End If

    End Sub
    Private Sub RefreshData()
      Me.m_isInitialized = False
      Dim eqitem As EquipmentItem = Me.CurrentTagItem
      Me.ClearItemOnly()


      If Not eqitem Is Nothing Then
        cmbCode.Text = eqitem.Code
        Me.m_oldCode = eqitem.Code
        Me.CurrentTagItem.oldcode = eqitem.Code
        Me.chkAutorun.Checked = eqitem.Autogen
        Me.UpdateAutogenStatus()

        Me.UpdateEqAutogenStatus()
        Me.CmbEQCode.Text = eqitem.equipment.Code

        If Not eqitem.equipment.Name Is Nothing Then
          Me.txtEQName.Text = eqitem.equipment.Name
        End If

        Me.txtEQIName.Text = eqitem.Name

        'If Not doc.Buydate.Equals(dtpBuyDocDate.Value) Then
        '  If Not m_dateSetting Then
        '    Me.TxtBuyDocDate.Text = MinDateToNull(dtpBuyDocDate.Value, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
        '    doc.Buydate = dtpBuyDocDate.Value.ToShortDateString
        '  End If


        If eqitem.Buycost <> 0 Then
          Me.TextEQIBuycost.Text = Configuration.FormatToString(eqitem.Buycost, DigitConfig.Price)
        Else
          Me.TextEQIBuycost.Text = ""
        End If

        Me.txtSerialNumber.Text = eqitem.Serailnumber
        Me.txtModel.Text = eqitem.Brand
        Me.txtlEQIlicense.Text = eqitem.License

        If Not eqitem.CurrentStatus Is Nothing Then
          Me.txtStatus.Text = eqitem.CurrentStatus.Description
        Else
          Me.txtStatus.Text = ""
        End If

        If eqitem.CurrentCostCenter.Code <> "" Then
          Me.txtCostcenterAddress.Text = eqitem.CurrentCostCenter.Code & " : " & eqitem.CurrentCostCenter.Name
        Else
          Me.txtCostcenterAddress.Text = ""
        End If

        'If eqitem.Buydoc IsNot Nothing Then
        '  txtEQIbuydoccode.Text = eqitem.Buydoc.Code
        'End If
        'If eqitem.Buydoc Is Nothing Then
        '  Me.txtEQIbuydoccode.Text = ""
        '  If Not MinDateToNull(eqitem.Buydate, "") = "" Then
        '    Me.TxtBuyDocDate.Text = eqitem.Buydate.ToShortDateString
        '  Else
        '    Me.TxtBuyDocDate.Text = ""
        '  End If
        'Else
        '  Me.txtEQIbuydoccode.Text = eqitem.Buydoc.Code
        '  If Not MinDateToNull(eqitem.Buydate, "") = "" Then
        '    Me.TxtBuyDocDate.Text = eqitem.Buydate.ToShortDateString
        '  Else
        '    Me.TxtBuyDocDate.Text = ""
        '  End If
        'End If

        If Not eqitem.Buydoc Is Nothing Then
          Me.txtEQIbuydoccode.Text = eqitem.Buydoc.Code
        Else
          Me.txtEQIbuydoccode.Text = ""
        End If
        If Not eqitem.Buydate.Equals(Date.MinValue) Then
          Me.TxtBuyDocDate.Text = eqitem.Buydate.ToShortDateString
        Else
          Me.TxtBuyDocDate.Text = ""
        End If

        'Me.TxtlastDateEdit.Text = MinDateToNull(eqitem.LastEditDate, Me.StringParserService.Parse(""))
        'Try
        '  Me.dtpLastEditDate.Value = eqitem.LastEditDate
        'Catch ex As Exception
        '  Me.dtpLastEditDate.Value = Now
        'End Try

        'If TxtBuyDocDate.Text = Date.Now Then
        ' Me.TxtBuyDocDate.Text = MinDateToNull(eqitem.Buydate, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
        'Try
        '  Me.dtpBuyDocDate.Value = eqitem.Buydate
        'Catch ex As Exception
        '  Me.dtpBuyDocDate.Value = Now
        'End Try
        'Else
        '  Me.TxtBuyDocDate.Text = "�ô�к�"
        'End If


        'dtpLastEditDate.Value = MinDateToNow(Me.m_entity.LastEditDate)
        Me.txtCostcenterCode.Text = eqitem.Costcenter.Code
        Me.txtCostCenterName.Text = eqitem.Costcenter.Name

        Me.txtAssetCode.Text = eqitem.Asset.Code
        Me.txtAssetName.Text = eqitem.Asset.Name

        Me.txtUnitCode.Text = eqitem.Unit.Code
        Me.txtUnit.Text = eqitem.Unit.Name
        'Me.txtRentalUnitCode.Text = eqitem.Rentalunit.Code
        'Me.txtRentalunit.Text = eqitem.Rentalunit.Name

        'TxtBuyDocDate.Text = MinDateToNull(eqitem.Buydate, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
        'dtpBuyDocDate.Value = MinDateToNow(eqitem.Buydate)
        If eqitem.Rentalrate <> 0 Then
          Me.txtRentalRate.Text = eqitem.Rentalrate
        Else
          Me.txtRentalRate.Text = ""
        End If

        Me.txtDescription.Text = eqitem.Description
        'picImage.Image = eqitem.Image
        'CheckLabelImgSize()
        'Dim lastEdited As String = ""
        'If Not eqitem.LastEditor Is Nothing Then
        '  lastEdited = "���ʼ���������ش : " & eqitem.LastEditor.Name
        'End If
        'lastEdited &= " �ѹ����������ش : " & eqitem.LastEditDate
        'Me.lblLasteditdate.Text = lastEdited.Trim
      End If
      Me.m_isInitialized = True
    End Sub

    Private Sub RefreshDocs()
      'Me.m_isInitialized = False
      ''Me.m_entity.ItemCollection.Populate(m_treeManager.Treetable)
      ''Me.m_treeManager.Treetable.AcceptChanges()
      ''Me.m_isInitialized = True

      'lv.Items.Clear()
      'For Each eqi As EquipmentItem In Me.m_entity.ItemCollection

      '  Dim lvItem As New ListViewItem(eqi.Code)
      '  lvItem.SubItems.Add(eqi.Name)

      '  If Not eqi.CurrentStatus Is Nothing Then
      '    lvItem.SubItems.Add(eqi.CurrentStatus.Description)
      '  Else
      '    lvItem.SubItems.Add("")
      '  End If
      '  'If Not eqi.Costcenter Is Nothing Then
      '  lvItem.SubItems.Add(eqi.Costcenter.Code & ":" & eqi.Costcenter.Name)
      '  'End If
      '  lvItem.Tag = eqi
      '  lv.Items.Add(lvItem).Tag = eqi
      'Next
      'Me.m_isInitialized = True
    End Sub
    Protected Overrides Sub EventWiring()
      ' ʶҹ��Թ��Ѿ��
      'AddHandler chkCancel.CheckedChanged, AddressOf Me.ChangeStatus
      'AddHandler chkDecay.CheckedChanged, AddressOf Me.ChangeStatus

      ' AddHandler cmbCode.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtEQIName.TextChanged, AddressOf Me.ChangeProperty
      AddHandler TextEQIBuycost.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtSerialNumber.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtModel.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtlEQIlicense.TextChanged, AddressOf Me.ChangeProperty

      AddHandler txtEQName.TextChanged, AddressOf Me.ChangeProperty
      AddHandler CmbEQCode.TextChanged, AddressOf Me.ChangeProperty
      'AddHandler CmbEQCode.SelectedIndexChanged, AddressOf Me.ChangeProperty

      AddHandler txtStatus.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtCostcenterAddress.TextChanged, AddressOf Me.ChangeProperty

      'AddHandler TextStatus.Validated, AddressOf Me.ChangeProperty
      AddHandler txtEQIbuydoccode.TextChanged, AddressOf Me.ChangeProperty
      AddHandler TxtBuyDocDate.TextChanged, AddressOf Me.ChangeProperty
      'AddHandler dtpBuyDocDate.ValueChanged, AddressOf Me.ChangeProperty
      'AddHandler TxtlastDateEdit.Validated, AddressOf Me.ChangeProperty 
      'AddHandler dtpLastEditDate.ValueChanged, AddressOf Me.ChangeProperty
      AddHandler txtRentalRate.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtDescription.TextChanged, AddressOf Me.ChangeProperty


      AddHandler txtCostcenterCode.TextChanged, AddressOf Me.TextHandler
      AddHandler txtCostcenterCode.Validated, AddressOf Me.ChangeProperty

      AddHandler txtAssetCode.TextChanged, AddressOf Me.TextHandler
      AddHandler txtAssetCode.Validated, AddressOf Me.ChangeProperty

      AddHandler txtUnitCode.Validated, AddressOf Me.ChangeProperty
      'AddHandler txtRentalUnitCode.Validated, AddressOf Me.ChangeProperty

      'AddHandler TxtCostcenterAddress.TextChanged, AddressOf Me.TextHandler
      'AddHandler TxtCostcenterAddress.Validated, AddressOf Me.ChangeProperty

      AddHandler cmbCode.TextChanged, AddressOf Me.ChangeProperty
      'AddHandler cmbCode.SelectedIndexChanged, AddressOf Me.ChangeProperty


    End Sub
    'Private Sub ItemTreetable_RowChanging(ByVal sender As Object, ByVal e As System.Data.DataRowChangeEventArgs)
    '  Me.UpdateEntityProperties()
    'End Sub
    'Public Sub ChangeStatus(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.m_entity Is Nothing Or Not Me.m_isInitialized Then
    '        Return
    '    End If
    '    Dim dirtyFlag As Boolean = False
    '    Select Case CType(sender, Control).Name.ToLower
    '        Case "chkcancel"
    '            Me.m_entity.Canceled = chkCancel.Checked
    '            dirtyFlag = True
    '        Case "chkdecay"
    '            If chkDecay.Checked Then
    '                Me.m_entity.Status.Value = 4
    '            Else
    '                Me.m_entity.Status.Value = 2
    '            End If
    '            dirtyFlag = True
    '    End Select
    '    Me.WorkbenchWindow.ViewContent.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty Or dirtyFlag
    '    CheckFormEnable()
    'End Sub
    'Public Sub SetValueFromAssetType()
    'If Not Me.m_entity.Type Is Nothing _
    'AndAlso Not Me.m_entity.Type.DepreAble Then
    '  'txtUnitCode.Enabled = False
    '  'txtUnitName.Enabled = False
    '  'btnUnitEdit.Enabled = False
    '  'btnUnitFind.Enabled = False
    '  'txtUnitCode.Text = Me.m_entity.Type.Unit.Code
    '  'txtUnitName.Text = Me.m_entity.Type.Unit.Name
    'Else
    '  ''txtUnitCode.Text = ""
    '  ''txtUnitName.Text = ""
    '  'txtUnitCode.Enabled = True
    '  'txtUnitName.Enabled = True
    '  'btnUnitEdit.Enabled = True
    '  'btnUnitFind.Enabled = True
    'End If
    'Dim flag As Boolean = Me.m_isInitialized
    'Me.m_isInitialized = False
    'If Not Me.m_entity.Account Is Nothing Then
    '  txtGLCode.Text = Me.m_entity.Account.Code
    '  txtGLName.Text = Me.m_entity.Account.Name
    'End If
    'If Not Me.m_entity.DepreOpeningAccount Is Nothing Then
    '  txtDepreOpeningAcctCode.Text = Me.m_entity.DepreOpeningAccount.Code
    '  txtDepreOpeningAcctName.Text = Me.m_entity.DepreOpeningAccount.Name
    'End If
    'If Not Me.m_entity.DepreAccount Is Nothing Then
    '  txtDepreAcctCode.Text = Me.m_entity.DepreAccount.Code
    '  txtDepreAcctName.Text = Me.m_entity.DepreAccount.Name
    'End If
    'Me.m_isInitialized = flag
    'End Sub
    'Public Sub NumerberTextBoxChange(ByVal sender As Object, ByVal e As EventArgs)
    'If Me.m_entity Is Nothing Or Not Me.m_isInitialized Then
    '  Return
    'End If
    'Select Case CType(sender, Control).Name.ToLower
    '  'Case "txtrent"
    '  '    txtRent.Text = Configuration.FormatToString(Me.m_entity.RentalRate, DigitConfig.Price)
    '  Case "txtage"
    '    txtAge.Text = Configuration.FormatToString(Me.m_entity.Age, DigitConfig.Int)
    '  Case "txtcalcrate"
    '    txtCalcRate.Text = Configuration.FormatToString(Me.m_entity.CalcRate, DigitConfig.Qty)
    '  Case "txtstartcalcamt"
    '    txtStartCalcAmt.Text = Configuration.FormatToString(Me.m_entity.StartCalcAmt, DigitConfig.Price)
    '  Case "txtsalvage"
    '    txtSalvage.Text = Configuration.FormatToString(Me.m_entity.Salvage, DigitConfig.Price)
    '  Case "txtdepreopenning"
    '    txtDepreOpenning.Text = Configuration.FormatToString(Me.m_entity.DepreOpening, DigitConfig.Price)
    '  Case "txtremainingvalue"
    '    txtRemainingValue.Text = Configuration.FormatToString(Me.m_entity.RemainValue, DigitConfig.Price)

    '  Case "txtbuyprice"
    '    txtBuyPrice.Text = Configuration.FormatToString(Me.m_entity.BuyPrice, DigitConfig.Price)
    'End Select
    'End Sub
    'Private m_dateSetting As Boolean = False
    'Dim m_cmbCodeChanged As Boolean = False
    'Dim m_txtEQINameChanged As Boolean = False
    'Dim m_TextEQIBuycostChanged As Boolean = False
    'Dim m_TextEQIserailnumberChanged As Boolean = False
    'Dim m_TextEQIbrandChanged As Boolean = False
    'Dim m_txtlEQIlicenseChanged As Boolean = False
    'Dim m_TextStatusChanged As Boolean = False
    'Dim m_TxtlastDateEditChanged As Boolean = False

    Dim m_txtCostcenterCodeChanged As Boolean = False
    Dim m_txtAssetCodeChanged As Boolean = False
    'Dim m_TxtCostcenterAddressChanged As Boolean = False

    'Dim m_txtEQIbuydoccodeChanged As Boolean = False
    'Dim m_TxtBuyDateChanged As Boolean = False
    'Dim m_txtRentalRateChanged As Boolean = False
    'Dim m_txtDescriptionChanegd As Boolean = False
    'Dim m_txtUnitCode1Change As Boolean = False
    Private Sub TextHandler(ByVal sender As Object, ByVal e As EventArgs)
      If Me.m_entity Is Nothing OrElse Not m_isInitialized Then
        Return
      End If
      Select Case CType(sender, Control).Name.ToLower
        'Case "cmbcode"
        '  m_cmbCodeChanged = True
        'Case "txteqiname"
        '  m_txtEQINameChanged = True
        'Case "texteqibuycost"
        '  m_TextEQIBuycostChanged = True
        'Case "texteqiserialnumber"
        '  m_TextEQIserailnumberChanged = True
        'Case "texteqibrand"
        '  m_TextEQIbrandChanged = True
        'Case "texteqilicense"
        '  m_txtlEQIlicenseChanged = True
        'Case "textstatus"
        '  m_TextStatusChanged = True
        'Case "textlastdateedit"
        '  m_TxtlastDateEditChanged = True
        Case "txtcostcentercode"
          m_txtCostcenterCodeChanged = True
        Case "txtassetcode"
          m_txtAssetCodeChanged = True
          'Case "txtcostcenteraddress" ************************************************
          '  m_TxtCostcenterAddressChanged = True *************************************
          'Case "txteqibuydoccode"
          '  m_txtEQIbuydoccodeChanged = True
          'Case "txtbuydocdate"
          '  m_TxtBuyDateChanged = True
          '  'Case "txtUnitCode1"
          '  '  m_txtUnitCode1Change = True
          'Case "txtdescription"
          '  m_txtDescriptionChanegd = True
          'Case "txtrentalrate"
          '  If m_txtRentalRateChanged Then
          '    Dim txt As String = txtRentalRate.Text
          '    txt = txt.Replace(",", "")
          '    If txt.Length = 0 Then
          '      Me.CurrentTagItem.Rentalrate = 0
          '    Else
          '      Try
          '        Me.CurrentTagItem.Rentalrate = CDec(TextHelper.TextParser.Evaluate(txt))
          '      Catch ex As Exception
          '        Me.CurrentTagItem.Rentalrate = 0
          '      End Try
          '    End If
          '    txtRentalRate.Text = Configuration.FormatToString(Me.m_entity.Rentalrate, DigitConfig.UnitPrice)
          '    m_txtRentalRateChanged = True
          'End If

      End Select
    End Sub
    Dim m_dateSetting As Boolean = False
    Public Sub ChangeProperty(ByVal sender As Object, ByVal e As EventArgs)

      If Me.m_entity Is Nothing OrElse Not Me.m_isInitialized Then
        Return
      End If
      Dim dirtyFlag As Boolean = False
      Dim tmpFlag As Boolean = Me.m_isInitialized
      Me.m_isInitialized = False

      Select Case CType(sender, Control).Name.ToLower
        Case "cmbeqcode"
          Me.m_entity.Code = CmbEQCode.Text
          dirtyFlag = True
        Case "txteqname"
          Me.m_entity.Name = txtEQName.Text
          dirtyFlag = True
      End Select

      If dirtyFlag Then
        Me.m_isInitialized = tmpFlag
        Me.WorkbenchWindow.ViewContent.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty Or dirtyFlag
      End If
      Dim doc As EquipmentItem = Me.CurrentTagItem
      If Me.m_entity.EquipmentItem Is Nothing Then
        '  doc = New EquipmentItem
        '  Me.m_entity.ItemCollection.Add(Doc)
        Return
      End If

      Select Case CType(sender, Control).Name.ToLower

        Case "cmbcode"
          'If doc.Autogen Then  'm_entity.AutoGen 
          '  '���� AutoCode
          '  If TypeOf cmbCode.SelectedItem Is AutoCodeFormat Then
          '    Me.m_entity.AutoCodeFormat = CType(cmbCode.SelectedItem, AutoCodeFormat)
          '    Me.m_entity.Code = m_entity.AutoCodeFormat.Format
          '  End If
          'Else
          doc.Code = cmbCode.Text
          'End If
          dirtyFlag = True
          doc.IsDirty = True

        Case "txteqiname"
          doc.Name = txtEQIName.Text
          dirtyFlag = True
          doc.IsDirty = True
        Case "texteqibuycost"
          dirtyFlag = True
          doc.IsDirty = True
          Dim val As Decimal = 0
          If IsNumeric(TextEQIBuycost.TextLength) Then
            val = CDec(TextEQIBuycost.Text)
          End If
          doc.Buycost = val
          'dirtyFlag = True
        Case txtSerialNumber.Name.ToLower
          doc.Serailnumber = txtSerialNumber.Text
          dirtyFlag = True
          doc.IsDirty = True
        Case txtModel.Name.ToLower
          doc.Brand = txtModel.Text
          dirtyFlag = True
          doc.IsDirty = True
        Case txtlEQIlicense.Name.ToLower
          doc.License = txtlEQIlicense.Text
          dirtyFlag = True
          doc.IsDirty = True

          'Case "textstatus"
          '  dirtyFlag = True
          '  If TextStatus.TextLength > 0 Then
          '    doc.CurrentStatus.Value = TextStatus.Text
          '  Else
          '    doc.CurrentStatus = Nothing
          '  End If
          '  'doc.CurrentStatus = CDec(TextStatus.Text)
          '  'dirtyFlag = True
        Case "txtcostcenteraddress"

          'If TxtCostcenterAddress.TextLength > 0 Then
          '  If IsNumeric(TxtCostcenterAddress.Text) Then
          '    doc.CurrentCostCenter = CDec(TxtCostcenterAddress.Text)
          '    dirtyFlag = True
          '  Else
          '    TxtCostcenterAddress.Text = ""
          '    doc.CurrentCostCenter = Nothing
          '  End If
          '  'doc.CurrentCostCenter = CDec(TxtCostcenterAddress.Text)
          'Else
          '  doc.CurrentCostCenter = Nothing
          'End If
          'doc.CurrentCostCenter = CDec(TxtCostcenterAddress.Text)
          'dirtyFlag = True
          'Case "txtlastdateedit"
          '  If Not TxtlastDateEdit.Text.Length = 0 AndAlso Me.Validator.GetErrorMessage(TxtlastDateEdit) = "" Then
          '    Dim thedate As Date = CDate(TxtlastDateEdit.Text)
          '    If Not doc.LastEditDate.Equals(thedate) Then
          '      doc.LastEditDate = thedate
          '      dtpLastEditDate.Value = doc.LastEditDate
          '      dirtyFlag = True
          '    End If

          '  End If
          'Case "dtplasteditdate"
          '  If Not doc.LastEditDate.Equals(dtpLastEditDate.Value) Then
          '    Me.TxtlastDateEdit.Text = MinDateToNull(dtpLastEditDate.Value, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
          '    doc.LastEditDate = dtpLastEditDate.Value
          '    dirtyFlag = True
          '    'Me.RefreshDocs()
          '  End If

        Case "txtcostcentercode"
          If m_txtCostcenterCodeChanged Then
            dirtyFlag = CostCenter.GetCostCenter(Me.txtCostcenterCode, Me.txtCostCenterName, Me.CurrentTagItem.Costcenter) 'doc.Costcenter
            doc.IsDirty = dirtyFlag
            doc.SetCurrentCostCenter(doc.Costcenter)
            Me.txtCostcenterAddress.Text = doc.CurrentCostCenter.Code & " : " & doc.CurrentCostCenter.Name

            m_txtCostcenterCodeChanged = False
            'Me.RefreshDocs()
          End If
        Case "txtassetcode"
          If m_txtAssetCodeChanged Then
            dirtyFlag = Asset.GetAsset(Me.txtAssetCode, Me.txtAssetName, Me.CurrentTagItem.Asset) 'doc.Costcenter
            Me.SetTextFromAsset()
            doc.IsDirty = dirtyFlag
            m_txtAssetCodeChanged = False
            'Me.RefreshDocs()
          End If
        Case "txteqibuydoccode"
          'doc.Buydoc = txtEQIbuydoccode.Text
          'dirtyFlag = True
          'Case "txtbuydocdate"
          '  m_dateSetting = True
          '  If Not Me.TxtBuyDocDate.Text.Length = 0 AndAlso Me.Validator.GetErrorMessage(Me.TxtBuyDocDate) = "" Then
          '    Dim theDate As DateTime = CDate(Me.TxtBuyDocDate.Text)
          '    If Not doc.Buydate.Equals(theDate) Then
          '      dtpBuyDocDate.Value = theDate
          '      doc.Buydate = dtpBuyDocDate.Value
          '      dirtyFlag = True
          '    End If
          '  Else
          '    doc.Buydate = Date.Now
          '    doc.Buydate = Date.MinValue
          '    dirtyFlag = True
          '  End If
          '  m_dateSetting = False
          'Case "dtpbuydocdate"
          '  If Not doc.Buydate.Equals(dtpBuyDocDate.Value) Then
          '    If Not m_dateSetting Then
          '      Me.TxtBuyDocDate.Text = MinDateToNull(dtpBuyDocDate.Value, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
          '      doc.Buydate = dtpBuyDocDate.Value.ToShortDateString
          '    End If
          '    dirtyFlag = True
          '  End If
        Case "txtunitcode"
          dirtyFlag = Unit.GetUnit(txtUnitCode, txtUnit, Me.CurrentTagItem.Unit)
          doc.IsDirty = dirtyFlag
          'Case "txtrentalunitcode"
          '  dirtyFlag = Unit.GetUnit(txtRentalUnitCode, txtRentalunit, Me.CurrentTagItem.Rentalunit)
        Case "txtrentalrate"
          'If txtRentalRate.Text.Length > 0 Then
          '  doc.Rentalrate = txtRentalRate.Text
          'End If

          Dim val As Decimal = 0
          If IsNumeric(txtRentalRate.Text) Then
            val = CDec(txtRentalRate.Text)
          End If
          doc.Rentalrate = val
          dirtyFlag = True
          doc.IsDirty = True

          'm_txtRentalRateChanged = True
          'dirtyFlag = True

        Case "txtdescription"
          'If m_txtDescriptionChanegd Then
          '  doc.Description = txtDescription.Text
          '  dirtyFlag = True
          '  m_txtDescriptionChanegd = False
          '  Me.RefreshDocs()
          'End If
          doc.Description = txtDescription.Text
          'Me.CurrentTagItem.Description = txtDescription.Text
          dirtyFlag = True
          doc.IsDirty = True

      End Select

      Me.m_isInitialized = tmpFlag
      'Me.WorkbenchWindow.ViewContent.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty Or dirtyFlag

      Me.RefreshDocs()
      CheckFormEnable()
    End Sub
    Private Sub SetTextFromAsset()
      Dim doc As EquipmentItem = Me.m_entity.EquipmentItem
      Me.txtAssetCode.Text = doc.Asset.Code
      Me.txtAssetName.Text = doc.Asset.Name
      Me.TextEQIBuycost.Text = Configuration.FormatToString(doc.Buycost, DigitConfig.Price)
      Me.txtEQIbuydoccode.Text = doc.Buydoc.Code
      Me.TxtBuyDocDate.Text = doc.Buydate.ToShortDateString
    End Sub
    'Private Sub CalcDepreEndCalcDate()
    '  txtEndCalcDate.Text = MinDateToNull(Me.m_entity.EndCalcDate, Me.StringParserService.Parse("${res:Global.BlankDateText}"))
    'End Sub
    'Private Sub CalcDepreCalcRate()
    '  txtCalcRate.Text = Configuration.FormatToString(Me.m_entity.CalcRate, DigitConfig.Qty)
    'End Sub
    '    Private Sub SetValue(ByVal sender As Object, ByVal e As EventArgs)
    '      'If Me.m_entity Is Nothing Or Not Me.m_isInitialized Then
    '      '  Return
    '      'End If
    '      ' �ӹǳ���������鹷���Ӥѭ
    '      'Select Case CType(sender, Control).Name.ToLower
    '      '  ' �ӹǳ����ǡѺ�ѹ����������
    '      '  Case "cmbcalctype"
    '      '    CalcDepreEndCalcDate()
    '      '    CalcDepreCalcRate()
    '      '  Case "txtage"
    '      '    CalcDepreEndCalcDate()
    '      '    CalcDepreCalcRate()

    '      '  Case "txtstartcalcdate"
    '      '    CalcDepreEndCalcDate()

    '      '  Case "dtpstartcalcdate"
    '      '    CalcDepreEndCalcDate()

    '      'Case "txtsalvage", "txtbuyprice", "txtdepreopenning", "txtstartcalcamt"
    '      '  txtRemainingValue.Text = Configuration.FormatToString(Me.m_entity.RemainValue, DigitConfig.Price)
    '      'End Select
    '    End Sub
#End Region

#Region "Style"
    Private Sub SetLVHeader()
      lv.MultiSelect = False
      'lv.CheckBoxes = True

      Dim lvColumn As ColumnHeader
      lvColumn = New ColumnHeader
      lvColumn.Name = "linenumber"
      lvColumn.Text = "�ӴѺ"
      lvColumn.TextAlign = HorizontalAlignment.Left
      lvColumn.Width = 60
      lv.Columns.Add(lvColumn)

      lvColumn = New ColumnHeader
      lvColumn.Name = "eqitemcode"
      lvColumn.Text = "����ͧ�ѡ���µ��"
      lvColumn.TextAlign = HorizontalAlignment.Left
      lvColumn.Width = 180
      lv.Columns.Add(lvColumn)

      'lvColumn = New ColumnHeader
      'lvColumn.Name = "eqitemname"
      'lvColumn.Text = "����"
      'lvColumn.TextAlign = HorizontalAlignment.Left
      'lvColumn.Width = 150
      'lv.Columns.Add(lvColumn)

      'lvColumn = New ColumnHeader
      'lvColumn.Name = "lotdate"
      'lvColumn.Text = "�ѹ��� Lot No."
      'lvColumn.TextAlign = HorizontalAlignment.Left
      'lvColumn.Width = 80
      'lv.Columns.Add(lvColumn)

      lvColumn = New ColumnHeader
      lvColumn.Name = "assetcode"
      lvColumn.Text = "�Թ��Ѿ��"
      lvColumn.TextAlign = HorizontalAlignment.Left
      lvColumn.Width = 150
      lv.Columns.Add(lvColumn)

      lvColumn = New ColumnHeader
      lvColumn.Name = "costcentername"
      lvColumn.Text = "Cost Center"
      lvColumn.TextAlign = HorizontalAlignment.Left
      lvColumn.Width = 150
      lv.Columns.Add(lvColumn)

      'lvColumn = New ColumnHeader
      'lvColumn.Name = "writeoffqty"
      'lvColumn.Text = "�ӹǹ Write off"
      'lvColumn.TextAlign = HorizontalAlignment.Right
      'lvColumn.Width = 100
      'lv.Columns.Add(lvColumn)

      'lvColumn = New ColumnHeader
      'lvColumn.Name = "remainqty"
      'lvColumn.Text = "�ӹǹ�������"
      'lvColumn.TextAlign = HorizontalAlignment.Right
      'lvColumn.Width = 100
      'lv.Columns.Add(lvColumn)

      lvColumn = New ColumnHeader
      lvColumn.Name = "refstatus"
      lvColumn.Text = "ʶҹС����ҧ�ԧ"
      lvColumn.TextAlign = HorizontalAlignment.Left
      lvColumn.Width = 100
      lv.Columns.Add(lvColumn)

      'lvColumn = New ColumnHeader
      'lvColumn.Name = "status"
      'lvColumn.Text = "status"
      'lvColumn.TextAlign = HorizontalAlignment.Left
      'lvColumn.Width = 80
      'lv.Columns.Add(lvColumn)

    End Sub
    'Public Function CreateTableStyle() As DataGridTableStyle
    '  Dim dst As New DataGridTableStyle
    '  dst.MappingName = "Equipment"
    '  Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)

    '  Dim csCode As New TreeTextColumn
    '  csCode.MappingName = "code"
    '  csCode.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.CodeHeaderText}")
    '  csCode.NullText = ""
    '  csCode.Width = 100
    '  csCode.DataAlignment = HorizontalAlignment.Center
    '  csCode.ReadOnly = True
    '  csCode.TextBox.Name = "code"

    '  Dim csName As New TreeTextColumn
    '  csName.MappingName = "name"
    '  csName.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.NameHeaderText}")
    '  csName.NullText = ""
    '  csName.Width = 100
    '  csName.ReadOnly = True
    '  csName.TextBox.Name = "name"

    '  Dim csStatus As New TreeTextColumn
    '  csStatus.MappingName = "status"
    '  csStatus.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.StatusHeaderText}")
    '  csStatus.NullText = ""
    '  csStatus.Width = 100
    '  csStatus.ReadOnly = True
    '  csStatus.TextBox.Name = "status"

    '  Dim csCostCenter As New TreeTextColumn
    '  csCostCenter.MappingName = "costcenter"
    '  csCostCenter.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.CostCenterHeaderText}")
    '  csCostCenter.NullText = ""
    '  csCostCenter.Width = 100
    '  csCostCenter.ReadOnly = True
    '  csCostCenter.TextBox.Name = "costcenter"

    '  dst.GridColumnStyles.Add(csCode)
    '  dst.GridColumnStyles.Add(csName)
    '  dst.GridColumnStyles.Add(csStatus)
    '  dst.GridColumnStyles.Add(csCostCenter)

    '  Return dst
    'End Function

#End Region

    '#Region "IListDetail"
    '    Private Sub CheckIsDepreciated(ByVal flag As Boolean)
    '      ' �ѧ�ѭ��
    '      txtGLCode.Enabled = Not flag
    '      txtGLName.Enabled = Not flag
    '      btnGLEdit.Enabled = Not flag
    '      btnGLFind.Enabled = Not flag
    '      ' �ѧ�ѭ�դ������������
    '      txtDepreOpeningAcctCode.Enabled = Not flag
    '      txtDepreOpeningAcctName.Enabled = Not flag
    '      btnDepreOpeningAcctEdit.Enabled = Not flag
    '      btnDepreOpeningAcctFind.Enabled = Not flag
    '      ' �ѧ�ѭ�դ��������
    '      txtDepreAcctCode.Enabled = Not flag
    '      txtDepreAcctName.Enabled = Not flag
    '      btnDepreAcctEdit.Enabled = Not flag
    '      btnDepreAcctFind.Enabled = Not flag
    '      ' cost center
    '      txtCostcenterCode.Enabled = Not flag
    '      txtCostcenterName.Enabled = Not flag
    '      btnCostcenterEdit.Enabled = Not flag
    '      btnCostcenterFind.Enabled = Not flag
    '      ' ��äӹǳ
    '      grbCalcDetail.Enabled = Not flag
    '      ' ��ë��� 
    '      grbBuyDetail.Enabled = Not flag
    '    End Sub

    ' ��Ǩ�ͺʶҹТͧ�����

    Public Overrides Sub CheckFormEnable()
      ' Protected from ...
      If Me.m_entity.Canceled Then
        For Each crlt As Control In grbDetail.Controls
          crlt.Enabled = False
        Next
        'For Each crlt As Control In grbStatus.Controls
        '  crlt.Enabled = False
        'Next
        'grbCalcDetail.Enabled = False
        'grbBuyDetail.Enabled = False
        'chkCancel.Enabled = True
      Else
        For Each crlt As Control In grbDetail.Controls
          crlt.Enabled = True
        Next
        If Me.m_entity.Originated Then
          Grbeqi.Enabled = True
          Me.CmbEQCode.Enabled = False
          Me.txtEQName.Enabled = False
          Me.chkEqAutoRun.Enabled = False
          Me.Validator.SetRequired(Me.txtEQIName, True)
          Me.Validator.SetRequired(Me.txtCostcenterCode, True)
        Else
          Grbeqi.Enabled = False
          Me.CmbEQCode.Enabled = True
          Me.txtEQName.Enabled = True
          Me.chkEqAutoRun.Enabled = True
          Me.Validator.SetRequired(Me.txtEQIName, False)
          Me.Validator.SetRequired(Me.txtCostcenterCode, False)
        End If
        ''For Each ctrl As Control In grbStatus.Controls
        ''    ctrl.Enabled = True
        ''Next
        'grbCalcDetail.Enabled = True
        'grbBuyDetail.Enabled = True
        '' ��˹� columns ����ͧ��� protect ������ա�äӹǳ���������
        'CheckIsDepreciated(Me.m_entity.IsDepreciated)

      End If

      'SetValueFromAssetType()
    End Sub
    Public Sub CheckEquipmentItemEnable()
      Dim eqi As EquipmentItem = Me.CurrentTagItem
      If eqi.IsReferenced Then
        'For Each crlt As Control In grbDetail.Controls
        '  crlt.Enabled = False
        'Next
        Grbeqi.Enabled = False
        'btnDel.Enabled = False
      Else
        Grbeqi.Enabled = True
        'btnDel.Enabled = True
      End If
    End Sub
    Public Sub ClearItemOnly()
      For Each ctrl As Control In Grbeqi.Controls
        If TypeOf ctrl Is TextBox Then
          ctrl.Text = ""
        End If
      Next

      'For Each ctrl As Control In TabPage1.Controls
      '  If TypeOf ctrl Is TextBox Then
      '    ctrl.Text = ""
      '  End If
      'Next
      'For Each ctrl As Control In TabPage2.Controls
      '  If TypeOf ctrl Is TextBox Then
      '    ctrl.Text = ""
      '  End If
      'Next
    End Sub

    ' ������������ control
    Public Overrides Sub ClearDetail()

      'For Each ctrl As Control In grbDetail.Controls
      '  If TypeOf ctrl Is FixedGroupBox OrElse TypeOf ctrl Is GroupBox Then
      '    For Each child As Control In ctrl.Controls
      '      If TypeOf child Is TextBox Then
      '        child.Text = ""
      '      End If
      '      If TypeOf child Is TabPage Then
      '        For Each ctrltab As Control In child.Controls
      '          If TypeOf ctrltab Is TextBox Then
      '            child.Text = ""
      '          End If
      '        Next
      '      End If
      '    Next
      '  ElseIf TypeOf ctrl Is TextBox Then
      '    ctrl.Text = ""
      '  End If
      'Next

      'For Each ctrl As Control In grbDetail.Controls
      '  If TypeOf ctrl Is TextBox Then
      '    ctrl.Text = ""
      '  End If
      'Next

      'For Each ctrl As Control In Grbeqi.Controls
      '  If TypeOf ctrl Is TextBox Then
      '    ctrl.Text = ""
      '  End If
      'Next

      'For Each ctrl As Control In TabPage1.Controls
      '  If TypeOf ctrl Is TextBox Then
      '    ctrl.Text = ""
      '  End If
      'Next
      'For Each ctrl As Control In TabPage2.Controls
      '  If TypeOf ctrl Is TextBox Then
      '    ctrl.Text = ""
      '  End If
      'Next

      'cmbCalcType.SelectedIndex = 0
      cmbCode.Text = ""
      'TxtlastDateEdit.Text = ""
      'dtpLastEditDate.Value = Date.Now

      'txtEndCalcDate.Text = Me.StringParserService.Parse("${res:Global.BlankDateText}")

      'TxtBuyDate.Text = Me.StringParserService.Parse("${res:Global.BlankDateText}")
      'DateTimePicker2.Value = Date.Now

      'dtpBuyDocDate.Value = Date.Now
      'TxtBuyDocDate.Text = "" 'Me.StringParserService.Parse("${res:Global.BlankDateText}")
      'lblLasteditdate.Text = "���ʼ���������ش" & " : " & " .... " & " �ѹ����������ش : " & Date.Now.ToString("dd/MM/yyyy")

      'Me.picImage.Image = Nothing

    End Sub

    Public Overrides Property Entity() As ISimpleEntity
      Get
        Return Me.m_entity
      End Get
      Set(ByVal Value As ISimpleEntity)

        If Not Object.ReferenceEquals(Me.m_entity, Value) Then
          Me.m_entity = Nothing
          If TypeOf Value Is Equipment Then
            Me.m_entity = CType(Value, Equipment)
          End If
          'Me.m_entity.LoadImage()
        End If

        If m_entity.EquipmentItem Is Nothing Then
          m_entity.EquipmentItem = New EquipmentItem
          m_entity.EquipmentItem.Autogen = True
        End If

        m_entity.EquipmentItem.equipment = Me.m_entity

        'Hack:
        'Me.m_entity.OnTabPageTextChanged(m_entity, EventArgs.Empty)
        'Me.m_isInitialized = False
        'RefreshDocs()
        'Me.m_entity.EquipmentItem = Me.m_entity.ItemCollection(0)
        'Me.RefreshData(Me.m_entity.EquipmentItem)
        'Me.m_isInitialized = True

        UpdateEntityProperties()
      End Set
    End Property



    '#End Region

#Region " IValidatable "
    Public ReadOnly Property FormValidator() As Components.PJMTextboxValidator Implements IValidatable.FormValidator
      Get
        Return Me.Validator
      End Get
    End Property
#End Region


#Region "Image button"
    Private Sub btnLoadImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadImage.Click
      'Private Sub btnLoadImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim dlg As New OpenFileDialog

      Dim fileFilters As String() = CType(AddInTreeSingleton.AddInTree.GetTreeNode("/Pojjaman/Workbench/Image/FileFilter").BuildChildItems(Me).ToArray(GetType(String)), String())
      dlg.Filter = String.Join("|", fileFilters)
      If dlg.ShowDialog = DialogResult.OK Then
        Dim img As Image = Image.FromFile(dlg.FileName)
        If img.Size.Height > Me.picImage.Height OrElse img.Size.Width >= Me.picImage.Width Then
          Dim percent As Decimal = 100 * (Math.Min(Me.picImage.Height / img.Size.Height, Me.picImage.Width / img.Size.Width))
          img = ImageHelper.Resize(img, percent)
        End If
        Me.picImage.Image = img
        m_entity.EquipmentItem.Image = img
        'Dim myContent As IViewContent = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ViewContent
        'myContent.IsDirty = True
        Dim doc As EquipmentItem = Me.CurrentTagItem
        If Not Me.m_entity.EquipmentItem Is Nothing Then
          doc.IsImageDirty = True
          doc.IsDirty = True
        End If
        CheckLabelImgSize()
      End If
    End Sub
    Private Sub btnClearImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearImage.Click
      'Private Sub btnClearImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      m_entity.EquipmentItem.Image = Nothing
      Me.picImage.Image = Nothing
      'Dim myContent As IViewContent = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ViewContent
      'myContent.IsDirty = True
      Dim doc As EquipmentItem = Me.CurrentTagItem
      If Not Me.m_entity.EquipmentItem Is Nothing Then
        doc.IsImageDirty = True
        doc.IsDirty = True
      End If
      CheckLabelImgSize()
    End Sub
    Private Sub CheckLabelImgSize()
      Me.lblPicSize.Text = "120 X 120 pixel"
      If Me.m_entity.EquipmentItem.Image Is Nothing Then
        Me.lblPicSize.Visible = True
      Else
        Me.lblPicSize.Visible = False
      End If
    End Sub
#End Region


#Region "IClipboardHandler Overrides"
    Public Overrides ReadOnly Property EnablePaste() As Boolean
      'Get
      '  Dim data As IDataObject = Clipboard.GetDataObject
      '  If data.GetDataPresent((New Account).FullClassName) Then
      '    If Not Me.ActiveControl Is Nothing Then
      '      Select Case Me.ActiveControl.Name.ToLower
      '        Case "txtglcode", "txtglname"
      '          Return True
      '        Case "txtdepreopeningacctcode", "txtdepreopeningacctname"
      '          Return True
      '        Case "txtdepreacctcode", "txtdepreacctname"
      '          Return True
      '      End Select
      '    End If
      '  End If
      '  If data.GetDataPresent((New Unit).FullClassName) Then
      '    If Not Me.ActiveControl Is Nothing Then
      '      Select Case Me.ActiveControl.Name.ToLower
      '        Case "txtunitcode", "txtunitname"
      '          Return True
      '      End Select
      '    End If
      '  End If
      '  If data.GetDataPresent((New AssetType).FullClassName) Then
      '    If Not Me.ActiveControl Is Nothing Then
      '      Select Case Me.ActiveControl.Name.ToLower
      '        Case "txttypecode", "txttypename"
      '          Return True
      '      End Select
      '    End If
      '  End If
      '  If data.GetDataPresent((New CostCenter).FullClassName) Then
      '    If Not Me.ActiveControl Is Nothing Then
      '      Select Case Me.ActiveControl.Name.ToLower
      '        Case "txtcostcentercode", "txtcostcentername"
      '          Return True
      '      End Select
      '    End If
      '  End If
      '  Return False
      'End Get
      Get
        Dim data As IDataObject = Clipboard.GetDataObject
        If data.GetDataPresent((New Unit).FullClassName) Then
          If Not Me.ActiveControl Is Nothing Then
            Select Case Me.ActiveControl.Name.ToLower
              Case "txtdefaultunitcode", "txtdefaultunit" _
               , "txtunitcode", "txtunit" _
               , "txtrentalunitcode", "txtrentalunit"

                Return True
            End Select
          End If
        End If
        If data.GetDataPresent((New Account).FullClassName) Then
          If Not Me.ActiveControl Is Nothing Then
            Select Case Me.ActiveControl.Name.ToLower
              Case "txtaccountcode", "txtaccount"
                Return True
            End Select
          End If
        End If
        Return False
      End Get
    End Property
    Public Overrides Sub Paste(ByVal sender As Object, ByVal e As System.EventArgs)
      'Dim data As IDataObject = Clipboard.GetDataObject
      'If data.GetDataPresent((New Account).FullClassName) Then
      '  Dim id As Integer = CInt(data.GetData((New Account).FullClassName))
      '  Dim entity As New Account(id)
      '  If Not Me.ActiveControl Is Nothing Then
      '    Select Case Me.ActiveControl.Name.ToLower
      '      Case "txtglcode", "txtglname"
      '        Me.SetAccountDialog(entity)
      '      Case "txtdepreopeningacctcode", "txtdepreopeningacctname"
      '        Me.SetDepreOpeningAccountDialog(entity)
      '      Case "txtdepreacctcode", "txtdepreacctname"
      '        Me.SetDepreAccountDialog(entity)
      '    End Select
      '  End If
      'End If
      'If data.GetDataPresent((New AssetType).FullClassName) Then
      '  Dim id As Integer = CInt(data.GetData((New AssetType).FullClassName))
      '  Dim entity As New AssetType(id)
      '  If Not Me.ActiveControl Is Nothing Then
      '    Select Case Me.ActiveControl.Name.ToLower
      '      Case "txttypecode", "txttypename"
      '        Me.SetAssetTypeDialog(entity)
      '    End Select
      '  End If
      'End If
      'If data.GetDataPresent((New CostCenter).FullClassName) Then
      '  Dim id As Integer = CInt(data.GetData((New CostCenter).FullClassName))
      '  Dim entity As New CostCenter(id)
      '  If Not Me.ActiveControl Is Nothing Then
      '    Select Case Me.ActiveControl.Name.ToLower
      '      Case "txtcostcentercode", "txtcostcentername"
      '        Me.SetCostCenterDialog(entity)
      '    End Select
      '  End If
      'End If
      Dim data As IDataObject = Clipboard.GetDataObject
      If data.GetDataPresent((New Unit).FullClassName) Then
        Dim id As Integer = CInt(data.GetData((New Unit).FullClassName))
        Dim entity As New Unit(id)
        If Not Me.ActiveControl Is Nothing Then
          Select Case Me.ActiveControl.Name.ToLower
            'Case "txtdefaultunit", "txtdefaultunitcode"
            '  Me.SetDefaultUnit(entity)
            Case "txtunitcode1", "txtunit1"
              Me.SetUnit1(entity)
              'Case "txtunitcode2", "txtunit2"
              '  'Me.SetUnit2(entity)

          End Select
        End If
      End If
      'If data.GetDataPresent((New Account).FullClassName) Then
      '  Dim id As Integer = CInt(data.GetData((New Account).FullClassName))
      '  Dim entity As New Account(id)
      '  If Not Me.ActiveControl Is Nothing Then
      '    Select Case Me.ActiveControl.Name.ToLower
      '      Case "txtaccountcode", "txtaccount"
      '        Me.SetAccount(entity)
      '    End Select
      '  End If
      'End If
    End Sub
    'End Sub
#End Region

#Region "Event of Button controls"
    ' Account button
    'Private Sub btnGLEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '  Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
    '  myEntityPanelService.OpenPanel(New Account)
    'End Sub
    'Private Sub btnGLFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '  Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
    '  Select Case CType(sender, Control).Name.ToLower
    '    Case "btnglfind"
    '      myEntityPanelService.OpenTreeDialog(New Account, AddressOf SetAccountDialog)
    '    Case "btndepreopeningacctfind"
    '      myEntityPanelService.OpenTreeDialog(New Account, AddressOf SetDepreOpeningAccountDialog)
    '    Case "btndepreacctfind"
    '      myEntityPanelService.OpenTreeDialog(New Account, AddressOf SetDepreAccountDialog)
    '  End Select
    'End Sub
    'Private Sub SetAccountDialog(ByVal e As ISimpleEntity)
    '  Me.txtGLCode.Text = e.Code
    '  Me.WorkbenchWindow.ViewContent.IsDirty = _
    '      Me.WorkbenchWindow.ViewContent.IsDirty _
    '      'Or Account.GetAccount(txtGLCode, txtGLName, Me.m_entity.Account)
    'End Sub
    'Private Sub SetDepreOpeningAccountDialog(ByVal e As ISimpleEntity)
    '  Me.txtDepreOpeningAcctCode.Text = e.Code
    '  Me.WorkbenchWindow.ViewContent.IsDirty = _
    '      Me.WorkbenchWindow.ViewContent.IsDirty _
    '      'Or Account.GetAccount(txtDepreOpeningAcctCode, txtDepreOpeningAcctName, Me.m_entity.DepreOpeningAccount)
    'End Sub
    'Private Sub SetDepreAccountDialog(ByVal e As ISimpleEntity)
    '  Me.txtDepreAcctCode.Text = e.Code
    '  Me.WorkbenchWindow.ViewContent.IsDirty = _
    '      Me.WorkbenchWindow.ViewContent.IsDirty _
    '      'Or Account.GetAccount(txtDepreAcctCode, txtDepreAcctName, Me.m_entity.DepreAccount)
    'End Sub
    ' Type button
    Private Sub btnTypeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New AssetType)
    End Sub
    'Private Sub btnTypeFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '  Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
    '  myEntityPanelService.OpenTreeDialog(New AssetType, AddressOf SetAssetTypeDialog)
    'End Sub
    'Private Sub SetAssetTypeDialog(ByVal e As ISimpleEntity)
    '  Me.txtTypeCode.Text = e.Code
    '  Me.WorkbenchWindow.ViewContent.IsDirty = _
    '      Me.WorkbenchWindow.ViewContent.IsDirty _
    '      'Or AssetType.GetAssetType(txtTypeCode, txtTypeName, Me.m_entity.Type)
    '  'SetValueFromAssetType()
    'End Sub

    ' Costcenter button
    Private Sub btnCostcenterEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnShowcostcenter.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(New CostCenter)
    End Sub
    Private Sub btnCostcenterFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnCostcenterDialog.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenTreeDialog(New CostCenter, AddressOf SetCostCenterDialog)
    End Sub
    Private Sub SetCostCenterDialog(ByVal e As ISimpleEntity)
      Dim eqi As EquipmentItem = Me.CurrentTagItem
      If eqi Is Nothing Then
        Return
      End If
      Me.txtCostcenterCode.Text = e.Code
      'Me.WorkbenchWindow.ViewContent.IsDirty = _
      '    Me.WorkbenchWindow.ViewContent.IsDirty _
      '    Or CostCenter.GetCostCenter(txtCostcenterCode, txtCostCenterName, eqi.Costcenter)

      'CostCenter.GetCostCenter(txtCostcenterCode, txtCostCenterName, eqi.Costcenter)
      eqi.IsDirty = CostCenter.GetCostCenter(txtCostcenterCode, txtCostCenterName, eqi.Costcenter)

      RefreshDocs()

      eqi.SetCurrentCostCenter(eqi.Costcenter)
      Me.txtCostcenterAddress.Text = eqi.CurrentCostCenter.Code & " : " & eqi.CurrentCostCenter.Name

    End Sub
    Private Sub btnAssetFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssetFind.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)

      'Dim filters(0) As Filter
      'filters(0) = New Filter("OnlyAssetNotRelateObject", True)

      myEntityPanelService.OpenListDialog(New AssetForToollotSelection, AddressOf SetAssetDialog)
    End Sub
    Private Sub SetAssetDialog(ByVal e As ISimpleEntity)
      'Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)
      Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      Dim doc As EquipmentItem = Me.m_entity.EquipmentItem 'Me.CurrentTagItem
      If doc Is Nothing Then
        Return
      End If

      Dim oldAsset As Asset = Nothing
      If Not doc.Asset Is Nothing Then
        oldAsset = doc.Asset
      End If

      Me.txtAssetCode.Text = e.Code
      'Me.WorkbenchWindow.ViewContent.IsDirty = _
      '    Me.WorkbenchWindow.ViewContent.IsDirty _
      '    Or Asset.GetAsset(txtAssetCode, txtAssetName, doc.Asset)
      'doc.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty

      Dim dirty As Boolean = False
      dirty = Asset.GetAsset(txtAssetCode, txtAssetName, doc.Asset)
      Me.SetTextFromAsset()

      'If dirty Then
      '  If doc.Buycost = 0 OrElse doc.Buycost = doc.Asset.BuyPrice Then
      '    doc.Buycost = doc.Asset.BuyPrice
      '    doc.Buydoc.Code = doc.Asset.BuyDocCode
      '    doc.Buydate = doc.Asset.BuyDate
      '  ElseIf doc.Buycost <> doc.Asset.BuyPrice Then
      '    '����� message �͡��ҵ�ͧ��Ѻ������ѹ���ǡѹ ����������� save
      '    msgServ.ShowMessageFormatted("${res:Longkong.Pojjaman.Gui.Panels.EquipmentDetailView.CostMustEqualBuyDocPrice}", _
      '                                 New String() {Me.txtAssetCode.Text, Configuration.FormatToString(doc.Asset.BuyPrice, DigitConfig.Price), _
      '                                               Configuration.FormatToString(doc.Buycost, DigitConfig.Price)})
      '    If oldAsset Is Nothing Then
      '      doc.Asset = New Asset
      '    Else
      '      doc.Asset = oldAsset
      '      Me.txtAssetCode.Text = oldAsset.Code
      '      Me.txtAssetName.Text = oldAsset.Name
      '    End If
      '  End If
      'End If

      'Me.RefreshListViewData()
    End Sub
    ' More detail
    Private Sub btnAssetAuxDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim myAuxPanel As New Longkong.Pojjaman.Gui.Panels.AssetAuxDetail
      'myAuxPanel.Entity = Me.m_entity
      Dim myDialog As New Longkong.Pojjaman.Gui.Dialogs.PanelDialog(myAuxPanel)
      If myDialog.ShowDialog() = DialogResult.Cancel Then
        'Me.WorkbenchWindow.ViewContent.IsDirty = False
      End If
    End Sub
#End Region

#Region " Autogencode "
    Private Sub chkAutorun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
      UpdateAutogenStatus()
    End Sub
    Private m_oldCode As String = ""
    Private m_oldEqCode As String = ""
    Private Sub UpdateAutogenStatus()
      If Me.m_entity Is Nothing Then
        Return
      End If

      Dim doc As EquipmentItem = Me.m_entity.EquipmentItem
      If doc Is Nothing Then
        Return
        'doc = New EquipmentItem
        'Me.m_entity.ItemCollection.Add(doc)
      End If

      If Me.chkAutorun.Checked Then
        Me.Validator.SetRequired(Me.cmbCode, False)
        Me.ErrorProvider1.SetError(Me.cmbCode, "")
        Me.cmbCode.DropDownStyle = ComboBoxStyle.DropDownList
        Dim currentUserId As Integer = Me.SecurityService.CurrentUser.Id
        BusinessLogic.Entity.NewPopulateCodeCombo(Me.cmbCode, doc.EntityId, currentUserId)

        If doc.Code Is Nothing OrElse doc.Code.Length = 0 Then
          If Me.cmbCode.Items.Count > 0 Then
            doc.Code = CType(Me.cmbCode.Items(0), AutoCodeFormat).Format
            Me.cmbCode.SelectedIndex = 0
            doc.AutoCodeFormat = CType(Me.cmbCode.Items(0), AutoCodeFormat)
          End If
        Else
          Me.cmbCode.SelectedIndex = Me.cmbCode.FindStringExact(doc.Code)
          If TypeOf Me.cmbCode.SelectedItem Is AutoCodeFormat Then
            doc.AutoCodeFormat = CType(Me.cmbCode.SelectedItem, AutoCodeFormat)
          End If
        End If

        'doc.oldcode = Me.cmbCode.Text
        'doc.Code = Me.cmbCode.Text
        doc.Autogen = True
      Else
        'Me.Validator.SetRequired(Me.txtCode, True)
        Me.cmbCode.DropDownStyle = ComboBoxStyle.Simple
        Me.cmbCode.Items.Clear()
        Me.cmbCode.Text = doc.Code
        'doc.Code = doc.oldcode
        doc.Autogen = False
      End If
    End Sub
    Private Sub UpdateEqAutogenStatus()
      If Me.chkEqAutoRun.Checked Then
        'Me.Validator.SetRequired(Me.txtCode, False)
        'Me.ErrorProvider1.SetError(Me.txtCode, "")
        Me.CmbEQCode.DropDownStyle = ComboBoxStyle.DropDownList
        Dim currentUserId As Integer = Me.SecurityService.CurrentUser.Id
        BusinessLogic.Entity.NewPopulateCodeCombo(Me.CmbEQCode, Me.m_entity.EntityId, currentUserId)
        If Me.m_entity.Code Is Nothing OrElse Me.m_entity.Code.Length = 0 Then
          If Me.CmbEQCode.Items.Count > 0 Then
            Me.m_entity.Code = CType(Me.CmbEQCode.Items(0), AutoCodeFormat).Format
            Me.CmbEQCode.SelectedIndex = 0
            Me.m_entity.AutoCodeFormat = CType(Me.CmbEQCode.Items(0), AutoCodeFormat)
          End If
        Else
          Me.CmbEQCode.SelectedIndex = Me.CmbEQCode.FindStringExact(Me.m_entity.Code)
          If Me.cmbCode.Items.Count > 0 Then
            cmbCode.SelectedIndex = 0
          End If
          If TypeOf Me.CmbEQCode.SelectedItem Is AutoCodeFormat Then
            Me.m_entity.AutoCodeFormat = CType(Me.CmbEQCode.SelectedItem, AutoCodeFormat)
          End If
        End If
        'm_oldEqCode = Me.CmbEQCode.Text
        'Me.m_entity.Code = m_oldEqCode
        Me.m_entity.AutoGen = True
      Else
        'Me.Validator.SetRequired(Me.txtCode, True)
        Me.CmbEQCode.DropDownStyle = ComboBoxStyle.Simple
        Me.CmbEQCode.Items.Clear()
        Me.CmbEQCode.Text = Me.m_entity.Code
        'Me.m_entity.Code = m_oldEqCode
        Me.m_entity.AutoGen = False
      End If
    End Sub
#End Region

    'Private Sub CheckLabelImgSize()
    '  Me.lblPicSize.Text = "272 X 204 pixel"
    '  If Me.m_entity.Image Is Nothing Then
    '    Me.lblPicSize.Visible = True
    '  Else
    '    Me.lblPicSize.Visible = False
    '  End If
    'End Sub

    Private Sub SetLVItemRegular()
      For Each item As ListViewItem In lv.Items
        item.Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
      Next
    End Sub

    'Private Sub lv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lv.SelectedIndexChanged

    '  Me.m_isInitialized = False
    '  'Me.RemoveEvent()
    '  If lv.SelectedItems.Count > 0 Then
    '    If Not lv.SelectedItems(0).Tag Is Nothing AndAlso TypeOf lv.SelectedItems(0).Tag Is EquipmentItem Then
    '      'If Me.m_entity.EquipmentItem.Id <> CType(lv.SelectedItems(0).Tag, EquipmentItem).Id Then
    '      Me.m_entity.EquipmentItem = CType(lv.SelectedItems(0).Tag, EquipmentItem)
    '      Dim eqi As EquipmentItem = Me.CurrentTagItem
    '      Me.RefreshData()
    '      Me.CheckEquipmentItemEnable()
    '      'End If
    '    End If

    '  End If
    '  Me.m_isInitialized = True
    '  'Me.EventWiring()
    'End Sub

    Private Sub ibtnAddWBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      Dim eqi As EquipmentItem = Me.CurrentTagItem
      'Dim index As Integer = lv.SelectedItems.Count
      Dim neweqi As New EquipmentItem
      'cmbCode.Text = eqitem.Code
      'Me.m_oldCode = eqitem.Code
      'Me.chkAutorun.Checked = eqitem.Autogen
      'Me.UpdateAutogenStatus()
      'neweqi.LastEditDate = Now
      'neweqi.Costcenter = New CostCenter
      'neweqi.Buydate = Now

      If Not eqi Is Nothing Then
        Me.m_entity.ItemCollection.Add(neweqi)
      Else
        Me.m_entity.ItemCollection.Insert(Me.m_entity.ItemCollection.IndexOf(eqi) + 1, neweqi)
      End If
      Me.RefreshData()



      'If Me.m_entity.ItemCollection.Count > -1 Then
      '  Return
      'End If
      'Dim newItem As New BlankItem("")
      'Dim prItem As New PRItem
      'lv.Items.Add = newItem
      'prItem.ItemType = New ItemType(0)
      'prItem.Qty = 0
      'Me.m_entity.ItemCollection.Insert(index, eqitem)
      'RefreshDocs()
      'eqitem.CurrentRowIndex = index



      'Dim lvItem As New ListViewItem(eqi.Code)

      'Dim AAA As EquipmentItem = m_entity.ItemCollection.CurrentItem

      'lvItem.SubItems.Add("")
      'lvItem.SubItems.Add("")
      'lvItem.SubItems.Add("")
      'lvItem.Tag = eqi
      'lv.Items.Add(lvItem).Tag = eqi

      'Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      'If Me.m_entity Is Nothing Then
      '  Return
      'End If
      'Dim doc As EquipmentItem = Me.m_entity.ItemCollection.CurrentItem
      'If doc Is Nothing Then
      '  Return
      'End If
      'Dim dt As TreeTable = Me.m_treeManager.Treetable
      'dt.Clear()
      'Dim view As Integer = 7
      'Dim wsdColl As EquipmentItemCollection = doc.Equipment.ItemCollection
      'If wsdColl.GetSumPercent >= 100 Then
      '  msgServ.ShowMessage("${res:Global.Error.WBSPercentExceed100}")
      'ElseIf doc.ItemType.Value = 160 Or doc.ItemType.Value = 162 Then
      '  msgServ.ShowMessage("${res:Global.Error.NoteCannotHaveWBS}")
      'Else
      'Dim eqid As New equi
      'Dim wbsd As New WBSDistribute
      'wbsd.CostCenter = Me.m_entity.CostCenter
      'wbsd.Percent = 100 - wsdColl.GetSumPercent
      'wsdColl.Add(wbsd)
      'End If
      'm_wbsdInitialized = False
      'wsdColl.Populate(dt, doc, view)
      'm_wbsdInitialized = True
      'Me.WorkbenchWindow.ViewContent.IsDirty = True
    End Sub
#Region "Event of Control"
    Private Sub SetUnit1(ByVal e As ISimpleEntity)
      Me.txtUnitCode.Text = e.Code
      Dim flag As Boolean = Unit.GetUnit(txtUnitCode, txtUnit, Me.CurrentTagItem.Unit)
      'Me.WorkbenchWindow.ViewContent.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty Or flag
      Dim eqi As EquipmentItem = Me.CurrentTagItem
      If eqi Is Nothing Then
        Return
      End If
      eqi.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty
    End Sub
    'Private Sub SetUnit2(ByVal e As ISimpleEntity)
    '  Me.txtRentalUnitCode.Text = e.Code
    '  Dim flag As Boolean = Unit.GetUnit(txtRentalUnitCode, txtRentalunit, Me.CurrentTagItem.Rentalunit)
    '  Me.WorkbenchWindow.ViewContent.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty Or flag
    'End Sub


    Private Sub ShowNewPanels(ByVal entity As ISimpleEntity)
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenPanel(entity)
    End Sub
#End Region
    'Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '  Dim dlg As New BasketDialog
    '  AddHandler dlg.EmptyBasket, AddressOf SetItems

    '  Dim filters(5) As Filter
    '  Dim excludeList As Object = ""
    '  excludeList = GetPRExcludeList()
    '  If excludeList.ToString.Length = 0 Then
    '    excludeList = DBNull.Value
    '  End If
    '  Dim prNeedsApproval As Boolean = False
    '  Dim prNeedsStoreApproval As Boolean = False

    '  Dim tmp As Object
    '  Dim tmp2 As Object
    '  tmp = Configuration.GetConfig("MWPRFull")
    '  tmp2 = Configuration.GetConfig("MWPRremainPO")

    '  prNeedsApproval = CBool(Configuration.GetConfig("ApprovePR"))
    '  prNeedsStoreApproval = CBool(Configuration.GetConfig("PRNeedStoreApprove"))

    '  filters(0) = New Filter("excludeList", excludeList)
    '  filters(1) = New Filter("prNeedsApproval", prNeedsApproval)
    '  filters(2) = New Filter("excludeCanceled", True)
    '  filters(3) = New Filter("PRNeedStoreApprove", prNeedsStoreApproval)
    '  filters(4) = New Filter("formEntity", Me.m_entity.EntityId)

    '  If CBool(tmp) Then
    '    filters(5) = New Filter("MWPRMode", 1)
    '  ElseIf CBool(tmp2) Then
    '    filters(5) = New Filter("MWPRMode", 2)
    '  Else
    '    filters(5) = New Filter("MWPRMode", 0)
    '  End If

    '  Dim Entities As New ArrayList
    '  If Not Me.m_entity.CostCenter Is Nothing AndAlso Me.m_entity.CostCenter.Originated Then
    '    Entities.Add(Me.m_entity.CostCenter)
    '  End If

    '  Dim view As AbstractEntityPanelViewContent = New PRSelectionView(New PR, New BasketDialog, filters, Entities)
    '  dlg.Lists.Add(view)
    '  Dim myDialog As New Longkong.Pojjaman.Gui.Dialogs.PanelDockingDialog(view, dlg)
    '  myDialog.ShowDialog()
    'End Sub
    Private Sub SetUnit(ByVal unit As ISimpleEntity)
      Me.m_treeManager.SelectedRow("Unit") = unit.Code
    End Sub
    'Private Sub IbtnAddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IbtnAddRow.Click

    '  Dim doc As EquipmentItem '= Me.CurrentTagItem
    '  'If doc Is Nothing Then
    '  doc = New EquipmentItem
    '  Me.m_entity.ItemCollection.Add(doc)
    '  doc.Autogen = True
    '  Me.m_entity.EquipmentItem = doc
    '  'End If

    '  Me.RefreshData()
    '  Me.RefreshDocs()
    '  Me.WorkbenchWindow.ViewContent.IsDirty = True
    'End Sub
    Private Sub SetItems(ByVal items As BasketItemCollection)

      Dim newCode As String = ""
      Dim currentUserId As Integer = Me.SecurityService.CurrentUser.Id

      Me.m_entity.ItemCollection.SetItems(items, newCode, currentUserId)

    End Sub

    'Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
    '  If Me.m_entity.EquipmentItem Is Nothing Then
    '    Return
    '  End If
    '  If Me.m_entity.ItemCollection.Contains(Me.m_entity.EquipmentItem) Then
    '    Me.m_entity.ItemCollection.Remove(Me.m_entity.ItemCollection.IndexOf(Me.m_entity.EquipmentItem))
    '    Me.WorkbenchWindow.ViewContent.IsDirty = True
    '  End If
    '  If Me.m_entity.ItemCollection.Count > 0 Then
    '    Me.m_entity.EquipmentItem = Me.m_entity.ItemCollection(0)
    '    Me.RefreshDocs()
    '    Me.RefreshData()
    '  End If
    'End Sub

    Private Sub ibtnShowUnitDialog1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnShowUnitDialog1.Click
      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New Unit, AddressOf SetUnit1) '******
    End Sub

    Private Sub ibtnShowUnit1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnShowUnit1.Click
      ShowNewPanels(New Unit)
    End Sub

    Private Sub ibtnShowUnit2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      ShowNewPanels(New Unit)
    End Sub

    Private m_chkAutorunCheckChanged As Boolean
    Private Sub chkAutorun_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutorun.CheckedChanged
      If Not m_chkAutorunCheckChanged Then
        UpdateAutogenStatus()
      End If
    End Sub

    Private Sub chkEqAutorun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEqAutoRun.CheckedChanged
      UpdateEqAutogenStatus()
    End Sub

    Private Function GetPRExcludeList() As String
      Dim ret As String = ""
      For Each item As POItem In Me.m_entity.ItemCollection
        If Not item.Pritem Is Nothing Then
          ret &= "|" & item.Pritem.Pr.Id.ToString & ":" & item.Pritem.LineNumber.ToString & "|"
        End If
      Next
      Return ret
    End Function

    'Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
    '  Dim filters(0) As Filter
    '  filters(0) = New Filter("id", 0)
    '  Dim dlg As New BasketDialog
    '  AddHandler dlg.EmptyBasket, AddressOf SetItems

    '  Dim Entities As New ArrayList
    '  Dim view As AbstractEntityPanelViewContent = New GoodsReceiptSelectionView(Me.m_entity, 0, dlg, filters, Entities)
    '  dlg.Lists.Add(view)

    '  Dim myDialog As New Longkong.Pojjaman.Gui.Dialogs.PanelDockingDialog(view, dlg)
    '  myDialog.ShowDialog()
    '  Me.RefreshDocs()
    '  Me.RefreshData()
    '  Me.UpdateAutogenStatus()
    'End Sub

    Private Sub btnPurchaseFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPurchaseFind.Click
      Dim filters(0) As Filter
      filters(0) = New Filter("id", 0)
      Dim dlg As New BasketDialog

      AddHandler dlg.EmptyBasket, AddressOf SetDocCode

      Dim Entities As New ArrayList
      Dim view As AbstractEntityPanelViewContent = New GoodsReceiptSelectionView(Me.m_entity, 0, dlg, filters, Entities)
      dlg.Lists.Add(view)

      Dim myDialog As New Longkong.Pojjaman.Gui.Dialogs.PanelDockingDialog(view, dlg)
      myDialog.ShowDialog()
    End Sub

    Private Sub SetDocCode(ByVal items As BasketItemCollection)

      If Not Me.m_entity.EquipmentItem Is Nothing Then
        Me.m_entity.EquipmentItem.SetDocCode(items)
        Me.RefreshTextData()
      End If

      'Dim newCode As String = ""
      'Dim currentUserId As Integer = Me.SecurityService.CurrentUser.Id

      'Me.m_entity.ItemCollection.SetItems(items, newCode, currentUserId)
      'If Me.m_entity.ItemCollection.Contains(Me.m_entity.ItemCollection(Me.m_entity.ItemCollection.Count - 1)) Then
      '  Me.m_entity.ToolLot = Me.m_entity.ItemCollection(Me.m_entity.ItemCollection.Count - 1)
      'End If
      'Me.WorkbenchWindow.ViewContent.IsDirty = True
    End Sub

    Private Sub ibtnNewLot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnNewLot.Click
      Me.m_entity.EquipmentItem = New EquipmentItem(Me.m_entity)

      m_chkAutorunCheckChanged = True
      Me.m_entity.EquipmentItem.Autogen = True
      Me.chkAutorun.Checked = Me.m_entity.EquipmentItem.Autogen
      'Me.UpdateAutogenStatus()
      Me.RefreshTextData()
      Me.SetLVItemRegular()
      m_chkAutorunCheckChanged = False
    End Sub

    Private Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
      If Me.m_entity Is Nothing OrElse Me.m_entity.EquipmentItem Is Nothing Then
        Return
      End If

      If Me.m_entity.SaveEquipmentItem(SecurityService.CurrentUser.Id) Then
        ibtnNewLot_Click(Nothing, Nothing)
        Me.RefreshListViewData()
        Me.SetLVItemRegular()
      End If

    End Sub

    Private Sub lv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lv.DoubleClick
      Try
        Dim index As Integer = lv.SelectedItems(0).Index

        Me.SetLVItemRegular()

        If Not Me.m_entity.EquipmentItem Is Nothing Then
          Me.m_entity.EquipmentItem = New EquipmentItem(Me.m_entity)

          Me.m_entity.EquipmentItem = CType(lv.SelectedItems(0).Tag, EquipmentItem)
          Me.m_entity.EquipmentItem.LoadImage()
          Me.RefreshTextData()
        End If

        lv.Items(index).Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, 0) '.SubItems(6).Text = "���ѧ��Ѻ��ا"
        Me.ToggleReferenced(Me.m_entity.EquipmentItem)
      Catch ex As Exception

      End Try
    End Sub
    Private Sub ToggleReferenced(ByVal eqi As EquipmentItem)
      Dim isEnable As Boolean = Not eqi.IsReferenced

      ibtnSave.Enabled = isEnable
      ibtnDel.Enabled = isEnable

      cmbCode.Enabled = isEnable
      chkAutorun.Enabled = isEnable
      txtEQIName.Enabled = isEnable
      txtCostcenterCode.Enabled = isEnable

      ibtnCostcenterDialog.Enabled = isEnable

      txtRentalRate.Enabled = isEnable
      txtUnitCode.Enabled = isEnable

      ibtnShowUnit1.Enabled = isEnable

      txtAssetCode.Enabled = isEnable
      btnAssetFind.Enabled = isEnable

      txtlEQIlicense.Enabled = isEnable
      txtModel.Enabled = isEnable
      txtSerialNumber.Enabled = isEnable

      'txtToollotBuyQTY.Enabled = isEnable
      'TxtToollotBuycost.Enabled = isEnable
      'TxtToollotbrand.Enabled = isEnable
      txtDescription.Enabled = isEnable
    End Sub

    Private Sub ibtnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDel.Click
      Dim msgServ As IMessageService = CType(ServiceManager.Services.GetService(GetType(IMessageService)), IMessageService)
      If Me.m_entity Is Nothing OrElse Me.m_entity.EquipmentItem Is Nothing OrElse Not Me.m_entity.EquipmentItem.Originated Then
        Return
      End If

      If Not msgServ.AskQuestionFormatted("${res:Longkong.Pojjaman.Gui.Panels.ToolLotDetailView.ConfirmDelete}", New String() {Me.m_entity.EquipmentItem.Code}) Then
        'If Not msgServ.AskQuestionFormatted("Longkong.Pojjaman.Gui.Panels.ToolLotDetailView.ConfirmDelete", Me.m_entity.ToolLot.Code) Then
        Return
      End If

      If Me.m_entity.DeleteLot() Then
        'Delete Success

        'Me.ClearAllText()
        Me.m_entity.EquipmentItem = New EquipmentItem(Me.m_entity)
        Me.RefreshListViewData()

        m_chkAutorunCheckChanged = True
        Me.m_entity.EquipmentItem.Autogen = True
        Me.chkAutorun.Checked = Me.m_entity.EquipmentItem.Autogen
        Me.UpdateAutogenStatus()
        Me.RefreshTextData()
        Me.SetLVItemRegular()
        m_chkAutorunCheckChanged = False
      End If
    End Sub

  End Class

End Namespace
