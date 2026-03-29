using CsvHelper.Configuration;

namespace RosterLib.Csv;

public class Manifest
{
    public string ManifestVersion { get; private set; } = "1.0";
    public string OneRosterVersion { get; private set; } = "1.2";
    public string? SourceSystemName { get; set; }
    public string? SourceSystemCode { get; set; }
    public Dictionary<string, FileMode> Files { get; init; } = [];

    public static Manifest FromRows(IEnumerable<ManifestRow> rows)
    {
        var manifest = new Manifest();

        foreach (var row in rows)
        {
            switch (row.PropertyName)
            {
                case "manifest.version":
                    if (row.Value != "1.0")
                        throw new InvalidDataException($"Unsupported manifest version: {row.Value}");
                    break;
                case "oneRoster.version":
                    if (row.Value != "1.2")
                        throw new InvalidDataException($"Unsupported OneRoster version: {row.Value}");
                    break;
                case "source.systemName":
                    manifest.SourceSystemName = row.Value;
                    break;
                case "source.systemCode":
                    manifest.SourceSystemCode = row.Value;
                    break;
                case var _ when row.PropertyName.StartsWith("file.", StringComparison.Ordinal):
                    var fileName = row.PropertyName[5..];
                    if (manifest.Files.ContainsKey(fileName))
                        throw new InvalidDataException($"Duplicate file entry in manifest: {fileName}");

                    manifest.Files[fileName] = row.Value switch
                    {
                        "absent" => FileMode.Absent,
                        "bulk" => FileMode.Bulk,
                        "delta" => FileMode.Delta,
                        _ => throw new InvalidDataException($"Invalid file mode for {fileName}: {row.Value}")
                    };
                    break;
                default:
                    throw new InvalidDataException($"Unknown manifest property: {row.PropertyName}");
            }
        }

        return manifest;
    }

    public List<ManifestRow> ToRows()
    {
        var rows = new List<ManifestRow>
        {
            new() { PropertyName = "manifest.version", Value = ManifestVersion },
            new() { PropertyName = "oneRoster.version", Value = OneRosterVersion }
        };

        if (!string.IsNullOrWhiteSpace(SourceSystemName))
            rows.Add(new ManifestRow { PropertyName = "source.systemName", Value = SourceSystemName });

        if (!string.IsNullOrWhiteSpace(SourceSystemCode))
            rows.Add(new ManifestRow { PropertyName = "source.systemCode", Value = SourceSystemCode });

        foreach (var file in Files.OrderBy(f => f.Key, StringComparer.OrdinalIgnoreCase))
        {
            var modeValue = file.Value switch
            {
                FileMode.Absent => "absent",
                FileMode.Bulk => "bulk",
                FileMode.Delta => "delta",
                _ => throw new InvalidDataException($"Invalid file mode for {file.Key}: {file.Value}")
            };
            rows.Add(new ManifestRow { PropertyName = $"file.{file.Key}", Value = modeValue });
        }

        return rows;
    }

    public enum FileMode
    {
        Absent,
        Bulk,
        Delta
    }
}

public sealed class ManifestRow
{
    public required string PropertyName { get; set; }
    public required string Value { get; set; }
}

public sealed class ManifestRowMap : ClassMap<ManifestRow>
{
    public ManifestRowMap()
    {
        Map(m => m.PropertyName).Name("propertyName").Index(0);
        Map(m => m.Value).Name("value").Index(1);
    }
}
