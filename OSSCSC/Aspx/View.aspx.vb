Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business
Imports System.IO

Namespace Web
    Partial Class View
        Inherits MasterPagePublic
        ' Methods
        Public Sub New()
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
        End Sub

        Private Function DirectoryPath(ByVal judgmentFolder As String) As String
            Dim str3 As String = ConfigurationSettings.AppSettings("DCA.TribunalsService.Ossc.Web.DecisionUploadFolder")
            Return (Me.Server.MapPath("~") & str3 & judgmentFolder)
        End Function

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub debug(ByVal msg as String)
            Dim strFile As String = "C:\\Windows\\Temp\\mgb.log"
            Dim fileExists As Boolean = File.Exists(strFile)
            Using sw As New StreamWriter(File.Open(strFile, FileMode.OpenOrCreate))
                sw.WriteLine(msg)
            End Using
        End Sub

        Private Sub ListJudgmentFiles()
            Dim str2 As String = ("j" & Me.decisionId.ToString)
            Dim path As String = Me.DirectoryPath((str2 & "/"))
            debug(path)
            If Directory.Exists(path) Then
                Dim files As FileInfo() = New DirectoryInfo(path).GetFiles
                Dim i As Integer
                For i = 0 To files.Length - 1
                    Dim child As New HyperLink With {
                        .ID = ("file" & i.ToString),
                        .NavigateUrl = ("~/judgmentfiles/" & str2 & "/" & files(i).Name),
                        .Target = "_blank",
                        .Text = files(i).Name
                    }
                    Dim link2 As HyperLink = child
                    Dim strArray As String() = New String() {link2.Text, " <img alt=""", files(i).Name, """  border=""0"" src=""../images/ic", Utility.GetExtension(files(i).Name), ".gif""/>"}
                    link2.Text = String.Concat(strArray)
                    Me.phLinks.Controls.Add(child)
                    Me.phLinks.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
                    Me.phLinks.Controls.Add(New LiteralControl("<br/>"))
                Next i
            End If
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Me.decisionId = IntegerType.FromString(Me.Request("id"))
            Catch exception1 As Exception
                Dim local1 As Exception = exception1
                ProjectData.SetProjectError(local1)
                Dim exception As Exception = local1
                Me.decisionId = 0
                ProjectData.ClearProjectError()
            End Try
            Me.PopulateForm()
            Me.hrefResults.NavigateUrl = ("~/aspx/default.aspx?" & Me.Request.Form.ToString)
        End Sub

        Private Sub PopulateForm()
            Dim decision As DataSet = DirectCast(New Decision().GetDecision(Me.decisionId, True), DataSet)
            If (decision.Tables(0).Rows.Count = 0) Then
                Throw New Exception("DataTable contains no rows")
            End If
            Dim row As DataRow = decision.Tables(0).Rows(0)
            Me.litReportedNumber.Text = Utility.TestDBNull(row("reported_no"))
            If (Utility.TestDBNull(Me.litReportedNumber.Text).Equals(String.Empty) AndAlso ((Not Utility.TestDBNull(row("reported_no_1")).Equals(String.Empty) And Not Utility.TestDBNull(row("reported_no_2")).Equals(String.Empty)) And Not Utility.TestDBNull(row("reported_no_3")).Equals(String.Empty))) Then
                Me.litReportedNumber.Text = StringType.FromObject(ObjectType.StrCatObj(ObjectType.StrCatObj(ObjectType.StrCatObj(ObjectType.StrCatObj(ObjectType.StrCatObj("R(", row("reported_no_1")), ")"), row("reported_no_2")), "/"), row("reported_no_3")))
            End If
            Me.litFileNo.Text = Utility.TestDBNull(row("filenumber"))
            Me.litClaimant.Text = Utility.TestDBNull(row("claimants"))
            Me.litRespondent.Text = Utility.TestDBNull(row("respondent"))
            Me.litDate.Text = Convert.ToDateTime(row("decision_datetime")).ToShortDateString
            Me.litDateAdded.Text = Convert.ToDateTime(row("created_datetime")).ToShortDateString
            Me.litCategory.Text = StringType.FromObject(row("category"))
            Me.litSubcategory.Text = StringType.FromObject(row("subcategory"))
            Me.litSecondaryCategory.Text = Utility.TestDBNull(row("seccategory"))
            Me.litSecondarySubcategory.Text = Utility.TestDBNull(row("secsubcategory"))
            Me.litNCNNumber.Text = Utility.TestDBNull(row("ncn_number"))
            If BooleanType.FromObject(ObjectType.BitOrObj((row("headnote_summary") Is DBNull.Value), (ObjectType.ObjTst(row("headnote_summary"), String.Empty, False) = 0))) Then
                Me.litNotes.Text = "Summary not available"
            Else
                Me.litNotes.Text = StringType.FromObject(row("headnote_summary"))
                If (Not Me.Request("txtNotes") Is Nothing) Then
                    Me.litNotes.Text = Utility.Highlight(Me.Request("txtNotes"), Me.litNotes.Text)
                End If
            End If
            If (decision.Tables(1).Rows.Count <> 0) Then
                Dim enumerator As IEnumerator
                Try
                    enumerator = decision.Tables(1).Rows.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                        Dim litCommissioners As Literal = Me.litCommissioners
                        litCommissioners.Text = StringType.FromObject(ObjectType.AddObj(litCommissioners.Text, ObjectType.StrCatObj(current("commissioner"), "<br />")))
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        DirectCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
            End If
            Me.ListJudgmentFiles()
        End Sub



        Private decisionId As Integer
    End Class
End Namespace
