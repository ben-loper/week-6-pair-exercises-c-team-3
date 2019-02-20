
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the MessageEncryptionDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='MessageEncryptionDB')
DROP DATABASE MessageEncryptionDB;
GO

-- Create a new MessageEncryptionDB Database
CREATE DATABASE MessageEncryptionDB;
GO

-- Switch to the MessageEncryptionDB Database
USE MessageEncryptionDB
GO

-- Begin a TRANSACTION that must complete with no errors
BEGIN TRANSACTION;

CREATE TABLE [user] (
    Id integer NOT NULL IDENTITY(1,1),
    UserName varchar(64) UNIQUE NOT NULL,
    Hash varchar(50) NOT NULL,
	Salt varchar(50) NOT NULL,
    CONSTRAINT pk_user PRIMARY KEY (Id)
);

CREATE TABLE message (
    Id integer NOT NULL IDENTITY(1,1),
    FromUserId INT NOT NULL,
	ToUserId INT NOT NULL,
	Message varchar(280) NOT NULL,
    CONSTRAINT pk_message PRIMARY KEY (id),
	CONSTRAINT fk_message_user_from FOREIGN KEY (FromUserId) REFERENCES [user] (Id),
	CONSTRAINT fk_message_user_to FOREIGN KEY (ToUserId) REFERENCES [user] (Id)
);

COMMIT TRANSACTION;