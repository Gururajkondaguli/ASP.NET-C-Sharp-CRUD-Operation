create procedure deleteEmployee
@EmpId INT
as 
begin 
delete from dbo.Employee
where empId=@empId;
end;


exec dbo.deleteEmployee @empId=1;	--Delete		--Delete