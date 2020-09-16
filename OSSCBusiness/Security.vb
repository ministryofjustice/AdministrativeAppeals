Imports OSSCSC.Data.Data

Namespace Business
    Public Class Security

        Public Function AuthenticateUser(ByVal Username As String, ByVal Password As String) As Boolean

            Dim data As New SecurityData
            Dim ds As DataSet = data.AuthenticateUser(Username, Password)
            If ds.Tables(0).Rows.Count = 1 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetUserList() As DataSet

            Dim data As New SecurityData
            Return data.GetUserList
        End Function

        Public Function GetUser(ByVal userID As Integer) As DataSet

            Dim data As New SecurityData
            Return data.GetUser(userID)
        End Function

        Public Sub Update(ByVal UserID As Integer, ByVal Username As String, ByVal Password As String,
                            ByVal Firstname As String, ByVal Surname As String)

            Dim data As New SecurityData
            data.Update(UserID, Username, Password, Firstname, Surname)
        End Sub

        Public Sub Delete(ByVal userID As Integer)

            Dim data As New SecurityData
            data.Delete(userID)
        End Sub

        Public Function Add(ByVal Username As String, ByVal Password As String,
                            ByVal Firstname As String, ByVal Surname As String) As Integer

            Dim data As New SecurityData
            Return data.Add(Username, Password, Firstname, Surname)
        End Function

    End Class
End Namespace

