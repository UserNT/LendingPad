using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessEntities;

namespace WebApi.Models.Users
{
    public class UserModel
    {
        [Required(ErrorMessage = "Name was not provided.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email was not provided.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Type was not provided.")]
        [EnumDataType(typeof(UserTypes))]
        public UserTypes Type { get; set; }

        [Range(User.MinAge, User.MaxAge)]
        public int Age {  get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "AnnualSalary must be greater than or equal to 0.")]
        public decimal? AnnualSalary { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}