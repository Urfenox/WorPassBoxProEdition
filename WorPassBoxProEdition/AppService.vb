Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Module AppService
    ReadOnly DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\AppData\Local\Worcome_Studios\Commons"
    ReadOnly DIRAppManager As String = DIRCommons & "\AppManager"
    ReadOnly DIRKeyManager As String = DIRAppManager & "\KeyManager"
    ReadOnly DirAppPatch As String = Application.ExecutablePath
    Public ReadOnly ServerSwitchURLs = {"http://worcomecorporations.000webhostapp.com/Source/WSS/WSS_ServerSwitch.ini",
                    "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/WSS_ServerSwitch.ini",
                    "http://worcomestudios.comule.com/Recursos/InfoData/WSS_ServerSwitch.ini",
                    "https://www.dropbox.com/s/9ktiht78g0n9v0y/WSS_ServerSwitch.ini?dl=1",
                    "https://docs.google.com/uc?export=download&id=1ztBhx9ROXfHTBknpAyJk49S3P0EeypOA"}
    'sera que por cada arranque elige un enlace de la lista (+1)?

    Public ReadOnly ServerSwitchFileURL As String = ServerSwitchURLs(0)

    Public AssemblyNameThis As String = My.Application.Info.AssemblyName
    Public AssemblyVersionThis As String = My.Application.Info.Version.ToString
    Public AssemblyProductNameThis As String = My.Application.Info.ProductName

    Public OfflineApp As Boolean
    Public SecureMode As Boolean
    Dim AppManager As Boolean
    Dim SignAutority As Boolean
    Dim AppServiceStatus As Boolean

#Region "AppService Vars"
    'AppService
    Public Assembly_Status As String
    Public Assembly_Name As String
    Public Assembly_Version As String 'Si la version del servidor coincide con '*.*.*.*' entonces no se hara verificacion de versiones
    Public Runtime_URL As String
    Public Runtime_MSG As String
    Public Runtime_ArgumentLine As String
    Public Runtime_Command As String
    Public Updates_Critical As String
    Public Updates_CriticalMessage As String
    Public Updates_RAW_Download As String
    Public Updates_Download As String
    Public Installer_Status As String
    Public Installer_DownloadFrom As String
#End Region

    Public IdiomaApp As String = "ENG"

    Public CSS1 As Boolean = False
    Public CSS2 As Boolean = False
    Public AMC As Boolean = False
    Public AAP As Boolean = False

#Region "ServerSwitch Vars"
    'ServerSwitch
    Public UsingServer As String = "WS1"
    Dim ServerStatus As String = Nothing
    Dim ServerMSG As String = Nothing
    Dim ServerURL As String = Nothing
    Dim ServerArgumentLine As String = Nothing
    Dim ServerCommand As String = Nothing
    Dim URLs_Update As String = Nothing
    Public CurrentServerURL As String = "http://worcomestudios.comule.com"
    Public URL_KeyAccessToken As String = "http://worcomestudios.comule.com/Recursos/InfoData/KeyAccessToken.WorCODE"
    Public URL_AppService As String = "http://worcomestudios.comule.com/Recursos/InfoData/WorAppServices"
    Public URL_AppUpdate As String = "http://worcomestudios.comule.com/Recursos/InfoData/Updates"
    Public URL_AppUpdate_WhatsNew As String = "http://worcomestudios.comule.com/Recursos/InfoData/WhatsNew"
    Public URL_AppHelper_Help As String = "http://worcomestudios.comule.com/Recursos/AppHelper"
    Public URL_AppHelper_About As String = "http://worcomestudios.comule.com/Recursos/AppHelper/AboutApps"
    Public URL_Support_Post As String = "http://worcomestudios.comule.com/Recursos/WorCommunity/soporte.php"
    Public URL_Telemetry_Post As String = "http://worcomestudios.comule.com/Recursos/InfoData/TelemetryPost.php"
    Public URL_Download_Updater As String = "http://worcomestudios.comule.com/Recursos/InfoData/Updates/Updater.zip"

#End Region

    Dim WithEvents DownloaderArrayServerSwitch As New Net.WebClient
    Dim WithEvents DownloaderArrayAppService As New Net.WebClient
    Dim DownloadURIServerSwitch As Uri
    Dim DownloadURIAppService As Uri

    Sub StartAppService(ByVal OffLineApp_SAS As Boolean, ByVal SecureModeSAS As Boolean, ByVal AppManager_SAS As Boolean, ByVal SignAutority_SAS As Boolean, ByVal AppServiceStatus_SAS As Boolean, Optional ByVal AssemblyName As String = Nothing, Optional ByVal AssemblyVersion As String = Nothing)
        Dim myCurrentLanguage As InputLanguage = InputLanguage.CurrentInputLanguage
        If myCurrentLanguage.Culture.EnglishName.Contains("Spanish") Then
            IdiomaApp = "ESP"
        ElseIf myCurrentLanguage.Culture.EnglishName.Contains("English") Then
            IdiomaApp = "ENG"
        Else
            IdiomaApp = "ENG"
        End If
        If AssemblyName IsNot Nothing And AssemblyVersion IsNot Nothing Then
            AssemblyNameThis = AssemblyName
            AssemblyVersionThis = AssemblyVersion
            AssemblyProductNameThis = AssemblyNameThis.Replace("Wor", Nothing)
        End If
        'If MsgBox("¿Desea ejecutar una instancia de AppService?", MsgBoxStyle.YesNo, "Worcome Security") = MsgBoxResult.Yes Then
        '    Try
        '        AppService.StartAppService(False, False, True, True, True) 'Offline, SecureMode, AppManager, SignAuthority (quitado), AppService
        '    Catch
        '    End Try
        'End If
        Console.WriteLine("[StartAppService]Iniciado en: " & vbCrLf & "   Offline Mode: " & OffLineApp_SAS & vbCrLf & "   Secure Mode: " & SecureModeSAS & vbCrLf & "   AppManager: " & AppManager_SAS & vbCrLf & "   SignAutority: " & SignAutority_SAS & vbCrLf & "   AppService: " & AppServiceStatus_SAS)
        OfflineApp = OffLineApp_SAS
        SecureMode = SecureModeSAS
        AppManager = AppManager_SAS
        SignAutority = SignAutority_SAS
        AppServiceStatus = AppServiceStatus_SAS
        If SecureMode = True Then
            If My.Computer.Network.IsAvailable = False Then
                MsgBox("Esta aplicación necesita acceso a internet para continuar", MsgBoxStyle.Critical, "Worcome Security")
                End 'END_PROGRAM
            End If
        End If
        Try
            If My.Computer.FileSystem.DirectoryExists(DIRCommons) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            If My.Computer.FileSystem.DirectoryExists(DIRAppManager) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRAppManager)
            End If
        Catch ex As Exception
        End Try
        If My.Computer.FileSystem.FileExists(DIRCommons & "\[" & AssemblyNameThis & "]Status_WSS.ini") = True Then
            My.Computer.FileSystem.DeleteFile(DIRCommons & "\[" & AssemblyNameThis & "]Status_WSS.ini")
        End If
        If OffLineApp_SAS = False Then
            DownloadURIServerSwitch = New Uri(ServerSwitchFileURL)
            DownloaderArrayServerSwitch.DownloadFileAsync(DownloadURIServerSwitch, DIRCommons & "\[" & AssemblyNameThis & "]Status_WSS.ini")
        Else
            Console.WriteLine("'AppService' Omitido")
        End If
    End Sub

    Private Sub DownloaderArrayServerSwitch_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles DownloaderArrayServerSwitch.DownloadFileCompleted
        ClueChangeServer(OfflineApp, SecureMode, AppManager, SignAutority, AppServiceStatus)
    End Sub

    Sub ClueChangeServer(ByVal OffLineApp_CCS As Boolean, ByVal SecureMode_CCS As Boolean, ByVal AppManager_CCS As Boolean, ByVal SignAutority_CCS As Boolean, ByVal AppServiceStatus_CCS As Boolean)
        Try
            Dim SwitchFilePath As String = DIRCommons & "\[" & AssemblyNameThis & "]Status_WSS.ini"
            UsingServer = GetIniValue("WSS", "UsingServer", SwitchFilePath)
            ServerStatus = GetIniValue("WSS", "ServerStatus", SwitchFilePath)
            ServerMSG = GetIniValue("WSS", "Message", SwitchFilePath)
            ServerURL = GetIniValue("WSS", "URL", SwitchFilePath)
            ServerArgumentLine = GetIniValue("WSS", "ArgumentLine", SwitchFilePath)
            ServerCommand = GetIniValue("WSS", "Command", SwitchFilePath)
            URLs_Update = GetIniValue("ServerSwitch", "URL_Update", SwitchFilePath)
            CurrentServerURL = GetIniValue("ServerSwitch", "CurrentServerURL", SwitchFilePath)
            URL_KeyAccessToken = GetIniValue("ServerSwitch", "URL_KeyAccessToken", SwitchFilePath)
            URL_AppService = GetIniValue("ServerSwitch", "URL_AppService", SwitchFilePath)
            URL_AppUpdate = GetIniValue("ServerSwitch", "URL_AppUpdate", SwitchFilePath)
            URL_AppUpdate_WhatsNew = GetIniValue("ServerSwitch", "URL_AppUpdate_WhatsNew", SwitchFilePath)
            URL_AppHelper_Help = GetIniValue("ServerSwitch", "URL_AppHelper_Help", SwitchFilePath)
            URL_AppHelper_About = GetIniValue("ServerSwitch", "URL_AppHelper_About", SwitchFilePath)
            URL_Support_Post = GetIniValue("ServerSwitch", "URL_AppSupport_Post", SwitchFilePath)
            URL_Telemetry_Post = GetIniValue("ServerSwitch", "URL_Telemetry_Post", SwitchFilePath)
            URL_Download_Updater = GetIniValue("ServerSwitch", "URL_Download_Updater", SwitchFilePath)
            If ServerStatus = "Stopped" Then
                If SecureMode = True Then
                    MsgBox("The Worcome Server are not working." & vbCrLf & "Try it later", MsgBoxStyle.Critical, "Worcome Security")
                    If ServerMSG = "None" Then
                    Else
                        MsgBox("Worcome Server Services" & vbCrLf & ServerMSG, MsgBoxStyle.Information, "Worcome Security")
                    End If
                    End 'END_PROGRAM
                End If
            Else
                If ServerMSG = "None" Then
                Else
                    MsgBox("Worcome Server Services" & vbCrLf & ServerMSG, MsgBoxStyle.Information, "Worcome Security")
                End If
                If ServerURL = "None" Then
                Else
                    Process.Start(ServerURL)
                End If
                If ServerArgumentLine = "None" Then
                Else
                    Process.Start(DirAppPatch, ServerArgumentLine)
                End If
                If ServerCommand = "None" Then
                Else
                    Process.Start(ServerCommand)
                End If
            End If
            Console.WriteLine("[AppService]Using Server: " & UsingServer)
            CSS1 = True
        Catch ex As Exception
            Console.WriteLine("[AppService@ServerSwitch:AnalizeInformation]Error: " & ex.Message)
            If SecureMode_CCS = True Then
                If My.Computer.Network.IsAvailable = False Then
                    MsgBox("Esta aplicación necesita acceso a internet para continuar", MsgBoxStyle.Critical, "Worcome Security")
                Else
                    MsgBox("No se pudo conectar a los Servidores de Servicios de Worcome", MsgBoxStyle.Critical, "Worcome Security")
                End If
                End 'END_PROGRAM
            End If
        End Try
        Try
            If URLs_Update = "No" Then
                If UsingServer = "WS1" Then
                    CurrentServerURL = "http://worcomestudios.comule.com"
                    URL_KeyAccessToken = "http://worcomestudios.comule.com/Recursos/InfoData/KeyAccessToken.WorCODE"
                    URL_AppService = "http://worcomestudios.comule.com/Recursos/InfoData/WorAppServices"
                    URL_AppUpdate = "http://worcomestudios.comule.com/Recursos/InfoData/Updates"
                    URL_AppUpdate_WhatsNew = "http://worcomestudios.comule.com/Recursos/InfoData/WhatsNew"
                    URL_AppHelper_Help = "http://worcomestudios.comule.com/Recursos/AppHelper"
                    URL_AppHelper_About = "http://worcomestudios.comule.com/Recursos/AppHelper/AboutApps"
                    URL_Support_Post = "http://worcomestudios.comule.com/Recursos/WorCommunity/soporte.php"
                    URL_Telemetry_Post = "http://worcomestudios.comule.com/Recursos/InfoData/TelemetryPost.php"
                ElseIf UsingServer = "WS2" Then
                    CurrentServerURL = "http://worcomestudios.mywebcommunity.org"
                    URL_KeyAccessToken = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/KeyAccessToken.WorCODE"
                    URL_AppService = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/WorAppServices"
                    URL_AppUpdate = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/Updates"
                    URL_AppUpdate_WhatsNew = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/WhatsNew"
                    URL_AppHelper_Help = "http://worcomestudios.mywebcommunity.org/Recursos/AppHelper"
                    URL_AppHelper_About = "http://worcomestudios.mywebcommunity.org/Recursos/AppHelper/AboutApps"
                    URL_Support_Post = "http://worcomestudios.mywebcommunity.org/Recursos/WorCommunity/soporte.php"
                    URL_Telemetry_Post = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/TelemetryPost.php"
                ElseIf UsingServer = "WS3" Then
                    CurrentServerURL = "http://worcomecorporations.000webhostapp.com"
                    URL_KeyAccessToken = "http://worcomecorporations.000webhostapp.com/Source/WSS/KeyAccessToken.WorCODE"
                    URL_AppService = "http://worcomecorporations.000webhostapp.com/Source/WSS/WorAppServices"
                    URL_AppUpdate = "http://worcomecorporations.000webhostapp.com/Source/WSS/Updates"
                    URL_AppUpdate_WhatsNew = "http://worcomecorporations.000webhostapp.com/Source/WSS/WhatsNew"
                    URL_AppHelper_Help = "http://worcomecorporations.000webhostapp.com/Source/WSS/AppHelper"
                    URL_AppHelper_About = "http://worcomecorporations.000webhostapp.com/Source/WSS/AppHelper/AboutApps"
                    URL_Support_Post = "http://worcomecorporations.000webhostapp.com/Source/WSS/soporte.php"
                    URL_Telemetry_Post = "http://worcomecorporations.000webhostapp.com/Source/WSS/TelemetryPost.php"
                End If
                Console.WriteLine("[AppService]Ahora estara utilizando el servidor '" & UsingServer & "'")
            End If
            CSS2 = True
        Catch ex As Exception
            Console.WriteLine("[AppService@ServerSwitch:ActionInformation]Error: " & ex.Message)
        End Try
        AppManagerCompatibility(OffLineApp_CCS, SecureMode_CCS, AppManager_CCS, SignAutority_CCS, AppServiceStatus_CCS)
    End Sub

    Sub AppManagerCompatibility(ByVal OffLineApp_AMC As Boolean, ByVal SecureMode_AMC As Boolean, ByVal AppManager_AMC As Boolean, ByVal SignAutority_AMC As Boolean, ByVal AppServiceStatus_AMC As Boolean)
        If AppManager_AMC = False Then
            Console.WriteLine("[AppService]'AppManagerCompatibility' Omitido")
            AppServiceStatusStack(OffLineApp_AMC, SecureMode_AMC, AppServiceStatus_AMC)
        Else
            Try
                If My.Computer.FileSystem.DirectoryExists(DIRAppManager) = False Then
                    My.Computer.FileSystem.CreateDirectory(DIRAppManager)
                ElseIf My.Computer.FileSystem.DirectoryExists(DIRAppManager) = True Then
                    If My.Computer.FileSystem.FileExists(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE") = False Then
                        My.Computer.FileSystem.WriteAllText(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE",
                                                            "AssemblyName>" & AssemblyNameThis &
                                                            vbCrLf & "ProductName>" & AssemblyProductNameThis &
                                                            vbCrLf & "Description>" & My.Application.Info.Description &
                                                            vbCr & "Version>" & AssemblyVersionThis &
                                                            vbCrLf & "Patch>" & DirAppPatch, False)
                        AMC = True
                    ElseIf My.Computer.FileSystem.FileExists(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE") = True Then
                        My.Computer.FileSystem.DeleteFile(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE")
                        My.Computer.FileSystem.WriteAllText(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE",
                                                            "AssemblyName>" & AssemblyNameThis &
                                                            vbCrLf & "ProductName>" & AssemblyProductNameThis &
                                                            vbCrLf & "Description>" & My.Application.Info.Description &
                                                            vbCr & "Version>" & AssemblyVersionThis &
                                                            vbCrLf & "Patch>" & DirAppPatch, False)
                        AMC = True
                    End If
                End If
            Catch ex As Exception
                Console.WriteLine("[AppService][AppManager Compatibility]Error: " & ex.Message)
            End Try
            AppServiceStatusStack(OffLineApp_AMC, SecureMode_AMC, AppServiceStatus_AMC)
        End If
    End Sub

    Sub AppServiceStatusStack(ByVal OffLineApp_ASS As Boolean, ByVal SecureMode_ASS As Boolean, ByVal AppServiceStatus_ASS As Boolean)
        If My.Computer.FileSystem.FileExists(DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini") Then
            My.Computer.FileSystem.DeleteFile(DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini")
        End If
        If AppServiceStatus_ASS = False Then
            Console.WriteLine("[AppService]'AppServiceStatus' Omitido")
            If OffLineApp_ASS = False Then
                Console.WriteLine("[AppService]Aplicacion en Linea")
                DownloadURIAppService = New Uri(URL_AppService & "/Wor_Services___" & AssemblyProductNameThis & ".WorCODE")
                DownloaderArrayAppService.DownloadFileAsync(DownloadURIAppService, DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini")
            Else
                Console.WriteLine("[AppService]Aplicacion fuera de Linea")
            End If
        Else
            DownloadURIAppService = New Uri(URL_AppService & "/Wor_Services___" & AssemblyProductNameThis & ".WorCODE")
            DownloaderArrayAppService.DownloadFileAsync(DownloadURIAppService, DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini")
        End If
    End Sub

    Private Sub DownloaderArrayAppService_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles DownloaderArrayAppService.DownloadFileCompleted
        ApplyAppService(OfflineApp, SecureMode, AppServiceStatus)
    End Sub

    Sub ApplyAppService(ByVal OffLineApp_AAS As Boolean, ByVal SecureMode_AAS As Boolean, ByVal AppServiceStatus_AAS As Boolean)
        Try
            Dim ServiceFilePath As String = DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini"
            Assembly_Status = GetIniValue("Assembly", "Status", ServiceFilePath)
            Assembly_Name = GetIniValue("Assembly", "Name", ServiceFilePath)
            Assembly_Version = GetIniValue("Assembly", "Version", ServiceFilePath)
            Runtime_URL = GetIniValue("Runtime", "URL", ServiceFilePath)
            Runtime_MSG = GetIniValue("Runtime", "MSG", ServiceFilePath)
            Runtime_ArgumentLine = GetIniValue("Runtime", "ArgumentLine", ServiceFilePath)
            Runtime_Command = GetIniValue("Runtime", "Command", ServiceFilePath)
            Updates_Critical = GetIniValue("Updates", "Critical", ServiceFilePath)
            Updates_CriticalMessage = GetIniValue("Updates", "CriticalMessage", ServiceFilePath)
            Updates_RAW_Download = GetIniValue("Updates", "RAW_Download", ServiceFilePath)
            Updates_Download = GetIniValue("Updates", "Download", ServiceFilePath)
            Installer_Status = GetIniValue("Installer", "Status", ServiceFilePath)
            Installer_DownloadFrom = GetIniValue("Installer", "DownloadFrom", ServiceFilePath)
            If Assembly_Name = AssemblyNameThis = True Then
                If Runtime_URL = "None" Then
                    Console.WriteLine("[AppService]Sin URL para Ejecutar")
                Else
                    Process.Start(Runtime_URL)
                    Console.WriteLine("[AppService]URL Ejecutada: " & Runtime_URL)
                End If
                If Runtime_MSG = "None" Then
                    Console.WriteLine("[AppService]Sin Mensajes para Ejecutar")
                Else
                    MsgBox(Runtime_MSG, MsgBoxStyle.Information, "Worcome Security")
                    Console.WriteLine("[AppService]Mensaje Ejecutado: " & Runtime_MSG)
                End If
                If Runtime_ArgumentLine = "None" Then
                    Console.WriteLine("[AppService]Sin Argumentos para Iniciar")
                Else
                    Process.Start(DirAppPatch, Runtime_ArgumentLine)
                    Console.WriteLine("[AppService]Argumento Iniciado: " & Runtime_Command)
                End If
                If Runtime_Command = "None" Then
                    Console.WriteLine("[AppService]Sin Comandos para Ejecutar")
                Else
                    Process.Start(Runtime_Command)
                    Console.WriteLine("[AppService]Comando Ejecutado: " & Runtime_Command)
                End If
                'COMPROBACION DE CUAL VERSION ES MAYOR O INFERIOR A OTRA ENTRE LOCAL Y SERVIDOR
                If Assembly_Version = "*.*.*.*" Then
                    Console.WriteLine("[AppService]Comprobacion de version omitida.")
                Else
                    Dim vL As String = AssemblyVersionThis
                    Dim vS As String = Assembly_Version
                    Dim version1 = New Version(vL)
                    Dim version2 = New Version(vS)
                    Dim result = version1.CompareTo(version2)
                    If (result > 0) Then
                        'MsgBox("Actualmente está corriendo una versión superior a la del servidor", MsgBoxStyle.Information, "Worcome Security")
                        Console.WriteLine("[AppService]La version actual esta sobre-actualizada")
                    ElseIf (result < 0) Then
                        If Updates_Critical = "True" Then
                            Console.WriteLine("[AppService]Actualizacion critica")
                            If Updates_CriticalMessage = "None" Then
                            Else
                                MsgBox(Updates_CriticalMessage, MsgBoxStyle.Information, "Worcome Security")
                                AppUpdate.CheckUpdater()
                                If My.Computer.FileSystem.FileExists(DIRCommons & "\Updater.exe") = True Then
                                    MsgBox("Se iniciara un asistente de actualizacion", MsgBoxStyle.Information, "Worcome Security")
                                    Process.Start(DIRCommons & "\Updater.exe", "/SearchForUpdates -" & AssemblyNameThis & " -" & AssemblyVersionThis & " -" & Application.ExecutablePath)
                                Else
                                    AppUpdate.ShowDialog()
                                End If
                            End If
                            End 'END_PROGRAM
                        Else
                            If Updates_CriticalMessage = "None" Then
                            Else
                                MsgBox(Updates_CriticalMessage, MsgBoxStyle.Information, "Worcome Security")
                            End If
                            Console.WriteLine("[AppService]Hay una nueva version disponible")
                            'MsgBox("Hay una Actualizacion Disponible", MsgBoxStyle.Information, "Worcome Security")
                        End If
                    Else
                        Console.WriteLine("[AppService]La Aplicacion esta Actualizada")
                    End If
                End If
                If Assembly_Status = "Enabled" Then
                    Console.WriteLine("[AppService]Aplicacion en Estado Activa")
                ElseIf Assembly_Status = "Disabled" Then
                    MsgBox("Los servicios de esta aplicación fueron desactivados por Worcome", MsgBoxStyle.Exclamation, "Worcome Security")
                    End 'END_PROGRAM
                ElseIf Assembly_Status = "Waiting" Then
                    MsgBox("Los servicios de esta aplicación están en espera...", MsgBoxStyle.Exclamation, "Worcome Security")
                    End 'END_PROGRAM
                ElseIf Assembly_Status = "Stopped" Then
                    MsgBox("Los servicios de esta aplicación fueron detenidos por Worcome", MsgBoxStyle.Exclamation, "Worcome Security")
                    End 'END_PROGRAM
                Else
                    Console.WriteLine("[AppService]Aplicacion en Estado Indefinida")
                    If SecureMode_AAS = True Then
                        Console.WriteLine("[AppService]Aplicacion en Estado Indefinida, Secure Mode esta Activado")
                        MsgBox("La aplicación está en un estado indefinido" & vbCrLf & "Secure Mode está activo" & vbCrLf & "La aplicación se cerrará", MsgBoxStyle.Critical, "Worcome Security")
                        End 'END_PROGRAM
                    End If
                End If
            End If
            AAP = True
        Catch ex As Exception
            Console.WriteLine("[AppService Status]Error: " & ex.Message)
            If SecureMode_AAS = True Then
                If My.Computer.Network.IsAvailable = False Then
                    MsgBox("Esta aplicación necesita acceso a internet para continuar", MsgBoxStyle.Critical, "Worcome Security")
                Else
                    MsgBox("No se pudo conectar a los Servidores de Servicios de Worcome", MsgBoxStyle.Critical, "Worcome Security")
                End If
                End 'END_PROGRAM
            End If
        End Try
    End Sub

    <DllImport("kernel32")>
    Private Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
        'Use GetIniValue("KEY_HERE", "SubKEY_HERE", "filepath")
    End Function

    Public Function GetIniValue(section As String, key As String, filename As String, Optional defaultValue As String = Nothing) As String
        Dim sb As New StringBuilder(500)
        If GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filename) > 0 Then
            Return sb.ToString
        Else
            Return defaultValue
        End If
    End Function
End Module
'Last update 23/10/2020 11:25 PM Chile by ElCris009
'Last update 17/09/2020 04:21 PM Argentina by Juako
'Last update 22/08/2020 06:56 PM Chile by ElCris009
'Updated 30/05/2020 09:45 PM Chile by ElCris009

'TO DO ?
'   Si es una app con SecureMode o Online obligado entonces se debera esperar a que AppService se ejhecute x completo para poder mostrar la ventana principal del programa
'   Archivos de memoria: Guarda la informacion general para cuando AppService es desactivado con el Modo Offline
'       Guardar datos de ServerSwitch
'       Guadar datos de la aplicacion para leerlos en caso de caida de servidores o modo offline