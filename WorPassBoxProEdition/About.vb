Public Class About

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Settings.OfflineMode = True Then
            CheckBox1.CheckState = CheckState.Checked
        Else
            CheckBox1.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        AppSupport.Show()
        AppSupport.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LangSelector.Show()
        LangSelector.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        AppUpdate.Show()
        AppUpdate.Focus()
    End Sub

    Sub NotSafeMode()
        Button1.Enabled = False
        Button2.Enabled = False
        LinkLabel1.Enabled = False
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        AppHelper.Show()
        AppHelper.Focus()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            My.Settings.OfflineMode = True
        ElseIf CheckBox1.CheckState = CheckState.Unchecked Then
            My.Settings.OfflineMode = False
        End If
        My.Settings.Save()
        My.Settings.Reload()
    End Sub
End Class