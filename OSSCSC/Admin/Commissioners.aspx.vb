Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business

Namespace Web
    Partial Class Commissioners
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
                Me.CommDataGrid.EditItemIndex = (Me.ds.Tables(0).Rows.Count - 1)
            End If
        End Sub

        Private Sub CommDataGrid_CancelCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles CommDataGrid.CancelCommand
            Me.CommDataGrid.EditItemIndex = -1
        End Sub

        Private Sub CommDataGrid_DeleteCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles CommDataGrid.DeleteCommand
            Dim label As Label = DirectCast(e.Item.FindControl("CommIDLabel"), Label)
            If (Not label Is Nothing) Then
                Me.Delete(IntegerType.FromString(label.Text))
            End If
        End Sub

        Private Sub CommDataGrid_EditCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles CommDataGrid.EditCommand
            Me.Page.Validate()

            If Me.Page.IsValid Then
                Me.CommDataGrid.EditItemIndex = e.Item.ItemIndex
            End If
        End Sub

        Private Sub CommDataGrid_UpdateCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles CommDataGrid.UpdateCommand
            Me.Page.Validate()

            If Me.Page.IsValid Then
                Dim objA As Label = DirectCast(e.Item.FindControl("CommIDLabel"), Label)
                Dim box As TextBox = DirectCast(e.Item.FindControl("PrefixTextBox"), TextBox)
                Dim box3 As TextBox = DirectCast(e.Item.FindControl("SurnameTextBox"), TextBox)
                Dim box2 As TextBox = DirectCast(e.Item.FindControl("SuffixTextBox"), TextBox)
                If (((Not Object.ReferenceEquals(objA, Nothing) And Not Object.ReferenceEquals(box, Nothing)) And Not Object.ReferenceEquals(box3, Nothing)) And Not Object.ReferenceEquals(box2, Nothing)) Then
                    Me.Save(Utility.ParseInt(objA.Text), box.Text, box3.Text, box2.Text)
                End If
            End If
        End Sub

        Private Sub Delete(ByVal Id As Integer)
            If (New Decision().GetCountByCommissioner(Id) > 0) Then
                Me.MessageLabel.Text = "Cannot delete Commissioner as one or more Judgments are currently assigned to it."
            Else
                Dim commissioner As New Commissioner
                commissioner.Delete(Id)
                Me.ds = commissioner.GetCommissionerList
                Me.CommDataGrid.EditItemIndex = -1
            End If
        End Sub

        Private Sub GetCategory()
            Me.ds = New Commissioner().GetCommissionerList(False)
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
            Me.CommDataGrid.DataSource = Me.ds
            Me.CommDataGrid.DataBind()

            If (Me.CommDataGrid.EditItemIndex >= 0) Then
                Dim control As Control = Me.CommDataGrid.Items(Me.CommDataGrid.EditItemIndex).FindControl("PrefixTextBox")
                If (Not control Is Nothing) Then
                    Me.SetFocus(control)
                End If
            End If
        End Sub

        Protected Overrides Sub PopulateTitle()
            Me.Controls.Add(New LiteralControl("<title>OSSCSC - Admin </title>" & ChrW(10)))
        End Sub

        Private Sub Save(ByVal id As Integer, ByVal prefix As String, ByVal surname As String, ByVal suffix As String)
            Dim commissioner As New Commissioner
            If (id = 0) Then
                commissioner.Add(prefix, surname, suffix)
            Else
                commissioner.Update(id, prefix, surname, suffix)
            End If
            Me.ds = commissioner.GetCommissionerList
            Me.CommDataGrid.EditItemIndex = -1
        End Sub

        Private ds As DataSet
    End Class
End Namespace
