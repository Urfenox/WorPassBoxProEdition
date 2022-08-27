Public Class SignUp
    Public UserClose As Boolean = True

    Private Sub SignUp_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If UserClose = True Then
            Debugger.Close()
        End If
    End Sub

    Private Sub Register_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = Environment.UserName
    End Sub

    Private Sub RegistrarBUTTON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Registrar()
    End Sub

    Sub Registrar()
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Rellene con la información solicitada", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Fill in the requested information", MsgBoxStyle.Critical, "Worcome Security")
            End If
        Else
            UserClose = False
            Debugger.RegisterPassed()
        End If
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        If TextBox2.PasswordChar = "●" Then
            TextBox2.PasswordChar = ""
            If My.Settings.Espanglish = "ESP" Then
                Label6.Text = "Ocultar"
            ElseIf My.Settings.Espanglish = "ENG" Then
                Label6.Text = "Hide"
            End If
        Else
            TextBox2.PasswordChar = "●"
            If My.Settings.Espanglish = "ESP" Then
                Label6.Text = "Mostrar"
            ElseIf My.Settings.Espanglish = "ENG" Then
                Label6.Text = "Show"
            End If
        End If
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        About.NotSafeMode()
        About.Show()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            Registrar()
        End If
    End Sub
End Class