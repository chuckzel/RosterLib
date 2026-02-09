using RosterLib.Domain.Common;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents an academic session in the OneRoster Rostering specification.
/// Academic sessions include school years, terms, semesters, and grading periods.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_AcademicSession">OneRoster AcademicSession specification</see>
/// </remarks>
public class AcademicSession : Base
{
    public required string Title { get; set; }
    public required AcademicSessionTypeEnum Type { get; set; }
    public required DateOnly StartDate { get; set; }
    public required DateOnly EndDate { get; set; }
    public string? ParentSourcedId { get; set; }
    public required string SchoolYear { get; set; }
}
