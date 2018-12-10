DROP TABLE tool_users;
DROP TABLE tool;
DROP TABLE users;

CREATE TABLE tool
(
    id int identity(1,1),
    name varchar(200) not null,
    description varchar(200) not null,
    checked_out bit not null,
    brand varchar(200) not null,
    
    CONSTRAINT pk_tool_id primary key(id)
);

CREATE TABLE users
(
   id int identity(1,1),
   name varchar(200) not null,
   drivers_license varchar(200) not null,

   CONSTRAINT pk_users_id primary key(id)
);

CREATE TABLE tool_users
(
	tool_id int not null,
	user_id int not null,
	date_borrowed datetime not null,
	date_returned datetime,
	due_date datetime not null,
	
	CONSTRAINT tool_users_tool_id foreign key(tool_id) references tool(id),
	CONSTRAINT tool_users_user_id foreign key(user_id) references users(id),
);