Public Class Idioma

    Public Class Ingles
        Shared Sub LANG_English()
            'SignUp Form
            SignUp.Label1.Text = "Sign Up"
            SignUp.Label2.Text = "User Name"
            SignUp.Label3.Text = "Password"
            SignUp.Label4.Text = "Email"
            SignUp.Label6.Text = "Show"
            SignUp.Button1.Text = "Sign Up"

            'SignIn Form
            SignIn.Label1.Text = "Sign In"
            SignIn.Label2.Text = "User Name"
            SignIn.Label3.Text = "Password"
            SignIn.Label4.Text = "Show"
            SignIn.Label5.Text = "Have you forgotten your Password?"
            SignIn.Button1.Text = "Sign In"

            'PassBox Form
            PassBox.Label1.Text = "Service Name:"
            PassBox.Label2.Text = "User Name:"
            PassBox.Label3.Text = "Email:"
            PassBox.Label4.Text = "Password:"
            PassBox.Label5.Text = "Show"
            PassBox.Label7.Text = "Backups >"
            PassBox.Label9.Text = "Notes: "
            PassBox.Label10.Text = "Random"
            PassBox.Label11.Text = "Show"
            PassBox.Button6.Text = "New Account"
            PassBox.Button1.Text = "Save Account"
            PassBox.Button2.Text = "Search"
            PassBox.Button3.Text = "Delete Account"
            PassBox.Button4.Text = "Categories"
            PassBox.Button5.Text = "Configuration"

            'Config Form
            Config.Text = "Wor: PassBox | Configuration"
            Config.Label1.Text = "Configuration"
            Config.TabPage3.Text = "Data Base"
            Config.TabPage4.Text = "Accounts"
            Config.TabPage1.Text = "Keys"
            Config.Label2.Text = "Data Base"
            Config.Label3.Text = "User Name:"
            Config.Label4.Text = "Email:"
            Config.Label5.Text = "Password:"
            Config.Label6.Text = "Current CryptoKey"
            Config.Label7.Text = "Not Available or in Development"
            Config.Label9.Text = "Show"
            Config.Label11.Text = "Access Keys"
            Config.GroupBox1.Text = "Access Keys Type"
            Config.RadioButton1.Text = "Start PassBox"
            Config.RadioButton2.Text = "Start PassBox and log in"
            Config.Button1.Text = "Save Changes"
            Config.Button3.Text = "Write a CryptoKey"
            Config.Button4.Text = "Generate a new CryptoKey"
            Config.Button5.Text = "Show CryptoKey"
            Config.Button6.Text = "Create"
            Config.DomainUpDown2.Text = "Default (Yes)"

            'BackUp Form
            Backup.Text = "Wor: PassBox | Security Copy"
            Backup.TabPage1.Text = "Create Backup"
            Backup.TabPage2.Text = "Read Backup"
            Backup.Label1.Text = "Create a Security Copy"
            Backup.Label2.Text = "Creating a Backup Copy will make all of your Accounts Copy to your Desktop." &
                vbCrLf & "They will be protected under encryption of the CryptoActions module, nobody will be able to read them."
            Backup.Label3.Text = "Create Backup"
            Backup.Label4.Text = "Read Backup"
            Backup.Label5.Text = "The Backup will be created on your desktop and will be encrypted." &
                vbCrLf & "To upload the Backup Copy back to PassBox, go to the 'Read Backup' tab" &
                vbCrLf & "Also the cryptographic key will be copied to your clipboard, save it, it will serve you when you go to read the backup copy"
            Backup.Label6.Text = "Drag and drop the folder with the name 'PassBook Accounts' in the gray area to recover the Backup" &
                vbCrLf & "You will be asked for the encryption key to access your files."
            Backup.Label7.Text = "Keep your Cipher Key in the Clipboard. Avoid minimizing or changing the window"
            Backup.Label8.Text = "Save your encryption key very well"
            Backup.Button1.Text = "Create a BackUp"

            'About Form
            'About.Label2.Text = "Account Protector"
            'About.Label3.Text = "World: Pass Box helps you not to forget your accounts, protects your accounts with an advanced security system to avoid the intruders." &
            '    vbCrLf & vbCrLf & "Features:" &
            '    vbCrLf & "     -CryptoActions: SistemTrail module that will be responsible for encrypting and decrypting your accounts." &
            '    vbCrLf & "     -SignON: Register and Log in so that only you can see your accounts." &
            '    vbCrLf & "     -BackUp: Read and create backups for when you change your version or computer."
            'About.LinkLabel1.Text = "Support"
            'About.LinkLabel2.Text = "Use Guide"
            'About.Label6.Text = "*Community Standard: AppSupport was Natively Added for the Application"
            'About.CheckBox1.Text = "Offline Mode"

            'ActionConfirm Dialog
            ActionConfirm.Text = "Confirm"
            ActionConfirm.Label1.Text = "Confirm action"
            ActionConfirm.Label2.Text = "In order to continue you must enter the information that is requested, this is done for security and to confirm the action."
            ActionConfirm.Button1.Text = "Confirm"

            'Category Form
            Categories.Text = "Categories"
            Categories.btnOpen.Text = "Open"
            Categories.btnNew.Text = "New"
            Categories.btnRemove.Text = "Remove"

            'ServerSync
            ServerSync.lblTitle.Text = "Synchronizing Accounts"
            ServerSync.lblSelectedServer.Text = "Selected server"
            ServerSync.btnGuardar.Text = "Save"
            ServerSync.btnCargar.Text = "Load"
        End Sub
    End Class

    Public Class Español
        Shared Sub LANG_Español()
            'SignUp Form
            SignUp.Label1.Text = "Registrate"
            SignUp.Label2.Text = "Nombre de Usuario"
            SignUp.Label3.Text = "Contraseña"
            SignUp.Label4.Text = "Correo"
            SignUp.Label6.Text = "Mostrar"
            SignUp.Button1.Text = "Registrarse"

            'SignIn Form
            SignIn.Label1.Text = "Iniciar Sesion"
            SignIn.Label2.Text = "Nombre de Usuario"
            SignIn.Label3.Text = "Contraseña"
            SignIn.Label4.Text = "Mostrar"
            SignIn.Label5.Text = "¿Has olvidado tu Contraseña?"
            SignIn.Button1.Text = "Iniciar Sesion"

            'PassBox Form
            PassBox.Label1.Text = "Nombre del Servicio:"
            PassBox.Label2.Text = "Nombre de Usuario:"
            PassBox.Label3.Text = "Correo:"
            PassBox.Label4.Text = "Contraseña:"
            PassBox.Label5.Text = "Mostrar"
            PassBox.Label7.Text = "Copia de Seguridad >"
            PassBox.Label9.Text = "Notas: "
            PassBox.Label10.Text = "Aleatorio"
            PassBox.Label11.Text = "Mostrar"
            PassBox.Button6.Text = "Nueva Cuenta"
            PassBox.Button1.Text = "Guardar Cuenta"
            PassBox.Button2.Text = "Buscar"
            PassBox.Button3.Text = "Borrar Cuenta"
            PassBox.Button4.Text = "Categorías"
            PassBox.Button5.Text = "Configuracion"

            'Config Form
            Config.Text = "Wor: PassBox | Configuracion"
            Config.Label1.Text = "Configuracion"
            Config.TabPage3.Text = "Base de Datos"
            Config.TabPage4.Text = "Cuentas"
            Config.TabPage1.Text = "Llaves"
            Config.Label2.Text = "Base de Datos"
            Config.Label3.Text = "Nombre de Usuario:"
            Config.Label4.Text = "Correo:"
            Config.Label5.Text = "Contraseña:"
            Config.Label6.Text = "CryptoKey actual"
            Config.Label7.Text = "No Disponible o se Encuentra en Desarrollo"
            Config.Label9.Text = "Mostrar"
            Config.Label11.Text = "Llave de acceso"
            Config.GroupBox1.Text = "Tipo de llave"
            Config.RadioButton1.Text = "Iniciar PassBox"
            Config.RadioButton2.Text = "Iniciar PassBox e iniciar sesion"
            Config.Button3.Text = "Escribir una CryptoKey"
            Config.Button4.Text = "Generar una nueva CryptoKey"
            Config.Button1.Text = "Guardar Cambios"
            Config.Button5.Text = "Mostrar CryptoKey"
            Config.Button6.Text = "Crear"
            Config.DomainUpDown2.Text = "Default (Si)"

            'BackUp Form
            Backup.Text = "Wor: PassBox | Copia de Seguridad"
            Backup.TabPage1.Text = "Crear Backup"
            Backup.TabPage2.Text = "Leer Backup"
            Backup.Label1.Text = "Crear Copia de Seguridad"
            Backup.Label2.Text = "Crear una Copia de Seguridad hara que todas sus Cuentas se Copien a su Escritorio." &
                vbCrLf & "Quedaran protegidas bajo encriptado del modulo CryptoActions, nadie las podra leer."
            Backup.Label3.Text = "Crear una Copiad de Seguridad"
            Backup.Label4.Text = "Leer Copia de Seguridad"
            Backup.Label5.Text = "La Copia de Seguridad se creara en tu escritorio y estaran encriptados." &
                vbCrLf & "Para cargar la Copia de Seguridad de vuelta a PassBox, Dirigete a la pestaña 'Leer Backup'" &
                vbCrLf & "Tambien se copiara la llave criptografica a tu portapapeles, guardala, te servira cuando vayas a leer la copia de seguridad"
            Backup.Label6.Text = "Arraste y suelte la carpeta con el nombre 'PassBoxAccounts' en la zona gris para recuperar la Copia de Seguridad" &
                vbCrLf & "Se te pedira la llave de cifrado para poder acceder a tus archivos."
            Backup.Label7.Text = "Manten tu Llave de Cifrado en el Portapaleles. Evita minimizar o cambiar de ventana"
            Backup.Label8.Text = "Guarda muy bien tu Llave de Cifrado"
            Backup.Button1.Text = "Crear Copia de Seguridad"

            'About Form
            'About.Label2.Text = "Protector de Cuentas"
            'About.Label3.Text = "Wor: PassBox te ayudara a no olvidar tus cuentas, protege tus cuentas con un avanzado sistema de seguridad para evitar a los instrusos." &
            '    vbCrLf & vbCrLf & "Caracteristicas:" &
            '    vbCrLf & "     -CryptoActions: Modulo de SistemTrail que se encargara de encriptar y desencriptar tus cuentas." &
            '    vbCrLf & "     -SignON: Registrate e Inisia Sesion para que solo tu puedas ver tus cuentas." &
            '    vbCrLf & "     -BackUp: Lee y Crea copias de seguridad para cuando cambies de version o de ordenador."
            'About.LinkLabel1.Text = "Soporte"
            'About.LinkLabel2.Text = "Guia de Uso"
            'About.CheckBox1.Text = "Modo Offline"

            'ActionConfirm Dialog
            ActionConfirm.Text = "Confirmar"
            ActionConfirm.Label1.Text = "Confirmar acción"
            ActionConfirm.Label2.Text = "Para poder continuar debe ingresar la información que se le pide, esto se hace por seguridad y para confirmar la acción."
            ActionConfirm.Button1.Text = "Confirmar"

            'Category Form
            Categories.Text = "Categorías"
            Categories.btnOpen.Text = "Abrir"
            Categories.btnNew.Text = "Nueva"
            Categories.btnRemove.Text = "Remover"

            'ServerSync
            ServerSync.lblTitle.Text = "Sincronización de Cuentas"
            ServerSync.lblSelectedServer.Text = "Servidor seleccionado"
            ServerSync.btnGuardar.Text = "Guardar"
            ServerSync.btnCargar.Text = "Cargar"
        End Sub
    End Class

End Class