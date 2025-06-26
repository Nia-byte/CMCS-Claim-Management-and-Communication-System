CREATE DATABASE CMCSDB;
GO

-- Use the newly created database
USE CMCSDB;
GO

-- Create the Lecturers table
CREATE TABLE Lecturers (
    LecturerId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
	Course NVARCHAR(10) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    HireDate DATE NOT NULL
);
GO


SELECT * FROM Claims;
  

-- Create the Claims table
CREATE TABLE Claims (
    ClaimId INT PRIMARY KEY IDENTITY(1,1),
    LecturerId INT NOT NULL,
    HoursWorked DECIMAL(18,2) NOT NULL,
    HourlyRate DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    Notes NVARCHAR(200),
    SubmissionDate DATETIME NOT NULL,
	 Month NVARCHAR(20),          
    Total DECIMAL(18,2),        
    Document VARBINARY(MAX),
	Module NVARCHAR(10) ,
    FOREIGN KEY (LecturerId) REFERENCES Lecturers(LecturerId)
);
GO


-- Create the ProgrammeCoordinator table
CREATE TABLE ProgrammeCoordinator
(
    CoordinatorId INT PRIMARY KEY IDENTITY(1,1), 
    FullName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL 
);
GO

-- Create the AcademicManager table
CREATE TABLE AcademicManager
(
    ManagerId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL 
);
GO

CREATE TABLE ReviewedClaims (
    ReviewId INT PRIMARY KEY IDENTITY(1,1),  -- Auto-increment ID for each review
    ClaimId INT NOT NULL,                    -- Foreign key to Claims table
    LecturerId INT NOT NULL,                 -- Foreign key to Lecturers table
    CoordinatorId INT NOT NULL,              -- Foreign key to ProgrammeCoordinator table
    Status NVARCHAR(20) NOT NULL,            -- Current status (e.g., Approved, Rejected)
    StatusApproval NVARCHAR(50) NOT NULL,    -- Approval status (e.g., Waiting for Approval, Approved, Rejected)
    ReviewedDate DATETIME DEFAULT GETDATE(), -- Date of review action
    FOREIGN KEY (ClaimId) REFERENCES Claims(ClaimId),
    FOREIGN KEY (LecturerId) REFERENCES Lecturers(LecturerId),
    FOREIGN KEY (CoordinatorId) REFERENCES ProgrammeCoordinator(CoordinatorId)
);
GO

select * from ReviewedClaims;
INSERT INTO ReviewedClaims ( ClaimId, LecturerId, CoordinatorId, Status, StatusApproval, ReviewedDate)
VALUES (5, 1, 1,'Approved' , 'Waiting for Approval',  GETDATE());


CREATE TABLE Notifications (
    NotificationId INT PRIMARY KEY IDENTITY(1,1),
    CoordinatorId INT NOT NULL,              -- Foreign key to ProgrammeCoordinator table
    Message NVARCHAR(255) NOT NULL,         -- Notification message
    IsRead BIT DEFAULT 0,                   -- Mark whether notification has been read
    CreatedDate DATETIME DEFAULT GETDATE(), -- Timestamp for the notification
    FOREIGN KEY (CoordinatorId) REFERENCES ProgrammeCoordinator(CoordinatorId)
);
GO

INSERT INTO ProgrammeCoordinator ( FullName, Password)
VALUES ('Jane Doe', 'password123');

INSERT INTO AcademicManager ( FullName, Password)
VALUES ('Cinderella', 'password123');


INSERT INTO Claims (LecturerId, HoursWorked, HourlyRate, Status, Notes, SubmissionDate)
VALUES (1, 10.00, 25.00, 'Pending', 'TEST4.', GETDATE());

INSERT INTO Lecturers ( Username, Password, FullName)
VALUES ('johndoe', 'password123', 'John Doe');


