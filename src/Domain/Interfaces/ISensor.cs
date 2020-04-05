namespace Domain.Interfaces
{
    public interface ISensor : IHaveId<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
