import sys
import mysql.connector

# first argument is an id of the document
arg = 5

# connection string section

mydb = mysql.connector.connect(
  host="localhost",
  user="root",
  password="root",
  database="itserwis",
  auth_plugin='mysql_native_password'
)

mycursor = mydb.cursor()

mycursor.execute(f"SELECT documentdate, clientname, clientsurename, clientaddress, employeename, employeesurname, employeeid, devicetype, devicebrand, devicemodel, description, documentnumber FROM servicedocument where id={arg}")

myresult = mycursor.fetchall()

for row in myresult:
    common_string = f''' 
    Data: {row['documentdate']}

            Dane klienta:                                                  Dane pracownika:

    Imię: {row['clientname']}                                       Imię: {row['employeename']}
    Nazwisko: {row['clientsurename']}                               Nazwisko: {row['employeesurname']}
    Adres: {row['clientaddress']}                                  Numer: {row['employeeid']}        
    


    ------------------------------------------------------------------------------------------------------


    Urządzenie:

    Typ: {row['devicetype']}
    Marka: {row['devicebrand']}
    Model: {row['devicemodel']}
    
    

    Opis:

    {row['description']}
    _______________________________________________________________________________________________________

    
    
    '''
    with open(f"D:\\Temp\\Itserwis\\{row['documentnumber']}.txt", "w") as f:
        f.write(common_string)