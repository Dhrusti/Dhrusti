
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TblCodeMst')
BEGIN 
	Create table dbo.TblCodeMst (
			Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
			Code decimal(18,0),
			CodeName nvarchar(255),
			IsActive bit default(0) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null
			);
	PRINT 'Table Created' 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'LevelFirstMst')
BEGIN 
	Create table dbo.LevelFirstMst (
			LevelFirstId int NOT NULL PRIMARY KEY IDENTITY(1,1),
			Code nvarchar(20),
			CodeName nvarchar(100),
			IsActive bit  not null,
			IsDeleted bit  not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null
			);
	PRINT 'Table Created' 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'LevelSecondMst')
BEGIN 
	Create table dbo.LevelSecondMst (
			LevelSecondId int NOT NULL PRIMARY KEY IDENTITY(1,1),
			Code nvarchar(20),
			CodeName nvarchar(100),
			LevelFirstId int  FOREIGN KEY REFERENCES LevelFirstMst(LevelFirstId),
			IsActive bit  not null,
			IsDeleted bit  not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null
			);
	PRINT 'Table Created' 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'LevelThirdMst')
BEGIN 
	Create table dbo.LevelThirdMst (
			LevelThirdId int NOT NULL PRIMARY KEY IDENTITY(1,1),
			Code nvarchar(20),
			CodeName nvarchar(100),
			LevelSecondId int  FOREIGN KEY REFERENCES LevelSecondMst(LevelSecondId),
			IsFinalLevel bit  not null,
			CreditDebit nvarchar(10),
			IsActive bit not null,
			IsDeleted bit  not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null
			);
	PRINT 'Table Created' 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'LevelFourthMst')
BEGIN 
	Create table dbo.LevelFourthMst (
			LevelFourthId int NOT NULL PRIMARY KEY IDENTITY(1,1),
			Code nvarchar(20),
			CodeName nvarchar(100),
			LevelThirdId int  FOREIGN KEY REFERENCES LevelThirdMst(LevelThirdId),
			IsFinalLevel bit  not null,
			CreditDebit nvarchar(10),
			IsActive bit  not null,
			IsDeleted bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null
			);
	PRINT 'Table Created' 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'LevelFifthMst')
BEGIN 
	Create table dbo.LevelFifthMst (
			LevelFifthId int NOT NULL PRIMARY KEY IDENTITY(1,1),
			Code nvarchar(20),
			CodeName nvarchar(100),
			LevelFourthId int  FOREIGN KEY REFERENCES LevelFourthMst(LevelFourthId),
			IsFinalLevel bit  not null,
			CreditDebit nvarchar(10),
			IsActive bit  not null,
			IsDeleted bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null
			);
	PRINT 'Table Created' 
END