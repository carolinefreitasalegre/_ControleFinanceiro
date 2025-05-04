using System.Drawing;
using Application.Contracts;
using Application.Dtos.RelatorioResponse;
using Infrastruture.DataAccess;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Application.Services
{
    public class ExcelExportService : IExcelExportService
    {
        private readonly AppDbContext _context;

        public ExcelExportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GerarRelatorioExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var dados = await _context.Contas
                .Include(u => u.Usuario)
                .Select(c => new ResumoExcelResponse
                {
                    Nome = c.Nome,
                    Valor = c.Valor,
                    Parcelas = c.Parcelas,
                    TipoDeConta = c.TipoDeConta,
                    DataLancamento = c.DataLancamento,
                    Status = c.Status,
                    Usuario = c.Usuario.Name,
                })
                .ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Relatório de dados");

                worksheet.Cells[1, 1].Value = "Nome";
                worksheet.Cells[1, 2].Value = "Valor";
                worksheet.Cells[1, 3].Value = "Parcelas";
                worksheet.Cells[1, 4].Value = "Tipo de Conta";
                worksheet.Cells[1, 5].Value = "Data de Lançamento";
                worksheet.Cells[1, 6].Value = "Status";
                worksheet.Cells[1, 7].Value = "Usuário";

                using (var range = worksheet.Cells[1, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int linha = 2;

                foreach (var dado in dados)
                {
                    worksheet.Cells[linha, 1].Value = dado.Nome;
                    worksheet.Cells[linha, 2].Value = dado.Valor;
                    worksheet.Cells[linha, 3].Value = dado.Parcelas;
                    worksheet.Cells[linha, 4].Value = dado.TipoDeConta;
                    worksheet.Cells[linha, 5].Value = dado.DataLancamento.ToString("dd/MM/yyyy");
                    worksheet.Cells[linha, 6].Value = dado.Status;
                    worksheet.Cells[linha, 7].Value = dado.Usuario;
                    linha++;
                }


                worksheet.Protection.IsProtected = true;
                worksheet.Protection.SetPassword("NãoPoderáSerEditado!!!");

             

                worksheet.Cells.AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }
}
