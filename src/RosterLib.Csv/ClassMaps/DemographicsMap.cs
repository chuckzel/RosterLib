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
        Map(m => m.BirthDate).Name("birthDate").Index(3).Optional().TypeConverter<DateOnlyConverter>();
        Map(m => m.Sex).Name("sex").Index(4).Optional().TypeConverter<ClassEnumConverter<GenderEnum>>();
        Map(m => m.AmericanIndianOrAlaskaNative).Name("americanIndianOrAlaskaNative").Optional().Index(5);
        Map(m => m.Asian).Name("asian").Optional().Index(6);
        Map(m => m.BlackOrAfricanAmerican).Name("blackOrAfricanAmerican").Optional().Index(7);
        Map(m => m.NativeHawaiianOrOtherPacificIslander).Name("nativeHawaiianOrOtherPacificIslander").Optional().Index(8);
        Map(m => m.White).Name("white").Optional().Index(9);
        Map(m => m.DemographicRaceTwoOrMoreRaces).Name("demographicRaceTwoOrMoreRaces").Optional().Index(10);
        Map(m => m.HispanicOrLatinoEthnicity).Name("hispanicOrLatinoEthnicity").Optional().Index(11);
        Map(m => m.CountryOfBirthCode).Name("countryOfBirthCode").Optional().Index(12);
        Map(m => m.StateOfBirthAbbreviation).Name("stateOfBirthAbbreviation").Optional().Index(13);
        Map(m => m.CityOfBirth).Name("cityOfBirth").Optional().Index(14);
        Map(m => m.PublicSchoolResidenceStatus).Name("publicSchoolResidenceStatus").Optional().Index(15);
    }
}
