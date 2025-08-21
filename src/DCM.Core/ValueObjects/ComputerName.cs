using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using DCM.Core.Enums;
using DCM.Core.ValueObjects;

namespace DCM.Core.ValueObjects
{
    /// <summary>
    /// Representa um nome de computador válido.
    /// </summary>
    public sealed class ComputerName
    {
        [Required, StringLength(15)]
        public string Value { get; }

        /// <summary>
        /// Construtor padrão privado para o EF Core.
        /// </summary>
        private ComputerName() 
        {
            Value = string.Empty;
        }

        /// <summary>
        /// Cria um nome de computador baseado no tipo de dispositivo.
        /// Se computerName for fornecido, valida e usa; caso contrário, gera automaticamente.
        /// </summary>
        /// <param name="deviceType">Tipo do dispositivo usado para determinar o prefixo</param>
        /// <param name="computerName">Nome do computador opcional (será gerado se null ou vazio)</param>
        /// <exception cref="ArgumentException">Lançado quando o computerName fornecido é inválido</exception>
        public ComputerName(DeviceType deviceType, string? computerName = null)
        {
            if (string.IsNullOrWhiteSpace(computerName))
            {
                // Gerar novo nome com base no tipo de dispositivo
                var prefix = DeviceTypeHelper.GetPrefix(deviceType);
                Value = NewComputerName(prefix);
            }
            else
            {
                // Validar e usar o nome fornecido
                var normalizedName = computerName.Trim().ToUpperInvariant();
                ValidateComputerName(normalizedName);
                Value = normalizedName;
            }
        }

        /// <summary>
        /// Cria um nome de computador com validação a partir de uma string.
        /// </summary>
        /// <param name="computerName">Nome do computador</param>
        /// <exception cref="ArgumentException">Lançado quando o nome é inválido</exception>
        public ComputerName(string computerName)
        {
            if (string.IsNullOrWhiteSpace(computerName))
                throw new ArgumentException("Nome do computador não pode ser vazio ou nulo.", nameof(computerName));

            var normalizedName = computerName.Trim().ToUpperInvariant();
            ValidateComputerName(normalizedName);
            Value = normalizedName;
        }

        /// <summary>
        /// Cria um nome de computador baseado no tipo de dispositivo (geração automática).
        /// </summary>
        /// <param name="deviceType">Tipo de dispositivo para usar como prefixo</param>
        /// <returns>Nova instância de ComputerName</returns>
        public static ComputerName FromDeviceType(DeviceType deviceType)
        {
            return new ComputerName(deviceType);
        }

        /// <summary>
        /// Cria um nome de computador baseado em um prefixo específico.
        /// </summary>
        /// <param name="prefix">Prefixo a ser usado</param>
        /// <returns>Nova instância de ComputerName</returns>
        /// <exception cref="ArgumentException">Lançado quando o prefixo é inválido</exception>
        public static ComputerName FromPrefix(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("Prefixo não pode ser vazio ou nulo.", nameof(prefix));

            if (!DeviceTypeHelper.IsValidPrefix(prefix))
                throw new ArgumentException($"Prefixo '{prefix}' não é válido.", nameof(prefix));

            var generatedName = NewComputerName(prefix);
            return new ComputerName(generatedName);
        }

        /// <summary>
        /// Obtém o tipo de dispositivo baseado no prefixo do nome do computador.
        /// </summary>
        /// <returns>Tipo de dispositivo ou null se não for possível determinar</returns>
        public DeviceType? GetDeviceType()
        {
            if (string.IsNullOrEmpty(Value))
                return null;

            var parts = Value.Split('-');
            if (parts.Length < 2)
                return null;

            var prefix = parts[0];
            
            try
            {
                return DeviceTypeHelper.GetDeviceType(prefix);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Verifica se o nome do computador está consistente com o tipo de dispositivo especificado.
        /// </summary>
        /// <param name="expectedDeviceType">Tipo de dispositivo esperado</param>
        /// <returns>True se o prefixo do nome corresponde ao tipo, false caso contrário</returns>
        public bool IsConsistentWithDeviceType(DeviceType expectedDeviceType)
        {
            var detectedType = GetDeviceType();
            return detectedType == expectedDeviceType;
        }

        /// <summary>
        /// Valida o formato do nome do computador.
        /// </summary>
        /// <param name="computerName">Nome do computador a ser validado</param>
        /// <exception cref="ArgumentException">Lançado quando o nome é inválido</exception>
        private static void ValidateComputerName(string computerName)
        {
            if (computerName.Length > 15)
                throw new ArgumentException("Nome do computador não pode ter mais de 15 caracteres.", nameof(computerName));

            if (!Regex.IsMatch(computerName, @"^[A-Z0-9-]+$"))
                throw new ArgumentException("Nome do computador deve conter apenas letras maiúsculas, números e hífens.", nameof(computerName));

            if (computerName.Contains("--") || computerName.StartsWith("-") || computerName.EndsWith("-"))
                throw new ArgumentException("Nome do computador não pode conter hífens consecutivos ou começar/terminar com hífen.", nameof(computerName));
        }

        /// <summary>
        /// Gera um nome de computador aleatório usando um prefixo específico.
        /// </summary>
        /// <param name="prefix">Prefixo a ser usado</param>
        /// <returns>Nome de computador gerado no formato PREFIXO-XXXX</returns>
        private static string NewComputerName(string prefix)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            
            var hex = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                hex.Append(random.Next(0, 16).ToString("X"));
            }

            return $"{prefix}-{hex}";
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj)
        {
            return obj is ComputerName other && Value == other.Value;
        }

        public override int GetHashCode() => Value?.GetHashCode() ?? 0;

        /// <summary>
        /// Operador de igualdade entre duas instâncias de ComputerName.
        /// </summary>
        public static bool operator ==(ComputerName? left, ComputerName? right)
        {
            return left?.Value == right?.Value;
        }

        /// <summary>
        /// Operador de desigualdade entre duas instâncias de ComputerName.
        /// </summary>
        public static bool operator !=(ComputerName? left, ComputerName? right)
        {
            return !(left == right);
        }
    }
}