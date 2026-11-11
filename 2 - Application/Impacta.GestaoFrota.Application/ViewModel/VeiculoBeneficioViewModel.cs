using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class VeiculoBeneficioViewModel
{
    public int IdBeneficio { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione um veículo.")]
    public int IdVeiculo { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o beneficiado.")]
    public int IdBeneficiado { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o motorista.")]
    public int IdMotorista { get; set; }

    [Required(ErrorMessage = "Informe a data de entrega do veículo.")]
    [DataType(DataType.Date)]
    public DateOnly? DataEntrega { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? DataDevolucao { get; set; }

    public bool UsoFds { get; set; }

    [StringLength(100, ErrorMessage = "O local da vaga não pode ter mais de 100 caracteres.")]
    public string? LocalVaga { get; set; }

    public string VeiculoDescricao { get; set; } = string.Empty;
    public string BeneficiadoNome { get; set; } = string.Empty;
    public string MotoristaNome { get; set; } = string.Empty;
    public string BeneficiadoSelecao { get; set; } = string.Empty;
    public string MotoristaSelecao { get; set; } = string.Empty;
}

public class PessoaOpcaoViewModel
{
    public int IdPessoa { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Rotulo => Nome;
}

public class VeiculoOpcaoViewModel
{
    public int IdVeiculo { get; set; }
    public string Descricao { get; set; } = string.Empty;
}