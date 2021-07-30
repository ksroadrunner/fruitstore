using System.Collections.Generic;
using System.Linq;

using FruitStore.Entity;
using FruitStore.DataAccess;
namespace FruitStore.Business
{
    public class BusFruitSeller : BaseBusiness, DataAccess.Services.IFruitSeller
    {
        public BusFruitSeller(DataContext context) : base(context) { }

        public List<Fruit> GetFruits()
        {
            return this.Query<Entity.Fruit>()
                       .ToList();
        }
    }
}