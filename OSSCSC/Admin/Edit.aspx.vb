Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business
Imports OSSCSC.Entity.Entity
Imports System.IO

Namespace Web
    Partial Class Edit
        Inherits MasterPage
        ' Methods
        Public Sub New()
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
        End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Me.SaveFile()
        End Sub

        Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
            Dim entity As DecisionEntity = Me.PopulateDecisionEntity
            If Me.Page.IsValid Then
                Dim decision As New Decision
                If Me.ValidateNCNFields(entity.Id) Then
                    If (entity.Id <> 0) Then
                        decision.Update(entity)
                    Else
                        Me.decisionId = decision.Add(entity)
                        Me.MoveUploadedFiles(Me.decisionId)
                    End If
                Else
                    Return
                End If
            End If
            Me.Response.Redirect(("edit.aspx?id=" & Me.decisionId.ToString))
        End Sub

        Private Function ClientSideScript() As String
            Return New SubCategory().GetScriptBlock
        End Function

        Private Sub ConfigurePage()
            Me.Page.RegisterClientScriptBlock("dropdowns", Me.ClientSideScript)
            Me.drpMainCategory.Attributes.Add("onchange", "populate(this, 'Form1', 'drpMainSubCategory', this.selectedIndex);")
            Me.drpSecondaryCategory.Attributes.Add("onchange", "populate(this, 'Form1', 'drpSecondarySubCategory', this.selectedIndex);")
        End Sub

        Private Function DirectoryPath(ByVal judgmentFolder As String) As String
            Dim str3 As String = Configuration.GetString("DCA.TribunalsService.Ossc.Web.DecisionUploadFolder")
            Return (Me.Server.MapPath("~") & str3 & judgmentFolder)
        End Function

        Private Sub FormatNCNFields()
            If (Me.decisionId > 0) Then
                Me.txtNCNCode1.Enabled = True
                Me.txtNCNCode2.Enabled = True
                Me.drpNCNCitation.Enabled = True
                Me.drpNCNYear.Enabled = True
            Else
                Me.txtNCNCode1.Enabled = False
                Me.txtNCNCode1.Text = "UKUT"
                Me.txtNCNCode2.Enabled = False
                Me.txtNCNCode2.Text = "AAC"
                Me.drpNCNCitation.Enabled = True
                Me.drpNCNYear.Enabled = True
            End If
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub ListJudgmentFiles()
            Dim str2 As String
            If (Me.decisionId <> 0) Then
                str2 = ("j" & Me.decisionId.ToString)
            ElseIf (Me.ViewState("tempDir") Is Nothing) Then
                Return
            Else
                str2 = Me.ViewState("tempDir").ToString
            End If
            Dim path As String = Me.DirectoryPath((str2 & "/"))
            If Directory.Exists(path) Then
                Dim files As FileInfo() = New DirectoryInfo(path).GetFiles
                Dim i As Integer
                For i = 0 To files.Length - 1
                    Dim child As New HyperLink With {
                        .ID = files(i).Name,
                        .NavigateUrl = ("~/judgmentfiles/" & str2 & "/" & files(i).Name),
                        .Target = "_blank",
                        .Text = files(i).Name
                    }
                    Dim link2 As HyperLink = child
                    link2.Text = (link2.Text & " <img border=""0"" src=""../images/ic" & Utility.GetExtension(files(i).Name) & ".gif""/>")
                    Me.phLinks.Controls.Add(child)
                    Me.phLinks.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
                    Me.phLinks.Controls.Add(New LiteralControl("<br/>"))
                Next i
            End If
        End Sub

        Private Sub MoveUploadedFiles(ByVal id As Integer)
            If (Me.ViewState("tempDir") Is Nothing) Then
                Me.SaveFile()
            Else
                Dim path As String = Me.DirectoryPath(Me.ViewState("tempDir").ToString)
                Dim str As String = Me.DirectoryPath(("j" & id.ToString))
                If (Directory.Exists(path) AndAlso Not Directory.Exists(str)) Then
                    Directory.Move(path, str)
                End If
            End If
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
            Dim child As New LinkButton With {
                .Text = "Remove Files",
                .ID = "Remove"
            }
            child.Attributes.Add("onclick", "return confirm('Are you sure you want to delete these files?\nThis action cannot be undone.');")
            AddHandler child.Click, New EventHandler(AddressOf Me.Remove_Click)
            Me.phLinks.Controls.Add(child)
            Me.phLinks.Controls.Add(New LiteralControl("<br/><br/>"))
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.decisionId = Utility.ParseInt(Me.Request.QueryString("Id"), 0)
            Me.ConfigurePage()

            If Me.IsPostBack Then
                Me.PopulateSubCategoryFromRequest(Me.drpMainCategory, Me.drpMainSubCategory)
                Me.PopulateSubCategoryFromRequest(Me.drpSecondaryCategory, Me.drpSecondarySubCategory)
            Else
                Me.PopulateDropDowns()

                If (Me.decisionId <> 0) Then
                    Me.PopulateForm()
                Else
                    Me.PopulalateNew()
                End If
                Me.ListJudgmentFiles()
                Me.FormatNCNFields()
            End If
            Me.lblError.Text = String.Empty
        End Sub

        Private Sub PopulalateNew()
            Me.lblID.Text = "New"
            Me.lblCreated.Text = Convert.ToString(DateTime.Now)
        End Sub

        Private Function PopulateDecisionEntity() As DecisionEntity
            Dim entity As New DecisionEntity With {
                .Id = Me.decisionId,
                .Is_published = Me.chkIsPublished.Checked,
                .File_no_1 = Me.txtPrefix.Text,
                .File_no_2 = Me.txtCaseNo.Text,
                .File_no_3 = Me.txtYear.Text,
                .Decision_datetime = Utility.IsDate(Me.txtDecisionDate.Text, DateTime.MinValue),
                .Claimants = Me.txtClaimant.Text,
                .Main_subcategory_id = Convert.ToInt32(Me.Request("drpMainSubCategory")),
                .Sec_subcategory_id = Convert.ToInt32(Me.Request("drpSecondarySubCategory")),
                .Headnote_summary = Me.txtNotes.Text,
                .Reported_no_1 = Me.txtReportedNumber1.Text,
                .Reported_no_2 = Me.txtReportedNumber2.Text,
                .Reported_no_3 = Me.txtReportedNumber3.Text,
                .Respondent = Me.txtRespondent.Text,
                .NCNYear = IntegerType.FromObject(Interaction.IIf((Me.drpNCNYear.SelectedIndex >= 0), Convert.ToInt32(Me.drpNCNYear.SelectedValue), -1)),
                .NCNCode1 = Me.txtNCNCode1.Text,
                .NCNCitation = IntegerType.FromObject(Interaction.IIf((Me.drpNCNCitation.SelectedIndex >= 0), Convert.ToInt32(Me.drpNCNCitation.SelectedValue), -1)),
                .NCNCode2 = Me.txtNCNCode2.Text
            }
            Dim list As New ArrayList
            Dim item As ListItem
            For Each item In Me.drpCommissioner.Items
                If item.Selected Then
                    list.Add(item.Value)
                End If
            Next
            entity.CommissionerIDList = list
            Return entity
        End Function

        Private Sub PopulateDropDowns()
            Utility.PopulateCategory(Me.drpMainCategory)
            Utility.PopulateCategory(Me.drpSecondaryCategory)
            Utility.PopulateCommissioners(Me.drpCommissioner, Me.decisionId)
            Me.PopulateNCNYearAndNCNCitation()
        End Sub

        Private Sub PopulateForm()
            Dim decision As DataSet = DirectCast(New Decision().GetDecision(Me.decisionId), DataSet)
            Dim row As DataRow = decision.Tables(0).Rows(0)
            Me.lblID.Text = StringType.FromObject(row("id"))
            If (DateTime.Compare(Utility.IsDate(StringType.FromObject(row("last_updatedtime")), DateTime.MinValue), DateTime.MinValue) <> 0) Then
                Me.lblUpdated.Text = Convert.ToDateTime(row("last_updatedtime")).ToShortDateString
            End If
            If (DateTime.Compare(Utility.IsDate(StringType.FromObject(row("created_datetime")), DateTime.MinValue), DateTime.MinValue) <> 0) Then
                Me.lblCreated.Text = Convert.ToDateTime(row("created_datetime")).ToShortDateString
            End If
            If (ObjectType.ObjTst(row("Is_Published"), 1, False) = 0) Then
                Me.chkIsPublished.Checked = True
                Me.chkIsPublished.Text = Convert.ToDateTime(row("publication_datetime")).ToShortDateString
            End If
            Me.txtYear.Text = Utility.TestDBNull(row("file_no_3"))
            Me.txtCaseNo.Text = Utility.TestDBNull(row("file_no_2"))
            Me.txtPrefix.Text = Utility.TestDBNull(row("file_no_1"))
            Me.txtDecisionDate.Text = StringType.FromObject(row("decision_datetime"))
            Me.txtClaimant.Text = StringType.FromObject(row("claimants"))
            Me.txtReportedNumber1.Text = Utility.TestDBNull(row("reported_no_1"))
            Me.txtReportedNumber2.Text = Utility.TestDBNull(row("reported_no_2"))
            Me.txtReportedNumber3.Text = Utility.TestDBNull(row("reported_no_3"))
            Me.txtNCNCode1.Text = Utility.TestDBNull(row("ncn_code1"))
            Me.txtNCNCode2.Text = Utility.TestDBNull(row("ncn_code2"))
            Me.txtRespondent.Text = Utility.TestDBNull(row("respondent"))
            If (Utility.TestNullForInt(row("ncn_year")) = -1) Then
                Me.drpNCNYear.SelectedIndex = -1
            Else
                Dim num4 As Integer = (Me.drpNCNYear.Items.Count - 1)
                Dim i As Integer = 0
                Do While (i <= num4)
                    If (ObjectType.ObjTst(Me.drpNCNYear.Items(i).Value, row("ncn_year"), False) = 0) Then
                        Me.drpNCNYear.SelectedIndex = i
                    End If
                    i += 1
                Loop
            End If
            If (Utility.TestNullForInt(row("ncn_citation")) = -1) Then
                Me.drpNCNCitation.SelectedIndex = -1
            Else
                Dim num3 As Integer = (Me.drpNCNCitation.Items.Count - 1)
                Dim i As Integer = 0
                Do While (i <= num3)
                    If (ObjectType.ObjTst(Me.drpNCNCitation.Items(i).Value, row("ncn_citation"), False) = 0) Then
                        Me.drpNCNCitation.SelectedIndex = i
                    End If
                    i += 1
                Loop
            End If
            Me.drpMainCategory.Items.FindByValue(StringType.FromObject(row("catid"))).Selected = True
            Utility.PopulateSubCategory(Me.drpMainSubCategory, IntegerType.FromObject(row("catid")))
            Me.drpMainSubCategory.Items.FindByValue(StringType.FromObject(row("subcatid"))).Selected = True
            If ((Not row("seccatid") Is DBNull.Value) AndAlso Not row("seccatid").Equals(String.Empty)) Then
                Me.drpSecondaryCategory.Items.FindByValue(StringType.FromObject(row("seccatid"))).Selected = True
                Utility.PopulateSubCategory(Me.drpSecondarySubCategory, IntegerType.FromObject(row("seccatid")))
                Me.drpSecondarySubCategory.Items.FindByValue(StringType.FromObject(row("secsubcatid"))).Selected = True
            End If
            Me.txtNotes.Text = StringType.FromObject(row("headnote_summary"))
            If (decision.Tables(1).Rows.Count <> 0) Then
                Dim enumerator As IEnumerator
                Try
                    enumerator = decision.Tables(1).Rows.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                        Me.drpCommissioner.Items.FindByValue(StringType.FromObject(current("commissionerid"))).Selected = True
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        DirectCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
            End If
        End Sub

        Private Sub PopulateNCNYearAndNCNCitation()
            Me.drpNCNYear.Items.Add(New ListItem("Select a Year", "-1"))
            Dim num As Integer = &H7D8
            Do While True
                Me.drpNCNYear.Items.Add(New ListItem(num.ToString, num.ToString))
                num += 1
                If (num > &H802) Then
                    Me.drpNCNCitation.Items.Add(New ListItem("Select a Citation No", "-1"))
                    Dim num2 As Integer = 1
                    Do While True
                        Me.drpNCNCitation.Items.Add(New ListItem(num2.ToString, num2.ToString))
                        num2 += 1
                        If (num2 > &H3E7) Then
                            Return
                        End If
                    Loop
                End If
            Loop
        End Sub

        Private Sub PopulateSubCategoryFromRequest(ByVal category As DropDownList, ByVal subcategory As DropDownList)
            If (DoubleType.FromString(category.SelectedItem.Value) <> -1) Then
                Utility.PopulateSubCategory(subcategory, IntegerType.FromString(category.SelectedItem.Value))
                If Not Me.Request(subcategory.ID.ToString).Equals(String.Empty) Then
                    subcategory.Items.FindByValue(Me.Request(subcategory.ID.ToString).ToString).Selected = True
                End If
            End If
        End Sub

        Protected Overrides Sub PopulateTitle()
            Me.Controls.Add(New LiteralControl("<title>OSSCSC - Admin </title>" & ChrW(10)))
        End Sub

        Public Sub Remove_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim path As String = Me.DirectoryPath((If((Me.decisionId <> 0), ("j" & Me.decisionId.ToString), Me.ViewState("tempDir").ToString) & "/"))
            If Directory.Exists(path) Then
                Directory.Delete(path, True)
            End If
            Me.ListJudgmentFiles()
        End Sub

        Private Sub SaveFile()
            If (Me.fileDecision.PostedFile.ContentLength > 0) Then
                Dim str3 As String
                If (Me.decisionId <> 0) Then
                    str3 = ("j" & Me.decisionId.ToString)
                Else
                    str3 = If((Not Me.ViewState("tempDir") Is Nothing), StringType.FromObject(Me.ViewState("tempDir")), Guid.NewGuid.ToString)
                    Me.ViewState("tempDir") = str3
                End If
                Dim path As String = Me.DirectoryPath((str3 & "/"))
                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                End If
                Dim fileName As String = IO.Path.GetFileName(Me.fileDecision.PostedFile.FileName)
                Try
                    Me.fileDecision.PostedFile.SaveAs((path & fileName))
                Catch exception1 As Exception
                    Dim local1 As Exception = exception1
                    ProjectData.SetProjectError(local1)
                    Dim exception As Exception = local1
                    ProjectData.ClearProjectError()
                End Try
                Me.ListJudgmentFiles()
            End If
        End Sub

        'Private Function ValidateNCNFields(ByVal decisionID As Integer) As Boolean
        '    If (decisionID <= 0) Then
        '        If (Me.drpNCNYear.SelectedIndex = 0) Then
        '            Me.lblError.Text = " Year is required;"
        '        End If
        '        If (Me.drpNCNCitation.SelectedIndex = 0) Then
        '            Me.lblError.Text = (Me.lblError.Text & " Citation No is required")
        '        End If
        '        Me.lblError.Text = Me.lblError.Text.TrimEnd(";".ToCharArray)
        '        If (Me.lblError.Text.Length > 0) Then
        '            Return False
        '        End If
        '    ElseIf Not (((Not Me.txtNCNCode1.Enabled And Not Me.txtNCNCode2.Enabled) And Me.drpNCNCitation.Enabled) And Me.drpNCNYear.Enabled) Then
        '        Return True
        '    Else
        '        If (Me.drpNCNYear.SelectedIndex = 0) Then
        '            Me.lblError.Text = " Year required;"
        '        End If
        '        If (Me.drpNCNCitation.SelectedIndex = 0) Then
        '            Me.lblError.Text = (Me.lblError.Text & " Citation No required")
        '        End If
        '        Me.lblError.Text = Me.lblError.Text.TrimEnd(";".ToCharArray)
        '        If (Me.lblError.Text.Length > 0) Then
        '            Return False
        '        End If
        '    End If
        '    Return True
        'End Function

        Private Function ValidateNCNFields(ByVal decisionID As Integer) As Boolean
            If (drpNCNCitation.SelectedIndex = 0 And drpNCNYear.SelectedIndex = 0) Or (drpNCNCitation.SelectedIndex <> 0 And drpNCNYear.SelectedIndex <> 0) Then
                Return True
            ElseIf (drpNCNCitation.SelectedIndex = 0 And drpNCNYear.SelectedIndex <> 0) Or (drpNCNCitation.SelectedIndex <> 0 And drpNCNYear.SelectedIndex = 0) Then
                If (Me.drpNCNYear.SelectedIndex = 0) Then
                    Me.lblError.Text = " Year is required;"
                End If
                If (Me.drpNCNCitation.SelectedIndex = 0) Then
                    Me.lblError.Text = (Me.lblError.Text & " Citation No is required")
                End If
                Me.lblError.Text = Me.lblError.Text.TrimEnd(";".ToCharArray)
                If (Me.lblError.Text.Length > 0) Then
                    Return False
                End If
            ElseIf Not (((Not Me.txtNCNCode1.Enabled And Not Me.txtNCNCode2.Enabled) And Me.drpNCNCitation.Enabled) And Me.drpNCNYear.Enabled) Then
                Return True
            End If
            Return True
        End Function

        Private decisionId As Integer
        Private Const NCN_CODE1 As String = "UKUT"
        Private Const NCN_CODE2 As String = "AAC"
    End Class
End Namespace
