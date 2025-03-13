using Common.Extensions;
using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class User : IdNameObject
    {
        public const int MinAge = 16;
        public const int MaxAge = 86;

        public string Email { get; private set; }

        public UserTypes Type { get; private set; }

        public decimal? MonthlySalary { get; private set; }

        public int Age { get; private set; }

        public IEnumerable<string> Tags { get; private set; } = new List<string>();

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email was not provided.", nameof(email));
            }
            Email = email;
        }

        public void SetType(UserTypes type)
        {
            Type = type;
        }

        public void SetAge(int age)
        {
            if (age < MinAge || age > MaxAge)
            {
                throw new ArgumentOutOfRangeException($"Age must be between {MinAge} and {MaxAge}");
            }
            Age = age;
        }

        public void SetMonthlySalary(decimal? monthlySalary)
        {
            if (monthlySalary.HasValue && monthlySalary.Value < 0)
            {
                throw new ArgumentOutOfRangeException("MonthlySalary must be greater than or equal to 0.", nameof(monthlySalary));
            }
            MonthlySalary = monthlySalary;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            ((List<string>)Tags).Initialize(tags);
        }
    }
}