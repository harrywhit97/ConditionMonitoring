namespace Domain.Interfaces
{
    public interface IHaveId<TId>
    {
        public  TId Id { get; set; }
    }
}
