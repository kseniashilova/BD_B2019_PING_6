import itertools
from typing import List, Tuple


def generate_all_combinations(t1, t2):
    n = len(t1) + len(t2)
    # Индексы позиций операций первой транзакции (всевозможные)
    t1_positions = list(itertools.combinations(range(n), len(t1)))
    for t1_pos_comb in t1_positions:
        # Индексы позиций операций второй транзакции (все оставшиеся от первой)
        t2_pos_comb = list(set(range(n)) - set(t1_pos_comb))
        combination = [None] * n
        for t1_elem, t1_i in zip(t1, t1_pos_comb):
            combination[t1_i] = (*t1_elem, 1)  # Добавляю номер транзакции 1
        for t2_elem, t2_i in zip(t2, t2_pos_comb):
            combination[t2_i] = (*t2_elem, 2)  # Добавляю номер транзакции 2
        yield combination


def is_serializable(series: List[Tuple[str, str, int]]):
    for i, (op, value, tr_num) in enumerate(series):
        if op != 'w':
            continue
        # Если встретил запись, иду в обратном направлении
        for j in range(i - 1, -1, -1):
            op_prev, value_prev, tr_num_prev = series[j]
            # Если встретил чтение этого значения этой же транзакцией, то всё ок,
            if op_prev == 'r' and value_prev == value and tr_num_prev == tr_num:
                break
            # Если встретил запись другой транзакцией, возвращаю False
            # (выходит, это произошло после чтения этой записи первой тразакцией)
            if op_prev == 'w' and value_prev == value and tr_num_prev != tr_num:
                return False
    return True


def to_str(combination):
    operations = ', '.join([f'{op}{tr_num}({value})' for op, value, tr_num in combination])
    serializable = 'сериализуема' if is_serializable(combination) else 'не сериализуема'
    return f'{operations} -> {serializable}'


t1 = [('r', 'A'), ('w', 'A'), ('r', 'B'), ('w', 'B')]
t2 = [('r', 'A'), ('w', 'A')]
all_combinations = generate_all_combinations(t1, t2)
for i, combination in enumerate(all_combinations):
    print(f'{i + 1})', to_str(combination))
