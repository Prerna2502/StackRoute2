Create database StackRoute_rdbms

USE StackRoute_rdbms

CREATE TABLE "User"
(
"user_id" INT PRIMARY KEY,
"user_name" VARCHAR(20),
user_added_date DATETIME,
user_password VARCHAR(20),
user_mobile VARCHAR(20)
)

CREATE TABLE Note
(
note_id INT IDENTITY PRIMARY KEY,
note_title VARCHAR(20),
note_content VARCHAR(MAX),
note_status VARCHAR(20),
note_creation_date DATETIME,
"user_id" int REFERENCES "User"("user_id")
)
DROP TABLE Category
CREATE TABLE Category
(
category_id INT IDENTITY PRIMARY KEY,
category_name VARCHAR(20),
category_descr VARCHAR(20),
category_creation_date DATETIME,
category_creator INT REFERENCES "User"("user_id")
)
drop table Reminder
CREATE TABLE Reminder
(
reminder_id INT IDENTITY PRIMARY KEY,
reminder_name VARCHAR(20),
reminder_descr VARCHAR(50),
reminder_type VARCHAR(20),
reminder_creation_date DATETIME,
reminder_creator INT REFERENCES "User"("user_id")
)
Drop table NoteReminder
CREATE TABLE NoteReminder
(
notereminder_id INT IDENTITY PRIMARY KEY,
note_id INT REFERENCES Note(note_id),
reminder_id INT REFERENCES Reminder(reminder_id)
)
ALTER TABLE NoteCategory ALTER COLUMN notecategory_id INT IDENTITY PRIMARY KEY
Drop table NoteCategory
CREATE TABLE NoteCategory
(
notecategory_id INT IDENTITY PRIMARY KEY,
note_id INT REFERENCES Note(note_id),
category_id INT REFERENCES Category(category_id)
)

INSERT INTO "User" VALUES (112233, 'Maya', '2019-01-02', 'Maya1214', '8012345679'), (112244, 'Nair', '2019-01-11', 'Welcome', '9023778467')

INSERT INTO Note VALUES 
('Today Tasks', '1.Check the emails and reply to them 2.Start the pro...', 'InProgress', '2019-01-21', 112233),
('Training to plan', '1.Application related 2.Technical related', 'Yet To Start', '2019-01-31', 112244),
('Things to have today', '1.Fruits 2.More water', 'InProgress', '2019-01-25', 112244)

INSERT INTO Category VALUES ('Official', 'Office related notes', '2019-01-21', 112233),('Diet', 'Health related notes', '2019-01-24', 112244)

INSERT INTO Reminder VALUES ('KT reminder', 'Session on technical queries', 'Office Reminders', '2019-02-12', 112233), ('Personal reminder', 'Pick children', 'Personal Reminders', '2019-02-14', 112244)

INSERT INTO NoteCategory VALUES (1,1), (2,1), (3,2)

INSERT INTO NoteReminder VALUES (3,2), (2,1)

SELECT * FROM "User" WHERE  user_id=112233 and user_password='Maya1214'

SELECT * FROM Note WHERE note_creation_date='01/31/2019'

SELECT * FROM Category WHERE category_creation_date>'01/22/2019'

SELECT * FROM Category WHERE category_id IN (SELECT category_id FROM NoteCategory WHERE note_id=1)

SELECT * FROM Note WHERE user_id=112244

SELECT * FROM Note WHERE note_id IN (SELECT note_id FROM NoteCategory WHERE category_id=1)

SELECT * FROM Reminder WHERE reminder_id IN (SELECT reminder_id FROM NoteReminder WHERE note_id=2)

UPDATE Note SET note_status='Completed' WHERE note_id=3

INSERT INTO NoteReminder VALUES (1, (SELECT reminder_id FROM Reminder WHERE reminder_type='Personal Reminders'))

DELETE FROM NoteReminder WHERE note_id=2