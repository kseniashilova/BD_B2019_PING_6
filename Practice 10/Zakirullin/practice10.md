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

Ключи-кандидаты:
 * ProductNo, CustomerNo, OrderDate
 * ProductName, CustomerNo, OrderDate (при условии уникальности имён продуктов)
 * ProductNo, CustomerName, OrderDate (при условии уникальности имён клиентов)
 * ProductName, CustomerName, OrderDate (при условии уникальности имён продуктов клиентов)

## Task 3

R(A, B, C, D)

F = {A → D, AB → C, AC → B} — минимальные ФЗ

F⁺ ⊇ {A → D, AB → DC, AC → BD}

Ключи-кандидаты:
 * AB
 * AC

3NF:
 1. Создать таблицы: (<ins>A</ins>D), (<ins>AB</ins>C), (<ins>AC</ins>B), (<ins>AB</ins>), (<ins>AC</ins>)
 2. Удалить лишние (оставшееся): (<ins>A</ins>D), (<ins>AB</ins>C), (<ins>AC</ins>B)
