using RosterLib.Domain.Rostering;

namespace RosterLib.Domain;

/// <summary>
/// Represents a complete OneRoster dataset snapshot.
/// </summary>
public class OneRosterSnapshot
{
    public List<AcademicSession> AcademicSessions { get; set; } = [];
    public List<Class> Classes { get; set; } = [];
    public List<Course> Courses { get; set; } = [];
    public List<Demographics> Demographics { get; set; } = [];
    public List<Enrollment> Enrollments { get; set; } = [];
    public List<Org> Orgs { get; set; } = [];
    public List<Role> Roles { get; set; } = [];
    public List<User> Users { get; set; } = [];
    public List<UserProfile> UserProfiles { get; set; } = [];
}
