using RosterLib.Domain.Common;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents an enrollment in the OneRoster Rostering specification.
/// An enrollment links a user to a class with a specific role.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_Enrollment">OneRoster Enrollment specification</see>
/// </remarks>
public class Enrollment : Base
{
    public required string ClassSourcedId { get; set; }
    public required string SchoolSourcedId { get; set; }
    public required string UserSourcedId { get; set; }
    public required EnrollmentRoleEnum Role { get; set; }
    public bool? Primary { get; set; }
    public DateOnly? BeginDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
