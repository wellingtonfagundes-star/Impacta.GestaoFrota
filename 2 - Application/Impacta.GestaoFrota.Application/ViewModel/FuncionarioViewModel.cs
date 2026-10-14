using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class FuncionarioViewModel
{
    public int IdFuncionario { get; set; }

    public string Matricula { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o nome do funcionário.")]
    [StringLength(70, ErrorMessage = "O nome não pode ter mais de 70 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o endereço.")]
    [StringLength(150, ErrorMessage = "O endereço não pode ter mais de 150 caracteres.")]
    public string Endereco { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o bairro.")]
    [StringLength(70, ErrorMessage = "O bairro não pode ter mais de 70 caracteres.")]
    public string Bairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a cidade.")]
    [StringLength(70, ErrorMessage = "A cidade não pode ter mais de 70 caracteres.")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o estado.")]
    [StringLength(70, ErrorMessage = "O estado não pode ter mais de 70 caracteres.")]
    public string Estado { get; set; } = string.Empty;

    [StringLength(15, ErrorMessage = "O CEP não pode ter mais de 15 caracteres.")]
    public string? Cep { get; set; }

    [Required(ErrorMessage = "Informe o sexo.")]
    [StringLength(1, MinimumLength = 1, ErrorMessage = "Informe apenas um caractere.")]
    public string Sexo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o CPF.")]
    [RegularExpression("^\\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 números, sem pontuação.")]
    public string Cpf { get; set; } = string.Empty;
}
