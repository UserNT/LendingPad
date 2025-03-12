using System;

namespace BusinessEntities
{
    public class IdNameObject : IdObject
    {
        public string Name { get; private set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name was not provided.", nameof(name));
            }
            Name = name;
        }
    }
}