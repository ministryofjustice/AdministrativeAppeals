Namespace Entity
    Public Class DecisionEntity

        Private _Claimants As System.String = ""
        Private _Created_datetime As System.DateTime = DateTime.MinValue
        Private _Decision_datetime As System.DateTime = DateTime.MinValue
        Private _Decision_type As System.String = ""
        Private _File_no_1 As System.String = ""
        Private _File_no_2 As System.String = ""
        Private _File_no_3 As System.String = ""
        Private _Headnote_summary As System.String = ""
        Private _Id As System.Int32 = 0
        Private _Is_published As System.Boolean = False
        Private _Last_updatedtime As System.DateTime = DateTime.MinValue
        Private _Main_subcategory_id As System.Int32 = 0
        Private _Publication_datetime As System.DateTime = DateTime.MinValue
        Private _Reported_no_1 As System.String = ""
        Private _Reported_no_2 As System.String = ""
        Private _Reported_no_3 As System.String = ""
        Private _Respondent As System.String = ""
        Private _Sec_subcategory_id As System.Int32 = 0
        Private _FileList As Hashtable
        Private _CommissionerIdList As ArrayList

        Private _NCN_Year As Integer
        Private _NCN_Code1 As String
        Private _NCN_Citation As Integer
        Private _NCN_Code2 As String




        Public Property NCNYear() As Integer
            Get
                Return _NCN_Year
            End Get
            Set(ByVal Value As Integer)
                _NCN_Year = Value
            End Set
        End Property

        Public Property NCNCitation() As Integer
            Get
                Return _NCN_Citation
            End Get
            Set(ByVal Value As Integer)
                _NCN_Citation = Value
            End Set
        End Property

        Public Property NCNCode1() As String
            Get
                Return _NCN_Code1
            End Get
            Set(ByVal Value As String)
                _NCN_Code1 = Value
            End Set
        End Property

        Public Property NCNCode2() As String
            Get
                Return _NCN_Code2
            End Get
            Set(ByVal Value As String)
                _NCN_Code2 = Value
            End Set
        End Property


        Public Property FileList() As Hashtable
            Get
                Return _FileList
            End Get
            Set(ByVal Value As Hashtable)
                _FileList = Value
            End Set
        End Property


        Public Property Claimants() As System.String
            Get
                Return Me._Claimants
            End Get
            Set(ByVal Value As System.String)
                Me._Claimants = Value
            End Set
        End Property



        Public Property Created_datetime() As System.DateTime
            Get
                Return Me._Created_datetime
            End Get
            Set(ByVal Value As System.DateTime)
                Me._Created_datetime = Value
            End Set
        End Property



        Public Property Decision_datetime() As System.DateTime
            Get
                Return Me._Decision_datetime
            End Get
            Set(ByVal Value As System.DateTime)
                Me._Decision_datetime = Value
            End Set
        End Property



        Public Property Decision_type() As System.String
            Get
                Return Me._Decision_type
            End Get
            Set(ByVal Value As System.String)
                Me._Decision_type = Value
            End Set
        End Property



        Public Property File_no_1() As System.String
            Get
                Return Me._File_no_1
            End Get
            Set(ByVal Value As System.String)
                Me._File_no_1 = Value
            End Set
        End Property



        Public Property File_no_2() As System.String
            Get
                Return Me._File_no_2
            End Get
            Set(ByVal Value As System.String)
                Me._File_no_2 = Value
            End Set
        End Property



        Public Property File_no_3() As System.String
            Get
                Return Me._File_no_3
            End Get
            Set(ByVal Value As System.String)
                Me._File_no_3 = Value
            End Set
        End Property



        Public Property Headnote_summary() As System.String
            Get
                Return Me._Headnote_summary
            End Get
            Set(ByVal Value As System.String)
                Me._Headnote_summary = Value
            End Set
        End Property



        Public Property Id() As System.Int32
            Get
                Return Me._Id
            End Get
            Set(ByVal Value As System.Int32)
                Me._Id = Value
            End Set
        End Property



        Public Property Is_published() As System.Boolean
            Get
                Return Me._Is_published
            End Get
            Set(ByVal Value As System.Boolean)
                Me._Is_published = Value
            End Set
        End Property



        Public Property Last_updatedtime() As System.DateTime
            Get
                Return Me._Last_updatedtime
            End Get
            Set(ByVal Value As System.DateTime)
                Me._Last_updatedtime = Value
            End Set
        End Property



        Public Property Main_subcategory_id() As System.Int32
            Get
                Return Me._Main_subcategory_id
            End Get
            Set(ByVal Value As System.Int32)
                Me._Main_subcategory_id = Value
            End Set
        End Property



        Public Property Publication_datetime() As System.DateTime
            Get
                Return Me._Publication_datetime
            End Get
            Set(ByVal Value As System.DateTime)
                Me._Publication_datetime = Value
            End Set
        End Property



        Public Property Reported_no_1() As System.String
            Get
                Return Me._Reported_no_1
            End Get
            Set(ByVal Value As System.String)
                Me._Reported_no_1 = Value
            End Set
        End Property



        Public Property Reported_no_2() As System.String
            Get
                Return Me._Reported_no_2
            End Get
            Set(ByVal Value As System.String)
                Me._Reported_no_2 = Value
            End Set
        End Property



        Public Property Reported_no_3() As System.String
            Get
                Return Me._Reported_no_3
            End Get
            Set(ByVal Value As System.String)
                Me._Reported_no_3 = Value
            End Set
        End Property



        Public Property Respondent() As System.String
            Get
                Return Me._Respondent
            End Get
            Set(ByVal Value As System.String)
                Me._Respondent = Value
            End Set
        End Property



        Public Property Sec_subcategory_id() As System.Int32
            Get
                Return Me._Sec_subcategory_id
            End Get
            Set(ByVal Value As System.Int32)
                Me._Sec_subcategory_id = Value
            End Set
        End Property

        Public Property CommissionerIDList() As ArrayList
            Get
                Return Me._CommissionerIdList
            End Get
            Set(ByVal Value As ArrayList)
                Me._CommissionerIdList = Value
            End Set
        End Property

    End Class
End Namespace


