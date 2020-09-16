Namespace Entity
    Public Class SearchCriteria
        ' Properties
        Public Property Respondent As String
            Get
                Return Me._respondent
            End Get
            Set(ByVal Value As String)
                Me._respondent = Value
            End Set
        End Property

        Public Property NCNCode1 As String
            Get
                Return Me._ncnCode1
            End Get
            Set(ByVal Value As String)
                Me._ncnCode1 = Value
            End Set
        End Property

        Public Property NCNCode2 As String
            Get
                Return Me._ncnCode2
            End Get
            Set(ByVal Value As String)
                Me._ncnCode2 = Value
            End Set
        End Property

        Public Property NCNYear As Integer
            Get
                Return Me._ncnYear
            End Get
            Set(ByVal Value As Integer)
                Me._ncnYear = Value
            End Set
        End Property

        Public Property NCNCitaion As Integer
            Get
                Return Me._ncnCitation
            End Get
            Set(ByVal Value As Integer)
                Me._ncnCitation = Value
            End Set
        End Property

        Public Property Notes As String
            Get
                Return Me._notes
            End Get
            Set(ByVal Value As String)
                Me._notes = Value
            End Set
        End Property

        Public Property SortColumn As String
            Get
                Return Me._sortColumn
            End Get
            Set(ByVal Value As String)
                Me._sortColumn = Value
            End Set
        End Property

        Public Property SortDirection As String
            Get
                Return Me._sortDirection
            End Get
            Set(ByVal Value As String)
                Me._sortDirection = Value
            End Set
        End Property

        Public Property CategoryID As Integer
            Get
                Return Me._categoryId
            End Get
            Set(ByVal Value As Integer)
                Me._categoryId = Value
            End Set
        End Property

        Public Property SubCategoryID As Integer
            Get
                Return Me._subCategoryId
            End Get
            Set(ByVal Value As Integer)
                Me._subCategoryId = Value
            End Set
        End Property

        Public Property DecisionDate As DateTime
            Get
                Return Me._decisionDate
            End Get
            Set(ByVal Value As DateTime)
                Me._decisionDate = Value
            End Set
        End Property

        Public Property FromDate As DateTime
            Get
                Return Me._fromDate
            End Get
            Set(ByVal Value As DateTime)
                Me._fromDate = Value
            End Set
        End Property

        Public Property ToDate As DateTime
            Get
                Return Me._toDate
            End Get
            Set(ByVal Value As DateTime)
                Me._toDate = Value
            End Set
        End Property

        Public Property CreatedDate As DateTime
            Get
                Return Me._createdDate
            End Get
            Set(ByVal Value As DateTime)
                Me._createdDate = Value
            End Set
        End Property

        Public Property FromDateAdded As DateTime
            Get
                Return Me._fromDateAdded
            End Get
            Set(ByVal Value As DateTime)
                Me._fromDateAdded = Value
            End Set
        End Property

        Public Property ToDateAdded As DateTime
            Get
                Return Me._toDateAdded
            End Get
            Set(ByVal Value As DateTime)
                Me._toDateAdded = Value
            End Set
        End Property

        Public Property Claimant As String
            Get
                Return Me._claimant
            End Get
            Set(ByVal Value As String)
                Me._claimant = Value
            End Set
        End Property

        Public Property Year As String
            Get
                Return Me._year
            End Get
            Set(ByVal Value As String)
                Me._year = Value
            End Set
        End Property

        Public Property CaseNo As String
            Get
                Return Me._caseNo
            End Get
            Set(ByVal Value As String)
                Me._caseNo = Value
            End Set
        End Property

        Public Property Prefix As String
            Get
                Return Me._prefix
            End Get
            Set(ByVal Value As String)
                Me._prefix = Value
            End Set
        End Property

        Public Property IsPublished As Boolean
            Get
                Return Me._isPublished
            End Get
            Set(ByVal Value As Boolean)
                Me._isPublished = Value
            End Set
        End Property

        Public Property CommissionerID As Integer
            Get
                Return Me._commissionerID
            End Get
            Set(ByVal Value As Integer)
                Me._commissionerID = Value
            End Set
        End Property

        Public Property Reported1 As String
            Get
                Return Me._reported1
            End Get
            Set(ByVal Value As String)
                Me._reported1 = Value
            End Set
        End Property

        Public Property Reported2 As String
            Get
                Return Me._reported2
            End Get
            Set(ByVal Value As String)
                Me._reported2 = Value
            End Set
        End Property

        Public Property Reported3 As String
            Get
                Return Me._reported3
            End Get
            Set(ByVal Value As String)
                Me._reported3 = Value
            End Set
        End Property


        ' Fields
        Private _sortDirection As String
        Private _sortColumn As String
        Private _categoryId As Integer
        Private _subCategoryId As Integer
        Private _decisionDate As DateTime
        Private _fromDate As DateTime
        Private _toDate As DateTime
        Private _createdDate As DateTime
        Private _fromDateAdded As DateTime
        Private _toDateAdded As DateTime
        Private _claimant As String
        Private _year As String
        Private _caseNo As String
        Private _prefix As String
        Private _isPublished As Boolean = False
        Private _commissionerID As Integer
        Private _reported1 As String
        Private _reported2 As String
        Private _reported3 As String
        Private _notes As String
        Private _respondent As String = String.Empty
        Private _ncnYear As Integer
        Private _ncnCode1 As String = String.Empty
        Private _ncnCitation As Integer
        Private _ncnCode2 As String = String.Empty
    End Class
End Namespace
