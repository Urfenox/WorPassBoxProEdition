Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Security.Principal
Imports System.Security.AccessControl
Public Class LockDir
    Dim DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\PassBoxData"
    Public DIREncrypted As String = DIRCommons & "\EncryptedFiles"
    Public DIRDecrypted As String = DIRCommons & "\DecryptedFiles"
    Public DefaultCryptoKey As String = "GI2ttibIY56gUYED52fUfIUHo6T97rIYviUR8647td"  'Default Cryptography Key
    Public CryptoKey As String = ""  'Cryptography Key
    Public Shared des As New TripleDESCryptoServiceProvider
    Public Shared hashmd5 As New MD5CryptoServiceProvider

    Private Sub LockDir_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Sub OnLoad()
        Try
            PassBox.ContadorNAME = 0
            For Each Fichero As String In My.Computer.FileSystem.GetFiles(DIREncrypted)
                CallDecrypt(Fichero, DIRDecrypted & "\" & PassBox.MemoryListNAME(PassBox.ContadorNAME))
                PassBox.ContadorNAME = PassBox.ContadorNAME + 1
            Next
            For Each Archivo As String In My.Computer.FileSystem.GetFiles(DIREncrypted)
                My.Computer.FileSystem.DeleteFile(Archivo)
            Next
            UnLockDirectory()
        Catch ex As Exception
            Console.WriteLine("[LockDir@OnLoad]Error: " & ex.Message)
        End Try
    End Sub

    Sub OnClose()
        Try
            PassBox.ContadorNAME = 0
            For Each Fichero As String In My.Computer.FileSystem.GetFiles(DIRDecrypted)
                CallEncrypt(Fichero, DIREncrypted & "\" & PassBox.MemoryListNAME(PassBox.ContadorNAME))
                PassBox.ContadorNAME = PassBox.ContadorNAME + 1
            Next
            For Each Archivo As String In My.Computer.FileSystem.GetFiles(DIRDecrypted)
                My.Computer.FileSystem.DeleteFile(Archivo)
            Next
            LockDirectory()
        Catch ex As Exception
            Console.WriteLine("[LockDir@OnClose]Error: " & ex.Message)
        End Try
    End Sub

#Region "COMPSubs"
    Function Encriptar(ByVal texto As String, ByVal Password As String) As String
        If Trim(texto) = "" Then
            Encriptar = ""
        Else
            If Password = Nothing Then
                des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(DefaultCryptoKey))
            Else
                des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(Password))
            End If
            des.Mode = CipherMode.ECB
            Dim encrypt As ICryptoTransform = des.CreateEncryptor()
            Dim buff() As Byte = UnicodeEncoding.UTF8.GetBytes(texto)
            Encriptar = Convert.ToBase64String(encrypt.TransformFinalBlock(buff, 0, buff.Length))
        End If
        Return Encriptar
    End Function

    Function Desencriptar(ByVal texto As String, ByVal Password As String) As String
        If Trim(texto) = "" Then
            Desencriptar = ""
        Else
            If Password = Nothing Then
                des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(DefaultCryptoKey))
            Else
                des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(Password))
            End If
            des.Mode = CipherMode.ECB
            Dim desencrypta As ICryptoTransform = des.CreateDecryptor()
            Dim buff() As Byte = Convert.FromBase64String(texto)
            Desencriptar = UnicodeEncoding.UTF8.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length))
        End If
        Return Desencriptar
    End Function

    Sub LockDirectory()
        Try
            Dim attribute As System.IO.FileAttributes = FileAttributes.Hidden
            File.SetAttributes(DIRCommons, attribute)
        Catch ex As Exception
            Console.WriteLine("[LockDir@LockDirectory:HiddeFolder]Error: " & ex.Message)
        End Try
        Try
            Dim ACCESO As FileSystemSecurity = File.GetAccessControl(DIRCommons)
            Dim USUARIOS As New SecurityIdentifier(WellKnownSidType.WorldSid, Nothing)
            ACCESO.AddAccessRule(New FileSystemAccessRule(USUARIOS, FileSystemRights.FullControl, AccessControlType.Deny))
            File.SetAccessControl(DIRCommons, ACCESO)
            Console.WriteLine("Directorio Bloqueado: " & DIRCommons)
        Catch ex As Exception
            Console.WriteLine("[LockDir@LockDirectory:ControlLock]Error: " & ex.Message)
        End Try
    End Sub
    Sub UnLockDirectory()
        Try
            Dim attribute As System.IO.FileAttributes = FileAttributes.Normal
            'File.SetAttributes(DIRCommons, attribute)
        Catch ex As Exception
            Console.WriteLine("[LockDir@LockDirectory:ShowFolder]Error: " & ex.Message)
        End Try
        Try
            Dim ACCESO As FileSystemSecurity = File.GetAccessControl(DIRCommons)
            Dim USUARIOS As New SecurityIdentifier(WellKnownSidType.WorldSid, Nothing)
            ACCESO.RemoveAccessRule(New FileSystemAccessRule(USUARIOS, FileSystemRights.FullControl, AccessControlType.Deny))
            File.SetAccessControl(DIRCommons, ACCESO)
            Console.WriteLine("Directorio Desbloqueado: " & DIRCommons)
        Catch ex As Exception
            Console.WriteLine("[LockDir@LockDirectory:ControlUnLock]Error: " & ex.Message)
        End Try
    End Sub

    Sub CreatePrivateKey()
        Try
            Dim obj As New Random()
            Dim posibles As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
            Dim longitud As Integer = posibles.Length
            Dim letra As Char
            Dim longitudnuevacadena As Integer = 30
            Dim nuevacadena As String = Nothing
            For i As Integer = 0 To longitudnuevacadena - 1
                letra = posibles(obj.[Next](longitud))
                nuevacadena += letra.ToString()
            Next
            CryptoKey = nuevacadena
            Debugger.SaveData()
            My.Settings.Save()
            My.Settings.Reload()
            Console.WriteLine("Llave criptografica privada: " & nuevacadena)
        Catch ex As Exception
            CryptoKey = DefaultCryptoKey
            My.Settings.Save()
            My.Settings.Reload()
            Console.WriteLine("[LockDir@CreatePrivateKey]Error: " & ex.Message)
        End Try
    End Sub

    Sub CreateDataBasePrivateKey()
        Try
            Dim obj As New Random()
            Dim posibles As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
            Dim longitud As Integer = posibles.Length
            Dim letra As Char
            Dim longitudnuevacadena As Integer = 30
            Dim nuevacadena As String = Nothing
            For i As Integer = 0 To longitudnuevacadena - 1
                letra = posibles(obj.[Next](longitud))
                nuevacadena += letra.ToString()
            Next
            My.Settings.DBCryptoKey = Encriptar(nuevacadena, DefaultCryptoKey)
            My.Settings.Save()
            My.Settings.Reload()
            Console.WriteLine("Llave criptografica DB privada: " & nuevacadena)
        Catch ex As Exception
            My.Settings.DBCryptoKey = DefaultCryptoKey
            My.Settings.Save()
            My.Settings.Reload()
            Console.WriteLine("[LockDir@CreateDBPrivateKey]Error: " & ex.Message)
        End Try
    End Sub

    Sub CallEncrypt(ByVal FileIN As String, ByVal FileOUT As String)
        Try
            Dim buffer As Byte()
            Using fs As New FileStream(FileIN, FileMode.Open, FileAccess.Read)
                Using ms As New MemoryStream()
                    Encrypt(fs, ms, CryptoKey)
                    buffer = ms.ToArray()
                End Using
            End Using
            Using fs As New FileStream(FileOUT, FileMode.CreateNew, FileAccess.Write)
                fs.Write(buffer, 0, buffer.Length)
            End Using
        Catch ex As Exception
            Console.WriteLine("[LockDir@CallEncrypt]Error: " & ex.Message)
        End Try
    End Sub
    Sub CallDecrypt(ByVal FileIN As String, ByVal FileOUT As String)
        Try
            Dim buffer As Byte() = Nothing
            Using fs As New FileStream(FileIN, FileMode.Open, FileAccess.Read)
                Using ms As New MemoryStream()
                    Decrypt(fs, ms, CryptoKey)
                    buffer = ms.ToArray()
                End Using
            End Using
            Using fs As New FileStream(FileOUT, FileMode.CreateNew, FileAccess.Write)
                fs.Write(buffer, 0, buffer.Length)
            End Using
        Catch ex As Exception
            Console.WriteLine("[LockDir@CallDecrypt]Error: " & ex.Message)
        End Try
    End Sub
    Friend Shared Sub Decrypt(inStream As Stream, outStream As Stream, pwd As String)
        Try
            Dim aes As New AesCryptoServiceProvider()
            aes.Mode = CipherMode.CFB
            aes.Key() = GetDeriveBytes(pwd, 32)
            aes.IV = GetDeriveBytes(pwd, 16)
            Dim stream As New CryptoStream(inStream, aes.CreateDecryptor(), CryptoStreamMode.Read)
            Dim length As Integer = 2048
            Dim buffer As Byte() = New Byte(length - 1) {}
            Try
                Dim i As Integer = stream.Read(buffer, 0, length)
                Do While (i > 0)
                    outStream.Write(buffer, 0, i)
                    i = stream.Read(buffer, 0, length)
                Loop
            Finally
                aes.Dispose()
                buffer = Nothing
            End Try
        Catch ex As Exception
            Console.WriteLine("[LockDir@Decrypt]Error: " & ex.Message)
        End Try
    End Sub
    Friend Shared Sub Encrypt(inStream As Stream, outStream As Stream, pwd As String)
        Try
            Dim aes As New AesCryptoServiceProvider()
            aes.Mode = CipherMode.CFB
            aes.Key() = GetDeriveBytes(pwd, 32)
            aes.IV = GetDeriveBytes(pwd, 16)
            Dim stream As New CryptoStream(outStream, aes.CreateEncryptor(), CryptoStreamMode.Write)
            Dim length As Integer = 2048
            Dim buffer As Byte() = New Byte(length - 1) {}
            Try
                Dim i As Integer = inStream.Read(buffer, 0, length)
                Do While (i > 0)
                    stream.Write(buffer, 0, i)
                    i = inStream.Read(buffer, 0, length)
                Loop
            Finally
                stream.FlushFinalBlock()
                aes.Dispose()
                buffer = Nothing
            End Try
        Catch ex As Exception
            Console.WriteLine("[LockDir@Encrypt]Error: " & ex.Message)
        End Try
    End Sub
    Friend Shared Function GetDeriveBytes(password As String, size As Integer) As Byte()
        If ((String.IsNullOrWhiteSpace(password)) OrElse (password.Length < 8)) Then
            MsgBox("Error en el Modulo 'GetDeriveBytes'" & vbCrLf & "La llave criptografica debe tener mas de 8 caracteres", MsgBoxStyle.Critical, "SystemTrail Modules")
        End If
        If ((size < 1) OrElse (size > 128)) Then
            MsgBox("Error en el Modulo 'GetDeriveBytes'" & vbCrLf & "El tamaño tiene que estar comprendido entre 1 y 128.", MsgBoxStyle.Critical, "SystemTrail Modules")
        End If
        Dim pwd As Byte() = UTF8Encoding.UTF8.GetBytes(password)
        Dim salt As Byte() = UTF8Encoding.UTF8.GetBytes(Convert.ToBase64String(pwd))
        Using bytes As New Rfc2898DeriveBytes(pwd, salt, 1000)
            ' Devolver la clave pseudoaletoria.
            Return bytes.GetBytes(size)
        End Using
    End Function
#End Region
End Class
