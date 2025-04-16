Imports DCA.TribunalsService.Ossc.Business
Imports System
Imports System.Diagnostics
Imports System.Web.UI

Namespace Web
    Public Class Header
        Inherits UserControl
        ' Methods
        Public Sub New()
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
            Me.baseURL = ConfigurationSettings.AppSettings("DCA.TribunalsService.Ossc.Web.BaseURL")
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        End Sub


        ' Fields
        Private designerPlaceholderDeclaration As Object
        Public baseURL As String
    End Class
End Namespace
