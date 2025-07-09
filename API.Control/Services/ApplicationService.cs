using API.Control.DTOs;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper _mapper;

        public ApplicationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Application_DTO Create(ApplicationCreateDto dto)
        {
            var model = _mapper.Map<Application>(dto);
            // salvar no banco, etc.
            return _mapper.Map<Application_DTO>(model);
        }
    }
}
