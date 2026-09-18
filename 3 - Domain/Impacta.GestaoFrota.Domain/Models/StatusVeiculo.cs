using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class StatusVeiculo
{
    public int IdStatus { get; set; }

    public double? KmEntregue { get; set; }

    public string Combustivel { get; set; } = null!;

    public string Lataria { get; set; } = null!;

    public string Pneus { get; set; } = null!;

    public string? ObsGeral { get; set; }

    public virtual ICollection<RetornoVeiculo> RetornoVeiculos { get; set; } = new List<RetornoVeiculo>();
}
