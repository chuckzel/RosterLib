using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Text.Json;

namespace RosterLib.Csv.Converters;

/// <summary>
/// Converts between Dictionary&lt;string, string&gt; and JSON string for metadata/extensions fields.
/// </summary>
public class MetadataConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(text);
        }
        catch
        {
            return null;
        }
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is not Dictionary<string, string> dict || dict.Count == 0)
            return null;

        return JsonSerializer.Serialize(dict);
    }
}
