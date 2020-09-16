Imports Microsoft.VisualBasic.CompilerServices
Imports System.ComponentModel

Namespace DCA.TribunalsService.Ossc.Web
    <ValidationProperty("Text"), ParseChildren(False), DefaultEvent("TextChanged")>
    Public Class HtmlDateControl
        Inherits TextBox
        ' Methods
        Private Function CreateClientScriptBlock() As String
            Return "<script type=""text/javascript"" src=""../javascript/HtmlDateControl.js""></script>"
        End Function

        Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
            MyBase.OnPreRender(e)
            Me.Page.RegisterClientScriptBlock("__HtmlDateControl", Me.CreateClientScriptBlock)
        End Sub

        Protected Overrides Sub Render(ByVal textWriter As HtmlTextWriter)
            MyBase.Render(textWriter)
            textWriter.Write(" ")
            If (StringType.StrCmp(HttpContext.Current.Request.Browser.Browser.ToLower, "ie", False) <> 0) Then
                textWriter.Write("(dd/mm/yyyy)")
            Else
                textWriter.Write("<img style=""cursor: hand;"" ")
                textWriter.Write((" onclick=""popUpCalendar(this, document.forms[0]." & Me.UniqueID.ToString & ", 'dd/mm/yyyy', -83, 6)"" "))
                textWriter.Write(" height=""17"" width=""17"" src=""../images/HtmlDateControl/cal.gif"" alt=""Calendar""/>")
            End If
        End Sub

    End Class
End Namespace
