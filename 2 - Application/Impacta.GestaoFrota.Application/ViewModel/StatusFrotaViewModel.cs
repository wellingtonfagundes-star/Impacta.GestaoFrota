namespace Impacta.GestaoFrota.Application.ViewModel
{
    public class StatusFrotaViewModel
    {
        public int IdStatusFrota { get; set; }

        public string NomeStatus { get; set; } = null!;

        public string? Descricao { get; set; }
    }
}
