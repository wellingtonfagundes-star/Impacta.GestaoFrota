namespace Impacta.GestaoFrota.Domain.Models;

public class CaracteristicaVeiculoDto
{
    public int IdVeiculo { get; set; }
    public int IdCaracteristica { get; set; }
    public decimal Valor { get; set; }
    public string? Comentario { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? DataUltAlteracao { get; set; }
    public string VeiculoDescricao { get; set; } = string.Empty;
    public string CaracteristicaDescricao { get; set; } = string.Empty;
}
