using DCM.Core.Entities;
using DCM.Core.Interfaces.Repositories;
using DCM.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCM.Core.Services.Implementations
{
    public class DeviceAssignmentService : IAssignmentService<Device>
    {
        private readonly IRepository<Device> _repository;

        public DeviceAssignmentService(IRepository<Device> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(Device entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Device>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Device> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Device entity)
        {
            throw new NotImplementedException();
        }

        // Implementação dos métodos...
    }
}
