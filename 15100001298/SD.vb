Imports System.IO
Imports System.Xml.Serialization
Imports System.ComponentModel

Public Class SD

    Public Sub SerializeToXml(ByVal filePath As String, ByVal ProductDetailsCollection As BindingList(Of Catalog))
        Try
            Dim serializer As New XmlSerializer(GetType(BindingList(Of Catalog)))
            Using fs As New FileStream(filePath, FileMode.Create)
                serializer.Serialize(fs, ProductDetailsCollection)
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred during serialization: " & ex.Message)
        End Try
    End Sub



    Public Function DeserializeFromXml(ByVal filePath As String) As BindingList(Of Catalog)
        Try
            Dim serializer As New XmlSerializer(GetType(BindingList(Of Catalog)))
            If File.Exists(filePath) Then
                Using fs As New FileStream(filePath, FileMode.Open)
                    Return CType(serializer.Deserialize(fs), BindingList(Of Catalog))
                End Using
            Else
                Return New BindingList(Of Catalog)()
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred during deserialization: " & ex.Message)
            Return New BindingList(Of Catalog)()
        End Try
    End Function

End Class
