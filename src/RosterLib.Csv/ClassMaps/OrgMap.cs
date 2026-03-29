using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for Org entity.
/// Maps to orgs.csv in OneRoster format.
/// </summary>
public sealed class OrgMap : BaseMap<Org>
{
    public OrgMap()
    {
        Map(m => m.Name).Name("name").Index(3);
        Map(m => m.Type).Name("type").Index(4).TypeConverter<ClassEnumConverter<OrgTypeEnum>>();
        Map(m => m.Identifier).Name("identifier").Index(5).Optional();
        Map(m => m.ParentSourcedId).Name("parentSourcedId").Index(6).Optional();
    }
}
