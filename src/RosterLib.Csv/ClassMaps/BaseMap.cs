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
        Map(m => m.SourcedId).Name("sourcedId");
        Map(m => m.Status).Name("status").TypeConverter<ClassEnumConverter<StatusEnum>>();
        Map(m => m.DateLastModified).Name("dateLastModified").TypeConverter<DateTimeConverter>();
        Map(m => m.Metadata).Name("metadata").TypeConverter<MetadataConverter>().Ignore();
    }
}
