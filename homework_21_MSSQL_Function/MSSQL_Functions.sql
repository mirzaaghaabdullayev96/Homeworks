create database Academy
use Academy

create table Academies
(
Id int primary key identity,
Name varchar(100) not null
)

create table Groups
(
Id int primary key identity,
Name varchar(100) not null,
IsDeleted bit default 0 not null,
AcademyId int foreign key references Academies(Id)
)

create table Students
(
Id int primary key identity,
Name varchar(50) not null,
Surname varchar(50) not null,
Age smallint not null,
Adulthood bit null,
GroupId int foreign key references Groups(Id)
)

create table DeletedStudents
(
Id int primary key identity,
Name varchar(50) not null,
Surname varchar(50) not null,
GroupId int 
)

create table DeletedGroups
(
Id int primary key identity,
Name varchar(50) not null,
AcademyId int 
)

create trigger TR_ADULTHOOD_INSERT on Students
after insert 
as
update Students
set Adulthood = case
when I.Age > 17 then 1
else 0
end
from Students S
inner join Inserted I ON S.Id = I.Id;


create trigger TR_ADULTHOOD_UPDATE on Students
after update 
as
update Students
set Adulthood = case
when I.Age > 17 then 1
else 0
end
from Students S
inner join Inserted I ON S.Id = I.Id;


create trigger TR_ADD_TO_DELETED_GROUPS on Students
after delete
as
insert into DeletedStudents 
select Name, Surname, GroupId from deleted



create trigger TR_ADD_TO_DELETED_STUDENTS on Groups
instead of delete
as
begin
insert into DeletedGroups
select Name, AcademyId from deleted;

update Groups
set IsDeleted = 1
where Id in (select Id from deleted);
end


INSERT INTO Academies (Name)
VALUES 
('Academy of Sciences'),
('Institute of Technology'),
('School of Arts');

INSERT INTO Groups (Name, AcademyId)
VALUES 
('Group A1', 1),
('Group A2', 1),
('Group A3', 1),
('Group B1', 2),
('Group B2', 2),
('Group B3', 2),
('Group C1', 3),
('Group C2', 3),
('Group C3', 3);


INSERT INTO Students (Name, Surname, Age, GroupId)
VALUES 
('Alice', 'Smith', 16, 1),
('Bob', 'Johnson', 18, 1),
('Charlie', 'Williams', 17, 2),
('David', 'Brown', 19, 2),
('Emma', 'Jones', 20, 3),
('Frank', 'Garcia', 21, 3),
('Grace', 'Miller', 15, 4),
('Hannah', 'Davis', 22, 4),
('Ivy', 'Martinez', 14, 5),
('Jack', 'Hernandez', 18, 5),
('Kevin', 'Lopez', 17, 6),
('Liam', 'Gonzalez', 19, 6),
('Mia', 'Wilson', 16, 7),
('Noah', 'Anderson', 20, 7),
('Olivia', 'Thomas', 21, 8),
('Paul', 'Taylor', 22, 8),
('Quincy', 'Moore', 23, 9),
('Rachel', 'Jackson', 15, 9),
('Sam', 'Martin', 18, 1),
('Tina', 'Lee', 19, 2);

delete from Students
where Id=3


create procedure GetByMoreAge @age int
as
select S.Id,S.Name+' '+S.Surname as Fullname,S.Age, S.Adulthood,G.Name as GroupnName from Students as S
join Groups as G
on
G.Id=S.Id
where S.Age>@age


create procedure GetByLessAge @age int
as
select S.Id,S.Name+' '+S.Surname as Fullname,S.Age, S.Adulthood,G.Name as GroupnName from Students as S
join Groups as G
on
G.Id=S.Id
where S.Age<@age

create procedure SP_GET_GROUP_BY_NAME @name varchar(100)
as
select * from Groups
where Name=@name

create view VW_ACADEMY_INFO
as
select * from Academies

create view VW_STUDENTS_INFO
as
select S.Id, S.Name, S.Surname, S.Age, S.Adulthood, G.Name as GroupName from Students as S
join Groups as G
on
S.GroupId=G.Id

create view VW_GROUPS_INFO
as
select G.Id,G.Name, A.Name as AcademyName from Groups as G
join Academies as A
on
G.AcademyId=A.Id


create function UF_GET_STUDENTS_BY_GROUPID (@id int)
returns table
as
return
(
select * from Students
where GroupId=@id
)

select * from UF_GET_STUDENTS_BY_GROUPID(1)

create function UF_GET_GROUPS_BY_ACADEMYID (@id int)
returns table
as
return
(
select * from Groups
where AcademyId=@id
)

select * from UF_GET_GROUPS_BY_ACADEMYID(2)






