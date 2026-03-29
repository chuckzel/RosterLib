using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using RosterLib.Domain.Rostering;

namespace RosterLib.Csv.Converters;

/// <summary>
/// Converts between List&lt;UserId&gt; and comma-delimited CSV field values.
/// OneRoster CSV format represents user IDs as pipe-delimited type:identifier pairs.
/// Format: {Type:Identifier} or {LDAP:Id},{LTI:Id},{Fed:Id}
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/spec/oneroster/v1p2/bind/csv/">OneRoster CSV Binding specification</see>
/// </remarks>
public class UserIdListConverter : DefaultTypeConverter
{
    /// <summary>
    /// Parses a comma-separated list of {Type:Identifier} strings into UserId objects.
    /// </summary>
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var userIds = new List<UserId>();

        // Split by comma to get individual {Type:Identifier} entries
        var entries = text.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var entry in entries)
        {
            var trimmed = entry.Trim();
            
            // Remove outer braces: {Type:Identifier} -> Type:Identifier
            if (trimmed.StartsWith('{') && trimmed.EndsWith('}'))
            {
                trimmed = trimmed[1..^1];
            }

            // Split by colon to get Type and Identifier
            var parts = trimmed.Split(':', StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length == 2)
            {
                userIds.Add(new UserId
                {
                    Type = parts[0].Trim(),
                    Identifier = parts[1].Trim()
                });
            }
        }

        return userIds.Count > 0 ? userIds : null;
    }

    /// <summary>
    /// Converts a List&lt;UserId&gt; to a comma-separated string of {Type:Identifier} entries.
    /// </summary>
    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is not List<UserId> userIds || userIds.Count == 0)
            return null;

        var formatted = userIds
            .Select(uid => $"{{{uid.Type}:{uid.Identifier}}}")
            .ToList();

        // CsvHelper will automatically add quotes if needed
        return string.Join(",", formatted);
    }
}
