------------------------------Added By Dhrusti 31-01-2023-------------------------------

--------------------------------UserMst-------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'UserMst')
BEGIN 
	Create table dbo.UserMst(
			Id int identity(1,1) primary key,
			EmployeeCode nvarchar(250) not null,
			FullName nvarchar(250) not null,
			Gender int not null,
			Email nvarchar(250) not null,
			PhoneNumber nvarchar(250) not null,
			EmergencyContact nvarchar(250) not null,
			DOB datetime not null,
			UserName nvarchar(250) not null,
			Password nvarchar(250) not null,
			ConfirmPassword nvarchar(250) not null,
			PermanentAddress nvarchar(250) not null,
			CurrentAddress nvarchar(250) not null,
			PostCode nvarchar(250) not null,
			EmploymentType int not null,
			CompanyName int not null,
			Department nvarchar(250) not null,
			Designation nvarchar(250) not null,
			Location nvarchar(250) not null,
			BloodGroup nvarchar(250) not null,
			OfferDate DateTime not null,
			JoinDate DateTime not null,
			Role int not null,
			BankName nvarchar(250) not null,
			AccountNumber nvarchar(250) not null,
			Branch nvarchar(250) not null,
			IFSCCode nvarchar(250) not null,
			PFAccountNumber nvarchar(250) not null,
			PANCardNumber nvarchar(250) not null,
			AdharCardNumber nvarchar(250) not null,
			Salary nvarchar(250) not null,
			ReportingManager int not null,
			Reason nvarchar(MAX) not null,
			EmployeePersonalEmailId nvarchar(250) not null,
			ProbationPeriod nvarchar(250) not null,
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


--------------------------------GenderMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'GenderMst')
BEGIN 
	Create table dbo.GenderMst(
			GenderId int identity(1,1) primary key,
			Gender nvarchar(250) not null,
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

--------------------------------EmployementTypeMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'EmployementTypeMst')
BEGIN 
	Create table dbo.EmployementTypeMst(
			EmployementTypeId int identity(1,1) primary key,
			EmployementType nvarchar(250) not null,
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

--------------------------------CompanyMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CompanyMst')
BEGIN 
	Create table dbo.CompanyMst(
			CompanyId int identity(1,1) primary key,
			CompanyName nvarchar(250) not null,
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

--------------------------------RoleMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'RoleMst')
BEGIN 
	Create table dbo.RoleMst(
			RoleId int identity(1,1) primary key,
			Role nvarchar(250) not null,
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

--------------------------------DesignationMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'DesignationMst')
BEGIN 
	Create table dbo.DesignationMst(
			DesignationId int identity(1,1) primary key,
			Designation nvarchar(250) not null,
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

--------------------------------ReportingManagerMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ReportingManagerMst')
BEGIN 
	Create table dbo.ReportingManagerMst(
			ReportingManagerId int identity(1,1) primary key,
			ReportingManagerName nvarchar(250) not null,
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

--------------------------------RequirementMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'RequirementMst')
BEGIN 
	Create table dbo.RequirementMst(
			RequirementId int identity(1,1) primary key,
			MainSkills nvarchar(250) not null,
			NoOfPosition int not null,
			TotalMinExp int not null,
			TotalMaxExp int not null,
			RelevantMinExp int not null,
			RelevantMaxExp int not null,
			TypeofEmployement int not null,
			POCName nvarchar(250) not null,
			MandatorySkill nvarchar(250) not null,
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
