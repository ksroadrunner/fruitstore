using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitStore.Entity
{
    public class User : DefaultEntity
    {
        [ForeignKey("UserGroup")]
        public int UserGroupId { get; set; }
        public string  Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Entity.UserGroup UserGroup { get; set; }
    }
}