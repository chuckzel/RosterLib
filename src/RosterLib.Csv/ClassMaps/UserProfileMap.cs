using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for UserProfile entity.
/// Maps to userProfiles.csv in OneRoster format.
/// </summary>
public sealed class UserProfileMap : BaseMap<UserProfile>
{
    public UserProfileMap()
    {
        Map(m => m.UserSourcedId).Name("userSourcedId");
        Map(m => m.ProfileId).Name("profileId");
        Map(m => m.ProfileType).Name("profileType");
        Map(m => m.VendorId).Name("vendorId");
        Map(m => m.ApplicationId).Name("applicationId");
        Map(m => m.Description).Name("description");
        Map(m => m.CredentialType).Name("credentialType");
        Map(m => m.Username).Name("username");
        Map(m => m.Password).Name("password");
    }
}
