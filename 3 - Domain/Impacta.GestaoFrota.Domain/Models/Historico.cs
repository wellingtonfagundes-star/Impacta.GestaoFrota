using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class Historico
{
    public int IdHistorico { get; set; }

    public int IdLocal { get; set; }

    public int IdTipoevento { get; set; }

    public int IdFuncionarioResponsavel { get; set; }

    public int IdClienteAtendido { get; set; }

    public DateOnly? DataEvento { get; set; }

    public string DescrEvento { get; set; } = null!;

    public double Quilometragem { get; set; }

    public decimal Valor { get; set; }

    public int NrNotaFiscal { get; set; }

    public string? Obs { get; set; }

    public virtual Pessoa IdClienteAtendidoNavigation { get; set; } = null!;

    public virtual Funcionario IdFuncionarioResponsavelNavigation { get; set; } = null!;

    public virtual Locai IdLocalNavigation { get; set; } = null!;

    public virtual TipoEvento IdTipoeventoNavigation { get; set; } = null!;
}
