using API.Control.DTOs.Inventory;
using API.Control.Models;
using API.Control.Services.Interfaces;
using API.Control2.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Control.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(AppDbContext context, IMapper mapper, ILogger<InventoryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<InventoryReadDTO>> GetAllAsync()
        {
            try
            {
                var inventories = await _context.Inventories.ToListAsync();
                return _mapper.Map<IEnumerable<InventoryReadDTO>>(inventories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os inventários.");
                throw;
            }
        }

        public async Task<InventoryReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var inventory = await _context.Inventories.FindAsync(id);
                return inventory == null ? null : _mapper.Map<InventoryReadDTO>(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar inventário por Id: {Id}", id);
                throw;
            }
        }

        public async Task<InventoryReadDTO> CreateAsync(InventoryCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var entity = _mapper.Map<Inventory>(dto);
                _context.Inventories.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Inventário criado com Id: {Id}", entity.Id);
                return _mapper.Map<InventoryReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar inventário.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, InventoryUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var existing = await _context.Inventories.FindAsync(id);
                if (existing == null) return false;

                _mapper.Map(dto, existing);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Inventário atualizado: {Id}", id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar inventário: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var entity = await _context.Inventories.FindAsync(id);
                if (entity == null) return false;

                _context.Inventories.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Inventário removido: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover inventário: {Id}", id);
                throw;
            }
        }

        Task<IEnumerable<InventoryReadDTO>> IInventoryService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<InventoryReadDTO?> IInventoryService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<InventoryReadDTO> IInventoryService.CreateAsync(InventoryCreateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}