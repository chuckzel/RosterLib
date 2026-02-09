using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for Role entity.
/// Maps to roles.csv in OneRoster format.
/// </summary>
public sealed class RoleMap : BaseMap<Role>
{
    public RoleMap()
    {
        Map(m => m.UserSourcedId).Name("userSourcedId");
        Map(m => m.RoleType).Name("roleType").TypeConverter<ClassEnumConverter<RoleTypeEnum>>();
        Map(m => m.RoleName).Name("role").TypeConverter<ClassEnumConverter<RoleEnum>>();
        Map(m => m.BeginDate).Name("beginDate").TypeConverter<DateOnlyConverter>();
        Map(m => m.EndDate).Name("endDate").TypeConverter<DateOnlyConverter>();
        Map(m => m.OrgSourcedId).Name("orgSourcedId");
        Map(m => m.UserProfileSourcedId).Name("userProfileSourcedId");
    }
}
