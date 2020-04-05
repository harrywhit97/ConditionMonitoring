namespace Domain.Interfaces
{
    public interface ISensor : IHaveId<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Pin { get; set; }
        public long CommsType { get; set; } //TODO make enum?
    }
}
