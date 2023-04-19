--============================================================RoleMst==============================================================--
--=================================================================================================================================--

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'RoleMst')
BEGIN 
	Create table dbo.RoleMst (
			Id int identity(1,1) primary key,
			RoleName nvarchar(50) not null,
			IsActive bit default(0) not null,
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

--============================================================UserMst=============================================================--
--================================================================================================================================--

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'UserMst')
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
			Role int not null,
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

--==========================================================AppointmentMst========================================================--
--================================================================================================================================--

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
			LastAppoitmentDate DateTime not null,
			Status nvarchar(50) not null,
			IsEditable bit not null,
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

--=========================================================CallTypeMst============================================================--
--================================================================================================================================--

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

--========================================================PhysicianMst============================================================--
--================================================================================================================================--

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
			SecretaryFirstName nvarchar(20) not null,
			SecretaryLastName nvarchar(20) not null,
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

--========================================================NotificationMst=========================================================--
--================================================================================================================================--
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'NotificationMst')
BEGIN 
	Create table dbo.NotificationMst(
			Id int identity(1,1) primary key,
			SenderId int not null,
			ReceiverId int not null,
			Description nvarchar(Max) not null,
			IsNotificationRead bit not null,
			ApprovalStatus nvarchar(50) not null,
			AdminDescription nvarchar(50) not null,
			DescriptionTitle nvarchar(50) not null,
			AdminDescriptionTitle nvarchar(50) not null,
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

--========================================================PatientEmailMst=========================================================--
--================================================================================================================================--

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


--========================================================RemarkMst===============================================================--
--================================================================================================================================--


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'RemarkMst')
BEGIN 
	Create table dbo.RemarkMst (
			Id int identity(1,1) primary key,
			AppointmentId int  not null,
			Datetime DateTime not null,
			Remark nvarchar(20) not null,
			Details nvarchar(Max) not null,
			Status int not null,
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
