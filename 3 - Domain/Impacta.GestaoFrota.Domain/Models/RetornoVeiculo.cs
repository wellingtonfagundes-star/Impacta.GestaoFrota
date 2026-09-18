using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class RetornoVeiculo
{
    public int IdRetorno { get; set; }

    public int IdVeiculo { get; set; }

    public int IdLocalRetorno { get; set; }

    public int IdReserva { get; set; }

    public int IdStatus { get; set; }

    public DateOnly? DataRetirada { get; set; }

    public DateOnly? DataRetorno { get; set; }

    public decimal? KmRetorno { get; set; }

    public virtual Locai IdLocalRetornoNavigation { get; set; } = null!;

    public virtual ReservaVeiculo IdReservaNavigation { get; set; } = null!;

    public virtual StatusVeiculo IdStatusNavigation { get; set; } = null!;

    public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
}
