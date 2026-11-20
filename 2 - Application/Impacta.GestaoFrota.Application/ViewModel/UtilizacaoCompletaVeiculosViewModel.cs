namespace Impacta.GestaoFrota.Application.ViewModel;

public class UtilizacaoCompletaVeiculosViewModel
{
    public string Placa { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public short AnoModelo { get; set; }
    public string NomeStatus { get; set; } = string.Empty;
    public int TotalReservas { get; set; }
    public DateOnly? UltimoEvento { get; set; }
}
