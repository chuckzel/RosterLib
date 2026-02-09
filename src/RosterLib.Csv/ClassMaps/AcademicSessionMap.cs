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
        Map(m => m.Title).Name("title");
        Map(m => m.Type).Name("type").TypeConverter<ClassEnumConverter<AcademicSessionTypeEnum>>();
        Map(m => m.StartDate).Name("startDate").TypeConverter<DateOnlyConverter>();
        Map(m => m.EndDate).Name("endDate").TypeConverter<DateOnlyConverter>();
        Map(m => m.ParentSourcedId).Name("parentSourcedId");
        Map(m => m.SchoolYear).Name("schoolYear");
    }
}
