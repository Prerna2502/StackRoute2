Create database NoteDb

Create table Users ( UserId int NOT NULL PRIMARY KEY, UserName varchar(20), Password varchar(15), Email varchar(30))
Select * from Users

Create table Notes ( NoteId int NOT NULL PRIMARY KEY, Title varchar(30), Description varchar(80), CreatedBy int FOREIGN KEY REFERENCES Users(UserId))
Select * from Notes

Insert into Users values(101, 'A', 'A#', 'A@gmail.com'), (102, 'B', 'B#', 'B@gmail.com')

Insert into Notes values(1001, 'Anote', 'This is a note created by user A', 101),(1002, 'Bnote', 'This is a note created by user B', 102)