Imports System.Data
Imports System.Data.SqlClient
Imports Reeb.SqlOM
Imports Reeb.SqlOM.Render
Imports Microsoft.ApplicationBlocks.Data
Imports DCA.TribunalsService.Ossc.Entity
Imports DCA.TribunalsService.Ossc.Entity.DCA.TribunalsService.Ossc.Entity

Namespace DCA.TribunalsService.Ossc.Data
    Public Class DecisionData

        Public Function GetDecision(ByVal id As Integer) As DataSet

            Dim parameters() As SqlParameter = {New SqlParameter("@DecisionId", SqlDbType.Int)}

            parameters(0).Value = id

            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                            CommandType.StoredProcedure,
                                            "spGetDecision",
                                            parameters)
        End Function

        Public Function GetDecision(ByVal id As Integer, ByVal IsPublished As Boolean) As DataSet

            Dim parameters() As SqlParameter = {New SqlParameter("@DecisionId", SqlDbType.Int),
                                                New SqlParameter("@Is_Published", SqlDbType.Bit)}

            parameters(0).Value = id
            parameters(1).Value = IsPublished

            Return SqlHelper.ExecuteDataset(Configuration.SqlConnectionString,
                                            CommandType.StoredProcedure,
                                            "spGetDecisionPublished",
                                            parameters)
        End Function

        Public Function GetCountBySubCategory(ByVal Id As Integer) As Integer

            Dim parameters() As SqlParameter = {New SqlParameter("@Id", SqlDbType.Int)}

            parameters(0).Value = Id

            Return SqlHelper.ExecuteScalar(Configuration.SqlConnectionString,
                                            CommandType.StoredProcedure,
                                            "spCountJudgmentsBySubCategory",
                                            parameters)
        End Function

        Public Function GetCountByCommissioner(ByVal Id As Integer) As Integer

            Dim parameters() As SqlParameter = {New SqlParameter("@Id", SqlDbType.Int)}

            parameters(0).Value = Id

            Return SqlHelper.ExecuteScalar(Configuration.SqlConnectionString,
                                            CommandType.StoredProcedure,
                                            "spCountJudgmentsByCommissioner",
                                            parameters)
        End Function

        Public Function SearchPaged(ByVal criteria As SearchCriteria, ByVal page As Integer,
                                    ByVal size As Integer, ByRef resultsCount As Integer) As DataSet

            Dim parameters() As SqlParameter = {New SqlParameter("@CategoryID", SqlDbType.Int),
                                                New SqlParameter("@SubCategoryID", SqlDbType.Int),
                                                New SqlParameter("@DecisionDate", SqlDbType.DateTime),
                                                New SqlParameter("@FromDate", SqlDbType.DateTime),
                                                New SqlParameter("@ToDate", SqlDbType.DateTime),
                                                New SqlParameter("@Appellant", SqlDbType.NText),
                                                New SqlParameter("@Respondent", SqlDbType.NText),
                                                New SqlParameter("@Year", SqlDbType.VarChar, 4),
                                                New SqlParameter("@CaseNo", SqlDbType.VarChar, 5),
                                                New SqlParameter("@Prefix", SqlDbType.VarChar, 5),
                                                New SqlParameter("@CommissionerID", SqlDbType.Int, 5),
                                                New SqlParameter("@Reported1", SqlDbType.VarChar, 3),
                                                New SqlParameter("@Reported2", SqlDbType.VarChar, 3),
                                                New SqlParameter("@Reported3", SqlDbType.VarChar, 2),
                                                New SqlParameter("@Notes", SqlDbType.VarChar, 50),
                                                New SqlParameter("@FromDateAdded", SqlDbType.DateTime),
                                                New SqlParameter("@ToDateAdded", SqlDbType.DateTime),
                                                New SqlParameter("@NCN_year", SqlDbType.Int),
                                                New SqlParameter("@NCN_code1", SqlDbType.VarChar, 20),
                                                New SqlParameter("@NCN_citation", SqlDbType.Int),
                                                New SqlParameter("@NCN_code2", SqlDbType.VarChar, 20)
                                                }

            'left join commissionerjudgmentmap cjm on j.[id] = cjm.judgment_id
            'left join commissioner cm on cjm.commissioner_id = cm.[id]
            Dim tableJudgment As FromTerm = FromTerm.Table("judgment", "j")
            Dim tableSubCategory As FromTerm = FromTerm.Table("subcategory", "s")
            Dim tableCategory As FromTerm = FromTerm.Table("category", "c")
            Dim tableCategory2 As FromTerm = FromTerm.Table("category", "c2")
            Dim tableSubCategory2 As FromTerm = FromTerm.Table("subcategory", "s2")
            Dim tableCommissionerJudgmentMap As FromTerm = FromTerm.Table("commissionerjudgmentmap", "cjm")
            Dim tableCommissioner As FromTerm = FromTerm.Table("commissioner", "cm")

            Dim query As New SelectQuery
            query.Distinct = True
            query.Columns.Add(New SelectColumn("id", tableJudgment, "judgmentid"))
            query.Columns.Add(New SelectColumn("decision_datetime", tableJudgment))
            query.Columns.Add(New SelectColumn("created_datetime", tableJudgment))
            query.Columns.Add(New SelectColumn("file_no_1", tableJudgment, "prefix"))
            query.Columns.Add(New SelectColumn("file_no_2", tableJudgment, "casenumber"))
            query.Columns.Add(New SelectColumn("file_no_3", tableJudgment, "year"))

            query.Columns.Add(New SelectColumn("ncn_year", tableJudgment, "ncnyear"))
            query.Columns.Add(New SelectColumn("ncn_code1", tableJudgment, "ncncode1"))
            query.Columns.Add(New SelectColumn("ncn_citation", tableJudgment, "ncncitation"))
            query.Columns.Add(New SelectColumn("ncn_code2", tableJudgment, "ncncode2"))

            query.Columns.Add(New SelectColumn("is_published", tableJudgment))
            query.Columns.Add(New SelectColumn("description", tableSubCategory, "subcategory"))
            query.Columns.Add(New SelectColumn("description", tableCategory, "category"))
            query.FromClause.BaseTable = tableJudgment
            query.FromClause.Join(JoinType.Inner, tableJudgment, tableSubCategory, "main_subcategory_id", "id")
            query.FromClause.Join(JoinType.Inner, tableSubCategory, tableCategory, "parent_num", "num")
            query.FromClause.Join(JoinType.Left, tableJudgment, tableSubCategory2, "sec_subcategory_id", "id")
            query.FromClause.Join(JoinType.Left, tableSubCategory2, tableCategory2, "parent_num", "num")
            query.FromClause.Join(JoinType.Left, tableJudgment, tableCommissionerJudgmentMap, "id", "judgment_id")
            query.FromClause.Join(JoinType.Left, tableCommissionerJudgmentMap, tableCommissioner, "commissioner_id", "id")

            ' Category
            If criteria.CategoryID <> -1 Then
                Dim group As New WhereClause(WhereClauseRelationship.Or)
                group.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("num", tableCategory), SqlExpression.Parameter("@CategoryID"), CompareOperator.Equal))
                group.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("num", tableCategory2), SqlExpression.Parameter("@CategoryID"), CompareOperator.Equal))
                query.WherePhrase.SubClauses.Add(group)
                parameters(0).Value = criteria.CategoryID
            End If

            ' Subcategory
            If criteria.SubCategoryID <> -1 Then
                Dim group As New WhereClause(WhereClauseRelationship.Or)
                group.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("id", tableSubCategory), SqlExpression.Parameter("@SubCategoryID"), CompareOperator.Equal))
                group.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("id", tableSubCategory2), SqlExpression.Parameter("@SubCategoryID"), CompareOperator.Equal))
                query.WherePhrase.SubClauses.Add(group)
                parameters(1).Value = criteria.SubCategoryID
            End If

            'Decision Date
            If Not criteria.DecisionDate.Equals(DateTime.MinValue) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("decision_datetime", tableJudgment), SqlExpression.Parameter("@DecisionDate"), CompareOperator.Equal))
                parameters(2).Value = criteria.DecisionDate
            End If

            ' From Date
            If Not criteria.FromDate.Equals(DateTime.MinValue) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("decision_datetime", tableJudgment), SqlExpression.Parameter("@FromDate"), CompareOperator.GreaterOrEqual))
                parameters(3).Value = criteria.FromDate
            End If

            ' To Date
            If Not criteria.ToDate.Equals(DateTime.MinValue) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("decision_datetime", tableJudgment), SqlExpression.Parameter("@ToDate"), CompareOperator.LessOrEqual))
                parameters(4).Value = criteria.ToDate
            End If

            ' Claimant
            If Not criteria.Claimant.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("claimants", tableJudgment), SqlExpression.Parameter("@Appellant"), CompareOperator.Like))
                parameters(5).Value = String.Concat("%", criteria.Claimant, "%")
            End If

            ' Respondent
            If Not criteria.Respondent.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("respondent", tableJudgment), SqlExpression.Parameter("@Respondent"), CompareOperator.Like))
                parameters(6).Value = String.Concat("%", criteria.Respondent, "%")
            End If

            ' Year
            If Not criteria.Year.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("file_no_3", tableJudgment), SqlExpression.Parameter("@Year"), CompareOperator.Equal))
                parameters(7).Value = criteria.Year
            End If

            ' CaseNo
            If Not criteria.CaseNo.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("file_no_2", tableJudgment), SqlExpression.Parameter("@CaseNo"), CompareOperator.Equal))
                parameters(8).Value = criteria.CaseNo
            End If

            ' Prefix
            If Not criteria.Prefix.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("file_no_1", tableJudgment), SqlExpression.Parameter("@Prefix"), CompareOperator.Equal))
                parameters(9).Value = criteria.Prefix
            End If

            'Commissioner
            If criteria.CommissionerID <> -1 Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("id", tableCommissioner), SqlExpression.Parameter("@CommissionerID"), CompareOperator.Equal))
                parameters(10).Value = criteria.CommissionerID
            End If

            'Reported1
            If Not criteria.Reported1.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("reported_no_1", tableJudgment), SqlExpression.Parameter("@Reported1"), CompareOperator.Equal))
                parameters(11).Value = criteria.Reported1
            End If

            'Reported2
            If Not criteria.Reported2.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("reported_no_2", tableJudgment), SqlExpression.Parameter("@Reported2"), CompareOperator.Equal))
                parameters(12).Value = criteria.Reported2
            End If

            'Reported3
            If Not criteria.Reported3.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("reported_no_3", tableJudgment), SqlExpression.Parameter("@Reported3"), CompareOperator.Equal))
                parameters(13).Value = criteria.Reported3
            End If

            'Notes
            If Not criteria.Notes.Equals(String.Empty) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("headnote_summary", tableJudgment), SqlExpression.Parameter("@Notes"), CompareOperator.Like))
                parameters(14).Value = String.Concat("%", criteria.Notes, "%")
            End If

            ' From Date Added
            If Not criteria.FromDateAdded.Equals(DateTime.MinValue) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("created_datetime", tableJudgment), SqlExpression.Parameter("@FromDateAdded"), CompareOperator.GreaterOrEqual))
                parameters(15).Value = criteria.FromDateAdded
            End If

            ' To Date Added
            If Not criteria.ToDateAdded.Equals(DateTime.MinValue) Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("created_datetime", tableJudgment), SqlExpression.Parameter("@ToDateAdded"), CompareOperator.LessOrEqual))
                parameters(16).Value = criteria.ToDateAdded
            End If

            ' IsPublished
            If criteria.IsPublished Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("is_published", tableJudgment), SqlExpression.Number(1), CompareOperator.Equal))
            End If

            If criteria.NCNYear > 0 Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("ncn_year", tableJudgment), SqlExpression.Parameter("@NCN_Year"), CompareOperator.Equal))
                parameters(17).Value = criteria.NCNYear
            End If

            If criteria.NCNCode1.Length > 0 Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("ncn_code1", tableJudgment), SqlExpression.Parameter("@NCN_Code1"), CompareOperator.Equal))
                parameters(18).Value = criteria.NCNCode1
            End If

            If criteria.NCNCitaion > 0 Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("ncn_citation", tableJudgment), SqlExpression.Parameter("@NCN_Citation"), CompareOperator.Equal))
                parameters(19).Value = criteria.NCNCitaion
            End If


            If criteria.NCNCode2.Length > 0 Then
                query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("ncn_code2", tableJudgment), SqlExpression.Parameter("@NCN_Code2"), CompareOperator.Equal))
                parameters(20).Value = criteria.NCNCode2
            End If


            ' order by direction
            Dim direction As OrderByDirection
            If criteria.SortDirection = "ASC" Then
                direction = OrderByDirection.Ascending
            Else
                direction = OrderByDirection.Descending
            End If

            ' order col
            Dim table As FromTerm
            If criteria.SortColumn = "category" Then
                table = tableCategory
                criteria.SortColumn = "description"
            End If
            If criteria.SortColumn = "subcategory" Then
                table = tableSubCategory
                criteria.SortColumn = "description"
            End If

            query.OrderByTerms.Add(New OrderByTerm(criteria.SortColumn, table, direction))

            If criteria.SortColumn = "file_no_1" Then
                query.OrderByTerms.Add(New OrderByTerm("file_no_2", table, direction))
                query.OrderByTerms.Add(New OrderByTerm("file_no_3", table, direction))
            End If

            'If criteria.SortColumn = "ncn_year" Then
            '    query.OrderByTerms.Add(New OrderByTerm("ncn_code1", table, direction))
            '    query.OrderByTerms.Add(New OrderByTerm("ncn_citation", table, direction))
            '    query.OrderByTerms.Add(New OrderByTerm("ncn_code2", table, direction))
            'End If

            Dim renderer As New SqlServerRenderer
            Dim sqlRowCount As String = renderer.RenderRowCount(query)

            resultsCount = CInt(SqlHelper.ExecuteScalar(Configuration.SqlConnectionString(), CommandType.Text, sqlRowCount, parameters))
            Dim sql As String = New SqlServerRenderer().RenderPage(page - 1, size, resultsCount, query)
            Dim ds As DataSet = SqlHelper.ExecuteDataset(Configuration.SqlConnectionString(), CommandType.Text, sql, parameters)

            Return ds
        End Function

        Public Function Add(ByVal decision As DecisionEntity) As Integer

            Dim parameters() As SqlParameter = {New SqlParameter("@Id", SqlDbType.Int),
                                                New SqlParameter("@Is_published", SqlDbType.Bit),
                                                New SqlParameter("@File_no_1", SqlDbType.VarChar, 5),
                                                New SqlParameter("@File_no_2", SqlDbType.VarChar, 5),
                                                New SqlParameter("@Decision_datetime", SqlDbType.DateTime),
                                                New SqlParameter("@Claimants", SqlDbType.NText),
                                                New SqlParameter("@Respondent", SqlDbType.NText),
                                                New SqlParameter("@Main_subcategory_id", SqlDbType.Int),
                                                New SqlParameter("@Sec_subcategory_id", SqlDbType.Int),
                                                New SqlParameter("@Headnote_summary", SqlDbType.NText),
                                                New SqlParameter("@File_no_3", SqlDbType.VarChar, 4),
                                                New SqlParameter("@Reported_no_1", SqlDbType.VarChar, 3),
                                                New SqlParameter("@Reported_no_2", SqlDbType.VarChar, 3),
                                                New SqlParameter("@Reported_no_3", SqlDbType.VarChar, 2),
                                                New SqlParameter("@NCN_year", SqlDbType.Int),
                                                New SqlParameter("@NCN_code1", SqlDbType.VarChar, 20),
                                                New SqlParameter("@NCN_citation", SqlDbType.Int),
                                                New SqlParameter("@NCN_code2", SqlDbType.VarChar, 20)
                                                }

            parameters(0).Direction = ParameterDirection.Output
            parameters(1).Value = decision.Is_published
            parameters(2).Value = decision.File_no_1
            parameters(3).Value = decision.File_no_2

            If decision.Decision_datetime = DateTime.MinValue Then
                parameters(4).Value = DBNull.Value
            Else
                parameters(4).Value = decision.Decision_datetime
            End If

            parameters(5).Value = decision.Claimants
            parameters(6).Value = decision.Respondent
            parameters(7).Value = decision.Main_subcategory_id

            If decision.Sec_subcategory_id = -1 Then
                parameters(8).Value = DBNull.Value
            Else
                parameters(8).Value = decision.Sec_subcategory_id
            End If

            parameters(9).Value = decision.Headnote_summary
            parameters(10).Value = decision.File_no_3
            parameters(11).Value = decision.Reported_no_1
            parameters(12).Value = decision.Reported_no_2
            parameters(13).Value = decision.Reported_no_3

            parameters(14).Value = decision.NCNYear
            parameters(15).Value = decision.NCNCode1
            parameters(16).Value = decision.NCNCitation
            parameters(17).Value = decision.NCNCode2


            Dim conn As New SqlConnection(Configuration.SqlConnectionString)
            conn.Open()
            Dim trans As SqlTransaction = conn.BeginTransaction

            Try
                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "spAddJudgment", parameters)

                decision.Id = Convert.ToInt32(parameters(0).Value)

                Me.DeleteCommissionerJudgmentMap(trans, decision.Id)
                If Not decision.CommissionerIDList Is Nothing Then
                    Me.AddCommissionerJudgmentMap(trans, decision)
                End If

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                'System.Web.HttpContext.Current.Response.Write(ex.ToString)
                Throw ex
            Finally
                trans = Nothing
                conn.Close()
                conn = Nothing
            End Try

            Return Convert.ToInt32(parameters(0).Value)
        End Function

        Public Sub Update(ByVal decision As DecisionEntity)

            Dim parameters() As SqlParameter = {New SqlParameter("@Id", SqlDbType.Int),
                                                New SqlParameter("@Is_published", SqlDbType.Bit),
                                                New SqlParameter("@File_no_1", SqlDbType.VarChar, 5),
                                                New SqlParameter("@File_no_2", SqlDbType.VarChar, 5),
                                                New SqlParameter("@Decision_datetime", SqlDbType.DateTime),
                                                New SqlParameter("@Claimants", SqlDbType.NText),
                                                New SqlParameter("@Respondent", SqlDbType.NText),
                                                New SqlParameter("@Main_subcategory_id", SqlDbType.Int),
                                                New SqlParameter("@Sec_subcategory_id", SqlDbType.Int),
                                                New SqlParameter("@Headnote_summary", SqlDbType.NText),
                                                New SqlParameter("@File_no_3", SqlDbType.VarChar, 4),
                                                New SqlParameter("@Reported_no_1", SqlDbType.VarChar, 3),
                                                New SqlParameter("@Reported_no_2", SqlDbType.VarChar, 3),
                                                New SqlParameter("@Reported_no_3", SqlDbType.VarChar, 2),
                                                New SqlParameter("@NCN_year", SqlDbType.Int),
                                                New SqlParameter("@NCN_code1", SqlDbType.VarChar, 20),
                                                New SqlParameter("@NCN_citation", SqlDbType.Int),
                                                New SqlParameter("@NCN_code2", SqlDbType.VarChar, 20)
                                                }

            parameters(0).Value = decision.Id
            parameters(1).Value = decision.Is_published
            parameters(2).Value = decision.File_no_1
            parameters(3).Value = decision.File_no_2

            If decision.Decision_datetime = DateTime.MinValue Then
                parameters(4).Value = DBNull.Value
            Else
                parameters(4).Value = decision.Decision_datetime
            End If

            parameters(5).Value = decision.Claimants
            parameters(6).Value = decision.Respondent
            parameters(7).Value = decision.Main_subcategory_id

            If decision.Sec_subcategory_id = -1 Then
                parameters(8).Value = DBNull.Value
            Else
                parameters(8).Value = decision.Sec_subcategory_id
            End If

            parameters(9).Value = decision.Headnote_summary
            parameters(10).Value = decision.File_no_3
            parameters(11).Value = decision.Reported_no_1
            parameters(12).Value = decision.Reported_no_2
            parameters(13).Value = decision.Reported_no_3

            parameters(14).Value = (IIf(decision.NCNYear = -1, DBNull.Value, decision.NCNYear))
            parameters(15).Value = decision.NCNCode1
            parameters(16).Value = (IIf(decision.NCNCitation = -1, DBNull.Value, decision.NCNCitation))
            parameters(17).Value = decision.NCNCode2

            Dim conn As New SqlConnection(Configuration.SqlConnectionString)
            conn.Open()
            Dim trans As SqlTransaction = conn.BeginTransaction

            Try
                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "spUpdateJudgment", parameters)

                Me.DeleteCommissionerJudgmentMap(trans, decision.Id)
                If Not decision.CommissionerIDList Is Nothing Then
                    Me.AddCommissionerJudgmentMap(trans, decision)
                End If

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                'System.Web.HttpContext.Current.Response.Write(ex.ToString)
                Throw ex
            Finally
                trans = Nothing
                conn.Close()
                conn = Nothing
            End Try

        End Sub

        ' Method Requires transaction
        Private Sub AddCommissionerJudgmentMap(ByVal trans As SqlTransaction, ByVal decision As DecisionEntity)
            Dim parameters() As SqlParameter = {New SqlParameter("@CommissionerID", SqlDbType.Int),
                                                New SqlParameter("@JudgmentID", SqlDbType.Int)}

            Dim en As IEnumerator = decision.CommissionerIDList.GetEnumerator
            While en.MoveNext
                parameters(0).Value = Convert.ToInt32(en.Current)
                parameters(1).Value = decision.Id
                SqlHelper.ExecuteNonQuery(trans,
                                            CommandType.StoredProcedure,
                                            "spAddCommissionerJudgmentMap",
                                            parameters)
            End While
        End Sub

        ' Method Requires transaction
        Private Sub DeleteCommissionerJudgmentMap(ByVal trans As SqlTransaction, ByVal judgmentId As Integer)
            Dim parameters() As SqlParameter = {New SqlParameter("@JudgmentID", SqlDbType.Int)}
            parameters(0).Value = judgmentId
            SqlHelper.ExecuteNonQuery(trans,
                                        CommandType.StoredProcedure,
                                        "spDeleteCommissionerJudgmentMap",
                                        parameters)
        End Sub

    End Class
End Namespace

