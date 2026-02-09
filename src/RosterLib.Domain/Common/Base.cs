namespace RosterLib.Domain.Common;

/// <summary>
/// Base class for all OneRoster data objects.
/// Contains common properties: sourcedId, status, dateLastModified, and metadata.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/spec/oneroster/v1p2">OneRoster v1.2 specification</see>
/// </remarks>
public class Base
{
    public required string SourcedId { get; set; } // GUID
    public StatusEnum? Status { get; set; } // "active" or "tobedeleted" (delta only)
    public DateTime? DateLastModified { get; set; }
    public Dictionary<string, string>? Metadata { get; set; } // Extensions
}
