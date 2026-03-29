using System.IO.Compression;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace RosterLib.Csv;

internal static class CsvZipSerialization
{
    public static async Task<List<TRecord>?> ReadEntryAsync<TRecord>(ZipArchive archive, string entryName, CsvConfiguration csvConfig, params ClassMap[] classMaps)
    {
        var entry = archive.GetEntry(entryName);
        if (entry is null)
            return null;

        using var stream = entry.Open();
        return await ReadCsvAsync<TRecord>(stream, csvConfig, classMaps, leaveOpen: false);
    }

    public static async Task WriteEntryAsync<TRecord>(ZipArchive archive, string entryName, IEnumerable<TRecord> records, CsvConfiguration csvConfig, params ClassMap[] classMaps)
    {
        var entry = archive.CreateEntry(entryName);
        using var stream = entry.Open();
        await WriteCsvAsync(stream, records, csvConfig, classMaps, leaveOpen: false);
    }

    public static async Task<List<TRecord>> ReadCsvAsync<TRecord>(Stream stream, CsvConfiguration csvConfig, ClassMap[] classMaps, bool leaveOpen)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: leaveOpen);
        using var csv = new CsvReader(reader, csvConfig);

        foreach (var classMap in classMaps)
        {
            csv.Context.RegisterClassMap(classMap);
        }

        var records = new List<TRecord>();
        await foreach (var record in csv.GetRecordsAsync<TRecord>())
        {
            records.Add(record);
        }

        return records;
    }

    public static async Task WriteCsvAsync<TRecord>(Stream stream, IEnumerable<TRecord> records, CsvConfiguration csvConfig, ClassMap[] classMaps, bool leaveOpen)
    {
        using var writer = new StreamWriter(stream, Encoding.UTF8, bufferSize: 1024, leaveOpen: leaveOpen);
        using var csv = new CsvWriter(writer, csvConfig);

        foreach (var classMap in classMaps)
        {
            csv.Context.RegisterClassMap(classMap);
        }

        await csv.WriteRecordsAsync(records);
        await writer.FlushAsync();
    }
}
