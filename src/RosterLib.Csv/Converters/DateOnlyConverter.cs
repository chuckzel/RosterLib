using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace RosterLib.Csv.Converters;

/// <summary>
/// Converts between DateOnly and ISO 8601 date format (yyyy-MM-dd).
/// </summary>
public class DateOnlyConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        if (DateOnly.TryParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return date;

        return null;
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is not DateOnly date)
            return null;

        return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}
