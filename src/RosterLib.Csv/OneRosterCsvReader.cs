using System.Globalization;
using System.IO.Compression;
using CsvHelper.Configuration;
using RosterLib.Domain;

namespace RosterLib.Csv;

public sealed class OneRosterCsvReader
{
    private readonly CsvConfiguration _csvConfig;

    public OneRosterCsvReader(CsvConfiguration? csvConfiguration = null)
    {
        _csvConfig = csvConfiguration ?? CreateDefaultCsvConfiguration();
    }

    public async Task<OneRosterSnapshot> ReadZipAsync(string zipPath)
    {
        using var archive = ZipFile.OpenRead(zipPath);
        return await ReadZipAsync(archive);
    }

    public async Task<OneRosterSnapshot> ReadZipAsync(Stream zipStream)
    {
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read, leaveOpen: true);
        return await ReadZipAsync(archive);
    }

    public async Task<OneRosterSnapshot> ReadZipAsync(ZipArchive archive)
    {
        var snapshot = new OneRosterSnapshot();
        var manifest = await TryReadManifestAsync(archive);

        if (manifest is null)
        {
            foreach (var definition in OneRosterFileDefinitions.All)
                await definition.ReadIntoSnapshotAsync(archive, _csvConfig, snapshot);

            return snapshot;
        }

        foreach (var file in manifest.Files)
        {
            if (file.Value == Manifest.FileMode.Absent)
                continue;

            if (!OneRosterFileDefinitions.TryGet(file.Key, out var definition) || definition is null)
                throw new InvalidDataException($"Unknown file in manifest: {file.Key}");

            await definition.ReadIntoSnapshotAsync(archive, _csvConfig, snapshot);
        }

        return snapshot;
    }

    private async Task<Manifest?> TryReadManifestAsync(ZipArchive archive)
    {
        var rows = await CsvZipSerialization.ReadEntryAsync<ManifestRow>(archive, "manifest.csv", _csvConfig, new ManifestRowMap());

        if (rows is null)
            return null;

        return Manifest.FromRows(rows);
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
