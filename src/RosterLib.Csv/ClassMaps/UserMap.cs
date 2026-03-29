using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for User entity.
/// Maps to users.csv in OneRoster format.
/// </summary>
public sealed class UserMap : BaseMap<User>
{
    public UserMap()
    {
        Map(m => m.EnabledUser).Name("enabledUser").Index(3);
        Map(m => m.Username).Name("username").Index(4);
        Map(m => m.UserIds).Name("userIds").Index(5).Optional().TypeConverter<UserIdListConverter>();
        Map(m => m.GivenName).Name("givenName").Index(6);
        Map(m => m.FamilyName).Name("familyName").Index(7);
        Map(m => m.MiddleName).Name("middleName").Index(8).Optional();
        Map(m => m.Identifier).Name("identifier").Index(9).Optional();
        Map(m => m.Email).Name("email").Index(10).Optional();
        Map(m => m.Sms).Name("sms").Index(11).Optional();
        Map(m => m.Phone).Name("phone").Index(12).Optional();
        Map(m => m.AgentSourcedIds).Name("agentSourcedIds").Index(13).Optional().TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.Grades).Name("grades").Index(14).Optional().TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.Password).Name("password").Index(15).Optional();
        Map(m => m.UserMasterIdentifier).Name("userMasterIdentifier").Index(16).Optional();
        Map(m => m.PreferredFirstName).Name("preferredGivenName").Index(17).Optional();
        Map(m => m.PreferredMiddleName).Name("preferredMiddleName").Index(18).Optional();
        Map(m => m.PreferredLastName).Name("preferredFamilyName").Index(19).Optional();
        Map(m => m.PrimaryOrgSourcedId).Name("primaryOrgSourcedId").Index(20).Optional();
        Map(m => m.Pronouns).Name("pronouns").Index(21).Optional();
    }
}
