namespace Impacta.GestaoFrota.Application.ViewModel;

public class ReservaAprovadaConsultaViewModel
{
    public int IdReserva { get; set; }
    public string Solicitante { get; set; } = string.Empty;
    public string Aprovador { get; set; } = string.Empty;
    public DateOnly? Data { get; set; }
    public double? KmEstimada { get; set; }
}
