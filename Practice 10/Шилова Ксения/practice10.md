## Задание 10

### Задача 1

Отношение (A, B, C, D, E, G) имеет следующие функциональные зависимости:

* AB → C
* C → A 
* BC → D 
* ACD → B 
* D → EG 
* BE → C 
* CG → BD 
* CE → AG 

Постройте закрытие атрибута (Attribute Closure )(BD)+
##### Решение:
1) (BD)  
2) Добавим C - (BCD)
3) Добавим A - (ABCD)
4) Добавим E и G - (ABCDEG)
5) Далее результат меняться не будет, то есть   
##### Ответ:
(ABCDEG) (получается, что это супер ключ)  
  
### Задача 2

Посмотрите на отношения: Order (ProductNo, ProductName, CustomerNo, CustomerName, OrderDate,UnitPrice, Quantity, SubTotal, Tax, Total)

Ставка налога зависит от Товара (например, 20 % для книг или 30 % для предметов роскоши).
В день допускается только один заказ на продукт и клиента (несколько заказов объединяются).

* А) Определить нетривиальные функциональные зависимости в отношении
##### Решение: 
ProductName → Tax  
ProductNo → ProductName  
ProductNo → Tax  
CustomerNo → CustomerName  
ProductNo → UnitPrice  
И из более сложного: товар, покупатель и дата заказа определяет Quantity, SubTotal, Total  
{ProductNo, CustomerNo, OrderDate} → {Quantity, SubTotal, Total}  
Также:  
{UnitPrice, Quantity} → {Total}  
{Tax, Total} → {SubTotal}  
И как следствие:  
{UnitPrice, Quantity, Tax} → {Total, SubTotal}  
  
И как следствие:  
{ProductNo, CustomerNo, OrderDate} → {ProductName, CustomerName, Tax, UnitPrice, Quantity, SubTotal, Total}  
* Б) Каковы ключи-кандидаты?  
##### Решение: 
Исходя из того, что {ProductNo, CustomerNo, OrderDate} → {ProductName, CustomerName, Tax, UnitPrice, Quantity, SubTotal, Total},  
Ключи-кандидаты - это {ProductNo, CustomerNo, OrderDate}
ИЛИ что то же самое почти {ProductName, CustomerName, OrderDate}

### Задача 3

Рассмотрим соотношение R(A, B, C, D) со следующими функциональными зависимостями:
F = {A→D, AB→ C, AC→ B}
* А) Каковы все ключи-кандидаты?
##### Решение:   
(AB)+ - (ABDC)
И для (AC)+ то же самое
##### Ответ:  
{A,B} и {A,C}  
* Б) Преобразуйте R в 3NF, используя алгоритм синтеза.
##### Решение: 
Шаг 1. Compute minimal basis:  
{A→D, AB→ C, AC→ B}  
Получается, ничего преобразовывать здесь не нужно  
Шаг 2.   
Создаем таблицы:  
{A, D}, {A, B, C}, {A, C, B}    
Шаг 3. Поглощение  
{A,D}, {A,B,C}  
##### Ответ:  
{A,D}, {A,B,C}  
