using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents an external identifier for a user.
/// Used to link OneRoster users to external systems (e.g., LDAP, LTI, Active Directory).
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_UserId">OneRoster UserId specification</see>
/// </remarks>
public class UserId
{
    public required string Type { get; set; }
    public required string Identifier { get; set; }
}
