## Task 1

BD → BDEG → BCDEG → ABCDEG

## Task 2

Order (ProductNo, ProductName, CustomerNo, CustomerName, OrderDate, UnitPrice, Quantity, SubTotal, Tax, Total)

 * ProductNo → ProductName, UnitPrice
 * ProductName → Tax
 * CustomerNo → CustomerName
 * ProductNo, CustomerNo, OrderDate → Quantity
 * UnitPrice, Quantity → SubTotal
 * SubTotal, Tax → Total
