using Domain.Entities;
using Domain.Enums;

public class Conta
{
    protected Conta()
    {
    }

    protected Conta(Usuario usuario)
    {
        Usuario = usuario;
        UsuarioId = usuario.Id;
    }

 
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataLancamento { get; set; } = DateTime.Now;
    public Status Status { get; set; }
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}
