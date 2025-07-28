using Microsoft.EntityFrameworkCore;
using trial.Data;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Repositry
{
    public class TripsRepository : GenericRepository<Trips>, ITripsRepository
    {
        public TripsRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Trips> FindAllWithDetails()
        {
            return context.Trips.Include(t => t.Country).ToList();
        }
    }
}
