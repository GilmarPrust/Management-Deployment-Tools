using System.Text;
using System.Text.RegularExpressions;

namespace API.Control.ValueObjects
{
    public sealed class ComputerName
    {
        public string Value { get; set; }

        private ComputerName(string value)
        {
            Value = value.ToUpper();
        }

        public static ComputerName Create(string value)
        {
            if (value.Length > 15)
                throw new ArgumentException("Nome do computador não pode ter mais de 15 caracteres.", nameof(value));

            if (string.IsNullOrWhiteSpace(value))
                return new ComputerName(Generate());

            return new ComputerName(value);
        }

        private static string Generate()
        {
            string[] prefixes = { "KIOSK", "DSKTP", "NOTBK", "TABLT", "SERVR" };
            var random = new Random(Guid.NewGuid().GetHashCode());
            string prefix = prefixes[random.Next(prefixes.Length)];

            var hex = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                hex.Append(random.Next(0, 16).ToString("X"));
            }

            return ($"{prefix}-{hex}");
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj) => obj is ComputerName other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}