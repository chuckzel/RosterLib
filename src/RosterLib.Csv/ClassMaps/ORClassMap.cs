using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for Class entity.
/// Maps to classes.csv in OneRoster format.
/// </summary>
public sealed class ORClassMap : BaseMap<Class>
{
    public ORClassMap()
    {
        Map(m => m.Title).Name("title");
        Map(m => m.Grades).Name("grades").TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.CourseSourcedId).Name("courseSourcedId");
        Map(m => m.ClassCode).Name("classCode");
        Map(m => m.ClassType).Name("classType").TypeConverter<ClassEnumConverter<ClassTypeEnum>>();
        Map(m => m.Location).Name("location");
        Map(m => m.SchoolSourcedId).Name("schoolSourcedId");
        Map(m => m.TermSourcedIds).Name("termSourcedIds").TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.Subjects).Name("subjects").TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.SubjectCodes).Name("subjectCodes").TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.Periods).Name("periods").TypeConverter<CommaSeparatedStringConverter>();
    }
}
