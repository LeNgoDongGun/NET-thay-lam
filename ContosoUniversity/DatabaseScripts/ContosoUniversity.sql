/*
========================================================
CONTOSO UNIVERSITY DATABASE SCRIPT
========================================================
Mục tiêu:
- Tạo database ContosoUniversity
- Tạo các bảng:
+ Student
+ Course
+ Enrollment
- Thêm dữ liệu mẫu
Sử dụng:
- SQL Server Express
- Chạy trực tiếp trong VS Code
========================================================
*/

/*
========================================================
XÓA DATABASE CŨ (NẾU ĐÃ TỒN TẠI)
========================================================
Nếu database đã tồn tại:
- Script sẽ xóa database cũ
- Sau đó tạo lại từ đầu
Lưu ý:
- Chỉ dùng trong quá trình học/demo
- Không dùng trên hệ thống production
========================================================
*/
USE master;
GO
IF EXISTS (
SELECT *
FROM sys.databases
WHERE name = 'ContosoUniversity'
)
BEGIN
ALTER DATABASE ContosoUniversity
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
DROP DATABASE ContosoUniversity;
END
GO

/*
========================================================
TẠO DATABASE
========================================================
*/

CREATE DATABASE ContosoUniversity;
GO

/*
========================================================
CHUYỂN SANG DATABASE VỪA TẠO
========================================================
*/

USE ContosoUniversity;  
GO

/*
========================================================
TẠO BẢNG STUDENT
========================================================
Ý nghĩa:
- Lưu thông tin sinh viên
Các cột:
- ID:
Khóa chính (Primary Key)
Tự động tăng
- LastName:
Họ
- FirstMidName:
Tên
- EnrollmentDate:
Ngày nhập học
========================================================
*/

CREATE TABLE Student
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    LastName NVARCHAR(50) NOT NULL,
    FirstMidName NVARCHAR(50) NOT NULL,
    EnrollmentDate DATETIME NOT NULL
);
GO
/*
========================================================
TẠO BẢNG COURSE
========================================================
Ý nghĩa:
- Lưu thông tin khóa học
Các cột:
- CourseID:
Mã khóa học
- Title:
Tên môn học
- Credits:
Số tín chỉ
========================================================
*/
CREATE TABLE Course
(
CourseID INT PRIMARY KEY,
Title NVARCHAR(100) NOT NULL,
Credits INT NOT NULL
);
GO

/*
========================================================
TẠO BẢNG ENROLLMENT
========================================================
Ý nghĩa:
- Bảng trung gian
- Lưu thông tin sinh viên đăng ký môn học
Quan hệ:
- Student 1 - N Enrollment
- Course 1 - N Enrollment
Các cột:
- EnrollmentID:
Khóa chính
- CourseID:
Khóa ngoại tới Course
- StudentID:
Khóa ngoại tới Student
- Grade:
Điểm
========================================================
*/
CREATE TABLE Enrollment
(
EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
CourseID INT NOT NULL,
StudentID INT NOT NULL,
Grade INT NULL,
CONSTRAINT FK_Enrollment_Course
FOREIGN KEY (CourseID)
REFERENCES Course(CourseID),
CONSTRAINT FK_Enrollment_Student
FOREIGN KEY (StudentID)
REFERENCES Student(ID)
);
GO
/*
========================================================
THÊM DỮ LIỆU MẪU CHO COURSE
========================================================
*/
INSERT INTO Course (CourseID, Title, Credits)
VALUES
(1050, 'Chemistry', 3),
(4022, 'Microeconomics', 3),
(4041, 'Macroeconomics', 3),
(1045, 'Calculus', 4),
(3141, 'Trigonometry', 4);
GO

/*
========================================================
THÊM DỮ LIỆU MẪU CHO STUDENT
========================================================
*/

INSERT INTO Student
(
LastName,
FirstMidName,
EnrollmentDate
)
VALUES
('Alexander', 'Carson', GETDATE()),
('Alonso', 'Meredith', GETDATE()),
('Anand', 'Arturo', GETDATE()),
('Barzdukas', 'Gytis', GETDATE()),
('Li', 'Yan', GETDATE());
GO

/*
========================================================
THÊM DỮ LIỆU MẪU CHO ENROLLMENT
========================================================
*/

INSERT INTO Enrollment
(
CourseID,
StudentID,
Grade
)
VALUES
(1050, 1, 0),
(4022, 1, 1),
(4041, 2, 2),
(1045, 3, 0),
(3141, 4, 1);
GO

/*
========================================================
KIỂM TRA DỮ LIỆU
========================================================
SELECT * FROM Student;
SELECT * FROM Course;
SELECT * FROM Enrollment;
========================================================
*/

SELECT * FROM Student;
SELECT * FROM Course;
SELECT * FROM Enrollment;
GO

/*
========================================================
HOÀN THÀNH
========================================================
*/

