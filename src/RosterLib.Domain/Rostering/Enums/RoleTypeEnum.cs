using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering.Enums;

/// <summary>
/// Represents whether a role is the primary or secondary role for a user in an organization.
/// Only one role per organization can be designated as primary.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Enumerated_RoleTypeEnum">OneRoster roleType vocabulary</see>
/// </remarks>
public record RoleTypeEnum : ClassEnum<string>
{
    public static readonly RoleTypeEnum Primary = new("primary");
    public static readonly RoleTypeEnum Secondary = new("secondary");

    public RoleTypeEnum(string value) : base(value) { }
}
