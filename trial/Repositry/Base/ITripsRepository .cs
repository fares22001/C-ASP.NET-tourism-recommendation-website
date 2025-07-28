using trial.Models;

namespace trial.Repositry.Base
{
    public interface ITripsRepository : IGenericRepository<Trips>
    {
        IEnumerable<Trips> FindAllWithDetails();
    }
}
