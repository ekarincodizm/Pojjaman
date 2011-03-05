Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.TextHelper
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.Services
Imports Longkong.Core.AddIns
Namespace Longkong.Pojjaman.Gui.Panels
  Public Class UserDetailView
    'Inherits UserControl
    Inherits AbstractEntityDetailPanelView
    Implements IHelperCapable, IValidatable

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
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtGroup As System.Windows.Forms.TextBox
    Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblGroup As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Validator As Longkong.Pojjaman.Gui.Components.PJMTextboxValidator
    Friend WithEvents btnResetPassword As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents ibtnBlank As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents ibtnDelRow As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents tgItem As Longkong.Pojjaman.Gui.Components.TreeGrid
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents chkAllCC As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoadImage As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents btnClearImage As Longkong.Pojjaman.Gui.Components.ImageButton
    Friend WithEvents lblPicSize As System.Windows.Forms.Label
    Friend WithEvents lblPR As System.Windows.Forms.Label
    Friend WithEvents lblPO As System.Windows.Forms.Label
    Friend WithEvents lblDO As System.Windows.Forms.Label
    Friend WithEvents grbApproveLevel As Longkong.Pojjaman.Gui.Components.FixedGroupBox
    Friend WithEvents lblLevelPR As System.Windows.Forms.Label
    Friend WithEvents lblLevelPO As System.Windows.Forms.Label
    Friend WithEvents lblLevelDO As System.Windows.Forms.Label
    Friend WithEvents nudLevelApprovePR As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudLevelApprovePO As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudLevelApproveDO As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMaxAmtPR As System.Windows.Forms.Label
    Friend WithEvents lblMaxAmtPO As System.Windows.Forms.Label
    Friend WithEvents lblMaxAmtDO As System.Windows.Forms.Label
    Friend WithEvents lblBaht1 As System.Windows.Forms.Label
    Friend WithEvents lblBaht2 As System.Windows.Forms.Label
    Friend WithEvents lblBaht3 As System.Windows.Forms.Label
    Friend WithEvents txtMaxAmtPR As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxAmtPO As System.Windows.Forms.TextBox
    Friend WithEvents nudLevelApprovePA As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPA As System.Windows.Forms.Label
    Friend WithEvents lblLevelPA As System.Windows.Forms.Label
    Friend WithEvents lblMaxAmtPA As System.Windows.Forms.Label
    Friend WithEvents lblBaht7 As System.Windows.Forms.Label
    Friend WithEvents txtMaxAmtPA As System.Windows.Forms.TextBox
    Friend WithEvents nudLevelApproveDR As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblDR As System.Windows.Forms.Label
    Friend WithEvents lblLevelDR As System.Windows.Forms.Label
    Friend WithEvents lblMaxAmtDR As System.Windows.Forms.Label
    Friend WithEvents lblBaht6 As System.Windows.Forms.Label
    Friend WithEvents txtMaxAmtDR As System.Windows.Forms.TextBox
    Friend WithEvents nudLevelApproveSC As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSC As System.Windows.Forms.Label
    Friend WithEvents lblLevelSC As System.Windows.Forms.Label
    Friend WithEvents lblMaxAmtSC As System.Windows.Forms.Label
    Friend WithEvents lblBaht5 As System.Windows.Forms.Label
    Friend WithEvents txtMaxAmtSC As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxAmtWR As System.Windows.Forms.TextBox
    Friend WithEvents nudLevelApproveWR As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblWR As System.Windows.Forms.Label
    Friend WithEvents lblLevelWR As System.Windows.Forms.Label
    Friend WithEvents lblMaxAmtWR As System.Windows.Forms.Label
    Friend WithEvents lblBaht4 As System.Windows.Forms.Label
    Friend WithEvents lblSignatureImage As System.Windows.Forms.Label
    Friend WithEvents txtMaxAmtDO As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Protected Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserDetailView))
      Me.grbDetail = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.grbApproveLevel = New Longkong.Pojjaman.Gui.Components.FixedGroupBox()
      Me.nudLevelApprovePA = New System.Windows.Forms.NumericUpDown()
      Me.lblPA = New System.Windows.Forms.Label()
      Me.lblLevelPA = New System.Windows.Forms.Label()
      Me.lblMaxAmtPA = New System.Windows.Forms.Label()
      Me.lblBaht7 = New System.Windows.Forms.Label()
      Me.txtMaxAmtPA = New System.Windows.Forms.TextBox()
      Me.nudLevelApproveDR = New System.Windows.Forms.NumericUpDown()
      Me.lblDR = New System.Windows.Forms.Label()
      Me.lblLevelDR = New System.Windows.Forms.Label()
      Me.lblMaxAmtDR = New System.Windows.Forms.Label()
      Me.lblBaht6 = New System.Windows.Forms.Label()
      Me.txtMaxAmtDR = New System.Windows.Forms.TextBox()
      Me.nudLevelApproveSC = New System.Windows.Forms.NumericUpDown()
      Me.lblSC = New System.Windows.Forms.Label()
      Me.lblLevelSC = New System.Windows.Forms.Label()
      Me.lblMaxAmtSC = New System.Windows.Forms.Label()
      Me.lblBaht5 = New System.Windows.Forms.Label()
      Me.txtMaxAmtSC = New System.Windows.Forms.TextBox()
      Me.txtMaxAmtWR = New System.Windows.Forms.TextBox()
      Me.nudLevelApproveWR = New System.Windows.Forms.NumericUpDown()
      Me.lblWR = New System.Windows.Forms.Label()
      Me.lblLevelWR = New System.Windows.Forms.Label()
      Me.lblMaxAmtWR = New System.Windows.Forms.Label()
      Me.lblBaht4 = New System.Windows.Forms.Label()
      Me.txtMaxAmtPR = New System.Windows.Forms.TextBox()
      Me.nudLevelApprovePR = New System.Windows.Forms.NumericUpDown()
      Me.nudLevelApprovePO = New System.Windows.Forms.NumericUpDown()
      Me.nudLevelApproveDO = New System.Windows.Forms.NumericUpDown()
      Me.lblPR = New System.Windows.Forms.Label()
      Me.lblPO = New System.Windows.Forms.Label()
      Me.lblDO = New System.Windows.Forms.Label()
      Me.lblLevelPR = New System.Windows.Forms.Label()
      Me.lblLevelPO = New System.Windows.Forms.Label()
      Me.lblLevelDO = New System.Windows.Forms.Label()
      Me.lblMaxAmtPR = New System.Windows.Forms.Label()
      Me.lblMaxAmtPO = New System.Windows.Forms.Label()
      Me.lblMaxAmtDO = New System.Windows.Forms.Label()
      Me.lblBaht1 = New System.Windows.Forms.Label()
      Me.lblBaht2 = New System.Windows.Forms.Label()
      Me.lblBaht3 = New System.Windows.Forms.Label()
      Me.txtMaxAmtPO = New System.Windows.Forms.TextBox()
      Me.txtMaxAmtDO = New System.Windows.Forms.TextBox()
      Me.lblPicSize = New System.Windows.Forms.Label()
      Me.btnLoadImage = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.btnClearImage = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.chkAllCC = New System.Windows.Forms.CheckBox()
      Me.ibtnBlank = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.ibtnDelRow = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.tgItem = New Longkong.Pojjaman.Gui.Components.TreeGrid()
      Me.lblItem = New System.Windows.Forms.Label()
      Me.picImage = New System.Windows.Forms.PictureBox()
      Me.btnResetPassword = New Longkong.Pojjaman.Gui.Components.ImageButton()
      Me.lblPassword = New System.Windows.Forms.Label()
      Me.txtPassword = New System.Windows.Forms.TextBox()
      Me.lblGroup = New System.Windows.Forms.Label()
      Me.txtGroup = New System.Windows.Forms.TextBox()
      Me.txtGroupName = New System.Windows.Forms.TextBox()
      Me.txtName = New System.Windows.Forms.TextBox()
      Me.lblName = New System.Windows.Forms.Label()
      Me.lblCode = New System.Windows.Forms.Label()
      Me.txtCode = New System.Windows.Forms.TextBox()
      Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
      Me.Validator = New Longkong.Pojjaman.Gui.Components.PJMTextboxValidator(Me.components)
      Me.lblSignatureImage = New System.Windows.Forms.Label()
      Me.grbDetail.SuspendLayout()
      Me.grbApproveLevel.SuspendLayout()
      CType(Me.nudLevelApprovePA, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.nudLevelApproveDR, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.nudLevelApproveSC, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.nudLevelApproveWR, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.nudLevelApprovePR, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.nudLevelApprovePO, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.nudLevelApproveDO, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.tgItem, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'grbDetail
      '
      Me.grbDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.grbDetail.Controls.Add(Me.lblSignatureImage)
      Me.grbDetail.Controls.Add(Me.grbApproveLevel)
      Me.grbDetail.Controls.Add(Me.lblPicSize)
      Me.grbDetail.Controls.Add(Me.btnLoadImage)
      Me.grbDetail.Controls.Add(Me.btnClearImage)
      Me.grbDetail.Controls.Add(Me.chkAllCC)
      Me.grbDetail.Controls.Add(Me.ibtnBlank)
      Me.grbDetail.Controls.Add(Me.ibtnDelRow)
      Me.grbDetail.Controls.Add(Me.tgItem)
      Me.grbDetail.Controls.Add(Me.lblItem)
      Me.grbDetail.Controls.Add(Me.picImage)
      Me.grbDetail.Controls.Add(Me.btnResetPassword)
      Me.grbDetail.Controls.Add(Me.lblPassword)
      Me.grbDetail.Controls.Add(Me.txtPassword)
      Me.grbDetail.Controls.Add(Me.lblGroup)
      Me.grbDetail.Controls.Add(Me.txtGroup)
      Me.grbDetail.Controls.Add(Me.txtGroupName)
      Me.grbDetail.Controls.Add(Me.txtName)
      Me.grbDetail.Controls.Add(Me.lblName)
      Me.grbDetail.Controls.Add(Me.lblCode)
      Me.grbDetail.Controls.Add(Me.txtCode)
      Me.grbDetail.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.grbDetail.ForeColor = System.Drawing.Color.Blue
      Me.grbDetail.Location = New System.Drawing.Point(8, 8)
      Me.grbDetail.Name = "grbDetail"
      Me.grbDetail.Size = New System.Drawing.Size(592, 608)
      Me.grbDetail.TabIndex = 0
      Me.grbDetail.TabStop = False
      Me.grbDetail.Text = "grbDetail: "
      '
      'grbApproveLevel
      '
      Me.grbApproveLevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.grbApproveLevel.Controls.Add(Me.nudLevelApprovePA)
      Me.grbApproveLevel.Controls.Add(Me.lblPA)
      Me.grbApproveLevel.Controls.Add(Me.lblLevelPA)
      Me.grbApproveLevel.Controls.Add(Me.lblMaxAmtPA)
      Me.grbApproveLevel.Controls.Add(Me.lblBaht7)
      Me.grbApproveLevel.Controls.Add(Me.txtMaxAmtPA)
      Me.grbApproveLevel.Controls.Add(Me.nudLevelApproveDR)
      Me.grbApproveLevel.Controls.Add(Me.lblDR)
      Me.grbApproveLevel.Controls.Add(Me.lblLevelDR)
      Me.grbApproveLevel.Controls.Add(Me.lblMaxAmtDR)
      Me.grbApproveLevel.Controls.Add(Me.lblBaht6)
      Me.grbApproveLevel.Controls.Add(Me.txtMaxAmtDR)
      Me.grbApproveLevel.Controls.Add(Me.nudLevelApproveSC)
      Me.grbApproveLevel.Controls.Add(Me.lblSC)
      Me.grbApproveLevel.Controls.Add(Me.lblLevelSC)
      Me.grbApproveLevel.Controls.Add(Me.lblMaxAmtSC)
      Me.grbApproveLevel.Controls.Add(Me.lblBaht5)
      Me.grbApproveLevel.Controls.Add(Me.txtMaxAmtSC)
      Me.grbApproveLevel.Controls.Add(Me.txtMaxAmtWR)
      Me.grbApproveLevel.Controls.Add(Me.nudLevelApproveWR)
      Me.grbApproveLevel.Controls.Add(Me.lblWR)
      Me.grbApproveLevel.Controls.Add(Me.lblLevelWR)
      Me.grbApproveLevel.Controls.Add(Me.lblMaxAmtWR)
      Me.grbApproveLevel.Controls.Add(Me.lblBaht4)
      Me.grbApproveLevel.Controls.Add(Me.txtMaxAmtPR)
      Me.grbApproveLevel.Controls.Add(Me.nudLevelApprovePR)
      Me.grbApproveLevel.Controls.Add(Me.nudLevelApprovePO)
      Me.grbApproveLevel.Controls.Add(Me.nudLevelApproveDO)
      Me.grbApproveLevel.Controls.Add(Me.lblPR)
      Me.grbApproveLevel.Controls.Add(Me.lblPO)
      Me.grbApproveLevel.Controls.Add(Me.lblDO)
      Me.grbApproveLevel.Controls.Add(Me.lblLevelPR)
      Me.grbApproveLevel.Controls.Add(Me.lblLevelPO)
      Me.grbApproveLevel.Controls.Add(Me.lblLevelDO)
      Me.grbApproveLevel.Controls.Add(Me.lblMaxAmtPR)
      Me.grbApproveLevel.Controls.Add(Me.lblMaxAmtPO)
      Me.grbApproveLevel.Controls.Add(Me.lblMaxAmtDO)
      Me.grbApproveLevel.Controls.Add(Me.lblBaht1)
      Me.grbApproveLevel.Controls.Add(Me.lblBaht2)
      Me.grbApproveLevel.Controls.Add(Me.lblBaht3)
      Me.grbApproveLevel.Controls.Add(Me.txtMaxAmtPO)
      Me.grbApproveLevel.Controls.Add(Me.txtMaxAmtDO)
      Me.grbApproveLevel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.grbApproveLevel.Location = New System.Drawing.Point(8, 392)
      Me.grbApproveLevel.Name = "grbApproveLevel"
      Me.grbApproveLevel.Size = New System.Drawing.Size(536, 203)
      Me.grbApproveLevel.TabIndex = 221
      Me.grbApproveLevel.TabStop = False
      Me.grbApproveLevel.Text = "�дѺ�Է�ԡ��͹��ѵ��͡���"
      '
      'nudLevelApprovePA
      '
      Me.nudLevelApprovePA.Location = New System.Drawing.Point(208, 170)
      Me.nudLevelApprovePA.Name = "nudLevelApprovePA"
      Me.nudLevelApprovePA.Size = New System.Drawing.Size(48, 21)
      Me.nudLevelApprovePA.TabIndex = 28
      Me.nudLevelApprovePA.Tag = "NotGigaSite"
      '
      'lblPA
      '
      Me.lblPA.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblPA.Location = New System.Drawing.Point(24, 168)
      Me.lblPA.Name = "lblPA"
      Me.lblPA.Size = New System.Drawing.Size(104, 24)
      Me.lblPA.TabIndex = 27
      Me.lblPA.Tag = "NotGigaSite"
      Me.lblPA.Text = "��Ѻ�ҹ (PA)"
      Me.lblPA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblLevelPA
      '
      Me.lblLevelPA.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblLevelPA.Location = New System.Drawing.Point(120, 168)
      Me.lblLevelPA.Name = "lblLevelPA"
      Me.lblLevelPA.Size = New System.Drawing.Size(80, 24)
      Me.lblLevelPA.TabIndex = 26
      Me.lblLevelPA.Tag = "NotGigaSite"
      Me.lblLevelPA.Text = "�дѺ�Է��:"
      Me.lblLevelPA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblMaxAmtPA
      '
      Me.lblMaxAmtPA.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblMaxAmtPA.Location = New System.Drawing.Point(264, 168)
      Me.lblMaxAmtPA.Name = "lblMaxAmtPA"
      Me.lblMaxAmtPA.Size = New System.Drawing.Size(128, 24)
      Me.lblMaxAmtPA.TabIndex = 25
      Me.lblMaxAmtPA.Tag = "NotGigaSite"
      Me.lblMaxAmtPA.Text = "ǧ�Թ�٧�ش���͹��ѵ���"
      Me.lblMaxAmtPA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblBaht7
      '
      Me.lblBaht7.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblBaht7.Location = New System.Drawing.Point(488, 168)
      Me.lblBaht7.Name = "lblBaht7"
      Me.lblBaht7.Size = New System.Drawing.Size(40, 24)
      Me.lblBaht7.TabIndex = 24
      Me.lblBaht7.Tag = "NotGigaSite"
      Me.lblBaht7.Text = "�ҷ"
      Me.lblBaht7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'txtMaxAmtPA
      '
      Me.Validator.SetDataType(Me.txtMaxAmtPA, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtMaxAmtPA, "")
      Me.Validator.SetGotFocusBackColor(Me.txtMaxAmtPA, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtMaxAmtPA, System.Drawing.Color.Empty)
      Me.txtMaxAmtPA.Location = New System.Drawing.Point(400, 170)
      Me.Validator.SetMinValue(Me.txtMaxAmtPA, "")
      Me.txtMaxAmtPA.Name = "txtMaxAmtPA"
      Me.Validator.SetRegularExpression(Me.txtMaxAmtPA, "")
      Me.Validator.SetRequired(Me.txtMaxAmtPA, False)
      Me.txtMaxAmtPA.Size = New System.Drawing.Size(80, 21)
      Me.txtMaxAmtPA.TabIndex = 29
      Me.txtMaxAmtPA.Tag = "NotGigaSite"
      Me.txtMaxAmtPA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'nudLevelApproveDR
      '
      Me.nudLevelApproveDR.Location = New System.Drawing.Point(208, 122)
      Me.nudLevelApproveDR.Name = "nudLevelApproveDR"
      Me.nudLevelApproveDR.Size = New System.Drawing.Size(48, 21)
      Me.nudLevelApproveDR.TabIndex = 22
      Me.nudLevelApproveDR.Tag = "NotGigaSite"
      '
      'lblDR
      '
      Me.lblDR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblDR.Location = New System.Drawing.Point(24, 120)
      Me.lblDR.Name = "lblDR"
      Me.lblDR.Size = New System.Drawing.Size(104, 24)
      Me.lblDR.TabIndex = 21
      Me.lblDR.Tag = "NotGigaSite"
      Me.lblDR.Text = "��ѡ (DR)"
      Me.lblDR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblLevelDR
      '
      Me.lblLevelDR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblLevelDR.Location = New System.Drawing.Point(120, 120)
      Me.lblLevelDR.Name = "lblLevelDR"
      Me.lblLevelDR.Size = New System.Drawing.Size(80, 24)
      Me.lblLevelDR.TabIndex = 20
      Me.lblLevelDR.Tag = "NotGigaSite"
      Me.lblLevelDR.Text = "�дѺ�Է��:"
      Me.lblLevelDR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblMaxAmtDR
      '
      Me.lblMaxAmtDR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblMaxAmtDR.Location = New System.Drawing.Point(264, 120)
      Me.lblMaxAmtDR.Name = "lblMaxAmtDR"
      Me.lblMaxAmtDR.Size = New System.Drawing.Size(128, 24)
      Me.lblMaxAmtDR.TabIndex = 19
      Me.lblMaxAmtDR.Tag = "NotGigaSite"
      Me.lblMaxAmtDR.Text = "ǧ�Թ�٧�ش���͹��ѵ���"
      Me.lblMaxAmtDR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblBaht6
      '
      Me.lblBaht6.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblBaht6.Location = New System.Drawing.Point(488, 120)
      Me.lblBaht6.Name = "lblBaht6"
      Me.lblBaht6.Size = New System.Drawing.Size(40, 24)
      Me.lblBaht6.TabIndex = 18
      Me.lblBaht6.Tag = "NotGigaSite"
      Me.lblBaht6.Text = "�ҷ"
      Me.lblBaht6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'txtMaxAmtDR
      '
      Me.Validator.SetDataType(Me.txtMaxAmtDR, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtMaxAmtDR, "")
      Me.Validator.SetGotFocusBackColor(Me.txtMaxAmtDR, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtMaxAmtDR, System.Drawing.Color.Empty)
      Me.txtMaxAmtDR.Location = New System.Drawing.Point(400, 122)
      Me.Validator.SetMinValue(Me.txtMaxAmtDR, "")
      Me.txtMaxAmtDR.Name = "txtMaxAmtDR"
      Me.Validator.SetRegularExpression(Me.txtMaxAmtDR, "")
      Me.Validator.SetRequired(Me.txtMaxAmtDR, False)
      Me.txtMaxAmtDR.Size = New System.Drawing.Size(80, 21)
      Me.txtMaxAmtDR.TabIndex = 23
      Me.txtMaxAmtDR.Tag = "NotGigaSite"
      Me.txtMaxAmtDR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'nudLevelApproveSC
      '
      Me.nudLevelApproveSC.Location = New System.Drawing.Point(208, 98)
      Me.nudLevelApproveSC.Name = "nudLevelApproveSC"
      Me.nudLevelApproveSC.Size = New System.Drawing.Size(48, 21)
      Me.nudLevelApproveSC.TabIndex = 16
      Me.nudLevelApproveSC.Tag = "NotGigaSite"
      '
      'lblSC
      '
      Me.lblSC.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblSC.Location = New System.Drawing.Point(24, 96)
      Me.lblSC.Name = "lblSC"
      Me.lblSC.Size = New System.Drawing.Size(104, 24)
      Me.lblSC.TabIndex = 15
      Me.lblSC.Tag = "NotGigaSite"
      Me.lblSC.Text = "���駨�ҧ (SC,VO)"
      Me.lblSC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblLevelSC
      '
      Me.lblLevelSC.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblLevelSC.Location = New System.Drawing.Point(120, 96)
      Me.lblLevelSC.Name = "lblLevelSC"
      Me.lblLevelSC.Size = New System.Drawing.Size(80, 24)
      Me.lblLevelSC.TabIndex = 14
      Me.lblLevelSC.Tag = "NotGigaSite"
      Me.lblLevelSC.Text = "�дѺ�Է��:"
      Me.lblLevelSC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblMaxAmtSC
      '
      Me.lblMaxAmtSC.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblMaxAmtSC.Location = New System.Drawing.Point(264, 96)
      Me.lblMaxAmtSC.Name = "lblMaxAmtSC"
      Me.lblMaxAmtSC.Size = New System.Drawing.Size(128, 24)
      Me.lblMaxAmtSC.TabIndex = 13
      Me.lblMaxAmtSC.Tag = "NotGigaSite"
      Me.lblMaxAmtSC.Text = "ǧ�Թ�٧�ش���͹��ѵ���"
      Me.lblMaxAmtSC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblBaht5
      '
      Me.lblBaht5.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblBaht5.Location = New System.Drawing.Point(488, 96)
      Me.lblBaht5.Name = "lblBaht5"
      Me.lblBaht5.Size = New System.Drawing.Size(40, 24)
      Me.lblBaht5.TabIndex = 12
      Me.lblBaht5.Tag = "NotGigaSite"
      Me.lblBaht5.Text = "�ҷ"
      Me.lblBaht5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'txtMaxAmtSC
      '
      Me.Validator.SetDataType(Me.txtMaxAmtSC, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtMaxAmtSC, "")
      Me.Validator.SetGotFocusBackColor(Me.txtMaxAmtSC, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtMaxAmtSC, System.Drawing.Color.Empty)
      Me.txtMaxAmtSC.Location = New System.Drawing.Point(400, 98)
      Me.Validator.SetMinValue(Me.txtMaxAmtSC, "")
      Me.txtMaxAmtSC.Name = "txtMaxAmtSC"
      Me.Validator.SetRegularExpression(Me.txtMaxAmtSC, "")
      Me.Validator.SetRequired(Me.txtMaxAmtSC, False)
      Me.txtMaxAmtSC.Size = New System.Drawing.Size(80, 21)
      Me.txtMaxAmtSC.TabIndex = 17
      Me.txtMaxAmtSC.Tag = "NotGigaSite"
      Me.txtMaxAmtSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'txtMaxAmtWR
      '
      Me.Validator.SetDataType(Me.txtMaxAmtWR, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtMaxAmtWR, "")
      Me.Validator.SetGotFocusBackColor(Me.txtMaxAmtWR, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtMaxAmtWR, System.Drawing.Color.Empty)
      Me.txtMaxAmtWR.Location = New System.Drawing.Point(400, 50)
      Me.Validator.SetMinValue(Me.txtMaxAmtWR, "")
      Me.txtMaxAmtWR.Name = "txtMaxAmtWR"
      Me.Validator.SetRegularExpression(Me.txtMaxAmtWR, "")
      Me.Validator.SetRequired(Me.txtMaxAmtWR, False)
      Me.txtMaxAmtWR.Size = New System.Drawing.Size(80, 21)
      Me.txtMaxAmtWR.TabIndex = 11
      Me.txtMaxAmtWR.Tag = "NotGigaSite"
      Me.txtMaxAmtWR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'nudLevelApproveWR
      '
      Me.nudLevelApproveWR.Location = New System.Drawing.Point(208, 50)
      Me.nudLevelApproveWR.Name = "nudLevelApproveWR"
      Me.nudLevelApproveWR.Size = New System.Drawing.Size(48, 21)
      Me.nudLevelApproveWR.TabIndex = 10
      Me.nudLevelApproveWR.Tag = "NotGigaSite"
      '
      'lblWR
      '
      Me.lblWR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblWR.Location = New System.Drawing.Point(24, 48)
      Me.lblWR.Name = "lblWR"
      Me.lblWR.Size = New System.Drawing.Size(104, 24)
      Me.lblWR.TabIndex = 9
      Me.lblWR.Tag = "NotGigaSite"
      Me.lblWR.Text = "㺢ͨ�ҧ (WR)"
      Me.lblWR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblLevelWR
      '
      Me.lblLevelWR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblLevelWR.Location = New System.Drawing.Point(120, 48)
      Me.lblLevelWR.Name = "lblLevelWR"
      Me.lblLevelWR.Size = New System.Drawing.Size(80, 24)
      Me.lblLevelWR.TabIndex = 6
      Me.lblLevelWR.Tag = "NotGigaSite"
      Me.lblLevelWR.Text = "�дѺ�Է��:"
      Me.lblLevelWR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblMaxAmtWR
      '
      Me.lblMaxAmtWR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblMaxAmtWR.Location = New System.Drawing.Point(264, 48)
      Me.lblMaxAmtWR.Name = "lblMaxAmtWR"
      Me.lblMaxAmtWR.Size = New System.Drawing.Size(128, 24)
      Me.lblMaxAmtWR.TabIndex = 7
      Me.lblMaxAmtWR.Tag = "NotGigaSite"
      Me.lblMaxAmtWR.Text = "ǧ�Թ�٧�ش���͹��ѵ���"
      Me.lblMaxAmtWR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblBaht4
      '
      Me.lblBaht4.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblBaht4.Location = New System.Drawing.Point(488, 48)
      Me.lblBaht4.Name = "lblBaht4"
      Me.lblBaht4.Size = New System.Drawing.Size(40, 24)
      Me.lblBaht4.TabIndex = 8
      Me.lblBaht4.Tag = "NotGigaSite"
      Me.lblBaht4.Text = "�ҷ"
      Me.lblBaht4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'txtMaxAmtPR
      '
      Me.Validator.SetDataType(Me.txtMaxAmtPR, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtMaxAmtPR, "")
      Me.Validator.SetGotFocusBackColor(Me.txtMaxAmtPR, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtMaxAmtPR, System.Drawing.Color.Empty)
      Me.txtMaxAmtPR.Location = New System.Drawing.Point(400, 26)
      Me.Validator.SetMinValue(Me.txtMaxAmtPR, "")
      Me.txtMaxAmtPR.Name = "txtMaxAmtPR"
      Me.Validator.SetRegularExpression(Me.txtMaxAmtPR, "")
      Me.Validator.SetRequired(Me.txtMaxAmtPR, False)
      Me.txtMaxAmtPR.Size = New System.Drawing.Size(80, 21)
      Me.txtMaxAmtPR.TabIndex = 1
      Me.txtMaxAmtPR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'nudLevelApprovePR
      '
      Me.nudLevelApprovePR.Location = New System.Drawing.Point(208, 26)
      Me.nudLevelApprovePR.Name = "nudLevelApprovePR"
      Me.nudLevelApprovePR.Size = New System.Drawing.Size(48, 21)
      Me.nudLevelApprovePR.TabIndex = 0
      '
      'nudLevelApprovePO
      '
      Me.nudLevelApprovePO.Location = New System.Drawing.Point(208, 74)
      Me.nudLevelApprovePO.Name = "nudLevelApprovePO"
      Me.nudLevelApprovePO.Size = New System.Drawing.Size(48, 21)
      Me.nudLevelApprovePO.TabIndex = 2
      '
      'nudLevelApproveDO
      '
      Me.nudLevelApproveDO.Location = New System.Drawing.Point(208, 146)
      Me.nudLevelApproveDO.Name = "nudLevelApproveDO"
      Me.nudLevelApproveDO.Size = New System.Drawing.Size(48, 21)
      Me.nudLevelApproveDO.TabIndex = 4
      '
      'lblPR
      '
      Me.lblPR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblPR.Location = New System.Drawing.Point(24, 24)
      Me.lblPR.Name = "lblPR"
      Me.lblPR.Size = New System.Drawing.Size(104, 24)
      Me.lblPR.TabIndex = 0
      Me.lblPR.Text = "㺢ͫ��� (PR)"
      Me.lblPR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblPO
      '
      Me.lblPO.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblPO.Location = New System.Drawing.Point(24, 72)
      Me.lblPO.Name = "lblPO"
      Me.lblPO.Size = New System.Drawing.Size(104, 24)
      Me.lblPO.TabIndex = 0
      Me.lblPO.Text = "���駫��� (PO)"
      Me.lblPO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblDO
      '
      Me.lblDO.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblDO.Location = New System.Drawing.Point(24, 144)
      Me.lblDO.Name = "lblDO"
      Me.lblDO.Size = New System.Drawing.Size(104, 24)
      Me.lblDO.TabIndex = 0
      Me.lblDO.Text = "��Ѻ�ͧ (DO)"
      Me.lblDO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblLevelPR
      '
      Me.lblLevelPR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblLevelPR.Location = New System.Drawing.Point(120, 24)
      Me.lblLevelPR.Name = "lblLevelPR"
      Me.lblLevelPR.Size = New System.Drawing.Size(80, 24)
      Me.lblLevelPR.TabIndex = 0
      Me.lblLevelPR.Text = "�дѺ�Է��:"
      Me.lblLevelPR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblLevelPO
      '
      Me.lblLevelPO.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblLevelPO.Location = New System.Drawing.Point(120, 72)
      Me.lblLevelPO.Name = "lblLevelPO"
      Me.lblLevelPO.Size = New System.Drawing.Size(80, 24)
      Me.lblLevelPO.TabIndex = 0
      Me.lblLevelPO.Text = "�дѺ�Է��:"
      Me.lblLevelPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblLevelDO
      '
      Me.lblLevelDO.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblLevelDO.Location = New System.Drawing.Point(120, 144)
      Me.lblLevelDO.Name = "lblLevelDO"
      Me.lblLevelDO.Size = New System.Drawing.Size(80, 24)
      Me.lblLevelDO.TabIndex = 0
      Me.lblLevelDO.Text = "�дѺ�Է��:"
      Me.lblLevelDO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblMaxAmtPR
      '
      Me.lblMaxAmtPR.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblMaxAmtPR.Location = New System.Drawing.Point(264, 24)
      Me.lblMaxAmtPR.Name = "lblMaxAmtPR"
      Me.lblMaxAmtPR.Size = New System.Drawing.Size(128, 24)
      Me.lblMaxAmtPR.TabIndex = 0
      Me.lblMaxAmtPR.Text = "ǧ�Թ�٧�ش���͹��ѵ���"
      Me.lblMaxAmtPR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblMaxAmtPO
      '
      Me.lblMaxAmtPO.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblMaxAmtPO.Location = New System.Drawing.Point(264, 72)
      Me.lblMaxAmtPO.Name = "lblMaxAmtPO"
      Me.lblMaxAmtPO.Size = New System.Drawing.Size(128, 24)
      Me.lblMaxAmtPO.TabIndex = 0
      Me.lblMaxAmtPO.Text = "ǧ�Թ�٧�ش���͹��ѵ���"
      Me.lblMaxAmtPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblMaxAmtDO
      '
      Me.lblMaxAmtDO.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblMaxAmtDO.Location = New System.Drawing.Point(264, 144)
      Me.lblMaxAmtDO.Name = "lblMaxAmtDO"
      Me.lblMaxAmtDO.Size = New System.Drawing.Size(128, 24)
      Me.lblMaxAmtDO.TabIndex = 0
      Me.lblMaxAmtDO.Text = "ǧ�Թ�٧�ش���͹��ѵ���"
      Me.lblMaxAmtDO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblBaht1
      '
      Me.lblBaht1.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblBaht1.Location = New System.Drawing.Point(488, 24)
      Me.lblBaht1.Name = "lblBaht1"
      Me.lblBaht1.Size = New System.Drawing.Size(40, 24)
      Me.lblBaht1.TabIndex = 0
      Me.lblBaht1.Text = "�ҷ"
      Me.lblBaht1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblBaht2
      '
      Me.lblBaht2.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblBaht2.Location = New System.Drawing.Point(488, 72)
      Me.lblBaht2.Name = "lblBaht2"
      Me.lblBaht2.Size = New System.Drawing.Size(40, 24)
      Me.lblBaht2.TabIndex = 0
      Me.lblBaht2.Text = "�ҷ"
      Me.lblBaht2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'lblBaht3
      '
      Me.lblBaht3.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblBaht3.Location = New System.Drawing.Point(488, 144)
      Me.lblBaht3.Name = "lblBaht3"
      Me.lblBaht3.Size = New System.Drawing.Size(40, 24)
      Me.lblBaht3.TabIndex = 0
      Me.lblBaht3.Text = "�ҷ"
      Me.lblBaht3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'txtMaxAmtPO
      '
      Me.Validator.SetDataType(Me.txtMaxAmtPO, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtMaxAmtPO, "")
      Me.Validator.SetGotFocusBackColor(Me.txtMaxAmtPO, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtMaxAmtPO, System.Drawing.Color.Empty)
      Me.txtMaxAmtPO.Location = New System.Drawing.Point(400, 74)
      Me.Validator.SetMinValue(Me.txtMaxAmtPO, "")
      Me.txtMaxAmtPO.Name = "txtMaxAmtPO"
      Me.Validator.SetRegularExpression(Me.txtMaxAmtPO, "")
      Me.Validator.SetRequired(Me.txtMaxAmtPO, False)
      Me.txtMaxAmtPO.Size = New System.Drawing.Size(80, 21)
      Me.txtMaxAmtPO.TabIndex = 3
      Me.txtMaxAmtPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'txtMaxAmtDO
      '
      Me.Validator.SetDataType(Me.txtMaxAmtDO, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtMaxAmtDO, "")
      Me.Validator.SetGotFocusBackColor(Me.txtMaxAmtDO, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtMaxAmtDO, System.Drawing.Color.Empty)
      Me.txtMaxAmtDO.Location = New System.Drawing.Point(400, 146)
      Me.Validator.SetMinValue(Me.txtMaxAmtDO, "")
      Me.txtMaxAmtDO.Name = "txtMaxAmtDO"
      Me.Validator.SetRegularExpression(Me.txtMaxAmtDO, "")
      Me.Validator.SetRequired(Me.txtMaxAmtDO, False)
      Me.txtMaxAmtDO.Size = New System.Drawing.Size(80, 21)
      Me.txtMaxAmtDO.TabIndex = 5
      Me.txtMaxAmtDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      '
      'lblPicSize
      '
      Me.lblPicSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.lblPicSize.Location = New System.Drawing.Point(430, 48)
      Me.lblPicSize.Name = "lblPicSize"
      Me.lblPicSize.Size = New System.Drawing.Size(100, 23)
      Me.lblPicSize.TabIndex = 220
      Me.lblPicSize.Text = "160 X 88 pixel"
      Me.lblPicSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'btnLoadImage
      '
      Me.btnLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnLoadImage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnLoadImage.ForeColor = System.Drawing.SystemColors.Control
      Me.btnLoadImage.Location = New System.Drawing.Point(512, 108)
      Me.btnLoadImage.Name = "btnLoadImage"
      Me.btnLoadImage.Size = New System.Drawing.Size(24, 23)
      Me.btnLoadImage.TabIndex = 218
      Me.btnLoadImage.TabStop = False
      Me.btnLoadImage.ThemedImage = CType(resources.GetObject("btnLoadImage.ThemedImage"), System.Drawing.Bitmap)
      '
      'btnClearImage
      '
      Me.btnClearImage.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnClearImage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnClearImage.Location = New System.Drawing.Point(536, 108)
      Me.btnClearImage.Name = "btnClearImage"
      Me.btnClearImage.Size = New System.Drawing.Size(24, 23)
      Me.btnClearImage.TabIndex = 219
      Me.btnClearImage.TabStop = False
      Me.btnClearImage.ThemedImage = CType(resources.GetObject("btnClearImage.ThemedImage"), System.Drawing.Bitmap)
      '
      'chkAllCC
      '
      Me.chkAllCC.ForeColor = System.Drawing.SystemColors.ControlText
      Me.chkAllCC.Location = New System.Drawing.Point(362, 137)
      Me.chkAllCC.Name = "chkAllCC"
      Me.chkAllCC.Size = New System.Drawing.Size(184, 16)
      Me.chkAllCC.TabIndex = 217
      Me.chkAllCC.Text = "����ö��ҹ��ء Cost Center"
      '
      'ibtnBlank
      '
      Me.ibtnBlank.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ibtnBlank.Location = New System.Drawing.Point(546, 153)
      Me.ibtnBlank.Name = "ibtnBlank"
      Me.ibtnBlank.Size = New System.Drawing.Size(24, 24)
      Me.ibtnBlank.TabIndex = 216
      Me.ibtnBlank.TabStop = False
      Me.ibtnBlank.ThemedImage = CType(resources.GetObject("ibtnBlank.ThemedImage"), System.Drawing.Bitmap)
      '
      'ibtnDelRow
      '
      Me.ibtnDelRow.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ibtnDelRow.Location = New System.Drawing.Point(546, 177)
      Me.ibtnDelRow.Name = "ibtnDelRow"
      Me.ibtnDelRow.Size = New System.Drawing.Size(24, 24)
      Me.ibtnDelRow.TabIndex = 215
      Me.ibtnDelRow.TabStop = False
      Me.ibtnDelRow.ThemedImage = CType(resources.GetObject("ibtnDelRow.ThemedImage"), System.Drawing.Bitmap)
      '
      'tgItem
      '
      Me.tgItem.AllowNew = False
      Me.tgItem.AllowSorting = False
      Me.tgItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.tgItem.AutoColumnResize = True
      Me.tgItem.CaptionVisible = False
      Me.tgItem.Cellchanged = False
      Me.tgItem.DataMember = ""
      Me.tgItem.HeaderForeColor = System.Drawing.SystemColors.ControlText
      Me.tgItem.Location = New System.Drawing.Point(10, 153)
      Me.tgItem.Name = "tgItem"
      Me.tgItem.Size = New System.Drawing.Size(536, 232)
      Me.tgItem.SortingArrowColor = System.Drawing.Color.Red
      Me.tgItem.TabIndex = 213
      Me.tgItem.TreeManager = Nothing
      '
      'lblItem
      '
      Me.lblItem.BackColor = System.Drawing.Color.Transparent
      Me.lblItem.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblItem.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblItem.Location = New System.Drawing.Point(10, 137)
      Me.lblItem.Name = "lblItem"
      Me.lblItem.Size = New System.Drawing.Size(208, 18)
      Me.lblItem.TabIndex = 214
      Me.lblItem.Text = "Cost Center ������Է�����ҹ:"
      Me.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'picImage
      '
      Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.picImage.Location = New System.Drawing.Point(400, 16)
      Me.picImage.Name = "picImage"
      Me.picImage.Size = New System.Drawing.Size(160, 88)
      Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
      Me.picImage.TabIndex = 18
      Me.picImage.TabStop = False
      '
      'btnResetPassword
      '
      Me.btnResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.btnResetPassword.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.btnResetPassword.ForeColor = System.Drawing.SystemColors.Control
      Me.btnResetPassword.Location = New System.Drawing.Point(352, 88)
      Me.btnResetPassword.Name = "btnResetPassword"
      Me.btnResetPassword.Size = New System.Drawing.Size(21, 21)
      Me.btnResetPassword.TabIndex = 17
      Me.btnResetPassword.TabStop = False
      Me.btnResetPassword.ThemedImage = CType(resources.GetObject("btnResetPassword.ThemedImage"), System.Drawing.Bitmap)
      '
      'lblPassword
      '
      Me.lblPassword.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblPassword.ForeColor = System.Drawing.Color.Black
      Me.lblPassword.Location = New System.Drawing.Point(8, 88)
      Me.lblPassword.Name = "lblPassword"
      Me.lblPassword.Size = New System.Drawing.Size(80, 18)
      Me.lblPassword.TabIndex = 16
      Me.lblPassword.Text = "lblPassword"
      Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtPassword
      '
      Me.Validator.SetDataType(Me.txtPassword, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtPassword, "")
      Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtPassword, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtPassword, System.Drawing.Color.Empty)
      Me.txtPassword.Location = New System.Drawing.Point(88, 87)
      Me.Validator.SetMinValue(Me.txtPassword, "")
      Me.txtPassword.Name = "txtPassword"
      Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
      Me.Validator.SetRegularExpression(Me.txtPassword, "")
      Me.Validator.SetRequired(Me.txtPassword, True)
      Me.txtPassword.Size = New System.Drawing.Size(264, 21)
      Me.txtPassword.TabIndex = 3
      '
      'lblGroup
      '
      Me.lblGroup.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblGroup.ForeColor = System.Drawing.Color.Black
      Me.lblGroup.Location = New System.Drawing.Point(8, 67)
      Me.lblGroup.Name = "lblGroup"
      Me.lblGroup.Size = New System.Drawing.Size(80, 18)
      Me.lblGroup.TabIndex = 12
      Me.lblGroup.Text = "lblGroup"
      Me.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtGroup
      '
      Me.Validator.SetDataType(Me.txtGroup, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtGroup, "")
      Me.txtGroup.Enabled = False
      Me.txtGroup.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtGroup, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtGroup, System.Drawing.Color.Empty)
      Me.txtGroup.Location = New System.Drawing.Point(88, 66)
      Me.Validator.SetMinValue(Me.txtGroup, "")
      Me.txtGroup.Name = "txtGroup"
      Me.txtGroup.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtGroup, "")
      Me.Validator.SetRequired(Me.txtGroup, False)
      Me.txtGroup.Size = New System.Drawing.Size(96, 21)
      Me.txtGroup.TabIndex = 2
      Me.txtGroup.TabStop = False
      '
      'txtGroupName
      '
      Me.Validator.SetDataType(Me.txtGroupName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtGroupName, "")
      Me.txtGroupName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtGroupName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtGroupName, System.Drawing.Color.Empty)
      Me.txtGroupName.Location = New System.Drawing.Point(184, 66)
      Me.Validator.SetMinValue(Me.txtGroupName, "")
      Me.txtGroupName.Name = "txtGroupName"
      Me.txtGroupName.ReadOnly = True
      Me.Validator.SetRegularExpression(Me.txtGroupName, "")
      Me.Validator.SetRequired(Me.txtGroupName, False)
      Me.txtGroupName.Size = New System.Drawing.Size(192, 21)
      Me.txtGroupName.TabIndex = 13
      Me.txtGroupName.TabStop = False
      '
      'txtName
      '
      Me.Validator.SetDataType(Me.txtName, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtName, "")
      Me.txtName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtName, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtName, System.Drawing.Color.Empty)
      Me.txtName.Location = New System.Drawing.Point(88, 45)
      Me.Validator.SetMinValue(Me.txtName, "")
      Me.txtName.Name = "txtName"
      Me.Validator.SetRegularExpression(Me.txtName, "")
      Me.Validator.SetRequired(Me.txtName, True)
      Me.txtName.Size = New System.Drawing.Size(288, 21)
      Me.txtName.TabIndex = 1
      '
      'lblName
      '
      Me.lblName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblName.ForeColor = System.Drawing.Color.Black
      Me.lblName.Location = New System.Drawing.Point(8, 46)
      Me.lblName.Name = "lblName"
      Me.lblName.Size = New System.Drawing.Size(80, 18)
      Me.lblName.TabIndex = 11
      Me.lblName.Text = "lblName"
      Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'lblCode
      '
      Me.lblCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblCode.ForeColor = System.Drawing.Color.Black
      Me.lblCode.Location = New System.Drawing.Point(8, 25)
      Me.lblCode.Name = "lblCode"
      Me.lblCode.Size = New System.Drawing.Size(80, 18)
      Me.lblCode.TabIndex = 7
      Me.lblCode.Text = "lblCode"
      Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'txtCode
      '
      Me.Validator.SetDataType(Me.txtCode, Longkong.Pojjaman.Gui.Components.DataTypeConstants.StringType)
      Me.Validator.SetDisplayName(Me.txtCode, "")
      Me.txtCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Validator.SetGotFocusBackColor(Me.txtCode, System.Drawing.Color.Empty)
      Me.Validator.SetInvalidBackColor(Me.txtCode, System.Drawing.Color.Empty)
      Me.txtCode.Location = New System.Drawing.Point(88, 24)
      Me.Validator.SetMinValue(Me.txtCode, "")
      Me.txtCode.Name = "txtCode"
      Me.Validator.SetRegularExpression(Me.txtCode, "")
      Me.Validator.SetRequired(Me.txtCode, True)
      Me.txtCode.Size = New System.Drawing.Size(96, 21)
      Me.txtCode.TabIndex = 0
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
      Me.Validator.GotFocusBackColor = System.Drawing.Color.Empty
      Me.Validator.HasNewRow = False
      Me.Validator.InvalidBackColor = System.Drawing.Color.Empty
      '
      'lblSignatureImage
      '
      Me.lblSignatureImage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.lblSignatureImage.ForeColor = System.Drawing.Color.Black
      Me.lblSignatureImage.Location = New System.Drawing.Point(397, 107)
      Me.lblSignatureImage.Name = "lblSignatureImage"
      Me.lblSignatureImage.Size = New System.Drawing.Size(87, 18)
      Me.lblSignatureImage.TabIndex = 227
      Me.lblSignatureImage.Text = "���ૹ��:"
      Me.lblSignatureImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'UserDetailView
      '
      Me.Controls.Add(Me.grbDetail)
      Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
      Me.Name = "UserDetailView"
      Me.Size = New System.Drawing.Size(608, 630)
      Me.grbDetail.ResumeLayout(False)
      Me.grbDetail.PerformLayout()
      Me.grbApproveLevel.ResumeLayout(False)
      Me.grbApproveLevel.PerformLayout()
      CType(Me.nudLevelApprovePA, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.nudLevelApproveDR, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.nudLevelApproveSC, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.nudLevelApproveWR, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.nudLevelApprovePR, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.nudLevelApprovePO, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.nudLevelApproveDO, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.tgItem, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Member"
    Private m_User As User
    Private m_isInitialized As Boolean = False
    Private m_helpers As HelperCollection
    Private m_treeManager As TreeManager
#End Region

#Region "Property"

#End Region

#Region "Constructor"
    Public Sub New()
      MyBase.New()
      Me.InitializeComponent()
      Me.SetLabelText()
      Initialize()

      'Check Module
      CheckModuleActivation()

      Dim dt As TreeTable = Me.GetSchemaTable
      Dim dst As DataGridTableStyle = Me.CreateTableStyle
      m_treeManager = New TreeManager(dt, tgItem)
      m_treeManager.SetTableStyle(dst)
      m_treeManager.AllowSorting = False
      m_treeManager.AllowDelete = False

      EventWiring()
      Me.m_helpers = New HelperCollection(Me)
      Me.LoadHelpers()
    End Sub
#End Region

#Region "CheckPJMModule"
    Private m_ApproveDocModule As New PJMModule("approvedoc")
    ReadOnly Property Activated() As Boolean
      Get
        Return m_ApproveDocModule.Activated
      End Get
    End Property
    Public Sub CheckModuleActivation()
      If Not Me.Activated Then
        Me.grbApproveLevel.Visible = False
      End If
    End Sub
#End Region

#Region "Method"

#End Region

#Region "IListDetail"
    ' ��Ǩ�ͺʶҹТͧ�����
    Public Overrides Sub CheckFormEnable()
      Dim secSrv As SecurityService = CType(ServiceManager.Services.GetService(GetType(SecurityService)), SecurityService)
      Dim accessID As Integer = Longkong.Pojjaman.BusinessLogic.Entity.GetAccessIdFromFullClassName(secSrv.CurrentUser.FullClassName)
      If accessID = 0 Then
        Me.btnResetPassword.Enabled = False
        Return
      End If
      Dim level As Integer = secSrv.GetAccess(accessID)
      Dim checkString As String = BinaryHelper.DecToBin(level, 5)
      checkString = BinaryHelper.RevertString(checkString)
      Me.btnResetPassword.Enabled = CBool(checkString.Substring(1, 1))
    End Sub

    ' ������������ control
    Public Overrides Sub ClearDetail()
      txtCode.Text = ""
      txtName.Text = ""
      txtGroup.Text = ""
      txtGroupName.Text = ""
      txtPassword.Text = ""
      'Hack:
      txtPassword.Enabled = True

      '��˹� Maximum ���Ѻ�дѺ Level
      nudLevelApprovePR.Maximum = Configuration.GetConfig("MaxLevelApprovePR")
      nudLevelApproveWR.Maximum = Configuration.GetConfig("MaxLevelApproveWR")
      nudLevelApprovePO.Maximum = Configuration.GetConfig("MaxLevelApprovePO")
      nudLevelApproveSC.Maximum = Configuration.GetConfig("MaxLevelApproveSC")
      nudLevelApproveDR.Maximum = Configuration.GetConfig("MaxLevelApproveDR")
      nudLevelApproveDO.Maximum = Configuration.GetConfig("MaxLevelApproveDO")
      nudLevelApprovePA.Maximum = Configuration.GetConfig("MaxLevelApprovePA")
      Me.m_treeManager.Treetable.Clear()

      '��� save ����� focus ������ ������� dirty
      Me.txtCode.Focus()
    End Sub
    Public Overrides Sub SetLabelText()
      If Not Me.m_User Is Nothing Then Me.Text = Me.StringParserService.Parse(Me.m_User.TabPageText)
      Me.lblCode.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblCode}")
      Me.Validator.SetDisplayName(Me.txtCode, Me.lblCode.Text.TrimEnd(":".ToCharArray))
      Me.lblName.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblName}")
      Me.Validator.SetDisplayName(Me.txtName, Me.lblName.Text.TrimEnd(":".ToCharArray))
      Me.grbDetail.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.grbDetail}")
      Me.lblGroup.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblGroup}")
      Me.lblPassword.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblPassword}")
      Me.lblSignatureImage.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblSignatureImage}")

      Me.lblPR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblPR}")
      Me.lblWR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblWR}")
      Me.lblPO.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblPO}")
      Me.lblSC.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblSC}")
      Me.lblDR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblDR}")
      Me.lblDO.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblDO}")
      Me.lblPA.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblPA}")

      Me.lblLevelPR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblLevelPR}")
      Me.lblLevelWR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblLevelPR}")
      Me.lblLevelPO.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblLevelPO}")
      Me.lblLevelSC.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblLevelPR}")
      Me.lblLevelDR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblLevelPR}")
      Me.lblLevelDO.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblLevelDO}")
      Me.lblLevelPA.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblLevelPR}")

      Me.lblMaxAmtPR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblMaxAmtPR}")
      Me.lblMaxAmtWR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblMaxAmtPR}")
      Me.lblMaxAmtPO.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblMaxAmtPO}")
      Me.lblMaxAmtSC.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblMaxAmtPR}")
      Me.lblMaxAmtDR.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblMaxAmtPR}")
      Me.lblMaxAmtDO.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblMaxAmtDO}")
      Me.lblMaxAmtPA.Text = Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.lblMaxAmtPR}")

      Me.lblBaht1.Text = Me.StringParserService.Parse("${res:Global.CurrencyText}")
      Me.lblBaht2.Text = Me.StringParserService.Parse("${res:Global.CurrencyText}")
      Me.lblBaht3.Text = Me.StringParserService.Parse("${res:Global.CurrencyText}")
      Me.lblBaht4.Text = Me.StringParserService.Parse("${res:Global.CurrencyText}")
      Me.lblBaht5.Text = Me.StringParserService.Parse("${res:Global.CurrencyText}")
      Me.lblBaht6.Text = Me.StringParserService.Parse("${res:Global.CurrencyText}")
      Me.lblBaht7.Text = Me.StringParserService.Parse("${res:Global.CurrencyText}")
    End Sub

    Protected Overrides Sub EventWiring()
      AddHandler txtCode.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtName.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtGroup.TextChanged, AddressOf Me.ChangeProperty
      AddHandler txtPassword.TextChanged, AddressOf Me.ChangeProperty


      AddHandler nudLevelApprovePR.TextChanged, AddressOf Me.TextHandler
      AddHandler nudLevelApprovePR.Click, AddressOf Me.ChangeProperty
      AddHandler nudLevelApprovePR.Validated, AddressOf Me.ChangeProperty
      AddHandler txtMaxAmtPR.TextChanged, AddressOf Me.TextHandler
      AddHandler txtMaxAmtPR.Validated, AddressOf Me.ChangeProperty

      AddHandler nudLevelApproveWR.TextChanged, AddressOf Me.TextHandler
      AddHandler nudLevelApproveWR.Click, AddressOf Me.ChangeProperty
      AddHandler nudLevelApproveWR.Validated, AddressOf Me.ChangeProperty
      AddHandler txtMaxAmtWR.TextChanged, AddressOf Me.TextHandler
      AddHandler txtMaxAmtWR.Validated, AddressOf Me.ChangeProperty

      AddHandler nudLevelApprovePO.TextChanged, AddressOf Me.TextHandler
      AddHandler nudLevelApprovePO.Click, AddressOf Me.ChangeProperty
      AddHandler nudLevelApprovePO.Validated, AddressOf Me.ChangeProperty
      AddHandler txtMaxAmtPO.TextChanged, AddressOf Me.TextHandler
      AddHandler txtMaxAmtPO.Validated, AddressOf Me.ChangeProperty

      AddHandler nudLevelApproveSC.TextChanged, AddressOf Me.TextHandler
      AddHandler nudLevelApproveSC.Click, AddressOf Me.ChangeProperty
      AddHandler nudLevelApproveSC.Validated, AddressOf Me.ChangeProperty
      AddHandler txtMaxAmtSC.TextChanged, AddressOf Me.TextHandler
      AddHandler txtMaxAmtSC.Validated, AddressOf Me.ChangeProperty

      AddHandler nudLevelApproveDR.TextChanged, AddressOf Me.TextHandler
      AddHandler nudLevelApproveDR.Click, AddressOf Me.ChangeProperty
      AddHandler nudLevelApproveDR.Validated, AddressOf Me.ChangeProperty
      AddHandler txtMaxAmtDR.TextChanged, AddressOf Me.TextHandler
      AddHandler txtMaxAmtDR.Validated, AddressOf Me.ChangeProperty

      AddHandler nudLevelApproveDO.TextChanged, AddressOf Me.TextHandler
      AddHandler nudLevelApproveDO.Click, AddressOf Me.ChangeProperty
      AddHandler nudLevelApproveDO.Validated, AddressOf Me.ChangeProperty
      AddHandler txtMaxAmtDO.TextChanged, AddressOf Me.TextHandler
      AddHandler txtMaxAmtDO.Validated, AddressOf Me.ChangeProperty

      AddHandler nudLevelApprovePA.TextChanged, AddressOf Me.TextHandler
      AddHandler nudLevelApprovePA.Click, AddressOf Me.ChangeProperty
      AddHandler nudLevelApprovePA.Validated, AddressOf Me.ChangeProperty
      AddHandler txtMaxAmtPA.TextChanged, AddressOf Me.TextHandler
      AddHandler txtMaxAmtPA.Validated, AddressOf Me.ChangeProperty

    End Sub
    Private nudLevelApprovePRChanged As Boolean = False
    Private nudLevelApproveWRChanged As Boolean = False
    Private nudLevelApprovePOChanged As Boolean = False
    Private nudLevelApproveSCChanged As Boolean = False
    Private nudLevelApproveDRChanged As Boolean = False
    Private nudLevelApproveDOChanged As Boolean = False
    Private nudLevelApprovePAChanged As Boolean = False
    Private txtMaxAmtPRChanged As Boolean = False
    Private txtMaxAmtWRChanged As Boolean = False
    Private txtMaxAmtPOChanged As Boolean = False
    Private txtMaxAmtSCChanged As Boolean = False
    Private txtMaxAmtDRChanged As Boolean = False
    Private txtMaxAmtDOChanged As Boolean = False
    Private txtMaxAmtPAChanged As Boolean = False
    Public Sub TextHandler(ByVal sender As Object, ByVal e As EventArgs)
      If Me.m_User Is Nothing Or Not m_isInitialized Then
        Return
      End If
      Select Case CType(sender, Control).Name.ToLower
        Case "nudlevelapprovepr"
          If nudLevelApprovePR.Text.Length = 0 Then
            nudLevelApprovePR.Text = 0
          End If
          If CDec(nudLevelApprovePR.Text) > Configuration.GetConfig("MaxLevelApprovePR") Then
            nudLevelApprovePR.Text = Configuration.GetConfig("MaxLevelApprovePR")
          End If
          nudLevelApprovePRChanged = True
        Case "nudlevelapprovewr"
          If nudLevelApproveWR.Text.Length = 0 Then
            nudLevelApproveWR.Text = 0
          End If
          If CDec(nudLevelApproveWR.Text) > Configuration.GetConfig("MaxLevelApproveWR") Then
            nudLevelApproveWR.Text = Configuration.GetConfig("MaxLevelApproveWR")
          End If
          nudLevelApproveWRChanged = True
        Case "nudlevelapprovepo"
          If nudLevelApprovePO.Text.Length = 0 Then
            nudLevelApprovePO.Text = 0
          End If
          If CDec(nudLevelApprovePO.Text) > Configuration.GetConfig("MaxLevelApprovePO") Then
            nudLevelApprovePO.Text = Configuration.GetConfig("MaxLevelApprovePO")
          End If
          nudLevelApprovePOChanged = True
        Case "nudlevelapprovesc"
          If nudLevelApproveSC.Text.Length = 0 Then
            nudLevelApproveSC.Text = 0
          End If
          If CDec(nudLevelApproveSC.Text) > Configuration.GetConfig("MaxLevelApproveSC") Then
            nudLevelApproveSC.Text = Configuration.GetConfig("MaxLevelApproveSC")
          End If
          nudLevelApproveSCChanged = True
        Case "nudlevelapprovedr"
          If nudLevelApproveDR.Text.Length = 0 Then
            nudLevelApproveDR.Text = 0
          End If
          If CDec(nudLevelApproveDR.Text) > Configuration.GetConfig("MaxLevelApprovedr") Then
            nudLevelApproveDR.Text = Configuration.GetConfig("MaxLevelApprovedr")
          End If
          nudLevelApproveDRChanged = True
        Case "nudlevelapprovedo"
          If nudLevelApproveDO.Text.Length = 0 Then
            nudLevelApproveDO.Text = 0
          End If
          If CDec(nudLevelApproveDO.Text) > Configuration.GetConfig("MaxLevelApproveDO") Then
            nudLevelApproveDO.Text = Configuration.GetConfig("MaxLevelApproveDO")
          End If
          nudLevelApproveDOChanged = True
        Case "nudlevelapprovepa"
          If nudLevelApprovePA.Text.Length = 0 Then
            nudLevelApprovePA.Text = 0
          End If
          If CDec(nudLevelApprovePA.Text) > Configuration.GetConfig("MaxLevelApprovePa") Then
            nudLevelApprovePA.Text = Configuration.GetConfig("MaxLevelApprovePa")
          End If
          nudLevelApprovePAChanged = True
        Case "txtmaxamtpr"
          txtMaxAmtPRChanged = True
        Case "txtmaxamtwr"
          txtMaxAmtWRChanged = True
        Case "txtmaxamtpo"
          txtMaxAmtPOChanged = True
        Case "txtmaxamtsc"
          txtMaxAmtSCChanged = True
        Case "txtmaxamtdr"
          txtMaxAmtDRChanged = True
        Case "txtmaxamtdo"
          txtMaxAmtDOChanged = True
        Case "txtmaxamtpa"
          txtMaxAmtPAChanged = True
      End Select
    End Sub
    Private isDirty As Boolean = False
    Public Sub ChangeProperty(ByVal sender As Object, ByVal e As EventArgs)
      If Me.m_User Is Nothing Or Not m_isInitialized Then
        Return
      End If
      Select Case CType(sender, Control).Name.ToLower
        Case "txtcode"
          Me.m_User.Code = txtCode.Text
          isDirty = True
        Case "txtname"
          Me.m_User.Name = txtName.Text
          isDirty = True
        Case "txtpassword"
          Me.m_User.Password = User.GeneratePassword(txtPassword.Text)
          isDirty = True


        Case "nudlevelapprovepr"
          If nudLevelApprovePRChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(7).Level = nudLevelApprovePR.Text
            isDirty = True
            nudLevelApprovePRChanged = False
          End If
        Case "nudlevelapprovewr"
          If nudLevelApproveWRChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(324).Level = nudLevelApproveWR.Text
            isDirty = True
            nudLevelApproveWRChanged = False
          End If
        Case "nudlevelapprovepo"
          If nudLevelApprovePOChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(6).Level = nudLevelApprovePO.Text
            isDirty = True
            nudLevelApprovePOChanged = False
          End If
        Case "nudlevelapprovesc"
          If nudLevelApproveSCChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(289).Level = nudLevelApproveSC.Text
            isDirty = True
            nudLevelApproveSCChanged = False
          End If
        Case "nudlevelapprovedr"
          If nudLevelApproveDRChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(291).Level = nudLevelApproveDR.Text
            isDirty = True
            nudLevelApproveDRChanged = False
          End If
        Case "nudlevelapprovedo"
          If nudLevelApproveDOChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(45).Level = nudLevelApproveDO.Text
            isDirty = True
            nudLevelApproveDOChanged = False
          End If
        Case "nudlevelapprovepa"
          If nudLevelApprovePAChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(292).Level = nudLevelApprovePA.Text
            isDirty = True
            nudLevelApprovePAChanged = False
          End If
        Case "txtmaxamtpr"
          If txtMaxAmtPRChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(7).MaxAmount = StringToDec(txtMaxAmtPR.Text)
            txtMaxAmtPR.Text = Configuration.FormatToString(Me.m_User.ApprovalDocLevelCollection.GetItem(7).MaxAmount, DigitConfig.Price)
            isDirty = True
            txtMaxAmtPRChanged = False
          End If
        Case "txtmaxamtwr"
          If txtMaxAmtWRChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(324).MaxAmount = StringToDec(txtMaxAmtWR.Text)
            txtMaxAmtWR.Text = Configuration.FormatToString(Me.m_User.ApprovalDocLevelCollection.GetItem(324).MaxAmount, DigitConfig.Price)
            isDirty = True
            txtMaxAmtWRChanged = False
          End If
        Case "txtmaxamtpo"
          If txtMaxAmtPOChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(6).MaxAmount = StringToDec(txtMaxAmtPO.Text)
            txtMaxAmtPO.Text = Configuration.FormatToString(Me.m_User.ApprovalDocLevelCollection.GetItem(6).MaxAmount, DigitConfig.Price)
            isDirty = True
            txtMaxAmtPOChanged = False
          End If
        Case "txtmaxamtsc"
          If txtMaxAmtSCChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(289).MaxAmount = StringToDec(txtMaxAmtSC.Text)
            txtMaxAmtSC.Text = Configuration.FormatToString(Me.m_User.ApprovalDocLevelCollection.GetItem(289).MaxAmount, DigitConfig.Price)
            isDirty = True
            txtMaxAmtSCChanged = False
          End If
        Case "txtmaxamtdr"
          If txtMaxAmtDRChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(291).MaxAmount = StringToDec(txtMaxAmtDR.Text)
            txtMaxAmtDR.Text = Configuration.FormatToString(Me.m_User.ApprovalDocLevelCollection.GetItem(291).MaxAmount, DigitConfig.Price)
            isDirty = True
            txtMaxAmtDRChanged = False
          End If
        Case "txtmaxamtdo"
          If txtMaxAmtDOChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(45).MaxAmount = StringToDec(txtMaxAmtDO.Text)
            txtMaxAmtDO.Text = Configuration.FormatToString(Me.m_User.ApprovalDocLevelCollection.GetItem(45).MaxAmount, DigitConfig.Price)
            isDirty = True
            txtMaxAmtDOChanged = False
          End If
        Case "txtmaxamtpa"
          If txtMaxAmtPAChanged Then
            Me.m_User.ApprovalDocLevelCollection.GetItem(292).MaxAmount = StringToDec(txtMaxAmtPA.Text)
            txtMaxAmtPA.Text = Configuration.FormatToString(Me.m_User.ApprovalDocLevelCollection.GetItem(292).MaxAmount, DigitConfig.Price)
            isDirty = True
            txtMaxAmtPAChanged = False
          End If
      End Select
      Me.WorkbenchWindow.ViewContent.IsDirty = Me.WorkbenchWindow.ViewContent.IsDirty OrElse isDirty
      isDirty = False
      CheckFormEnable()
    End Sub
    Private Function StringToDec(ByVal inVal As String) As Decimal
      Dim temp As Decimal
      inVal = inVal.Replace(",", "")
      Try
        temp = CDec(TextParser.Evaluate(inVal))
      Catch ex As Exception
        temp = 0
      End Try
      Return temp
    End Function
    ' �ʴ���Ң�����ŧ� control ������躹�����
    Public Overrides Sub UpdateEntityProperties()
      m_isInitialized = False
      ClearDetail()
      If m_User Is Nothing Then
        Return
      End If

      m_User.LoadImage()

      txtCode.Text = m_User.Code
      txtName.Text = m_User.Name

      If m_User.Originated Then
        txtPassword.Text = "**********"
        txtPassword.Enabled = False
      End If

      picImage.Image = m_User.Signature
      CheckLabelImgSize()

      Me.RefreshUserAccess(0)

      'ApprovalDoc
      If Not m_User.ApprovalDocLevelCollection Is Nothing Then
        Dim temp As ApprovalDocLevelCollection = m_User.ApprovalDocLevelCollection
        Me.nudLevelApprovePR.Text = temp.GetItem(7).Level
        Me.txtMaxAmtPR.Text = Configuration.FormatToString(temp.GetItem(7).MaxAmount, DigitConfig.Price)
        Me.nudLevelApproveWR.Text = temp.GetItem(324).Level
        Me.txtMaxAmtWR.Text = Configuration.FormatToString(temp.GetItem(324).MaxAmount, DigitConfig.Price)
        Me.nudLevelApprovePO.Text = temp.GetItem(6).Level
        Me.txtMaxAmtPO.Text = Configuration.FormatToString(temp.GetItem(6).MaxAmount, DigitConfig.Price)
        Me.nudLevelApproveSC.Text = temp.GetItem(289).Level
        Me.txtMaxAmtSC.Text = Configuration.FormatToString(temp.GetItem(289).MaxAmount, DigitConfig.Price)
        Me.nudLevelApproveDR.Text = temp.GetItem(291).Level
        Me.txtMaxAmtDR.Text = Configuration.FormatToString(temp.GetItem(291).MaxAmount, DigitConfig.Price)
        Me.nudLevelApproveDO.Text = temp.GetItem(45).Level
        Me.txtMaxAmtDO.Text = Configuration.FormatToString(temp.GetItem(45).MaxAmount, DigitConfig.Price)
        Me.nudLevelApprovePA.Text = temp.GetItem(292).Level
        Me.txtMaxAmtPA.Text = Configuration.FormatToString(temp.GetItem(292).MaxAmount, DigitConfig.Price)
      End If

      SetLabelText()
      'SetStatus()
      SetLabelText()
      CheckFormEnable()
      m_isInitialized = True
    End Sub

    Public Overrides Property Entity() As ISimpleEntity
      Get
        Return Me.m_User
      End Get
      Set(ByVal Value As ISimpleEntity)
        Me.m_User = Nothing
        Me.m_User = CType(Value, User)
        'Hack:
        Me.m_User.OnTabPageTextChanged(m_User, EventArgs.Empty)
        'Me.m_User.CostCenterUserAccessCollection = New CostCenterUserAccessCollection(Me.m_User)
        'Me.m_User.ApprovalDocLevelCollection = New ApprovalDocLevelCollection(Me.m_User)
        UpdateEntityProperties()
        DisableGigaSiteControl()
      End Set
    End Property

    Private Sub DisableGigaSiteControl()
      If Longkong.Pojjaman.BusinessLogic.Configuration.CheckGigaSiteRight Then
        Me.nudLevelApprovePA.Enabled = False
        Me.lblPA.Enabled = False
        Me.lblLevelPA.Enabled = False
        Me.lblMaxAmtPA.Enabled = False
        Me.lblBaht7.Enabled = False
        Me.txtMaxAmtPA.Enabled = False
        Me.nudLevelApproveDR.Enabled = False
        Me.lblDR.Enabled = False
        Me.lblLevelDR.Enabled = False
        Me.lblMaxAmtDR.Enabled = False
        Me.lblBaht6.Enabled = False
        Me.txtMaxAmtDR.Enabled = False
        Me.nudLevelApproveSC.Enabled = False
        Me.lblSC.Enabled = False
        Me.lblLevelSC.Enabled = False
        Me.lblMaxAmtSC.Enabled = False
        Me.lblBaht5.Enabled = False
        Me.txtMaxAmtSC.Enabled = False
        Me.txtMaxAmtWR.Enabled = False
        Me.nudLevelApproveWR.Enabled = False
        Me.lblWR.Enabled = False
        Me.lblLevelWR.Enabled = False
        Me.lblMaxAmtWR.Enabled = False
        Me.lblBaht4.Enabled = False
      End If
    End Sub
    Public Overrides Sub Initialize()

    End Sub
#End Region

#Region "CostCenterUserAccess"
    Public Shared Function GetSchemaTable() As TreeTable
      Dim myDatatable As New TreeTable("CostCenterUserAccess")
      myDatatable.Columns.Add(New DataColumn("ccua_linenumber", GetType(Integer)))
      myDatatable.Columns.Add(New DataColumn("Code", GetType(String)))
      myDatatable.Columns.Add(New DataColumn("Name", GetType(String)))
      Return myDatatable
    End Function

    Public Function CreateTableStyle() As DataGridTableStyle
      Dim dst As New DataGridTableStyle
      dst.MappingName = "CostCenterUserAccess"
      Dim myStringParserService As StringParserService = CType(ServiceManager.Services.GetService(GetType(StringParserService)), StringParserService)

      Dim csLineNumber As New TreeTextColumn
      csLineNumber.MappingName = "ccua_linenumber"
      csLineNumber.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.LineNumberHeaderText}")
      csLineNumber.NullText = ""
      csLineNumber.Width = 50
      csLineNumber.DataAlignment = HorizontalAlignment.Center
      csLineNumber.ReadOnly = True
      csLineNumber.TextBox.Name = "ccua_linenumber"

      Dim csCode As New TreeTextColumn
      csCode.MappingName = "Code"
      csCode.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.CodeHeaderText}")
      csCode.NullText = ""
      csCode.Width = 100
      csCode.ReadOnly = True
      csCode.TextBox.Name = "Code"

      Dim csName As New TreeTextColumn
      csName.MappingName = "Name"
      csName.HeaderText = myStringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.UserDetailView.NameHeaderText}")
      csName.NullText = ""
      csName.Width = 250
      csName.TextBox.Name = "Name"
      csName.ReadOnly = True

      dst.GridColumnStyles.Add(csLineNumber)
      dst.GridColumnStyles.Add(csCode)
      dst.GridColumnStyles.Add(csName)
      Return dst
    End Function

    Private Sub ibtnBlank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnBlank.Click
      Dim index As Integer = tgItem.CurrentRowIndex
      If index < 0 Then
        index = 0
      End If

      Dim filters() As Filter
      filters = New Filter() {New Filter("IDList", GetItemIDList())}

      Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
      myEntityPanelService.OpenListDialog(New CostCenter, AddressOf SetItems, filters)

      Me.RefreshUserAccess(index)
    End Sub
    Private Function GetItemIDList() As String
      Dim ret As String = ""
      For Each item As CostCenterUserAccess In Me.m_User.CostCenterUserAccessCollection
        ret &= item.CostCenterId.ToString & ","
      Next
      If ret.EndsWith(",") Then
        ret = ret.Substring(0, ret.Length - 1)
      End If
      Return ret
    End Function
    Private Sub RefreshUserAccess(ByVal index As Integer)
      If Not Me.m_User.CostCenterUserAccessCollection Is Nothing Then
        Me.m_User.CostCenterUserAccessCollection.PopulateCostCenter4User(m_treeManager.Treetable)
        Me.chkAllCC.Checked = Me.m_User.CostCenterUserAccessCollection.IsSuperMod
      End If
      tgItem.CurrentRowIndex = index
    End Sub
    Private Sub SetItems(ByVal items As BasketItemCollection)
      If tgItem.CurrentRowIndex = 0 Then
        'Hack
        tgItem.CurrentRowIndex = 1
      End If
      Dim index As Integer = tgItem.CurrentRowIndex
      For i As Integer = items.Count - 1 To 0 Step -1
        Dim item As BasketItem = CType(items(i), BasketItem)

        Dim doc As New CostCenterUserAccess
        Dim mycc As New CostCenter(item.Id)
        doc.CostCenterId = mycc.Id
        doc.CostCenterCode = mycc.Code
        doc.CostCenterName = mycc.Name
        doc.UserId = Me.m_User.Id
        doc.UserCode = Me.m_User.Code
        doc.UserName = Me.m_User.Name
        doc.AccessValue = 1

        'If Me.m_entity.CostCenterUserAccessCollection.Count > 0 Then
        '    Me.m_entity.CostCenterUserAccessCollection.Insert(index, doc)
        'Else
        Me.m_User.CostCenterUserAccessCollection.Add(doc)
        'End If
      Next
      Me.WorkbenchWindow.ViewContent.IsDirty = True
    End Sub
    Private Sub ibtnDelRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ibtnDelRow.Click
      If Me.m_User.CostCenterUserAccessCollection.Count <= 0 Then
        Return
      End If
      Dim index As Integer = Me.tgItem.CurrentRowIndex
      Me.m_User.CostCenterUserAccessCollection.Remove(index)
      Me.RefreshUserAccess(index)

      Me.WorkbenchWindow.ViewContent.IsDirty = True
    End Sub
    Private Sub chkAllCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllCC.Click
      If Not Me.m_User.CostCenterUserAccessCollection.IsSuperMod Then
        Me.m_User.CostCenterUserAccessCollection.IsSuperMod = True
      Else
        Me.m_User.CostCenterUserAccessCollection.IsSuperMod = False
      End If
      Me.WorkbenchWindow.ViewContent.IsDirty = True
    End Sub
#End Region

#Region "IHelperCapable"
    Public ReadOnly Property Helpers() As HelperCollection Implements IHelperCapable.Helpers
      Get

      End Get
    End Property

    Public Sub LoadHelpers() Implements IHelperCapable.LoadHelpers

    End Sub

    Public Sub UpdateValue(ByVal value As BusinessLogic.BusinessEntity) Implements IHelperCapable.UpdateValue

    End Sub

#End Region

#Region "IValidatable"
    Public ReadOnly Property FormValidator() As components.PJMTextboxValidator Implements IValidatable.FormValidator
      Get
        Return Me.Validator
      End Get
    End Property
#End Region

#Region "Event handlers"
    Private Sub btnResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetPassword.Click
      If Me.m_User Is Nothing OrElse Not Me.m_User.Originated Then
        Return
      End If
      Dim dlg As New Longkong.Pojjaman.Gui.Dialogs.ResetPasswordDialog(m_User)
      dlg.ShowDialog()
    End Sub
#End Region

#Region " Load Image "
    Private Sub btnLoadImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadImage.Click
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
        m_User.Signature = img
        'Hack
        Dim myContent As IViewContent = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ViewContent
        myContent.IsDirty = True
        CheckLabelImgSize()
      End If
    End Sub
#End Region

    Private Sub btnClearImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearImage.Click
      m_User.Signature = Nothing
      Me.picImage.Image = Nothing
      Dim myContent As IViewContent = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ViewContent
      myContent.IsDirty = True
      CheckLabelImgSize()
    End Sub
    Private Sub CheckLabelImgSize()
      Me.lblPicSize.Text = "160 X 88 pixel"
      If Me.m_User.Signature Is Nothing Then
        Me.lblPicSize.Visible = True
      Else
        Me.lblPicSize.Visible = False
      End If
    End Sub
  End Class

End Namespace
