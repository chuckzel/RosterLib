using CsvHelper;
using CsvHelper.Configuration;
using RosterLib.Csv.ClassMaps;
using RosterLib.Domain.Common;
using RosterLib.Domain.Rostering;
using System.Globalization;
using System.IO.Compression;

namespace RosterLib.Csv;

/// <summary>
/// Represents a complete OneRoster dataset snapshot.
/// </summary>
public class OneRosterSnapshot
{
    public List<AcademicSession> AcademicSessions { get; set; } = [];
    public List<Class> Classes { get; set; } = [];
    public List<Course> Courses { get; set; } = [];
    public List<Demographics> Demographics { get; set; } = [];
    public List<Enrollment> Enrollments { get; set; } = [];
    public List<Org> Orgs { get; set; } = [];
    public List<Role> Roles { get; set; } = [];
    public List<User> Users { get; set; } = [];
    public List<UserProfile> UserProfiles { get; set; } = [];
}

/// <summary>
/// Service for reading and writing OneRoster CSV files.
/// </summary>
public class OneRosterCsvService
{
    private readonly CsvConfiguration _csvConfig;

    public OneRosterCsvService()
    {
        _csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            MissingFieldFound = null, // Ignore missing fields
            HeaderValidated = null    // Don't validate headers strictly
        };
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
        var snapshot = new OneRosterSnapshot();

        using var zip = ZipFile.OpenRead(zipPath);
        
        if (zip.GetEntry("academicSessions.csv") is { } academicSessionsEntry)
            snapshot.AcademicSessions = await ReadFromZipEntryAsync<AcademicSession, AcademicSessionMap>(zip, "academicSessions.csv");
        
        if (zip.GetEntry("classes.csv") is { } classesEntry)
            snapshot.Classes = await ReadFromZipEntryAsync<Class, ORClassMap>(zip, "classes.csv");
        
        if (zip.GetEntry("courses.csv") is { } coursesEntry)
            snapshot.Courses = await ReadFromZipEntryAsync<Course, CourseMap>(zip, "courses.csv");
        
        if (zip.GetEntry("demographics.csv") is { } demographicsEntry)
            snapshot.Demographics = await ReadFromZipEntryAsync<Demographics, DemographicsMap>(zip, "demographics.csv");
        
        if (zip.GetEntry("enrollments.csv") is { } enrollmentsEntry)
            snapshot.Enrollments = await ReadFromZipEntryAsync<Enrollment, EnrollmentMap>(zip, "enrollments.csv");
        
        if (zip.GetEntry("orgs.csv") is { } orgsEntry)
            snapshot.Orgs = await ReadFromZipEntryAsync<Org, OrgMap>(zip, "orgs.csv");
        
        if (zip.GetEntry("roles.csv") is { } rolesEntry)
            snapshot.Roles = await ReadFromZipEntryAsync<Role, RoleMap>(zip, "roles.csv");
        
        if (zip.GetEntry("users.csv") is { } usersEntry)
            snapshot.Users = await ReadFromZipEntryAsync<User, UserMap>(zip, "users.csv");
        
        if (zip.GetEntry("userProfiles.csv") is { } userProfilesEntry)
            snapshot.UserProfiles = await ReadFromZipEntryAsync<UserProfile, UserProfileMap>(zip, "userProfiles.csv");

        return snapshot;
    }

    /// <summary>
    /// Writes all OneRoster data to a zip archive.
    /// </summary>
    public async Task WriteZipAsync(string zipPath, OneRosterSnapshot snapshot)
    {
        using var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create);

        if (snapshot.AcademicSessions.Count > 0)
            await WriteToZipEntryAsync<AcademicSession, AcademicSessionMap>(zip, "academicSessions.csv", snapshot.AcademicSessions);
        
        if (snapshot.Classes.Count > 0)
            await WriteToZipEntryAsync<Class, ORClassMap>(zip, "classes.csv", snapshot.Classes);
        
        if (snapshot.Courses.Count > 0)
            await WriteToZipEntryAsync<Course, CourseMap>(zip, "courses.csv", snapshot.Courses);
        
        if (snapshot.Demographics.Count > 0)
            await WriteToZipEntryAsync<Demographics, DemographicsMap>(zip, "demographics.csv", snapshot.Demographics);
        
        if (snapshot.Enrollments.Count > 0)
            await WriteToZipEntryAsync<Enrollment, EnrollmentMap>(zip, "enrollments.csv", snapshot.Enrollments);
        
        if (snapshot.Orgs.Count > 0)
            await WriteToZipEntryAsync<Org, OrgMap>(zip, "orgs.csv", snapshot.Orgs);
        
        if (snapshot.Roles.Count > 0)
            await WriteToZipEntryAsync<Role, RoleMap>(zip, "roles.csv", snapshot.Roles);
        
        if (snapshot.Users.Count > 0)
            await WriteToZipEntryAsync<User, UserMap>(zip, "users.csv", snapshot.Users);
        
        if (snapshot.UserProfiles.Count > 0)
            await WriteToZipEntryAsync<UserProfile, UserProfileMap>(zip, "userProfiles.csv", snapshot.UserProfiles);
    }

    /// <summary>
    /// Reads all OneRoster CSV files from a zip stream.
    /// </summary>
    public async Task<OneRosterSnapshot> ReadZipAsync(Stream zipStream)
    {
        var snapshot = new OneRosterSnapshot();

        using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read, leaveOpen: true);
        
        if (zip.GetEntry("academicSessions.csv") is { })
            snapshot.AcademicSessions = await ReadFromZipEntryAsync<AcademicSession, AcademicSessionMap>(zip, "academicSessions.csv");
        
        if (zip.GetEntry("classes.csv") is { })
            snapshot.Classes = await ReadFromZipEntryAsync<Class, ORClassMap>(zip, "classes.csv");
        
        if (zip.GetEntry("courses.csv") is { })
            snapshot.Courses = await ReadFromZipEntryAsync<Course, CourseMap>(zip, "courses.csv");
        
        if (zip.GetEntry("demographics.csv") is { })
            snapshot.Demographics = await ReadFromZipEntryAsync<Demographics, DemographicsMap>(zip, "demographics.csv");
        
        if (zip.GetEntry("enrollments.csv") is { })
            snapshot.Enrollments = await ReadFromZipEntryAsync<Enrollment, EnrollmentMap>(zip, "enrollments.csv");
        
        if (zip.GetEntry("orgs.csv") is { })
            snapshot.Orgs = await ReadFromZipEntryAsync<Org, OrgMap>(zip, "orgs.csv");
        
        if (zip.GetEntry("roles.csv") is { })
            snapshot.Roles = await ReadFromZipEntryAsync<Role, RoleMap>(zip, "roles.csv");
        
        if (zip.GetEntry("users.csv") is { })
            snapshot.Users = await ReadFromZipEntryAsync<User, UserMap>(zip, "users.csv");
        
        if (zip.GetEntry("userProfiles.csv") is { })
            snapshot.UserProfiles = await ReadFromZipEntryAsync<UserProfile, UserProfileMap>(zip, "userProfiles.csv");

        return snapshot;
    }

    /// <summary>
    /// Writes all OneRoster data to a zip stream.
    /// </summary>
    public async Task WriteZipAsync(Stream zipStream, OneRosterSnapshot snapshot)
    {
        using var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true);

        if (snapshot.AcademicSessions.Count > 0)
            await WriteToZipEntryAsync<AcademicSession, AcademicSessionMap>(zip, "academicSessions.csv", snapshot.AcademicSessions);
        
        if (snapshot.Classes.Count > 0)
            await WriteToZipEntryAsync<Class, ORClassMap>(zip, "classes.csv", snapshot.Classes);
        
        if (snapshot.Courses.Count > 0)
            await WriteToZipEntryAsync<Course, CourseMap>(zip, "courses.csv", snapshot.Courses);
        
        if (snapshot.Demographics.Count > 0)
            await WriteToZipEntryAsync<Demographics, DemographicsMap>(zip, "demographics.csv", snapshot.Demographics);
        
        if (snapshot.Enrollments.Count > 0)
            await WriteToZipEntryAsync<Enrollment, EnrollmentMap>(zip, "enrollments.csv", snapshot.Enrollments);
        
        if (snapshot.Orgs.Count > 0)
            await WriteToZipEntryAsync<Org, OrgMap>(zip, "orgs.csv", snapshot.Orgs);
        
        if (snapshot.Roles.Count > 0)
            await WriteToZipEntryAsync<Role, RoleMap>(zip, "roles.csv", snapshot.Roles);
        
        if (snapshot.Users.Count > 0)
            await WriteToZipEntryAsync<User, UserMap>(zip, "users.csv", snapshot.Users);
        
        if (snapshot.UserProfiles.Count > 0)
            await WriteToZipEntryAsync<UserProfile, UserProfileMap>(zip, "userProfiles.csv", snapshot.UserProfiles);
    }

    // Helper methods for zip operations
    private async Task<List<T>> ReadFromZipEntryAsync<T, TMap>(ZipArchive zip, string entryName)
        where T : Base
        where TMap : ClassMap<T>
    {
        var entry = zip.GetEntry(entryName);
        if (entry == null)
            return [];

        using var entryStream = entry.Open();
        using var reader = new StreamReader(entryStream);
        using var csv = new CsvReader(reader, _csvConfig);
        
        csv.Context.RegisterClassMap<TMap>();
        
        var records = new List<T>();
        await foreach (var record in csv.GetRecordsAsync<T>())
        {
            records.Add(record);
        }
        
        return records;
    }

    private async Task WriteToZipEntryAsync<T, TMap>(ZipArchive zip, string entryName, IEnumerable<T> records)
        where T : Base
        where TMap : ClassMap<T>
    {
        var entry = zip.CreateEntry(entryName);
        using var entryStream = entry.Open();
        using var writer = new StreamWriter(entryStream);
        using var csv = new CsvWriter(writer, _csvConfig);
        
        csv.Context.RegisterClassMap<TMap>();
        
        await csv.WriteRecordsAsync(records);
        await writer.FlushAsync();
    }
}
