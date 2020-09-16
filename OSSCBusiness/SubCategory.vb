Imports DCA.TribunalsService.Ossc.Data
Imports System.Text
Imports System
Imports DCA.TribunalsService.Ossc.Data.DCA.TribunalsService.Ossc.Data

Namespace DCA.TribunalsService.Ossc.Business
    Public Class SubCategory

        Private Function GetList() As DataSet
            Dim data As New SubCategoryData
            Return data.GetList
        End Function

        Public Function GetListByCategory(ByVal categoryId As Integer) As DataSet
            Dim data As New SubCategoryData
            Return data.GetListByCategory(categoryId)
        End Function

        Public Function GetScriptBlock() As String
            Dim ds As DataSet = Me.GetList
            Dim sb As New StringBuilder
            sb.Append("<script language=""Javascript"" type=""text/javascript"">" & vbLf)
            sb.Append("<!--" & vbLf)
            sb.Append("var subTextArray = new Array();" & vbLf)
            sb.Append("var subValueArray = new Array();" & vbLf)

            Dim currCatId As Integer = 0
            For Each dr As DataRow In ds.Tables(0).Rows
                If Not currCatId = Int32.Parse(dr("parent_num")) Then
                    sb.Append("subTextArray[" & dr("parent_num") & "] = new Array();" & vbLf)
                    sb.Append("subValueArray[" & dr("parent_num") & "] = new Array();" & vbLf)

                    currCatId = Int32.Parse(dr("parent_num"))
                End If
                'sb.Append("subTextArray[" & Replace(dr("parent_num"), "'", "\'") & "][" & dr("num") & "] = """ & dr("parent_num") & "." & dr("num") & " " & Replace(dr("description"), "'", "\'") & """;" & vbLf)
                sb.Append("subTextArray[" & Replace(dr("parent_num"), "'", "\'") & "][" & dr("num") & "] = """ & Replace(dr("description"), "'", "\'") & """;" & vbLf)
                sb.Append("subValueArray[" & dr("parent_num") & "][" & dr("num") & "] = " & dr("id") & ";" & vbLf)
            Next
            sb.Append("//-->" & vbLf)
            sb.Append("</script>" & vbLf)

            Return sb.ToString
        End Function

        Public Sub Update(ByVal id As Integer, ByVal description As String, ByVal number As Integer, ByVal parentId As Integer)
            Dim data As New SubCategoryData
            data.Update(id, description, number, parentId)
        End Sub

        Public Sub Add(ByVal description As String, ByVal number As Integer, ByVal parentId As Integer)
            Dim data As New SubCategoryData
            data.Add(description, number, parentId)
        End Sub

        Public Sub Delete(ByVal Id As Integer, ByVal parentId As Integer)
            Dim data As New SubCategoryData
            data.Delete(Id, parentId)
        End Sub

        Public Function CountByCategory(ByVal categoryId As Integer) As Integer
            Dim data As New SubCategoryData
            Return data.CountByCategory(categoryId)
        End Function

    End Class
End Namespace

