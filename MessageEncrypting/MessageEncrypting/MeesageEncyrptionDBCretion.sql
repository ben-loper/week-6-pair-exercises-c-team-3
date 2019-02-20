
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
    id integer NOT NULL IDENTITY(1,1),
    username varchar(64) UNIQUE NOT NULL,
    password character(64) NOT NULL,
    CONSTRAINT pk_user PRIMARY KEY (id)
);

CREATE TABLE message (
    id integer NOT NULL IDENTITY(1,1),
    fromuserid INT NOT NULL,
	touserid INT NOT NULL,
	message varchar(280) NOT NULL,
    CONSTRAINT pk_message PRIMARY KEY (id),
	CONSTRAINT fk_message_user_from FOREIGN KEY (fromuserid) REFERENCES [user] (id),
	CONSTRAINT fk_message_user_to FOREIGN KEY (touserid) REFERENCES [user] (id)
);

COMMIT TRANSACTION;