Imports System.Xml.Serialization
Imports System.IO

<XmlInclude(GetType(Catalog))>
Public Class Catalog

    Public Property ProductID As Integer
    Public Property ProductName As String
    Public Property ProductBrand As String
    Public Property Size As String
    Public Property Flavor As String
    Public Property AnimalType As String
    Public Property Price As Integer
    Public Property Category As String
    Public Property Quantity As Decimal

    Public Sub New()
    End Sub

    Public Sub New(ByVal pro1 As Integer, ByVal pro2 As String, ByVal pro3 As String, ByVal pro4 As Decimal)
        Me.ProductID = pro1
        Me.ProductName = pro2
        Me.ProductBrand = pro3
        Me.Size = pro4
    End Sub

    Public Sub New(ByVal cat1 As String, ByVal cat2 As String, ByVal cat3 As Integer, ByVal cat4 As String, ByVal cat5 As Decimal)
        Me.Flavor = cat1
        Me.AnimalType = cat2
        Me.Price = cat3
        Me.Category = cat4
        Me.Quantity = cat5
    End Sub

    Public Sub New(ByVal pro1 As Integer, ByVal pro2 As String, ByVal pro3 As String, ByVal pro4 As Decimal,
                   ByVal cat1 As String, ByVal cat2 As String, ByVal cat3 As Integer, ByVal cat4 As String, ByVal cat5 As Decimal)
        Me.ProductID = pro1
        Me.ProductName = pro2
        Me.ProductBrand = pro3
        Me.Size = pro4
        Me.Flavor = cat1
        Me.AnimalType = cat2
        Me.Price = cat3
        Me.Category = cat4
        Me.Quantity = cat5
    End Sub
End Class
