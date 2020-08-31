import mysql.connector
import sys
from random import random, randint

mydb = mysql.connector.connect(
    host = "localhost",
    user="root",
    password="root",
    database="itserwis"
)

arg_id = sys.argv[1]

select = f"Select * from servicedocument where id={arg_id}"

mycursor = mydb.cursor()
mycursor.execute(select)

myresult = mycursor.fetchall()

rnd = randint(0, 10)

with open(f"d:\\moje\\docs\\test{rnd}.txt", "w") as f:
    for x in myresult:
        x = str(x)
        f.writelines(x)