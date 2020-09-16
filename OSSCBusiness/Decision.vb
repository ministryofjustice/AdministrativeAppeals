Imports OSSCSC.Data.Data
Imports OSSCSC.Entity.Entity

Namespace Business
    Public Class Decision

        Public Function SearchPaged(ByVal criteria As SearchCriteria, ByVal page As Integer,
                                    ByVal size As Integer, ByRef resultsCount As Integer) As DataSet

            Dim data As New DecisionData
            Return data.SearchPaged(criteria, page, size, resultsCount)
        End Function

        Public Function GetDecision(ByVal id As Integer)
            Dim data As New DecisionData
            Return data.GetDecision(id)
        End Function

        Public Function GetDecision(ByVal id As Integer, ByVal IsPublished As Boolean)
            Dim data As New DecisionData
            Return data.GetDecision(id, IsPublished)
        End Function

        Public Sub Update(ByVal decision As DecisionEntity)
            Dim data As New DecisionData
            data.Update(decision)
        End Sub

        Public Function Add(ByVal decision As DecisionEntity) As Integer
            Dim data As New DecisionData
            Return data.Add(decision)
        End Function

        Public Function GetCountBySubCategory(ByVal Id As Integer) As Integer
            Dim data As New DecisionData
            Return data.GetCountBySubCategory(Id)
        End Function

        Public Function GetCountByCommissioner(ByVal Id As Integer) As Integer
            Dim data As New DecisionData
            Return data.GetCountByCommissioner(Id)
        End Function

    End Class
End Namespace

