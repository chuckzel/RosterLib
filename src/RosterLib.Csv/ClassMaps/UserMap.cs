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
        Map(m => m.UserMasterIdentifier).Name("userMasterIdentifier");
        Map(m => m.Username).Name("username");
        Map(m => m.EnabledUser).Name("enabledUser");
        Map(m => m.GivenName).Name("givenName");
        Map(m => m.FamilyName).Name("familyName");
        Map(m => m.MiddleName).Name("middleName");
        Map(m => m.PreferredFirstName).Name("preferredFirstName");
        Map(m => m.PreferredMiddleName).Name("preferredMiddleName");
        Map(m => m.PreferredLastName).Name("preferredLastName");
        Map(m => m.Pronouns).Name("pronouns");
        Map(m => m.PrimaryOrgSourcedId).Name("primaryOrgSourcedId");
        Map(m => m.Identifier).Name("identifier");
        Map(m => m.Email).Name("email");
        Map(m => m.Sms).Name("sms");
        Map(m => m.Phone).Name("phone");
        Map(m => m.AgentSourcedIds).Name("agentSourcedIds").TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.Grades).Name("grades").TypeConverter<CommaSeparatedStringConverter>();
        Map(m => m.Password).Name("password");
        
        // UserIds collection is not included in standard CSV - would require separate file or custom handling
        Map(m => m.UserIds).Ignore();
    }
}
