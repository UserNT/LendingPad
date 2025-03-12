using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        public void Update(User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags, int age)
        {
            user.SetEmail(email);
            user.SetName(name);
            user.SetType(type);
            user.SetMonthlySalary(annualSalary.HasValue ? annualSalary.Value / 12 : (decimal?)null);
            user.SetTags(tags);
            user.SetAge(age);
        }
    }
}