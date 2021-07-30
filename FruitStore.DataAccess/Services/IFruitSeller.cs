using System.Collections.Generic;

namespace FruitStore.DataAccess.Services
{
    public interface IFruitSeller
    {
        List<Entity.Fruit> GetFruits();
    }
}