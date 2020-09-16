Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace Data
    Public Class SecurityData

        Public Function AuthenticateUser(ByVal Username As String, ByVal Password As String) As DataSet

            Dim parameters() As SqlParameter = {New SqlParameter("@Username", SqlDbType.VarChar, 100),
                                                New SqlParameter("@Password", SqlDbType.VarChar, 15)}

            parameters(0).Value = Username
            parameters(1).Value = Password

            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                            CommandType.StoredProcedure,
                                            "spLoginUser",
                                            parameters)
        End Function

        Public Function GetUserList() As DataSet

            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                CommandType.StoredProcedure,
                                "spGetUserList")

        End Function

        Public Function GetUser(ByVal userID As Integer) As DataSet

            Dim parameters() As SqlParameter = {New SqlParameter("@UserID", SqlDbType.Int)}

            parameters(0).Value = userID

            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                CommandType.StoredProcedure,
                                "spGetUser",
                                parameters)

        End Function

        Public Sub Delete(ByVal userID As Integer)

            Dim parameters() As SqlParameter = {New SqlParameter("@UserID", SqlDbType.Int)}

            parameters(0).Value = userID

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                CommandType.StoredProcedure,
                                "spDeleteUser",
                                parameters)

        End Sub

        Public Sub Update(ByVal UserID As Integer, ByVal Username As String, ByVal Password As String, ByVal Firstname As String, ByVal Surname As String)

            Dim parameters() As SqlParameter = {New SqlParameter("@UserID", SqlDbType.Int),
                                                New SqlParameter("@Username", SqlDbType.VarChar, 50),
                                                New SqlParameter("@Password", SqlDbType.VarChar, 50),
                                                New SqlParameter("@Firstname", SqlDbType.VarChar, 50),
                                                New SqlParameter("@Lastname", SqlDbType.VarChar, 50)}

            parameters(0).Value = UserID
            parameters(1).Value = Username
            parameters(2).Value = Password
            parameters(3).Value = Firstname
            parameters(4).Value = Surname

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                            CommandType.StoredProcedure,
                                            "spUpdateUser",
                                            parameters)
        End Sub

        Public Function Add(ByVal Username As String, ByVal Password As String, ByVal Firstname As String, ByVal Surname As String) As Integer

            Dim parameters() As SqlParameter = {New SqlParameter("@UserID", SqlDbType.Int),
                                                New SqlParameter("@Username", SqlDbType.VarChar, 50),
                                                New SqlParameter("@Password", SqlDbType.VarChar, 50),
                                                New SqlParameter("@Firstname", SqlDbType.VarChar, 50),
                                                New SqlParameter("@Lastname", SqlDbType.VarChar, 50)}

            parameters(0).Direction = ParameterDirection.Output
            parameters(1).Value = Username
            parameters(2).Value = Password
            parameters(3).Value = Firstname
            parameters(4).Value = Surname

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                            CommandType.StoredProcedure,
                                            "spAddUser",
                                            parameters)

            Return Convert.ToInt32(parameters(0).Value)
        End Function

    End Class
End Namespace

