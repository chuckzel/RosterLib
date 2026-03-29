using CsvHelper.Configuration;
using RosterLib.Domain.Common;
using RosterLib.Csv.Converters;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// Base mapping for all OneRoster entities that extend Base.
/// Maps common fields: sourcedId, status, dateLastModified, metadata.
/// </summary>
public abstract class BaseMap<T> : ClassMap<T> where T : Base
{
    protected BaseMap()
    {
        Map(m => m.SourcedId).Name("sourcedId").Index(0);
        Map(m => m.Status).Name("status").Index(1).TypeConverter<ClassEnumConverter<StatusEnum>>();
        Map(m => m.DateLastModified).Name("dateLastModified").Index(2).TypeConverter<DateTimeConverter>();
        Map(m => m.Metadata).Name("metadata").TypeConverter<MetadataConverter>().Ignore();
    }
}
