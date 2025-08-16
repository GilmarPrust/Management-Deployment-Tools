using System;
using System.Threading;
using System.Threading.Tasks;

namespace DCM.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface para Unit of Work, gerenciando transa��es e coordenando reposit�rios.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Reposit�rio para entidades Device.
        /// </summary>
        IDeviceRepository Devices { get; }

        /// <summary>
        /// Reposit�rio para entidades DeviceModel.
        /// </summary>
        IDeviceModelRepository DeviceModels { get; }

        /// <summary>
        /// Reposit�rio para entidades Application.
        /// </summary>
        IApplicationRepository Applications { get; }

        /// <summary>
        /// Reposit�rio para entidades ApplicationGroup.
        /// </summary>
        IApplicationGroupRepository ApplicationGroups { get; }

        /// <summary>
        /// Reposit�rio para entidades AppxPackage.
        /// </summary>
        IAppxPackageRepository AppxPackages { get; }

        /// <summary>
        /// Reposit�rio para entidades AppxPackageGroup.
        /// </summary>
        IAppxPackageGroupRepository AppxPackageGroups { get; }

        /// <summary>
        /// Reposit�rio para entidades DeployProfile.
        /// </summary>
        IDeployProfileRepository DeployProfiles { get; }

        /// <summary>
        /// Reposit�rio para entidades Inventory.
        /// </summary>
        IInventoryRepository Inventories { get; }

        /// <summary>
        /// Reposit�rio para entidades DriverPack.
        /// </summary>
        IDriverPackRepository DriverPacks { get; }

        /// <summary>
        /// Reposit�rio para entidades Firmware.
        /// </summary>
        IFirmwareRepository Firmwares { get; }

        /// <summary>
        /// Reposit�rio para entidades Image.
        /// </summary>
        IImageRepository Images { get; }

        /// <summary>
        /// Reposit�rio para entidades Manufacturer.
        /// </summary>
        IManufacturerRepository Manufacturers { get; }

        /// <summary>
        /// Reposit�rio para entidades OperatingSystem.
        /// </summary>
        IOperatingSystemRepository OperatingSystems { get; }

        /// <summary>
        /// Reposit�rio para entidades ProfileTask.
        /// </summary>
        IProfileTaskRepository ProfileTasks { get; }

        /// <summary>
        /// Salva todas as altera��es no banco de dados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>N�mero de entidades afetadas</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Inicia uma transa��o de banco de dados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirma a transa��o atual.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Desfaz a transa��o atual.
        /// </summary>
        Task RollbackTransactionAsync();
    }
}