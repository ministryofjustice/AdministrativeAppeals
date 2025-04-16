Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business
Imports OSSCSC.Entity.Entity
Imports NLog

Namespace Web
    Public Class Search
        Inherits MasterPagePublic
        Private Shared ReadOnly logger As Logger = LogManager.GetCurrentClassLogger()
        

        ' Methods
        Public Sub New()
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
            AddHandler MyBase.PreRender, New EventHandler(AddressOf Me.Page_PreRender)
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.PagerControl.PageIndex = 1
        End Sub

        Private Function ClientSideScript() As String
            Return New SubCategory().GetScriptBlock
        End Function

        Private Sub DecisionGrid_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles DecisionGrid.ItemDataBound
            If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
                e.Item.Attributes.Add("onclick", StringType.FromObject(ObjectType.StrCatObj(ObjectType.StrCatObj("RowOnClick('", DataBinder.Eval(e.Item.DataItem, "judgmentid")), "');")))
                e.Item.Cells(0).Text = DateTime.Parse(StringType.FromObject(DataBinder.Eval(e.Item.DataItem, "decision_datetime"))).ToShortDateString
                Dim cell As TableCell = e.Item.Cells(1)
                cell.Text = StringType.FromObject(ObjectType.AddObj(cell.Text, ObjectType.StrCatObj(ObjectType.StrCatObj(ObjectType.StrCatObj(" ", DataBinder.Eval(e.Item.DataItem, "casenumber")), " "), DataBinder.Eval(e.Item.DataItem, "year"))))
                cell = e.Item.Cells(2)
                cell.Text = StringType.FromObject(ObjectType.AddObj(cell.Text, ObjectType.StrCatObj(ObjectType.StrCatObj(ObjectType.StrCatObj(ObjectType.StrCatObj(ObjectType.StrCatObj(ObjectType.StrCatObj(" ", DataBinder.Eval(e.Item.DataItem, "ncncode1")), " "), DataBinder.Eval(e.Item.DataItem, "ncncitation")), " "), DataBinder.Eval(e.Item.DataItem, "ncncode2")), "")))
            End If
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.PagerControl.PageSize = Configuration.GetInt("DCA.TribunalsService.Ossc.Web.Search.Grid.PageSize")
            Me.Page.RegisterClientScriptBlock("dropdowns", Me.ClientSideScript)
            Utility.PopulateCommissioners(Me.drpCommissioner, 0)
            Me.PopulateNCNYearAndNCNCitation()

            If Not Me.IsPostBack Then
                Utility.PopulateCategory(Me.drpCategory)
                Me.PagerControl.PageIndex = 1
            End If
            If Me.IsPostBack Then
                If (Me.Request.UrlReferrer.AbsolutePath.IndexOf("view.aspx") <> -1) Then
                    Utility.PopulateCategory(Me.drpCategory)
                    Me.PagerControl.PageIndex = IntegerType.FromString(Me.Request.QueryString("PagerControl_PageIndex"))
                    If (StringType.StrCmp(Me.Request.QueryString("drpCategory"), "", False) <> 0) Then
                        Me.drpCategory.Items.FindByValue(Me.Request.QueryString("drpCategory")).Selected = True
                    End If
                End If
                If (DoubleType.FromString(Me.drpCategory.SelectedItem.Value) <> -1) Then
                    Utility.PopulateSubCategory(Me.drpSubcategory, IntegerType.FromString(Me.drpCategory.SelectedItem.Value))
                    If Not Me.Request("drpSubcategory").Equals(String.Empty) Then
                        Me.drpSubcategory.Items.FindByValue(Me.Request("drpSubcategory").ToString).Selected = True
                    End If
                End If
                If Not Me.Request("drpCommissioner").Equals(String.Empty) Then
                    Me.drpCommissioner.Items.FindByValue(Me.Request("drpCommissioner").ToString).Selected = True
                End If
                If Not Me.Request("drpNCNYear").Equals(String.Empty) Then
                    Me.drpNCNYear.Items.FindByValue(Me.Request("drpNCNYear").ToString).Selected = True
                End If
                If Not Me.Request("drpNCNCitation").Equals(String.Empty) Then
                    Me.drpNCNCitation.Items.FindByValue(Me.Request("drpNCNCitation").ToString).Selected = True
                End If
            End If
            Me.drpCategory.Attributes.Add("onchange", "populate(this, 'Form1', 'drpSubcategory', this.selectedIndex);")
            Me.btnSearch.Attributes.Add("onclick", "document.forms[0].action = ""default.aspx""")
        End Sub

        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs)
            Me.PopulateGrid()
        End Sub

        Private Function PopulateGrid() As Object
                logger.Info("Executing search with criteria: CategoryID={0}, SubCategoryID={1}",
                           criteria.CategoryID, criteria.SubCategoryID)
            Dim obj2 As Object
            Dim criteria As SearchCriteria = Me.PopulateSearchCriteria
            If (StringType.StrCmp(Me.DecisionGrid.SortDirection, Nothing, False) = 0) Then
                Me.DecisionGrid.SortColumn = "decision_datetime"
                Me.DecisionGrid.SortDirection = "DESC"
            End If
            Dim ds As DataSet = New Decision().SearchPaged(criteria, PagerControl.PageIndex, PagerControl.PageSize, Me.PagerControl.ResultCount)
            Me.DecisionGrid.DataSource = ds
            Me.DecisionGrid.DataBind()
            Return obj2
        End Function

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

        Private Function PopulateSearchCriteria() As SearchCriteria
            Return New SearchCriteria With {
                .SortColumn = Me.DecisionGrid.SortColumn,
                .SortDirection = Me.DecisionGrid.SortDirection,
                .CategoryID = IntegerType.FromString(Me.drpCategory.SelectedItem.Value),
                .SubCategoryID = IntegerType.FromString(Me.drpSubcategory.SelectedItem.Value),
                .FromDate = If((DateTime.Compare(Utility.IsDate(Me.txtFromDate.Text, DateTime.MinValue), DateTime.MinValue) = 0), DateTime.MinValue, DateTime.Parse((Me.txtFromDate.Text & " 00:00:00"))),
                .ToDate = If((DateTime.Compare(Utility.IsDate(Me.txtToDate.Text, DateTime.MinValue), DateTime.MinValue) = 0), DateTime.MinValue, DateTime.Parse((Me.txtToDate.Text & " 23:59:59"))),
                .FromDateAdded = If((DateTime.Compare(Utility.IsDate(Me.txtFromDateAdded.Text, DateTime.MinValue), DateTime.MinValue) = 0), DateTime.MinValue, DateTime.Parse((Me.txtFromDateAdded.Text & " 00:00:00"))),
                .ToDateAdded = If((DateTime.Compare(Utility.IsDate(Me.txtToDateAdded.Text, DateTime.MinValue), DateTime.MinValue) = 0), DateTime.MinValue, DateTime.Parse((Me.txtToDateAdded.Text & " 23:59:59"))),
                .Claimant = Me.txtClaimant.Text.Trim,
                .Prefix = Me.txtPrefix.Text.Trim,
                .Year = Me.txtYear.Text.Trim,
                .CaseNo = Me.txtCase.Text.Trim,
                .CommissionerID = IntegerType.FromString(Me.drpCommissioner.SelectedItem.Value),
                .Reported1 = Me.txtReported1.Text.Trim,
                .Reported2 = Me.txtReported2.Text.Trim,
                .Reported3 = Me.txtReported3.Text.Trim,
                .IsPublished = True,
                .Notes = Me.txtNotes.Text.Trim,
                .Respondent = Me.txtRespondent.Text.Trim,
                .NCNCitaion = IntegerType.FromString(Me.drpNCNCitation.SelectedItem.Value),
                .NCNYear = IntegerType.FromString(Me.drpNCNYear.SelectedItem.Value)
            }
        End Function
    End Class
End Namespace
