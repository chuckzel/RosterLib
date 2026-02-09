using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering.Enums;

/// <summary>
/// Represents a sex value in demographics.
/// This vocabulary may be extended with custom values.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Enumerated_GenderEnum">OneRoster sex vocabulary</see>
/// </remarks>
public record GenderEnum : ClassEnum<string>
{
    public static readonly GenderEnum Male = new("male");
    public static readonly GenderEnum Female = new("female");
    public static readonly GenderEnum Unspecified = new("unspecified");
    public static readonly GenderEnum Other = new("other");

    public GenderEnum(string value) : base(value) { }
}
