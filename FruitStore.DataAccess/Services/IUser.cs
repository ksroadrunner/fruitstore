
namespace FruitStore.DataAccess.Services
{
    public interface IUser
    {
        Entity.User Login(string userName, string password);
    }
}