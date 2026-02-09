using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace RosterLib.Csv.Converters;

/// <summary>
/// Converts between List&lt;string&gt; and comma-delimited CSV field values per RFC4180.
/// OneRoster CSV format uses comma separation for multi-value fields, enclosed in quotes.
/// Examples: "1,2,3" or "{LDAP:Id},{LTI:Id}"
/// </summary>
public class CommaSeparatedStringConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        return text.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToList();
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is not List<string> list || list.Count == 0)
            return null;

        // CsvHelper will automatically add quotes if needed
        return string.Join(",", list);
    }
}
