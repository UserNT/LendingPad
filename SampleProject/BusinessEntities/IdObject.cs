using System;

namespace BusinessEntities
{
    public abstract class IdObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            return obj is IdObject other && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}