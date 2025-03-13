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

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteDate { get; set; }

        public void MarkAsDeleted()
        {
            DeleteDate = DateTime.UtcNow;
        }
    }
}
