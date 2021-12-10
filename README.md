CREATE TABLE Employee

(

Id INT PRIMARY KEY,

Name NVARCHAR(100),

Gender NVARCHAR(50),

DepartmentId INT

)

INSERT INTO Employee VALUES (1,'John Doe','Male',1);

INSERT INTO Employee VALUES (2,'Jonathan Tamm','Male',2);

INSERT INTO Employee VALUES (3,'Mary Jane','Female',1);

INSERT INTO Employee VALUES (4,'Jane Tamm','Female',2);

CREATE PROC GetEmployees

AS BEGIN

SELECT * FROM Employee

END

CREATE PROC GetEmployeesByGenderAndDepartment

@Gender NVARCHAR(50),

@DepartmentId INT

AS BEGIN

SELECT * FROM Employee

WHERE Gender = @Gender AND

DepartmentId = @DepartmentId

END

CREATE PROC GetEmployeeCount

AS BEGIN

SELECT COUNT(Id) AS EmployeeCount FROM Employee

END

CREATE PROC GetEmployeeCountByGender

@Gender NVARCHAR(50)

AS BEGIN

SELECT COUNT(Id) AS EmployeeCount FROM Employee

WHERE Gender = @Gender

END

CREATE PROC GetNameById

@Id INT

AS BEGIN

SELECT Name FROM Employee WHERE @Id = Id

END
