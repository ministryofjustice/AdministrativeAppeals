Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business
Imports OSSCSC.Entity.Entity
Imports System.Drawing

Namespace Web
    Partial Class _DefaultAdmin
        Inherits MasterPage
        ' Methods
        Public Sub New()
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
            AddHandler MyBase.PreRender, New EventHandler(AddressOf Me.Page_PreRender)
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
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
                If (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "is_published")) <> 0) Then
                    e.Item.Cells(4).Text = "Y"
                Else
                    e.Item.Cells(4).Text = "N"
                    e.Item.Cells(4).ForeColor = Color.Red
                End If
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
                If (StringType.StrCmp(Me.Request.UrlReferrer.AbsolutePath.ToString, Me.Request.Path.ToString, False) <> 0) Then
                    Me.PagerControl.PageIndex = IntegerType.FromString(Me.Request.QueryString("PagerControl_PageIndex"))
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

            End If
            Me.drpCategory.Attributes.Add("onchange", "populate(this, 'Form1', 'drpSubcategory', this.selectedIndex);")
            'Me.btnSearch.Attributes.Add("onclick", "document.forms[0].action = ""default.aspx""")
        End Sub

        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs)
            Me.PopulateGrid()
        End Sub

        Private Function PopulateGrid() As Object
            Dim obj2 As Object
            Dim criteria As SearchCriteria = Me.PopulateSearchCriteria
            If (StringType.StrCmp(Me.DecisionGrid.SortDirection, Nothing, False) = 0) Then
                Me.DecisionGrid.SortColumn = "decision_datetime"
                Me.DecisionGrid.SortDirection = "DESC"
            End If
            Dim ds As DataSet = New Decision().SearchPaged(criteria, Me.PagerControl.PageIndex, Me.PagerControl.PageSize, Me.PagerControl.ResultCount)
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
                .DecisionDate = Utility.IsDate(Me.txtDecisionDate.Text, DateTime.MinValue),
                .FromDate = Utility.IsDate(Me.txtFromDate.Text, DateTime.MinValue),
                .ToDate = Utility.IsDate(Me.txtToDate.Text, DateTime.MinValue),
                .Claimant = Me.txtClaimant.Text.Trim,
                .Prefix = Me.txtPrefix.Text.Trim,
                .Year = Me.txtYear.Text.Trim,
                .CaseNo = Me.txtCase.Text.Trim,
                .CommissionerID = IntegerType.FromString(Me.drpCommissioner.SelectedItem.Value),
                .Reported1 = Me.txtReported1.Text.Trim,
                .Reported2 = Me.txtReported2.Text.Trim,
                .Reported3 = Me.txtReported3.Text.Trim,
                .Notes = String.Empty,
                .NCNCitaion = IntegerType.FromString(Me.drpNCNCitation.SelectedItem.Value),
                .NCNYear = IntegerType.FromString(Me.drpNCNYear.SelectedItem.Value)
            }
        End Function

        Protected Overrides Sub PopulateTitle()
            Me.Controls.Add(New LiteralControl("<title>OSSCSC - Admin </title>" & ChrW(10)))
        End Sub


    End Class
End Namespace
