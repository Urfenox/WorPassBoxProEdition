Imports System.IO

Public Class Config

    Private Sub Config_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActionConfirm.Target(Debugger.Login_Password)
        If ActionConfirm.ShowDialog = DialogResult.OK Then
            TextBox1.Text = Debugger.Login_Username
            TextBox2.Text = Debugger.Login_Email
            TextBox3.Text = Debugger.Login_Password
            If My.Settings.OfflineMode = False Then
                CheckBox1.CheckState = CheckState.Checked
            Else
                CheckBox1.CheckState = CheckState.Unchecked
            End If
            TabPage1.Visible = False
            TabPage1.Visible = False
            If My.Settings.OnlyMe = False Then
                DomainUpDown2.Text = "No"
            ElseIf My.Settings.OnlyMe = True Then
                DomainUpDown2.Text = "Si"
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub SaveChanges_Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ActionConfirm.Target(Debugger.Login_Password)
        If ActionConfirm.ShowDialog = DialogResult.OK Then
            If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Then
                MsgBox("Faltan Datos" & vbCrLf & "No se Aplicaran los Cambios", MsgBoxStyle.Information, "Worcome Secuity")
                Me.Close()
            Else
                Debugger.Login_Username = TextBox1.Text
                Debugger.Login_Email = TextBox2.Text
                Debugger.Login_Password = TextBox3.Text
                If CheckBox1.CheckState = CheckState.Checked Then
                    My.Settings.OfflineMode = True
                Else
                    If MsgBox("¿Want to run a AppService instance?", MsgBoxStyle.YesNo, "Worcome Security") = MsgBoxResult.Yes Then
                        Try
                            AppService.StartAppService(False, False, True, True, True) 'Offline, SecureMode, AppManager, SignAuthority (quitado), AppService
                        Catch
                        End Try
                    End If
                    My.Settings.OfflineMode = False
                End If
                If DomainUpDown2.Text = "No" Then
                    My.Settings.OnlyMe = False
                ElseIf DomainUpDown2.Text = "Si" Then
                    My.Settings.OnlyMe = True
                End If
                My.Settings.Save()
                My.Settings.Reload()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ActionConfirm.Target(Debugger.Login_Password)
        If ActionConfirm.ShowDialog = DialogResult.OK Then
            Dim RESULTADO As MsgBoxResult = MsgBox("¿Seguro que Quieres Resetear todo?" & vbCrLf & "TODO SERA ELIMINADO y Sera Restaurado a la Version Original", MsgBoxStyle.YesNo, "Worcome Security")
            If RESULTADO = MsgBoxResult.Yes Then
                If InputBox("Ingrese el Correo nuevamente para Confirmar" & vbCrLf & "Enter the Email again to Confirm", "Worcome Security") = Debugger.Login_Email Then
                    If InputBox("Ingrese su Nombre de Usuario para Continuar" & vbCrLf & "Enter the User Name again to Confirm", "Worcome Security") = Debugger.Login_Username Then
                        Try
                            If My.Computer.FileSystem.DirectoryExists(PassBox.DENFiles) = True Then
                                My.Computer.FileSystem.DeleteDirectory(PassBox.DENFiles, FileIO.DeleteDirectoryOption.DeleteAllContents)
                            End If
                            PassBox.CARGAR()
                        Catch ex As Exception
                            Console.WriteLine("[Config@Button2_Click:Reset]Error: " & ex.Message)
                        End Try
                        Debugger.FactoryReset()
                        If My.Settings.Espanglish = "ESP" Then
                            MsgBox("El Programa fue Restaurado a la Version de Fabrica", MsgBoxStyle.Critical, "Worcome Security")
                        ElseIf My.Settings.Espanglish = "ENG" Then
                            MsgBox("The Program was Restored to the Factory Version", MsgBoxStyle.Critical, "Worcome Security")
                        End If
                        Debugger.Close()
                    Else
                        If My.Settings.Espanglish = "ESP" Then
                            MsgBox("El UserName Ingresado no Coincide con el Nombre de Usuario Previamente Registrado" & vbCrLf & "PassBox se Cerrara", MsgBoxStyle.Critical, "Worcome Security")
                        ElseIf My.Settings.Espanglish = "ENG" Then
                            MsgBox("The entered UserName does not match the Previously Registered User Name" & vbCrLf & "PassBox will be closed", MsgBoxStyle.Critical, "Worcome Security")
                        End If
                        End
                    End If
                Else
                    If My.Settings.Espanglish = "ESP" Then
                        MsgBox("El Correo Ingresado no Coincide con el Correo Previamente Registrado" & "PassBox se Cerrara", MsgBoxStyle.Critical, "Worcome Security")
                    ElseIf My.Settings.Espanglish = "ENG" Then
                        MsgBox("Registered Mail Does Not Match Previously Registered Mail" & "PassBox Will Close", MsgBoxStyle.Critical, "Worcome Security")
                    End If
                    End
                End If
            Else
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        If Label9.Text = "Mostrar" Then
            TextBox4.Text = "DBUR " & Debugger.Login_CryptoKey & " / ACCT" & Debugger.Login_AccountsCryptoKey
            Label9.Text = "Ocultar"
        Else
            TextBox4.Text = Nothing
            Label9.Text = "Mostrar"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim TextBoxVirtualCryptoKey = InputBox("Ingrese una Llave Criptografica para la Base de Datos" & vbCrLf & "Enter a Cryptographic Key (Data Base)", "Worcome Security")
        Dim TextBoxVirtualAccountaCryptoKey = InputBox("Ingrese una Llave Criptografica para las Cuentas" & vbCrLf & "Enter a Cryptographic Key (Accounts)", "Worcome Security")
        If TextBoxVirtualCryptoKey = Nothing Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Rellene con la Informacion Solicitada", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Fill in the requested information", MsgBoxStyle.Critical, "Worcome Security")
            End If
            Exit Sub
        End If
        If TextBoxVirtualAccountaCryptoKey = Nothing Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Rellene con la Informacion Solicitada", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Fill in the requested information", MsgBoxStyle.Critical, "Worcome Security")
            End If
            Exit Sub
        End If
        Debugger.Login_CryptoKey = TextBoxVirtualCryptoKey
        Debugger.Login_AccountsCryptoKey = TextBoxVirtualAccountaCryptoKey
        Debugger.SaveAppData()
        Debugger.SaveUserData()
        Threading.Thread.Sleep(50)
        If My.Settings.Espanglish = "ESP" Then
            MsgBox("Llave Criptografica Agregada Correctamente!", MsgBoxStyle.Information, "Worcome Security")
        ElseIf My.Settings.Espanglish = "ENG" Then
            MsgBox("Cryptographic Key Added Correctly!", MsgBoxStyle.Information, "Worcome Security")
        End If
        PassBox.Close()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        CryptoActions.CreatePrivateKey()
        CryptoActions.CreateDataBasePrivateKey()
        Debugger.SaveAppData()
        Debugger.SaveUserData()
        Threading.Thread.Sleep(150)
        If My.Settings.Espanglish = "ESP" Then
            MsgBox("Llaves Criptograficas Creadas" & vbCrLf & "PassBox se Cerrara", MsgBoxStyle.Information, "Worcome Security")
        ElseIf My.Settings.Espanglish = "ENG" Then
            MsgBox("Cryptographic Keys Created" & vbCrLf & "PassBox Will Close", MsgBoxStyle.Information, "Worcome Security")
        End If
        PassBox.Close()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ActionConfirm.Target(Debugger.Login_Password)
        If ActionConfirm.ShowDialog = DialogResult.OK Then
            TextBox4.Text = "DBUR: " & Debugger.Login_CryptoKey & "/ ACCT: " & Debugger.Login_AccountsCryptoKey
        Else
            Me.Close()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Checked Then
            'modo oscuro
        Else

        End If
    End Sub

    Private Sub btnCreateKey_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If RadioButton1.Checked = True Then
            Dim WSHShell As Object = CreateObject("WScript.Shell")
            Dim Shortcut As Object = WSHShell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\[" & Environment.UserName & "_AccessKey]PassBoxPro.lnk")
            Shortcut.IconLocation = Application.ExecutablePath
            Shortcut.TargetPath = Application.ExecutablePath
            Shortcut.Arguments = CryptoActions.Encriptar("ENABLED/" & "Start/" & Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)
            Shortcut.Description = "Access key for " & My.Application.Info.ProductName
            Shortcut.WindowStyle = 1
            Shortcut.Save()
        ElseIf RadioButton2.Checked = True Then
            Dim WSHShell As Object = CreateObject("WScript.Shell")
            Dim Shortcut As Object = WSHShell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\[" & Environment.UserName & "_AccessKey]PassBoxPro.lnk")
            Shortcut.IconLocation = Application.ExecutablePath
            Shortcut.TargetPath = Application.ExecutablePath
            Shortcut.Arguments = "ENABLED/" & CryptoActions.Encriptar("Jump/" & Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)
            Shortcut.Description = "Access key for " & My.Application.Info.ProductName
            Shortcut.WindowStyle = 1
            Shortcut.Save()
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If MessageBox.Show("El modo portable creara una version separada del ejecutable instalado, la base de datos y las cuentas seran movidas de la carpeta por defecto (no se guardara ninguna copia)." &
                           vbCrLf & "Podra elegir donde se creara y moveran las cuentas." &
                           vbCrLf & "¿Desea continuar?", "Worcome Security", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            Dim FolderOpen As New FolderBrowserDialog With {
                .Description = "Seleccione en donde se creara la version portable",
                .RootFolder = Environment.SpecialFolder.Desktop,
                .ShowNewFolderButton = False
            }
            Dim Mode As String
            Dim Active As Boolean
            Dim Key As String
            Dim stub As String
            Const FS1 As String = "|WPSB|"
            Dim Temp As String = Application.StartupPath + "/STUB.exe"
            If FolderOpen.ShowDialog = DialogResult.OK Then
                Mode = "PRTBL"
                Active = True
                Key = CryptoActions.CreateRandomName(Debugger.Login_CryptoKey)
                Try
                    Dim bytesEXE As Byte() = System.IO.File.ReadAllBytes(Application.ExecutablePath)
                    File.WriteAllBytes(Temp, bytesEXE)
                    FileOpen(1, Temp, OpenMode.Binary, OpenAccess.Read, OpenShare.Default)
                    stub = Space(LOF(1))
                    FileGet(1, stub)
                    FileClose(1)
                    FileOpen(1, FolderOpen.SelectedPath & "\" & "Wor PassBox Pro" & ".exe", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.Default)
                    FilePut(1, stub & FS1 & Mode & FS1 & Active & FS1 & Key & FS1)
                    FileClose(1)
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class