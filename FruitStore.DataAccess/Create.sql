﻿CREATE DATABASE FruitStore
GO
USE FruitStore
GO
CREATE TABLE UserGroup(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	Deleted BIT DEFAULT 0 NOT NULL,
	CreateTime DATETIME DEFAULT GETDATE() NOT NULL,
	UpdateTime DATETIME DEFAULT GETDATE() NOT NULL
)
GO
CREATE TABLE [User](
	Id INT IDENTITY(1,1) PRIMARY KEY,
	UserGroupId INT REFERENCES UserGroup(Id) NOT NULL,
	[Name] VARCHAR(100) NOT NULL,
	Surname VARCHAR(100) NOT NULL,
	Birthday DATETIME,
	UserName VARCHAR(100),
	[Password] VARCHAR(100),
	Deleted BIT DEFAULT 0 NOT NULL,
	CreateTime DATETIME DEFAULT GETDATE() NOT NULL,
	UpdateTime DATETIME DEFAULT GETDATE() NOT NULL
)
GO
CREATE TABLE Fruit(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	[Count] INT DEFAULT 0 NOT NULL,
	UnitPrice REAL DEFAULT 0 NOT NULL,
	Vitamins INT DEFAULT 0 NOT NULL,
	Deleted BIT DEFAULT 0 NOT NULL,
	CreateTime DATETIME DEFAULT GETDATE() NOT NULL,
	UpdateTime DATETIME DEFAULT GETDATE() NOT NULL
)