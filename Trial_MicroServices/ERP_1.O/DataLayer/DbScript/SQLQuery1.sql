------------------------------Added By Dhrusti 29-03-2023-------------------------------

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