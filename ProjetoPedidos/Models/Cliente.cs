public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Pedido> Pedidos { get; set; }
}
