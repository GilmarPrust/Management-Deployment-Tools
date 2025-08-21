using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DCM.Core.Entities; // Refer�ncia aos tipos do dom�nio principal

namespace DCM.Core.Entities.secondary
{
    /// <summary>
    /// Representa um grupo de aplica��es para organiza��o e implanta��o conjunta.
    /// </summary>
    public class ApplicationGroup : BaseEntity
    {
        /// <summary>
        /// Nome do grupo de aplica��es.
        /// </summary>
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descri��o do grupo de aplica��es.
        /// </summary>
        [StringLength(240)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Prioridade do grupo para ordena��o de instala��o.
        /// Valores menores t�m maior prioridade.
        /// </summary>
        [Range(1, 9999, ErrorMessage = "A prioridade deve estar entre 1 e 9999.")]
        public int Priority { get; set; } = 100;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public ApplicationGroup() { }

        /// <summary>
        /// Construtor para criar um grupo com nome e descri��o.
        /// </summary>
        /// <param name="name">Nome do grupo</param>
        /// <param name="description">Descri��o do grupo</param>
        /// <param name="priority">Prioridade do grupo (padr�o: 100)</param>
        public ApplicationGroup(string name, string description = "", int priority = 100)
        {
            Name = name;
            Description = description;
            Priority = priority;
        }

        // Backings para EF Core com lazy initialization
        private ICollection<Device>? _devices;
        public virtual ICollection<Device> Devices 
        {
            get => _devices ??= new List<Device>();
            set => _devices = value;
        }

        private ICollection<DeviceModel>? _deviceModels;
        public virtual ICollection<DeviceModel> DeviceModels 
        {
            get => _deviceModels ??= new List<DeviceModel>();
            set => _deviceModels = value;
        }

        private ICollection<Application>? _applications;
        public virtual ICollection<Application> Applications 
        {
            get => _applications ??= new List<Application>();
            set => _applications = value;
        }

        private ICollection<DeployProfile>? _deployProfiles;
        public virtual ICollection<DeployProfile> DeployProfiles 
        {
            get => _deployProfiles ??= new List<DeployProfile>();
            set => _deployProfiles = value;
        }

        /// <summary>
        /// Verifica se o grupo tem aplica��es associadas.
        /// </summary>
        public bool HasApplications => _applications?.Count > 0;

        /// <summary>
        /// Obt�m a contagem de aplica��es no grupo.
        /// </summary>
        public int ApplicationCount => _applications?.Count ?? 0;

        /// <summary>
        /// Verifica se o grupo tem dispositivos associados.
        /// </summary>
        public bool HasDevices => _devices?.Count > 0;

        /// <summary>
        /// Obt�m a contagem de dispositivos no grupo.
        /// </summary>
        public int DeviceCount => _devices?.Count ?? 0;

        /// <summary>
        /// Verifica se o grupo tem modelos de dispositivos associados.
        /// </summary>
        public bool HasDeviceModels => _deviceModels?.Count > 0;

        /// <summary>
        /// Obt�m a contagem de modelos de dispositivos no grupo.
        /// </summary>
        public int DeviceModelCount => _deviceModels?.Count ?? 0;

        /// <summary>
        /// Verifica se o grupo est� associado a perfis de implanta��o.
        /// </summary>
        public bool HasDeployProfiles => _deployProfiles?.Count > 0;

        /// <summary>
        /// Obt�m a contagem de perfis de implanta��o associados ao grupo.
        /// </summary>
        public int DeployProfileCount => _deployProfiles?.Count ?? 0;

        #region Applications
        /// <summary>
        /// Adiciona uma aplica��o ao grupo com regras de dom�nio.
        /// </summary>
        public void AddApplication(Application application)
            => this.AddItem(Applications, application, app =>
            {
                if (!Enabled)
                    throw new System.InvalidOperationException("Cannot add application to disabled group");
            });

        /// <summary>
        /// Remove uma aplica��o do grupo.
        /// </summary>
        public void RemoveApplication(Application application)
            => this.RemoveItem(Applications, application);

        /// <summary>
        /// Verifica se uma aplica��o pertence ao grupo.
        /// </summary>
        public bool ContainsApplication(Application application) => Applications.ContainsItem(application);

        /// <summary>
        /// Remove todas as aplica��es do grupo.
        /// </summary>
        public void ClearApplications() => this.ClearItems(Applications);
        #endregion

        #region Devices
        /// <summary>
        /// Adiciona um dispositivo ao grupo com regras de dom�nio.
        /// </summary>
        public void AddDevice(Device device)
            => this.AddItem(Devices, device, d =>
            {
                if (!Enabled)
                    throw new System.InvalidOperationException("Cannot add device to disabled group");
            });

        /// <summary>
        /// Remove um dispositivo do grupo.
        /// </summary>
        public void RemoveDevice(Device device)
            => this.RemoveItem(Devices, device);

        /// <summary>
        /// Verifica se um dispositivo pertence ao grupo.
        /// </summary>
        public bool ContainsDevice(Device device) => Devices.ContainsItem(device);

        /// <summary>
        /// Remove todos os dispositivos do grupo.
        /// </summary>
        public void ClearDevices() => this.ClearItems(Devices);
        #endregion

        #region DeviceModels
        /// <summary>
        /// Adiciona um modelo de dispositivo ao grupo com regras de dom�nio.
        /// </summary>
        public void AddDeviceModel(DeviceModel model)
            => this.AddItem(DeviceModels, model, m =>
            {
                if (!Enabled)
                    throw new System.InvalidOperationException("Cannot add device model to disabled group");
            });

        /// <summary>
        /// Remove um modelo de dispositivo do grupo.
        /// </summary>
        public void RemoveDeviceModel(DeviceModel model)
            => this.RemoveItem(DeviceModels, model);

        /// <summary>
        /// Verifica se um modelo de dispositivo pertence ao grupo.
        /// </summary>
        public bool ContainsDeviceModel(DeviceModel model) => DeviceModels.ContainsItem(model);

        /// <summary>
        /// Remove todos os modelos de dispositivos do grupo.
        /// </summary>
        public void ClearDeviceModels() => this.ClearItems(DeviceModels);
        #endregion

        #region DeployProfiles
        /// <summary>
        /// Adiciona um perfil de implanta��o ao grupo.
        /// </summary>
        public void AddDeployProfile(DeployProfile profile)
            => this.AddItem(DeployProfiles, profile, p =>
            {
                if (!Enabled)
                    throw new System.InvalidOperationException("Cannot add deploy profile to disabled group");
            });

        /// <summary>
        /// Remove um perfil de implanta��o do grupo.
        /// </summary>
        public void RemoveDeployProfile(DeployProfile profile)
            => this.RemoveItem(DeployProfiles, profile);

        /// <summary>
        /// Verifica se um perfil de implanta��o pertence ao grupo.
        /// </summary>
        public bool ContainsDeployProfile(DeployProfile profile) => DeployProfiles.ContainsItem(profile);

        /// <summary>
        /// Remove todos os perfis de implanta��o do grupo.
        /// </summary>
        public void ClearDeployProfiles() => this.ClearItems(DeployProfiles);
        #endregion
    }
}