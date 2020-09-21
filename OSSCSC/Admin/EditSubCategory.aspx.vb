Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business

Namespace Web
    Partial Class EditSubCategory
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
                row("id") = 0
                Me.ds.Tables(0).Rows.Add(row)
                Me.SubCatDataGrid.EditItemIndex = (Me.ds.Tables(0).Rows.Count - 1)
            End If
        End Sub

        Private Sub Delete(ByVal Id As Integer, ByVal parentId As Integer)
            If (New Decision().GetCountBySubCategory(Id) > 0) Then
                Me.MessageLabel.Text = "Cannot delete Subcategory as Judgment(s) are currently assigned to it."
            Else
                Dim category As New SubCategory
                category.Delete(Id, parentId)
                Me.ds = category.GetListByCategory(parentId)
                Me.SubCatDataGrid.EditItemIndex = -1
            End If
        End Sub

        Private Sub GetSubCategory(ByVal categoryId As Integer)
            Me.ds = New SubCategory().GetListByCategory(categoryId)
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.categoryId = Utility.ParseInt(Me.Request("id"), 1)
            Me.GetSubCategory(Me.categoryId)
        End Sub

        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs)
            Me.SubCatDataGrid.DataSource = Me.ds
            Me.SubCatDataGrid.DataBind()

            If (Me.SubCatDataGrid.EditItemIndex >= 0) Then
                Dim control As Control = Me.SubCatDataGrid.Items(Me.SubCatDataGrid.EditItemIndex).FindControl("DescriptionTextBox")
                If (Not control Is Nothing) Then
                    Me.SetFocus(control)
                End If
            End If
        End Sub

        Protected Overrides Sub PopulateTitle()
            Me.Controls.Add(New LiteralControl("<title>OSSCSC - Admin </title>" & ChrW(10)))
        End Sub

        Private Sub Save(ByVal id As Integer, ByVal description As String, ByVal number As Integer, ByVal parentId As Integer)
            Dim category As New SubCategory
            If (id = 0) Then
                category.Add(description, number, parentId)
            Else
                category.Update(id, description, number, parentId)
            End If
            Me.ds = category.GetListByCategory(parentId)
            Me.SubCatDataGrid.EditItemIndex = -1
        End Sub

        Private Sub SubCatDataGrid_CancelCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles SubCatDataGrid.CancelCommand
            Me.SubCatDataGrid.EditItemIndex = -1
        End Sub

        Private Sub SubCatDataGrid_DeleteCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles SubCatDataGrid.DeleteCommand
            Dim label As Label = DirectCast(e.Item.FindControl("SubCatIDLabel"), Label)
            If (Not label Is Nothing) Then
                Me.Delete(IntegerType.FromString(label.Text), Me.categoryId)
            End If
        End Sub

        Private Sub SubCatDataGrid_EditCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles SubCatDataGrid.EditCommand
            Me.Page.Validate()

            If Me.Page.IsValid Then
                Me.SubCatDataGrid.EditItemIndex = e.Item.ItemIndex
            End If
        End Sub

        Private Sub SubCatDataGrid_UpdateCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles SubCatDataGrid.UpdateCommand
            Me.Page.Validate()

            If Me.Page.IsValid Then
                Dim objA As Label = DirectCast(e.Item.FindControl("SubCatIDLabel"), Label)
                Dim box As TextBox = DirectCast(e.Item.FindControl("DescriptionTextBox"), TextBox)
                Dim box2 As TextBox = DirectCast(e.Item.FindControl("NumberTextBox"), TextBox)
                If ((Not Object.ReferenceEquals(objA, Nothing) And Not Object.ReferenceEquals(box, Nothing)) And Not Object.ReferenceEquals(box2, Nothing)) Then
                    Me.Save(Utility.ParseInt(objA.Text), box.Text, IntegerType.FromString(box2.Text), Me.categoryId)
                End If
            End If
        End Sub

        Private ds As DataSet
        Private categoryId As Integer
    End Class
End Namespace
