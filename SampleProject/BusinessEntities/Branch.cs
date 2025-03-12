using Common.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities
{
    public class Branch : IdNameObject
    {
        private readonly List<User> _members = new List<User>();

        public void SetMembers(IEnumerable<User> members)
        {
            _members.Initialize(members.Where(q => q.Type == UserTypes.Employee));
        }
    }
}