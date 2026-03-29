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
        Map(m => m.UserSourcedId).Name("userSourcedId").Index(3);
        Map(m => m.ProfileId).Name("profileId").Index(4);
        Map(m => m.ProfileType).Name("profileType").Index(5);
        Map(m => m.VendorId).Name("vendorId").Index(6);
        Map(m => m.ApplicationId).Name("applicationId").Index(7).Optional();
        Map(m => m.Description).Name("description").Index(8).Optional();
        Map(m => m.CredentialType).Name("credentialType").Index(9);
        Map(m => m.Username).Name("username").Index(10);
        Map(m => m.Password).Name("password").Index(11).Optional();
    }
}
