using trial.Data;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Repositry
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context) { }

        public bool ExistsByEmail(string email)
        {
            return context.Persons.Any(p => p.Email == email);
        }

        public bool ExistsByName(string name)
        {
            return context.Persons.Any(p => p.Name == name);
        }
    }
}
