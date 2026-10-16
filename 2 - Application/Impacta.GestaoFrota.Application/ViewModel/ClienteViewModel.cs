using System.ComponentModel.DataAnnotations;
using Impacta.GestaoFrota.Application.Validation;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class ClienteViewModel
{
    public int IdPessoa { get; set; }

    [Required(ErrorMessage = "Informe o nome do cliente.")]
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

    [Required(ErrorMessage = "Selecione o estado.")]
    [RegularExpression("^(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)$", ErrorMessage = "Selecione uma sigla de estado válida.")]
    public string Estado { get; set; } = string.Empty;

    [StringLength(15, ErrorMessage = "O CEP não pode ter mais de 15 caracteres.")]
    public string? Cep { get; set; }

    [Required(ErrorMessage = "Informe o sexo.")]
    [RegularExpression("^[MF]$", ErrorMessage = "Selecione Masculino ou Feminino.")]
    public string Sexo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione Pessoa Física ou Pessoa Jurídica.")]
    [RegularExpression("^(PF|PJ)$", ErrorMessage = "Selecione Pessoa Física ou Pessoa Jurídica.")]
    public string TipoCliente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o CPF ou CNPJ.")]
    [DocumentoClienteValido]
    public string Documento { get; set; } = string.Empty;
}
