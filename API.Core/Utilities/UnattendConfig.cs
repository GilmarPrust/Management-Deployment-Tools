namespace DCM.Core.Utilities
{
    public class UnattendConfig
    {
        public string ComputerName { get; set; } = "PC-001";
        public string TimeZone { get; set; } = "E. South America Standard Time";
        public string DomainName { get; set; } = "empresa.local";
        public string DomainUser { get; set; } = "instalador";
        public string DomainPassword { get; set; } = "senha123";
        public string OUPath { get; set; } = "OU=TI,DC=empresa,DC=local";
        public string LocalAdminPassword { get; set; } = "senha123";
        public bool JoinDomain { get; set; } = true;
    }
}
