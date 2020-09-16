Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace DCA.TribunalsService.Ossc.Web
    <ParseChildren(False), ValidationProperty("Text"), DefaultProperty("Text")>
    Public Class LabelForControl
        Inherits WebControl
        ' Methods
        Protected Overrides Sub Render(ByVal output As HtmlTextWriter)
            Dim control As Control = Me.NamingContainer.FindControl(Me._control)
            output.Write("<label for=""")
            If (StringType.StrCmp(Me.NamingContainer.ID, "", False) <> 0) Then
                output.Write(Me.NamingContainer.ID)
                output.Write("_")
            End If
            If (Not control Is Nothing) Then
                output.Write(control.ID)
            End If
            output.Write(""">")
            output.Write(Me._text)
            output.Write("</label>")
        End Sub


        ' Properties
        <DefaultValue(""), Category("Appearance"), Bindable(True)>
        Public Property [Text] As String
            Get
                Return Me._text
            End Get
            Set(ByVal Value As String)
                Me._text = Value
            End Set
        End Property

        Public Property Control As String
            Get
                Return Me._control
            End Get
            Set(ByVal Value As String)
                Me._control = Value
            End Set
        End Property


        ' Fields
        Private _text As String
        Private _control As String
    End Class
End Namespace
