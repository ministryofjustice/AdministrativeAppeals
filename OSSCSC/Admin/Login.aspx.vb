Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business

Namespace Web
    Partial Class Login
        Inherits MasterPage
        ' Methods
        Public Sub New()
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
        End Sub

        Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
            If New Security().AuthenticateUser(Me.txtUser.Text, CryptoUtil.EncryptTripleDES(Me.txtPassword.Text)) Then
                FormsAuthentication.RedirectFromLoginPage(Me.txtUser.Text, False)
            Else
                Me.lblError.Text = "! The Email Address and/or Password you supplied are incorrect."
            End If
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (StringType.StrCmp(Me.Request.QueryString.ToString, "", False) = 0) Then
                Me.Response.Redirect(("~/Admin/Login.aspx?ReturnUrl=" & Me.Server.UrlEncode("Default.aspx")))
            End If
        End Sub


    End Class
End Namespace
