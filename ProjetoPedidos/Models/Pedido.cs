// Models/Pedido.cs
public class Pedido
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public ICollection<ItemPedido> Itens { get; set; }
}
