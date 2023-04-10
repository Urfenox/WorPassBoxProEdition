Public Class Categories
    Dim DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\PassBoxData"

    Private Sub Categories_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Debugger.ApplyLang()
        LoadCategories()
        lstCategory.SelectedItem = Debugger.CurrentCategory
    End Sub

    Sub SaveCategories()
        Try
            Dim StringPassedFiles As String = Nothing
            For Each Items As String In PassBox.CategoryList
                StringPassedFiles = StringPassedFiles & Items & vbCrLf
            Next
            Dim errString As String = StringPassedFiles
            Dim correctString As String = errString.Replace("Default", Nothing)
            StringPassedFiles = correctString
            My.Computer.FileSystem.WriteAllText(DIRCommons & "\Categories.lst", CryptoActions.Encriptar("Default" & vbCrLf & "SyncServer" & vbCrLf & StringPassedFiles, CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)), False)
            LoadCategories()
        Catch ex As Exception
        End Try
    End Sub

    Sub LoadCategories()
        PassBox.CategoryList.Clear()
        lstCategory.Items.Clear()
        Try
            Dim tmpString As New TextBox
            tmpString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(DIRCommons & "\Categories.lst"), CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey))
            For Each Linea As String In tmpString.Lines
                If Linea = Nothing Then
                Else
                    PassBox.CategoryList.Add(Linea)
                    lstCategory.Items.Add(Linea)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAbrir_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Try
            If lstCategory.SelectedItem = Nothing Then
            ElseIf lstCategory.SelectedItem = "Default" Then
                Debugger.CurrentCategory = lstCategory.SelectedItem
                Debugger.PB_UserFiles = PassBox.DIRCommons
                Debugger.SessionShield = PassBox.DIRCommons & "\Session.ses"
                Debugger.PB_ENCFiles = PassBox.DIRCommons & "\EncryptedFiles"
                Debugger.PB_DENFiles = PassBox.DIRCommons & "\DecryptedFiles"
                PassBox.UserClose = False
                Debugger.LoadCategory()
            ElseIf lstCategory.SelectedItem = "SyncServer" Then
                Select Case AppLicenser.GetAppLicense()
                    Case 0 'Sin licencia
                        MsgBox("Debe tener una licencia para poder utilizar esta funcion", MsgBoxStyle.Critical, "Worcome Security")
                    Case 1 'Con licencia
                        OpenCategory("SyncServer\Accounts")
                    Case Else 'Otra situacion controlada
                End Select
            Else
                OpenCategory(lstCategory.SelectedItem)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub OpenCategory(ByVal cat As String)
        Dim encDir As String = PassBox.DIRCommons & "\" & cat & "\EncryptedFiles"
        Dim denDir As String = PassBox.DIRCommons & "\" & cat & "\DecryptedFiles"
        Dim flsDir As String = PassBox.DIRCommons & "\" & cat
        If My.Computer.FileSystem.DirectoryExists(encDir) = False Then
            My.Computer.FileSystem.CreateDirectory(encDir)
        End If
        If My.Computer.FileSystem.DirectoryExists(denDir) = False Then
            My.Computer.FileSystem.CreateDirectory(denDir)
        End If
        If My.Computer.FileSystem.DirectoryExists(flsDir) = False Then
            My.Computer.FileSystem.CreateDirectory(flsDir)
        End If
        ''No necesario por que la categoria debe estar cerrada para poder subirse
        'If Debugger.CurrentCategory = "SyncServer" Then
        '    CryptoActions.OnClose()
        '    ServerSync.PackerForUpload()
        'End If
        If cat = "SyncServer\Accounts" Then
            Debugger.CurrentCategory = "SyncServer"
        Else
            Debugger.CurrentCategory = cat
        End If
        Debugger.SessionShield = flsDir & "\Session.ses"
        Debugger.PB_ENCFiles = encDir
        Debugger.PB_DENFiles = denDir
        Debugger.PB_UserFiles = flsDir
        PassBox.UserClose = False
        Debugger.LoadCategory()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Dim TXVR = InputBox("Ingrese un nombre para la categoría." & vbCrLf & "Enter a name for the category.", "Worcome Security")
        If TXVR = Nothing Then
        Else
            If TXVR = "Default" Or TXVR = "SyncServer" Then
                If Debugger.Espanglish = "ESP" Then
                    MsgBox("No puede crear una categoría con ese nombre", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf Debugger.Espanglish = "ENG" Then
                    MsgBox("You cannot create a category with that name", MsgBoxStyle.Critical, "Worcome Security")
                End If
            Else
                PassBox.CategoryList.Add(TXVR)
                lstCategory.Items.Add(TXVR)
                SaveCategories()
            End If
        End If
    End Sub

    Private Sub btnRemover_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            If lstCategory.SelectedItem = "Default" Or lstCategory.SelectedItem = "SyncServer" Then
                If Debugger.Espanglish = "ESP" Then
                    MsgBox("No se puede eliminar la categoría", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf Debugger.Espanglish = "ENG" Then
                    MsgBox("Cannot delete category", MsgBoxStyle.Critical, "Worcome Security")
                End If
            Else
                ActionConfirm.Target(Debugger.Login_Password)
                If ActionConfirm.ShowDialog = DialogResult.OK Then
                    Dim RESULTADO As MsgBoxResult = MsgBox("¿Seguro que quieres eliminar la categoria: " & lstCategory.SelectedItem & "?" & vbCrLf & "Se eliminarán todas las cuentas que se encuentren en esta", MsgBoxStyle.YesNo, "Worcome Security")
                    If RESULTADO = MsgBoxResult.Yes Then
                        If My.Computer.FileSystem.DirectoryExists(PassBox.DIRCommons & "\" & lstCategory.SelectedItem) = True Then
                            My.Computer.FileSystem.DeleteDirectory(PassBox.DIRCommons & "\" & lstCategory.SelectedItem, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        End If
                        PassBox.CategoryList.RemoveAt(lstCategory.SelectedIndex)
                        lstCategory.Items.Remove(lstCategory.SelectedItem)
                        SaveCategories()
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSyncServer_Click(sender As Object, e As EventArgs) Handles btnSyncServer.Click
        Select Case AppLicenser.GetAppLicense()
            Case 0 'Sin licencia
                'Inactiva, obtener licencia...
                If AppLicenser.ShowDialog() = DialogResult.Cancel Then
                    AppLicenser.Close()
                End If
            Case 1 'Con licencia
                'Activa, continuar
                ServerSync.Show()
                ServerSync.Focus()
            Case Else 'Otra situacion controlada
        End Select
    End Sub
End Class
'al cambiar de categoria, se muestra el mensaje de que la sesion de passbox no se cerro, OJO, no es el mensaje de categoria, es el principal