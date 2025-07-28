using trial.Models;

namespace trial.Repositry.Base
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        bool CountryNameExists(string countryName);
    }
}
