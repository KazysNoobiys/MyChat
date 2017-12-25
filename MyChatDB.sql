USE master

CREATE DATABASE MyChatDB
ON
(
	NAME = 'MyChatDB',
	FILENAME = 'D:\DataBase\MyChatDB.mdf',
	SIZE = 10MB,
	MAXSIZE = 100MB,
	FILEGROWTH = 10MB
)
LOG ON
(
	NAME = 'LogMyChatDB',
	FILENAME = 'D:\DataBase\MyChatDB.ldf',
	SIZE = 5MB,
	MAXSIZE = 50MB,
	FILEGROWTH = 5MB
)

USE MyChatDB

CREATE TABLE Users
(
	UserID int IDENTITY NOT NULL, 	
	UserName Varchar(20) NOT NULL,
	UserPassword Varchar(20) NOT NULL,
	OnlineStatus bit
)  

ALTER TABLE Users
ADD CONSTRAINT 	OnlineStatus_b_def
DEFAULT 0 FOR OnlineStatus


ALTER TABLE Users
ADD CONSTRAINT PK_Users
PRIMARY KEY (UserID)	


CREATE TABLE SessionsLog
(
	LogID int IDENTITY NOT NULL,
	UserID int NOT NULL,
	StartSession DateTime NOT NULL,
	EndSession DateTime
)



ALTER TABLE SessionsLog
ADD CONSTRAINT PK_SessionsLog
PRIMARY KEY (LogId)

ALTER TABLE SessionsLog
ADD CONSTRAINT FK_SessionsLogUserID
FOREIGN KEY (UserID) REFERENCES Users(UserID)	

CREATE TABLE Chat
(
	MessageID int IDENTITY NOT NULL,
	UserID int NOT NULL,	
	MessageText VarChar(max),
	MessageDate DateTime NOT NULL
)

ALTER TABLE Chat
ADD CONSTRAINT PK_Chat
PRIMARY KEY (MessageID)

ALTER TABLE Chat
ADD CONSTRAINT FK_ChatUserID
FOREIGN KEY (UserID) REFERENCES Users(UserID)  

