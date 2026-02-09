using RosterLib.Domain.Common;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents a class in the OneRoster Rostering specification.
/// A class is a scheduled instance of a course delivered by a school.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_Class">OneRoster Class specification</see>
/// </remarks>
public class Class : Base
{
    public required string Title { get; set; }
    public List<string>? Grades { get; set; }
    public required string CourseSourcedId { get; set; }
    public string? ClassCode { get; set; }
    public required ClassTypeEnum ClassType { get; set; }
    public string? Location { get; set; }
    public required string SchoolSourcedId { get; set; }
    public required List<string> TermSourcedIds { get; set; }
    public List<string>? Subjects { get; set; }
    public List<string>? SubjectCodes { get; set; }
    public List<string>? Periods { get; set; }
}
