using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Microsoft.EntityFrameworkCore;
namespace FruitStore.DataAccess
{
    public class DataContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var asm = typeof(Entity.Fruit).Assembly;
            var entities = asm.GetTypes()
                              .Where(a => a.IsClass
                                          && (a.BaseType == typeof(Entity.BaseEntity)
                                              || a.BaseType == typeof(Entity.DefaultEntity))
                                          && a.Name != "DefaultEntity"
                                          && !a.GetCustomAttributes(false).Any(t => t is NotMappedAttribute))
                              .ToList();

            entities.ForEach(a => modelBuilder.Entity(a));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=CR\\SQLEXPRESS;Database=FruitStore;User Id=sa;Password=12qwaszxcv!;");
        }
    }
}