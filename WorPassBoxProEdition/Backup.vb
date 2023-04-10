Public Class Backup

    Private Sub Backup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debugger.ApplyLang()
        ActionConfirm.Target(Debugger.Login_Password)
        If ActionConfirm.ShowDialog = DialogResult.OK Then

        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnCrearBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ActionConfirm.Target(Debugger.Login_Password)
        If ActionConfirm.ShowDialog = DialogResult.OK Then
            Try
                Try
                    Me.Hide()
                    'cerrar todas las categorias para crear la Backup
                    Debugger.SaveUserData()
                    Debugger.SaveAppData()
                    CryptoActions.OnClose()
                    Threading.Thread.Sleep(500)
                    My.Settings.Save()
                    My.Settings.Reload()
                    PassBox.UserClose = False
                    PassBox.Close()
                Catch ex As Exception
                    Console.WriteLine("[BackUp@Button1_Click:Preparing(Stage 1/2)]Error: " & ex.Message)
                End Try
                Try
                    CryptoActions.OnClose()
                    Threading.Thread.Sleep(150)
                    CryptoActions.UnLockDirectory()
                    Threading.Thread.Sleep(150)
                    My.Settings.Save()
                    My.Settings.Reload()
                Catch ex As Exception
                    Console.WriteLine("[BackUp@Button1_Click:Preparing(Stage 2/2)]Error: " & ex.Message)
                End Try
            Catch ex As Exception
                Console.WriteLine("[BackUp@Button1_Click:Preparing]Error: " & ex.Message)
            End Try
            Try
                My.Computer.FileSystem.CopyDirectory("C:\Users\" & System.Environment.UserName & "\PassBoxData", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\PassBoxAccounts")
                If Debugger.Espanglish = "ESP" Then
                    My.Computer.FileSystem.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\PassBoxAccounts\Codigo_de_Acceso.txt", "Este archivo contiene el codigo de acceso para PassBox Pro Edition. Version: " & My.Application.Info.Version.ToString &
                                                        vbCrLf & "Su codigo de acceso es: " & CryptoActions.Encriptar(Debugger.Login_AccountsCryptoKey & ">" & Debugger.Login_CryptoKey, CryptoActions.DefaultCryptoKey) &
                                                        vbCrLf & "Se le pediran cuando vayas a restaurar la copia de seguridad." &
                                                        vbCrLf & "No compartas tu codigo de acceso con nadie." &
                                                        vbCrLf & vbCrLf & "Recomendamos eliminar este fichero cuando la copia de seguridad ya este aplicada.", False)
                    MsgBox("Copia de Seguridad creada correctamente en su Escritorio", MsgBoxStyle.Information, "Worcome Security")
                ElseIf Debugger.Espanglish = "ENG" Then
                    My.Computer.FileSystem.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\PassBoxAccounts\Access_Code.txt", "This file contains the Access Code of PassBox Pro Edition. Version: " & My.Application.Info.Version.ToString &
                                                        vbCrLf & "Your Access Code is: " & CryptoActions.Encriptar(Debugger.Login_AccountsCryptoKey & ">" & Debugger.Login_CryptoKey, CryptoActions.DefaultCryptoKey) &
                                                        vbCrLf & "You will be asked when you are going to restore the backup." &
                                                        vbCrLf & "Do not share the access code with anyone." &
                                                        vbCrLf & vbCrLf & "We recommend deleting this file when the backup is already applied.", False)
                    MsgBox("Backup created successfuly on your Desktop", MsgBoxStyle.Information, "Worcome Security")
                End If
                Debugger.SaveAppData()
                Debugger.SaveUserData()
                End
            Catch ex As Exception
                Console.WriteLine("[BackUp@Button1_Click]Error: " & ex.Message)
            End Try
            Me.Close()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub pnlLeerBackup_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel1.DragDrop
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                If MessageBox.Show("Esto eliminara la base de datos del Programa para leer la copia de seguridad" & vbCrLf & "¿Deseas Continuar?" & vbCrLf & "This will remove the Program database to read the backup copy" & vbCrLf & "Do you want to continue?", "Worcome Security", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    ActionConfirm.Target(Debugger.Login_Password)
                    If ActionConfirm.ShowDialog = DialogResult.OK Then
                        Dim TextBoxVirtualACKey = InputBox("Ingresa tu Codigo de Acceso" & vbCrLf & "Enter your Access Code", "Worcome Security")
                        If TextBoxVirtualACKey = Nothing Then
                        Else
                            Dim NombreAleatorio As String = CryptoActions.Desencriptar(TextBoxVirtualACKey, CryptoActions.DefaultCryptoKey)
                            Dim Cadena As String() = NombreAleatorio.Split(">")
                            Dim Login_AccountsCryptoKey As String = Nothing
                            Dim Login_CryptoKey As String = Nothing
                            Login_AccountsCryptoKey = Cadena(0)
                            Login_CryptoKey = Cadena(1)
                            Debugger.Login_AccountsCryptoKey = Login_AccountsCryptoKey
                            Debugger.Login_CryptoKey = Login_CryptoKey
                            Debugger.SaveAppData()
                            Debugger.SaveUserData()
                            If My.Computer.FileSystem.DirectoryExists(PassBox.DIRCommons) Then
                                My.Computer.FileSystem.DeleteDirectory(PassBox.DIRCommons, FileIO.DeleteDirectoryOption.DeleteAllContents)
                            End If
                            If My.Computer.FileSystem.DirectoryExists(PassBox.DIRCommons) = False Then
                                My.Computer.FileSystem.CreateDirectory(PassBox.DIRCommons)
                            End If
                            If My.Computer.FileSystem.DirectoryExists(PassBox.ENCFiles) = False Then
                                My.Computer.FileSystem.CreateDirectory(PassBox.ENCFiles)
                            End If
                            If My.Computer.FileSystem.DirectoryExists(PassBox.DENFiles) = False Then
                                My.Computer.FileSystem.CreateDirectory(PassBox.DENFiles)
                            End If
                            Threading.Thread.Sleep(50)
                            Dim strRutaArchivos() As String
                            Dim i As Integer
                            strRutaArchivos = e.Data.GetData(DataFormats.FileDrop)
                            For i = 0 To strRutaArchivos.Length - 1
                                My.Computer.FileSystem.CopyDirectory(strRutaArchivos(i), PassBox.DIRCommons)
                            Next
                            Dim tempString As New TextBox
                            'tempString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(PassBox.DIRCommons & "\FileNames.WorCODE"), CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey))
                            'Dim StringPassedNames As String = Nothing
                            'For Each Item As String In tempString.Lines
                            '    StringPassedNames = StringPassedNames & Item & vbCrLf
                            'Next
                            'My.Computer.FileSystem.WriteAllText(PassBox.DIRCommons & "\FileNames.WorCODE", CryptoActions.Encriptar(StringPassedNames, CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)), False)
                            'Dim tmpString As New TextBox
                            'tmpString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(PassBox.DIRCommons & "\FileList.WorCODE"), CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey))
                            'Dim StringPassedListNames As String = Nothing
                            'For Each Item As String In tmpString.Lines
                            '    StringPassedListNames = StringPassedListNames & Item & vbCrLf
                            'Next
                            'My.Computer.FileSystem.WriteAllText(PassBox.DIRCommons & "\FileList.WorCODE", CryptoActions.Encriptar(StringPassedListNames, CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)), False)
                            If My.Computer.FileSystem.FileExists(PassBox.DIRCommons & "\Codigo_de_Acceso.txt") = True Then
                                My.Computer.FileSystem.DeleteFile(PassBox.DIRCommons & "\Codigo_de_Acceso.txt")
                            End If
                            If My.Computer.FileSystem.FileExists(PassBox.DIRCommons & "\Access_Code.txt") = True Then
                                My.Computer.FileSystem.DeleteFile(PassBox.DIRCommons & "\Access_Code.txt")
                            End If
                            If Debugger.Espanglish = "ESP" Then
                                MsgBox("Copia de Seguridad leida Correctamente" & vbCrLf & "Vuelva a iniciar la Aplicacion", MsgBoxStyle.Information, "Worcome Security")
                            ElseIf Debugger.Espanglish = "ENG" Then
                                MsgBox("Securely Read Backup" & vbCrLf & "Restart the Application", MsgBoxStyle.Information, "Worcome Security")
                                End If
                                Debugger.Close()
                            End If
                            Else
                        Me.Close()
                    End If
                Else
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("[BackUp@Panel1_DragDrop]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Panel1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
End Class