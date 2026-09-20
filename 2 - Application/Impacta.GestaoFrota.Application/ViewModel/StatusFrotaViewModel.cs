using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel
{
    public class StatusFrotaViewModel
    {
        public int IdStatusFrota { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string NomeStatus { get; set; } = null!;

        [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
        public string? Descricao { get; set; }
    }
}

