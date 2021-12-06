**Задача 1**

Отношение (A, B, C, D, E, G) имеет следующие функциональные зависимости:

-   AB → C

-   C → A

-   BC → D

-   ACD → B

-   D → EG

-   BE → C

-   CG → BD

-   CE → AG

Постройте закрытие атрибута (Attribute Closure )(BD)+

Решение:

1.  B и D принадлежат (BD)+ {B, D}

2.  Т.к D → EG, то E и G принадлежат (BD)+ {B, D, E, G}

3.  Т.к BE → C, то C принадлежит (BD)+ {B, C, D, E, G}

4.  Т.к C → A, то А принадлежит (BD)+ {A, B, C, D, E, G}

Таким образом, (BD)+ = {A, B, C, D, E, G}

**Задача 2**

Посмотрите на отношения: Order (ProductNo, ProductName, CustomerNo,
CustomerName,OrderDate,UnitPrice, Quantity, SubTotal, Tax, Total)

Ставка налога зависит от Товара (например, 20 % для книг или 30 % для
предметов роскоши). В день допускается только один заказ на продукт и
клиента (несколько заказов объединяются).

-   А) Определить нетривиальные функциональные зависимости в отношении

Ставка налога зависит от товара, товар определяется ProductNo,
покупатель определяется CustomerNo. Тогда:

ProductNo -&gt; ProductName

ProductNo -&gt; Tax

ProductNo -&gt; UnitPrice

CustomerNo -&gt; CustomerName

{ProductNo, OrderDate, CustomerNo} -&gt; {ProductName, CustomerName,
Quantity, UnitPrice, SubTotal, Total}

{UnitPrice, Quantity} -&gt; SubTotal

{SubTotal, Tax} -&gt; Total

-   Б) Каковы ключи-кандидаты?

> {ProductNo, OrderDate, CustomerNo}

**Задача 3**

Рассмотрим соотношение R(A, B, C, D) со следующими функциональными
зависимостями: F = {A→D, AB→ C, AC→ B}

A→D

AB→ C

AC→ B

-   А) \*Каковы все ключи-кандидаты?

{A, B}, {A, C}

-   Б) Преобразуйте R в 3NF, используя алгоритм синтеза.

1\) F = {A→D, AB→ C, AC→ B}

2\) {A, D}, {A, B, C}, {A, C, B}

3\) {A, D}, {A, B}, {A, C} ({AC→ B} убираем)

4\) {A, D}, {A, B, C}

Ответ: {A, D}, {A, B, C}
