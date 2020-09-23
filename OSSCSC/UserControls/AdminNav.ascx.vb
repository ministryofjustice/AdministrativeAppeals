Imports System
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace Web
    Partial Class AdminNav
        Inherits UserControl
        ' Methods
        Public Sub New()
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub lnkSignOut_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSignOut.Click
            FormsAuthentication.SignOut()
            Me.Response.Redirect("Default.aspx")
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        End Sub


    End Class
End Namespace
