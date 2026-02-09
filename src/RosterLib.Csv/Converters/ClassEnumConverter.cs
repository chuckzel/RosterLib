using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using RosterLib.Domain.Common;
using System.Reflection;

namespace RosterLib.Csv.Converters;

/// <summary>
/// Generic converter for ClassEnum&lt;T&gt; types.
/// Converts between ClassEnum instances and their string values.
/// Prefers predefined static members when available, and creates new instances for extensibility.
/// </summary>
public class ClassEnumConverter<TEnum> : DefaultTypeConverter where TEnum : ClassEnum<string>
{
    
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        // todo: rewrite enums to fix this
        var constructor = typeof(TEnum).GetConstructor([typeof(string)]);
        if (constructor != null)
            return constructor.Invoke([text]);

        throw new TypeConverterException(this, memberMapData, text, row.Context, $"Cannot convert '{text}' to {typeof(TEnum).Name}");
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        return (value as TEnum)?.Value;
    }
}
