# RosterLib.Csv

Helper library for OneRoster v1.2 CSV import/export.

⚠️ **Work in Progress** - This library is under active development and does not yet provide working functionality.

## Features

- **ClassMaps** for all OneRoster entities (User, Role, Org, Class, Course, Enrollment, Demographics, AcademicSession, UserProfile)
- **Type Converters** for ClassEnum types, DateOnly, DateTime, pipe-delimited lists, and metadata dictionaries
- **OneRosterCsvService** for simple read/write operations

## Usage

### Reading CSV Files

```csharp
using RosterLib.Csv;
using RosterLib.Csv.ClassMaps;
using RosterLib.Domain.Rostering;

var csvService = new OneRosterCsvService();

// Read users from users.csv
var users = await csvService.ReadCsvAsync<User, UserMap>("users.csv");

// Read enrollments from enrollments.csv
var enrollments = await csvService.ReadCsvAsync<Enrollment, EnrollmentMap>("enrollments.csv");
```

### Writing CSV Files

```csharp
var users = new List<User>
{
    new User
    {
        SourcedId = Guid.NewGuid().ToString(),
        Username = "jdoe",
        GivenName = "John",
        FamilyName = "Doe",
        EnabledUser = true,
        Email = "jdoe@example.com"
    }
};

await csvService.WriteCsvAsync<User, UserMap>("users.csv", users);
```

### Supported Entity Types

| Entity | ClassMap | CSV File |
|--------|----------|----------|
| User | UserMap | users.csv |
| Role | RoleMap | roles.csv |
| UserProfile | UserProfileMap | userProfiles.csv |
| Org | OrgMap | orgs.csv |
| Class | ClassMap | classes.csv |
| Course | CourseMap | courses.csv |
| Enrollment | EnrollmentMap | enrollments.csv |
| Demographics | DemographicsMap | demographics.csv |
| AcademicSession | AcademicSessionMap | academicSessions.csv |

## CSV Format Notes

### Metadata/Extensions
Dictionary fields are serialized as JSON strings in CSV.

### Nested Collections
- `User.UserIds` collection is ignored in CSV (requires separate handling)
- `UserProfile.Credentials` collection is ignored in CSV (per OneRoster CSV binding spec)

## Example: Full OneRoster Bulk Import

```csharp
var csvService = new OneRosterCsvService();

var orgs = await csvService.ReadCsvAsync<Org, OrgMap>("orgs.csv");
var academicSessions = await csvService.ReadCsvAsync<AcademicSession, AcademicSessionMap>("academicSessions.csv");
var courses = await csvService.ReadCsvAsync<Course, CourseMap>("courses.csv");
var classes = await csvService.ReadCsvAsync<Class, ClassMap>("classes.csv");
var users = await csvService.ReadCsvAsync<User, UserMap>("users.csv");
var enrollments = await csvService.ReadCsvAsync<Enrollment, EnrollmentMap>("enrollments.csv");
var demographics = await csvService.ReadCsvAsync<Demographics, DemographicsMap>("demographics.csv");
var roles = await csvService.ReadCsvAsync<Role, RoleMap>("roles.csv");
var userProfiles = await csvService.ReadCsvAsync<UserProfile, UserProfileMap>("userProfiles.csv");
```
