# CMCS - Claim Management and Communication System

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Platform](https://img.shields.io/badge/platform-.NET-purple.svg)
![C#](https://img.shields.io/badge/C%23-10.0+-blue.svg)
![ASP.NET](https://img.shields.io/badge/ASP.NET-Core-green.svg)

A Claim Management and Communication System (CMCS) developed for academic institutions to streamline the claim submission, verification, and approval process for lecturers.
## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Project Structure](#project-structure)
- [Database Setup](#database-setup)
- [Building and Running](#building-and-running)
- [User Roles](#user-roles)
- [System Features by Role](#system-features-by-role)
- [Data Models](#data-models)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## 🎯 Overview

**CMCS (Claim Management and Communication System)** is a comprehensive claim management platform designed specifically for academic institutions. The application provides a seamless experience for lecturers to submit monthly claims for hours worked, while enabling Programme Coordinators and Academic Managers to review, verify, and approve claims efficiently. The system includes automated validation, document management, and real-time status tracking.

### Key Highlights

- **Multi-Role System**: Separate interfaces for Lecturers, Programme Coordinators, and Academic Managers
- **Automated Validation**: Built-in claim verification with configurable business rules
- **Document Management**: Support for uploading and storing supporting documentation
- **Real-time Tracking**: Live claim status updates and notification system
- **Comprehensive Reporting**: Analytics and claim history for all users
- **Secure Authentication**: Role-based access control with encrypted credentials

---

## ✨ Features

### Lecturer Features
- **User Authentication**: Secure login with role-based access
- **Claim Submission**: Submit monthly claims with hours worked and hourly rates
- **Document Upload**: Attach supporting documents (PDF, Word, etc.)
- **Claim Tracking**: View claim status (Pending, Approved, Rejected)
- **Claim History**: Access historical claim records
- **Profile Management**: Update personal information (email, phone, address)
- **Automatic Calculation**: System calculates total claim amount automatically
- **Notifications**: Receive updates on claim status changes

### Programme Coordinator Features
- **Claim Dashboard**: View all submitted claims requiring review
- **Claim Verification**: Verify claims against validation criteria
- **Analytics**: View claim statistics and trends
- **Lecturer Management**: Access lecturer information and claim history
- **Document Review**: Download and review supporting documentation
- **Search & Filter**: Filter claims by status, date, lecturer, or module
- **Add Notes**: Provide feedback on claims
- **Notification System**: Receive alerts for new claim submissions

### Academic Manager Features
- **Final Approval**: Approve or reject claims reviewed by coordinators
- **Comprehensive Reports**: Generate detailed claim and payment reports
- **User Management**: Manage lecturers and coordinators
- **Financial Overview**: View total approved claims and budget tracking
- **System Administration**: Configure validation rules and system settings
- **Audit Trail**: Track all claim actions and status changes

### Common Features
- **Secure Authentication**: Role-based login system
- **Responsive Interface**: Clean and intuitive user interface
- **Real-time Updates**: Automatic data synchronization
- **Data Validation**: Built-in validation for all inputs
- **Database Integration**: SQL Server backend with Entity Framework Core

---

## 🛠 Technology Stack

### Core Technologies
- **Language**: C# 10.0+
- **Framework**: ASP.NET Core
- **Architecture**: MVC (Model-View-Controller)
- **ORM**: Entity Framework Core
- **Database**: Microsoft SQL Server

### Libraries & Dependencies

#### ASP.NET Core
- `Microsoft.AspNetCore.Mvc` - MVC framework
- `Microsoft.AspNetCore.Authentication` - Authentication middleware
- `Microsoft.AspNetCore.Authorization` - Authorization policies
- `Microsoft.AspNetCore.Identity` - User management

#### Entity Framework Core
- `Microsoft.EntityFrameworkCore` - ORM framework
- `Microsoft.EntityFrameworkCore.SqlServer` - SQL Server provider
- `Microsoft.EntityFrameworkCore.Tools` - Migration tools

#### Data Validation
- `System.ComponentModel.DataAnnotations` - Model validation
- `System.ComponentModel.DataAnnotations.Schema` - Database schema attributes

#### Security
- `System.Security.Claims` - Claims-based authorization
- Password hashing and encryption

#### Other Libraries
- `System.Linq` - LINQ queries
- `System.Collections.Generic` - Generic collections
- `Microsoft.Extensions.DependencyInjection` - Dependency injection

---

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

- **Visual Studio**: 2022 or later (Community, Professional, or Enterprise)
- **.NET SDK**: .NET 6.0 or later
- **SQL Server**: SQL Server 2019 or later (Express, Developer, or Enterprise)
- **SQL Server Management Studio (SSMS)**: Latest version recommended
- **Git**: For version control

### Minimum System Requirements
- Windows 10/11 or Windows Server 2016+
- 4GB RAM minimum (8GB recommended)
- 10GB free disk space
- Internet connection for NuGet packages

---

## 📦 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/VCPTA/bca3-prog7314-part-2-submission-ST10396650.git
cd bca3-prog7314-part-2-submission-ST10396650
```

### 2. Open in Visual Studio

1. Launch Visual Studio 2022
2. Click "Open a project or solution"
3. Navigate to the cloned repository folder
4. Open `POEFINAL_CMCS_ST10396650.sln`
5. Wait for NuGet packages to restore

### 3. Configure Database Connection

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=CMCSDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g., `localhost`, `.\SQLEXPRESS`, or `(localdb)\MSSQLLocalDB`).

### 4. Build the Project

```bash
dotnet build
```

Or use Visual Studio:
- **Build → Build Solution** (Ctrl+Shift+B)

---

## 📁 Project Structure

```
POEFINAL_CMCS_ST10396650/
├── Models/
│   ├── AcademicManager.cs          # Academic Manager entity
│   ├── Claim.cs                    # Claim entity (base)
│   ├── ClaimModel.cs               # Extended claim model with validation
│   ├── ClaimHistory.cs             # Claim history tracking
│   ├── Lecture.cs                  # Lecturer entity
│   ├── ProgrammeCoordinator.cs     # Programme Coordinator entity
│   └── ReviewedClaim.cs            # Reviewed claim entity
├── Services/
│   ├── IClaimVerificationService.cs       # Claim verification interface
│   └── ClaimVerificationService.cs        # Claim verification implementation
├── Data/
│   └── ApplicationDbContext.cs     # EF Core DbContext
├── Controllers/
│   ├── LecturerController.cs       # Lecturer operations
│   ├── ClaimController.cs          # Claim management
│   ├── CoordinatorController.cs    # Coordinator operations
│   └── ManagerController.cs        # Manager operations
├── Views/
│   ├── Lecturer/                   # Lecturer views
│   ├── Coordinator/                # Coordinator views
│   ├── Manager/                    # Manager views
│   └── Shared/                     # Shared layouts and partials
├── wwwroot/
│   ├── css/                        # Stylesheets
│   ├── js/                         # JavaScript files
│   └── lib/                        # Third-party libraries
├── Database/
│   └── SQLQuery1.sql               # Database creation script
├── appsettings.json                # Application configuration
├── Program.cs                      # Application entry point
└── Startup.cs                      # Service configuration
```

---

## 🗄️ Database Setup

### 1. Create the Database

#### Using SQL Server Management Studio (SSMS)

1. Open SSMS and connect to your SQL Server instance
2. Open the `SQLQuery1.sql` file from the `Database` folder
3. Execute the entire script (F5)
4. Verify the database and tables were created successfully

### 2. Database Schema Overview

The system uses the following database structure:

#### Tables

**Lecturers**
- Primary table for lecturer information
- Contains authentication credentials and personal details
- Fields: LecturerId, Username, Password, FullName, Course, Email, PhoneNumber, Address, HireDate

**Claims**
- Stores all claim submissions
- Links to Lecturers table via foreign key
- Fields: ClaimId, LecturerId, Month, HoursWorked, HourlyRate, Total, Status, Notes, SubmissionDate, Document, Module

**ProgrammeCoordinator**
- Stores Programme Coordinator accounts
- Fields: CoordinatorId, FullName, Password

**AcademicManager**
- Stores Academic Manager accounts
- Fields: ManagerId, FullName, Password

**ReviewedClaims**
- Tracks claim review history
- Links to Claims, Lecturers, and ProgrammeCoordinator tables
- Fields: ReviewId, ClaimId, LecturerId, CoordinatorId, Status, StatusApproval, ReviewedDate

**Notifications**
- Stores system notifications
- Fields: NotificationId, CoordinatorId, Message, IsRead, CreatedDate

### 3. Initial Data

The script includes sample data for testing:

- **Programme Coordinator**: Username: `Jane Doe`, Password: `password123`
- **Academic Manager**: Username: `Cinderella`, Password: `password123`
- **Lecturer**: Username: `johndoe`, Password: `password123`


### 4. Database Connection Verification

Test your connection string in Visual Studio:
1. Open **Server Explorer**
2. Right-click **Data Connections** → **Add Connection**
3. Enter your server name and test the connection

---

## 🚀 Building and Running

### Using Visual Studio

1. **Set Startup Project**:
   - Right-click the project in Solution Explorer
   - Select "Set as Startup Project"

2. **Run the Application**:
   - Press F5 (Debug mode) or Ctrl+F5 (Release mode)
   - The application will open in your default browser

3. **Default URL**:
   ```
   https://localhost:5001
   http://localhost:5000
   ```
---

## 👥 User Roles

The application supports three user roles:

### 1. Lecturer
- Submit monthly claims for hours worked
- Upload supporting documentation
- Track claim status
- View claim history
- Update profile information

### 2. Programme Coordinator
- Review submitted claims
- Verify claim documentation
- Approve or reject claims
- Add review notes
- View lecturer information

### 3. Academic Manager
- Final approval authority
- View all claims and reviews
- Generate reports
- Manage system users
- Access analytics dashboard

---

## 📱 Workflows by Role

### Lecturer Workflow

1. **Login** → Authenticate with credentials
2. **Dashboard** → View claim summary and status
3. **Submit Claim** → Enter hours worked, hourly rate, and month
4. **Upload Document** → Attach supporting documentation
5. **Submit** → System validates and calculates total
6. **Track** → Monitor claim status (Pending → Under Review → Approved/Rejected)
7. **History** → View past claims and payments

### Programme Coordinator Workflow

1. **Login** → Authenticate with coordinator credentials
2. **Dashboard** → View pending claims requiring review
3. **Review Claim** → Examine claim details and documentation
4. **Verify** → Check against validation criteria:
   - Hours worked ≤ 40 per week
   - Hourly rate between R50 and R200
   - Supporting documentation provided
   - No duplicate submissions
5. **Approve/Reject** → Update claim status with notes
6. **Forward** → Send approved claims to Academic Manager
7. **Reports** → Generate coordinator-level reports

### Academic Manager Workflow

1. **Login** → Authenticate with manager credentials
2. **Dashboard** → View claims awaiting final approval
3. **Final Review** → Review coordinator recommendations
4. **Approve for Payment** → Authorize payment processing
5. **Reports** → Generate financial and statistical reports
6. **User Management** → Add/edit system users
7. **System Config** → Configure validation rules

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. **Fork the Repository**
```bash
git clone https://github.com/YOUR-USERNAME/POEFINAL_CMCS_ST10396650.git
```

2. **Create a Feature Branch**
```bash
git checkout -b feature/YourFeatureName
```

3. **Commit Your Changes**
```bash
git commit -m "Add some feature"
```

4. **Push to Branch**
```bash
git push origin feature/YourFeatureName
```

5. **Open a Pull Request**

---

## 📄 License

This project is licensed under the MIT License.

```
MIT License

Copyright (c) 2025 Nia Diale

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

### Support

If you encounter any issues or have questions:

1. Check the database connection string
2. Verify SQL Server is running
3. Ensure all NuGet packages are restored
4. Review the application logs
5. Create an issue on GitHub with detailed description

---

## 🙏 Acknowledgments

- **Institution** - Varsity College for educational support and project requirements
- **Microsoft** - For .NET and Entity Framework Core
- **SQL Server** - Database management system
- **ASP.NET Community** - For excellent documentation and resources

## 📚 Additional Resources

### Documentation
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [SQL Server Documentation](https://docs.microsoft.com/en-us/sql/)

### Tutorials
- [ASP.NET Core MVC Tutorial](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/)
- [Entity Framework Core Tutorial](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)
- [C# Best Practices](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

---

**Note**: This is an academic project.
