using Domain.Entities;
using Domain.Enums;

namespace Application.Dtos.Request
{
    public class ContaEdicaoInput
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public Status Status { get; set; }

      
    }
}
