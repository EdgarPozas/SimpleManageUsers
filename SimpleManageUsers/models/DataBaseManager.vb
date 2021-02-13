Imports System.Data.SQLite

Public Class DataBaseManager

    Private Connection As SQLiteConnection
    Private Shared Instance As DataBaseManager

    Private Sub New()
    End Sub

    Public Shared Function GetInstance()

        If Instance Is Nothing Then
            Instance = New DataBaseManager()
        End If

        Return Instance

    End Function

    Public Sub Connect(ByVal Path As String)
        Connection = New SQLiteConnection($"Data Source={Path};Version=3;")
        Connection.Open()
    End Sub

    Public Sub Close()
        Connection.Close()
    End Sub


    Public Sub Query(ByVal Query_ As String)
        If Connection Is Nothing Then
            Return
        End If

        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Command.CommandText = Query_
        Command.ExecuteNonQuery()

    End Sub

    Public Function getQuery(ByVal Query_ As String) As SQLiteDataReader
        If Connection Is Nothing Then
            Return Nothing
        End If

        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Command.CommandText = Query_

        Dim Reader As SQLiteDataReader = Command.ExecuteReader()

        Return Reader
    End Function

End Class
