Imports System.IO
Public Class PassBox
    Public ReadOnly DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\PassBoxData"
    Public ENCFiles As String = Debugger.PB_ENCFiles
    Public DENFiles As String = Debugger.PB_DENFiles
    Public UserFiles As String = Debugger.PB_UserFiles
    Public SessionShield As String = Debugger.SessionShield

    Public MemoryFile As New ArrayList
    Public CONTADOR As Integer = -1
    Public MemoryFileNames As New ArrayList
    Public ContadorNAME As Integer = -1

    Public UserClose As Boolean = True
    Public CategoryList As New ArrayList

    Private Sub PassBox_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Hide()
            If My.Computer.FileSystem.FileExists(UserFiles & "\FileNames.WorCODE") = True Then
                My.Computer.FileSystem.DeleteFile(UserFiles & "\FileNames.WorCODE")
            End If
            If My.Computer.FileSystem.FileExists(UserFiles & "\FileList.WorCODE") = True Then
                My.Computer.FileSystem.DeleteFile(UserFiles & "\FileList.WorCODE")
            End If
            If My.Computer.FileSystem.FileExists(SessionShield) = True Then
                My.Computer.FileSystem.DeleteFile(SessionShield)
            End If
            Dim StringPassedFiles As String = Nothing
            For Each Items As String In MemoryFile
                Dim cuttingStringENC As String() = Items.Split("\")
                Items = Items.Replace(cuttingStringENC(2), Environment.UserName)
                StringPassedFiles = StringPassedFiles & Items & vbCrLf
            Next
            My.Computer.FileSystem.WriteAllText(UserFiles & "\FileList.WorCODE", CryptoActions.Encriptar(StringPassedFiles, CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)), False)
            Dim StringPassedNames As String = Nothing
            For Each Item As String In MemoryFileNames
                StringPassedNames = StringPassedNames & Item & vbCrLf
            Next
            My.Computer.FileSystem.WriteAllText(UserFiles & "\FileNames.WorCODE", CryptoActions.Encriptar(StringPassedNames, CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)), False)
        Catch ex As Exception
            'Console.WriteLine("[PassBox@PassBox_FormClosing]Error: " & ex.Message)
        End Try
        If UserClose = True Then
            Debugger.Close()
        End If
    End Sub

    Private Sub PassBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserClose = True
        Debugger.ApplyLang()
        StartPassBox()
    End Sub

    Sub StartPassBox()
        Try
            If Debugger.Login_AccountsCryptoKey = Nothing Then
                CryptoActions.CreatePrivateKey()
            End If
            MemoryFileNames.Clear()
            Dim tempString As New TextBox
            tempString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(UserFiles & "\FileNames.WorCODE"), CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey))
            For Each Linea As String In tempString.Lines
                If Linea = Nothing Then
                Else
                    MemoryFileNames.Add(Linea)
                End If
            Next
            MemoryFile.Clear()
            Dim tmpString As New TextBox
            tmpString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(UserFiles & "\FileList.WorCODE"), CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey))
            For Each Linea As String In tmpString.Lines
                If Linea = Nothing Then
                Else
                    Dim cuttingStringENC As String() = Linea.Split("\")
                    Linea = Linea.Replace(cuttingStringENC(2), Environment.UserName)
                    MemoryFile.Add(Linea)
                End If
            Next
            CryptoActions.OnLoadApp()
            Threading.Thread.Sleep(30)
        Catch ex As Exception
            'Console.WriteLine("[PassBox@PassBox_Load:LockDirCOMPs]Error: " & ex.Message)
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists(DENFiles) = False Then
                My.Computer.FileSystem.CreateDirectory(DENFiles)
            End If
            CARGAR()
        Catch ex As Exception
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Error al Generar la Base", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Error Generating Base", MsgBoxStyle.Critical, "Worcome Security")
            End If
        End Try
        If My.Settings.Espanglish = "ESP" Then
            Label12.Text = "Categoría: " & Debugger.CurrentCategory
        ElseIf My.Settings.Espanglish = "ENG" Then
            Label12.Text = "Category: " & Debugger.CurrentCategory
        End If
    End Sub

    Private Sub btGuardarCuenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ServiceName.Text = Nothing Or Correo.Text = Nothing Or Password.Text = Nothing Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Rellena con la información solicitada", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Fill with the requested information", MsgBoxStyle.Critical, "Worcome Security")
            End If
        Else
            Try
                Dim DATAServiceName As String = ServiceName.Text
                Dim DATAUserName As String = "UserName>" & UserName.Text & vbCrLf
                Dim DATACorreo As String = "Correo>" & Correo.Text & vbCrLf
                Dim DATAPassword As String = "Password>" & Password.Text & vbCrLf
                Dim DATANotas As String = "Notas<" & Notas.Text
                Dim CuentaContenido As String = DATAUserName & DATACorreo & DATAPassword & DATANotas
                Dim NewAccountName As String = DATAServiceName & ".WorCODE"
                Dim DIRAccount As String = DENFiles
                Dim FileDir As String = Nothing
                Dim tmpString As String = Nothing
                If ServiceName.Text = HelloWorld.SelectedItem Then
                    My.Computer.FileSystem.DeleteFile(DIRAccount & "\" & NewAccountName)
                    My.Computer.FileSystem.WriteAllText(DIRAccount & "\" & NewAccountName, CryptoActions.Encriptar(CuentaContenido, CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)), False)
                Else
                    Try
                        tmpString = DIRAccount & "\" & NewAccountName
                        tmpString = tmpString.Remove(0, tmpString.LastIndexOf("\") + 1)
                        My.Computer.FileSystem.WriteAllText(DIRAccount & "\" & NewAccountName, CryptoActions.Encriptar(CuentaContenido, CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)), False)
                    Catch ex As Exception
                    End Try
                    FileDir = DIRAccount & "\" & NewAccountName
                    Dim tempString As String = Nothing
                    Dim Extencion As String = IO.Path.GetExtension(FileDir)
                    tempString = FileDir.ToString
                    tempString = tempString.Remove(0, tempString.LastIndexOf("\") + 1)
                    Dim RandomName As String = Nothing
                    RandomName = CryptoActions.CreateRandomName(tempString)
                    Threading.Thread.Sleep(150)
                    MemoryFile.Add(RandomName & Extencion & ">" & FileDir)
                    MemoryFileNames.Add(tempString)
                    RandomName = Nothing
                End If
                CARGAR()
                LIMPIA()
                ServiceName.Focus()
            Catch ex As Exception
                'Console.WriteLine("[PassBox@Button1_Click:SaveNewFileMET2]Error: " & ex.Message)
            End Try
        End If
    End Sub

    Sub LIMPIA()
        ServiceName.Clear()
        UserName.Clear()
        Correo.Clear()
        Password.Clear()
        Notas.Clear()
    End Sub

    Public Sub CARGAR()
        Try
            HelloWorld.Items.Clear()
            MemoryFileNames.Clear()
            For Each Linea As String In MemoryFile
                Linea = Linea.Remove(0, Linea.LastIndexOf("\") + 1)
                If Linea = Nothing Then
                Else
                    MemoryFileNames.Add(Linea)
                End If
                Linea = Linea.Replace(".WorCODE", "")
                If Linea = Nothing Then
                Else
                    HelloWorld.Items.Add(Linea)
                End If
            Next
        Catch ex As Exception
            'Console.WriteLine("[PassBox@CARGAR]Error: " & ex.Message)
        End Try
    End Sub

    Public Sub DATOS()
        Try
            LIMPIA()
            Dim NOMBRE As String = HelloWorld.SelectedItem
            NOMBRE = NOMBRE.Remove(0, NOMBRE.LastIndexOf("\") + 1)
            NOMBRE = NOMBRE.Replace(".WorCODE", "")
            ServiceName.Text = NOMBRE
            Dim tempString As New TextBox
            Dim NombreAleatorio As String = MemoryFile(CONTADOR)
            Dim Cadena As String() = NombreAleatorio.Split(">")
            Dim RandomName As String = Nothing
            Dim OriginalName As String = Nothing
            RandomName = Cadena(0)
            OriginalName = Cadena(1)
            tempString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(OriginalName), CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey))
            Dim Lineas = tempString.Lines
            UserName.Text = Lineas(0).Split(">"c)(1).Trim()
            Correo.Text = Lineas(1).Split(">"c)(1).Trim()
            Password.Text = Lineas(2).Split(">"c)(1).Trim()
            Try
                Dim tmpString As String = tempString.Text
                Notas.Text = tmpString.Remove(0, tmpString.LastIndexOf("<") + 1)
            Catch ex As Exception
                Notas.Clear()
                Console.WriteLine("No compatible")
            End Try
        Catch ex As Exception
            'Console.WriteLine("[PassBox@DATOS]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnBuscarCuenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim ENCONTRADO As Boolean
            Dim NOMBRE As String = InputBox("Busca la cuenta según el nombre del servicio." & vbCrLf & "Search for the account according to the name of the service.", "Worcome Security")
            For I = 0 To HelloWorld.Items.Count - 1
                If HelloWorld.Items(I) = NOMBRE Then
                    HelloWorld.SelectedIndex = I
                    ENCONTRADO = True
                    Exit For
                End If
            Next
            If ENCONTRADO = False Then
                If My.Settings.Espanglish = "ESP" Then
                    MsgBox("No se pudo encontrar", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf My.Settings.Espanglish = "ENG" Then
                    MsgBox("Could not be found", MsgBoxStyle.Critical, "Worcome Security")
                End If
            End If
        Catch ex As Exception
            'Console.WriteLine("[PassBox@Button2_Click]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelloWorld.SelectedIndexChanged
        Try
            If HelloWorld.SelectedItem.ToString.StartsWith("->") Then
            Else
                CONTADOR = HelloWorld.SelectedIndex
                DATOS()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnBorrarCuenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            ActionConfirm.Target(Debugger.Login_Password)
            If ActionConfirm.ShowDialog = DialogResult.OK Then
                Dim RESULTADO As MsgBoxResult = MsgBox("¿Seguro que quieres eliminar la información relacionada con: " & ServiceName.Text & "?", MsgBoxStyle.YesNo, "Worcome Security")
                If RESULTADO = MsgBoxResult.Yes Then
                    MemoryFile.RemoveAt(HelloWorld.SelectedIndex)
                    MemoryFileNames.Remove(HelloWorld.SelectedItem & ".WorCODE")
                    Try
                        File.Delete(DENFiles & "\" & ServiceName.Text & ".WorCODE")
                    Catch ex As Exception
                    End Try
                    CARGAR()
                    If My.Settings.Espanglish = "ESP" Then
                        MsgBox("Información eliminada correctamente", MsgBoxStyle.Information, "Worcome Security")
                    ElseIf My.Settings.Espanglish = "ENG" Then
                        MsgBox("Information successfully deleted", MsgBoxStyle.Information, "Worcome Security")
                    End If
                    LIMPIA()
                End If
            Else
                Me.Close()
            End If
        Catch ex As Exception
            'Console.WriteLine("[PassBox@Button3_Click]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub BorrarCuentaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrarCuentaToolStripMenuItem.Click
        Try
            ActionConfirm.Target(Debugger.Login_Password)
            If ActionConfirm.ShowDialog = DialogResult.OK Then
                Dim RESULTADO As MsgBoxResult = MsgBox("¿Seguro que quieres eliminar la información relacionada con: " & ServiceName.Text & "?", MsgBoxStyle.YesNo, "Worcome Security")
                If RESULTADO = MsgBoxResult.Yes Then
                    MemoryFile.RemoveAt(HelloWorld.SelectedIndex)
                    MemoryFileNames.Remove(HelloWorld.SelectedItem & ".WorCODE")
                    Try
                        File.Delete(DENFiles & "\" & ServiceName.Text & ".WorCODE")
                    Catch ex As Exception
                    End Try
                    CARGAR()
                    If My.Settings.Espanglish = "ESP" Then
                        MsgBox("Información eliminada correctamente", MsgBoxStyle.Information, "Worcome Security")
                    ElseIf My.Settings.Espanglish = "ENG" Then
                        MsgBox("Information successfully deleted", MsgBoxStyle.Information, "Worcome Security")
                    End If
                    LIMPIA()
                End If
            Else
                Me.Close()
            End If
        Catch ex As Exception
            'Console.WriteLine("[PassBox@Button3_Click]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        If Password.PasswordChar = "●" Then
            Password.PasswordChar = ""
            If My.Settings.Espanglish = "ESP" Then
                Label5.Text = "Ocultar"
            ElseIf My.Settings.Espanglish = "ENG" Then
                Label5.Text = "Hide"
            End If
        Else
            Password.PasswordChar = "●"
            If My.Settings.Espanglish = "ESP" Then
                Label5.Text = "Mostrar"
            ElseIf My.Settings.Espanglish = "ENG" Then
                Label5.Text = "Show"
            End If
        End If
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        About.Show()
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        Backup.Show()
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CARGAR()
    End Sub

    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Config.Show()
        Config.Focus()
    End Sub

    Private Sub btnNuevaCuenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        HelloWorld.SelectedItem = Nothing
        ServiceName.Clear()
        UserName.Clear()
        Correo.Clear()
        Password.Clear()
        Notas.Clear()
    End Sub

    Private Sub ServiceName_KeyDown(sender As Object, e As KeyEventArgs) Handles ServiceName.KeyDown
        If e.KeyCode = Keys.Enter Then
            UserName.Focus()
        End If
    End Sub

    Private Sub UserName_KeyDown(sender As Object, e As KeyEventArgs) Handles UserName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Correo.Focus()
        End If
    End Sub

    Private Sub Correo_KeyDown(sender As Object, e As KeyEventArgs) Handles Correo.KeyDown
        If e.KeyCode = Keys.Enter Then
            Password.Focus()
        End If
    End Sub

    Private Sub Password_KeyDown(sender As Object, e As KeyEventArgs) Handles Password.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Try
            Dim obj As New Random()
            Dim posibles As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890"
            Dim longitud As Integer = posibles.Length
            Dim letra As Char
            Dim longitudnuevacadena As Integer = 13
            Dim nuevacadena As String = Nothing
            For i As Integer = 0 To longitudnuevacadena - 1
                letra = posibles(obj.[Next](longitud))
                nuevacadena += letra.ToString()
            Next
            Password.Text = nuevacadena
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Notas_TextChanged(sender As Object, e As EventArgs) Handles Notas.TextChanged
        If Notas.Text.Contains("<") Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Ciertos caracteres no están permitidos", MsgBoxStyle.Information, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Certain characters are not allowed", MsgBoxStyle.Information, "Worcome Security")
            End If
        ElseIf Notas.Text.Contains("UserName>") Or Notas.Text.Contains("Password>") Then
            Notas.Clear()
        End If
        Notas.Text = Notas.Text.Replace("<", Nothing)
    End Sub

    Private Sub ServiceName_TextChanged(sender As Object, e As EventArgs) Handles ServiceName.TextChanged
        If ServiceName.Text.Contains(">") Or ServiceName.Text.Contains("\") Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Ciertos caracteres no están permitidos", MsgBoxStyle.Information, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Certain characters are not allowed", MsgBoxStyle.Information, "Worcome Security")
            End If
            ServiceName.Text = ServiceName.Text.Replace(">", Nothing)
            ServiceName.Text = ServiceName.Text.Replace("\", Nothing)
        End If
    End Sub

    Private Sub UserName_TextChanged(sender As Object, e As EventArgs) Handles UserName.TextChanged
        If UserName.Text.Contains(">") Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Ciertos caracteres no están permitidos", MsgBoxStyle.Information, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Certain characters are not allowed", MsgBoxStyle.Information, "Worcome Security")
            End If
            UserName.Text = UserName.Text.Replace(">", Nothing)
        End If
    End Sub

    Private Sub Correo_TextChanged(sender As Object, e As EventArgs) Handles Correo.TextChanged
        If Correo.Text.Contains(">") Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Ciertos caracteres no están permitidos", MsgBoxStyle.Information, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Certain characters are not allowed", MsgBoxStyle.Information, "Worcome Security")
            End If
        End If
        Correo.Text = Correo.Text.Replace(">", Nothing)
    End Sub

    Private Sub Password_TextChanged(sender As Object, e As EventArgs) Handles Password.TextChanged
        If Password.Text.Contains(">") Then
            If My.Settings.Espanglish = "ESP" Then
                MsgBox("Ciertos caracteres no están permitidos", MsgBoxStyle.Information, "Worcome Security")
            ElseIf My.Settings.Espanglish = "ENG" Then
                MsgBox("Certain characters are not allowed", MsgBoxStyle.Information, "Worcome Security")
            End If
        End If
        Password.Text = Password.Text.Replace(">", Nothing)
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        If Notas.PasswordChar = "●" Then
            Notas.PasswordChar = ""
            If My.Settings.Espanglish = "ESP" Then
                Label11.Text = "Ocultar"
            ElseIf My.Settings.Espanglish = "ENG" Then
                Label11.Text = "Hide"
            End If
        Else
            Notas.PasswordChar = "●"
            If My.Settings.Espanglish = "ESP" Then
                Label11.Text = "Mostrar"
            ElseIf My.Settings.Espanglish = "ENG" Then
                Label11.Text = "Show"
            End If
        End If
    End Sub

    Dim POSICIONI, POSICIONF
    Private Sub HelloWorld_MouseUp(sender As Object, e As MouseEventArgs) Handles HelloWorld.MouseUp
        Try
            POSICIONF = HelloWorld.SelectedIndex
            If POSICIONF <> POSICIONI Then
                If POSICIONF < POSICIONI Then
                    MemoryFile.Insert(POSICIONF, MemoryFile(POSICIONI))
                    MemoryFile.RemoveAt(POSICIONI + 1)
                Else
                    MemoryFile.Insert(POSICIONF + 1, MemoryFile(POSICIONI))
                    MemoryFile.RemoveAt(POSICIONI)
                End If
                HelloWorld.Items.Clear()
                For Each ELEMENTO In MemoryFile
                    ELEMENTO = ELEMENTO.Remove(0, ELEMENTO.LastIndexOf("\") + 1)
                    ELEMENTO = ELEMENTO.Replace(".WorCODE", "")
                    HelloWorld.Items.Add(ELEMENTO)
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AgregarUnSeparadorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarUnSeparadorToolStripMenuItem.Click
        Dim TextBoxInput = InputBox("Escribe el nombre del Separador", "Agregar un Separador")
        If TextBoxInput = Nothing Then
        Else
            HelloWorld.Items.Add("->" & TextBoxInput)
            MemoryFile.Add("->" & TextBoxInput)
        End If
    End Sub

    Private Sub QuitarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitarToolStripMenuItem.Click
        MemoryFile.RemoveAt(HelloWorld.SelectedIndex)
        MemoryFileNames.RemoveAt(HelloWorld.SelectedIndex)
        HelloWorld.Items.RemoveAt(HelloWorld.SelectedIndex)
    End Sub

    Private Sub RecargarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecargarToolStripMenuItem.Click
        CARGAR()
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        'para mover una cuenta de categoria deberan pasar varios pasos:
        '   cifrar el archivo de cuenta en EncryptedFiles de la categoria actual (o moverla de una a la EncryptedFiles de la categoria seleccionada, saltando el segundo paso)
        '   Luego moverlo a la carpeta EncryptedFiles de la categoria seleccionada
        '   a continuacion, se debera agregar ese archivo las listas FileNames y FileList de esa categoria
        '   y en teoria eso es todo. en la practica es todo algo diferente.
    End Sub

    Private Sub btnCategorias_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Categories.Show()
        Categories.Focus()
    End Sub

    Private Sub ActivarModoConcentradoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActivarModoConcentradoToolStripMenuItem.Click
        If TopMost = False Then
            If My.Settings.Espanglish = "ESP" Then
                ActivarModoConcentradoToolStripMenuItem.Text = "Desactivar modo concentrado"
            ElseIf My.Settings.Espanglish = "ENG" Then
                ActivarModoConcentradoToolStripMenuItem.Text = "Disable focus mode"
            End If
            TopMost = True
        Else
            If My.Settings.Espanglish = "ESP" Then
                ActivarModoConcentradoToolStripMenuItem.Text = "Activar modo concentrado"
            ElseIf My.Settings.Espanglish = "ENG" Then
                ActivarModoConcentradoToolStripMenuItem.Text = "Enable focus mode"
            End If
            TopMost = False
        End If
    End Sub

    Private Sub HelloWorld_MouseDown(sender As Object, e As MouseEventArgs) Handles HelloWorld.MouseDown
        POSICIONI = HelloWorld.SelectedIndex
    End Sub
End Class