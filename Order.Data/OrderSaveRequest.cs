namespace Order.Data
{
    public class OrderSaveRequest<T> : BaseRequest where T : class
    {
        public T Entity { get; set; }
    }
}
