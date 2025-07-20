using API.Control.DTOs.Image;
using API.Control.Models;
using API.Control.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Control.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ImageService> _logger;

        public ImageService(AppDbContext context, IMapper mapper, ILogger<ImageService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ImageReadDTO>> GetAllAsync()
        {
            try
            {
                var images = await _context.Image
                    .Include(i => i.DeployProfiles)
                    .ToListAsync();
                return _mapper.Map<IEnumerable<ImageReadDTO>>(images);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as imagens.");
                throw;
            }
        }

        public async Task<ImageReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            try
            {
                var image = await _context.Image
                    .Include(i => i.DeployProfiles)
                    .FirstOrDefaultAsync(i => i.Id == id);

                return image == null ? null : _mapper.Map<ImageReadDTO>(image);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar imagem por Id: {Id}", id);
                throw;
            }
        }

        public async Task<ImageReadDTO> CreateAsync(ImageCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            try
            {
                var entity = _mapper.Map<Image>(dto);
                _context.Image.Add(entity);
                await _context.SaveChangesAsync();
                return _mapper.Map<ImageReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar imagem.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, ImageUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            try
            {
                var image = await _context.Image.FindAsync(id);
                if (image == null) return false;

                _mapper.Map(dto, image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar imagem: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            try
            {
                var image = await _context.Image.FindAsync(id);
                if (image == null) return false;

                _context.Image.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover imagem: {Id}", id);
                throw;
            }
        }
    }
}