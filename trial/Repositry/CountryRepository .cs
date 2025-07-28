using Microsoft.EntityFrameworkCore;
using trial.Data;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Repositry
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context) { }

        public bool CountryNameExists(string countryName)
        {
            return context.Countries.Any(c => EF.Functions.Like(c.CountryName, countryName));
        }
    }
}
