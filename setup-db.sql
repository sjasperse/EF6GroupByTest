create database EF6GroupByTest
use EF6GroupByTest;

create table Customers (
    Id int identity(1,1) primary key, 
    Name varchar(20),
    Region varchar(10),
    OrderVolume money null
)

insert into Customers (Name, Region, OrderVolume)
values
    ('Customer 1', 'NA', 100000),
    ('Customer 2', 'NA', 100000),
    ('Customer 3', 'EU', 100000),
    ('Customer 4', 'EU', 100000),
    ('Customer 5', 'EU', 100000)
