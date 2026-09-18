using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class Locai
{
    public int IdLocal { get; set; }

    public string Nome { get; set; } = null!;

    public string Endereco { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string DescrServico { get; set; } = null!;

    public string NomeContato { get; set; } = null!;

    public virtual ICollection<Historico> Historicos { get; set; } = new List<Historico>();

    public virtual ICollection<ReservaVeiculo> ReservaVeiculos { get; set; } = new List<ReservaVeiculo>();

    public virtual ICollection<RetornoVeiculo> RetornoVeiculos { get; set; } = new List<RetornoVeiculo>();
}
