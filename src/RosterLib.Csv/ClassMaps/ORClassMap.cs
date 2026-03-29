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
        Map(m => m.Title).Name("title").Index(3);
        Map(m => m.Grades).Name("grades").Index(4).Optional().TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.CourseSourcedId).Name("courseSourcedId").Index(5);
        Map(m => m.ClassCode).Name("classCode").Index(6).Optional();
        Map(m => m.ClassType).Name("classType").Index(7).TypeConverter<ClassEnumConverter<ClassTypeEnum>>();
        Map(m => m.Location).Name("location").Index(8).Optional();
        Map(m => m.SchoolSourcedId).Name("schoolSourcedId").Index(9);
        Map(m => m.TermSourcedIds).Name("termSourcedIds").Index(10).TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.Subjects).Name("subjects").Index(11).Optional().TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.SubjectCodes).Name("subjectCodes").Index(12).Optional().TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.Periods).Name("periods").Index(13).Optional().TypeConverter<CommaSeparatedStringConverter>();
    }
}
