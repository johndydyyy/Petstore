Imports System.ComponentModel
Imports System.IO
Imports System.Xml.Serialization




Public Class Inventory

    Private colCountActual As Integer
    Private ImagePathGlobal As String
    Private ProductDetailsCollection As BindingList(Of Catalog)


    Private Sub Inventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sd As New SD()
        sd.DeserializeFromXml("Catalog.xml")
        ProductDetailsCollection = New BindingList(Of Catalog)()
        BindingSource1.DataSource = ProductDetailsCollection
        DataGridView1.DataSource = BindingSource1
        DataGridView1.AutoGenerateColumns = True

        SetColumnPriority()
        UnbindControls()
        ApplyBindings()
    End Sub


    Private Sub SetColumnPriority()
        Dim columns As DataGridViewColumnCollection = DataGridView1.Columns
        columns("ProductID").DisplayIndex = 0
        columns("ProductName").DisplayIndex = 1
        columns("ProductBrand").DisplayIndex = 2
        columns("Size").DisplayIndex = 3
        columns("Flavor").DisplayIndex = 4
        columns("AnimalType").DisplayIndex = 5
        columns("Price").DisplayIndex = 6
        columns("Category").DisplayIndex = 7
        columns("Quantity").DisplayIndex = 8
    End Sub


    Private Sub ApplyBindings()
        UnbindControls()
        TextBox1.DataBindings.Add("Text", BindingSource1, "ProductID", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox2.DataBindings.Add("Text", BindingSource1, "ProductName", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox3.DataBindings.Add("Text", BindingSource1, "ProductBrand", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox4.DataBindings.Add("Text", BindingSource1, "Size", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox5.DataBindings.Add("Text", BindingSource1, "Flavor", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox6.DataBindings.Add("Text", BindingSource1, "AnimalType", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox7.DataBindings.Add("Text", BindingSource1, "Price", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox8.DataBindings.Add("Text", BindingSource1, "Category", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox9.DataBindings.Add("Text", BindingSource1, "Quantity", True, DataSourceUpdateMode.OnPropertyChanged)

    End Sub


    Private Sub UnbindControls()
        TextBox1.DataBindings.Clear()
        TextBox2.DataBindings.Clear()
        TextBox3.DataBindings.Clear()
        TextBox4.DataBindings.Clear()
        TextBox5.DataBindings.Clear()
        TextBox6.DataBindings.Clear()
        TextBox7.DataBindings.Clear()
        TextBox8.DataBindings.Clear()
        TextBox9.DataBindings.Clear()
    End Sub


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Try
            Dim sd As New SD()
            sd.SerializeToXml("Catalog.xml", ProductDetailsCollection)
            MessageBox.Show("Data saved successfully.")
        Catch ex As Exception
            MessageBox.Show("An error occurred while serializing data: " & ex.Message)
        End Try
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        UnbindControls()
        MessageBox.Show("Input products.")
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox6.Focus()
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim items As New Catalog With {
            .ProductID = Integer.Parse(TextBox1.Text),
            .ProductName = TextBox2.Text,
            .ProductBrand = TextBox3.Text,
            .Size = TextBox4.Text,
            .Flavor = TextBox5.Text,
            .AnimalType = TextBox6.Text,
            .Price = Integer.Parse(TextBox7.Text),
            .Category = TextBox8.Text,
            .Quantity = Decimal.Parse(TextBox9.Text)
        }
        ProductDetailsCollection.Add(items)
        MessageBox.Show("Data Saved.")
        Button1.Text = "Serialize Records {ProductDetailsCollection.Count - colCountActual}"
        BindingSource1.ResetBindings(False)
        ApplyBindings()
    End Sub


    Private Sub Button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button6.Click
        Try
            Dim sd As New SD()
            ProductDetailsCollection = sd.DeserializeFromXml("Catalog.xml")
            BindingSource1.DataSource = ProductDetailsCollection
            DataGridView1.Refresh()
            MessageBox.Show("Loading Inventory...")
        Catch ex As Exception
            MessageBox.Show("An error occurred while deserializing data: " & ex.Message)
        End Try
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to remove all records?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            ProductDetailsCollection.Clear()
            ProductDetailsCollection.ResetBindings()
            Button1.Text = "Serialize Records 0"
            MessageBox.Show("All records have been removed.")
        End If
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Welcome.Show()
        Me.Hide()
    End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Application.Exit()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try

            Dim productID As Integer = Integer.Parse(TextBox10.Text)
            Dim productToRemove As Catalog = ProductDetailsCollection.FirstOrDefault(Function(p) p.ProductID = productID)
            If productToRemove IsNot Nothing Then
                ProductDetailsCollection.Remove(productToRemove)
                BindingSource1.ResetBindings(False)
                MessageBox.Show("Product removed successfully.")
            Else
                MessageBox.Show("Product not found.")
            End If

            TextBox10.Clear()

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
