using RealProperty.Data.Entities;

namespace RealProperty.Data.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string email, string password);
    }
}
