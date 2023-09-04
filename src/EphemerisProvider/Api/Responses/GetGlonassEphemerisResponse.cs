namespace EphemerisProvider.Api.Responses;

public class GetGlonassEphemerisResponse
{
    public int CsSatNumber { get; set; }
    public int OrbitPointId { get; set; }
    public int Health { get; set; }
    public int FrequencyLitera { get; set; }
    public DateTime EpochTimeUtc { get; set; }
    public double XEcefKm { get; set; }
    public double YEcefKm { get; set; }
    public double ZEcefKm { get; set; }
    public double VxEcefKmPerSec { get; set; }
    public double VyEcefKmPerSec { get; set; }
    public double VzEcefKmPerSec { get; set; }
    public double AxEcefKmPerSec2 { get; set; }
    public double AyEcefKmPerSec2 { get; set; }
    public double AzEcefKmPerSec2 { get; set; }
    public double Af0Hz { get; set; }
    public double Af1HzPerSec { get; set; }
}