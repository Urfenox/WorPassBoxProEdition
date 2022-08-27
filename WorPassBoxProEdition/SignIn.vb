Public Class SignIn
    Public UserClose As Boolean = True
    Dim TresAgain As Integer 'Logica sera 2

    Private Sub SignIn_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If UserClose = True Then
            Debugger.Close()
        End If
    End Sub

    Private Sub SignIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub IniciarSesion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        IniciarSesion()
    End Sub

    Sub IniciarSesion()
        Try
            If TresAgain >= 3 Then
                If My.Settings.Espanglish = "ESP" Then
                    MsgBox("Máximo de intentos alcanzados", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf My.Settings.Espanglish = "ENG" Then
                    MsgBox("Maximum attempts reached", MsgBoxStyle.Critical, "Worcome Security")
                End If
                Debugger.Close()
            End If
            If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Then
                If My.Settings.Espanglish = "ESP" Then
                    MsgBox("Rellene con la información solicitada", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf My.Settings.Espanglish = "ENG" Then
                    MsgBox("Fill in the requested information", MsgBoxStyle.Critical, "Worcome Security")
                End If
                TresAgain = TresAgain + 1
            Else
                If TextBox1.Text = Debugger.Login_Username And TextBox2.Text = Debugger.Login_Password Then
                    UserClose = False
                    Debugger.LoginPassed()
                Else
                    TresAgain = TresAgain + 1
                    If My.Settings.Espanglish = "ESP" Then
                        MsgBox("Los datos no coinciden con el registro", MsgBoxStyle.Critical, "Worcome Security")
                    ElseIf My.Settings.Espanglish = "ENG" Then
                        MsgBox("The data does not match the record", MsgBoxStyle.Critical, "Worcome Security")
                    End If
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox1.Focus()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("[SignIn@IniciarSesion_Click]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        If TextBox2.PasswordChar = "●" Then
            TextBox2.PasswordChar = ""
            If My.Settings.Espanglish = "ESP" Then
                Label4.Text = "Ocultar"
            ElseIf My.Settings.Espanglish = "ENG" Then
                Label4.Text = "Hide"
            End If
        Else
            TextBox2.PasswordChar = "●"
            If My.Settings.Espanglish = "ESP" Then
                Label4.Text = "Mostrar"
            ElseIf My.Settings.Espanglish = "ENG" Then
                Label4.Text = "Show"
            End If
        End If
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            IniciarSesion()
        End If
    End Sub
End Class