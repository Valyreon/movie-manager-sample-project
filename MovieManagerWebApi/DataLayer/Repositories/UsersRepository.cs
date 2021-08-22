using System.Linq;
using DataLayer.Interfaces;
using Domain;

namespace DataLayer.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(MovieDbContext context) : base(context)
        {
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByUsername(string username)
        {
            return context.Users.SingleOrDefault(u => u.Username == username);
        }
    }
}
