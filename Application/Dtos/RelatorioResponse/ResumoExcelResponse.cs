using Domain.Entities;
using Domain.Enums;

namespace Application.Dtos.RelatorioResponse
{
    public class ResumoExcelResponse
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int? Parcelas { get; set; }
        public TipoDeConta TipoDeConta { get; set; }
        public DateTime DataLancamento { get; set; } = DateTime.Now;
        public Status Status { get; set; }
        public string Usuario { get; set; } = string.Empty;

    }
}
