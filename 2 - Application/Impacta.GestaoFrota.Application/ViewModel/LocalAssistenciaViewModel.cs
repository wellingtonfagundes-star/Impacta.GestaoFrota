using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class LocalAssistenciaViewModel
{
    public int IdLocal { get; set; }

    [Required(ErrorMessage = "Informe o nome do local.")]
    [StringLength(70, ErrorMessage = "O nome não pode ter mais de 70 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o endereço.")]
    [StringLength(150, ErrorMessage = "O endereço não pode ter mais de 150 caracteres.")]
    public string Endereco { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o telefone.")]
    [StringLength(15, ErrorMessage = "O telefone não pode ter mais de 15 caracteres.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe os serviços oferecidos.")]
    public string DescrServico { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o nome do contato.")]
    [StringLength(37, ErrorMessage = "O nome do contato não pode ter mais de 37 caracteres.")]
    public string NomeContato { get; set; } = string.Empty;
}
