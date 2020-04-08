namespace Domain.Interfaces
{
    public interface IHasId<TId>
    {
        public  TId Id { get; set; }
    }
}
