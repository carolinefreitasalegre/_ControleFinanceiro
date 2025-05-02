

namespace Domain.Entities
{
    public class ContaParcelada : Conta
    {

        public ContaParcelada()
        {
        }
        public ContaParcelada(Usuario usuario) : base(usuario) { }

        public int QuantudadeParcelas { get; set; }
        public decimal ValorParcela { get; set; }
    }
}
