/*
create database SQLFullcourse
use sqlfullcourse
create table test (ID int identity(1,1), name varchar(25))
insert into test(name)values('balu')
select * from test

recordes insere,update, delete in a table-----
insert into test values('a'),('b'),('c'),('d');
update test set name='x' where ID=1 
delete from test where id=9

--create, update, delete table structer
create table test2 (id int primary key, name varchar(25),phone varchar (15),address varchar(20));
exec sp_rename 'test2', 'test3'; // change table name or modify table name
exec sp_rename 'test3.name', 'NAME','column'; //change column name
alter table test3 alter column phone varchar(15);// change data type for column
alter table test3 ADD DateOfBirth DATE; -- add new column in table
alter table Employees DROP COLUMN DateOfBirth -- delete column in table
drop table test3 //delete table
select * from test3
exec sp_rename 'test3', 'test2'; // change table name or modify table name

insert into test2 (id,name,phone,address,dateofbirth) values(2,'balu','9876543212','hyd','12-12-12');
update test2 set name='xyz',phone='7745676543',address='goa',dateofbirth='09-09-09' where ID=2 
insert into test2 (id,name,phone,address,dateofbirth) values(3,'balu','9876543212','hyd','12-12-12');
delete from test2 where id=3 


select * from test2
select,from,where,order by,group by,destinct,Top ,having
create table users(ID int identity(1,1) primary key, Name varchar(20),country varchar(20),Score int)
insert into users values('Maria','Germany',350)
insert into users values('John','Usa',900),('Georg','Uk',750),('Martin','Germany',500),('Peter','usa',0)
select * from users
select id,name from users
select id,name,country from users
select id,name,country,score from users

select * from users where id=2
select * from users where id!=2
select * from users where country='uk'
select * from users where country!='uk' and country='usa'
select * from users where country!='uk' or country='usa'
select id,name,country from users where country!='uk' or country='usa'

select * from users order by score
select * from users order by score desc
select * from users order by score asc
select * from users order by name desc
select * from users order by name asc
select * from users order by country desc
select * from users order by country asc
select * from users order by country asc,score desc

select  country, sum(score) as total_score from users group by country
select country, sum(score) as total_score from users group by country order by country desc

select country, sum(score) as total_score from users group by country having sum(score)>800
select country, sum(score) as total_score from users group by country having sum(score)<800

select id,country,score from users
select id,country,score from users where score !=0
select country,avg(score) as avg_scr from users where 
score !=0 group by country

select country,avg(score) as avg_scr from users where 
score !=0 group by country having avg(score) > 430


select * from users
select id from users
select distinct id from users
select distinct country from users
select distinct name from users 

select * from users
select top 3 *from users
select * from users ORDER BY score DESC
select top 3 * from users ORDER BY score DESC
select top 3 * from users ORDER BY score asc

create table orders (orderid int identity(1,1) primary key, C_ID int, orderData date,sales int,
FOREIGN KEY (C_ID) REFERENCES users(ID))
select * from orders

insert into orders values(1,'09-09-2019',12)
insert into orders values (2,'01-05-2025',9)
select distinct top 2 col1, sum(col2) from table where col=10 group by col1 having sum(col1)<20 order by col1 asc

select * from orders
select * from users
select 'jhj' as a
select 123 as B


create table persons( id int not null,personName varchar(40) not null,dob date,phone varchar(15) not null,
constraint pk_persons primary key(id) 
select * from persons
alter table persons add email varchar(50) not null
alter table persons drop column email
drop table persons

update users set score=0
where country='ind'

select * from users
insert into users (name,country) values('teat','ind'),
('test','uk'),
('hai','ar'),('team','irn')


insert into users (name) values('hello')


select *from users 
where score is null

update users set score=10
where score is null

delete from users  where score is null >7
select * from users  where id >7
select * from users where score =0
delete from users  where score =0

truncate table users;
--comparision operators
select * from users where country ='Germany'
select * from users where country !='Germany'
select * from users where score > 400
select * from users where score >= 400;
-- same as  =,!=,<,<=,>,>= ..

--logical operators:

select * from users where country='usa' and score > 500
select * from users where country='usa' or score > 500
select * from users where country='usa' and score < 500
select * from users where country='germany' or score >500
select * from users where country='usa' or score < 500
select * from users where  not score < 500
select * from users where  score < 500
select * from users where  score between 500 and 1000 --or--
select * from users where  score >= 500 and   score < 1000
select * from users where  country ='usa' or country='germany' --or--
select * from users where  country in('germany','usa')
select * from users where  country not in('germany','usa')
select * from users where country  like 'g%'
select * from users where country  like 'g_%'
select * from users where country like '%y'
select * from users where country like '%a%'
select * from users where name like '_a%'


  
  
--joins--

select * from users
select * from orders

select * from users inner join orders on
id=c_id;--(or)
select * from users inner join orders on
users.id=orders.c_id
--or
select id, name,country,score,orderid,orderdate,sales from users
inner join orders on id=c_id
--exec sp_rename 'orders.orderData', 'orderDate','column';
select users.id, users.name,users.score,orders.orderid,orders.sales,orders.orderDate
from users inner join orders on
users.id=orders.c_id
--or--
select u.id, u.name,u.score,o.orderid,o.sales,o.orderDate
from users as u inner join orders as o on
u.id=o.c_id

select * from orders


select * from users inner join orders on
users.id=orders.c_id

select * from users full join orders on
users.id=orders.c_id

select * from users left join orders on
users.id=orders.c_id

select * from users right join orders on
users.id=orders.c_id

select * from users cross join orders
select * from orders cross join users

--left anti join--
select * from users left join orders on
users.id=orders.c_id where orders.c_id is null
--or--
select * from users as u
left join orders as o
on u.id=o.c_id where o.c_id is null

select * from users as u
right join orders as o
on u.id=o.c_id where u.id is null

select * from orders
insert into orders (c_id,orderdate,sales)
values( 9,'09-09-2024',90)

 select * from orders as o
 left join users as u on
 u.id=o.c_id  where u.id is null

 --full anti join--
 select * from users as u
 full join orders as o on u.id=o.c_id
 where u.id is null

  select * from orders as o
 full join users as u on u.id=o.c_id
  where o.c_id is null

    select * from orders as o
 full join users as u on u.id=o.c_id
  where o.c_id is not null

    select * from users as u
 left join orders as o on u.id=o.c_id
  where o.c_id is not null;

    select * from users as u
 right join orders as o on u.id=o.c_id
  where o.c_id is not null;


  
  */
  /*
  
  create table products(ProductID int identity(1,1) primary key not null,
  Product varchar(20) not null,
  Category varchar(20) not null,
  Price decimal(10,2) not null);

  create table customers (
  CustomerID int identity(1,1) primary key not null,
  FirstName varchar(20) not null, 
  LastName varchar(20) not null,
  Country varchar(20) not null, 
  Score int not null);

  create table Employees(EmployeeId int not null identity(1,1) primary key,
  FirstName varchar(20) not null, 
  LastName varchar(20) not null,
  Department varchar(20) not null, 
  BirthDate date not null,
    Gender varchar(5) not null, 
  Salary decimal(10,2) not null,
  ManagerID int not null);

  create table Orders_new(OrderID int identity(1,1) primary key,
  ProductID int,
  CustomerID int,
  SalesPersonID int,
  OrderDate date,
  ShipDate date,
  OrderStatus varchar(10),
  ShipAddress varchar(20),
  BillAddress varchar(20),
  Quantity int,
  Sales Decimal(10,2),
  CreationTime DateTime,
  foreign key(ProductID) references Products(ProductID),
    foreign key(CustomerID) references Customers(CustomerID),
	  foreign key(SalesPersonID) references Employees(EmployeeID)
	  );


	  select * from orders_new
	

	  --adding records all tables--
	  INSERT INTO Products (Product, Category, Price) VALUES
('Laptop', 'Electronics', 750.00),
('Phone', 'Electronics', 500.00),
('Desk', 'Furniture', 120.00),
('Chair', 'Furniture', 60.00),
('Tablet', 'Electronics', 300.00),
('Monitor', 'Electronics', 200.00),
('Pen', 'Stationery', 2.00),
('Notebook', 'Stationery', 5.00),
('Printer', 'Electronics', 150.00),
('Router', 'Electronics', 80.00);

INSERT INTO Customers (FirstName, LastName, Country, Score) VALUES
('John', 'Doe', 'USA', 600),
('Jane', 'Smith', 'UK', 750),
('Ali', 'Khan', 'Pakistan', 680),
('Maria', 'Garcia', 'Spain', 720),
('Chen', 'Li', 'China', 700),
('Ahmed', 'Hassan', 'Egypt', 640),
('Emily', 'Clark', 'Canada', 800),
('Ivan', 'Petrov', 'Russia', 670),
('Sara', 'Kim', 'South Korea', 710),
('Diego', 'Lopez', 'Mexico', 690);
INSERT INTO Employees (FirstName, LastName, Department, BirthDate, Gender, Salary, ManagerID) VALUES
('Robert', 'King', 'Sales', '1985-06-10', 'M', 70000, 1),
('Linda', 'Brown', 'HR', '1990-02-15', 'F', 65000, 1),
('James', 'Wilson', 'IT', '1982-11-22', 'M', 80000, 1),
('Karen', 'Scott', 'Marketing', '1988-03-05', 'F', 72000, 1),
('David', 'Lee', 'Sales', '1991-09-30', 'M', 69000, 1),
('Susan', 'Taylor', 'Finance', '1979-12-12', 'F', 75000, 1),
('Tom', 'Clark', 'Sales', '1983-07-25', 'M', 68000, 1),
('Nancy', 'Evans', 'IT', '1994-01-19', 'F', 77000, 1),
('Kevin', 'Hill', 'HR', '1987-08-02', 'M', 66000, 1),
('Angela', 'White', 'Marketing', '1989-10-14', 'F', 71000, 1);

INSERT INTO Orders_new (ProductID, CustomerID, SalesPersonID, OrderDate, ShipDate, OrderStatus, ShipAddress, BillAddress, Quantity, Sales, CreationTime) VALUES
(1, 1, 1, '2024-01-01', '2024-01-05', 'Shipped', 'NY Address', 'NY Address', 2, 1500.00, GETDATE()),
(2, 2, 2, '2024-02-15', '2024-02-18', 'Shipped', 'London Address', 'London Address', 1, 500.00, GETDATE()),
(3, 3, 3, '2024-03-10', '2024-03-13', 'Pending', 'Lahore Address', 'Lahore Address', 1, 120.00, GETDATE()),
(4, 4, 4, '2024-04-05', '2024-04-07', 'Cancelled', 'Madrid Address', 'Madrid Address', 1, 60.00, GETDATE()),
(5, 5, 5, '2024-05-01', '2024-05-04', 'Shipped', 'Beijing Address', 'Beijing Address', 1, 300.00, GETDATE()),
(6, 6, 6, '2024-06-12', '2024-06-15', 'Shipped', 'Cairo Address', 'Cairo Address', 2, 400.00, GETDATE()),
(7, 7, 7, '2024-07-20', '2024-07-22', 'Pending', 'Toronto Address', 'Toronto Address', 5, 10.00, GETDATE()),
(8, 8, 8, '2024-08-25', '2024-08-27', 'Shipped', 'Moscow Address', 'Moscow Address', 3, 15.00, GETDATE()),
(9, 9, 9, '2024-09-30', '2024-10-02', 'Shipped', 'Seoul Address', 'Seoul Address', 1, 150.00, GETDATE()),
(10, 10, 10, '2024-10-05', '2024-10-10', 'Cancelled', 'Mexico Address', 'Mexico Address', 1, 80.00, GETDATE());



  select * from customers
   select * from employees
	 select * from orders_new
	  select * from products
	   select * from test
	   select * from test1
	     select * from orders
	   select * from users
	  

--set operators--//union,union All,except,intersect
--set operators following this rules
rule1: order by can be used only once,
rule2: same number of columns,
rule3:matching data types,
rule4:same order of columns,
rule5:first query controls Aliases,
rule6:mapping correct columns

  select customerId,firstName,LastName from customers union
  select employeeid,firstName,LastName from employees

  
  select customerId,firstName,LastName from customers union All
  select employeeid,firstName,LastName from employees

  select customerId,firstName,LastName from customers except
  select employeeid,firstName,LastName from employees

    select employeeid,firstName,LastName from employees except
    select customerId,firstName,LastName from customers 

	select employeeid,firstName,LastName from employees intersect
    select customerId,firstName,LastName from customers 

	select customerId,firstName,LastName from customers intersect
	select employeeid,firstName,LastName from employees 
 
     */

--functions: 
--single row functions(string,numeric,date & time, null),
--multi-row functions(aggregate functions, window-function) 

--string: (manipulation,calculation,string-extration)
--manipulation(concat,upper,lower,trim,replace)
--calculation(len)
--string-extration(left,right,substring)
    --select * from customers
--concat: ex- 

SELECT FirstName,LastName, 
CONCAT(FirstName, '_', LastName) AS fullName
FROM Customers;
 
SELECT FirstName,LastName, 
CONCAT(FirstName, '_', LastName) AS fullName,
lower(FirstName) as l_f_name
FROM Customers;

SELECT FirstName,LastName, 
CONCAT(FirstName, '_', LastName) AS fullName,
lower(FirstName) as l_f_name,
Upper(lastname)as u_name
from customers

select firstname,lastname,concat(firstname ,'   ',lastname) as f_name,
lower(firstname) as fname,
upper(lastname)as u_name
from customers




















