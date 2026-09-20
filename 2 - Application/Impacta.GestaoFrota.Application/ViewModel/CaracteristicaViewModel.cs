using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class CaracteristicaViewModel
{
    public int IdCaracteristica { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 255 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    public bool Opcional { get; set; }

    [StringLength(500, ErrorMessage = "A observação não pode ter mais de 500 caracteres.")]
    public string? Obs { get; set; }
}
