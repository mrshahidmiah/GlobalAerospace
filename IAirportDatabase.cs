using GlobalAeroTechnicalTest.Model;

namespace GlobalAeroTechnicalTest.Data;

public interface IAirportDatabase
{
    IList<Airport> GetByCountry(string countryCode);
    IList<Airport> LondonAirports { get; }
}