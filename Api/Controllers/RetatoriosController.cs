using Application.Contracts;
using Application.Dtos.RelatorioResponse;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetatoriosController : ControllerBase
    {
        private readonly IExcelExportService _excelExportService;

        public RetatoriosController(IExcelExportService excelExportService)
        {
            _excelExportService = excelExportService;
        }


        [HttpGet("relatorio-excel")]
        public async Task<IActionResult> GerarRelatorioExcel()
        {
            var arquivo = await _excelExportService.GerarRelatorioExcel();
            var nome = $"Relatório_excel_contas{DateTime.Now:ddMMyyyyHHmmss}.xlsx";

            return File(arquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nome);

        }

    }
}
