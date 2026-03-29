using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for Enrollment entity.
/// Maps to enrollments.csv in OneRoster format.
/// </summary>
public sealed class EnrollmentMap : BaseMap<Enrollment>
{
    public EnrollmentMap()
    {
        Map(m => m.ClassSourcedId).Name("classSourcedId").Index(3);
        Map(m => m.SchoolSourcedId).Name("schoolSourcedId").Index(4);
        Map(m => m.UserSourcedId).Name("userSourcedId").Index(5);
        Map(m => m.Role).Name("role").Index(6).TypeConverter<ClassEnumConverter<EnrollmentRoleEnum>>();
        Map(m => m.Primary).Name("primary").Index(7).Optional();
        Map(m => m.BeginDate).Name("beginDate").Index(8).Optional().TypeConverter<DateOnlyConverter>();
        Map(m => m.EndDate).Name("endDate").Index(9).Optional().TypeConverter<DateOnlyConverter>();
    }
}
