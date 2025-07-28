using Microsoft.EntityFrameworkCore;
using trial.Data;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Repositry
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Comment> GetCommentsByTripId(int tripId)
        {
            return context.Comments.Where(c => c.TripId == tripId).Include(c => c.Person).ToList();
        }
    }
}
