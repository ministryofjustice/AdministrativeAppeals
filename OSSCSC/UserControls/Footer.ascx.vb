Imports System
Imports System.Diagnostics
Imports System.Web.UI

Namespace DCA.TribunalsService.Ossc.Web
    Public Class Footer
        Inherits UserControl
        ' Methods
        Public Sub New()
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

    End Class
End Namespace
