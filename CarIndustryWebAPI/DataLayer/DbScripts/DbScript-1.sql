-------------------------------Added By DS 17-08-2022-------------------------------

---------------------------------------CarMst---------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CarMst')
BEGIN 
	Create table dbo.CarMst(
			Id int identity(1,1) primary key,
			Model nvarchar(250) not null,
			RegistrationId int not null,
			Price int not null,
			Brand nvarchar(250) not null,
			BuyTime datetime not null,
			IsActive bit not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

---------------------------------------BrandMst---------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'BrandMst')
BEGIN 
	Create table dbo.BrandMst(
			Id int identity(1,1) primary key,
			Brand nvarchar(250) not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

Insert into BrandMst(Brand) Values('Mercedes')

Insert into BrandMst(Brand) Values('Audi')

Insert into BrandMst(Brand) Values('Skoda')

Insert into BrandMst(Brand) Values('Range Rover')

Insert into BrandMst(Brand) Values('Morris Garages')

------------------------

ALTER TABLE CarMst
ALTER COLUMN  Brand int;

