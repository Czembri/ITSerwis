CREATE TABLE CUSTOMERS (
	ID int NOT NULL AUTO_INCREMENT,
	FIRSTNAME VARCHAR(40),
	LASTNAME VARCHAR(40),
	ADDRESS1 VARCHAR(100),
	ADDRESS2 VARCHAR(100),
	CITY VARCHAR(40),
	VOIVODESHIP VARCHAR(50),
	POSTCODE VARCHAR(20),
	COUNTRY VARCHAR(40),
	PHONE VARCHAR(30),
	EMAIL VARCHAR(50),
	BUSINESSNAME VARCHAR(40),
	FAX VARCHAR(30),
	MOBILE VARCHAR(20),
	REFERRALLID int,
	SUBMITTEDDATE DATE,
	NIP int,
	ACCOUNTNUMBER int,
	DISCOUNT int,
	PRIMARY KEY (ID)
);