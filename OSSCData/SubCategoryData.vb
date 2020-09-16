Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web

Namespace Data
    Public Class SubCategoryData

        Public Function GetList() As DataSet
            Dim ds As DataSet
            If HttpContext.Current.Cache("dsSubCategoryList") Is Nothing Then

                ds = SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                    CommandType.StoredProcedure,
                                    "spGetSubCategoryList")

                HttpContext.Current.Cache.Insert("dsSubCategoryList", ds)
            Else
                ds = CType(HttpContext.Current.Cache("dsSubCategoryList"), DataSet)
            End If

            Return ds
        End Function

        Public Function GetListByCategory(ByVal categoryId As Integer) As DataSet

            Dim parameters() As SqlParameter = {New SqlParameter("@CategoryId", SqlDbType.Int)}
            parameters(0).Value = categoryId
            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spGetSubCategoryListByCategory",
                                        parameters)

        End Function

        Public Sub Update(ByVal id As Integer, ByVal description As String, ByVal number As Integer, ByVal parentId As Integer)

            Dim parameters() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int),
                                                New SqlParameter("@Description", SqlDbType.VarChar, 100),
                                                New SqlParameter("@Parent_Num", SqlDbType.TinyInt),
                                                New SqlParameter("@Num", SqlDbType.TinyInt)
                                               }
            parameters(0).Value = id
            parameters(1).Value = description
            parameters(2).Value = parentId
            parameters(3).Value = number

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spUpdateSubCategory",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsSubCategoryList")
        End Sub

        Public Sub Add(ByVal description As String, ByVal number As Integer, ByVal parentId As Integer)

            Dim parameters() As SqlParameter = {New SqlParameter("@Description", SqlDbType.VarChar, 100),
                                                New SqlParameter("@Parent_Num", SqlDbType.TinyInt),
                                                New SqlParameter("@Num", SqlDbType.TinyInt)
                                               }
            parameters(0).Value = description
            parameters(1).Value = parentId
            parameters(2).Value = number

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spAddSubCategory",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsSubCategoryList")
        End Sub

        Public Sub Delete(ByVal Id As Integer, ByVal parentId As Integer)

            Dim parameters() As SqlParameter = {New SqlParameter("@Id", SqlDbType.Int)}
            parameters(0).Value = Id

            SqlHelper.ExecuteNonQuery(Configuration.SqlConnectionString,
                                        CommandType.StoredProcedure,
                                        "spDeleteSubCategory",
                                        parameters)

            HttpContext.Current.Cache.Remove("dsSubCategoryList")
        End Sub

        Public Function CountByCategory(ByVal categoryId As Integer) As Integer

            Dim parameters() As SqlParameter = {New SqlParameter("@CategoryId", SqlDbType.Int)}
            parameters(0).Value = categoryId

            Return SqlHelper.ExecuteScalar(Configuration.SqlConnectionString,
                                            CommandType.StoredProcedure,
                                            "spCountSubCategoryByCategory",
                                            parameters)
        End Function

    End Class
End Namespace

