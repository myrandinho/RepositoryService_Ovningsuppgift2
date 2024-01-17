DROP TABLE Users
DROP TABLE Roles

CREATE TABLE Roles
(
	Id int not null identity primary key,
	RoleName nvarchar(50) not null unique
)

CREATE TABLE Users
(
	Id int not null identity primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	Email varchar(100) not null,
	RoleId int not null references Roles(Id)

)

INSERT INTO Roles (RoleName) VALUES ('Administrator')
INSERT INTO Users (FirstName, LastName, Email, RoleId) VALUES ('vr', 'vr', 'vr', 1)
