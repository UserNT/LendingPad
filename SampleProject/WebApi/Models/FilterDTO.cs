using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public enum FilterLogic
    {
        AND,
        OR
    }

    public enum FilterOperator
    {
        LessThan,
        GreatThan,
        LessThanOrEqual,
        GreatThanOrEqual,
        Contains,
        Equal
    }

    public class FilterDTO
    {
        [EnumDataType(typeof(FilterLogic))]
        public FilterLogic Logic { get; set; }

        public List<FilterDTO> Filters { get; set; }
        
        public string Left { get; set; }

        [EnumDataType(typeof(FilterOperator))]
        public FilterOperator Operator { get; set; }

        public object Right { get; set; }
    }

    public class FilterRequestDTO
    {
        [Required]
        public int Skip { get; set; } = 0;

        [Required]
        public int Take { get; set; } = 50;

        public string OrderBy { get; set; }

        public bool IsDescOrder { get; set; } = false;

        [Required]
        public FilterDTO Filters { get; set; }
    }
}