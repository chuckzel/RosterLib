using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering.Enums;

/// <summary>
/// Represents a user's role type.
/// This vocabulary may be extended with custom values.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Enumerated_RoleEnum">OneRoster role vocabulary</see>
/// </remarks>
public record RoleEnum : ClassEnum<string>
{
    public static readonly RoleEnum Student = new("student");
    public static readonly RoleEnum Teacher = new("teacher");
    public static readonly RoleEnum Parent = new("parent");
    public static readonly RoleEnum Guardian = new("guardian");
    public static readonly RoleEnum Principal = new("principal");
    public static readonly RoleEnum SiteAdministrator = new("siteAdministrator");
    public static readonly RoleEnum SystemAdministrator = new("systemAdministrator");

    public RoleEnum(string value) : base(value) { }
}
