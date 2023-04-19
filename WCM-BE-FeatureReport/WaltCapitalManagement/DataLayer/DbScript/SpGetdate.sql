create or alter PROCEDURE Get_DateInformation	@StartDate datetime,	@EndDate datetimeASBegin	--SELECT  @StartDate FirstParam,	--        @EndDate SecondParam	--select * from #Results	--set @StartDate = dateadd(day,1,getdate());	--set @EndDate =  dateadd(day,6,getdate());	IF OBJECT_ID('tempdb..#Results') IS NOT NULL
	--Begin
		Truncate TABLE #Results
	--End
	else
	--Begin
		CREATE TABLE #Results
					(dob date null,
					full_name nvarchar(255) null,
					age float null)
	--End
--select @begindate as start_date,@enddate as end_date
--select dob from usermst where month(dob)=10 or month(dob)=11

	while(@StartDate <= @enddate)
	begin
		insert into #Results  
		select 
			dob
			,concat(firstname,' ',lastname) as full_name
			,FLOOR(DATEDIFF(DAY, dob , getdate()) / 365.25) as age 
		from UserMst 
		where day(@StartDate)=day(dob) and month(@StartDate)=month(dob)

		--and month(@begindate)=month(dob) and 
		--day(@enddate)>=day(dob) and month(@enddate)=MONTH(dob)
		set @StartDate= DATEADD(day,1,@StartDate)
	end

	select 
		--cast(dob as datetime) as DOB,
		dob,
		full_name,
		age
	from #Results
End
go

declare @bg datetime = dateadd(DAY,1, getdate()),@ed datetime = dateadd(DAY,6, getdate());exec Get_DateInformation @bg, @ed