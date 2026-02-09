namespace RosterLib.Domain.Common;

/// <summary>
/// Represents the status of a OneRoster object.
/// Used in delta processing to indicate active objects or objects marked for deletion.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Enumerated_BaseStatusEnum">OneRoster status vocabulary</see>
/// </remarks>
public record StatusEnum : ClassEnum<string>
{
    public static readonly StatusEnum Active = new("active");
    public static readonly StatusEnum ToBeDeleted = new("tobedeleted");

    public StatusEnum(string value) : base(value) { }
}