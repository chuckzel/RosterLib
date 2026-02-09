using RosterLib.Domain.Common;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents an organization from the OneRoster Rostering specification.
/// Organizations can represent departments, schools, districts, or other institutional entities.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_Org">OneRoster Org specification</see>
/// </remarks>
public class Org : Base
{
    public required string Name { get; set; } // Name of the organization
    public required OrgTypeEnum Type { get; set; } // department, district, local, national, school, state
    public string? Identifier { get; set; } // Human readable identifier (e.g. NCES ID)
    public string? ParentSourcedId { get; set; } // Parent org sourcedId
}
