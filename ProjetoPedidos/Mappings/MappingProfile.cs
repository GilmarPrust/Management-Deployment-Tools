// Mappings/MappingProfile.cs
using AutoMapper;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Cliente, ClienteDto>().ReverseMap();
        CreateMap<Pedido, PedidoDto>().ReverseMap();
        CreateMap<ItemPedido, ItemPedidoDto>().ReverseMap();
    }
}
