Imports System.IO

Public Class Form1
    Private toDoItems As New List(Of String)
    Private filePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "todolist.txt") ' Default file path
    Private isEditing As Boolean = False


    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Dim selectedIndex As Integer = lstTasks.SelectedIndex

        If selectedIndex <> -1 Then
            isEditing = True  'Set editing mode to True
            txtTask.Text = lstTasks.Items(selectedIndex).ToString()

            btnAdd.Text = "Save"  'Change "Add" button to "Save"
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Load tasks from the saved file path (if it exists)
        If File.Exists(filePath) Then
            toDoItems.Clear()
            toDoItems.AddRange(File.ReadAllLines(filePath))
            lstTasks.Items.AddRange(toDoItems.ToArray())
        Else
            ' Initialize with some tasks (optional) - only if the file doesn't exist
            toDoItems.Add("Buy groceries")
            toDoItems.Add("Walk the dog")
            toDoItems.Add("Finish project report")
            lstTasks.Items.AddRange(toDoItems.ToArray())
        End If

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim newTask As String = txtTask.Text.Trim()
        Dim selectedIndex As Integer = lstTasks.SelectedIndex

        If newTask <> "" Then
            If isEditing Then
                ' Update existing task
                If selectedIndex >= 0 AndAlso selectedIndex < toDoItems.Count Then
                    toDoItems(selectedIndex) = newTask
                    lstTasks.Items(selectedIndex) = newTask
                End If
                isEditing = False  'Reset editing mode
                btnAdd.Text = "Add"  'Change back to "Add"
            Else
                ' Add new task
                toDoItems.Add(newTask)
                lstTasks.Items.Add(newTask)
            End If
            txtTask.Clear()
            lstTasks.SelectedIndex = -1 ' Deselect item after editing or adding
        End If

    End Sub

    Private Sub lstTasks_DoubleClick(sender As Object, e As EventArgs) Handles lstTasks.DoubleClick

        Dim selectedIndex As Integer = lstTasks.SelectedIndex

        If selectedIndex <> -1 Then
            isEditing = True  'Set editing mode to True
            txtTask.Text = lstTasks.Items(selectedIndex).ToString()

            btnAdd.Text = "Save"  'Change "Add" button to "Save"
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim selectedIndex As Integer = lstTasks.SelectedIndex

        If selectedIndex <> -1 Then 'Check if an item is selected
            toDoItems.RemoveAt(selectedIndex)
            lstTasks.Items.RemoveAt(selectedIndex)
            txtTask.Clear() ' Clear the textbox after deleting
        End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        saveFileDialog.Title = "Save To-Do List"

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = saveFileDialog.FileName

            Using writer As New StreamWriter(filePath)
                For Each task As String In toDoItems
                    writer.WriteLine(task)
                Next
            End Using

            MessageBox.Show("To-Do list saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub
End Class
