using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering.Enums;

/// <summary>
/// Represents an enrollment role.
/// This vocabulary may be extended with custom values.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Enumerated_EnrolRoleEnum">OneRoster enrollment role vocabulary</see>
/// </remarks>
public record EnrollmentRoleEnum : ClassEnum<string>
{
    public static readonly EnrollmentRoleEnum Administrator = new("administrator");
    public static readonly EnrollmentRoleEnum Proctor = new("proctor");
    public static readonly EnrollmentRoleEnum Student = new("student");
    public static readonly EnrollmentRoleEnum Teacher = new("teacher");

    public EnrollmentRoleEnum(string value) : base(value) { }
}
