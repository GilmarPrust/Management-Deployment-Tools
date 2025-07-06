// Services/ClienteService.cs
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoPedidos.Data;

public class ClienteService {
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public ClienteService(IMapper mapper, AppDbContext context) {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<ClienteDto>> ObterTodosAsync() {
        var clientes = await _context.Clientes.ToListAsync();
        return _mapper.Map<List<ClienteDto>>(clientes);
    }
}
