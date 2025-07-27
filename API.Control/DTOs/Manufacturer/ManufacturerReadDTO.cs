namespace API.Control.DTOs.Manufacturer
{
    public class ManufacturerReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string ShortName { get; init; } = string.Empty;

    }
}
