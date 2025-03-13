using System;

namespace BusinessEntities
{
    public class OrderItem : IdObject
    {
        public OrderItem(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Quantity must be greater than 0", nameof(quantity));

            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }

        public int Quantity { get; }
    }
}
