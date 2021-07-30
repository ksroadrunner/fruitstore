using System.Collections.Generic;

namespace FruitStore.Entity
{
    public class UserGroup : DefaultEntity
    {
        public string Name { get; set; }
        public List<Entity.User> Users { get; set; }
    }
}