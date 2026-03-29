using RosterLib.Domain.Common;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Domain.Rostering;

/// <summary>
/// Represents demographics for a user in the OneRoster Rostering specification.
/// Demographics are an optional extension to the core user record. Its SourcedId is the same as the associated user.
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/sites/default/files/spec/oneroster/v1p2/rostering-informationmodel/OneRosterv1p2RosteringService_InfoModelv1p0.html#Data_Demographics">OneRoster Demographics specification</see>
/// </remarks>
public class Demographics : Base
{
    public DateOnly? BirthDate { get; set; }
    public GenderEnum? Sex { get; set; }
    public bool? AmericanIndianOrAlaskaNative { get; set; }
    public bool? Asian { get; set; }
    public bool? BlackOrAfricanAmerican { get; set; }
    public bool? NativeHawaiianOrOtherPacificIslander { get; set; }
    public bool? White { get; set; }
    public bool? DemographicRaceTwoOrMoreRaces { get; set; }
    public bool? HispanicOrLatinoEthnicity { get; set; }
    public string? CountryOfBirthCode { get; set; }
    public string? StateOfBirthAbbreviation { get; set; }
    public string? CityOfBirth { get; set; }
    public string? PublicSchoolResidenceStatus { get; set; }
}
