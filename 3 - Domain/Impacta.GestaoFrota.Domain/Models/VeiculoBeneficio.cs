using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class VeiculoBeneficio
{
    public int IdBeneficio { get; set; }

    public int IdVeiculo { get; set; }

    public DateOnly? DataEntrega { get; set; }

    public DateOnly? DataDevolucao { get; set; }

    public int IdBeneficiado { get; set; }

    public bool? UsoFds { get; set; }

    public int IdMotorista { get; set; }

    public string? LocalVaga { get; set; }

    public virtual Pessoa IdBeneficiadoNavigation { get; set; } = null!;

    public virtual Pessoa IdMotoristaNavigation { get; set; } = null!;

    public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
}
