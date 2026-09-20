using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class StatusFrota
{
    public int IdStatusFrota { get; set; }

    public string NomeStatus { get; set; } = null!;

    public string? Descricao { get; set; }

    public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}
