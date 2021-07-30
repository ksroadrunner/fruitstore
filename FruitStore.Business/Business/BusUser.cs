using Microsoft.EntityFrameworkCore;
using System.Linq;

using FruitStore.DataAccess;
namespace FruitStore.Business
{
    public class BusUser : BaseBusiness, DataAccess.Services.IUser
    {
        public BusUser(DataContext context) : base(context) { }

        public Entity.User Login(string userName, string password)
        {
            // password : sha1 :)
            return this.Query<Entity.User>()
                       .Include(a => a.UserGroup)
                       .FirstOrDefault(a => a.UserName == userName
                                            && a.Password == password);
        }
    }
}