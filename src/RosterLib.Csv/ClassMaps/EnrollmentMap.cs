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
        Map(m => m.ClassSourcedId).Name("classSourcedId");
        Map(m => m.SchoolSourcedId).Name("schoolSourcedId");
        Map(m => m.UserSourcedId).Name("userSourcedId");
        Map(m => m.Role).Name("role").TypeConverter<ClassEnumConverter<EnrollmentRoleEnum>>();
        Map(m => m.Primary).Name("primary");
        Map(m => m.BeginDate).Name("beginDate").TypeConverter<DateOnlyConverter>();
        Map(m => m.EndDate).Name("endDate").TypeConverter<DateOnlyConverter>();
    }
}
