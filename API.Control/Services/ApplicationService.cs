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

        public ApplicationDTO Create(ApplicationCreateDto dto)
        {
            var model = _mapper.Map<Application>(dto);
            // salvar no banco, etc.
            return _mapper.Map<ApplicationDTO>(model);
        }
    }
}
