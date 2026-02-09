using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents a course in the OneRoster Rostering specification.
/// A course defines a catalog-level offering that can be instantiated as classes.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_Course">OneRoster Course specification</see>
/// </remarks>
public class Course : Base
{
    public required string Title { get; set; }
    public string? SchoolYearSourcedId { get; set; }
    public string? CourseCode { get; set; }
    public List<string>? Grades { get; set; }
    public required string OrgSourcedId { get; set; }
    public List<string>? Subjects { get; set; }
    public List<string>? SubjectCodes { get; set; }
}
