Imports Longkong.Pojjaman.Services
Imports Longkong.Core.Services
Imports Longkong.Pojjaman.PanelDisplayBinding
Imports Longkong.Pojjaman.Gui
Imports Longkong.Pojjaman.Gui.Pads
Imports Longkong.Pojjaman.Gui.Components
Imports Longkong.Pojjaman.BusinessLogic
Imports Longkong.Pojjaman.DataAccessLayer
Namespace Longkong.Pojjaman.Gui.Panels
    Public Class ReportPanelView
        Inherits AbstractEntityPanelViewContent
        Implements ISimpleListPanel

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
        Friend WithEvents pnlFilter As System.Windows.Forms.Panel
        Friend WithEvents tgItem As Longkong.Pojjaman.Gui.Components.TreeGrid
        Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.pnlFilter = New System.Windows.Forms.Panel
            Me.tgItem = New Longkong.Pojjaman.Gui.Components.TreeGrid
            Me.Splitter1 = New System.Windows.Forms.Splitter
            CType(Me.tgItem, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'pnlFilter
            '
            Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFilter.Location = New System.Drawing.Point(0, 0)
            Me.pnlFilter.Name = "pnlFilter"
            Me.pnlFilter.Size = New System.Drawing.Size(768, 152)
            Me.pnlFilter.TabIndex = 0
            '
            'tgItem
            '
            Me.tgItem.AllowDrop = True
            Me.tgItem.AllowNew = False
            Me.tgItem.AllowSorting = False
            Me.tgItem.AutoColumnResize = False
            Me.tgItem.CaptionVisible = False
            Me.tgItem.Cellchanged = False
            Me.tgItem.ColorList.AddRange(New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(128, Byte), CType(255, Byte), CType(128, Byte)), System.Drawing.Color.FromArgb(CType(255, Byte), CType(224, Byte), CType(192, Byte)), System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte)), System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))})
            Me.tgItem.DataMember = ""
            Me.tgItem.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tgItem.HeaderBackColor = System.Drawing.Color.Khaki
            Me.tgItem.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
            Me.tgItem.HeaderForeColor = System.Drawing.SystemColors.ControlText
            Me.tgItem.Location = New System.Drawing.Point(0, 155)
            Me.tgItem.Name = "tgItem"
            Me.tgItem.Size = New System.Drawing.Size(768, 328)
            Me.tgItem.SortingArrowColor = System.Drawing.Color.Red
            Me.tgItem.TabIndex = 7
            Me.tgItem.TreeManager = Nothing
            '
            'Splitter1
            '
            Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Splitter1.Location = New System.Drawing.Point(0, 0)
            Me.Splitter1.Name = "Splitter1"
            Me.Splitter1.Size = New System.Drawing.Size(768, 3)
            Me.Splitter1.TabIndex = 1
            Me.Splitter1.TabStop = False
            '
            'ReportPanelView
            '
            Me.Controls.Add(Me.tgItem)
            Me.Controls.Add(Me.Splitter1)
            Me.Controls.Add(Me.pnlFilter)
            Me.Name = "ReportPanelView"
            Me.Size = New System.Drawing.Size(768, 483)
            CType(Me.tgItem, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "Members"
        Private m_filterSubPanel As IReportFilterSubPanel
        Private m_entity As Report

        Private m_treeManager As TreeManager
#End Region

#Region "Constructors"
        Public Sub New(ByVal entity As ISimpleEntity, ByVal handler As Object, ByVal basket As BasketDialog, ByVal filters As Filter(), ByVal entities As ArrayList)
            MyBase.New()

            InitializeComponent()
            m_entity = CType(entity, Report)
            Me.SetLabelText()
            Me.TitleName = Me.StringParserService.Parse(m_entity.ListPanelTitle)
            Me.PanelName = Me.Name

            Dim myEntityPanelService As IEntityPanelService = CType(ServiceManager.Services.GetService(GetType(IEntityPanelService)), IEntityPanelService)
            m_filterSubPanel = myEntityPanelService.GetReportFilterSubPanel(m_entity)

            Dim filterControl As UserControl = CType(Me.m_filterSubPanel, UserControl)
            Me.pnlFilter.Controls.Add(filterControl)
            Me.pnlFilter.Height = filterControl.Height
            AddHandler Me.m_filterSubPanel.SearchButton.Click, AddressOf btnSearch_Click

            Dim dt As TreeTable = m_entity.GetSimpleSchemaTable
            Dim dst As DataGridTableStyle = m_entity.CreateSimpleTableStyle
            m_treeManager = New TreeManager(dt, tgItem)
            m_treeManager.SetTableStyle(dst)
            m_treeManager.AllowSorting = False
            m_treeManager.AllowDelete = False
        End Sub
#End Region

#Region "Properties"

#End Region

#Region "Methods"
        Public Sub ChangeTitle(ByVal sender As Object, ByVal e As EventArgs) Implements ISimpleListPanel.ChangeTitle
            If Me.WorkbenchWindow.ActiveViewContent Is Me Then
                Me.TitleName = Me.StringParserService.Parse(m_entity.ListPanelTitle)
                Return
            End If
            'If Not m_selectedEntity Is Nothing Then
            '    Me.TitleName = m_selectedEntity.TabPageText
            'End If
        End Sub
        Public Sub OnEntityPropertyChanged(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent EntityPropertyChanged(sender, e)
        End Sub
#End Region

#Region "Event Handlers"
        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim filters As Filter() = Me.m_filterSubPanel.GetFilterArray
            Dim fixVals As DocPrintingItemCollection = Me.m_filterSubPanel.GetFixValueCollection
            m_entity.Filters = filters
            m_entity.FixValueCollection = fixVals
            m_entity.RefreshDataSet()
            m_entity.ListInGrid(m_treeManager)
        End Sub
        Private Sub RefreshEditableStatus()
            'WorkbenchSingleton.Workbench.RedrawAllComponents()
        End Sub
#End Region

#Region "ISimpleListPanel"
        Public Event EntitySelected(ByVal e As ISimpleEntity) Implements ISimpleListPanel.EntitySelected
        Public Event EntityPropertyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements ISimpleEntityPanel.EntityPropertyChanged

        Public Sub CheckFormEnable() Implements ISimpleEntityPanel.CheckFormEnable

        End Sub
        Public Sub ClearDetail() Implements ISimpleEntityPanel.ClearDetail

        End Sub

        Public Property Entity() As BusinessLogic.ISimpleEntity Implements ISimpleEntityPanel.Entity
            Get
                Return Me.m_entity
            End Get
            Set(ByVal Value As ISimpleEntity)
                Me.m_entity = CType(Value, Report)
            End Set
        End Property
        Public Sub Initialize() Implements ISimpleEntityPanel.Initialize

        End Sub
        Public Sub SetLabelText() Implements ISimpleEntityPanel.SetLabelText
            If Not m_entity Is Nothing Then
                Me.Text = Me.StringParserService.Parse(m_entity.ListPanelTitle)
            End If
        End Sub
        Public Sub UpdateEntityProperties() Implements ISimpleEntityPanel.UpdateEntityProperties

        End Sub
        Private Sub CloseHandler(ByVal sender As Object, ByVal e As EventArgs)
            'Dim dlg As Longkong.Pojjaman.Gui.Dialogs.PanelDialog = CType(sender, Longkong.Pojjaman.Gui.Dialogs.PanelDialog)
            'Dim row As DataRow = CType(dlg.Control, AssetSelectionView).SelectedRow
            'If dlg.DialogResult <> DialogResult.OK OrElse row Is Nothing Then
            '    Return
            'End If
            'm_selectedEntity = New Asset(row)
            'If Not m_selectedEntity Is Nothing Then
            '    AddHandler m_selectedEntity.TabPageTextChanged, AddressOf Me.ChangeTitle
            'End If
            'If TypeOf Me.WorkbenchWindow.ActiveViewContent Is ISecondaryViewContent Then
            '    For Each view As ISimpleEntityPanel In Me.WorkbenchWindow.SubViewContents
            '        view.Entity = m_selectedEntity
            '    Next
            'End If
            'If Not TypeOf Me.WorkbenchWindow.ActiveViewContent Is ISecondaryViewContent Then
            '    Me.WorkbenchWindow.SwitchView(1)
            'End If
        End Sub
        Public Sub AddNew() Implements ISimpleListPanel.AddNew

        End Sub
        Public Sub RefreshData(ByVal id As String) Implements ISimpleListPanel.RefreshData

        End Sub
        Public Property SelectedEntity() As BusinessLogic.ISimpleEntity Implements ISimpleListPanel.SelectedEntity
            Get
                Return Me.m_entity
            End Get
            Set(ByVal Value As BusinessLogic.ISimpleEntity)
                Me.m_entity = CType(Value, Report)
            End Set
        End Property
        Public ReadOnly Property Icon() As String Implements ISimplePanel.Icon
            Get
                Return Me.m_entity.ListPanelIcon
            End Get
        End Property
        Public Sub ShowInPad() Implements ISimplePanel.ShowInPad
            Return
        End Sub
        Public ReadOnly Property Title() As String Implements ISimplePanel.Title
            Get
                Return Me.m_entity.ListPanelTitle
            End Get
        End Property
#End Region

#Region "Overrides"
        Public Overrides ReadOnly Property TabPageText() As String
            Get
                Return Me.StringParserService.Parse("${res:Longkong.Pojjaman.Gui.Panels.ReportPanelView.TabPageText}")
            End Get
        End Property
#End Region

#Region "IClipboardHandler"
        Public Overrides ReadOnly Property EnablePaste() As Boolean
            Get
                If Not Me.m_filterSubPanel Is Nothing Then
                    Return Me.m_filterSubPanel.EnablePaste
                End If
            End Get
        End Property
        Public Overrides Sub Paste(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Me.m_filterSubPanel Is Nothing AndAlso Me.m_filterSubPanel.EnablePaste Then
                Me.m_filterSubPanel.Paste(sender, e)
            End If
        End Sub
#End Region

    End Class
End Namespace

