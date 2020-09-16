Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DCA.TribunalsService.Ossc.Entity
Imports System.Web

Namespace DCA.TribunalsService.Ossc.Data
    Public Class CommissionerData

        Public Function GetCommissionerList() As DataSet
            Dim ds As DataSet
            If HttpContext.Current.Cache("dsCommissionerList") Is Nothing Then

                ds = SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                    CommandType.StoredProcedure,
                                    "spGetCommissionerList")

                HttpContext.Current.Cache.Insert("dsCommissionerList", ds)
            Else
                ds = CType(HttpContext.Current.Cache("dsCommissionerList"), DataSet)
            End If

            Return ds
        End Function

        Public Function GetCommissionerListNoCache() As DataSet

            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                    CommandType.StoredProcedure,
                                    "spGetCommissionerList")

        End Function

        Public Function GetCommissionerListSelected(ByVal Id As Integer) As DataSet

            Dim parameters() As SqlParameter = {New SqlParameter("@DecisionId", SqlDbType.Int)}
            parameters(0).Value = Id

            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spGetCommissionerListSelected",
                                        parameters)

        End Function

        Public Sub Update(ByVal id As Integer, ByVal prefix As String, ByVal surname As String, ByVal suffix As String)

            Dim parameters() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int),
                                                New SqlParameter("@Prefix", SqlDbType.VarChar, 100),
                                                New SqlParameter("@Surname", SqlDbType.VarChar, 300),
                                                New SqlParameter("@Suffix", SqlDbType.VarChar, 100)
                                               }
            parameters(0).Value = id
            parameters(1).Value = prefix
            parameters(2).Value = surname
            parameters(3).Value = suffix

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spUpdateCommissioner",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsCommissionerList")
        End Sub

        Public Sub Add(ByVal prefix As String, ByVal surname As String, ByVal suffix As String)

            Dim parameters() As SqlParameter = {New SqlParameter("@Prefix", SqlDbType.VarChar, 100),
                                                New SqlParameter("@Surname", SqlDbType.VarChar, 300),
                                                New SqlParameter("@Suffix", SqlDbType.VarChar, 100)
                                               }
            parameters(0).Value = prefix
            parameters(1).Value = surname
            parameters(2).Value = suffix

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spAddCommissioner",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsCommissionerList")
        End Sub

        Public Sub Delete(ByVal Id As Integer)

            Dim parameters() As SqlParameter = {New SqlParameter("@Id", SqlDbType.Int)}
            parameters(0).Value = Id

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spDeleteCommissioner",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsCommissionerList")
        End Sub

    End Class
End Namespace

