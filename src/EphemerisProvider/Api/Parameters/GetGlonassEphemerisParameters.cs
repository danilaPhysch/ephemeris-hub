namespace EphemerisProvider.Api.Parameters;

public class GetGlonassEphemerisParameters
{
    public long CsSatelliteNumber { get; set; }

    public DateTime Time { get; set; }

    public override string ToString()
    {
        return $"satellite number: {CsSatelliteNumber}; time: {Time};";
    }
}