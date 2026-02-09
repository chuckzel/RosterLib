using RosterLib.Domain.Common;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents a user from the OneRoster Rostering specification.
/// Users can be students, teachers, parents, administrators, or other system participants.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_User">OneRoster User specification</see>
/// </remarks>
public class User : Base
{
    /// <summary>
    /// The user's master identifier. This is a definitive globally unique identifier (not the interoperability sourcedId).
    /// </summary>
    public string? UserMasterIdentifier { get; set; }

    /// <summary>
    /// The username used to log in to the system.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// The set of user identifiers (e.g., state ID, national ID, etc.).
    /// </summary>
    public List<UserId>? UserIds { get; set; }

    /// <summary>
    /// Whether the user is enabled in the system.
    /// </summary>
    public bool EnabledUser { get; set; } = true;

    /// <summary>
    /// The given name. Also known as the first name.
    /// </summary>
    public required string GivenName { get; set; }

    /// <summary>
    /// The family name. Also known as the last name.
    /// </summary>
    public required string FamilyName { get; set; }

    /// <summary>
    /// The set of middle names. If more than one middle name is needed, separate using a space, e.g. 'Wingarde Granville'.
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// The user's preferred first name.
    /// </summary>
    public string? PreferredFirstName { get; set; }

    /// <summary>
    /// The user's preferred middle name(s).
    /// </summary>
    public string? PreferredMiddleName { get; set; }

    /// <summary>
    /// The user's preferred last name.
    /// </summary>
    public string? PreferredLastName { get; set; }

    /// <summary>
    /// The pronoun(s) by which this person is referenced. Examples (in the case of English) include 'she/her/hers', 'he/him/his', 'they/them/theirs', 'ze/hir/hir', 'xe/xir', or a statement that the person's name should be used instead of any pronoun.
    /// </summary>
    public string? Pronouns { get; set; }

    /// <summary>
    /// The sourcedId of the primary organization for the user.
    /// </summary>
    public string? PrimaryOrgSourcedId { get; set; }

    /// <summary>
    /// National/state identifier for the user.
    /// </summary>
    public string? Identifier { get; set; }

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The user's SMS number.
    /// </summary>
    public string? Sms { get; set; }

    /// <summary>
    /// The user's phone number.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// The list of sourcedIds for agents (e.g., parents, guardians) associated with this user.
    /// </summary>
    public List<string>? AgentSourcedIds { get; set; }

    /// <summary>
    /// The list of grades for the user.
    /// </summary>
    public List<string>? Grades { get; set; }

    /// <summary>
    /// The plain text password for the user (deprecated for security reasons).
    /// </summary>
    public string? Password { get; set; }
}
