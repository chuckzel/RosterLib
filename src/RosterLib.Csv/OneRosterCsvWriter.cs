using System.Globalization;
using System.IO.Compression;
using CsvHelper.Configuration;
using RosterLib.Domain;

namespace RosterLib.Csv;

public sealed class OneRosterCsvWriter
{
    private readonly CsvConfiguration _csvConfig;

    public OneRosterCsvWriter(CsvConfiguration? csvConfiguration = null)
    {
        _csvConfig = csvConfiguration ?? CreateDefaultCsvConfiguration();
    }

    public async Task WriteZipAsync(string zipPath, OneRosterSnapshot snapshot)
    {
        using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
        await WriteZipAsync(archive, snapshot);
    }

    public async Task WriteZipAsync(Stream zipStream, OneRosterSnapshot snapshot)
    {
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true);
        await WriteZipAsync(archive, snapshot);
    }

    public async Task WriteZipAsync(ZipArchive archive, OneRosterSnapshot snapshot, string? sourceSystemName = null, string? sourceSystemCode = null)
    {
        var manifest = CreateManifest(snapshot, sourceSystemName, sourceSystemCode);

        foreach (var definition in OneRosterFileDefinitions.All)
        {
            if (manifest.Files.TryGetValue(definition.FileName, out var mode) && mode != Manifest.FileMode.Absent)
                await definition.WriteFromSnapshotAsync(archive, _csvConfig, snapshot);
        }

        await CsvZipSerialization.WriteEntryAsync(archive, "manifest.csv", manifest.ToRows(), _csvConfig, new ManifestRowMap());
    }

    private static Manifest CreateManifest(OneRosterSnapshot snapshot, string? sourceSystemName, string? sourceSystemCode)
    {
        var manifest = new Manifest
        {
            SourceSystemName = sourceSystemName,
            SourceSystemCode = sourceSystemCode
        };

        foreach (var definition in OneRosterFileDefinitions.All)
        {
            manifest.Files[definition.FileName] = definition.HasData(snapshot)
                ? Manifest.FileMode.Bulk
                : Manifest.FileMode.Absent;
        }

        return manifest;
    }

    private static CsvConfiguration CreateDefaultCsvConfiguration()
    {
        return new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            MissingFieldFound = null,
            HeaderValidated = null
        };
    }
}
