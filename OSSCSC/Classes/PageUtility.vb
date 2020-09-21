Namespace Web
    Public Class PageUtility
        ' Methods
        Public Shared Sub SetFocus(ByVal ctrl As Control)
            Dim builder As New StringBuilder("")
            Dim parent As Control = ctrl.Parent
            Do While Not TypeOf parent Is HtmlForm
                parent = parent.Parent
            Loop
            Dim builder2 As StringBuilder = builder
            builder2.Append("<script language='JavaScript'>")
            builder2.Append("function SetFocus()")
            builder2.Append("{")
            builder2.Append("document.")
            builder2.Append(parent.ID)
            builder2.Append("['")
            builder2.Append(ctrl.UniqueID)
            builder2.Append("'].focus();")
            builder2.Append("}")
            builder2.Append("window.onload = SetFocus;")
            builder2.Append("")
            builder2.Append("</script")
            builder2.Append(">")
            builder2 = Nothing
            ctrl.Page.RegisterClientScriptBlock("SetFocus", builder.ToString)
        End Sub

    End Class
End Namespace
