Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business

Namespace Web
    Partial Class EditCategory
        Inherits MasterPage
        ' Methods
        Public Sub New()
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
            AddHandler MyBase.PreRender, New EventHandler(AddressOf Me.Page_PreRender)
        End Sub

        Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNew.Click
            Me.Page.Validate()

            If (Me.Page.IsValid And Not Object.ReferenceEquals(Me.ds, Nothing)) Then
                Dim row As DataRow = Me.ds.Tables(0).NewRow
                row("num") = 0
                Me.ds.Tables(0).Rows.Add(row)
                Me.CatDataGrid.EditItemIndex = (Me.ds.Tables(0).Rows.Count - 1)
            End If
        End Sub

        Private Sub CatDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles CatDataGrid.ItemDataBound
            If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
                e.Item.Attributes.Add("onclick", StringType.FromObject(ObjectType.StrCatObj(ObjectType.StrCatObj("RowOnClick('", DataBinder.Eval(e.Item.DataItem, "num")), "');")))
            End If
        End Sub

        Private Sub Delete(ByVal Id As Integer)
            If (New SubCategory().CountByCategory(Id) > 0) Then
                Me.MessageLabel.Text = "Cannot delete Category as one or more Subcategories are currently assigned to it."
            Else
                Dim category2 As New Category
                category2.Delete(Id)
                Me.ds = category2.GetCategoryList
                Me.CatDataGrid.EditItemIndex = -1
            End If
        End Sub

        Private Sub GetCategory()
            Me.ds = New Category().GetCategoryList(False)
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.GetCategory()
        End Sub

        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs)
            Me.CatDataGrid.DataSource = Me.ds
            Me.CatDataGrid.DataBind()

            If (Me.CatDataGrid.EditItemIndex >= 0) Then
                Dim control As Control = Me.CatDataGrid.Items(Me.CatDataGrid.EditItemIndex).FindControl("DescriptionTextBox")
                If (Not control Is Nothing) Then
                    Me.SetFocus(control)
                End If
            End If
        End Sub

        Protected Overrides Sub PopulateTitle()
            Me.Controls.Add(New LiteralControl("<title>OSSCSC - Admin </title>" & ChrW(10)))
        End Sub

        Private Sub Save(ByVal id As Integer, ByVal description As String)
            Dim category As New Category
            If (id = 0) Then
                category.Add(description)
            Else
                category.Update(id, description)
            End If
            Me.ds = category.GetCategoryList
            Me.CatDataGrid.EditItemIndex = -1
        End Sub

        Private Sub SubCatDataGrid_CancelCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles CatDataGrid.CancelCommand
            Me.CatDataGrid.EditItemIndex = -1
        End Sub

        Private Sub SubCatDataGrid_DeleteCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles CatDataGrid.DeleteCommand
            Dim label As Label = DirectCast(e.Item.FindControl("CatIDLabel"), Label)
            If (Not label Is Nothing) Then
                Me.Delete(IntegerType.FromString(label.Text))
            End If
        End Sub

        Private Sub SubCatDataGrid_EditCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles CatDataGrid.EditCommand
            Me.Page.Validate()

            If Me.Page.IsValid Then
                Me.CatDataGrid.EditItemIndex = e.Item.ItemIndex
            End If
        End Sub

        Private Sub SubCatDataGrid_UpdateCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles CatDataGrid.UpdateCommand
            Me.Page.Validate()

            If Me.Page.IsValid Then
                Dim objA As Label = DirectCast(e.Item.FindControl("CatIDLabel"), Label)
                Dim box As TextBox = DirectCast(e.Item.FindControl("DescriptionTextBox"), TextBox)
                If (Not Object.ReferenceEquals(objA, Nothing) And Not Object.ReferenceEquals(box, Nothing)) Then
                    Me.Save(Utility.ParseInt(objA.Text), box.Text)
                End If
            End If
        End Sub


        Private ds As DataSet
    End Class
End Namespace
