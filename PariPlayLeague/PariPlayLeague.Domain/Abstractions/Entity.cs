using PariPlayLeague.Domain.Abstractions.Interfaces;

namespace PariPlayLeague.Domain.Abstractions
{
    public abstract class Entity<TId> : IIdentifiable<TId> where TId : struct
    {
        public virtual TId Id { get; set; }
    }
}
