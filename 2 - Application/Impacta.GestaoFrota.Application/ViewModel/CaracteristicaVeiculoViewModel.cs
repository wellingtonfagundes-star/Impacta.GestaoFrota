using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class CaracteristicaVeiculoViewModel
{
    [Required(ErrorMessage = "Este campo é obrigatório.")]
    public int IdVeiculo { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    public int IdCaracteristica { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [StringLength(500, ErrorMessage = "O comentário não pode ter mais de 500 caracteres.")]
    public string? Comentario { get; set; }

    public DateOnly? DataCriacao { get; set; }

    public DateOnly? DataUltAlteracao { get; set; }

    // Para exibição no dropdown
    public string VeiculoDescricao { get; set; } = string.Empty;

    public string CaracteristicaDescricao { get; set; } = string.Empty;
}
