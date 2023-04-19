
--------------------------------RegistrationMst-------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'RegistrationMst')
BEGIN 
	Create table dbo.RegistrationMst(
			RegistrationId int identity(1,1) primary key,
			FirstName nvarchar(250) not null,
			LastName nvarchar(250) not null,
			Email nvarchar(250) not null,
			Password nvarchar(250) not null,
			MobileNo nvarchar(250) not null,
			Designation int not null,
			Address nvarchar(250) not null,
			IsActive bit not null,
			IsDeleted bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate DateTime not null,
			UpdatedDate DateTime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--------------------------------TokenMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'TokenMst')
BEGIN 
	Create table dbo.TokenMst(
			TokenId int identity(1,1) primary key,
			UserId int not null,
			Token nvarchar(MAX) not null,
			RefreshToken nvarchar(MAX) not null,
			TokenCreated DateTime not null,
			TokenExpires DateTime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO