using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UpdateProductDTO
    {
        /// <summary>
        /// Can be positive or negative (like Oil in the past) or 0 (free product)
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}