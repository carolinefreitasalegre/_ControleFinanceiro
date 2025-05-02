using Domain.Enums;

namespace Application.Dtos.Request
{
    public class ContaResponse
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public Status Status { get; set; }
        public string UsuarioName { get; set; } = string.Empty; 
    }
}
