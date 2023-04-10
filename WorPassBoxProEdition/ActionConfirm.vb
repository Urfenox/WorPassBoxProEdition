Public Class ActionConfirm
    Dim Data As String

    Private Sub ActionConfirm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Debugger.ApplyLang()
        TextBox1.Focus()
    End Sub

    Sub Target(ByVal Dato As String)
        Try
            Data = Dato
            If Dato = Debugger.Login_Password Then
                If Debugger.Espanglish = "ESP" Then
                    Label3.Text = "Ingrese su Contraseña"
                ElseIf Debugger.Espanglish = "ENG" Then
                    Label3.Text = "Enter your Password"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles Button1.Click
        VerifyData()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            VerifyData()
        End If
    End Sub

    Sub VerifyData()
        If TextBox1.Text = Nothing Then
            If Debugger.Espanglish = "ESP" Then
                MsgBox("Rellene con la información solicitada", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf Debugger.Espanglish = "ENG" Then
                MsgBox("Fill in the requested information", MsgBoxStyle.Critical, "Worcome Security")
            End If
            TextBox1.Focus()
        Else
            If TextBox1.Text = Data Then
                TextBox1.Clear()
                If Debugger.Espanglish = "ESP" Then
                    Label3.Text = "Ingrese la información"
                ElseIf Debugger.Espanglish = "ENG" Then
                    Label3.Text = "Enter the information"
                End If
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                TextBox1.Clear()
                If Debugger.Espanglish = "ESP" Then
                    MsgBox("La información ingresada no coincide con el registro", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf Debugger.Espanglish = "ENG" Then
                    MsgBox("The information entered does not match the record", MsgBoxStyle.Critical, "Worcome Security")
                End If
            End If
        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        If TextBox1.PasswordChar = "●" Then
            TextBox1.PasswordChar = Nothing
        Else
            TextBox1.PasswordChar = "●"
        End If
    End Sub

    Private Sub ActionConfirm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub
End Class