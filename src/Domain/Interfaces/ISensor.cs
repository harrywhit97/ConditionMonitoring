namespace Domain.Interfaces
{
    public interface ISensor<TId> : IHaveId<TId>
    {
        public TId Id { get; set; }
        public string Name { get; set; }
    }
}
