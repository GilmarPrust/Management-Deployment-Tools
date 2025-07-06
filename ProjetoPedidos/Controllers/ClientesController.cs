// Controllers/ClientesController.cs
[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase {
    private readonly ClienteService _service;

    public ClientesController(ClienteService service) {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClienteDto>>> Get() {
        var clientes = await _service.ObterTodosAsync();
        return Ok(clientes);
    }
}