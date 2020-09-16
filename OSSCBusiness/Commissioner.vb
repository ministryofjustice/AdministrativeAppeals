Imports DCA.TribunalsService.Ossc.Data
Imports DCA.TribunalsService.Ossc.Data.DCA.TribunalsService.Ossc.Data

Namespace DCA.TribunalsService.Ossc.Business
    Public Class Commissioner

        Public Function GetCommissionerList() As DataSet
            Dim data As New CommissionerData
            Return data.GetCommissionerList
        End Function

        Public Function GetCommissionerList(ByVal useCache As Boolean) As DataSet
            Dim data As New CommissionerData

            If useCache Then
                Return data.GetCommissionerList
            Else
                Return data.GetCommissionerListNoCache
            End If
        End Function

        Public Function GetCommissionerListSelected(ByVal id As Integer) As DataSet
            Dim data As New CommissionerData
            Return data.GetCommissionerListSelected(id)
        End Function

        Public Sub Add(ByVal prefix As String, ByVal surname As String, ByVal suffix As String)
            Dim data As New CommissionerData
            data.Add(prefix, surname, suffix)
        End Sub

        Public Sub Update(ByVal id As Integer, ByVal prefix As String, ByVal surname As String, ByVal suffix As String)
            Dim data As New CommissionerData
            data.Update(id, prefix, surname, suffix)
        End Sub

        Public Sub Delete(ByVal Id As Integer)
            Dim data As New CommissionerData
            data.Delete(Id)
        End Sub

    End Class
End Namespace

