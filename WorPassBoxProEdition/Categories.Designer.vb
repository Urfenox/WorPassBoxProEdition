<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Categories
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Categories))
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lstCategory = New System.Windows.Forms.ListBox()
        Me.btnSyncServer = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnOpen
        '
        Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpen.Location = New System.Drawing.Point(300, 12)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(84, 40)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Abrir"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNew.Location = New System.Drawing.Point(300, 58)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(84, 28)
        Me.btnNew.TabIndex = 2
        Me.btnNew.Text = "Nueva"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.Location = New System.Drawing.Point(300, 123)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(84, 23)
        Me.btnRemove.TabIndex = 3
        Me.btnRemove.Text = "Remover"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'lstCategory
        '
        Me.lstCategory.FormattingEnabled = True
        Me.lstCategory.Items.AddRange(New Object() {"Default"})
        Me.lstCategory.Location = New System.Drawing.Point(12, 12)
        Me.lstCategory.Name = "lstCategory"
        Me.lstCategory.Size = New System.Drawing.Size(282, 134)
        Me.lstCategory.TabIndex = 0
        '
        'btnSyncServer
        '
        Me.btnSyncServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSyncServer.Location = New System.Drawing.Point(300, 93)
        Me.btnSyncServer.Name = "btnSyncServer"
        Me.btnSyncServer.Size = New System.Drawing.Size(84, 22)
        Me.btnSyncServer.TabIndex = 4
        Me.btnSyncServer.Text = "Sync"
        Me.btnSyncServer.UseVisualStyleBackColor = True
        '
        'Categories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 158)
        Me.Controls.Add(Me.btnSyncServer)
        Me.Controls.Add(Me.lstCategory)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnOpen)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Categories"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Caterogias | PassBox Pro Edition"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOpen As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnRemove As Button
    Friend WithEvents lstCategory As ListBox
    Friend WithEvents btnSyncServer As Button
End Class
