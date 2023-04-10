Public Class LangSelector
    Dim btnClicked As Boolean = False

    Private Sub LangSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.SelectedItem = "Idioma / Language" Or ComboBox1.Text = "Idioma / Language" Then
            MsgBox("Elige un Idioma" & vbCrLf & "Select a Language", MsgBoxStyle.Critical, "Worcome Security")
        Else
            If ComboBox1.SelectedItem = "Español (España)" Then
                Idioma.Español.LANG_Español()
                Debugger.Espanglish = "ESP"
            ElseIf ComboBox1.SelectedItem = "English (Unites States)" Then
                Idioma.Ingles.LANG_English()
                Debugger.Espanglish = "ENG"
            End If
            Debugger.SaveRegedit()
            btnClicked = True
            Me.Close()
        End If
    End Sub

    Private Sub LangSelector_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If btnClicked = False Then
            Idioma.Ingles.LANG_English()
            Debugger.Espanglish = "ENG"
            Debugger.SaveRegedit()
        End If
    End Sub
End Class