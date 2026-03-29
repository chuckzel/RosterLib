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
        Map(m => m.UserSourcedId).Name("userSourcedId").Index(3);
        Map(m => m.RoleType).Name("roleType").Index(4).TypeConverter<ClassEnumConverter<RoleTypeEnum>>();
        Map(m => m.RoleName).Name("role").Index(5).TypeConverter<ClassEnumConverter<RoleEnum>>();
        Map(m => m.BeginDate).Name("beginDate").Index(6).Optional().TypeConverter<DateOnlyConverter>();
        Map(m => m.EndDate).Name("endDate").Index(7).Optional().TypeConverter<DateOnlyConverter>();
        Map(m => m.OrgSourcedId).Name("orgSourcedId").Index(8);
        Map(m => m.UserProfileSourcedId).Name("userProfileSourcedId").Index(9).Optional();
    }
}
