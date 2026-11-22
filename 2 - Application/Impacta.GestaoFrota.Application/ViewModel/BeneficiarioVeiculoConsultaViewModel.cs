namespace Impacta.GestaoFrota.Application.ViewModel;

public class BeneficiarioVeiculoConsultaViewModel
{
    public int IdBeneficio { get; set; }
    public string Beneficiado { get; set; } = string.Empty;
    public string Motorista { get; set; } = string.Empty;
    public string Placa { get; set; } = string.Empty;
    public DateTime? DataEntrega { get; set; }
    public DateTime? DataDevolucao { get; set; }
}
