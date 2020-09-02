import sys
import mysql.connector
from fpdf import FPDF 
   
arg = sys.argv[1]

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
    Data: {row[0]}\n\n

    Dane klienta:\t\t\t\t\t\t\t\t\t\tDane pracownika:

    Imię: {row[1]}\t\t\t\t\t\t\t\t\t\tImię: {row[4]}
    Nazwisko: {row[2]}\t\t\t\t\t\t\t\t\tNazwisko: {row[5]}
    Adres: {row[3]}\t\t\t\t\t\t\t\t\t\tNumer: {row[6]}        
                
    \n\n\n

    ------------------------------------------------------------------------------------------------------


    Urządzenie:

    Typ: {row[7]}
    Marka: {row[8]}
    Model: {row[9]}
    
    \n\n\n

    Opis:

    {row[10]}
    _______________________________________________________________________________________________________

    
    \n\n\n
    
    Podpis klienta:\t\t\t\t\t\t\t\t\t\tPodpis przyjmującego zlecenie:
    
    _____________________\t\t\t\t\t\t\t\t\t_____________________
    
    '''
    
    fileName = row[11][20:]
    with open(f"D:\\Temp\\Itserwis\\{fileName}.txt", "w") as f:
        f.write(common_string)
        
   # # open the text file in read mode 
# f = open(f"D:\\Temp\\Itserwis\\{fileName}.pdf", "w") 

# # insert the texts in pdf 
# pdf.cell(200, 10, txt = common_string) 
   
# # save the pdf with name .pdf 
# pdf.output(f"D:\\Temp\\Itserwis\\{fileName}.pdf") 