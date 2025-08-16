using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DCM.Core.Entities;
using DCM.Core.Entities.secondary;
using OSEntity = DCM.Core.Entities.OperatingSystem;

namespace DCM.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface espec�fica para reposit�rio de dispositivos.
    /// </summary>
    public interface IDeviceRepository : IRepository<Device>
    {
        /// <summary>
        /// Obt�m um dispositivo pelo nome do computador.
        /// </summary>
        /// <param name="computerName">Nome do computador</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Dispositivo encontrado ou null</returns>
        Task<Device?> GetByComputerNameAsync(string computerName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m um dispositivo pelo endere�o MAC.
        /// </summary>
        /// <param name="macAddress">Endere�o MAC</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Dispositivo encontrado ou null</returns>
        Task<Device?> GetByMacAddressAsync(string macAddress, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m um dispositivo pelo n�mero de s�rie.
        /// </summary>
        /// <param name="serialNumber">N�mero de s�rie</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Dispositivo encontrado ou null</returns>
        Task<Device?> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m dispositivos por modelo de dispositivo.
        /// </summary>
        /// <param name="deviceModelId">ID do modelo de dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de dispositivos</returns>
        Task<IEnumerable<Device>> GetByDeviceModelAsync(Guid deviceModelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m dispositivos com todas as rela��es inclu�das.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de dispositivos com rela��es</returns>
        Task<IEnumerable<Device>> GetWithAllRelationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adiciona um novo dispositivo ao reposit�rio.
        /// </summary>
        /// <param name="device">Dispositivo a ser adicionado</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task que representa a opera��o ass�ncrona</returns>
        Task AddAsync(Device device, CancellationToken cancellationToken);

        /// <summary>
        /// Atualiza um dispositivo existente no reposit�rio.
        /// </summary>
        /// <param name="existing">Dispositivo a ser atualizado</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task que representa a opera��o ass�ncrona</returns>
        Task UpdateAsync(Device existing, CancellationToken cancellationToken);

        /// <summary>
        /// Verifica se existe um dispositivo com o ID especificado.
        /// </summary>
        /// <param name="id">ID do dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de modelos de dispositivo.
    /// </summary>
    public interface IDeviceModelRepository : IRepository<DeviceModel>
    {
        /// <summary>
        /// Obt�m um modelo de dispositivo pelo fabricante e modelo.
        /// </summary>
        /// <param name="manufacturer">Nome do fabricante</param>
        /// <param name="model">Nome do modelo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Modelo encontrado ou null</returns>
        Task<DeviceModel?> GetByManufacturerAndModelAsync(string manufacturer, string model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m modelos de dispositivo por fabricante.
        /// </summary>
        /// <param name="manufacturer">Nome do fabricante</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de modelos do fabricante</returns>
        Task<IEnumerable<DeviceModel>> GetByManufacturerAsync(string manufacturer, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m modelos com todas as rela��es inclu�das.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de modelos com rela��es</returns>
        Task<IEnumerable<DeviceModel>> GetWithAllRelationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m modelos ativos ordenados por fabricante e modelo.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de modelos ativos</returns>
        Task<IEnumerable<DeviceModel>> GetAllActiveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um modelo pelo fabricante e modelo.
        /// </summary>
        /// <param name="manufacturer">Nome do fabricante</param>
        /// <param name="model">Nome do modelo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByManufacturerAndModelAsync(string manufacturer, string model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um modelo por ID.
        /// </summary>
        /// <param name="deviceModelId">ID do modelo de dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsAsync(Guid deviceModelId, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de aplica��es.
    /// </summary>
    public interface IApplicationRepository : IRepository<Application>
    {
        /// <summary>
        /// Obt�m todas as aplica��es ativas ordenadas por DisplayName.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de aplica��es ativas</returns>
        Task<IEnumerable<Application>> GetAllActiveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m uma aplica��o pelo NameID.
        /// </summary>
        /// <param name="nameID">Identificador da aplica��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Aplica��o encontrada ou null</returns>
        Task<Application?> GetByNameIdAsync(string nameID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m aplica��es por vers�o.
        /// </summary>
        /// <param name="version">Vers�o do aplicativo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de aplica��es na vers�o informada</returns>
        Task<IEnumerable<Application>> GetByVersionAsync(string version, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe uma aplica��o pelo NameID.
        /// </summary>
        /// <param name="nameID">Identificador da aplica��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByNameIdAsync(string nameID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adiciona uma nova aplica��o ao reposit�rio.
        /// </summary>
        /// <param name="application">Aplica��o a ser adicionada</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task que representa a opera��o ass�ncrona</returns>
        Task AddAsync(Application application, CancellationToken cancellationToken);

        /// <summary>
        /// Atualiza uma aplica��o existente no reposit�rio.
        /// </summary>
        /// <param name="existing">Aplica��o a ser atualizada</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task que representa a opera��o ass�ncrona</returns>
        Task UpdateAsync(Application existing, CancellationToken cancellationToken);

        /// <summary>
        /// Verifica se existe uma aplica��o com o ID especificado.
        /// </summary>
        /// <param name="id">ID da aplica��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de grupos de aplica��es.
    /// </summary>
    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        /// <summary>
        /// Obt�m um grupo de aplica��es pelo nome.
        /// </summary>
        /// <param name="name">Nome do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Grupo encontrado ou null</returns>
        Task<ApplicationGroup?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m grupos ordenados por prioridade.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de grupos ordenados por prioridade</returns>
        Task<IEnumerable<ApplicationGroup>> GetOrderedByPriorityAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m grupo com aplica��es inclu�das.
        /// </summary>
        /// <param name="id">ID do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Grupo com aplica��es ou null</returns>
        Task<ApplicationGroup?> GetWithApplicationsAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m grupos por categoria.
        /// </summary>
        /// <param name="category">Categoria dos grupos</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de grupos da categoria</returns>
        Task<IEnumerable<ApplicationGroup>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m grupos ativos ordenados por prioridade.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de grupos ativos</returns>
        Task<IEnumerable<ApplicationGroup>> GetActiveGroupsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um grupo pelo nome.
        /// </summary>
        /// <param name="name">Nome do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de pacotes AppX.
    /// </summary>
    public interface IAppxPackageRepository : IRepository<AppxPackage>
    {
        /// <summary>
        /// Obt�m um pacote AppX pelo nome completo.
        /// </summary>
        /// <param name="packageFullName">Nome completo do pacote</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Pacote AppX encontrado ou null</returns>
        Task<AppxPackage?> GetByPackageFullNameAsync(string packageFullName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m pacotes AppX por publicador.
        /// </summary>
        /// <param name="publisher">Publicador</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de pacotes AppX</returns>
        Task<IEnumerable<AppxPackage>> GetByPublisherAsync(string publisher, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m pacotes AppX que pertencem a um grupo espec�fico.
        /// </summary>
        /// <param name="appxPackageGroupId">ID do grupo de pacotes AppX</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de pacotes AppX do grupo</returns>
        Task<IEnumerable<AppxPackage>> GetByAppxPackageGroupAsync(Guid appxPackageGroupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m pacotes AppX por categoria.
        /// </summary>
        /// <param name="category">Categoria dos pacotes</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de pacotes AppX da categoria</returns>
        Task<IEnumerable<AppxPackage>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m pacotes AppX por dispositivo.
        /// </summary>
        /// <param name="deviceId">ID do dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de pacotes AppX do dispositivo</returns>
        Task<IEnumerable<AppxPackage>> GetByDeviceAsync(Guid deviceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m pacotes AppX ativos ordenados por nome.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de pacotes AppX ativos</returns>
        Task<IEnumerable<AppxPackage>> GetAllActiveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um pacote AppX pelo nome completo.
        /// </summary>
        /// <param name="packageFullName">Nome completo do pacote</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByPackageFullNameAsync(string packageFullName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adiciona um novo pacote AppX ao reposit�rio.
        /// </summary>
        /// <param name="package">Pacote AppX a ser adicionado</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task que representa a opera��o ass�ncrona</returns>
        Task AddAsync(AppxPackage package, CancellationToken cancellationToken);

        /// <summary>
        /// Atualiza um pacote AppX existente no reposit�rio.
        /// </summary>
        /// <param name="existing">Pacote AppX a ser atualizado</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task que representa a opera��o ass�ncrona</returns>
        Task UpdateAsync(AppxPackage existing, CancellationToken cancellationToken);

        /// <summary>
        /// Verifica se existe um pacote AppX com o ID especificado.
        /// </summary>
        /// <param name="id">ID do pacote AppX</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de grupos de pacotes AppX.
    /// </summary>
    public interface IAppxPackageGroupRepository : IRepository<AppxPackageGroup>
    {
        /// <summary>
        /// Obt�m um grupo de pacotes AppX pelo nome.
        /// </summary>
        /// <param name="name">Nome do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Grupo encontrado ou null</returns>
        Task<AppxPackageGroup?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m grupos ordenados por prioridade.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de grupos ordenados por prioridade</returns>
        Task<IEnumerable<AppxPackageGroup>> GetOrderedByPriorityAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m grupo com pacotes AppX inclu�dos.
        /// </summary>
        /// <param name="id">ID do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Grupo com pacotes AppX ou null</returns>
        Task<AppxPackageGroup?> GetWithAppxPackagesAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m grupos por categoria.
        /// </summary>
        /// <param name="category">Categoria dos grupos</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de grupos da categoria</returns>
        Task<IEnumerable<AppxPackageGroup>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m grupos ativos ordenados por prioridade.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de grupos ativos</returns>
        Task<IEnumerable<AppxPackageGroup>> GetActiveGroupsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um grupo pelo nome.
        /// </summary>
        /// <param name="name">Nome do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de perfis de implanta��o.
    /// </summary>
    public interface IDeployProfileRepository : IRepository<DeployProfile>
    {
        /// <summary>
        /// Obt�m um perfil de implanta��o pelo nome.
        /// </summary>
        /// <param name="name">Nome do perfil</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Perfil de implanta��o encontrado ou null</returns>
        Task<DeployProfile?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m perfis de implanta��o com todas as rela��es.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de perfis com rela��es</returns>
        Task<IEnumerable<DeployProfile>> GetWithAllRelationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m perfis de implanta��o ativos.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de perfis ativos</returns>
        Task<IEnumerable<DeployProfile>> GetActiveProfilesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m perfis por imagem.
        /// </summary>
        /// <param name="imageId">ID da imagem</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de perfis da imagem</returns>
        Task<IEnumerable<DeployProfile>> GetByImageAsync(Guid imageId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um perfil pelo nome.
        /// </summary>
        /// <param name="name">Nome do perfil</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de invent�rios.
    /// </summary>
    public interface IInventoryRepository : IRepository<Inventory>
    {
        /// <summary>
        /// Obt�m invent�rio por dispositivo.
        /// </summary>
        /// <param name="deviceId">ID do dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Invent�rio encontrado ou null</returns>
        Task<Inventory?> GetByDeviceIdAsync(Guid deviceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m invent�rios com hardware inclu�do.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de invent�rios com hardware</returns>
        Task<IEnumerable<Inventory>> GetWithHardwareAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m invent�rios mais recentes por dispositivo.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de invent�rios mais recentes</returns>
        Task<IEnumerable<Inventory>> GetLatestInventoriesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe invent�rio para um dispositivo.
        /// </summary>
        /// <param name="deviceId">ID do dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByDeviceIdAsync(Guid deviceId, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de pacotes de driver.
    /// </summary>
    public interface IDriverPackRepository : IRepository<DriverPack>
    {
        /// <summary>
        /// Obt�m pacotes de driver por modelo de dispositivo.
        /// </summary>
        /// <param name="deviceModelId">ID do modelo de dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de pacotes de driver</returns>
        Task<IEnumerable<DriverPack>> GetByDeviceModelAsync(Guid deviceModelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m pacotes de driver por sistema operacional.
        /// </summary>
        /// <param name="operatingSystem">Sistema operacional</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de pacotes de driver</returns>
        Task<IEnumerable<DriverPack>> GetByOperatingSystemAsync(string operatingSystem, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m pacotes de driver por modelo de dispositivo e sistema operacional.
        /// </summary>
        /// <param name="deviceModelId">ID do modelo de dispositivo</param>
        /// <param name="operatingSystem">Sistema operacional</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de pacotes de driver</returns>
        Task<IEnumerable<DriverPack>> GetByDeviceModelAndOSAsync(Guid deviceModelId, string operatingSystem, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m a vers�o mais recente do driver para um modelo.
        /// </summary>
        /// <param name="deviceModelId">ID do modelo de dispositivo</param>
        /// <param name="operatingSystem">Sistema operacional</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Driver pack mais recente ou null</returns>
        Task<DriverPack?> GetLatestVersionAsync(Guid deviceModelId, string operatingSystem, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de firmware.
    /// </summary>
    public interface IFirmwareRepository : IRepository<Firmware>
    {
        /// <summary>
        /// Obt�m firmware por modelo de dispositivo.
        /// </summary>
        /// <param name="deviceModelId">ID do modelo de dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Firmware encontrado ou null</returns>
        Task<Firmware?> GetByDeviceModelAsync(Guid deviceModelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m a vers�o mais recente do firmware para um modelo.
        /// </summary>
        /// <param name="deviceModelId">ID do modelo de dispositivo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Firmware mais recente ou null</returns>
        Task<Firmware?> GetLatestByDeviceModelAsync(Guid deviceModelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m firmwares por vers�o.
        /// </summary>
        /// <param name="version">Vers�o do firmware</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de firmwares</returns>
        Task<IEnumerable<Firmware>> GetByVersionAsync(string version, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m firmwares ativos ordenados por vers�o.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de firmwares ativos</returns>
        Task<IEnumerable<Firmware>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de imagens.
    /// </summary>
    public interface IImageRepository : IRepository<Image>
    {
        /// <summary>
        /// Obt�m uma imagem pelo nome.
        /// </summary>
        /// <param name="name">Nome da imagem</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Imagem encontrada ou null</returns>
        Task<Image?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m imagens ativas ordenadas por nome.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de imagens ativas</returns>
        Task<IEnumerable<Image>> GetActiveImagesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m imagens por vers�o.
        /// </summary>
        /// <param name="version">Vers�o da imagem</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de imagens</returns>
        Task<IEnumerable<Image>> GetByVersionAsync(string version, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m imagens por sistema operacional.
        /// </summary>
        /// <param name="operatingSystemId">ID do sistema operacional</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de imagens</returns>
        Task<IEnumerable<Image>> GetByOperatingSystemAsync(Guid operatingSystemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe uma imagem pelo nome.
        /// </summary>
        /// <param name="name">Nome da imagem</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de fabricantes.
    /// </summary>
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
        /// <summary>
        /// Obt�m um fabricante pelo nome curto.
        /// </summary>
        /// <param name="shortName">Nome curto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Fabricante encontrado ou null</returns>
        Task<Manufacturer?> GetByShortNameAsync(string shortName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m um fabricante pelo nome completo.
        /// </summary>
        /// <param name="name">Nome completo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Fabricante encontrado ou null</returns>
        Task<Manufacturer?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m fabricantes ativos ordenados por nome.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de fabricantes ativos</returns>
        Task<IEnumerable<Manufacturer>> GetAllActiveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um fabricante pelo nome curto.
        /// </summary>
        /// <param name="shortName">Nome curto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByShortNameAsync(string shortName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um fabricante pelo nome completo.
        /// </summary>
        /// <param name="name">Nome completo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de sistemas operacionais.
    /// </summary>
    public interface IOperatingSystemRepository : IRepository<OSEntity>
    {
        /// <summary>
        /// Obt�m um sistema operacional pelo nome curto.
        /// </summary>
        /// <param name="shortName">Nome curto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Sistema operacional encontrado ou null</returns>
        Task<OSEntity?> GetByShortNameAsync(string shortName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m sistemas operacionais suportados (ativos).
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de sistemas operacionais suportados</returns>
        Task<IEnumerable<OSEntity>> GetSupportedVersionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m sistemas operacionais ativos ordenados por nome.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de sistemas operacionais ativos</returns>
        Task<IEnumerable<OSEntity>> GetAllActiveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m um sistema operacional pelo nome completo.
        /// </summary>
        /// <param name="name">Nome completo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Sistema operacional encontrado ou null</returns>
        Task<OSEntity?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m sistemas operacionais por vers�o.
        /// </summary>
        /// <param name="version">Vers�o do sistema operacional</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de sistemas operacionais</returns>
        Task<IEnumerable<OSEntity>> GetByVersionAsync(string version, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um sistema operacional pelo nome curto.
        /// </summary>
        /// <param name="shortName">Nome curto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByShortNameAsync(string shortName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um sistema operacional pelo nome completo.
        /// </summary>
        /// <param name="name">Nome completo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adiciona um novo sistema operacional ao reposit�rio.
        /// </summary>
        /// <param name="operatingSystem">Sistema operacional a ser adicionado</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task que representa a opera��o ass�ncrona</returns>
        Task AddAsync(OSEntity operatingSystem, CancellationToken cancellationToken);

        /// <summary>
        /// Atualiza um sistema operacional existente no reposit�rio.
        /// </summary>
        /// <param name="operatingSystem">Sistema operacional a ser atualizado</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task que representa a opera��o ass�ncrona</returns>
        Task UpdateAsync(OSEntity operatingSystem, CancellationToken cancellationToken);

        /// <summary>
        /// Verifica se existe um sistema operacional com o ID especificado.
        /// </summary>
        /// <param name="id">ID do sistema operacional</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Interface espec�fica para reposit�rio de tarefas de perfil.
    /// </summary>
    public interface IProfileTaskRepository : IRepository<ProfileTask>
    {
        /// <summary>
        /// Obt�m tarefas de perfil por perfil de implanta��o.
        /// </summary>
        /// <param name="deployProfileId">ID do perfil de implanta��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de tarefas de perfil</returns>
        Task<IEnumerable<ProfileTask>> GetByDeployProfileAsync(Guid deployProfileId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m tarefas por tipo.
        /// </summary>
        /// <param name="taskType">Tipo da tarefa</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de tarefas do tipo especificado</returns>
        Task<IEnumerable<ProfileTask>> GetByTypeAsync(string taskType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m tarefas ordenadas por ordem de execu��o.
        /// </summary>
        /// <param name="deployProfileId">ID do perfil de implanta��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de tarefas ordenadas</returns>
        Task<IEnumerable<ProfileTask>> GetOrderedTasksAsync(Guid deployProfileId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m tarefas ativas de um perfil.
        /// </summary>
        /// <param name="deployProfileId">ID do perfil de implanta��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de tarefas ativas</returns>
        Task<IEnumerable<ProfileTask>> GetActiveTasksAsync(Guid deployProfileId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m tarefas por fase de execu��o.
        /// </summary>
        /// <param name="phase">Fase de execu��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de tarefas da fase</returns>
        Task<IEnumerable<ProfileTask>> GetByPhaseAsync(string phase, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe tarefa para um perfil espec�fico.
        /// </summary>
        /// <param name="deployProfileId">ID do perfil de implanta��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByDeployProfileAsync(Guid deployProfileId, CancellationToken cancellationToken = default);
    }
}