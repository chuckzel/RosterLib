using RosterLib.Csv.Converters;
using RosterLib.Domain.Rostering;
using RosterLib.Domain.Rostering.Enums;

namespace RosterLib.Csv.ClassMaps;

/// <summary>
/// CSV mapping for Demographics entity.
/// Maps to demographics.csv in OneRoster format.
/// </summary>
public sealed class DemographicsMap : BaseMap<Demographics>
{
    public DemographicsMap()
    {
        Map(m => m.UserSourcedId).Name("userSourcedId");
        Map(m => m.BirthDate).Name("birthDate").TypeConverter<DateOnlyConverter>();
        Map(m => m.Sex).Name("sex").TypeConverter<ClassEnumConverter<GenderEnum>>();
        Map(m => m.AmericanIndianOrAlaskaNative).Name("americanIndianOrAlaskaNative");
        Map(m => m.Asian).Name("asian");
        Map(m => m.BlackOrAfricanAmerican).Name("blackOrAfricanAmerican");
        Map(m => m.NativeHawaiianOrOtherPacificIslander).Name("nativeHawaiianOrOtherPacificIslander");
        Map(m => m.White).Name("white");
        Map(m => m.DemographicRaceTwoOrMoreRaces).Name("demographicRaceTwoOrMoreRaces");
        Map(m => m.HispanicOrLatinoEthnicity).Name("hispanicOrLatinoEthnicity");
        Map(m => m.CountryOfBirthCode).Name("countryOfBirthCode");
        Map(m => m.StateOfBirthAbbreviation).Name("stateOfBirthAbbreviation");
        Map(m => m.CityOfBirth).Name("cityOfBirth");
        Map(m => m.PublicSchoolResidenceStatus).Name("publicSchoolResidenceStatus");
    }
}
