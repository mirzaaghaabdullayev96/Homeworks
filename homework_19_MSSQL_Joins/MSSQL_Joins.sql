--create database Library
--use Library

create table Authors
(
Id int primary key identity(1,1),
Name varchar(100) not null,
Surname varchar(100) not null
)

create table Books
(
Id int primary key identity(1,1),
Name varchar(100) not null,
PageCount int not null check(PageCount>0),
CostPrice decimal(6,2) not null check(CostPrice>0),
SalePrice decimal(6,2) not null check(SalePrice>0),
AuthorId int foreign key references Authors(Id)
)


create table Tags
(
Id int primary key identity(1,1),
Name varchar(100) not null,
)

create table BooksTags
(
Id int primary key identity(1,1),
BooksId int foreign key references Books(Id),
Tags int foreign key references Tags(Id)
)

insert into Authors
values 
('Alex', 'Michaelides'),
('Delia','Owens'),
('Matt', 'Haig')

select * from Authors

insert into Books
values
('The Silent Patient',336,10.50,15.99,1),
('Where the Crawdads Sing',384, 12.00 ,17.99,2),
('The Midnight Library', 304, 11.25, 16.99,3),
('How to Stop Time', 352, 10.75, 15.99,3)

insert into Tags
values
('New'),
('Bestseller'),
('Featured'),
('Classic'),
('For Kids')

insert into BooksTags
values
(1,2),
(1,4),
(2,4),
(2,5),
(3,2),
(3,3),
(3,4),
(4,1),
(4,3)


select * from BooksTags
--select Id, Name + ' ' + Surname AS AuthorFullName from Authors

--Id | AuthorFullName | BookName | BookPrice | PageCount | TagName


select B.Id, A.Name + ' ' + A.Surname AS AuthorFullName, B.Name as BookName, SalePrice as BookPrice, PageCount, T.Name as TagName from Books as B
join Authors as A	
on B.AuthorId =  A.Id
join BooksTags as BT
on B.Id=BT.BooksId
join Tags as T
on BT.TagsId=T.Id

--Tags elnen tableden TagsID deyishmishem











