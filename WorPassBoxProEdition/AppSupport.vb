Imports System.IO
Imports System.Net
Imports System.Text
Public Class AppSupport
    Dim ComputerInfo As String
    Dim DIRCommons As String = "C:\Users\" & Environment.UserName & "\AppData\Local\Worcome_Studios\Commons\AppFiles"
    Dim SendAdjunto As Boolean = True
    Dim IdiomaApp As String = "ENG"
    Dim ErrorLogBox As String = Nothing
    Dim IDentification As String = CreateIdentification("Identification")
    Dim NombreArchivo As String = "\[" & Format(DateAndTime.TimeOfDay, "hh") & "_" & Format(DateAndTime.TimeOfDay, "mm") & "_" & Format(DateAndTime.TimeOfDay, "ss") &
                                                "@" & My.Application.Info.AssemblyName & "]ErrorLog.log"

    Private Sub AppSupportPHP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myCurrentLanguage As InputLanguage = InputLanguage.CurrentInputLanguage
        If myCurrentLanguage.Culture.EnglishName.Contains("Spanish") Then
            LANG_Español()
            IdiomaApp = "ESP"
        ElseIf myCurrentLanguage.Culture.EnglishName.Contains("English") Then
            LANG_English()
            IdiomaApp = "ENG"
        Else
            LANG_English()
            IdiomaApp = "ENG"
        End If
        If AppService.OfflineApp = False Then
            If CSS1 = False Or CSS2 = False Or AMC = False Or AAP = False Then
                If AppService.IdiomaApp = "Spanish" Then
                    MsgBox("AppService no se ejecutó correctamente. Es posible que tenga problemas con este módulo.", MsgBoxStyle.Exclamation, "Worcome Security")
                ElseIf AppService.IdiomaApp = "English" Then
                    MsgBox("AppService did not run correctly. You may have problems with this module.", MsgBoxStyle.Exclamation, "Worcome Security")
                End If
            End If
        End If
        Try
            If My.Computer.FileSystem.DirectoryExists(DIRCommons) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            ComputerInfo = vbCrLf &
                "Aplicacion: " & My.Application.Info.AssemblyName &
                vbCrLf & "      Version: " & My.Application.Info.Version.ToString &
                vbCrLf & "      Titulo: " & My.Application.Info.Title &
                vbCrLf & "      Almacenado en: " & My.Application.Info.DirectoryPath &
                vbCrLf & "      Compañia: " & My.Application.Info.CompanyName &
                vbCrLf & "Sistema Operativo: " & My.Computer.Info.OSFullName & My.Computer.Info.OSVersion &
                vbCrLf & "      RAM: " & My.Computer.Info.TotalPhysicalMemory &
                vbCrLf & "      Cuenta OS: " & My.User.Name &
                vbCrLf & "      Pantalla: " & My.Computer.Screen.Bounds.ToString & " | (Area en Uso: " & My.Computer.Screen.WorkingArea.ToString & ")" &
                vbCrLf & "      Idioma OS: " & My.Computer.Info.InstalledUICulture.NativeName &
                vbCrLf & "      Hora y Fecha Local: " & My.Computer.Clock.LocalTime &
                vbCrLf & "Informacion Compilada por la Aplicacion Nativa de Soporte"
            SaveErrorLog()
        Catch ex As Exception
            Console.WriteLine("[AppSupport@AppSupport_Load]Esto es vergonzoso, error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Then
        Else
            If TextBox1.Text.Contains("&") Or TextBox2.Text.Contains("&") Then
                If IdiomaApp = "ESP" Then
                    MsgBox("No puedes ingresar símbolos especiales (&)", MsgBoxStyle.Information, "Worcome Security")
                ElseIf IdiomaApp = "ENG" Then
                    MsgBox("You cannot enter special symbols (&", MsgBoxStyle.Information, "Worcome Security")
                Else
                    MsgBox("You cannot enter special symbols (&", MsgBoxStyle.Information, "Worcome Security")
                End If
            Else
                UploadSupportMsg()
            End If
        End If
    End Sub

    Sub UploadSupportMsg()
        Dim request As WebRequest = WebRequest.Create(URL_Support_Post)
        request.Method = "POST"
        Dim postData As String = "email=" & TextBox1.Text & "&mensaje=" &
            TextBox2.Text &
            vbCrLf & vbCrLf & vbCrLf & ComputerInfo &
            vbCrLf & vbCrLf & "Informacion del Envio: " & vbCrLf &
            ((Format(DateAndTime.TimeOfDay, "hh")) & ":") & ((Format(DateAndTime.TimeOfDay, "mm")) & ":") & ((Format(DateAndTime.TimeOfDay, "ss")) & " ") & ((Format(DateAndTime.TimeOfDay, "tt")) & " ") & (" " & (DateAndTime.Today)) &
            vbCrLf & "Idioma Selecionado: " & IdiomaApp &
            vbCrLf & "Desde el formulario: " & Me.Text &
            vbCrLf & vbCrLf & "Los mensajes pueden ser respondidos al correo que proporciono el usuario." &
            vbCrLf & "ID: " & IDentification
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()
        Dim response As WebResponse = request.GetResponse()
        Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
        If SendAdjunto = True Then
            Try
                Dim requestTLM As WebRequest = WebRequest.Create(URL_Telemetry_Post)
                requestTLM.Method = "POST"
                Dim postDataTLM As String = "ident=" & My.Application.Info.AssemblyName & "_" & IDentification & "&log=" & ErrorLogBox
                Dim byteArrayTLM As Byte() = Encoding.UTF8.GetBytes(postDataTLM)
                requestTLM.ContentType = "application/x-www-form-urlencoded"
                requestTLM.ContentLength = byteArrayTLM.Length
                Dim dataStreamTLM As Stream = requestTLM.GetRequestStream()
                dataStreamTLM.Write(byteArrayTLM, 0, byteArrayTLM.Length)
                dataStreamTLM.Close()
                Dim responseTLM As WebResponse = requestTLM.GetResponse()
                Console.WriteLine(CType(responseTLM, HttpWebResponse).StatusDescription)
                responseTLM.Close()
            Catch ex As Exception
            End Try
        End If
        If CType(response, HttpWebResponse).StatusDescription = "OK" Then
            If IdiomaApp = "ESP" Then
                MsgBox("Mensaje enviado correctamente!", MsgBoxStyle.Information, "Worcome Security")
            ElseIf IdiomaApp = "ENG" Then
                MsgBox("Message sent correctly!", MsgBoxStyle.Information, "Worcome Security")
            Else
                MsgBox("Message sent correctly!", MsgBoxStyle.Information, "Worcome Security")
            End If
        ElseIf CType(response, HttpWebResponse).StatusDescription = "Unable to open file!" Then
            If IdiomaApp = "ESP" Then
                MsgBox("No se pudo crear la consulta.", MsgBoxStyle.Information, "Worcome Security")
            ElseIf IdiomaApp = "ENG" Then
                MsgBox("The query could not be created.", MsgBoxStyle.Information, "Worcome Security")
            Else
                MsgBox("The query could not be created.", MsgBoxStyle.Information, "Worcome Security")
            End If
        Else
            If IdiomaApp = "ESP" Then
                MsgBox("No se pudo obtener una respuesta del servidor.", MsgBoxStyle.Information, "Worcome Security")
            ElseIf IdiomaApp = "ENG" Then
                MsgBox("Could not get a response from the server.", MsgBoxStyle.Information, "Worcome Security")
            Else
                MsgBox("Could not get a response from the server.", MsgBoxStyle.Information, "Worcome Security")
            End If
        End If
        response.Close()
        Me.Close()
    End Sub

#Region "Subs"
    'AddErrorLog("Nombre_Formulario_Aqui", Error_Aqui, False)
    Sub AddErrorLog(ByVal From As String, ByVal LOG As String, ByVal Important As Boolean)
        Try
            If Important = True Then
                ErrorLogBox = ErrorLogBox & "[IMPORTANT@" & From.ToString & " " & Format(DateAndTime.TimeOfDay, "hh") & ":" & Format(DateAndTime.TimeOfDay, "mm") & ":" & Format(DateAndTime.TimeOfDay, "ss") & " " & Format(DateAndTime.TimeOfDay, "tt") & "]Error: " & LOG & vbCrLf
            ElseIf Important = False Then
                ErrorLogBox = ErrorLogBox & "[" & From.ToString & " " & Format(DateAndTime.TimeOfDay, "hh") & ":" & Format(DateAndTime.TimeOfDay, "mm") & ":" & Format(DateAndTime.TimeOfDay, "ss") & " " & Format(DateAndTime.TimeOfDay, "tt") & "]Error: " & LOG & vbCrLf
            End If
        Catch ex As Exception
            Console.WriteLine("[AppSupport@AddErrorLog]Esto es vergonzoso, error: " & ex.Message)
        End Try
    End Sub

    Sub SaveErrorLog()
        Try
            If My.Computer.FileSystem.FileExists(DIRCommons & NombreArchivo) = True Then
                My.Computer.FileSystem.DeleteFile(DIRCommons & NombreArchivo)
            End If
            My.Computer.FileSystem.WriteAllText(DIRCommons & NombreArchivo, ErrorLogBox, False)
            If ErrorLogBox = Nothing Then
                If My.Computer.FileSystem.FileExists(DIRCommons & NombreArchivo) = True Then
                    My.Computer.FileSystem.DeleteFile(DIRCommons & NombreArchivo)
                End If
                SendAdjunto = False
            Else
                SetAdjunto(DIRCommons & NombreArchivo)
            End If
        Catch ex As Exception
            Console.WriteLine("[AppSupport@SaveErrorLog]Esto es vergonzoso, error: " & ex.Message)
        End Try
    End Sub

    Sub SetAdjunto(ByVal dir As String)
        'Post PHP con la ruta del archivo a subir

    End Sub

    Function CreateIdentification(ByVal CreatedString As String)
        Dim obj As New Random()
        Dim posibles As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
        Dim longitud As Integer = posibles.Length
        Dim letra As Char
        Dim longitudnuevacadena As Integer = 35
        Dim nuevacadena As String = Nothing
        For i As Integer = 0 To longitudnuevacadena - 1
            letra = posibles(obj.[Next](longitud))
            nuevacadena += letra.ToString()
        Next
        Return nuevacadena
    End Function
#End Region

#Region "LANGSelector"
    Sub LANG_Español()
        'Formulario Soporte
        Me.Text = "Wor: Support | Native Application Support"
        Label1.Text = "Soporte"
        Label2.Text = "Escribenos tu Duda, Consulta o Problema"
        Label3.Text = "Tu Correo:"
        Label4.Text = "Mensaje:"
        Button1.Text = "Enviar >"
    End Sub

    Sub LANG_English()
        'Support Form
        Me.Text = "Wor: Support | Soporte Nativa de la Aplicacion"
        Label1.Text = "Support"
        Label2.Text = "Write us your Doubt, Consultation or Problem"
        Label3.Text = "Your email:"
        Label4.Text = "Message:"
        Button1.Text = "Send >"
    End Sub
#End Region
End Class