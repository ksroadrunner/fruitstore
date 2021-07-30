namespace FruitStore.DataAccess.Services
{
    public interface IUserSession
    {
        Entity.User User { get; set; }
    }
}