namespace API.Control.Entities
{
    public class Manufacturer : _BaseEntity
    {
        [Required, StringLength(50)]
        public required string Name { get; init; }

        [Required, StringLength(50)]
        public required string ShortName { get; init; }
        

        public Manufacturer() { }
    }
}
