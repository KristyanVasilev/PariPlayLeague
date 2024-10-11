namespace PariPlayLeague.Domain.Abstractions.Interfaces
{
    public interface IIdentifiable<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
