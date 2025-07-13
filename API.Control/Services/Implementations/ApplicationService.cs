using API.Control.DTOs.Application;
using API.Control.Models;
using API.Control.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Control.Services.Implementations
{
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ApplicationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationReadDTO>> GetAllAsync()
        {
            var apps = await _context.Applications.ToListAsync();
            return _mapper.Map<IEnumerable<ApplicationReadDTO>>(apps);
        }

        public async Task<ApplicationReadDTO?> GetByIdAsync(Guid id)
        {
            var app = await _context.Applications.FindAsync(id);
            return app == null ? null : _mapper.Map<ApplicationReadDTO>(app);
        }

        public async Task<ApplicationReadDTO> CreateAsync(ApplicationCreateDTO dto)
        {
            var entity = _mapper.Map<Application>(dto);

            _context.Applications.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ApplicationReadDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, ApplicationUpdateDTO dto)
        {
            var existing = await _context.Applications.FindAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Applications.FindAsync(id);
            if (entity == null) return false;

            _context.Applications.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
