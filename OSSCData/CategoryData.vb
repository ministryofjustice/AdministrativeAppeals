Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web

Namespace DCA.TribunalsService.Ossc.Data
    Public Class CategoryData

        Public Function GetCategoryList() As DataSet
            Dim ds As DataSet

            If HttpContext.Current.Cache("dsCategoryList") Is Nothing Then

                ds = SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                    CommandType.StoredProcedure,
                                    "spGetCategoryList")

                HttpContext.Current.Cache.Insert("dsCategoryList", ds)
            Else
                ds = CType(HttpContext.Current.Cache("dsCategoryList"), DataSet)
            End If

            Return ds
        End Function

        Public Function GetCategoryListNoCache() As DataSet

            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                CommandType.StoredProcedure,
                                "spGetCategoryList")

        End Function

        Public Sub Update(ByVal id As Integer, ByVal description As String)

            Dim parameters() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int),
                                                New SqlParameter("@Description", SqlDbType.VarChar, 100)
                                               }
            parameters(0).Value = id
            parameters(1).Value = description

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spUpdateCategory",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsCategoryList")
        End Sub

        Public Sub Add(ByVal description As String)

            Dim parameters() As SqlParameter = {New SqlParameter("@Description", SqlDbType.VarChar, 100)}
            parameters(0).Value = description

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spAddCategory",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsCategoryList")
        End Sub

        Public Sub Delete(ByVal Id As Integer)

            Dim parameters() As SqlParameter = {New SqlParameter("@Id", SqlDbType.Int)}
            parameters(0).Value = Id

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spDeleteCategory",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsCategoryList")
        End Sub

    End Class
End Namespace

