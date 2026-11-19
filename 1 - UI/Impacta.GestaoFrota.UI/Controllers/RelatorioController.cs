using System.Globalization;
using System.Text;
using ClosedXML.Excel;
using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Impacta.GestaoFrota.UI.Controllers;

public class RelatorioController : Controller
{
    private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    private readonly FrotaContext _db;

    static RelatorioController()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public RelatorioController(FrotaContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult QuantidadeVeiculosPorFabricante()
    {
        return View(new RelatorioPageViewModel
        {
            Titulo = "Quantidade de Veículos por Fabricante",
            PdfUrl = Url.Action(nameof(QuantidadeVeiculosPorFabricantePdf))!,
            XlsxUrl = Url.Action(nameof(QuantidadeVeiculosPorFabricanteXlsx))!,
            CsvUrl = Url.Action(nameof(QuantidadeVeiculosPorFabricanteCsv))!,
            TextoUrl = Url.Action(nameof(QuantidadeVeiculosPorFabricanteTxt))!
        });
    }

    public IActionResult UtilizacaoCompletaVeiculos()
    {
        return View(new RelatorioPageViewModel
        {
            Titulo = "Completo de Utilização dos Veículos",
            PdfUrl = Url.Action(nameof(UtilizacaoCompletaVeiculosPdf))!,
            XlsxUrl = Url.Action(nameof(UtilizacaoCompletaVeiculosXlsx))!,
            CsvUrl = Url.Action(nameof(UtilizacaoCompletaVeiculosCsv))!,
            TextoUrl = Url.Action(nameof(UtilizacaoCompletaVeiculosTxt))!
        });
    }

    public IActionResult QuantidadeVeiculosPorFabricantePdf()
    {
        var dados = ConsultarQuantidadeVeiculosPorFabricante();
        var linhas = dados.Select(item => new[]
        {
            item.Fabricante,
            item.Quantidade.ToString("N0", CultureInfo.GetCultureInfo("pt-BR"))
        });

        return File(GerarPdf("Quantidade de Veículos por Fabricante", ["Fabricante", "Quantidade"], linhas), "application/pdf");
    }

    public IActionResult QuantidadeVeiculosPorFabricanteXlsx()
    {
        var dados = ConsultarQuantidadeVeiculosPorFabricante()
            .Select(item => new object?[] { item.Fabricante, item.Quantidade });

        return BaixarXlsx("quantidade-veiculos-por-fabricante.xlsx", "Veículos por fabricante", ["Fabricante", "Quantidade"], dados);
    }

    public IActionResult QuantidadeVeiculosPorFabricanteCsv()
    {
        var dados = ConsultarQuantidadeVeiculosPorFabricante()
            .Select(item => new object?[] { item.Fabricante, item.Quantidade });

        return BaixarCsv("quantidade-veiculos-por-fabricante.csv", ["Fabricante", "Quantidade"], dados);
    }

    public IActionResult QuantidadeVeiculosPorFabricanteTxt()
    {
        var dados = ConsultarQuantidadeVeiculosPorFabricante()
            .Select(item => new object?[] { item.Fabricante, item.Quantidade });

        return BaixarTexto("quantidade-veiculos-por-fabricante.txt", ["Fabricante", "Quantidade"], dados);
    }

    public IActionResult UtilizacaoCompletaVeiculosPdf()
    {
        var dados = ConsultarUtilizacaoCompletaVeiculos();
        var linhas = dados.Select(item => new[]
        {
            item.Placa,
            item.Fabricante,
            item.AnoModelo.ToString(CultureInfo.InvariantCulture),
            item.NomeStatus,
            item.TotalReservas.ToString("N0", CultureInfo.GetCultureInfo("pt-BR")),
            item.UltimoEvento?.ToString("dd/MM/yyyy") ?? "—"
        });

        return File(GerarPdf(
            "Completo de Utilização dos Veículos",
            ["Placa", "Fabricante", "Ano Modelo", "Status", "Total Reservas", "Último Evento"],
            linhas), "application/pdf");
    }

    public IActionResult UtilizacaoCompletaVeiculosXlsx()
    {
        var dados = ConsultarUtilizacaoCompletaVeiculos()
            .Select(item => new object?[]
            {
                item.Placa,
                item.Fabricante,
                item.AnoModelo,
                item.NomeStatus,
                item.TotalReservas,
                item.UltimoEvento?.ToDateTime(TimeOnly.MinValue)
            });

        return BaixarXlsx(
            "utilizacao-completa-veiculos.xlsx",
            "Utilização dos veículos",
            ["Placa", "Fabricante", "Ano Modelo", "Status", "Total Reservas", "Último Evento"],
            dados);
    }

    public IActionResult UtilizacaoCompletaVeiculosCsv()
    {
        var dados = ConsultarUtilizacaoCompletaVeiculos()
            .Select(item => new object?[]
            {
                item.Placa,
                item.Fabricante,
                item.AnoModelo,
                item.NomeStatus,
                item.TotalReservas,
                item.UltimoEvento?.ToDateTime(TimeOnly.MinValue)
            });

        return BaixarCsv(
            "utilizacao-completa-veiculos.csv",
            ["Placa", "Fabricante", "Ano Modelo", "Status", "Total Reservas", "Último Evento"],
            dados);
    }

    public IActionResult UtilizacaoCompletaVeiculosTxt()
    {
        var dados = ConsultarUtilizacaoCompletaVeiculos()
            .Select(item => new object?[]
            {
                item.Placa,
                item.Fabricante,
                item.AnoModelo,
                item.NomeStatus,
                item.TotalReservas,
                item.UltimoEvento?.ToDateTime(TimeOnly.MinValue)
            });

        return BaixarTexto(
            "utilizacao-completa-veiculos.txt",
            ["Placa", "Fabricante", "Ano Modelo", "Status", "Total Reservas", "Último Evento"],
            dados);
    }

    private List<QuantidadeFabricanteItem> ConsultarQuantidadeVeiculosPorFabricante()
    {
        return _db.Veiculos.AsNoTracking()
            .GroupBy(item => item.Fabricante)
            .Select(grupo => new QuantidadeFabricanteItem
            {
                Fabricante = grupo.Key,
                Quantidade = grupo.Count()
            })
            .OrderByDescending(item => item.Quantidade)
            .ToList();
    }

    private List<UtilizacaoVeiculoItem> ConsultarUtilizacaoCompletaVeiculos()
    {
        var consulta =
            from veiculo in _db.Veiculos.AsNoTracking()
            join status in _db.StatusFrota.AsNoTracking()
                on veiculo.IdStatusFrota equals status.IdStatusFrota
            join beneficio in _db.VeiculoBeneficios.AsNoTracking()
                on veiculo.IdVeiculo equals beneficio.IdVeiculo into beneficios
            from beneficio in beneficios.DefaultIfEmpty()
            join reserva in _db.ReservaVeiculos.AsNoTracking()
                on (int?)beneficio.IdBeneficiado equals reserva.IdClienteSolicitante into reservas
            from reserva in reservas.DefaultIfEmpty()
            join historico in _db.Historicos.AsNoTracking()
                on (int?)beneficio.IdBeneficiado equals (int?)historico.IdClienteAtendido into historicos
            from historico in historicos.DefaultIfEmpty()
            group new { Reserva = reserva, Historico = historico } by new
            {
                veiculo.Placa,
                veiculo.Fabricante,
                veiculo.AnoModelo,
                status.NomeStatus
            } into grupo
            orderby grupo.Count(item => item.Reserva != null) descending
            select new UtilizacaoVeiculoItem
            {
                Placa = grupo.Key.Placa,
                Fabricante = grupo.Key.Fabricante,
                AnoModelo = grupo.Key.AnoModelo,
                NomeStatus = grupo.Key.NomeStatus,
                TotalReservas = grupo.Count(item => item.Reserva != null),
                UltimoEvento = grupo.Max(item => item.Historico == null ? (DateOnly?)null : item.Historico.DataEvento)
            };

        return consulta.ToList();
    }

    private static byte[] GerarPdf(string titulo, IReadOnlyList<string> cabecalhos, IEnumerable<string[]> linhas)
    {
        return Document.Create(documento =>
        {
            documento.Page(pagina =>
            {
                pagina.Size(PageSizes.A4.Landscape());
                pagina.Margin(24);
                pagina.DefaultTextStyle(estilo => estilo.FontSize(9));
                pagina.Header()
                    .PaddingBottom(12)
                    .Text(titulo)
                    .FontSize(18)
                    .Bold()
                    .FontColor(Colors.Blue.Darken3);
                pagina.Content().Table(tabela =>
                {
                    tabela.ColumnsDefinition(colunas =>
                    {
                        foreach (var _ in cabecalhos)
                        {
                            colunas.RelativeColumn();
                        }
                    });

                    tabela.Header(cabecalho =>
                    {
                        foreach (var item in cabecalhos)
                        {
                            cabecalho.Cell()
                                .Background(Colors.Blue.Darken2)
                                .Padding(6)
                                .Text(item)
                                .FontColor(Colors.White)
                                .Bold();
                        }
                    });

                    foreach (var linha in linhas)
                    {
                        foreach (var item in linha)
                        {
                            tabela.Cell()
                                .BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(5)
                                .Text(item);
                        }
                    }
                });
                pagina.Footer()
                    .AlignCenter()
                    .Text(texto =>
                    {
                        texto.Span("Página ");
                        texto.CurrentPageNumber();
                        texto.Span(" de ");
                        texto.TotalPages();
                    });
            });
        }).GeneratePdf();
    }

    private static IActionResult BaixarXlsx(string nomeArquivo, string nomePlanilha, IReadOnlyList<string> cabecalhos, IEnumerable<object?[]> linhas)
    {
        using var workbook = new XLWorkbook();
        var planilha = workbook.Worksheets.Add(nomePlanilha);

        for (var coluna = 0; coluna < cabecalhos.Count; coluna++)
        {
            planilha.Cell(1, coluna + 1).Value = cabecalhos[coluna];
        }

        var linhaAtual = 2;
        foreach (var linha in linhas)
        {
            for (var coluna = 0; coluna < linha.Length; coluna++)
            {
                var celula = planilha.Cell(linhaAtual, coluna + 1);
                switch (linha[coluna])
                {
                    case null:
                        break;
                    case string texto:
                        celula.Value = texto;
                        break;
                    case int inteiro:
                        celula.Value = inteiro;
                        break;
                    case long inteiroLongo:
                        celula.Value = inteiroLongo;
                        break;
                    case double numero:
                        celula.Value = numero;
                        break;
                    case decimal numeroDecimal:
                        celula.Value = (double)numeroDecimal;
                        break;
                    case DateTime data:
                        celula.Value = data;
                        celula.Style.NumberFormat.Format = "dd/MM/yyyy";
                        break;
                    default:
                        celula.Value = Convert.ToString(linha[coluna], CultureInfo.InvariantCulture) ?? string.Empty;
                        break;
                }
            }

            linhaAtual++;
        }

        var cabecalho = planilha.Range(1, 1, 1, cabecalhos.Count);
        cabecalho.Style.Font.Bold = true;
        cabecalho.Style.Font.FontColor = XLColor.White;
        cabecalho.Style.Fill.BackgroundColor = XLColor.FromHtml("#0B5ED7");
        planilha.SheetView.FreezeRows(1);
        planilha.Columns().AdjustToContents();

        using var memoria = new MemoryStream();
        workbook.SaveAs(memoria);
        return new FileContentResult(memoria.ToArray(), XlsxContentType) { FileDownloadName = nomeArquivo };
    }

    private static IActionResult BaixarCsv(string nomeArquivo, IReadOnlyList<string> cabecalhos, IEnumerable<object?[]> linhas)
    {
        var conteudo = new List<string> { string.Join(",", cabecalhos.Select(EscaparCsv)) };
        conteudo.AddRange(linhas.Select(linha => string.Join(",", linha.Select(valor => EscaparCsv(FormatarCsv(valor))))));
        return new FileContentResult(new UTF8Encoding(true).GetBytes(string.Join(Environment.NewLine, conteudo)), "text/csv; charset=utf-8")
        {
            FileDownloadName = nomeArquivo
        };
    }

    private static IActionResult BaixarTexto(string nomeArquivo, IReadOnlyList<string> cabecalhos, IEnumerable<object?[]> linhas)
    {
        var conteudo = new List<string> { string.Join("\t", cabecalhos) };
        conteudo.AddRange(linhas.Select(linha => string.Join("\t", linha.Select(FormatarTexto))));
        return new FileContentResult(new UTF8Encoding(true).GetBytes(string.Join(Environment.NewLine, conteudo)), "text/plain; charset=utf-8")
        {
            FileDownloadName = nomeArquivo
        };
    }

    private static string FormatarCsv(object? valor)
    {
        return valor switch
        {
            DateTime data => data.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            DateOnly data => data.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            IFormattable formatavel => formatavel.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty,
            _ => valor?.ToString() ?? string.Empty
        };
    }

    private static string FormatarTexto(object? valor)
    {
        var texto = valor switch
        {
            DateTime data => data.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR")),
            DateOnly data => data.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR")),
            IFormattable formatavel => formatavel.ToString(null, CultureInfo.GetCultureInfo("pt-BR")) ?? string.Empty,
            _ => valor?.ToString() ?? string.Empty
        };

        return texto.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ');
    }

    private static string EscaparCsv(string valor)
    {
        return $"\"{valor.Replace("\"", "\"\"")}\"";
    }

    private sealed class QuantidadeFabricanteItem
    {
        public string Fabricante { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }

    private sealed class UtilizacaoVeiculoItem
    {
        public string Placa { get; set; } = string.Empty;
        public string Fabricante { get; set; } = string.Empty;
        public short AnoModelo { get; set; }
        public string NomeStatus { get; set; } = string.Empty;
        public int TotalReservas { get; set; }
        public DateOnly? UltimoEvento { get; set; }
    }
}
