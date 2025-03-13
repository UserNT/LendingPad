using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private readonly List<OrderItem> orderItems = new List<OrderItem>();

        public IEnumerable<OrderItem> Items => orderItems.AsReadOnly();

        public decimal Total { get; private set; }

        public DateTime CreateDate { get; private set; } = DateTime.UtcNow;

        public DateTime? DeleteDate { get; private set; }

        public void MarkAsDeleted()
        {
            if (DeleteDate.HasValue)
                throw new InvalidOperationException("The delete date has already been set.");

            DeleteDate = DateTime.UtcNow;
        }

        public void AddItem(Product product, int quantity)
        {
            orderItems.Add(new OrderItem(product, quantity));

            Total += product.Price * quantity;
        }
    }
}
