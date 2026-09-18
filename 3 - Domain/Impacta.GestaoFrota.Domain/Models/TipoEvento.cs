using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class TipoEvento
{
    public int IdTipoevento { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public string? InfoAdicional { get; set; }

    public int? Periodicidade { get; set; }

    public virtual ICollection<Historico> Historicos { get; set; } = new List<Historico>();
}
