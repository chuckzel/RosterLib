namespace RosterLib.Domain.Rostering;

/// <summary>
/// The container for a single set of credentials for an account.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/spec/oneroster/v1p2#Data_Credential">OneRoster v1.2 - Credential</see>
/// </remarks>
public class Credential
{
    /// <summary>
    /// The type of credentials for the profile. This should be indicative of when this specific credential should be used.
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// The username.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// The password. This may be omitted or hashed for security.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Proprietary extensions for the credential.
    /// </summary>
    public Dictionary<string, string>? Extensions { get; set; }
}
