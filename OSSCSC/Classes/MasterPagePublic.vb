Imports DCA.TribunalsService.Ossc.Business
Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace Web
    Public Class MasterPagePublic
        Inherits Page
        ' Methods
        Public Sub New()
            AddHandler Init, New EventHandler(AddressOf Me.InitialisePage)
        End Sub

        Protected Overrides Sub AddParsedSubObject(ByVal obj As Object)
            Me.ContentPlaceHolder.Controls.Add(DirectCast(obj, Control))
        End Sub

        Protected Sub BuildFramework()
            Me.Response.Expires = -1
            Me.Response.AddHeader("pragma", "no-cache")
            Me.Response.CacheControl = "Private"
            Me.Controls.Add(New LiteralControl("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">" & ChrW(10)))
            Me.Controls.Add(New LiteralControl("<head>" & ChrW(10)))
            Me.Controls.Add(New LiteralControl("<meta name=""Keywords"" content=""tribunals, sir andrew leggatt, transforming public services, administrative justice, independence, panel members, judiciary, appeals, benefits, hearing centre, dispute resolution, executive agency, mental health, disability, criminal injuries, employment disputes, immigration, land, party -v- party, citizen -v- state, tribunals service, uk, government, department for constitutional affairs, dca"" />" & ChrW(10)))
            Me.Controls.Add(New LiteralControl("<meta name=""Description"" content=""The Tribunals Service is a new government agency that will be launched in April 2006 to provide common administrative support to the main central government tribunals. The agency will form part of the Department for Constitutional Affairs (DCA). Launch of the new Service will be the biggest change to the tribunals system in this country in almost half a century."" />" & ChrW(10)))
            Me.Controls.Add(New LiteralControl("<meta name=""author"" content=""Tribunals Group Web Team"" />" & ChrW(10)))
            Me.Controls.Add(New LiteralControl("<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-1"" />" & ChrW(10)))
            Me.PopulateTitle()
            Me.Controls.Add(New LiteralControl(("<link href=""/NEWstyles.css"" rel=""stylesheet"" media=""screen"" type=""text/css"" />" & ChrW(10))))
            Me.Controls.Add(New LiteralControl("</head>" & ChrW(10)))
            Me.Controls.Add(New LiteralControl("<body>" & ChrW(10)))
            If Me.DisplayHeader Then
                Me.Controls.Add(Me.HeaderPlaceHolder)
            End If
            Me.Controls.Add(Me.ContentPlaceHolder)
            Me.Controls.Add(New LiteralControl("<div class=""clear""></div>" & ChrW(10)))
            Me.Controls.Add(New LiteralControl("</div>" & ChrW(10)))
            If Me.DisplayFooter Then
                Me.Controls.Add(Me.FooterPlaceHolder)
            End If
            Me.Controls.Add(New LiteralControl("</body>" & ChrW(10)))
            Me.Controls.Add(New LiteralControl("</html>" & ChrW(10)))
        End Sub

        Protected Overridable Sub InitialisePage(ByVal sender As Object, ByVal e As EventArgs)
            Me.BuildFramework()
        End Sub

        Protected Overridable Sub PopulateTitle()
            Me.Controls.Add(New LiteralControl("<title>Social Security & Child Support Commissioners</title>" & ChrW(10)))
        End Sub

        Public Sub SetFocus(ByVal control As Control)
            PageUtility.SetFocus(control)
        End Sub

        Private Function StylesheetHref(ByVal stylesheet As String) As String
            Return (ConfigurationSettings.AppSettings("DCA.TribunalsService.Ossc.Web.Stylesheet.Href") & stylesheet)
        End Function


        ' Fields
        Private HeaderPlaceHolder As PlaceHolder = New PlaceHolder
        Private ContentPlaceHolder As PlaceHolder = New PlaceHolder
        Private FooterPlaceHolder As PlaceHolder = New PlaceHolder
        Protected DisplayHeader As Boolean = True
        Protected DisplayFooter As Boolean = True
    End Class
End Namespace
