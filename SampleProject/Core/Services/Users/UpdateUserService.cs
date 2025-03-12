using BusinessEntities;
using Common;
using Data.Repositories;
using System.Collections.Generic;

namespace Core.Services.Users
{
    [AutoRegister]
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Update(User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags, int age)
        {
            user.SetEmail(email);
            user.SetName(name);
            user.SetType(type);
            user.SetMonthlySalary(annualSalary.HasValue ? annualSalary.Value / 12 : (decimal?)null);
            user.SetTags(tags);
            user.SetAge(age);
            _userRepository.Save(user);
        }
    }
}