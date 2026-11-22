using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class CaracteristicaVeiculo
{
    public int IdVeiculo { get; set; }

    public int IdCaracteristica { get; set; }

    public decimal Valor { get; set; }

    public string? Comentario { get; set; }

    public DateTime? DataCriacao { get; set; }

    public DateTime? DataUltAlteracao { get; set; }

    public virtual Caracteristica IdCaracteristicaNavigation { get; set; } = null!;

    public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
}
