namespace FruitStore.Entity
{
    public class Fruit : DefaultEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public float UnitPrice { get; set; }
        public Enums.Vitamin Vitamins { get; set; }
    }
}