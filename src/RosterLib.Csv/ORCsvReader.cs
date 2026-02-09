using System.IO.Compression;
using CsvHelper;
using CsvHelper.Configuration;

namespace RosterLib.Csv;

public static class FileDefinitions
{
    public class FileDefinition<T>
    {
        public required string FileName { get; init; }
        public required IEnumerable<ClassMap> MapsToRegister { get; init; }
        public required Func<OneRosterSnapshot, List<T>> GetData { get; init; }

    }
}

public class Manifest
{
    public string ManifestVersion { get; private set; } = "1.0";
    public string OneRosterVersion { get; private set; } = "1.2";
    public string? SourceSystemName { get; private set; }
    public string? SourceSystemCode { get; private set; }
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
                case var _ when row.PropertyName.StartsWith("file."):
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

        if (!string.IsNullOrEmpty(SourceSystemName))
        {
            rows.Add(new ManifestRow { PropertyName = "source.systemName", Value = SourceSystemName });
        }

        if (!string.IsNullOrEmpty(SourceSystemCode))
        {
            rows.Add(new ManifestRow { PropertyName = "source.systemCode", Value = SourceSystemCode });
        }

        foreach (var file in Files)
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

public class ManifestRow
{
    public required string PropertyName { get; set; }
    public required string Value { get; set; }
}

public class ManifestRowMap : ClassMap<ManifestRow>
{
    public ManifestRowMap()
    {
        Map(m => m.PropertyName).Name("propertyName");
        Map(m => m.Value).Name("value");
    }
}

public class ORCsvReader
{
    public async Task<OneRosterSnapshot> ReadZipAsync(ZipArchive archive)
    {
        throw new NotImplementedException();
    }


    private async Task<List<T>?> ReadEntryAsync<T>(ZipArchive archive, string entryName, IEnumerable<ClassMap> mapsToRegister)
    {
        var entry = archive.GetEntry(entryName);
        if (entry == null)
            return null;

        using var entryStream = entry.Open();
        return await ReadCsvAsync<T>(entryStream, mapsToRegister);
    }

    private async Task<List<T>?> ReadCsvAsync<T>(Stream entryStream, IEnumerable<ClassMap> mapsToRegister)
    {
        using var reader = new StreamReader(entryStream);
        using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

        foreach (var map in mapsToRegister)
        {
            csv.Context.RegisterClassMap(map);
        }

        var records = csv.GetRecordsAsync<T>();
        return await records.ToListAsync();
    }
}
