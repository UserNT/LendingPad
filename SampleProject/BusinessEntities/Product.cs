using System;

namespace BusinessEntities
{
    public class Product : IdNameObject
    {
        /// <summary>
        /// Can be positive or negative (like Oil in the past) or 0 (free product)
        /// </summary>
        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; private set; } = DateTime.UtcNow;

        public DateTime? DeleteDate { get; private set; }

        public void MarkAsDeleted()
        {
            if (DeleteDate.HasValue)
                throw new InvalidOperationException("The delete date has already been set.");

            DeleteDate = DateTime.UtcNow;
        }
    }
}
