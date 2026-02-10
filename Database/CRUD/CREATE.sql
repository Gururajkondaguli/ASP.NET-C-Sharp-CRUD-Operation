CREATE PROCEDURE InsertEmploye
    @FirstName     VARCHAR(50),
    @LastName      VARCHAR(50),
    @Email         VARCHAR(100),
    @PhoneNo       VARCHAR(15),
    @DepartmentId  INT,
    @JobTitle      VARCHAR(50),
    @DateOfJoining DATE,
    @Salary        DECIMAL(10,2),
    @Status        VARCHAR(20)
AS
BEGIN
    INSERT INTO dbo.Employe (
        FirstName,
        LastName,
        Email,
        PhoneNo,
        DepartmentId,
        JobTitle,
        DateOfJoining,
        Salary,
        Status
    )
    VALUES (
        @FirstName,
        @LastName,
        @Email,
        @PhoneNo,
        @DepartmentId,
        @JobTitle,
        @DateOfJoining,
        @Salary,
        @Status
    );
END;							--Create Operation




exec dbo.InsertEmploye
   
    @FirstName='Attu',
    @LastName='K',
    @Email='pawan@gmail.com',
    @PhoneNo=8884696496,
    @DepartmentId='10',
    @JobTitle='Cricketer',
    @DateOfJoining='2001-01-01',
    @Salary=20000.00,
    @Status='active'



    select * from Employe

 

