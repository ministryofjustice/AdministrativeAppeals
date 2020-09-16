Imports OSSCSC.Data.Data

Namespace Business
    Public Class Category

        Public Function GetCategoryList() As DataSet
            Dim data As New CategoryData
            Return data.GetCategoryList
        End Function

        Public Function GetCategoryList(ByVal useCache As Boolean) As DataSet
            Dim data As New CategoryData
            If useCache Then
                Return data.GetCategoryList
            Else
                Return data.GetCategoryListNoCache
            End If
        End Function


        Public Sub Add(ByVal description As String)
            Dim data As New CategoryData
            data.Add(description)
        End Sub

        Public Sub Update(ByVal id As Integer, ByVal description As String)
            Dim data As New CategoryData
            data.Update(id, description)
        End Sub

        Public Sub Delete(ByVal Id As Integer)
            Dim data As New CategoryData
            data.Delete(Id)
        End Sub

    End Class
End Namespace

