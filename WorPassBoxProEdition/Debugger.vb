Imports Microsoft.Win32
Public Class Debugger
    Dim AppFiles As String = "C:\Users\" & System.Environment.UserName & "\AppData\Local\Worcome_Studios\Commons\AppFiles"
    Dim DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\PassBoxData"
    Dim parametros As String
    Public Login_Username As String = Nothing
    Public Login_Password As String = Nothing
    Public Login_Email As String = Nothing
    Public Login_AccountsCryptoKey As String = Nothing
    Public Login_CryptoKey As String = Nothing
    Public IsRegistered As String = "False"
    Public CurrentCategory As String = "Default"
    Public Espanglish As String = "0"
    Public OfflineMode As Boolean = False

    Public SyncServer As String = "https://crz-labs.crizacio.com/WSS/AppAccounts/Accounts"
    Public SyncServerPost As String = "https://crz-labs.crizacio.com/WSS/AppAccounts/uploadAccount.php"

    Public SessionShield As String = DIRCommons & "\Session.ses"
    Public PB_ENCFiles As String = DIRCommons & "\EncryptedFiles"
    Public PB_DENFiles As String = DIRCommons & "\DecryptedFiles"
    Public PB_UserFiles As String = DIRCommons

    Private Sub Debugger_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            SaveUserData()
            SaveAppData()
            SaveRegedit()
            SaveMyRegedit()
            CryptoActions.OnClose()
            Threading.Thread.Sleep(500)
            My.Settings.Save()
            My.Settings.Reload()
            CryptoActions.LockDirectory()
            End
        Catch ex As Exception
            Console.WriteLine("[Debugger@Debugger_FormClosing]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Debugger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        parametros = Microsoft.VisualBasic.Command
        Me.Hide()
        'Try
        '    FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read)
        '    Dim stub As String = Space(LOF(1))
        '    FileGet(1, stub)
        '    FileClose(1)
        '    Dim Mode As String
        '    Dim Active As String
        '    Dim Key As Boolean
        '    Dim ArgsX() As String = Split(stub, "|WPSB|")
        '    Mode = ArgsX(1)
        '    Active = ArgsX(2)
        '    Key = Boolean.Parse(ArgsX(3))
        '    If Mode = "PRTBL" Then
        '        If Active = True Then
        '            If Key = Key Then
        '                Console.WriteLine("EL MODO '" & Mode & "' ESTA ACTIVO")
        '                'hacer las cosas necesarias para la compatibilidad
        '                '   la ruta de las cuentas siempre estara en la carpeta raiz de en donde se encuentra este ejecutable modificado portable
        '                '   Application.StartUpPatch

        '                '   primero hacer una copia de los archivos que se encuentran en las rutas
        '                '   AppFiles y DIRCommons (Debugger)
        '                '       Actualizar variables que usan DIRCommons o AppFiles
        '                '   DIRCommons (PassBox)
        '                '   asdasdasad

        '                '   Luego cambiar las rutas 'AppFiles' y 'DIRCommons'
        '                '   a una nueva ruta portable con Application.StartupPatch

        '                '   

        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        'End Try
        If My.Computer.FileSystem.FileExists(DIRUser_DBFile) = False Then
            IsRegistered = "False"
            Dim myCurrentLanguage As InputLanguage = InputLanguage.CurrentInputLanguage
            If myCurrentLanguage.Culture.EnglishName.Contains("Spanish") Then
                Espanglish = "ESP"
            ElseIf myCurrentLanguage.Culture.EnglishName.Contains("English") Then
                Espanglish = "ENG"
            Else
                Espanglish = "ENG"
            End If
        Else
            IsRegistered = "True"
        End If
        Try
            If My.Computer.FileSystem.FileExists(DIRCommons & "\CryptoKey.key") = True Then
                My.Computer.FileSystem.DeleteFile(DIRCommons & "\CryptoKey.key")
            End If
            If My.Computer.FileSystem.DirectoryExists(AppFiles) = False Then
                My.Computer.FileSystem.CreateDirectory(AppFiles)
            End If
            If My.Computer.FileSystem.DirectoryExists(DIRCommons) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            If My.Computer.FileSystem.DirectoryExists(DIRCommons & "\EncryptedFiles") = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons & "\EncryptedFiles")
            End If
            If My.Computer.FileSystem.DirectoryExists(DIRCommons & "\DecryptedFiles") = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons & "\DecryptedFiles")
            End If
            GetAppData()
            GetUserData()
            GetRegedit()
            GetMyRegedit()
            'ADVERTENCIA!: Si no el usuario deja de tener activada la opcion, de igual forma se podria acceder a  saltar el login debido
            '   a que el "enabled/disabled" no es almacenado en la DB.
            Try
                If parametros.Contains("ENABLED/") Then
                    Dim ClearedArgs As String = parametros.Replace("ENABLED/", Nothing)
                    Dim ArgsKeys As String() = CryptoActions.Desencriptar(ClearedArgs, Login_CryptoKey).Split("/")
                    'Access Key String ENABLED/JUMP/Login_AccountsCryptoKey
                    If ArgsKeys(0) = "Jump" Then
                        If ArgsKeys(1) = Login_AccountsCryptoKey Then
                            InicioComun(False)
                        Else
                            End
                        End If
                    ElseIf ArgsKeys(0) = "Start" Then
                        InicioComun()
                    Else
                        End
                    End If
                End If
            Catch ex As Exception
                Console.WriteLine("[CheckAccessKey]Error: " & ex.Message)
            End Try
            If parametros = Nothing Then
                LoadPastAppFile()
            ElseIf parametros = "/FactoryReset" Then
                FactoryReset()
            End If
        Catch ex As Exception
            Console.WriteLine("[Debugger@Debugger_Load]Error: " & ex.Message)
        End Try
    End Sub

#Region "Base de Datos"
    Dim DIRUser_DBFile As String = DIRCommons & "\DB_user.dat"
    Dim DIRApp_DBFile As String = DIRCommons & "\DB_app.dat"
    'no esta guardando la DB del programa DBAPP (NI IDEA QUE ES ESTO XDN'T)

    'Podriamos agregar un sistema IniValue retrocompatible con versiones anteriores dejando el formato antiguo junto con el nuevo (ini)
    '   esto crearia tener que actualizar los dos formatos del mismo archivo al mismo tiempo
    Sub GetUserData()
        Try
            Dim tempString As New TextBox
            tempString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(DIRUser_DBFile), Login_CryptoKey)
            Dim Lineas = tempString.Lines
            Login_Username = Lineas(1).Split(">"c)(1).Trim()
            Login_Password = Lineas(2).Split(">"c)(1).Trim()
            Login_Email = Lineas(3).Split(">"c)(1).Trim()
            Login_AccountsCryptoKey = Lineas(4).Split(">"c)(1).Trim()
        Catch ex As Exception
            Console.WriteLine("[Debugger@GetUserData]Error: " & ex.Message)
        End Try
    End Sub
    Sub GetAppData()
        Try
            Dim tempString As New TextBox
            tempString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(DIRApp_DBFile), CryptoActions.DefaultCryptoKey)
            Dim Lineas = tempString.Lines
            IsRegistered = Lineas(1).Split(">"c)(1).Trim()
            Login_CryptoKey = Lineas(2).Split(">"c)(1).Trim()
        Catch ex As Exception
            Console.WriteLine("[Debugger@GetAppData]Error: " & ex.Message)
        End Try
    End Sub
    Sub SaveUserData()
        CryptoActions.UnLockDirectory()
        If My.Computer.FileSystem.FileExists(DIRUser_DBFile) = True Then
            My.Computer.FileSystem.DeleteFile(DIRUser_DBFile)
        End If
        Try
            Dim tempString As New TextBox
            tempString.Text = "#Wor PassBox Pro User Data Base" &
                vbCrLf & "Username>" & Login_Username &
                vbCrLf & "Password>" & Login_Password &
                vbCrLf & "Email>" & Login_Email &
                vbCrLf & "CryptoKey>" & Login_AccountsCryptoKey
            My.Computer.FileSystem.WriteAllText(DIRUser_DBFile, CryptoActions.Encriptar(tempString.Text, Login_CryptoKey), False)
            GetUserData()
        Catch ex As Exception
            Console.WriteLine("[Debugger@SaveUserData]Error: " & ex.Message)
        End Try
    End Sub
    Sub SaveAppData()
        CryptoActions.UnLockDirectory()
        If My.Computer.FileSystem.FileExists(DIRApp_DBFile) = True Then
            My.Computer.FileSystem.DeleteFile(DIRApp_DBFile)
        End If
        Try
            Dim tempString As New TextBox
            tempString.Text = "#Wor PassBox Pro App Data Base" &
                vbCrLf & "IsRegistered>" & IsRegistered &
                vbCrLf & "CryptoKey>" & Login_CryptoKey
            My.Computer.FileSystem.WriteAllText(DIRApp_DBFile, CryptoActions.Encriptar(tempString.Text, CryptoActions.DefaultCryptoKey), False)
            GetAppData()
        Catch ex As Exception
            Console.WriteLine("[Debugger@SaveAppData]Error: " & ex.Message)
        End Try
    End Sub

    Dim myRegeditKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName & "\\SyncServer", True)
    Sub SaveMyRegedit()
        If myRegeditKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName & "\\SyncServer")
            myRegeditKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName & "\\SyncServer", True)
        End If
        Try
            myRegeditKey.SetValue("SyncServer", CryptoActions.Encriptar(SyncServer, Login_CryptoKey), RegistryValueKind.String)
            myRegeditKey.SetValue("SyncServerPost", CryptoActions.Encriptar(SyncServerPost, Login_CryptoKey), RegistryValueKind.String)
            GetMyRegedit()
        Catch
        End Try
    End Sub
    Sub GetMyRegedit()
        Try
            SyncServer = CryptoActions.Desencriptar(myRegeditKey.GetValue("SyncServer"), Login_CryptoKey)
            SyncServerPost = CryptoActions.Desencriptar(myRegeditKey.GetValue("SyncServerPost"), Login_CryptoKey)
        Catch
        End Try
    End Sub

    Dim RegeditKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName, True)
    Sub SaveRegedit()
        If RegeditKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName)
            RegeditKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName, True)
        End If
        Try
            RegeditKey.SetValue("LANG", Espanglish, RegistryValueKind.String)
            RegeditKey.SetValue("OfflineMode", OfflineMode, RegistryValueKind.String)
            GetRegedit()
        Catch
        End Try
    End Sub
    Sub GetRegedit()
        Try
            Espanglish = RegeditKey.GetValue("LANG")
            OfflineMode = Boolean.Parse(RegeditKey.GetValue("OfflineMode"))
        Catch
        End Try
    End Sub
#End Region

#Region "PastAppFile"
    Dim SecureWord As String
    Dim SecureVersion As String
    Dim SecureBoolean As Boolean = My.Settings.OnlyMe
    Sub LoadPastAppFile()
        InicioComun()
        'If My.Settings.OnlyMe = True Then
        '    If My.Computer.FileSystem.FileExists(AppFiles & "\" & My.Application.Info.AssemblyName & "PastAppFile_OnlyMe.WorCODE") = False Then
        '        My.Computer.FileSystem.WriteAllText(AppFiles & "\" & My.Application.Info.AssemblyName & "PastAppFile_OnlyMe.WorCODE", "#Read modules and compares with the Original/Changed" & vbCrLf & "OnlyMe>" & SecureBoolean & vbCrLf & "Version>" & My.Application.Info.Version.ToString, False)
        '        ReadPastAppFile()
        '    ElseIf My.Computer.FileSystem.FileExists(AppFiles & "\" & My.Application.Info.AssemblyName & "PastAppFile_OnlyMe.WorCODE") = True Then
        '        ReadPastAppFile()
        '    End If
        'ElseIf My.Settings.OnlyMe = False Then
        '    If My.Computer.FileSystem.FileExists(AppFiles & "\" & My.Application.Info.AssemblyName & "PastAppFile_OnlyMe.WorCODE") = True Then
        '        My.Computer.FileSystem.DeleteFile(AppFiles & "\" & My.Application.Info.AssemblyName & "PastAppFile_OnlyMe.WorCODE")
        '        InicioComun()
        '    End If
        'End If
    End Sub

    Sub ReadPastAppFile()
        Dim Lines = System.IO.File.ReadAllLines(AppFiles & "\" & My.Application.Info.AssemblyName & "PastAppFile_OnlyMe.WorCODE")
        SecureWord = Lines(1).Split(">"c)(1).Trim()
        SecureVersion = Lines(2).Split(">"c)(1).Trim()
        If SecureWord = "True" Then
            If SecureVersion = My.Application.Info.Version.ToString = True Then
                'Seguir normalmente
                InicioComun()
            ElseIf SecureVersion = My.Application.Info.Version.ToString = False Then
                'No seguir
                MsgBox("La Aplicacion no se Ejecutara" & vbCrLf & "La Aplicacion original tiene 'OnlyMe' Activado", MsgBoxStyle.Information, "Worcome Security")
                End
            End If
        ElseIf SecureWord = "False" Then
            InicioComun()
        End If
    End Sub
#End Region

    Sub InicioComun(Optional ByVal common As Boolean = True)
        Try
            If OfflineMode = False Then
                AppService.StartAppService(False, False, True, True, True)
            End If
            Threading.Thread.Sleep(150)
        Catch ex As Exception
            MsgBox("ERROR CRITICO CON 'AppService'", MsgBoxStyle.Critical, "Worcome Security")
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists(DIRCommons) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            If Login_CryptoKey = Nothing Then
                CryptoActions.CreatePrivateKey()
            End If
            If Login_AccountsCryptoKey = Nothing Then
                CryptoActions.CreateDataBasePrivateKey()
            End If
            If Espanglish = "0" Then
                LangSelector.ShowDialog()
            End If
            My.Settings.Save()
            My.Settings.Reload()
            ApplyLang()
            If common = True Then
                If IsRegistered = "False" Then
                    SignUp.Show()
                Else
                    SignIn.Show()
                End If
            Else
                LoginPassed()
            End If
        Catch ex As Exception
            Console.WriteLine("[Debugger@InicioComun]Error: " & ex.Message)
        End Try
    End Sub
    Sub ApplyLang()
        If Espanglish = "ESP" Then
            Idioma.Español.LANG_Español()
        ElseIf Espanglish = "ENG" Then
            Idioma.Ingles.LANG_English()
        Else
            LangSelector.ShowDialog()
        End If
    End Sub
    Sub LoginPassed()
        SignIn.Close()
        SignUp.Close()
        PassBox.Show()
        SaveAppData()
        SaveUserData()
        Try
            If My.Computer.FileSystem.FileExists(SessionShield) = True Then
                MsgBox("La ultima sesion de PassBox Pro no termino correctamente", MsgBoxStyle.Exclamation, "Worcome Security")
                'recuperar
                '   cifrar y todo lo que se hace al cerrar
                My.Computer.FileSystem.DeleteFile(SessionShield)
            End If
            My.Computer.FileSystem.WriteAllText(SessionShield, Nothing, False)
        Catch ex As Exception
        End Try
    End Sub
    Sub RegisterPassed()
        IsRegistered = "True"
        Login_Username = SignUp.TextBox1.Text
        Login_Password = SignUp.TextBox2.Text
        Login_Email = SignUp.TextBox3.Text
        My.Settings.Save()
        My.Settings.Reload()
        SignUp.Close()
        SignIn.Show()
        SaveAppData()
        SaveUserData()
    End Sub

    Sub LoadCategory()
        If My.Computer.FileSystem.FileExists(SessionShield) = True Then
            MsgBox("La ultima sesion de PassBox Pro en esta categoria no termino correctamente", MsgBoxStyle.Exclamation, "Worcome Security")
            'recuperar
            '   cifrar y todo lo que se hace al cerrar
            My.Computer.FileSystem.DeleteFile(SessionShield)
        End If
        My.Computer.FileSystem.WriteAllText(SessionShield, Nothing, False)
        SaveUserData()
        SaveAppData()
        ''no necesario porque la categoria debe estar cerrada para poder subirla
        'If CurrentCategory = "SyncServer" Then
        'Else
        '    CryptoActions.OnClose()
        'End If
        CryptoActions.OnClose()
        Threading.Thread.Sleep(500)
        My.Settings.Save()
        My.Settings.Reload()
        PassBox.Close()
        Categories.Close()
        PassBox.Show()
        PassBox.Focus()
    End Sub
    Sub Restart()
        SignIn.Close()
        SignUp.Close()
        InicioComun()
    End Sub
    Sub FactoryReset()
        Try
            If MessageBox.Show("¿Desea hacer un reset?", "Worcome Security", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                If InputBox("Escribe la Contraseña Previamente Registrada para Proceder con el 'FactoryReset'" & "Write the Previously Registered Password to Proceed with the 'FactoryReset'", "Worcome Security") = Login_Password Then
                    If InputBox("Escribe el Correo Previamente Registrado para Proceder con el 'FactoryReset'" & vbCrLf & "Write the Previously Registered Mail to Proceed with the 'FactoryReset", "Worcome Security") = Login_Email Then
                        IsRegistered = "False"
                        Login_Username = Nothing
                        Login_Password = Nothing
                        Login_Email = Nothing
                        Login_AccountsCryptoKey = Nothing
                        Login_CryptoKey = Nothing
                        Espanglish = "0"
                        SaveAppData()
                        SaveUserData()
                        CryptoActions.UnLockDirectory()
                        Threading.Thread.Sleep(150)
                        If My.Computer.FileSystem.DirectoryExists(DIRCommons) = True Then
                            My.Computer.FileSystem.DeleteDirectory(DIRCommons, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        End If
                        My.Settings.Save()
                        My.Settings.Reload()
                        Try
                            Registry.CurrentUser.DeleteSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName)
                        Catch ex As Exception
                        End Try
                        MsgBox("Reset completado", MsgBoxStyle.Information, "Worcome Security")
                        End
                    Else
                        End
                    End If
                Else
                    End
                End If
            Else
                End
            End If
        Catch ex As Exception
            Console.WriteLine("[Debugger@FactoryReset]Error: " & ex.Message)
        End Try
    End Sub
End Class