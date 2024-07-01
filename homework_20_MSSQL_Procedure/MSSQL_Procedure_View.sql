create database Library_Homework_20

use Library_Homework_20

create table Authors
(
Id int primary key identity,
Name varchar(50) not null,
Surname varchar(50) not null
)

create table Books
(
Id int primary key identity,
Name varchar(100) not null,
Pagecount smallint null,
AuthorID int foreign key references Authors(Id)
)

INSERT INTO Authors (Name, Surname)
VALUES 
('John', 'Doe'),
('Jane', 'Smith'),
('Emily', 'Johnson'),
('Michael', 'Williams'),
('Sarah', 'Brown');

INSERT INTO Books (Name, Pagecount, AuthorID)
VALUES 
('The Great Adventure', 320, 1),
('Mystery of the Old House', 280, 1),
('Journey to the Unknown', 350, 2),
('Secrets of the Forest', 400, 2),
('The Lost Treasure', 310, 3),
('Echoes of the Past', 275, 3),
('Whispers in the Wind', 290, 4),
('Shadows of the Night', 365, 4),
('The Silent Guardian', 340, 5),
('Heart of the Sea', 360, 5),
('Tales of the Ancients', 330, 1),
('The Last Frontier', 310, 2),
('Beyond the Horizon', 345, 3),
('The Forgotten Realm', 385, 4),
('The Enchanted Forest', 370, 5);



create view BooksDetails
as
select B.Id, B.Name as BookName, B.Pagecount, CONCAT(A.Name,'', A.Surname) as AuthorFullName   from Books as B
left join Authors as A
on
A.Id=B.AuthorID


create view AuthorsInfo
as
select A.Id, CONCAT(A.Name,' ', A.Surname) as AuthorFullName, Count(*) as BooksCount, Max(B.Pagecount) as MaxPageCount  from Books as B
join Authors as A
on
A.Id=B.AuthorID
group by A.Id, A.Name, A.Surname

select * from Books as B
join Authors as A
on
A.Id=B.AuthorID
where A.Name='John'

create procedure SP_Get_By_Name @name varchar(50)
as
select * from BooksDetails
where BookName=@name or AuthorFullName=@name

exec SP_Get_By_Name 'John Doe'







