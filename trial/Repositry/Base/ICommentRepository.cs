using trial.Models;

namespace trial.Repositry.Base
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IEnumerable<Comment> GetCommentsByTripId(int tripId);
    }
}
