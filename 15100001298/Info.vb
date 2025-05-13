Public Class Info
    Public Property FirstName As String
    Public Property LastName As String
    Public Property Email As String
    Public Property Address As String
    Public Property PhoneNo As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) OrElse String.IsNullOrWhiteSpace(TextBox4.Text) OrElse String.IsNullOrWhiteSpace(TextBox5.Text) Then
            MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Me.FirstName = TextBox1.Text
        Me.LastName = TextBox2.Text
        Me.Email = TextBox3.Text
        Me.Address = TextBox4.Text
        Me.PhoneNo = TextBox5.Text
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Info_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Store.Show()
        Me.Hide()
    End Sub
End Class
