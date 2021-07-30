namespace FruitStore.Business
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Content { get; set; }
        public string ErrorMessage { get; set; }
    }
}