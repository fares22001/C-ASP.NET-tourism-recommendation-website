using trial.Models;

namespace trial.Repositry.Base
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        bool ExistsByEmail(string email);
        bool ExistsByName(string name);
    }
}
