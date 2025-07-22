
namespace API.Control.DTOs.ProfileTask
{
    public class ProfileTaskReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool Enabled { get; init; }

    }
}


