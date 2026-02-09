using RosterLib.Domain.Common;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents a role assignment from the OneRoster Rostering specification.
/// Defines the relationship between a user and an organization with a specific role type.
/// A user can have multiple roles in multiple organizations.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_Role">OneRoster Role specification</see>
/// </remarks>
public class Role : Base
{
    /// <summary>
    /// Indicates if this role is the primary or secondary role for that org. There MUST be one, and only one, primary role for each org.
    /// </summary>
    public required RoleTypeEnum RoleType { get; set; } // primary | secondary

    /// <summary>
    /// The role of the user in the org. The permitted values are from an enumerated list.
    /// </summary>
    public required RoleEnum RoleName { get; set; } // role vocabulary (student, teacher, etc.)

    /// <summary>
    /// The start date for the role (optional).
    /// </summary>
    public DateOnly? BeginDate { get; set; }

    /// <summary>
    /// The end date for the role (optional).
    /// </summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// The link to the associated user i.e. the sourcedId for the user.
    /// </summary>
    public required string UserSourcedId { get; set; }

    /// <summary>
    /// The link to the associated org i.e. the sourcedId for the org.
    /// </summary>
    public required string OrgSourcedId { get; set; }

    /// <summary>
    /// The link to the associated user profile (optional) i.e. the sourcedId for the user profile.
    /// </summary>
    public string? UserProfileSourcedId { get; set; }
}
