using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class VeiculoViewModel
{
    public int IdVeiculo { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [StringLength(10, ErrorMessage = "A placa não pode ter mais de 10 caracteres.")]
    public string Placa { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(15, ErrorMessage = "O RENAVAM não pode ter mais de 15 caracteres.")]
    public string? Renavam { get; set; }

    [StringLength(25, ErrorMessage = "O chassi não pode ter mais de 25 caracteres.")]
    public string? Chassi { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [StringLength(70, ErrorMessage = "O fabricante não pode ter mais de 70 caracteres.")]
    public string Fabricante { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [Range(1900, 2100, ErrorMessage = "O ano modelo deve estar entre 1900 e 2100.")]
    public short AnoModelo { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [StringLength(20, ErrorMessage = "A identificação não pode ter mais de 20 caracteres.")]
    public string NrIdentificacao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [StringLength(30, ErrorMessage = "A cor não pode ter mais de 30 caracteres.")]
    public string Cor { get; set; } = string.Empty;

    public DateOnly? DataCompra { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quilometragem deve ser um valor positivo.")]
    public int KmAtual { get; set; }

    [StringLength(30, ErrorMessage = "A seguradora não pode ter mais de 30 caracteres.")]
    public string? Seguradora { get; set; }

    public int? NrApoliceSeguro { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [StringLength(12, ErrorMessage = "O tipo de habilitação não pode ter mais de 12 caracteres.")]
    public string TipoHabilitacao { get; set; } = string.Empty;

    public string? InfoRodizio { get; set; }

    public int? NrAtivo { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    public int IdStatusFrota { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [StringLength(3, ErrorMessage = "O tipo de veículo não pode ter mais de 3 caracteres.")]
    public string TipoVeiculo { get; set; } = string.Empty;
}
