# coding: utf-8
# Excelの読み込みのテストプログラム
import sys
import xlrd
import csv

args = sys.argv
# print("sample")

# book = xlrd.open_workbook('test.xlsx')

def SheetRead(file_name):

    print("File Name : " + file_name)

    book = xlrd.open_workbook(file_name)

    sheet1 = book.sheet_by_index(0)

    colum = [sheet1.cell(1, 1).value, sheet1.cell(1, 2).value, sheet1.cell(1, 3).value]
    ids = [sheet1.cell(2, 1).value, sheet1.cell(3, 1).value, sheet1.cell(4, 1).value]
    names = [sheet1.cell(2, 2).value, sheet1.cell(3, 2).value, sheet1.cell(4, 2).value]
    prices = [sheet1.cell(2, 3).value, sheet1.cell(3, 3).value, sheet1.cell(4, 3).value]

    i = 0
    print colum[0] + "\t" + colum[1] + "\t" + colum[2]
    # print(colum[0] + "\t" +colum[1] + "\t" + colum[2])
    while(i < len(colum)) :
        print(str(int(ids[i])) + "\t" + names[i] + "\t" + str(int(prices[i])))
        i += 1


# SheetRead(args[1])

def CSVCreate():
    f = open('test.csv', 'w')
    writer = csv.writer(f, lineterminator = '\n')

    csv_list = []
    csv_list.append("first01")
    csv_list.append("second02")
    writer.writerow(csv_list)

    csv_list = []
    csv_list.append("third03")
    csv_list.append("fourth04")
    writer.writerow(csv_list)

    f.close()

def CSVCreate2():
    with open('test.csv', 'w') as text: # close()が不必要になる

        writer = csv.writer(text, lineterminator = '\n')

        csv_list = []
        csv_list.append("first001")
        csv_list.append("second002")
        writer.writerow(csv_list)

        csv_list = []
        csv_list.append("third003")
        csv_list.append("fourth004")
        writer.writerow(csv_list)

CSVCreate2()
