------------------------------Added By Sonal 11-01-23-------------------------------

--------------------------------UserTokenMst-------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'UserTokenMst')
BEGIN 
	Create table dbo.UserTokenMst(
			Id int identity(1,1) primary key,
			UserId int not null,
			Token nvarchar(max) not null,
			RefreshToken nvarchar(max) not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			ExpiredOn datetime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--------------------------------AppointmentMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'AppointmentMst')
BEGIN 
	Create table dbo.AppointmentMst(
			Id int identity(1,1) primary key,
			CallTypeId int  not null,
			AccountNo nvarchar(50)not null,
			Date datetime null,
			NewAppoitmentDate datetime  null,
			ActualAppoitmentDate datetime  null,
			AppoitmentLastDate bit null,
			ExtensionId int   null,
			TaxId  nvarchar(20) null,
			PatientFirstName nvarchar(50) not null,
			PatientLastName nvarchar(50)  not null,
			PatientEmail nvarchar(50)  not null,
			PatientMobileNo nvarchar(20)  not null,
			PatientDOB datetime  not null,
			AppDoctorId int   null,
			DoctorGender nvarchar(20)  null,
			PCP nvarchar(50) null,
			PCPMobileNo nvarchar(20)  null,
			ReferingMD nvarchar(50) null,
			ReferingMobileNo nvarchar(20)  null,
			PrimaryInsuranceId nvarchar(50) not null,
			PrimaryInsuranceName nvarchar(50)not null,
			SecondaryInsuranceId nvarchar(50) null,
			SecondaryInsuranceName nvarchar(50) null,
			Notes nvarchar(Max)not null,
			Reason nvarchar(Max) null,
			IsAppoitmentVehicleOrworkInjury bit default(1)  null,
			IsCovidPossitive bit default(1)  null,
			IsIdCurrentOrExpired nvarchar(50) null,
			IsVaccinated bit default(1) not null,
			IdExpirationDate datetime not null,
			IsMatchInsurance bit default(1) not null,
			IsActive bit default(1) not null,
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


---------------------------------------------------------ActivityLogMst-----------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ActivityLogMst')
BEGIN 
CREATE TABLE dbo.ActivityLogMst (
				ID int PRIMARY KEY IDENTITY(1,1),
				ExecutionDate Datetime not null,
				APIURL nvarchar(MAX),
				MethodType nvarchar(10),
				Request nvarchar(MAX),
				Response nvarchar(MAX)
                );
PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

---------------------------------------------------------CallTypeMst-----------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CallTypeMst')
BEGIN 
	Create table dbo.CallTypeMst (
			Id int identity(1,1) primary key,
			CallTypeName nvarchar(50) not null,
			IsActive bit default(1) not null,
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

---------------------------------------------------------ClientMst-----------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClientMst')
BEGIN 
	Create table dbo.ClientMst(
			Id int identity(1,1) primary key,
			ClientAccountNo nvarchar(50),
			FirstName nvarchar(20) not null,
			LastName nvarchar(20) not null,
			OfficeName nvarchar(20) not null,
			Country int not null,
			StreetNo nvarchar(50),
			HomeName nvarchar(100),
			StreetName nvarchar(100),
			Suburb nvarchar(50),
			City nvarchar(50),
			Province int not null,
			PostalCode nvarchar(10),
			InfoEmail nvarchar(50) not null,
			AppoitmentEmail nvarchar(50) not null,
			DoctorEmail nvarchar(50) not null,
			MobileNo nvarchar(20) not null,
			FaxNo nvarchar(20) not null,
			IsActive bit default(1) not null,
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


---------------------------------------------------------ExtensionMst-----------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ExtensionMst')
BEGIN 
	Create table dbo.ExtensionMst (
			Id int identity(1,1) primary key,
			ExtensionName nvarchar(50) not null,
			IsActive bit default(1) not null,
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


---------------------------------------------------------PhysicianMst-----------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PhysicianMst')
BEGIN 
	Create table dbo.PhysicianMst(
			Id int identity(1,1) primary key,
			DoctorFirstName nvarchar(20) not null,
			DoctorLastName nvarchar(20) not null,
			DoctorDegreeName1 nvarchar(20) not null,
			DoctorDegreeName2 nvarchar(20) not null,
			DoctorDegreeName3 nvarchar(20) not null,
			secretaryFirstName nvarchar(20) not null,
			secretaryLastName nvarchar(20) not null,
			DoctorEmail nvarchar(50) not null,
			DoctorMobileNo nvarchar(20) not null,
			IsActive bit default(1) not null,
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

---------------------------------------------------------RemarkMst-----------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'RemarkMst')
BEGIN 
	Create table dbo.RemarkMst (
			Id int identity(1,1) primary key,
			AppointmentId int  not null,
			Datetime DateTime not null,
			Remark nvarchar(20) not null,
			Details nvarchar(Max) not null,
			IsActive bit default(1) not null,
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

---------------------------------------------------------UserMst-----------------------------------------------------------------


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'UserMst')
BEGIN 
	Create table dbo.UserMst (
			Id int identity(1,1) primary key,
			FirstName nvarchar(20) not null,
			LastName nvarchar(20) not null,
			UserName nvarchar(50) not null,
			Password nvarchar(max) not null,
			DOB DateTime not null,
			MobileNo nvarchar(20) not null,
			Email nvarchar(50) not null,
			IsActive bit default(1) not null,
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

---------------------------------------------------------DurationMst-----------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'DurationMst')
BEGIN 
	Create table dbo.DurationMst (
			Id int identity(1,1) primary key,
			StartDate DateTime  null,
			EndDate DateTime null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			AppointmentId decimal null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

---------------------------------------------------------PatientEmailMst-----------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PatientEmailMst')
BEGIN 
	Create table dbo.PatientEmailMst(
			Id int identity(1,1) primary key,
			SenderId int not null,
			ReceiverId int not null,
			EmailFor nvarchar(250) not null,
			Subject nvarchar(MAX) not null,
			Message nvarchar(MAX) not null,
			IsActive bit default(1) not null,
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

-----------------------------------------------------Appointmentmst---------------------------------------------
------------------------------Added By Sonal 13-01-23-------------------------------


alter table AppointmentMst
Add LastAppoitmentDate DateTime null

--------------------------------RoleMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'RoleMst')
BEGIN 
	Create table dbo.RoleMst (
			Id int identity(1,1) primary key,
			RoleName nvarchar(50) not null,
			IsActive bit default(1) not null,
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

------------------------------Added By Sonal 01-02-23-------------------------------

alter table UserMst
Add Role int null

------------------------------Added By Sonal 01-02-23-------------------------------

alter table  RemarkMSt
add Status int null

------------------------------Added By Sonal 01-02-23-------------------------------


--------------------------------NotificationMst---------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'NotificationMst')
BEGIN 
	Create table dbo.NotificationMst(
			Id int identity(1,1) primary key,
			SenderId int not null,
			ReceiverId int not null,
			Description nvarchar(Max) not null,
			IsNotificationRead bit not null,
			IsActive bit default(1) not null,
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


------------------------------Added By Sonal 10-02-23-------------------------------

Alter table NotificationMst
Add ApprovalStatus nvarchar(50) null

Alter table NotificationMst
Add AdminDescription nvarchar(50) null

------------------------------Added By Sonal 10-02-23-------------------------------

--------------------------------AprrovalMst---------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'AprrovalMst')
BEGIN 
	Create table dbo.AprrovalMst (
			Id int identity(1,1) primary key,
			StatusName nvarchar(50) not null,
			IsActive bit default(1) not null,
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

------------------------------Added By Sonal 10-02-23-------------------------------

--------------------------------AppointmentMst---------------------------------


alter table  AppointmentMst
Add  Status nvarchar(50) null

------------------------------Added By Sonal 10-02-23-------------------------------

--------------------------------AppointmentMst---------------------------------

alter table PatientEmailMSt
Add PatientEMail nvarchar(50) null

------------------------------Added By Sonal 10-02-23-------------------------------

alter table NotificationMst
Add DescriptionTitle nvarchar(50) null

alter table NotificationMst
Add AdminDescriptionTitle nvarchar(50) null

------------------------------Added By Sonal 10-02-23-------------------------------
Alter table AppointmentMst
Add IsEditable bit null
------------------------------Added By Sonal 10-02-23-------------------------------

