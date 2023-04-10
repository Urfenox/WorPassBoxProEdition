<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerSync
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerSync))
        Me.gbServerInfo = New System.Windows.Forms.GroupBox()
        Me.btnCargar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.cbServer = New System.Windows.Forms.ComboBox()
        Me.lblSelectedServer = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbServerInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbServerInfo
        '
        Me.gbServerInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbServerInfo.Controls.Add(Me.btnCargar)
        Me.gbServerInfo.Controls.Add(Me.btnGuardar)
        Me.gbServerInfo.Controls.Add(Me.cbServer)
        Me.gbServerInfo.Controls.Add(Me.lblSelectedServer)
        Me.gbServerInfo.Location = New System.Drawing.Point(12, 93)
        Me.gbServerInfo.Name = "gbServerInfo"
        Me.gbServerInfo.Size = New System.Drawing.Size(360, 156)
        Me.gbServerInfo.TabIndex = 0
        Me.gbServerInfo.TabStop = False
        '
        'btnCargar
        '
        Me.btnCargar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCargar.Location = New System.Drawing.Point(112, 110)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.Size = New System.Drawing.Size(136, 28)
        Me.btnCargar.TabIndex = 2
        Me.btnCargar.Text = "Cargar"
        Me.btnCargar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnGuardar.Location = New System.Drawing.Point(112, 76)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(136, 28)
        Me.btnGuardar.TabIndex = 1
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'cbServer
        '
        Me.cbServer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbServer.FormattingEnabled = True
        Me.cbServer.Items.AddRange(New Object() {"WS2 Apps", "Custom"})
        Me.cbServer.Location = New System.Drawing.Point(112, 41)
        Me.cbServer.Name = "cbServer"
        Me.cbServer.Size = New System.Drawing.Size(136, 21)
        Me.cbServer.TabIndex = 1
        Me.cbServer.Text = "WSC Crizacio"
        '
        'lblSelectedServer
        '
        Me.lblSelectedServer.AutoSize = True
        Me.lblSelectedServer.Location = New System.Drawing.Point(124, 25)
        Me.lblSelectedServer.Name = "lblSelectedServer"
        Me.lblSelectedServer.Size = New System.Drawing.Size(112, 13)
        Me.lblSelectedServer.TabIndex = 0
        Me.lblSelectedServer.Text = "Servidor seleccionado"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(337, 31)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Sincronizacion de Cuentas"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(15, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(357, 50)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Server: N/A"
        '
        'ServerSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 261)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.gbServerInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ServerSync"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ServerSync"
        Me.gbServerInfo.ResumeLayout(False)
        Me.gbServerInfo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbServerInfo As GroupBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSelectedServer As Label
    Friend WithEvents cbServer As ComboBox
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnCargar As Button
    Friend WithEvents Label1 As Label
End Class
