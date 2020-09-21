Imports Microsoft.VisualBasic.CompilerServices
Imports OSSCSC.Business.Business

Namespace Web
    Partial Class EditUsers
        Inherits MasterPage
        ' Methods
        Public Sub New()
            AddHandler MyBase.Init, New EventHandler(AddressOf Me.Page_Init)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Page_Load)
        End Sub

        Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
            Dim sec As New Security
            sec.Delete(IntegerType.FromString(Me.UserDropDownList.SelectedItem.Value))
            Me.PopulateUsers()
            Me.UserDropDownList.Items.FindByValue(StringType.FromInteger(-1)).Selected = True
            Me.PopulateUserDetails(-1)
        End Sub

        <DebuggerStepThrough>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Me.InitializeComponent()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this user?\nThis action cannot be undone.');")
            If Not Me.IsPostBack Then
                Me.PopulateUsers()
            End If
        End Sub

        Protected Overrides Sub PopulateTitle()
            Me.Controls.Add(New LiteralControl("<title>OSSCSC - Admin </title>" & ChrW(10)))
        End Sub

        Private Sub PopulateUserDetails(ByVal userID As Integer)
            Dim security As New Security
            If (userID = -1) Then
                Me.EmailTextBox.Text = String.Empty
                Me.FirstNameTextBox.Text = String.Empty
                Me.LastNameTextbox.Text = String.Empty
            Else
                Dim row As DataRow = security.GetUser(userID).Tables(0).Rows(0)
                Me.EmailTextBox.Text = StringType.FromObject(row("username"))
                Me.FirstNameTextBox.Text = Utility.TestDBNull(row("firstname"))
                Me.LastNameTextbox.Text = Utility.TestDBNull(row("lastname"))
            End If
        End Sub

        Private Sub PopulateUsers()
            Dim security As New Security
            Me.UserDropDownList.DataSource = security.GetUserList
            Me.UserDropDownList.DataTextField = "username"
            Me.UserDropDownList.DataValueField = "userid"
            Me.UserDropDownList.DataBind()
            Utility.AddExtraRow(Me.UserDropDownList, "-- create new user --")
        End Sub

        Private Sub SaveBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveBtn.Click
            If Me.Page.IsValid Then
                Dim str As String
                If (StringType.StrCmp(Me.PasswordTextBox.Text.Trim, String.Empty, False) <> 0) Then
                    str = CryptoUtil.EncryptTripleDES(Me.PasswordTextBox.Text.Trim)
                End If
                Dim security As New Security
                If (DoubleType.FromString(Me.UserDropDownList.SelectedItem.Value) <> -1) Then
                    security.Update(IntegerType.FromString(Me.UserDropDownList.SelectedItem.Value), Me.EmailTextBox.Text, str, Me.FirstNameTextBox.Text, Me.LastNameTextbox.Text)
                Else
                    Dim userID As Integer = security.Add(Me.EmailTextBox.Text, str, Me.FirstNameTextBox.Text, Me.LastNameTextbox.Text)
                    Me.PopulateUsers()
                    Me.UserDropDownList.Items.FindByValue(userID.ToString).Selected = True
                    Me.PopulateUserDetails(userID)
                End If
            End If
        End Sub

        Private Sub UserDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles UserDropDownList.SelectedIndexChanged
            Me.PopulateUserDetails(IntegerType.FromString(Me.UserDropDownList.SelectedItem.Value))
        End Sub


    End Class
End Namespace
