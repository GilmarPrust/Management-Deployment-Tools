using System.Text.RegularExpressions;

namespace API.Control.ValueObjects
{
    /// <summary>
    /// Representa um modelo de dispositivo.
    /// </summary>
    public sealed class MacAddress
    {
        public string Value { get; }

        public MacAddress(string value)
        {
            Value = value.ToUpper();
        }

        public static MacAddress Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("MAC address não pode ser vazio.", nameof(value));

            if (!Regex.IsMatch(value, @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$"))
                throw new ArgumentException("Formato de MAC address inválido.", nameof(value));

            return new MacAddress(value);
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj) => obj is MacAddress other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}