Imports System.ComponentModel
Imports System.IO
Imports System.Xml.Serialization

Module Module1
    Public ProductDetailsCollection As New BindingList(Of Catalog)()
    Private Const filePath As String = "Catalog.xml"

    Public Sub SerializeToXml()
        Try
            Dim serializer As New XmlSerializer(GetType(BindingList(Of Catalog)))
            Using fs As New FileStream(filePath, FileMode.Create)
                serializer.Serialize(fs, ProductDetailsCollection)
            End Using
            MessageBox.Show("Data saved to Catalog.xml successfully.")
        Catch ex As Exception
            MessageBox.Show("An error occurred while saving data: " & ex.Message)
        End Try
    End Sub

    Public Sub DeserializeFromXml()
        Try
            Dim serializer As New XmlSerializer(GetType(BindingList(Of Catalog)))
            If File.Exists(filePath) Then
                Using fs As New FileStream(filePath, FileMode.Open)
                    ProductDetailsCollection = CType(serializer.Deserialize(fs), BindingList(Of Catalog))
                End Using
                MessageBox.Show("Data loaded from Catalog.xml successfully. Total records: {ProductDetailsCollection.Count}")
            Else
                MessageBox.Show("No data file found, starting with an empty collection.")
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading data: " & ex.Message)
        End Try
    End Sub

End Module
