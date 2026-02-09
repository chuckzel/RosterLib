namespace RosterLib.Domain.Common;

/// <summary>
/// Base class for OneRoster extensible vocabulary enumerations.
/// Supports predefined values while allowing extensions with custom values (e.g., 'ext:customValue').
/// </summary>
/// <remarks>
/// <see href="https://www.imsglobal.org/spec/oneroster/v1p2/bind/csv/#extending-profiling-the-csv-binding">OneRoster CSV extensibility</see>
/// </remarks>
public record ClassEnum<T>
{
    public T Value { get; init; }

    public ClassEnum(T value)
    {
        Value = value;
    }
}
