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
        Map(m => m.SchoolYearSourcedId).Name("schoolYearSourcedId").Index(3).Optional();
        Map(m => m.Title).Name("title").Index(4);
        Map(m => m.CourseCode).Name("courseCode").Index(5).Optional();
        Map(m => m.Grades).Name("grades").Index(6).Optional().TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.OrgSourcedId).Name("orgSourcedId").Index(7);
        Map(m => m.Subjects).Name("subjects").Index(8).Optional().TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.SubjectCodes).Name("subjectCodes").Index(9).Optional().TypeConverter<CommaSeparatedStringConverter>();
    }
}
