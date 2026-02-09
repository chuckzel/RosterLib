using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering.Enums;

/// <summary>
/// Represents the type of an academic session.
/// This vocabulary may be extended with custom values.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Enumerated_AcademicSessionTypeEnum">OneRoster academicSession type vocabulary</see>
/// </remarks>
public record AcademicSessionTypeEnum : ClassEnum<string>
{
    public static readonly AcademicSessionTypeEnum GradingPeriod = new("gradingPeriod");
    public static readonly AcademicSessionTypeEnum Semester = new("semester");
    public static readonly AcademicSessionTypeEnum SchoolYear = new("schoolYear");
    public static readonly AcademicSessionTypeEnum Term = new("term");

    public AcademicSessionTypeEnum(string value) : base(value) { }
}
