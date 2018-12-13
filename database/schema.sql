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

INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Angle Grinder', 'DeWaltDisney', 'Used to Grind down metal or bricks in small amounts', 1, 2, '2018-12-11', '2018-12-18');
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Angle Grinder', 'GitBosch', 'Used to Grind down metal or bricks in small amounts', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Angle Grinder', 'KOBOL', 'Used to Grind down metal or bricks in small amounts', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Angle Grinder', 'SkillzFund', 'Used to Grind down metal or bricks in small amounts', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Angle Grinder', 'StanLee', 'Used to Grind down metal or bricks in small amounts', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Belt Sander', 'DeWaltDisney', 'A sander that uses a moving abrasive belt to smooth surfaces.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Belt Sander', 'GitBosch', 'A sander that uses a moving abrasive belt to smooth surfaces', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Belt Sander', 'KOBOL', 'A sander that uses a moving abrasive belt to smooth surfaces', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Belt Sander', 'SkillzFund', 'A sander that uses a moving abrasive belt to smooth surfaces', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Belt Sander', 'StanLee', 'A sander that uses a moving abrasive belt to smooth surfaces', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Drill', 'DeWaltDisney', 'A hand tool, power tool, or machine with a rotating cutting tip or reciprocating hammer or chisel, used for making holes.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Drill', 'GitBosch', 'A hand tool, power tool, or machine with a rotating cutting tip or reciprocating hammer or chisel, used for making holes.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Drill', 'KOBOL', 'A hand tool, power tool, or machine with a rotating cutting tip or reciprocating hammer or chisel, used for making holes.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Drill', 'SkillzFund', 'A hand tool, power tool, or machine with a rotating cutting tip or reciprocating hammer or chisel, used for making holes.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Drill', 'StanLee', 'A hand tool, power tool, or machine with a rotating cutting tip or reciprocating hammer or chisel, used for making holes.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('JigSaw', 'DeWaltDisney', 'a machine saw with a fine blade enabling it to cut curved lines in a sheet of wood, metal, or plastic.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('JigSaw', 'GitBosch', 'a machine saw with a fine blade enabling it to cut curved lines in a sheet of wood, metal, or plastic.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('JigSaw', 'KOBOL', 'a machine saw with a fine blade enabling it to cut curved lines in a sheet of wood, metal, or plastic.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('JigSaw', 'SkillzFund', 'a machine saw with a fine blade enabling it to cut curved lines in a sheet of wood, metal, or plastic.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('JigSaw', 'StanLee', 'a machine saw with a fine blade enabling it to cut curved lines in a sheet of wood, metal, or plastic.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Miter Saw', 'DeWaltDisney', 'A miter saw is a saw used to make accurate crosscuts and miters in a workpiece by pulling a large backsaw or a mounted circular saw blade down onto a board in a quick motion', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Miter Saw', 'GitBosch', 'A miter saw is a saw used to make accurate crosscuts and miters in a workpiece by pulling a large backsaw or a mounted circular saw blade down onto a board in a quick motion', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Miter Saw', 'KOBOL', 'A miter saw is a saw used to make accurate crosscuts and miters in a workpiece by pulling a large backsaw or a mounted circular saw blade down onto a board in a quick motion', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Miter Saw', 'SkillzFund', 'A miter saw is a saw used to make accurate crosscuts and miters in a workpiece by pulling a large backsaw or a mounted circular saw blade down onto a board in a quick motion', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Miter Saw', 'StanLee', 'A miter saw is a saw used to make accurate crosscuts and miters in a workpiece by pulling a large backsaw or a mounted circular saw blade down onto a board in a quick motion', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Table Saw', 'DeWaltDisney', 'A circular saw mounted under a table or bench so that the blade projects up through a slot.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Table Saw', 'GitBosch', 'A circular saw mounted under a table or bench so that the blade projects up through a slot.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Table Saw', 'KOBOL', 'A circular saw mounted under a table or bench so that the blade projects up through a slot.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Table Saw', 'SkillzFund', 'A circular saw mounted under a table or bench so that the blade projects up through a slot.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Table Saw', 'StanLee', 'A circular saw mounted under a table or bench so that the blade projects up through a slot.', 0, null, null, null);
INSERT INTO tool (tool_name, brand, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('Orbital Sander', 'KOBOL', 'A circular sander that uses an abrasive circle to smooth surfaces; Takes off less than a belt sander. ', 0, null, null, null);



ALTER TABLE tool
ADD FOREIGN KEY(current_borrower)
REFERENCES member(id);




































