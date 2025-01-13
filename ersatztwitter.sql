use master;
go;

drop database if exists ersatztwitter_db;
go;

create database ersatztwitter_db;
go;

use ersatztwitter_db;
go;

create table users
(
    id int identity primary key,
    pseudo varchar(100) not null unique
);
go;

create table tweets
(
    id int identity primary key,
    content varchar(140),
    userId int not null references users
);
go;

create table likes
(
    userId int not null references users,
    tweetId int not null references tweets on delete cascade,
    creationDate datetime not null default getdate(),
    primary key (userId, tweetId)
);
go;

insert into users(pseudo)
values
    ('Thisma'),
    ('Arisu')
go;

insert into tweets(content, userId)
values
    ('This is my first tweet', 1)
go;

insert into likes(userId, tweetId)
values
    (2, 1);
go;

select * from users;
go;

select * from tweets;
go;

select * from likes;
go;