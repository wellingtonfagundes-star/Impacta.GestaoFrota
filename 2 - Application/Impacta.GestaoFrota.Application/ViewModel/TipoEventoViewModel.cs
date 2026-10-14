using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class TipoEventoViewModel
{
    public int IdTipoevento { get; set; }

    [Required(ErrorMessage = "O nome do tipo de evento é obrigatório.")]
    [StringLength(20, ErrorMessage = "O nome não pode ter mais de 20 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição do tipo de evento é obrigatória.")]
    [StringLength(70, ErrorMessage = "A descrição não pode ter mais de 70 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    public string? InfoAdicional { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A periodicidade deve ser maior que zero.")]
    public int? Periodicidade { get; set; }
}
