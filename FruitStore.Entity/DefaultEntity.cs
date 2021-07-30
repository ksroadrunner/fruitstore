using System;

namespace FruitStore.Entity
{
    public class DefaultEntity : BaseEntity
    {
        public bool Deleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public DefaultEntity()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }
    }
}