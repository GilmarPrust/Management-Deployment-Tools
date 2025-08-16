using System;
using System.Threading;
using System.Threading.Tasks;

namespace DCM.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface para Unit of Work, gerenciando transações e coordenando repositórios.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Repositório para entidades Device.
        /// </summary>
        IDeviceRepository Devices { get; }

        /// <summary>
        /// Repositório para entidades DeviceModel.
        /// </summary>
        IDeviceModelRepository DeviceModels { get; }

        /// <summary>
        /// Repositório para entidades Application.
        /// </summary>
        IApplicationRepository Applications { get; }

        /// <summary>
        /// Repositório para entidades ApplicationGroup.
        /// </summary>
        IApplicationGroupRepository ApplicationGroups { get; }

        /// <summary>
        /// Repositório para entidades AppxPackage.
        /// </summary>
        IAppxPackageRepository AppxPackages { get; }

        /// <summary>
        /// Repositório para entidades AppxPackageGroup.
        /// </summary>
        IAppxPackageGroupRepository AppxPackageGroups { get; }

        /// <summary>
        /// Repositório para entidades DeployProfile.
        /// </summary>
        IDeployProfileRepository DeployProfiles { get; }

        /// <summary>
        /// Repositório para entidades Inventory.
        /// </summary>
        IInventoryRepository Inventories { get; }

        /// <summary>
        /// Repositório para entidades DriverPack.
        /// </summary>
        IDriverPackRepository DriverPacks { get; }

        /// <summary>
        /// Repositório para entidades Firmware.
        /// </summary>
        IFirmwareRepository Firmwares { get; }

        /// <summary>
        /// Repositório para entidades Image.
        /// </summary>
        IImageRepository Images { get; }

        /// <summary>
        /// Repositório para entidades Manufacturer.
        /// </summary>
        IManufacturerRepository Manufacturers { get; }

        /// <summary>
        /// Repositório para entidades OperatingSystem.
        /// </summary>
        IOperatingSystemRepository OperatingSystems { get; }

        /// <summary>
        /// Repositório para entidades ProfileTask.
        /// </summary>
        IProfileTaskRepository ProfileTasks { get; }

        /// <summary>
        /// Salva todas as alterações no banco de dados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Número de entidades afetadas</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Inicia uma transação de banco de dados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirma a transação atual.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Desfaz a transação atual.
        /// </summary>
        Task RollbackTransactionAsync();
    }
}