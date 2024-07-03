create database AcademyADO_NET
use AcademyADO_NET

create table Academies
(
Id int primary key identity,
Name varchar(100) not null
)

create table Groups
(
Id int primary key identity,
Name varchar(100) not null,
AcademyId int foreign key references Academies(Id)
)

create table Students
(
Id int primary key identity,
Name varchar(50) not null,
Surname varchar(50) not null,
Age smallint not null,
[Grant] decimal null,
GroupId int foreign key references Groups(Id)
)

INSERT INTO Academies (Name)
VALUES ('Academy of Science and Technology'),
       ('Academy of Arts and Design');

INSERT INTO Groups (Name, AcademyId)
VALUES ('Biology Club', 1),
       ('Computer Science Society', 1),
       ('Art Club', 2),
       ('Drama Club', 2),
       ('Math League', 1);


INSERT INTO Students (Name, Surname, Age, [Grant], GroupId)
VALUES ('John', 'Smith', 18, 0, 1),
       ('Emily', 'Johnson', 17, 1000, 1),
       ('Michael', 'Williams', 16, 0, 2),
       ('Sophia', 'Brown', 18, 500, 2),
       ('James', 'Davis', 17, 0, 3),
       ('Olivia', 'Wilson', 16, 750, 3),
       ('Benjamin', 'Moore', 18, 0, 4),
       ('Emma', 'Taylor', 17, 0, 4),
       ('William', 'Anderson', 16, 1200, 5),
       ('Ava', 'Martinez', 17, 0, 5)

update Students 
set Name='Baba'
where Id=3

