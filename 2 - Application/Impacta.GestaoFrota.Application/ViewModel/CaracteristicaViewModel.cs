namespace Impacta.GestaoFrota.Application.ViewModel;

public class CaracteristicaViewModel
{
    public int IdCaracteristica { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public bool? Opcional { get; set; }

    public string? Obs { get; set; }
}
