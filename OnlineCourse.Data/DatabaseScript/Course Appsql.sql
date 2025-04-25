----CREATE DATABASE OnlineCourse

-- UserProfile TABLE
CREATE TABLE UserProfile(
	id INT IDENTITY(1, 1),
	username NVARCHAR(100) NOT NULL CONSTRAINT DF_userProfile_username DEFAULT 'Guest',
	firstName NVARCHAR(100) NOT NULL,
	lastName NVARCHAR(100) NOT NULL,
	email NVARCHAR(100) NOT NULL,
	adObjId NVARCHAR(128) NOT NULL

	CONSTRAINT PK_UserProfile_id PRIMARY KEY (id)
)

-- Roles TABLE
CREATE TABLE Roles(
	id INT IDENTITY(1,1) PRIMARY KEY,
	roleName NVARCHAR(50) NOT NULL,
)

---- UserRole TABLE
CREATE TABLE UserRole(
	id INT IDENTITY(1, 1),
	roleId INT NOT NULL,
	userId INT NOT NULL,

	CONSTRAINT PK_UserRole_id PRIMARY KEY (id),
	CONSTRAINT FK_UserRole_UserProfile FOREIGN KEY (userId) REFERENCES UserProfile( id ),
	CONSTRAINT FK_UserRole_Roles FOREIGN KEY (roleId) REFERENCES Roles(id)
)	

-- CourseCategory TABLE
CREATE TABLE CourseCategory(
	id INT IDENTITY(1, 1) PRIMARY KEY,
	categoryName NVARCHAR(50) NOT NULL,
	description NVARCHAR(250),
)

-- Instructor TABLE
CREATE TABLE Instructor(
	id INT IDENTITY(1, 1),
	firstName NVARCHAR(50) NOT NULL,
	lastName NVARCHAR(50) NOT NULL,
	email NVARCHAR(100) NOT NULL,
	biography NVARCHAR(MAX),
	userId INT NOT NULL,

	CONSTRAINT PK_Instructor_id PRIMARY KEY (id),
	CONSTRAINT FK_Instructor_UserProfile FOREIGN KEY (userId) REFERENCES UserProfile(id)
)

-- Course TABLE	
CREATE TABLE Course(
	id INT IDENTITY(1, 1) PRIMARY KEY,
	title NVARCHAR(100) NOT NULL,
	description NVARCHAR(MAX) NOT NULL,
	price DECIMAL (18, 2) NOT NULL,
	courseType NVARCHAR(10) NOT NULL CHECK ( courseType IN ('Online', 'Offline')),
	seatsAvailable INT CHECK ( seatsAvailable >= 0 ),
	duration DECIMAL(5, 2) NOT NULL, -- duration in hours
	categoryId INT NOT NULL,
	instructorId INT NOT NULL,
	startDate DATETIME,
	endDate DATETIME,

	CONSTRAINT FK_Course_CourseCategory FOREIGN KEY ( categoryId ) REFERENCES CourseCategory(id),
	CONSTRAINT FK_Course_Instructor FOREIGN KEY ( instructorId ) REFERENCES Instructor(id) 
)

-- SessionDetails TABLE
CREATE TABLE SessionDetails(
	id INT IDENTITY(1, 1),
	courseId INT NOT NULL,
	title NVARCHAR(50) NOT NULL,
	description NVARCHAR(MAX),
	videoURL NVARCHAR(500),
	videoOrder INT NOT NULL,

	CONSTRAINT PK_SessionDetails_id PRIMARY KEY (id),
	CONSTRAINT FK_SessionDetails_Course FOREIGN KEY ( courseId ) REFERENCES Course( id )
)

-- Enrollment TABLE 
CREATE TABLE Enrollment (
	id INT IDENTITY(1, 1) PRIMARY KEY,
	courseId INT NOT NULL,
	userId INT NOT NULL,
	enrollmentDate DATETIME NOT NULL DEFAULT GETDATE(),
	paymentStatus NVARCHAR(20) NOT NULL CHECK (paymentStatus IN ('Pending', 'Completed', 'Failed')),
	
	CONSTRAINT FK_Enrollment_Course FOREIGN KEY (courseId) REFERENCES Course(id),
	CONSTRAINT FK_Enrollment_UserProfile FOREIGN KEY (userId) REFERENCES UserProfile(id),
)

-- Payment TABLE
CREATE TABLE Payment(
	id INT IDENTITY(1, 1) PRIMARY KEY,
	enrollmentId INT NOT NULL,
	amount DECIMAL(18, 2) NOT NULL,
	paymentDate DATETIME NOT NULL DEFAULT GETDATE(),
	paymentMethod NVARCHAR(50) NOT NULL,
	paymentStatus NVARCHAR(20) CHECK (paymentStatus IN ('Pending', 'Completed', 'Failed')),

	CONSTRAINT FK_Payment_Enrollment FOREIGN KEY (enrollmentId) REFERENCES Enrollment(id)
)

-- Review TABLE
CREATE TABLE Review(
	id INT IDENTITY(1, 1) PRIMARY KEY,
	courseId INT NOT NULL,
	userId INT NOT NULL,
	rating INT NOT NULL CHECK (rating BETWEEN 1 AND 5),
	comments NVARCHAR(MAX),
	reviewDate DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT FK_Review_Course FOREIGN KEY (courseId) REFERENCES Course(id),
	CONSTRAINT FK_Review_UserProfile FOREIGN KEY (userId) REFERENCES UserProfile(id)
)

INSERT INTO UserProfile(username, firstName, lastName, email, adObjId)
VALUES
('John Doe', 'John', 'Doe', 'john.doe@example.com', 'ad-obj-id-001'),
('Jane Smith', 'Jane', 'Smith', 'jane.smith@example.com', 'ad-obj-id-002'),
('Johnny Bravo', 'Johnny', 'Bravo', 'johnny.bravo@example.com', 'ad-obj-id-001')

INSERT INTO Roles (roleName)
VALUES 
('Admin'),
('Instructor'),
('Student')

INSERT INTO UserRole(roleId, userId)
VALUES
(1, 1),
(2, 2),
(3, 3)

INSERT INTO CourseCategory(categoryName, description)
VALUES
('Programming', 'Courses related to software development'),
('Data science', 'Courses covering data science concepts'),
('Design', 'Courses related to graphic design')

INSERT INTO Instructor(firstName, lastName, email, biography, userId)
VALUES
('Jane', 'Smith', 'jane.smith@example.com', 'Experienced software engineer with 10 years in the industry.', 2)

INSERT INTO Course(title, description, price, courseType, seatsAvailable, duration, categoryId, instructorId, startDate, endDate)
VALUES
('Angular full course', 'Comprehensive course covering angular', 99.99, 'Online', 5, 4.3, 1, 2, GETDATE(), GETDATE() + 3)

INSERT INTO SessionDetails(courseId, title, description, videoURL, videoOrder)
VALUES
(1, 'Introduction to angular', 'Overview of angular and its core concepts', 'https://example.com', 1),
(2, 'Data science Intro', 'Overview of data sciencse and its core concepts', 'https://example.com', 2),
(3, 'Introduction to design', 'Overview of design and its core concepts', 'https://example.com', 1)

INSERT INTO Enrollment (courseId, userId, enrollmentDate, paymentStatus)
VALUES
(1, 3, GETDATE(), 'Completed'),
(2, 3, GETDATE(), 'Pending'),
(3, 1, GETDATE(), 'Completed')

INSERT INTO Payment(enrollmentId, amount, paymentDate, paymentMethod, paymentStatus)
VALUES
(1, 199.99, GETDATE(), 'Credit Card', 'Completed')
