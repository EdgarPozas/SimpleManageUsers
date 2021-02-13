Imports System.Data.SQLite

Public Class User

    Public UserId As Int32
    Public Name As String
    Public LastName As String

    Public Sub New(userId As Integer, name As String, lastName As String)
        Me.UserId = userId
        Me.Name = name
        Me.LastName = lastName
    End Sub

    Public Sub Refresh()
        Dim DataBase As DataBaseManager = DataBaseManager.GetInstance()
        Dim Reader As SQLiteDataReader = DataBase.getQuery($"select * from users where user_id={UserId }")

        While Reader.Read()
            Me.UserId = Reader.GetInt32(0)
            Me.Name = Reader.GetString(1)
            Me.LastName = Reader.GetString(2)
        End While

    End Sub


    Public Shared Function GetAll() As List(Of User)
        Dim DataBase As DataBaseManager = DataBaseManager.GetInstance()
        Dim Reader As SQLiteDataReader = DataBase.getQuery($"select * from users")
        Dim UserList As List(Of User) = New List(Of User)

        While Reader.Read()
            UserList.Add(New User(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2)))
        End While

        Return UserList

    End Function

    Public Sub Save()
        Dim DataBase As DataBaseManager = DataBaseManager.GetInstance()
        DataBase.Query($"insert into users(name,lastname) values('{Name}','{LastName}')")
    End Sub

    Public Sub Update()
        Dim DataBase As DataBaseManager = DataBaseManager.GetInstance()
        DataBase.Query($"update users set name='{Name}',lastname='{LastName}' where user_id={UserId}")
    End Sub

    Public Sub Remove()
        Dim DataBase As DataBaseManager = DataBaseManager.GetInstance()
        DataBase.Query($"delete from users where user_id={UserId}")
    End Sub

End Class
