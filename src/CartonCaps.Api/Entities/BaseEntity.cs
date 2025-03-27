namespace CartonCaps.Api.Entities
{
    public abstract record class BaseEntity
    {
        public required int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}