Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls
Imports System.Text

Namespace DCA.TribunalsService.Ossc.Business
    Public Class Utility

        '/ <summary>
        '/ Parse an integer value from the specified string
        '/ or return the 'defaultValue' if the parsing fails.
        '/ </summary>
        '/ <param name="val"></param>
        '/ <param name="defaultValue"></param>
        '/ <returns></returns>
        Public Shared Function ParseInt(ByVal val As String, ByVal defaultValue As Integer) As Integer
            Try
                Return Integer.Parse(val)
            Catch
                Return defaultValue
            End Try
        End Function

        '/ <summary>
        '/ Parse an integer value from the specified string
        '/ or return '-1' if the parsing fails.
        '/ </summary>
        '/ <param name="val"></param>
        '/ <returns></returns>
        Public Shared Function ParseInt(ByVal val As String) As Integer
            Return ParseInt(val, -1)
        End Function

        Public Shared Function IsDate(ByVal input As String, ByVal defaultTo As DateTime) As DateTime
            If Not input.Equals(String.Empty) Then
                Try
                    Return DateTime.Parse(input)
                Catch ex As Exception
                    Return defaultTo
                End Try
            End If
            Return defaultTo
        End Function

        Public Shared Function PopulateSubCategory(ByVal dropDownList As DropDownList, ByVal categoryId As Integer)
            Dim data As New SubCategory
            Dim ds As DataSet = data.GetListByCategory(categoryId)
            Dim sb As New StringBuilder
            For Each dr As DataRow In ds.Tables(0).Rows
                'sb.Append(dr("parent_num"))
                'sb.Append(".")
                'sb.Append(dr("num"))
                'sb.Append(" ")
                sb.Append(dr("Description"))
                dropDownList.Items.Add(New ListItem(sb.ToString, dr("id")))
                sb.Remove(0, sb.Length)
            Next
            ds = Nothing
            data = Nothing
        End Function

        Public Shared Function GetExtension(ByVal filename As String) As String
            Dim lastPeriod As Integer = filename.LastIndexOf(".", filename.Length)
            If lastPeriod > 0 And filename.Length > lastPeriod Then
                Return filename.Substring(lastPeriod + 1)
            Else
                Return String.Empty
            End If

        End Function

        Public Shared Sub PopulateCategory(ByVal dropDownList As DropDownList)
            AddExtraRow(dropDownList, "-- Choose a Category --")
            Dim cat As New Business.Category
            Dim ds As DataSet = cat.GetCategoryList()
            For Each dr As DataRow In ds.Tables(0).Rows
                'dropDownList.Items.Add(New ListItem(String.Concat(dr("num"), " ", dr("description")), dr("num")))
                dropDownList.Items.Add(New ListItem(String.Concat(dr("description")), dr("num")))
            Next
            ds = Nothing
        End Sub

        Public Shared Sub PopulateCommissioners(ByVal listContol As ListControl, ByVal id As Integer)

            Dim comm As New Business.Commissioner
            Dim ds As DataSet
            If id = 0 Then
                AddExtraRow(listContol, "-- Choose a Judge/Commissioner --")
                ds = comm.GetCommissionerList
            Else
                AddExtraRow(listContol, "-- Use Crtl to select multiple Judges/Commissioners --")
                ds = comm.GetCommissionerListSelected(id)
            End If
            For Each dr As DataRow In ds.Tables(0).Rows
                listContol.Items.Add(New ListItem(dr("surname") & ", " & dr("prefix") & " " & dr("surname") & " " & dr("suffix"), dr("id")))
            Next
        End Sub

        Public Shared Sub AddExtraRow(ByVal listControl As ListControl, ByVal text As String)

            listControl.Items.Insert(0, New ListItem(text, "-1"))

        End Sub

        Public Shared Function TestDBNull(ByVal value As Object) As String
            If value Is DBNull.Value Then
                Return String.Empty
            Else
                Return value
            End If
        End Function

        Public Shared Function TestNullForInt(ByVal value As Object) As Integer
            If value Is DBNull.Value Then
                Return -1
            Else
                Return value
            End If
        End Function

        Public Shared Function Highlight(ByVal searchFor As String, ByVal searchIn As String) As String

            ' Setup the regular expression and add the Or operator.
            Dim RegExp As Regex = New Regex(searchFor, RegexOptions.IgnoreCase)

            ' Highlight keywords by calling the MatchEvaluator delegate each time a keyword is found.
            Highlight = RegExp.Replace(searchIn, New MatchEvaluator(AddressOf ReplaceKeyWords))

            ' Set the Regex to nothing.
            RegExp = Nothing

        End Function

        Public Shared Function ReplaceKeyWords(ByVal m As Match) As String

            Return "<span class=highlight>" & m.Value & "</span>"

        End Function


        Public Shared Function GetDate(ByVal dt As String) As String
            If dt.Length > 0 Then
                Return DateTime.Parse(dt).ToString("dd/MM/yyyy")
            Else
                Return String.Empty
            End If
        End Function


    End Class
End Namespace

