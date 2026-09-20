namespace Impacta.GestaoFrota.Application.ViewModel;

public class CaracteristicaVeiculoViewModel
{
    public int IdVeiculo { get; set; }

    public int IdCaracteristica { get; set; }

    public decimal Valor { get; set; }

    public string? Comentario { get; set; }

    public DateOnly? DataCriacao { get; set; }

    public DateOnly? DataUltAlteracao { get; set; }

    // Para exibição no dropdown
    public string VeiculoDescricao { get; set; } = string.Empty;

    public string CaracteristicaDescricao { get; set; } = string.Empty;
}
