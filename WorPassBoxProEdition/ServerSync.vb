Imports System.IO.Compression
Public Class ServerSync
    Dim SyncServerDir As String = PassBox.DIRCommons & "\SyncServer"
    Dim DirToZip As String = SyncServerDir & "\Accounts"
    Dim TheZipFile As String = SyncServerDir & "\[" & Debugger.Login_Username & "]SyncAcct.zip"

    Private Sub ServerSync_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Debugger.ApplyLang()
        If My.Computer.FileSystem.FileExists(TheZipFile) Then
            My.Computer.FileSystem.DeleteFile(TheZipFile)
        End If
        Label1.Text = "Server: " & Debugger.SyncServer
    End Sub

    Sub PackerForUpload()
        Try
            'cifrar las cuentas para la subida (LIST¨*frmCategorias lo hace al cambiar y verificar que se sale de SyncServer)
            If My.Computer.FileSystem.FileExists(TheZipFile) Then
                My.Computer.FileSystem.DeleteFile(TheZipFile)
            End If
            ZipFile.CreateFromDirectory(DirToZip, TheZipFile)
            PostAccounts()
        Catch ex As Exception
        End Try
    End Sub

    Sub UnpackerForLoad()
        Try
            If My.Computer.FileSystem.DirectoryExists(DirToZip) = True Then
                My.Computer.FileSystem.DeleteDirectory(DirToZip, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            My.Computer.FileSystem.CreateDirectory(DirToZip)
            GetAccounts()
            ZipFile.ExtractToDirectory(TheZipFile, DirToZip)
            MsgBox("Cuentas cargadas", MsgBoxStyle.Information, "Worcome Security")
        Catch ex As Exception
        End Try
    End Sub

    Sub GetAccounts()
        Try
            'Download the .zip from the server with IDFTP
            My.Computer.Network.DownloadFile(Debugger.SyncServer & "/[" & Debugger.Login_Username & "]SyncAcct.zip", TheZipFile)
        Catch ex As Exception
        End Try
    End Sub

    Sub PostAccounts()
        Try
            'Upload the .zip from local to server with IDFTP
            My.Computer.Network.UploadFile(TheZipFile, Debugger.SyncServerPost)
            MsgBox("Cuentas guardadas", MsgBoxStyle.Information, "Worcome Security")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'debe estar la categoria cerrada para el sync??????. Creo que no, pues lo solucione, pero de ser necesario, seria mejor a nivel de codigo
        If Debugger.CurrentCategory = "SyncServer" Then
            MsgBox("No se puede subir con la categoria abierta", MsgBoxStyle.Critical, "Worcome Security")
        Else
            PackerForUpload()
        End If
    End Sub

    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        If Debugger.CurrentCategory = "SyncServer" Then
            MsgBox("No se puede cargar con la categoria abierta", MsgBoxStyle.Critical, "Worcome Security")
        Else
            UnpackerForLoad()
        End If
    End Sub

    Private Sub cbServer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbServer.SelectedIndexChanged
        Try
            Debugger.GetMyRegedit()
            Label1.Text = "Server: " & Debugger.SyncServer
            If cbServer.Text = "Custom" Then
                Dim SyncServer = InputBox("Ingrese la ruta en donde se encontrara en .ZIP" & vbCrLf & "Ej.: http://subdomain.domain.com/PassBoxPro/Accounts", "Worcome Security")
                Dim SyncServerPost = InputBox("Ingrese la ruta del PHP al que se le realizara la subida del .ZIP" & vbCrLf & "Ej.: http://subdomain.domain.com/PassBoxPro/postAccounts.php", "Worcome Security")
                If SyncServer = Nothing Then
                Else
                    If SyncServerPost = Nothing Then
                    Else
                        Debugger.SyncServer = SyncServer
                        Debugger.SyncServerPost = SyncServerPost
                        Debugger.SaveMyRegedit()
                        Label1.Text = "Server: " & Debugger.SyncServer
                        MsgBox("Ahora se utilizara su servidor para guardar/cargar las cuentas" & vbCrLf & "Puede volver al servidor por defecto seleccionandolo", MsgBoxStyle.Information, "Worcome Security")
                    End If
                End If
            Else
                Debugger.SyncServer = "https://crz-labs.crizacio.com/WSS/AppAccounts/Accounts"
                Debugger.SyncServerPost = "https://crz-labs.crizacio.com/WSS/AppAccounts/uploadAccount.php"
                Label1.Text = "Server: " & Debugger.SyncServer
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class