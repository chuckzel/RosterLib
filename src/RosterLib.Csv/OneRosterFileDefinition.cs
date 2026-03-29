using System.IO.Compression;
using CsvHelper.Configuration;
using RosterLib.Csv.ClassMaps;
using RosterLib.Domain.Rostering;

namespace RosterLib.Csv;

internal interface IOneRosterFileDefinition
{
    string FileName { get; }
    bool HasData(OneRosterSnapshot snapshot);
    Task ReadIntoSnapshotAsync(ZipArchive archive, CsvConfiguration csvConfig, OneRosterSnapshot snapshot);
    Task WriteFromSnapshotAsync(ZipArchive archive, CsvConfiguration csvConfig, OneRosterSnapshot snapshot);
}

internal sealed class OneRosterFileDefinition<TRecord> : IOneRosterFileDefinition
{
    private readonly Func<OneRosterSnapshot, List<TRecord>> _recordsAccessor;
    private readonly ClassMap[] _classMaps;

    public OneRosterFileDefinition(string fileName, Func<OneRosterSnapshot, List<TRecord>> recordsAccessor, params ClassMap[] classMaps)
    {
        FileName = fileName;
        _recordsAccessor = recordsAccessor;
        _classMaps = classMaps.Length > 0 ? classMaps : throw new ArgumentException("At least one ClassMap must be provided", nameof(classMaps));
    }

    public string FileName { get; }

    public bool HasData(OneRosterSnapshot snapshot) => _recordsAccessor(snapshot).Count > 0;

    public async Task ReadIntoSnapshotAsync(ZipArchive archive, CsvConfiguration csvConfig, OneRosterSnapshot snapshot)
    {
        var records = await CsvZipSerialization.ReadEntryAsync<TRecord>(archive, FileName, csvConfig, _classMaps);
        if (records is null)
            return;

        _recordsAccessor(snapshot).AddRange(records);
    }

    public Task WriteFromSnapshotAsync(ZipArchive archive, CsvConfiguration csvConfig, OneRosterSnapshot snapshot)
    {
        var records = _recordsAccessor(snapshot);
        if (records.Count == 0)
            return Task.CompletedTask;

        return CsvZipSerialization.WriteEntryAsync(archive, FileName, records, csvConfig, _classMaps);
    }
}

internal static class OneRosterFileDefinitions
{
    private static readonly IReadOnlyList<IOneRosterFileDefinition> _all =
    [
        new OneRosterFileDefinition<AcademicSession>("academicSessions.csv", s => s.AcademicSessions, new AcademicSessionMap()),
        new OneRosterFileDefinition<Class>("classes.csv", s => s.Classes, new ORClassMap()),
        new OneRosterFileDefinition<Course>("courses.csv", s => s.Courses, new CourseMap()),
        new OneRosterFileDefinition<Demographics>("demographics.csv", s => s.Demographics, new DemographicsMap()),
        new OneRosterFileDefinition<Enrollment>("enrollments.csv", s => s.Enrollments, new EnrollmentMap()),
        new OneRosterFileDefinition<Org>("orgs.csv", s => s.Orgs, new OrgMap()),
        new OneRosterFileDefinition<Role>("roles.csv", s => s.Roles, new RoleMap()),
        new OneRosterFileDefinition<User>("users.csv", s => s.Users, new UserMap()),
        new OneRosterFileDefinition<UserProfile>("userProfiles.csv", s => s.UserProfiles, new UserProfileMap())
    ];

    private static readonly IReadOnlyDictionary<string, IOneRosterFileDefinition> _byFileName =
        _all.ToDictionary(f => f.FileName, StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyList<IOneRosterFileDefinition> All => _all;

    public static bool TryGet(string key, out IOneRosterFileDefinition? definition)
    {
        if (_byFileName.TryGetValue(key, out definition))
            return true;

        if (!key.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            return _byFileName.TryGetValue($"{key}.csv", out definition);

        return false;
    }
}
