Public Class Categories
    Dim DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\PassBoxData"

    Private Sub Categories_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Debugger.ApplyLang()
        LoadCategories()
        ListBox1.SelectedItem = Debugger.CurrentCategory
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
            My.Computer.FileSystem.WriteAllText(DIRCommons & "\Categories.lst", CryptoActions.Encriptar("Default" & vbCrLf & StringPassedFiles, CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey)), False)
            LoadCategories()
        Catch ex As Exception
        End Try
    End Sub

    Sub LoadCategories()
        PassBox.CategoryList.Clear()
        ListBox1.Items.Clear()
        Try
            Dim tmpString As New TextBox
            tmpString.Text = CryptoActions.Desencriptar(My.Computer.FileSystem.ReadAllText(DIRCommons & "\Categories.lst"), CryptoActions.Desencriptar(Debugger.Login_AccountsCryptoKey, Debugger.Login_CryptoKey))
            For Each Linea As String In tmpString.Lines
                If Linea = Nothing Then
                Else
                    PassBox.CategoryList.Add(Linea)
                    ListBox1.Items.Add(Linea)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAbrir_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If ListBox1.SelectedItem = "Default" Then
                Debugger.CurrentCategory = ListBox1.SelectedItem
                Debugger.PB_UserFiles = PassBox.DIRCommons
                Debugger.SessionShield = PassBox.DIRCommons & "\Session.ses"
                Debugger.PB_ENCFiles = PassBox.DIRCommons & "\EncryptedFiles"
                Debugger.PB_DENFiles = PassBox.DIRCommons & "\DecryptedFiles"
                PassBox.UserClose = False
                Debugger.LoadCategory()
            Else
                Dim encDir As String = PassBox.DIRCommons & "\" & ListBox1.SelectedItem & "\EncryptedFiles"
                Dim denDir As String = PassBox.DIRCommons & "\" & ListBox1.SelectedItem & "\DecryptedFiles"
                Dim flsDir As String = PassBox.DIRCommons & "\" & ListBox1.SelectedItem
                If My.Computer.FileSystem.DirectoryExists(encDir) = False Then
                    My.Computer.FileSystem.CreateDirectory(encDir)
                End If
                If My.Computer.FileSystem.DirectoryExists(denDir) = False Then
                    My.Computer.FileSystem.CreateDirectory(denDir)
                End If
                If My.Computer.FileSystem.DirectoryExists(flsDir) = False Then
                    My.Computer.FileSystem.CreateDirectory(flsDir)
                End If
                Debugger.CurrentCategory = ListBox1.SelectedItem
                Debugger.SessionShield = flsDir & "\Session.ses"
                Debugger.PB_ENCFiles = encDir
                Debugger.PB_DENFiles = denDir
                Debugger.PB_UserFiles = flsDir
                PassBox.UserClose = False
                Debugger.LoadCategory()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim TXVR = InputBox("Ingrese un nombre para la categoría." & vbCrLf & "Enter a name for the category.", "Worcome Security")
        If TXVR = Nothing Then
        Else
            If TXVR = "Default" Then
                If My.Settings.Espanglish = "ESP" Then
                    MsgBox("No puede crear una categoría con ese nombre", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf My.Settings.Espanglish = "ENG" Then
                    MsgBox("You cannot create a category with that name", MsgBoxStyle.Critical, "Worcome Security")
                End If
            Else
                PassBox.CategoryList.Add(TXVR)
                ListBox1.Items.Add(TXVR)
                SaveCategories()
            End If
        End If
    End Sub

    Private Sub btnRemover_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If ListBox1.SelectedItem = "Default" Then
                If My.Settings.Espanglish = "ESP" Then
                    MsgBox("No se puede eliminar la categoría por defecto", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf My.Settings.Espanglish = "ENG" Then
                    MsgBox("Cannot delete default category", MsgBoxStyle.Critical, "Worcome Security")
                End If
            Else
                ActionConfirm.Target(Debugger.Login_Password)
                If ActionConfirm.ShowDialog = DialogResult.OK Then
                    Dim RESULTADO As MsgBoxResult = MsgBox("¿Seguro que quieres eliminar la categoria: " & ListBox1.SelectedItem & "?" & vbCrLf & "Se eliminarán todas las cuentas que se encuentren en esta", MsgBoxStyle.YesNo, "Worcome Security")
                    If RESULTADO = MsgBoxResult.Yes Then
                        If My.Computer.FileSystem.DirectoryExists(PassBox.DIRCommons & "\" & ListBox1.SelectedItem) = True Then
                            My.Computer.FileSystem.DeleteDirectory(PassBox.DIRCommons & "\" & ListBox1.SelectedItem, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        End If
                        PassBox.CategoryList.RemoveAt(ListBox1.SelectedIndex)
                        ListBox1.Items.Remove(ListBox1.SelectedItem)
                        SaveCategories()
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
'al cambiar de categoria, se muestra el mensaje de que la sesion de passbox no se cerro, OJO, no es el mensaje de categoria, es el principal