Public Class WindowStart

    Private UserList As List(Of User)
    Private UserSelected As User
    Private TryUpdate As Boolean

    Private Sub WindowStart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DataBase As DataBaseManager = DataBaseManager.GetInstance()
        DataBase.Connect($"C:\Users\LAP-INN-01\Desktop\Independientes\SimpleManageUsers\SimpleManageUsers\models\database.db")
        TryUpdate = False
        UpdateTable()
    End Sub

    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        ClearFields()
    End Sub

    Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click

        If TryUpdate Then

            If UserSelected Is Nothing Then
                Return
            End If

            UserSelected.Name = TextName.Text
            UserSelected.LastName = TextLastName.Text

            UserSelected.Update()
        Else

            Dim User As User = New User(-1, TextName.Text, TextLastName.Text)
            User.Save()

        End If

        ClearFields()
        UpdateTable()

    End Sub

    Private Sub ClearFields()
        TextName.Text = ""
        TextLastName.Text = ""

        ButtonClear.Text = "Clear fields"
        ButtonNew.Text = "New user"
        ButtonDelete.Visible = False

        TryUpdate = False
    End Sub

    Private Sub DataGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGrid.CellClick

        If e.RowIndex >= UserList.Count Then
            Return
        End If

        Me.UserSelected = UserList(e.RowIndex)
        ButtonClear.Text = "Cancel"
        ButtonNew.Text = "Update"

        TextName.Text = Me.UserSelected.Name
        TextLastName.Text = Me.UserSelected.LastName
        ButtonDelete.Visible = True

        TryUpdate = True

    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Me.UserSelected.Remove()
        ClearFields()
        UpdateTable()
    End Sub

    Private Sub UpdateTable()

        Me.UserList = User.GetAll()

        DataGrid.Columns.Clear()
        DataGrid.Rows.Clear()

        DataGrid.Columns.Add("UserId", "UserId")
        DataGrid.Columns.Add("Name", "Name")
        DataGrid.Columns.Add("LastName", "LastName")

        For Each user As User In UserList
            DataGrid.Rows.Add(user.UserId, user.Name, user.LastName)
        Next

    End Sub

End Class
