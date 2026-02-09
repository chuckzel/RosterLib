using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering.Enums;

/// <summary>
/// Represents the class type.
/// This vocabulary may be extended with custom values.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Enumerated_ClassTypeEnum">OneRoster classType vocabulary</see>
/// </remarks>
public record ClassTypeEnum : ClassEnum<string>
{
    public static readonly ClassTypeEnum Homeroom = new("homeroom");
    public static readonly ClassTypeEnum Scheduled = new("scheduled");

    public ClassTypeEnum(string value) : base(value) { }
}
