

namespace Domain.Entities
{
    public class ContaVariavel : Conta
    {
        public ContaVariavel()
        {
        }

        public ContaVariavel(Usuario usuario) : base(usuario) { }

        public string Observacao { get; set; } = string.Empty;
    }
}
