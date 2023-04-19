--added on 15-06-22 by Priyansi---------Start
--First Create databse and select database name then execute DBScript.
--- Database Name: BookMgtDB
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  			TABLE_NAME = 'TblUserMst')BEGIN 	CREATE TABLE dbo.TblUserMst (
				UserID int PRIMARY KEY IDENTITY(1,1),
				FullName nvarchar(255),
				Email nvarchar(255),
				UserName nvarchar(255),
				[Password] nvarchar(255),
				[RoleId] int not null,  --admin = 1 ,user = 2--
				[Address] nvarchar(255),
				ContactNumber nvarchar(20),
				ResetCode nvarchar(20),
				CreatedBy int not null,
				UpdateBy int not null,
				CreatedOn Datetime not null,
				UpdateOn Datetime not null,
				IsActive bit not null default(1),
				IsDeleted bit not null default(0)
                );
	PRINT 'Table Created' ENDELSEBEGIN 	PRINT 'Table Already Exist' END--GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  			TABLE_NAME = 'TblCategoryMst')BEGIN
	CREATE TABLE dbo.TblCategoryMst (
			CategoryId int PRIMARY KEY IDENTITY(1,1),
			CategoryName nvarchar(255),
			CreatedBy int not null,
			UpdateBy int not null,
			CreatedOn Datetime not null,
			UpdateOn Datetime not null,
			IsActive bit not null default(1),
			IsDeleted bit not null default(0)
			);

	PRINT 'Table Created' ENDELSEBEGIN 	PRINT 'Table Already Exist' END
--go

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  			TABLE_NAME = 'TblSubCategoryMst')BEGIN
	CREATE TABLE dbo.TblSubCategoryMst (
				SubCategoryId int PRIMARY KEY IDENTITY(1,1),
				CategoryID int not null,	
				SubCategoryName nvarchar(255),
				CreatedBy int not null,
				UpdateBy int not null,
				CreatedOn Datetime not null,
				UpdateOn Datetime not null,
				IsActive bit not null default(1),
				IsDeleted bit not null default(0)
				);
	PRINT 'Table Created' ENDELSEBEGIN 	PRINT 'Table Already Exist' END
go
--added on 15-06-22 by Priyansi---------End
--added on 15-06-22 by Brajesh---------Start
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  			TABLE_NAME = 'TblPermissionMst')BEGIN

	CREATE TABLE dbo.TblPermissionMst (
				PId int PRIMARY KEY IDENTITY(1,1),
				PermissionName varchar(250) not null,
				IsActive bit not null default(1),
				IsDeleted bit not null default(0)
				);

	PRINT 'Table Created' ENDELSEBEGIN 	PRINT 'Table Already Exist' END
go

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  			TABLE_NAME = 'TblAccessMst')BEGIN

	CREATE TABLE dbo.TblAccessMst (
				AccessId int PRIMARY KEY IDENTITY(1,1),
				AccessName varchar(250) not null,
				IsActive bit not null default(1),
				IsDeleted bit not null default(0)
				);

	PRINT 'Table Created' ENDELSEBEGIN 	PRINT 'Table Already Exist' END
go
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  			TABLE_NAME = 'TblUserAccessPermission')BEGIN

	CREATE TABLE dbo.TblUserAccessPermission (
				Id int PRIMARY KEY IDENTITY(1,1),
				UserId int not null,
				Permissionid int not null,
				AccessId int not null,
				IsActive bit not null default(1),
				IsDeleted bit not null default(0)
				);

	PRINT 'Table Created' ENDELSEBEGIN 	PRINT 'Table Already Exist' END
go

--added on 15-06-22 by Brajesh---------End
--added on 15-06-22 by Dhrusti---------Start
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  			TABLE_NAME = 'TblBookMst')BEGIN

	CREATE TABLE dbo.TblBookMst (
				BookID int PRIMARY KEY IDENTITY(1,1),
				CategoryId int not null,
				SubCategoryId int not null,
				BookName nvarchar(255),
				AuthorName nvarchar(255),
				BookPages int not null,
				Publisher nvarchar(255),
				PublishDate DateTime,  
				Edition nvarchar(255),
				[Description] nvarchar(MAX),
				Price decimal(18,0) not null,
				CoverImagePath nvarchar(MAX),
				PDFPath nvarchar(MAX),
				CreatedBy int not null,
				UpdateBy int not null,
				CreatedOn Datetime not null,
				UpdateOn Datetime not null,
				IsActive bit not null default(1),
				IsDeleted bit not null default(0)
                );
	PRINT 'Table Created' ENDELSEBEGIN 	PRINT 'Table Already Exist' END
go

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  			TABLE_NAME = 'TblBookDownloadMst')BEGIN	CREATE TABLE dbo.TblBookDownloadMst (				BookUserID int PRIMARY KEY IDENTITY(1,1),				BookId int not null,				FirstName nvarchar(255),				LastNane nvarchar(255),				EmailId nvarchar(255),				ContactNumber nvarchar(20),				[Location] nvarchar(255),				CreatedBy int not null,				UpdateBy int not null,				CreatedOn Datetime not null,				UpdateOn Datetime not null,				IsActive bit not null default(1),				IsDeleted bit not null default(0)                );	PRINT 'Table Created' ENDELSEBEGIN 	PRINT 'Table Already Exist' END
go
--added on 15-06-22 by Dhrusti---------End
---Executed on local 15-06-2022 by Brajesh.


--Insert Query added on 15-06-22 by Dhrusti--

Insert into TblPermissionMst (PermissionName , IsActive)
Values ('Catagory',1);

Insert into TblPermissionMst (PermissionName , IsActive)
Values ('SubCatagory',1);

Insert into TblPermissionMst (PermissionName , IsActive)
Values ('Books',1);

--Insert Query added on 15-06-22 by Dhrusti--


--Insert Query added on 15-06-22 by Dhrusti--

Insert into TblAccessMst(AccessName , IsActive)
Values ('Add',1);

Insert into TblAccessMst(AccessName , IsActive)
Values ('Delete',1);

Insert into TblAccessMst(AccessName , IsActive)
Values ('Edit',1);

Insert into TblAccessMst(AccessName , IsActive)
Values ('View',1);

--Insert Query added on 15-06-22 by Dhrusti--
--Execute on local on 15-06-22 by Brajesh

--Insert Query for TblUserMst added on 17-06-22 by Dhrusti--

Insert into TblUserMst(FullName,Email,UserName,[Password],RoleId,[Address],ContactNumber,CreatedBy,CreatedOn,UpdateBy,UpdateOn)
Values('Dhrusti','dhrusti.suthar@reynasolutions.com','Dhrush',123,1,'Vadodara',9632563256,1,17/06/2022,1,17/06/2022);

--Execute Insert Query on local 17-06-2022 by Brajesh.


--Insert Query for TblUserMst added on 01-07-22 by Dhrusti--

ALTER TABLE dbo.TblUserMst
ADD ResetCode nvarchar(MAX);