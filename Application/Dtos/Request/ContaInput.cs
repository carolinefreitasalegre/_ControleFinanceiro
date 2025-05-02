using Domain.Entities;
using Domain.Enums;

namespace Application.Dtos.Request
{
    public class ContaInput
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoDeConta TipoDeConta { get; set; }
        public int? Parcelas { get; set; }
        public Status Status { get; set; }

        public Guid UsuarioId { get; set; }
    }
}
