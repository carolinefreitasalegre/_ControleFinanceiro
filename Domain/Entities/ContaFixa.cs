

namespace Domain.Entities
{
    public class ContaFixa : Conta
    {
        public ContaFixa() { }
        public ContaFixa(Usuario usuario) : base(usuario) { }

        public DateTime Venciment { get; set; }

    }
}
