# School Management System

## Overview

This is a **backend API** built using **.NET Core Web API** to manage students, teachers, courses, classes, attendance, assignments, and grading with **role-based access control** and a **relational database design**.

Roles supported:

* **Admin:** Manage users, departments, and courses
* **Teacher:** Manage classes, attendance, assignments, and grades
* **Student:** View courses, submit assignments, view grades

The system follows best practices in **clean architecture**, **EF Core relationships**, and **JWT-based authentication**.

---

## Features

### Entities

* **User:** Id, Name, Email, Password (hashed), Role, CreatedDate, UpdatedDate
* **Department:** Id, Name, Description, HeadOfDepartmentId, CreatedDate, UpdatedDate
* **Course:** Id, Name, Code, Description, DepartmentId, Credits, CreatedDate, UpdatedDate
* **Class (Batch):** Id, Name, CourseId, TeacherId, Semester, StartDate, EndDate, IsActive, CreatedDate, UpdatedDate
* **StudentClass:** Id, StudentId, ClassId, EnrollmentDate
* **Attendance:** Id, ClassId, StudentId, Date, Status, MarkedByTeacherId, CreatedDate
* **Assignment:** Id, ClassId, Title, Description, DueDate, CreatedDate, CreatedByTeacherId
* **Submission:** Id, AssignmentId, StudentId, SubmittedDate, FileUrl, Grade, GradedByTeacherId, Remarks
* **Notification (Optional):** Id, Title, Message, RecipientRole, RecipientId, CreatedDate, IsRead

---

## Authentication & Authorization

* JWT-based authentication
* Role-based access:

  * Admin, Teacher, Student
* Endpoints:

  * `POST /api/auth/register` → Register new user
  * `POST /api/auth/login` → Login and receive JWT
  * `POST /api/auth/refresh-token` → Refresh JWT token

---

## Admin APIs

* CRUD for Departments, Courses, and optionally Users
* Validation rules:

  * Unique department names
  * Unique course codes per department
  * Only assign teachers as Head of Department

## Teacher APIs

* Manage Classes: create, update, deactivate, assign students
* Attendance: mark and view history
* Assignments: create, view, grade submissions
* Notifications: send to class/students

## Student APIs

* View enrolled classes
* View attendance
* Submit assignments
* View grades
* Receive notifications

---

## Data Validation Rules

* Valid email format and password strength
* Prevent duplicate enrollments
* Assignment due date cannot be in the past
* Only assigned teacher can mark attendance or grade submissions
* Admin-only endpoints restricted to Admin role

---

## Tech Stack

* **Backend:** .NET Core Web API
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Authentication:** JWT
* **Optional Tools:** AutoMapper, FluentValidation, Serilog

---

## Setup Instructions

1. Clone the repository:

```bash
git clone https://github.com/Abdallah-200/School-Management-System.git
cd "School Management System"
```

2. Configure **connection string** in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=SchoolDB;Trusted_Connection=True;"
}
```

3. Install dependencies:

```bash
dotnet restore
```

4. Run database migrations:

```bash
dotnet ef migrations add InitialCreate -s School Management System.API
 dotnet ef database update -s School Management System.API
```

5. Run the project:

```bash
dotnet run --project School Management System.API
```

6. Open **Swagger** to test APIs:
   `https://localhost:{PORT}/swagger/index.html`

---

## Sample API Requests (Swagger recommended)

* Register User: `POST /api/auth/register`
* Login: `POST /api/auth/login`
* Create Department (Admin only): `POST /api/admin/departments`
* Create Course (Admin only): `POST /api/admin/courses`
* Create Class (Teacher only): `POST /api/teacher/classes`
* Submit Assignment (Student only): `POST /api/student/assignments/{id}/submit`

---

## Notes

* Async/await is used for database operations
* Pagination and filtering implemented for large datasets
* Logging via Serilog (optional)
* Soft delete supported (IsActive = false)
* Notifications and file uploads are optional features

