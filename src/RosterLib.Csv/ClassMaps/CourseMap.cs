using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for Course entity.
/// Maps to courses.csv in OneRoster format.
/// </summary>
public sealed class CourseMap : BaseMap<Course>
{
    public CourseMap()
    {
        Map(m => m.Title).Name("title");
        Map(m => m.SchoolYearSourcedId).Name("schoolYearSourcedId");
        Map(m => m.CourseCode).Name("courseCode");
        Map(m => m.Grades).Name("grades").TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.OrgSourcedId).Name("orgSourcedId");
        Map(m => m.Subjects).Name("subjects").TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.SubjectCodes).Name("subjectCodes").TypeConverter<CommaSeparatedStringConverter>();
    }
}
