
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

declare 