using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents a user profile from the OneRoster Rostering specification.
/// Contains authentication and authorization information for accessing vendor systems, tools, and applications.
/// </summary>
/// <remarks>
/// <para>
/// The multiplicity of the credentials does not seem to agree between the information model and the CSV binding
/// (0 to many vs. exactly 1). Here the CSV model is followed for simplicity, but may be changed once the REST API is implemented.
/// </para>
/// <para>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_UserProfile">OneRoster UserProfile specification</see>
/// </para>
/// </remarks>
public class UserProfile : Base
{
    /// <summary>
    /// The link to the associated user i.e. the sourcedId for the user.
    /// </summary>
    public required string UserSourcedId { get; set; }

    /// <summary>
    /// The unique identifier for the profile. This does not need to be a globally unique identifier but it must be unique within the scope of the user.
    /// </summary>
    public required string ProfileId { get; set; }

    /// <summary>
    /// The type of profile. There is no predefined vocabulary.
    /// </summary>
    public required string ProfileType { get; set; }

    /// <summary>
    /// The vendor identifier for the system/tool.
    /// </summary>
    public required string VendorId { get; set; }

    /// <summary>
    /// The application identifier for the system/tool.
    /// </summary>
    public string? ApplicationId { get; set; }

    /// <summary>
    /// A human readable description of the use of the profile. This should not contain any security information for access to the account.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The type of credentials for the user profile. This should be indicative of when this credential should be used.
    /// </summary>
    public string? CredentialType { get; set; }

    /// <summary>
    /// The username for this profile.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The password for the user. This may or may not be an encrypted string.
    /// </summary>
    public string? Password { get; set; }
}
