DROP TABLE tool;
DROP TABLE member;

CREATE TABLE member
(
	id int identity(1,1),
	member_name varchar(200) not null,
	drivers_license varchar(200) not null,

	CONSTRAINT pk_member_id PRIMARY KEY(id)
);

CREATE TABLE tool
(
    id int identity(1,1),
	brand varchar(200) not null,
	tool_name varchar(200) not null,
	description varchar(200) not null,
	checked_out bit not null,
	current_borrower int,
	date_borrowed datetime,
	due_date datetime,
	   
	CONSTRAINT pk_tool_id PRIMARY KEY(id)
);

SET IDENTITY_INSERT member ON;
INSERT INTO member (id, member_name, drivers_license) VALUES (1, 'Honor Banvard', 'LH760387');
INSERT INTO member (id, member_name, drivers_license) VALUES (2, 'Nick Hawk', 'LP581325');
INSERT INTO member (id, member_name, drivers_license) VALUES (3, 'Russell McFadden', 'LM356094');
INSERT INTO member (id, member_name, drivers_license) VALUES (4, 'Nathanael Foley', 'LW850785');
SET IDENTITY_INSERT member OFF;

INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Table Saw', 'DeWaltDisney', 'Used to make cuts that go with the grain', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Miter Saw', 'DeWaltDisney', 'Used to make cuts that go against the grain', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Belt Sander', 'Kobal', 'Handheld tool that grinds down the wood to a more fine finish', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Jig Saw', 'StanLee', 'Handheld tool used to make cuts at any angle', 1, 2, '2018-12-11', '2018-12-18');

ALTER TABLE tool
ADD FOREIGN KEY(current_borrower)
REFERENCES member(id);