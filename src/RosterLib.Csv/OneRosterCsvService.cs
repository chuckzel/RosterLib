using CsvHelper;
using CsvHelper.Configuration;
using RosterLib.Domain;
using RosterLib.Domain.Common;
using System.Globalization;

namespace RosterLib.Csv;

/// <summary>
/// Service for reading and writing OneRoster CSV files.
/// </summary>
public class OneRosterCsvService
{
    private readonly CsvConfiguration _csvConfig;
    private readonly OneRosterCsvReader _zipReader;
    private readonly OneRosterCsvWriter _zipWriter;

    public OneRosterCsvService()
    {
        _csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            MissingFieldFound = null, // Ignore missing fields
            HeaderValidated = null,   // Don't validate headers strictly
        };

        _zipReader = new OneRosterCsvReader(_csvConfig);
        _zipWriter = new OneRosterCsvWriter(_csvConfig);
    }

    /// <summary>
    /// Reads records from a CSV file.
    /// </summary>
    public async Task<List<T>> ReadCsvAsync<T, TMap>(string filePath) 
        where T : Base
        where TMap : ClassMap<T>
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, _csvConfig);
        
        csv.Context.RegisterClassMap<TMap>();
        
        var records = new List<T>();
        await foreach (var record in csv.GetRecordsAsync<T>())
        {
            records.Add(record);
        }
        
        return records;
    }

    /// <summary>
    /// Reads records from a CSV stream.
    /// </summary>
    public async Task<List<T>> ReadCsvAsync<T, TMap>(Stream stream) 
        where T : Base
        where TMap : ClassMap<T>
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, _csvConfig);
        
        csv.Context.RegisterClassMap<TMap>();
        
        var records = new List<T>();
        await foreach (var record in csv.GetRecordsAsync<T>())
        {
            records.Add(record);
        }
        
        return records;
    }

    /// <summary>
    /// Writes records to a CSV file.
    /// </summary>
    public async Task WriteCsvAsync<T, TMap>(string filePath, IEnumerable<T> records) 
        where T : Base
        where TMap : ClassMap<T>
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, _csvConfig);
        
        csv.Context.RegisterClassMap<TMap>();
        
        await csv.WriteRecordsAsync(records);
    }

    /// <summary>
    /// Writes records to a CSV stream.
    /// </summary>
    public async Task WriteCsvAsync<T, TMap>(Stream stream, IEnumerable<T> records) 
        where T : Base
        where TMap : ClassMap<T>
    {
        using var writer = new StreamWriter(stream, leaveOpen: true);
        using var csv = new CsvWriter(writer, _csvConfig);
        
        csv.Context.RegisterClassMap<TMap>();
        
        await csv.WriteRecordsAsync(records);
        await writer.FlushAsync();
    }

    /// <summary>
    /// Reads all OneRoster CSV files from a zip archive.
    /// </summary>
    public async Task<OneRosterSnapshot> ReadZipAsync(string zipPath)
    {
        return await _zipReader.ReadZipAsync(zipPath);
    }

    /// <summary>
    /// Writes all OneRoster data to a zip archive.
    /// </summary>
    public async Task WriteZipAsync(string zipPath, OneRosterSnapshot snapshot)
    {
        await _zipWriter.WriteZipAsync(zipPath, snapshot);
    }

    /// <summary>
    /// Reads all OneRoster CSV files from a zip stream.
    /// </summary>
    public async Task<OneRosterSnapshot> ReadZipAsync(Stream zipStream)
    {
        return await _zipReader.ReadZipAsync(zipStream);
    }

    /// <summary>
    /// Writes all OneRoster data to a zip stream.
    /// </summary>
    public async Task WriteZipAsync(Stream zipStream, OneRosterSnapshot snapshot)
    {
        await _zipWriter.WriteZipAsync(zipStream, snapshot);
    }
}
