Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.IO
Imports System.Linq

Public Class Store

    Public Shared ProductDetailsCollection As BindingList(Of Catalog)
    Private BindingSource1 As New BindingSource()
    Private TransactionList As New BindingList(Of Transaction)()
    Public Property UserFirstName As String
    Public Property UserLastName As String
    Public Property UserEmail As String
    Public Property UserAddress As String
    Public Property UserPhoneNo As String


    Private Sub Store_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadProductDetails()
        LoadTransactionHistory()
        ComboBox2.Items.Add("All")
        ComboBox2.Items.Add("food")
        ComboBox2.Items.Add("grooming")
        ComboBox2.Items.Add("toys")
        ComboBox2.SelectedIndex = 0

        ComboBox1.Items.Add("Cash")
        ComboBox1.Items.Add("PayMaya")
        ComboBox1.Items.Add("Gcash")
        ComboBox1.SelectedIndex = 0

        ConfigureTransactionGrid()
    End Sub


    Private Sub LoadProductDetails()
        Try
            If File.Exists("Catalog.xml") Then
                Dim sd As New SD()
                ProductDetailsCollection = sd.DeserializeFromXml("Catalog.xml")
            Else
                ProductDetailsCollection = New BindingList(Of Catalog)()
            End If
            BindingSource1.DataSource = ProductDetailsCollection
            DataGridView1.DataSource = BindingSource1
            DataGridView1.AutoGenerateColumns = True

            SetColumnOrder()
            DataGridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading product details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub LoadTransactionHistory()
        Try
            If File.Exists("Transactions.xml") Then
                Dim serializer As New XmlSerializer(GetType(BindingList(Of Transaction)))
                Using reader As New StreamReader("Transactions.xml")
                    TransactionList = DirectCast(serializer.Deserialize(reader), BindingList(Of Transaction))
                End Using
            Else
                TransactionList = New BindingList(Of Transaction)()
            End If
            DataGridView2.DataSource = TransactionList
            DataGridView2.AutoGenerateColumns = False
            DataGridView2.Refresh()
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading transaction history: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SetColumnOrder()
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

        columns("ProductID").Width = 81
        columns("ProductName").Width = 150
        columns("ProductBrand").Width = 130
        columns("Size").Width = 75
        columns("Flavor").Width = 90
        columns("AnimalType").Width = 90
        columns("Price").Width = 90
        columns("Category").Width = 93
        columns("Quantity").Width = 80

        For Each column As DataGridViewColumn In columns
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next
        DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Sub


    Private Sub SaveTransactionData()
        Try
            Dim serializer As New XmlSerializer(GetType(BindingList(Of Transaction)))
            Using writer As New StreamWriter("Transactions.xml")
                serializer.Serialize(writer, TransactionList)
            End Using
            MessageBox.Show("Transaction data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error saving transaction data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureTransactionGrid()
        DataGridView2.Columns.Clear()

        Dim colProductName As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "ProductName",
            .HeaderText = "Product Name"
        }
        colProductName.Width = 125

        Dim colQuantity As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "QuantityPurchased",
            .HeaderText = "Quantity"
        }
        colQuantity.Width = 70

        Dim colTotalPrice As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "TotalPrice",
            .HeaderText = "Total Price"
        }
        colTotalPrice.Width = 84

        Dim colReceivedAmount As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "ReceivedAmount",
            .HeaderText = "Received Amount"
        }
        colReceivedAmount.Width = 88

        Dim colChange As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "Change",
            .HeaderText = "Change"
        }
        colChange.Width = 88

        Dim colPaymentMethod As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "PaymentMethod",
            .HeaderText = "Payment Method"
        }
        colPaymentMethod.Width = 90

        Dim colLastName As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "UserLastName",
            .HeaderText = "Last Name"
        }
        colLastName.Width = 90

        Dim colPhoneNo As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "UserPhoneNo",
            .HeaderText = "Phone No."
        }
        colPhoneNo.Width = 115

        Dim colEmail As New DataGridViewTextBoxColumn With {
            .DataPropertyName = "UserEmail",
            .HeaderText = "Email"
        }
        colEmail.Width = 129

        DataGridView2.Columns.Add(colProductName)
        DataGridView2.Columns.Add(colQuantity)
        DataGridView2.Columns.Add(colTotalPrice)
        DataGridView2.Columns.Add(colReceivedAmount)
        DataGridView2.Columns.Add(colChange)
        DataGridView2.Columns.Add(colPaymentMethod)
        DataGridView2.Columns.Add(colLastName)
        DataGridView2.Columns.Add(colPhoneNo)
        DataGridView2.Columns.Add(colEmail)
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) OrElse _
               String.IsNullOrWhiteSpace(TextBox4.Text) OrElse ComboBox1.SelectedItem Is Nothing Then
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If


            Dim infoForm As New Info()
            If infoForm.ShowDialog() = DialogResult.OK Then
                UserFirstName = infoForm.FirstName
                UserLastName = infoForm.LastName
                UserEmail = infoForm.Email
                UserAddress = infoForm.Address
                UserPhoneNo = infoForm.PhoneNo
            End If


            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MessageBox.Show("Please enter a Product ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim productID As String = TextBox1.Text
            Dim quantityToBuy As Integer
            Dim cashAvailable As Decimal


            If Not Integer.TryParse(TextBox3.Text, quantityToBuy) OrElse quantityToBuy <= 0 Then
                MessageBox.Show("Please enter a valid quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If


            If Not Decimal.TryParse(TextBox4.Text, cashAvailable) OrElse cashAvailable <= 0 Then
                MessageBox.Show("Please enter a valid cash amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If


            Dim selectedProduct As Catalog = ProductDetailsCollection.FirstOrDefault(Function(p) p.ProductID = productID)


            If selectedProduct Is Nothing Then
                MessageBox.Show("Product not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If


            If selectedProduct.Quantity < quantityToBuy Then
                MessageBox.Show("Insufficient stock for this product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If


            Dim totalPrice As Decimal = selectedProduct.Price * quantityToBuy


            If ComboBox1.SelectedItem.ToString() = "Gcash" OrElse ComboBox1.SelectedItem.ToString() = "PayMaya" Then
                totalPrice -= totalPrice * 0.15D
            End If


            If cashAvailable < totalPrice Then
                MessageBox.Show("Insufficient cash.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If


            selectedProduct.Quantity -= quantityToBuy
            Dim remainingCash As Decimal = cashAvailable - totalPrice
            DataGridView1.Refresh()


            Dim paymentMethod As String = ComboBox1.SelectedItem.ToString()


            Dim newTransaction As New Transaction With {
                .ProductName = selectedProduct.ProductName,
                .QuantityPurchased = quantityToBuy,
                .TotalPrice = totalPrice,
                .Change = remainingCash,
                .ReceivedAmount = cashAvailable,
                .UserFirstName = UserFirstName,
                .UserLastName = UserLastName,
                .UserEmail = UserEmail,
                .UserAddress = UserAddress,
                .UserPhoneNo = UserPhoneNo,
                .PaymentMethod = paymentMethod
            }

            TransactionList.Add(newTransaction)
            DataGridView2.Refresh()


            Dim receipt As String = "Product: " & selectedProduct.ProductName & vbCrLf & _
                        vbCrLf &
                        "Quantity: " & quantityToBuy & vbCrLf & _
                        vbCrLf &
                        "Price (Each): ₱" & selectedProduct.Price.ToString("0.00") & vbCrLf & _
                        vbCrLf &
                        "Total Price: ₱" & totalPrice.ToString("0.00") & vbCrLf & _
                        vbCrLf &
                        "Received Amount: ₱" & cashAvailable.ToString("0.00") & vbCrLf & _
                        vbCrLf &
                        "Change: ₱" & remainingCash.ToString("0.00") & vbCrLf & _
                        vbCrLf &
                        "Payment Method: " & paymentMethod & vbCrLf & _
                        vbCrLf &
                        "Customer: " & UserFirstName & " " & UserLastName & vbCrLf & _
                        vbCrLf &
                        "Phone: " & UserPhoneNo & vbCrLf & _
                        vbCrLf &
                        "Email: " & UserEmail & vbCrLf & _
                        vbCrLf &
                        vbCrLf &
                        "Thank you for your purchase!"

            MessageBox.Show(receipt, "Receipt")



            TextBox1.Clear()
            TextBox3.Clear()
            TextBox4.Clear()

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to save the product details?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Dim serializer As New XmlSerializer(GetType(BindingList(Of Catalog)))
                Using writer As New StreamWriter("Catalog.xml")
                    serializer.Serialize(writer, ProductDetailsCollection)
                End Using
                MessageBox.Show("Saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Save operation canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Welcome.Show()
        Me.Hide()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Application.Exit()
    End Sub


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim serializer As New XmlSerializer(GetType(BindingList(Of Transaction)))
            Using writer As New StreamWriter("Transactions.xml")
                serializer.Serialize(writer, TransactionList)
            End Using
            MessageBox.Show("Transactions saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error saving transaction data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to clear the transaction records?", "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question)


            If result = DialogResult.Yes Then
                TransactionList.Clear()
                DataGridView2.Refresh()
                MessageBox.Show("Transaction records cleared.", "Clear Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                MessageBox.Show("Action canceled.", "Clear Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("No Records found: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            Dim selectedCategory As String = ComboBox2.SelectedItem.ToString()
            If Not String.IsNullOrWhiteSpace(selectedCategory) Then
                If selectedCategory = "All" Then
                    BindingSource1.DataSource = ProductDetailsCollection
                Else
                    Dim filteredList = From product In ProductDetailsCollection
                                       Where product.Category = selectedCategory
                                       Select product
                    BindingSource1.DataSource = filteredList.ToList()
                    If filteredList.Count() = 0 Then
                        MessageBox.Show("Error 404.", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
                DataGridView1.DataSource = BindingSource1
                DataGridView1.Refresh()
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while sorting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try

            If ProductDetailsCollection Is Nothing OrElse ProductDetailsCollection.Count = 0 Then
                MessageBox.Show("No products to search.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            Dim searchTerm As String = TextBox2.Text.ToLower().Trim()
            If String.IsNullOrEmpty(searchTerm) Then
                MessageBox.Show("Error 404.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If


            Dim searchResults = From item In ProductDetailsCollection
                                Where (If(item.ProductName, "").ToLower().Contains(searchTerm)) OrElse
                                      (If(item.Flavor, "").ToLower().Contains(searchTerm)) OrElse
                                      (If(item.AnimalType, "").ToLower().Contains(searchTerm))
                                Select item


            If searchResults.Any() Then
                BindingSource1.DataSource = searchResults.ToList()
                DataGridView1.DataSource = BindingSource1
                DataGridView1.Refresh()
            Else
                MessageBox.Show("No matching item found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception

            MessageBox.Show("Error 404." & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try

            DataGridView1.DataSource = Nothing
            LoadProductDetails()
            ComboBox2.SelectedIndex = 0
            ComboBox1.SelectedIndex = 0
            TextBox2.Clear()
            DataGridView1.Refresh()

            MessageBox.Show("Refreshed.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

            MessageBox.Show("Error 404" & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim selectedPaymentMethod As String = ComboBox1.SelectedItem.ToString()

        If selectedPaymentMethod = "Gcash" Or selectedPaymentMethod = "PayMaya" Then
            MessageBox.Show("You get a 15% discount when using Gcash or PayMaya!", "Discount Offer", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class


