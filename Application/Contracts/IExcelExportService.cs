using Application.Dtos.RelatorioResponse;

namespace Application.Contracts
{
    public interface IExcelExportService
    {
        Task<byte[]> GerarRelatorioExcel();
    }
}
