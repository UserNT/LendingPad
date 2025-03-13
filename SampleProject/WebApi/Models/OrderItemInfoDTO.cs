namespace WebApi.Models
{
    public class OrderItemInfoDTO
    {
        public ProductInfoDTO Product { get; set; }

        public int Quantity { get; set; }
    }
}