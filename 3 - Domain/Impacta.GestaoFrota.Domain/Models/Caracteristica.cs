using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class Caracteristica
{
    public int IdCaracteristica { get; set; }

    public string Descricao { get; set; } = null!;

    public bool? Opcional { get; set; }

    public string? Obs { get; set; }

    public virtual ICollection<CaracteristicaVeiculo> CaracteristicaVeiculos { get; set; } = new List<CaracteristicaVeiculo>();
}
