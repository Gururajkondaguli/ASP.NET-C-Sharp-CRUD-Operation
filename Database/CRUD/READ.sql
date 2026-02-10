CREATE PROCEDURE ReadEmployee
@empId INT
as
begin
select *
from dbo.Employe
where empId=@empId;
end;

exec dbo.ReadEmployee @empId=4;	


--Read Employee


CREATE PROCEDURE ReadingEmployee
as
begin
select *
from dbo.Employee;
end;

EXEC dbo.ReadEmployee;

 