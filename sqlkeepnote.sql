create database keepnotedb

use keepnotedb

create table note
(
id int identity primary key,
title varchar(50),
descriptions varchar(50),
dates date
)
select * from note