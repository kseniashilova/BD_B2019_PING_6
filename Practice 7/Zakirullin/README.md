## Требования

 * python (работало на версии 3.8)
 * пакеты:
   * sqlalchemy
   * Faker
   * psycopg2

Пакеты можно установить командой

``` sh
make install_deps [PYTHON=команда_запуска_пайтона]
```

## Запуск

Убедиться, что база данных пустая.

Запустить скрипт:

``` sh
./fill_data.py --database-url postgresql://...
```

Либо

``` sh
python fill_data.py --database-url postgresql://...
```

## Указать количества элементов

Указываются аргументами, которые можно посмотреть аргументом `--help`.
