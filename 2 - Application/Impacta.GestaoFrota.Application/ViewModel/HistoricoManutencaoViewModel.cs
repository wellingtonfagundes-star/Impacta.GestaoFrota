using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class HistoricoManutencaoViewModel
{
    public int IdHistorico { get; set; }

    [Required(ErrorMessage = "Selecione um local.")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione um local.")]
    public int IdLocal { get; set; }

    [Required(ErrorMessage = "Selecione um tipo de evento.")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo de evento.")]
    public int IdTipoEvento { get; set; }

    [Required(ErrorMessage = "Selecione o responsável.")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione o responsável.")]
    public int IdFuncionarioResponsavel { get; set; }

    [Required(ErrorMessage = "Selecione um cliente.")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione um cliente.")]
    public int IdClienteAtendido { get; set; }

    [Required(ErrorMessage = "Informe a data do evento.")]
    public DateOnly? DataEvento { get; set; }

    [Required(ErrorMessage = "Informe a descrição do evento.")]
    [StringLength(50, ErrorMessage = "A descrição não pode ter mais de 50 caracteres.")]
    public string DescrEvento { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a quilometragem.")]
    [Range(0, double.MaxValue, ErrorMessage = "A quilometragem deve ser maior ou igual a zero.")]
    public double? Quilometragem { get; set; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "O valor deve ser maior ou igual a zero.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "Informe o número da nota fiscal.")]
    [Range(1, int.MaxValue, ErrorMessage = "Informe um número de nota fiscal válido.")]
    public int? NrNotaFiscal { get; set; }

    public string? Obs { get; set; }

    public string LocalNome { get; set; } = string.Empty;
    public string TipoEventoNome { get; set; } = string.Empty;
    public string FuncionarioResponsavelNome { get; set; } = string.Empty;
    public string ClienteNome { get; set; } = string.Empty;
}
