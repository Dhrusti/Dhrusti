
-- Use this after creating the table UserMst for Admin User
--insert into UserMst values(NULL, 1, 0000001, 123, 1, 1, N'Trevor', 'Noah', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'trevor_noah@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL)
--insert into UserMst values(NULL, 1, 0000002, 123, 1, 1, N'QA', 'TEST', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'qa@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL)
--insert into UserMst values(NULL, 1, 0000003, 123, 1, 1, N'Tanmay', 'Sadamast', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'tanmay@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL)
--insert into UserMst values(NULL, 1, 0000004, 123, 1, 1, N'Sunil', 'Salat', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'sunil@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL)


-- Use this after creating the table AccessCategoryTypeMst
insert into AccessCategoryTypeMst values ('Roles', 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryTypeMst values ('Group', 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryTypeMst values ('Module', 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryTypeMst values ('AccessControl', 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryTypeMst values ('Functionality', 1, 0, 1, 1, GETDATE(), GETDATE());

--Truncate table CSVDATAMST
--Truncate table CSVFileUploadLogMst


-- Use this after creating the table AccessCategoryMst
--insert into AccessCategoryMst values ('SuperAdmin', 0, 1, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Clients', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Developers', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Fund Managers', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Super Users', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Clients', 0, 3, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Access Control', 6, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Functionality', 6, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Add New Client', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Filter Clients', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Show All Clients', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())

--insert into AccessCategoryMst values ('CRM', 0, 3, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Access Control', 12, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Functionality', 12, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Edit CRM Information', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Link Account To Profile', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('New Link Account', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Remove Photo', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Remove The Selected Linked Account From This Profile', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('New Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('UPdate Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
--insert into AccessCategoryMst values ('Delete Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())

----------------------------------------------------------------------------------------------------------------------------


insert into AccessCategoryMst values ('SuperAdmin', 0, 1, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Clients', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Developers', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Fund Managers', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Super Users', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Clients', 0, 3, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Access Control', 6, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Functionality', 6, 5, 1, 0, 1, 1, GETDATE(), GETDATE())


insert into AccessCategoryMst values ('Add New Client', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Filter Clients', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Show All Clients', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())

insert into AccessCategoryMst values ('CRM', 0, 3, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Access Control', 12, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Functionality', 12, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Edit CRM Information', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Link Account To Profile', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('New Link Account', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Remove Photo', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Remove The Selected Linked Account From This Profile', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('New Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Update Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Delete Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())

insert into AccessCategoryMst values ('New Account', 7, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Update Account', 7, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Delete Account', 7, 4, 1, 0, 1, 1, GETDATE(), GETDATE())


-- Use this after creating the table AccessCategoryPermissionMst
insert into AccessCategoryPermissionMst values (2, 9, 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryPermissionMst values (2, 11, 1, 0, 1, 1, GETDATE(), GETDATE());


insert into AccountTypeMst values('AccountType 1', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccountTypeMst values('AccountType 2', 1, 0, 1, 1, GETDATE(), GETDATE())
select * from AccountTypeMst

insert into ClientTypeMst values('ClientType 1', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into ClientTypeMst values('ClientType 2', 1, 0, 1, 1, GETDATE(), GETDATE())
select * from ClientTypeMst

insert into PersonalityTypeMst values('PersonalityType 1', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into PersonalityTypeMst values('PersonalityType 2', 1, 0, 1, 1, GETDATE(), GETDATE())
select * from PersonalityTypeMst

insert into WaltCapConsultantMst values('WaltConsultant 1', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into WaltCapConsultantMst values('WaltConsultant 2', 1, 0, 1, 1, GETDATE(), GETDATE())
select * from WaltCapConsultantMst

insert into IFAMst values('IFA 1', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into IFAMst values('IFA 2', 1, 0, 1, 1, GETDATE(), GETDATE())
select * from IFAMst

insert into ServiceProviderMst values('Allan Gray', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into ServiceProviderMst values('Interactive Brokers', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into ServiceProviderMst values('PPM', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into ServiceProviderMst values('Trade Station', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into ServiceProviderMst values('Walt Capital Fund', 1, 0, 1, 1, GETDATE(), GETDATE())
select * from ServiceProviderMst

insert into ServiceProviderTypeMst values('Pension', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into ServiceProviderTypeMst values('RA', 1, 0, 1, 1, GETDATE(), GETDATE())
insert into ServiceProviderTypeMst values('TFSA', 1, 0, 1, 1, GETDATE(), GETDATE())
select * from ServiceProviderTypeMst

-- New Coluns Added

Truncate table UserMst

insert into UserMst values(NULL, 1, 0000001, 123, 1, 1, N'Trevor', 'Noah', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'trevor_noah@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL)
insert into UserMst values(NULL, 1, 0000002, 123, 1, 1, N'QA', 'TEST', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'qa@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL)
insert into UserMst values(NULL, 1, 0000003, 123, 1, 1, N'Tanmay', 'Sadamast', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'tanmay@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL)
insert into UserMst values(NULL, 1, 0000004, 123, 1, 1, N'Sunil', 'Salat', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'sunil@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL)

-- New Columns Added.

--added by NIkunj on 26-9-2022-----------------------------------------------Start
truncate table dbo.AccessCategoryTypeMst

truncate table dbo.AccessCategoryMst
go

insert into AccessCategoryTypeMst values ('Roles', 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryTypeMst values ('Group', 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryTypeMst values ('Module', 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryTypeMst values ('AccessControl', 1, 0, 1, 1, GETDATE(), GETDATE());
insert into AccessCategoryTypeMst values ('Functionality', 1, 0, 1, 1, GETDATE(), GETDATE());
go

insert into AccessCategoryMst values ('SuperAdmin', 0, 1, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Clients', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Developers', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Fund Managers', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Super Users', 0, 2, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Clients', 0, 3, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Access Control', 6, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Functionality', 6, 5, 1, 0, 1, 1, GETDATE(), GETDATE())


insert into AccessCategoryMst values ('Add New Client', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Filter Clients', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Show All Clients', 8, 5, 1, 0, 1, 1, GETDATE(), GETDATE())

insert into AccessCategoryMst values ('CRM', 0, 3, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Access Control', 12, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Functionality', 12, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Edit CRM Information', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Link Account To Profile', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('New Link Account', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Remove Photo', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Remove The Selected Linked Account From This Profile', 14, 5, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('New Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Update Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Delete Account', 13, 4, 1, 0, 1, 1, GETDATE(), GETDATE())

insert into AccessCategoryMst values ('New Account', 7, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Update Account', 7, 4, 1, 0, 1, 1, GETDATE(), GETDATE())
insert into AccessCategoryMst values ('Delete Account', 7, 4, 1, 0, 1, 1, GETDATE(), GETDATE())

--added by NIkunj on 26-9-2022-----------------------------------------------End
--Executed on Local by Nikunj on 26-9-2022-----------------------------------

--added by Tanmay on 26-9-2022-----------------------------------------------Start

insert into FundMst values(1, 1, 1, 'Aggregate', 'No Philosophy', 'A, B, C, D', GETDATE(), 10, 10, 10 , 10, 'USD', 100, 10, 1, 0, 1, 1, GETDATE(), GETDATE())

insert into FundDynamicFieldMst values(1, 'lb1', 'val1', 1, 0, 1, 1, GETDATE(), GETDATE(), 1)
insert into FundDynamicFieldMst values(1, 'lb2', 'val2', 1, 0, 1, 1, GETDATE(), GETDATE(), 1)
insert into FundDynamicFieldMst values(1, 'Bank Acc.', '023423423', 1, 0, 1, 1, GETDATE(), GETDATE(), 1)
insert into FundDynamicFieldMst values(1, 'Stock Acc.', '23234234', 1, 0, 1, 1, GETDATE(), GETDATE(), 1)

--added by Tanmay on 26-9-2022-----------------------------------------------End
--Executed on Local by Tanmay on 26-9-2022-------------------------------------Executed on Local by Nikunj on 26-9-2022-----------------------------------


--added by NIkunj on 28-9-2022-----------------------------------------------Start
--Executed on Local by Nikunj on 28-9-2022-----------------------------------
Truncate table UserMst

insert into UserMst values(NULL, 1, 0000001, 123, 1, 1, N'Trevor', 'Noah', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'trevor_noah@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL,null,null)
insert into UserMst values(NULL, 1, 0000002, 123, 1, 1, N'QA', 'TEST', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'qa@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL,null,null)
insert into UserMst values(NULL, 1, 0000003, 123, 1, 1, N'Tanmay', 'Sadamast', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'tanmay@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL,null,null)
insert into UserMst values(NULL, 1, 0000004, 123, 1, 1, N'Sunil', 'Salat', 1, GETDATE(), N'12345', N'8888888888', N'7777777777', N'sunil@gmail.com', N'123', 1, N'1', N'1', N'1', N'1', N'1', 1, N'324333', N'1', N'1', 1, N'1', N'1', N'1', 1, 1, 1, 1, N'1', 1, N'1', GETDATE(), N'1', N'1', N'1', 1, 1, 1, 1, N'1', N'1', N'1', N'1', N'1', N'1', N'1', 1, 1, NULL, 1, 0, 1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL,null,null)
--added by NIkunj on 28-9-2022-----------------------------------------------End
--Executed on Local by Nikunj on 28-9-2022-----------------------------------


insert into PermissionCredentialMst values('1', 'WNWabGVctOyyQFnVyMvEkg==');