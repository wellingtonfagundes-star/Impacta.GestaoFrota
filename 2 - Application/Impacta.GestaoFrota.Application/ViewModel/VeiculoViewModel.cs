namespace Impacta.GestaoFrota.Application.ViewModel;

public class VeiculoViewModel
{
    public int IdVeiculo { get; set; }

    public string Placa { get; set; } = string.Empty;

    public string? Renavam { get; set; }

    public string? Chassi { get; set; }

    public string Fabricante { get; set; } = string.Empty;

    public short AnoModelo { get; set; }

    public string NrIdentificacao { get; set; } = string.Empty;

    public string Cor { get; set; } = string.Empty;

    public DateOnly? DataCompra { get; set; }

    public int KmAtual { get; set; }

    public string? Seguradora { get; set; }

    public int? NrApoliceSeguro { get; set; }

    public string TipoHabilitacao { get; set; } = string.Empty;

    public string? InfoRodizio { get; set; }

    public int? NrAtivo { get; set; }

    public int IdStatusFrota { get; set; }

    public string TipoVeiculo { get; set; } = string.Empty;
}
