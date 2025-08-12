using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace DCM.Core.Utilities
{
    /// <summary>
    /// Representa um nome de computador válido.
    /// </summary>
    public sealed class ComputerName
    {
        [Required, StringLength(15)]
        public string Value { get; }

        // Construtor padrão privado para o EF Core
        private ComputerName() { }

        public ComputerName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                value = Generate();

            if (value.Length > 15)
                throw new ArgumentException("Nome do computador não pode ter mais de 15 caracteres.", nameof(value));

            if (!Regex.IsMatch(value, @"^[A-Z0-9-]+$"))
                throw new ArgumentException("Nome do computador deve conter apenas letras maiúsculas, números e hífens.", nameof(value));

            if (value.Contains("--") || value.StartsWith("-") || value.EndsWith("-"))
                throw new ArgumentException("Nome do computador não pode conter hífens consecutivos ou começar/terminar com hífen.", nameof(value));

            Value = value.ToUpperInvariant();
        }

        public override string ToString() => Value;

        public override bool Equals(object obj)
        {
            return obj is ComputerName other && Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        private static string Generate()
        {
            string[] prefixes = { "KIOSK", "DSKTP", "NOTBK", "TABLT", "SERVR", "VM" };
            var random = new Random(Guid.NewGuid().GetHashCode());
            string prefix = prefixes[random.Next(prefixes.Length)];

            var hex = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                hex.Append(random.Next(0, 16).ToString("X"));
            }

            return ($"{prefix}-{hex}");
        }
    }
}