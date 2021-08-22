using Domain;

namespace DataLayer.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        User GetUserByEmail(string email);
        User GetUserByUsername(string username);
    }
}
