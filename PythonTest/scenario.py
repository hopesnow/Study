# coding: utf-8
# scenario.rb を pythonで書く練習

import xlrd
import csv
import sys

args = sys.argv

file_name = args[1]

print("file name is " + file_name)


def CheckInt(num):
    if not isinstance(num, (int, float, long)):
        return False
    # print("check num : " + str(num))
    check = int(num) == num # intであればキャストしても誤差がでないのでTrue
    # print("check is " + str(check))
    return check


def SheetRead(file_name):
    book = xlrd.open_workbook(file_name)

    for sheet in book.sheets():

        print("nrows : " + str(sheet.nrows))
        print("ncols : " + str(sheet.ncols))

        row = 0
        while row < sheet.nrows:
            col = 0
            while col < sheet.ncols:
                cell = sheet.cell(row, col).value
                # if type(cell) == int:
                # if isinstance(cell, int): # cellがint型もしくはlong型だったらTrue
                #     cell = int(cell)
                if CheckInt(cell):
                    cell = int(cell)
                print(str(cell) + "\t"),
                col = col + 1
            print("\n")
            row = row + 1


SheetRead(file_name)
