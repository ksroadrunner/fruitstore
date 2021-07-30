using Microsoft.AspNetCore.Http;

using FruitStore.DataAccess.Services;
using FruitStore.Entity;
using FruitStore.WebApp.Extensions;
namespace FruitStore.WebApp
{
    public class SessionService : IUserSession
    {
        public User User
        {
            get => _session.Get<Entity.User>();
            set => _session.Set(value);
        }

        ISession _session;
        public SessionService(IHttpContextAccessor context)
        {
            this._session = context.HttpContext.Session;
        }
    }
}