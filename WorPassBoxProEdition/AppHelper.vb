Public Class AppHelper
    Dim DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\AppData\Local\Worcome_Studios\Commons\AppFiles"

    Private Sub AppHelper_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If AppService.OfflineApp = False Then
            If CSS1 = False Or CSS2 = False Or AMC = False Or AAP = False Then
                If AppService.IdiomaApp = "Spanish" Then
                    MsgBox("AppService no se ejecutó correctamente. Es posible que tenga problemas con este módulo.", MsgBoxStyle.Exclamation, "Worcome Security")
                ElseIf AppService.IdiomaApp = "English" Then
                    MsgBox("AppService did not run correctly. You may have problems with this module.", MsgBoxStyle.Exclamation, "Worcome Security")
                End If
            End If
        End If
        Start()
    End Sub

    Sub Start()
        Try
            If My.Computer.Network.IsAvailable Then
                WebBrowser1.Navigate(AppService.URL_AppHelper_Help & "/" & My.Application.Info.AssemblyName & ".html")
            Else
                MsgBox("Debe conectarse a internet", MsgBoxStyle.Information, "Worcome Security")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        If WebBrowser1.DocumentTitle = "404 Error | Worcome Studios" Then
            Label1.Text = "No se encontro el documento"
            MsgBox("El documento de ayuda todavia no existe dentro del Servidor." & vbCrLf & "Contacte con Soporte", MsgBoxStyle.Information, "Worcome Security")
            Me.Close()
        ElseIf WebBrowser1.DocumentTitle = "Esta página no se puede mostrar" Then
            Label1.Text = "Document not found"
            MsgBox("No hay conexion a internet", MsgBoxStyle.Critical, "Worcome Security")
            Me.Close()
        Else
            WebBrowser1.Visible = True
            Label1.Visible = False
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        WebBrowser1.Navigate(AppService.URL_AppHelper_Help & "/" & My.Application.Info.AssemblyName & ".html")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        WebBrowser1.Navigate(AppService.URL_AppHelper_About & "/About_" & My.Application.Info.AssemblyName & ".html")
    End Sub
End Class