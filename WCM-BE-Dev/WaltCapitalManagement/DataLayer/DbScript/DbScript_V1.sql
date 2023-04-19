------------------------------Added By Tanmay 19-8-22-------------------------------

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

 --------------------------------OfficeMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'OfficeMst')
BEGIN 
	Create table dbo.OfficeMst(
			Id int identity(1,1) primary key,
			Office nvarchar(100) not null,
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

--Executed On Local By Tanmay 19-08-22

 --------------------------------PersonalityTypeMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PersonalityTypeMst')
BEGIN 
	Create table dbo.PersonalityTypeMst(
			Id int identity(1,1) primary key,
			PersonalityType nvarchar(255) not null,
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

--Executed On Local By Ajay 22-08-22
------------------------------Added By Dhrusti 22-08-2022-------------------------------

--------------------------------AccountTypeMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'AccountTypeMst')
BEGIN 
	Create table dbo.AccountTypeMst (
			Id int identity(1,1) primary key,
			AccountType nvarchar(100) not null,
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

--Executed On Local By Dhrusti 22-08-2022

------------------------------Added By Dhrusti 22-08-2022-------------------------------

--------------------------------WaltCapConsultantMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'WaltCapConsultantMst')
BEGIN 
	Create table dbo.WaltCapConsultantMst (
			Id int identity(1,1) primary key,
			WaltCapConsultant nvarchar(100) not null,
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

--Executed On Local By Dhrusti 22-08-2022


------------------------------Added By Dhrusti 22-08-2022-------------------------------

--------------------------------IFAMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'IFAMst')
BEGIN 
	Create table dbo.IFAMst (
			Id int identity(1,1) primary key,
			IFA nvarchar(100) not null,
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

--Executed On Local By Dhrusti 22-08-2022

--------------------------------ExternalAccountDetails ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ExternalAccountDetails')
BEGIN 
	Create table dbo.ExternalAccountDetails (
			Id int identity(1,1) primary key,
			ServiceProvider int not null,
			Type int not null,
			AccountCode nvarchar(100) not null,
			ClientId int not null,
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

--Executed On Local By Tanmay 23-08-2022

--------------------------------UserMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'UserMst')
BEGIN 
	Create table dbo.UserMst (
			Id int identity(1,1) primary key,
			ProfilePhoto nvarchar(max),
			Office int not null,
			ClientAccNo nvarchar(50) not null,
			Password nvarchar(max) not null,
			AccessCategoryId int not null,
			ResponsiblePerson nvarchar(50),
			FirstName nvarchar(20) not null,
			LastName nvarchar(20) not null,
			PositionHeld nvarchar(50),
			DOB DateTime not null,
			TrustRegNo nvarchar(50),
			MobileNo nvarchar(20) not null,
			WorkNo nvarchar(20),
			Email nvarchar(50) not null,
			SARSTaxNo nvarchar(50) not null,
			Country int not null,
			StreetNo nvarchar(50),
			HomeName nvarchar(100),
			StreetName nvarchar(100),
			Suburb nvarchar(50),
			City nvarchar(50),
			Province int not null,
			PostalCode nvarchar(10),
			AccountHolder nvarchar(50),
			Bank nvarchar(50),
			AccountType int not null,
			AccountNo nvarchar(50),
			BranchCode nvarchar(50),
			SwiftCode nvarchar(50),
			ClientType int not null,
			PersonalityType int not null,
			WaltCapConsultant int not null,
			IFA int not null,
			MaritalStatus nvarchar(50) not null,
			SoftwareAccessGroup int not null,
			SpouseName nvarchar(100),
			SpouseDOB DateTime,
			NickName nvarchar(50),
			FASerial nvarchar(100),
			Notes nvarchar(MAX),
			Equity bit not null,
			TFSA bit not null,
			DCS bit not null,
			MCS bit not null,
			InitialFee nvarchar(50),
			AnnualManagementFee nvarchar(50),
			PerformanceFee nvarchar(50),
			BrokerageRate nvarchar(50),
			FlatBrokerageRate nvarchar(50),
			AdminMonthlyFee nvarchar(50),
			Other nvarchar(MAX),
			IsVATApplicable bit not null,
			LoadWithoutFee bit not null,
			DeviceId nvarchar(max),
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

--Executed On Local By Tanmay 23-08-2022

--------------------------------AccessCategoryMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'AccessCategoryMst')
BEGIN 
	Create table dbo.AccessCategoryMst (
			Id int identity(1,1) primary key,
			AccessCategory nvarchar(100) not null,
			ParentId int not null,
			TypeId int not null,
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

--Executed On Local By Tanmay 23-08-2022

--------------------------------AccessCategoryTypeMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'AccessCategoryTypeMst')
BEGIN 
	Create table dbo.AccessCategoryTypeMst (
			Id int identity(1,1) primary key,
			AccessCategoryType nvarchar(100) not null,
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

--Executed On Local By Tanmay 23-08-2022

Insert into dbo.AccessCategoryTypeMst values ('Roles', 1,0,1,1,GETDATE(),GETDATE())
Insert into dbo.AccessCategoryMst values ('SuperAdmin', 0, 1, 1, 0, 1, 1, GETDATE(), GETDATE())

--Executed On Local By Preyansi 25-08-2022

--------------------------------LinkMst-------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'LinkMst')
BEGIN 
	Create table dbo.LinkMst(
			Id int identity(1,1) primary key,
			UserId int not null,
			ResetPasswordLink nvarchar(max) not null,
			IsClicked bit not null,
			CreatedDate datetime not null,
			ExpiredDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO
--Executed On Local By Preyansi 25-08-2022

--------------------------------UserDocumentMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'UserDocumentMst')
BEGIN 
	Create table dbo.UserDocumentMst (
			Id int identity(1,1) primary key,
			UserId int not null,
			DocumentTypeId int not null,
			DocumentPath nvarchar(MAX) not null,			
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

--Executed On Local By Dhrusti 24-08-2022

--------------------------------DocumentTypeMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'DocumentTypeMst')
BEGIN 
	Create table dbo.DocumentTypeMst (
			Id int identity(1,1) primary key,
			DocumentTypeName nvarchar(250) not null,		
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

--Executed On Local By Dhrusti 24-08-2022

--------------------------------ClientTypeMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClientTypeMst')
BEGIN 
	Create table dbo.ClientTypeMst(
			Id int identity(1,1) primary key,
			ClientType nvarchar(100) not null,
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

--------------------------------ServiceProviderMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ServiceProviderMst')
BEGIN 
	Create table dbo.ServiceProviderMst(
			Id int identity(1,1) primary key,
			ServiceProvider nvarchar(100) not null,
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
--------------------------------ServiceProviderTypeMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ServiceProviderTypeMst')
BEGIN 
	Create table dbo.ServiceProviderTypeMst(
			Id int identity(1,1) primary key,
			ServiceProviderType nvarchar(100) not null,
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
--Executed On Local By Preyansi 29-08-2022

 --------------------------------AccessCategoryPermissionMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'AccessCategoryPermissionMst')
BEGIN 
	Create table dbo.AccessCategoryPermissionMst(
			Id int identity(1,1) primary key,
			GroupId int not null, -- Id of AccessCategoryMst where TypeId = 2 / Group
			AccessableCategoryId int not null, -- Id of AccessCategoryMst where TypeId = 3,4,5 / Module, AccessControl, Functionality
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
--Executed On Local By Tanmay 29-08-2022

ALTER TABLE UserMst
ADD IsDeviceApproved bit null;

--ALTER TABLE UserMst ALTER COLUMN IsDeviceApproved bit NULL

ALTER TABLE OfficeMst
ADD CityId int default(0) not null;

--Executed On Local By Tanmay 02-09-2022

 --------------------------------CSVFileUploadLogMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CSVFileUploadLogMst')
BEGIN 
	Create table dbo.CSVFileUploadLogMst(
			Id int identity(1,1) primary key,
			CSVFileName nvarchar(255) not null,
			FileSize nvarchar(50) not null,
			Extension nvarchar(10) not null,
			Path nvarchar(max) not null,
			CreatedBy int not null,
			CreatedDate datetime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

 --------------------------------CSVDataMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CSVDataMst')
BEGIN 
	Create table dbo.CSVDataMst(
			Id int identity(1,1) primary key,
			AccountNo nvarchar(50),
			Surname nvarchar(255),
			Category nvarchar(255),
			InvDate DateTime ,
			Share nvarchar(255),
			Quantity int,
			Price float,
			Value float,
			PercentTot float,
			CSVFileId int not null);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--------------------------------ClientCSVDataLogMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClientCSVDataLogMst')
BEGIN 

	CREATE TABLE [dbo].[ClientCSVDataLogMst](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientCSVFileName] [nvarchar](255) NOT NULL,
	[FileSize] [nvarchar](50) NOT NULL,
	[Extension] [nvarchar](10) NOT NULL,
	[Path] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
			PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO


--Executed On Local By Sonal 01-09-2022

--------------------------------ClientCSVDataMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClientCSVDataMst')
BEGIN 

	CREATE TABLE [dbo].[ClientCSVDataMst](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdviserCode] [nvarchar](255) NULL,
	[ClientName] [nvarchar](255) NULL,
	[RegistrationNumber] [nvarchar](max) NULL,
	[ClientNumber] [int] NULL,
	[Product] [nvarchar](max) NULL,
	[AccountName] [nvarchar](max) NULL,
	[AccountGroups] [nvarchar](max) NULL,
	[AccountNumber] [nvarchar](max) NULL,
	[InceptionDate] [datetime] NULL,
	[FundManager] [nvarchar](max) NULL,
	[FundName] [nvarchar](max) NULL,
	[FundCode] [nvarchar](max) NULL,
	[InitialAdvisorFee] [nvarchar](max) NULL,
	[AnnualAdvisorFee] [nvarchar](max) NULL,
	[Section14AdvisorFeeRenewal] [datetime] NULL,
	[MonthlyDebitOrder] [nvarchar](max) NULL,
	[AnnuityIncomeRegularWithdrawal] [nvarchar](max) NULL,
	[AnnuityIncomeRegularWithdrawalFrequency] [nvarchar](max) NULL,
	[AnnuityIncomeAnniversary] [datetime] NULL,
	[AccountFUndAllocation] [nvarchar](max) NULL,
	[Units] [float] NULL,
	[UnitPrice(Cents)] [float] NULL,
	[PriceDate] [datetime] NULL,
	[FundCurrency] [nvarchar](max) NULL,
	[MarketValueInFundCurrency] [float] NULL,
	[ExchangeRate] [float] NULL,
	[MarketValueInRands] [float] NULL,
	[AnnuitRevisionEffectiveMonth] [nvarchar](max) NULL,
	[NetCapitalGainOrLoss] [nvarchar](max) NULL,
	[ModelPortFolioName] [nvarchar](max) NULL,
	[DimFee] [float] NULL,
	[RicFee] [float] NULL,
	[GroupRAEmployer] [nvarchar](max) NULL,
	[ClientCsvfieldId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
			PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO
----------Executed On Local By Sonal 06-09-2022---------------

--------------------------------CountryCustomMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CountryCustomMst')
BEGIN 
	Create table dbo.CountryCustomMst (
			CountryId int identity(1,1) primary key,
			CountryName nvarchar(255) not null,
			CreatedBy int not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--------------------------------StateCustomMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'StateCustomMst')
BEGIN 
	Create table dbo.StateCustomMst (
			StateId int identity(1,1) primary key,
			StateName nvarchar(255) not null,
			CountryId int not null,
			CreatedBy int not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO


--------------------------------CityCustomMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CityCustomMst')
BEGIN 
	Create table dbo.CityCustomMst (
			CityId int identity(1,1) primary key,
			CityName nvarchar(255) not null,
			StateId int not null,
			CreatedBy int not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO
--Executed On Local By Preyansi 08-09-2022

--Executed On Local By Preyansi 20-09-2022

ALTER TABLE UserMst
ADD MiddleName nvarchar(255);

ALTER TABLE UserMst
ADD Salutation nvarchar(10);

--Executed On Local By Preyansi 20-09-2022

--Executed On Local By sonal 21-09-2022

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClientTransactionMst')
BEGIN
CREATE TABLE [dbo].[ClientTransactionMst](
	[Id] [int] IDENTITY(1,1) primary key,
	[FundName] [nvarchar](max) NOT NULL,
	[Client] [int] NOT NULL,
	[IFA] [int] NOT NULL,
	[IFAUpFrontFee] [int] NOT NULL,
	[IfaAnnualFee] [int] NOT NULL,
	[BuySell] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionAmount] [int] NOT NULL,
	[NumberOfUnits] [int] NOT NULL,
	[UnitPrice] [int] NOT NULL,
	[HWM] [int] NOT NULL,
	[ContingentRedemption] [int] NOT NULL,
	[AllocateTo] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--Executed On Local By sonal 21-09-2022

--Executed On Local By Preyansi 22-09-2022

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundMst')
BEGIN 
	Create table dbo.FundMst (
			Id int identity(1,1) primary key,
			FundRiskRating  int not null,
			IsVATApplicable bit not null,
			VAT numeric(18,2) not null,
			FundName nvarchar(250) not null,
			FundPhilosophy nvarchar(MAX) not null,
			PricingInputs nvarchar(MAX) not null,
			InceptionDate Datetime not null,
			UnitStartingPrice numeric(18,2) not null,
			ManagementFee numeric(18,2) not null,
			PerformanceFee numeric(18,2) not null,
			AuditFee numeric(18,2) not null,
			Currency nvarchar(250) not null,
			ComplianceFee numeric(18,2) not null,
			TrusteesFee numeric(18,2) not null,
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

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundDynamicFieldMst')
BEGIN 
	Create table dbo.FundDynamicFieldMst(
			Id int identity(1,1) primary key,
			FundId int not null, 
			[Label] nvarchar(255) not null, 
			[Value] nvarchar(255) not null, 
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

--Executed On Local By Preyansi 22-09-2022

--Executed On Local By Preyansi 23-09-2022

ALTER TABLE UserMst
ADD IsDueDiligence bit Null;

ALTER TABLE UserMst
ADD IsAML bit Null;
--Executed On Local By Preyansi 23-09-2022

--executed up to here on local by Nikunj 26-9-22----------------

--Executed On Local By Preyansi 23-09-2022

--Executed On Local By Preyansi 26-09-2022

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'DisclaimerMst')
BEGIN 
	Create table dbo.DisclaimerMst(
			Id int identity(1,1) primary key,
			UserId int not null,
			Disclaimer bit, -- if true popup show after 30 days else show every time.
			CreatedOn datetime not null,
			ValidTill datetime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

--Executed On Local By Preyansi 26-09-2022

--Executed On Local By Preyansi 27-09-2022
--------------------------------PermissionCredentialMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PermissionCredentialMst')
BEGIN 
	Create table dbo.PermissionCredentialMst(
			Id int identity(1,1) primary key,
			AccessCategoryId int not null,
			Pwd nvarchar(255) not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO
--Executed On Local By Preyansi 27-09-2022
--Executed On Local By Preyansi 26-09-2022


--Executed On Local By Tanmay 23-09-2022

ALTER TABLE UserMst
ADD DueDiligenceUpdatedDate datetime Null;

ALTER TABLE UserMst
ADD AMLUpdatedDate datetime Null;

ALTER TABLE UserMst
ADD WelcomeMailSent bit Null;

ALTER TABLE FundDynamicFieldMst
ADD RowId bit Null;

--Executed On Local By Tanmay 23-09-2022

--Executed On QA Server By Tanmay 27-09-2022

ALTER TABLE FundDynamicFieldMst
DROP COLUMN RowId;

ALTER TABLE FundDynamicFieldMst
ADD RowId int Null;

ALTER TABLE FundMst
ADD IsFactSheetCreated bit Default(0) Null;

--Executed On Local By Tanmay 27-09-2022
--Executed On Local By Preyansi 23-09-2022


--------------------------------FactSheetMst---------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FactSheetMst')
BEGIN 

	CREATE TABLE dbo.FactSheetMst(
	Id int identity(1,1) primary key,
	FundId int NOT NULL,
	InvestmentObjective nvarchar(MAX) NOT NULL,
	PortfolioManager nvarchar(250) NOT NULL,
	Email nvarchar(250) NOT NULL,
	FSP nvarchar(250) NOT NULL,
	Telephone nvarchar(250) NOT NULL,
	InceptionDate DateTime NOT NULL,
	Sector nvarchar(250) NOT NULL,
	Target nvarchar(MAX) NOT NULL,
	ParticipatoryStructure nvarchar(MAX) NOT NULL,
	MinInvestment nvarchar(250) NOT NULL,
	PerformanceFee nvarchar(250) NULL,
	BaseFee nvarchar(250) NOT NULL,
	FeeHurdle nvarchar(MAX) NOT NULL,
	SharingRatio nvarchar(250) NOT NULL,
	FeeExample nvarchar(MAX) NOT NULL,
	Method nvarchar(MAX) NOT NULL,
	IsActive bit NOT NULL,
	IsDeleted bit NOT NULL,
	CreatedBy int NULL,
	UpdatedBy int NULL,
	CreatedDate datetime NULL,
	UpdatedDate datetime NULL
	);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

Alter table FactSheetMst Add Recommended nvarchar(250);

Alter table FactSheetMst Add ShortCommentary nvarchar(MAX);

Alter table FactSheetMst Add Disclaimer nvarchar(MAX);



--Executed On Local By Dhrusti 26-09-2022


--Executed On DEV Server By Tanmay 28-09-2022

--Executed On UAT Server By Tanmay 28-09-2022

--Executed On UAT Server By preyansi 03-10-2022
--------------------------------DueDiligenceMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'DueDiligenceMst')
BEGIN 
	Create table dbo.DueDiligenceMst(
			Id int identity(1,1) primary key,
			UserId int not null,
			CreatedOn datetime not null,
			ValidTill datetime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'AMLMst')
BEGIN 
	Create table dbo.AMLMst(
			Id int identity(1,1) primary key,
			UserId int not null,
			CreatedOn datetime not null,
			ValidTill datetime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

--Executed On UAT Server By Preyansi 03-10-2022

--Executed On UAT Server By Tanmay 28-09-2022

--Executed On QA Server By Tanmay 10-10-2022
------------------------------Added By Dhrusti 03-10-2022-------------------------------

Drop table IFAMst;

--------------------------------IFAMst ---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'IFAMst')
BEGIN 
	Create table dbo.IFAMst (
			Id int identity(1,1) primary key,
			IFA nvarchar(100) not null,
			FSCARegistrationNo nvarchar(250) not null,
			IFAPractiseNo nvarchar(250) not null,
			ResponsiblePersonTitle nvarchar(250) not null,
			FirstName nvarchar(250) not null,
			Surname nvarchar(250) not null,
			PositionHeld nvarchar(250) not null,
			DateOfBirth DateTime not null,
			CompanyName nvarchar(250) not null,
			CompRegNumber nvarchar(250) not null,
			SARSTaxNumber nvarchar(250) not null,
			VATNumber nvarchar(250) not null,
			BuildingName nvarchar(250) not null,
			FloorOfficeNumber nvarchar(250) not null,
			StreetName nvarchar(250) not null,
			Suburb nvarchar(250) not null,
			City nvarchar(250) not null,
			Province nvarchar(250) not null,
			Postalcode nvarchar(250) not null,
			IsFSCARegistration bit not null, 
			LastDate nvarchar(250) not null,
			PersonChecked nvarchar(250) not null,
			Consultant nvarchar(250) not null,
			MobileNumber nvarchar(250) not null,
			WorkNumber nvarchar(250) not null,
			Email nvarchar(250) not null,
			Notepad nvarchar(MAX) not null,
			ProfilePictutePath nvarchar(MAX) not null,
			DocumentPath nvarchar(MAX) not null,
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

--Executed On Local By Dhrusti 04-10-2022
--Executed On Local By Preyansi 13-10-2022
ALTER TABLE UserMst
ADD IsProminentPolitical bit null;
--Executed On Local By Preyansi 13-10-2022

--====================================================================================--
--===================================== Sprint 3 =====================================--
--====================================================================================--


--------------------------------------------------------- ClientTransactionMst -----------------------------------------------------------------

Drop Table ClientTransactionMst;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClientTransactionMst')
BEGIN 
	Create table dbo.ClientTransactionMst (
			Id int identity(1,1) primary key,
			Fund int not null,
			Client int not null,
			IFA int not null,
			IFAUpFrontFee float not null,
			IfaAnnualFee float not null,
			TransactionType varchar(10) not null,
			TransactionIn varchar(10) not null,
			TransactionDate datetime not null,
			TransactionAmount float not null,
		    NumberOfUnits float not null ,
			UnitPrice float not null ,
            AllocateTo nvarchar(255) not null,
            IsActive bit not null,
            IsDeleted bit not null,
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

--------------------------------------------------------- FundMst -----------------------------------------------------------------

Truncate table FundDynamicFieldMst
Drop Table FundMst;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundMst')
BEGIN 
	Create table dbo.FundMst (
			Id int identity(1,1) primary key,
			FundRiskRating  int not null,
			IsVATApplicable bit not null,
			VAT float not null,
			FundName nvarchar(250) not null,
			FundPhilosophy nvarchar(MAX) not null,
			PricingInputs nvarchar(MAX) not null,
			InceptionDate Datetime not null,
			UnitStartingPrice float not null,
			ManagementFeeA float not null,
			ManagementFeeB float not null,
			ManagementFeeC float not null,
			PerformanceFeeA float not null,
			PerformanceFeeB float not null,
			PerformanceFeeC float not null,
			AuditFee float not null,
			Currency nvarchar(250) not null,
			ComplianceFee float not null,
			TrusteesFee float not null,
			IsFactSheetCreated bit default(0) not null,
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

---------------------------------------------------------FactSheet Changes-----------------------------------------------------------------

Drop table FactSheetMst;


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FactSheetMst')
BEGIN 

	CREATE TABLE dbo.FactSheetMst(
	Id int identity(1,1) primary key,
	FundId int NOT NULL,
	InvestmentObjective nvarchar(MAX) NOT NULL,
	PortfolioManager nvarchar(250) NOT NULL,
	Email nvarchar(250) NOT NULL,
	FSP nvarchar(250) NOT NULL,
	Telephone nvarchar(250) NOT NULL,
	InceptionDate DateTime NOT NULL,
	Sector nvarchar(250) NOT NULL,
	Target nvarchar(MAX) NOT NULL,
	ParticipatoryStructure nvarchar(MAX) NOT NULL,
	MinInvestment float NOT NULL,
    AnnualFeesUnitA float NOT NULL,
	AnnualFeesUnitB float NOT NULL,
	AnnualFeesUnitC float NOT NULL,
	BaseFee float NOT NULL,
	Recommended nvarchar(250) NOT NULL,
	ShortCommentary nvarchar(MAX) NOT NULL,
	Disclaimer nvarchar(MAX) NOT NULL,
	FeeHurdle nvarchar(MAX) NOT NULL,
	PerformanceFeesUnitA float NOT NULL, 
	PerformanceFeesUnitB float NOT NULL, 
	PerformanceFeesUnitC float NOT NULL, 
	FeeExample nvarchar(MAX) NOT NULL,
	Method nvarchar(MAX) NOT NULL,
	IsActive bit NOT NULL,
	IsDeleted bit NOT NULL,
	CreatedBy int NULL,
	UpdatedBy int NULL,
	CreatedDate datetime NULL,
	UpdatedDate datetime NULL
	);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO
--Executed On Physical Server By Tanmay 18-10-2022--

--------------------Added fields in UserMst for IFA--------------------------

Alter table UserMst Add FSCA int;
Alter table UserMst Add CompanyName nvarchar(250);
Alter table UserMst Add CompRegNumber int;
Alter table UserMst Add VATNo int;
Alter table UserMst Add FloorandOfficeNo int;
Alter table UserMst Add IsFSCAactive bit;
Alter table UserMst Add LastDateChecked DateTime;
Alter table UserMst Add PersonChecked nvarchar(250);
Alter table UserMst Add AnnualAdvisorFees nvarchar(250);


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


--Executed On Physical Server By Tanmay 28-10-2022--
--Executed On DEV Server By Tanmay 28-10-2022--
--Executed On QA Server By Tanmay 28-10-2022--
--Executed On UAT Server By Tanmay 28-10-2022--

Alter table UserMst
Alter Column FSCA nvarchar(50)

Alter table UserMst
Alter Column CompRegNumber nvarchar(50)

Alter table UserMst
Alter Column VATno nvarchar(50)

Alter table UserMst
Alter Column FloorandOfficeNo nvarchar(50)


--Executed On Physical Server By Tanmay 28-10-2022--
--Executed On DEV Server By Tanmay 28-10-2022--
--Executed On QA Server By Tanmay 28-10-2022--
--Executed On UAT Server By Tanmay 28-10-2022--



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

--Executed On Local By Sonal 07-11-2022

Alter table RoleMst
Add RoleDescription nvarchar(50) null

--Executed On Local By Sonal 14-11-2022

--Executed On Local By Preyansi 14-11-2022
ALTER TABLE ClientTransactionMst 
ADD  UnitType nvarchar(50);
--Executed On Local By Preyansi 14-11-2022

ALTER TABLE RoleMst
ALTER COLUMN RoleDescription nvarchar(Max) null;

--Executed On Local By Sonal 15-11-2022

--Executed On Local By Preyansi 15-11-2022
ALTER TABLE ClientTransactionMst
ADD UnitBalance decimal(38,8) null DEFAULT 0;

ALTER TABLE ClientTransactionMst
ADD AmountBalance decimal(38,8) null DEFAULT 0;

ALTER TABLE ClientTransactionMst
ADD TransactionDateTime  datetime;

--Executed On Local By Preyansi 15-11-2022

--ALTER TABLE RoleMst
--ALTER COLUMN RoleDescription nvarchar(Max) null;

ALTER TABLE UserMst
ADD Role nvarchar(50) null


--Executed On Physical Server By Tanmay 17-11-2022--
--Executed On DEV Server By Tanmay 17-11-2022--
--Executed On QA Server By Tanmay 18-11-2022--
--Executed On DEV Server By Tanmay 17-11-2022--


--------------------------------CurrencyMst---------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CurrencyMst')
BEGIN 
	Create table dbo.CurrencyMst (
			Id int identity(1,1) primary key,
			CurrencyName nvarchar(50) not null,
			Symbol nvarchar(MAX) not null,
			BaseValue nvarchar(250) not null,
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


--Executed On Local By Dhrusti 17-11-2022
--Executed on Dev By Ajay Zala 21-11-2022

--------------------------------UserMst---------------------------------
--Created By Ajay Zala 21-11-2022---------------------------------------


--Add New Column IdNumber In UserMst;

ALTER TABLE UserMst
ADD IDNumber nvarchar(50) null

--Executed On Physical By Ajay 21-11-2022
--Executed on Dev By Ajay Zala 21-11-2022

--Added by NP 21-11-2022-------------------------------Start--------------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundCurrentBalanceMst')
BEGIN 
	Create table dbo.FundCurrentBalanceMst(
			Id int identity(1,1) primary key,
			FundId int not null,
			UnitPrice decimal(38,8) not null,
			FundBalance decimal(38,8) not null,
			UnitBalance decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundDynamicInputPriceMst')
BEGIN 
	Create table dbo.FundDynamicInputPriceMst(
			Id int identity(1,1) primary key,
			FundId int not null, 
			[Label] nvarchar(255) not null, 
			[Value] decimal(38,8) not null, 
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

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PricingMst')
BEGIN 
	Create table dbo.PricingMst(
			Id int identity(1,1) primary key,
			FundId int not null,
			InceptionDate DateTime null,
			TransactionDate DateTime null,
			DynPrcInpFundId int not null,
			UnitType nvarchar(255) not null,
			--FundDynamicFieldId int not null,
			DynPrcInpTotal decimal(38,8) not null,
			Units decimal(38,8) not null,
			HWM decimal(38,8) not null,
			ManFees decimal(38,8) not null,
			PerfFees decimal(38,8) not null,
			ComplianceFees decimal(38,8) not null,
			AuditFees decimal(38,8) not null,
			IFAFees decimal(38,8) not null,
			UnitPriceNav decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--Added by NP 21-11-2022-------------------------------End--------------------------------------------
--Executed on Physical server up to here on 21-11-22----------NP

--Added by NP 23-11-2022-------------------------------Start--------------------------------------------
drop table FundCurrentBalanceMst
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundCurrentBalanceMst')
BEGIN 
	Create table dbo.FundCurrentBalanceMst(
			Id int identity(1,1) primary key,
			FundId int not null,
			BalanceDate datetime not null,
			DynPrcInpId int not null,
			UnitType nvarchar(255) not null,
			FundBalance decimal(38,8) not null,
			UnitBalance decimal(38,8) not null,
			UnitPrice decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO
--Added by NP 23-11-2022-------------------------------End--------------------------------------------
--Executed on Physical server up to here on 23-11-22----------NP

--Added by NP 24-11-2022-------------------------------Start--------------------------------------------
drop table FundCurrentBalanceMst
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundCurrentBalanceMst')
BEGIN 
	Create table dbo.FundCurrentBalanceMst(
			Id int identity(1,1) primary key,
			FundId int not null,
			BalanceDate datetime not null,
			DynPrcInpId int not null,
			DynPrcInpLabel nvarchar(255) not null,
			UnitType nvarchar(255) not null,
			FundBalance decimal(38,8) not null,
			UnitBalance decimal(38,8) not null,
			UnitPrice decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

drop table FundDynamicInputPriceMst

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundDynamicInputPriceMst')
BEGIN 
	Create table dbo.FundDynamicInputPriceMst(
			Id int identity(1,1) primary key,
			FundId int not null, 
			[Label] nvarchar(255) not null, 
			[Value] decimal(38,8) not null, 
			BalanceDate Datetime not null,
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

drop table FundCurrentBalanceMst
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundCurrentBalanceMst')
BEGIN 
	Create table dbo.FundCurrentBalanceMst(
			Id int identity(1,1) primary key,
			FundId int not null,
			BalanceDate datetime not null,
			DynPrcInpId int not null,
			DynPrcInpLabel nvarchar(255) not null,
			UnitType nvarchar(255) not null,
			FundBalance decimal(38,8) not null,
			UnitBalance decimal(38,8) not null,
			UnitPrice decimal(38,8) not null,
			HWM decimal(38,8) not null,
			ManFees decimal(38,8) not null,
			PerfFees decimal(38,8) not null,
			ComplianceFees decimal(38,8) not null,
			AuditFees decimal(38,8) not null,
			IFAInitialFee decimal(38,8) not null,
			IFAAnnualFee decimal(38,8) not null,
			IFAFees decimal(38,8) not null,
			UnitPriceNav decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO
--Added by NP 24-11-2022-------------------------------End--------------------------------------------
--Executed on Physical server up to here on 24-11-22----------NP

--Added by NP 25-11-2022-------------------------------Start--------------------------------------------
drop table FundDynamicInputPriceMst

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundDynamicInputPriceMst')
BEGIN 
	Create table dbo.FundDynamicInputPriceMst(
			Id int identity(1,1) primary key,
			FundId int not null, 
			UnitType nvarchar(255) not null,
			[Label] nvarchar(255) not null, 
			[Value] decimal(38,8) not null, 
			BalanceDate Datetime not null,
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
--Added by NP 25-11-2022-------------------------------End--------------------------------------------
--Executed on Physical server up to here on 25-11-22----------NP
--Executed on Dev By Ajay Zala 21-11-2022


ALTER TABLE UserMst
ADD WCFundAdministration bit null

Drop table CSVDataMst
 --------------------------------CSVDataMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CSVDataMst')
BEGIN 
	Create table dbo.CSVDataMst(
			Id int identity(1,1) primary key,
			AccountNo nvarchar(50) null,
			Surname nvarchar(255) null,
			Category nvarchar(255) null,
			InvDate DateTime null,
			Share nvarchar(255) null,
			Quantity int null,
			Price float null,
			Value float null,
			PercentTot float null,
			CSVFileId int not null);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

Drop table ClientCsVDataMSt

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClientCSVDataMst')
BEGIN 

	CREATE TABLE [dbo].[ClientCSVDataMst](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdviserCode] [nvarchar](255) NULL,
	[ClientName] [nvarchar](255) NULL,
	[RegistrationNumber] [nvarchar](max) NULL,
	[ClientNumber] [int] NULL,
	[Product] [nvarchar](max) NULL,
	[AccountName] [nvarchar](max) NULL,
	[AccountGroups] [nvarchar](max) NULL,
	[AccountNumber] [nvarchar](max) NULL,
	[InceptionDate] [datetime] NULL,
	[FundManager] [nvarchar](max) NULL,
	[FundName] [nvarchar](max) NULL,
	[FundCode] [nvarchar](max) NULL,
	[InitialAdvisorFee] [nvarchar](max) NULL,
	[AnnualAdvisorFee] [nvarchar](max) NULL,
	[Section14AdvisorFeeRenewal] [datetime] NULL,
	[MonthlyDebitOrder] [nvarchar](max) NULL,
	[AnnuityIncomeRegularWithdrawal] [nvarchar](max) NULL,
	[AnnuityIncomeRegularWithdrawalFrequency] [nvarchar](max) NULL,
	[AnnuityIncomeAnniversary] [datetime] NULL,
	[AccountFUndAllocation] [nvarchar](max) NULL,
	[Units] [float] NULL,
	[UnitPrice(Cents)] [float] NULL,
	[PriceDate] [datetime] NULL,
	[FundCurrency] [nvarchar](max) NULL,
	[MarketValueInFundCurrency] [float] NULL,
	[ExchangeRate] [float] NULL,
	[MarketValueInRands] [float] NULL,
	[AnnuitRevisionEffectiveMonth] [nvarchar](max) NULL,
	[NetCapitalGainOrLoss] [nvarchar](max) NULL,
	[ModelPortFolioName] [nvarchar](max) NULL,
	[DimFee] [float] NULL,
	[RicFee] [float] NULL,
	[GroupRAEmployer] [nvarchar](max) NULL,
	[ClientCsvfieldId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
			PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--Executed On Staging By Tanmay 28-11-2022

 --------------------------------FundBenchMarkMst---------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundBenchMarkMst')
BEGIN 
	Create table dbo.FundBenchMarkMst (
			Id int identity(1,1) primary key,
			FundId int not null,
			BenchMarkName nvarchar(255) not null,
			BenchMarkDate datetime not null,
			BenchMarkValue decimal(38,8) not null,
			IsAddMode	bit	not null default(1),
			IsInDashboard	bit		null,	
			IsActive	bit		not null default(0),
			IsDeleted	bit		not null default(1),
			CreatedDate	datetime not null,	
			UpdatedDate	datetime not null,	
			CreatedBy	int		not null,	
			UpdatedBy	int		not null	
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--Executed On Local By Sonal 14-12-2022

--Added by DS 05-12-2022-------------------------------Start--------------------------------------------
 --------------------------------MeetingMst---------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'MeetingMst')
BEGIN 
	Create table dbo.MeetingMst(
			Id int identity(1,1) primary key,
			ReminderDate Datetime not null,
			ReminderTime Datetime not null,
			Venue nvarchar(255) not null,
			Attendees nvarchar(MAX) not null,
			ClientAction nvarchar(MAX) not null,
			WaltCapitalActions nvarchar(MAX) not null,
			Discussion nvarchar(MAX) not null,
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

--Added by DS 05-12-2022-------------------------------End--------------------------------------------

--------------------------------DailyEventEmailMst---------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'DailyEventEmailMst')
BEGIN 
	Create table dbo.DailyEventEmailMst(
			Id int identity(1,1) primary key,
			SenderId int not null,
			ReceiverId int not null,
			Gender nvarchar(250) not null,
			EmailFor nvarchar(250) not null,
			Subject nvarchar(MAX) not null,
			UploadTemplate nvarchar(MAX) not null,
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
--Executed On Local By Dhrusti 15-12-2022
--Executed On Staging By Tanmay 28-11-2022

--added by Nikunj on 15-12-2022-----------------------------------Start-----------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClientTransactionDetails')
BEGIN 
	Create table dbo.ClientTransactionDetails(
			Id int identity(1,1) primary key,
			TransactionId int not null,
			UnitBalance decimal(38,8) not null,
			AmountBalance decimal(38,8) not null,
			UnitPrice decimal(38,8) not null,
			IFAUpFrontFee decimal(38,8) not null,
			IFAAnnualFee decimal(38,8) not null,
			IFAFees decimal(38,8) not null,
			ManFees decimal(38,8) not null,
			PerfFees decimal(38,8) not null,
			TotalFees decimal(38,8) not null,
			HWM decimal(38,8) not null,
			UnitBalanceWithFee decimal(38,8) not null,
			AmountBalanceWithFee decimal(38,8) not null,
			UnitPriceWithFee decimal(38,8) not null,
			UnitPriceNav decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundTotalBalanceMst')
BEGIN 
	Create table dbo.FundTotalBalanceMst(
			Id int identity(1,1) primary key,
			FundId int not null,
			BalanceDate datetime not null,
			UnitBalance decimal(38,8) not null,
			UnitPrice decimal(38,8) not null,
			AmountBalace decimal(38,8) not null,
			UnitBalanceWithFees decimal(38,8) not null,
			UnitPriceWithFees decimal(38,8) not null,
			AmountBalaceWithFees decimal(38,8) not null,
			ComplianceFees decimal(38,8) not null,
			AuditFees decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO
--added by Nikunj on 15-12-2022-----------------------------------Start-----------------------------------
--Executed on physical server in 15-12-2022 by Nikunj-----------------------------------------------------

--added on 20-12-22-----------------by Nikunj--------------------START-----------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundFeeCalculationDetails')
BEGIN 
	Create table dbo.FundFeeCalculationDetails(
			Id int identity(1,1) primary key,
			FundId int not null,
			BalanceDate datetime not null,
			DynPrcInpId int not null,
			DynPrcInpLabel nvarchar(255) not null,
			UnitType nvarchar(255) not null,
			StartValue decimal(38,8) not null,
			FeesPayable decimal(38,8) not null,
			FeesPaid decimal(38,8) not null,
			TotalFeesDue decimal(38,8) not null,
			HWM decimal(38,8) not null,
			ProfitOffHWM decimal(38,8) not null,
			PreUnits decimal(38,8) not null,
			UnitChanges decimal(38,8) not null,
			PostUnits decimal(38,8) not null,
			UnitPrice decimal(38,8) not null,
			BankBalance decimal(38,8) not null,
			TSTotalValue decimal(38,8) not null,
			TSTheoValue decimal(38,8) not null,
			TotalTheorValue decimal(38,8) not null,
			ManFees decimal(38,8) not null,
			PerfFees decimal(38,8) not null,
			ComplianceFees decimal(38,8) not null,
			AuditFees decimal(38,8) not null,
			IFAInitialFee decimal(38,8) not null,
			IFAAnnualFee decimal(38,8) not null,
			IFAFees decimal(38,8) not null,
			UnitPriceNav decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
go

alter table FundDynamicInputPriceMst
add IsAddedFromPricing bit default(0) not null
go

alter table dbo.PricingMst
add TotalTheorValue decimal(38,8) not null default(0)

alter table dbo.PricingMst
add FeesPayable decimal(38,8) not null default(0)

alter table dbo.PricingMst
add TotalFeesDue decimal(38,8) not null default(0)


--added on 20-12-22-----------------by Nikunj--------------------END-----------------------------------------
--Executed on physical server by Nikunj on 20-12-22----------------------------------------------------------

drop table dbo.FundTotalBalanceMst
drop table dbo.ClientTransactionDetails
drop table dbo.FundCurrentBalanceMst

--executed on physical and dev server up to here---------------------------on 23-12-22----------------by NP and TS--------------------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundFeesMst')
BEGIN 
	Create table dbo.FundFeesMst(
			Id int identity(1,1) primary key,
			FundId int not null,
			FeesName nvarchar(255) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'RunFeesDetail')
BEGIN 
	Create table dbo.RunFeesDetail(
			Id int identity(1,1) primary key,
			FundId int not null,
			Feesid int not null,
			LastRunDate datetime not null,
			LastAmount decimal(38,8) not null,
			NextRunDate datetime null,
			PendingAmount decimal(38,8) not null,
			Total decimal(38,8) not null,
			VAT decimal(38,8) not null,
			TotalIncVAT decimal(38,8) not null,
			IsActive bit default(1) not null,
			IsDeleted bit default(0) not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

insert into dbo.FundFeesMst values(0,'Audit Fee',1,0,1,1,getdate(),getdate())
insert into dbo.FundFeesMst values(0,'Compliance Fee',1,0,1,1,getdate(),getdate())
insert into dbo.FundFeesMst values(0,'Management Fee',1,0,1,1,getdate(),getdate())
insert into dbo.FundFeesMst values(0,'Performance Fee',1,0,1,1,getdate(),getdate())
insert into dbo.FundFeesMst values(0,'IFA Fee',1,0,1,1,getdate(),getdate())

--Executed On DEV Server By Tanmay 26-12-2022--
--Executed On QA Server By Tanmay 26-12-2022--
--Executed On STAGE Server By Tanmay 26-12-2022--
--Executed On UAT Server By Tanmay 26-12-2022--

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundHistoryMst')
BEGIN 
	Create table dbo.FundHistoryMst (
			Id int identity(1,1) primary key,
			FundId int not null,
			FundRiskRating  int not null,
			IsVATApplicable bit not null,
			VAT float not null,
			FundName nvarchar(250) not null,
			FundPhilosophy nvarchar(MAX) not null,
			PricingInputs nvarchar(MAX) not null,
			InceptionDate Datetime not null,
			UnitStartingPrice float not null,
			ManagementFeeA float not null,
			ManagementFeeB float not null,
			ManagementFeeC float not null,
			PerformanceFeeA float not null,
			PerformanceFeeB float not null,
			PerformanceFeeC float not null,
			AuditFee float not null,
			Currency nvarchar(250) not null,
			ComplianceFee float not null,
			TrusteesFee float not null,
			IsFactSheetCreated bit default(0) not null,
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

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FundDynamicFieldHistoryMst')
BEGIN 
	Create table dbo.FundDynamicFieldHistoryMst(
			Id int identity(1,1) primary key,
			DynamicFieldId int not null,
			FundId int not null, 
			Label nvarchar(255) not null, 
			Value nvarchar(255) not null, 
			RowId int,
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

--Executed On Physical Server By Tanmay 03-01-2023--
--Executed On Dev Server By Ajay 10-01-2023--
--Executed On QA Server By Ajay 10-01-2023--
--Executed On Staging Server By Ajay 10-01-2023--
--Executed On UAT Server By Ajay 10-01-2023--

ALTER TABLE FactSheetMst 
ALTER COLUMN BaseFee Float NULL; 

--Executed On Physical Server By Dhrusti 25-01-2023--

alter table CsvFileuploadlogmst
Add ServiceProviderId int  

--Executed On Physical Server By preyansi 24-01-2023--