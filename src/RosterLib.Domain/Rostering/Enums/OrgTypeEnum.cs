using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering.Enums;

/// <summary>
/// Represents the type of an organization.
/// This vocabulary may be extended with custom values.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Enumerated_OrgTypeEnum">OneRoster orgType vocabulary</see>
/// </remarks>
public record OrgTypeEnum : ClassEnum<string>
{
    public static readonly OrgTypeEnum Department = new("department");
    public static readonly OrgTypeEnum District = new("district");
    public static readonly OrgTypeEnum Local = new("local");
    public static readonly OrgTypeEnum National = new("national");
    public static readonly OrgTypeEnum School = new("school");
    public static readonly OrgTypeEnum State = new("state");

    public OrgTypeEnum(string value) : base(value) { }
}