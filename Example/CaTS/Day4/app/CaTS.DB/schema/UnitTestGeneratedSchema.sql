
    if exists (select * from dbo.sysobjects where id = object_id(N'StaffMembers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table StaffMembers

    create table StaffMembers (
        Id INT IDENTITY NOT NULL,
       EmployeeNumber NVARCHAR(255) null,
       FirstName NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       PasswordHash NVARCHAR(255) null,
       Roles INT null,
       primary key (Id)
    )
