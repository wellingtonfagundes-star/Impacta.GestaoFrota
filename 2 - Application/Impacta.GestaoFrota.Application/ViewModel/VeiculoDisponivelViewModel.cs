namespace Impacta.GestaoFrota.Application.ViewModel;

public class VeiculoDisponivelViewModel
{
    public int IdVeiculo { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public short AnoModelo { get; set; }
}
