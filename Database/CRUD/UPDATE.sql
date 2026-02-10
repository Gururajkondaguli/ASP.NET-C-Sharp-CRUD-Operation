create procedure UpdateEmployee
    @EmpId INT,
    @FirstName     VARCHAR(50),
    @LastName      VARCHAR(50),
    @Email         VARCHAR(100),
    @PhoneNo       VARCHAR(15),
    @DepartmentId  INT,
    @JobTitle      VARCHAR(50),
    @DateOfJoining DATE,
    @Salary        DECIMAL(10,2),
    @Status        VARCHAR(20)
as
begin
update dbo.Employee
set     FirstName=@FirstName,
        LastName=@LastName,
        Email=@Email,
        PhoneNo=@PhoneNo,
        DepartmentId=@DepartmentId,
        JobTitle=@JobTitle,
        DateOfJoining=@DateOfJoining,
        Salary=@Salary,
        Status=@Status
where empId=@empId;
end;



exec dbo.UpdateEmployee
    @empId = 1,
    @FirstName='Parshu',
    @LastName='K',
    @Email='pawan@gmail.com',
    @PhoneNo=8884696496,
    @DepartmentId='10',
    @JobTitle='Cricketer',
    @DateOfJoining='2001-01-01',
    @Salary=20000.00,
    @Status='active'            --Update

    exec dbo.UpdateEmployee
    @empId = 7,
    @FirstName='Sachin',
    @LastName='K',
    @Email='pawan@gmail.com',
    @PhoneNo=8884696496,
    @DepartmentId='10',
    @JobTitle='Cricketer',
    @DateOfJoining='2001-01-01',
    @Salary=20000.00,
    @Status='active'  