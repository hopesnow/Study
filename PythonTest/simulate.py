# coding: utf-8

import random

# print random.randint(1, 8) # 1 ~ 8からランダム

shuffle_list = ["おまえ", "うまそう", "おれ", "くう"]
random.shuffle(shuffle_list)
for msg in shuffle_list:
    print(msg),
