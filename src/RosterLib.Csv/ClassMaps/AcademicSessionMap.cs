using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for AcademicSession entity.
/// Maps to academicSessions.csv in OneRoster format.
/// </summary>
public sealed class AcademicSessionMap : BaseMap<AcademicSession>
{
    public AcademicSessionMap()
    {
        Map(m => m.Title).Name("title").Index(3);
        Map(m => m.Type).Name("type").Index(4).TypeConverter<ClassEnumConverter<AcademicSessionTypeEnum>>();
        Map(m => m.StartDate).Name("startDate").Index(5).TypeConverter<DateOnlyConverter>();
        Map(m => m.EndDate).Name("endDate").Index(6).TypeConverter<DateOnlyConverter>();
        Map(m => m.ParentSourcedId).Name("parentSourcedId").Optional().Index(7);
        Map(m => m.SchoolYear).Name("schoolYear").Index(8);
    }
}
