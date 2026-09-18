using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class Veiculo
{
    public int IdVeiculo { get; set; }

    public string Placa { get; set; } = null!;

    public string? Renavam { get; set; }

    public string? Chassi { get; set; }

    public string Fabricante { get; set; } = null!;

    public short AnoModelo { get; set; }

    public string NrIdentificacao { get; set; } = null!;

    public string Cor { get; set; } = null!;

    public DateOnly? DataCompra { get; set; }

    public int KmAtual { get; set; }

    public string? Seguradora { get; set; }

    public int? NrApoliceSeguro { get; set; }

    public string TipoHabilitacao { get; set; } = null!;

    public string? InfoRodizio { get; set; }

    public int? NrAtivo { get; set; }

    public int IdStatusFrota { get; set; }

    public string TipoVeiculo { get; set; } = null!;

    public virtual ICollection<CaracteristicaVeiculo> CaracteristicaVeiculos { get; set; } = new List<CaracteristicaVeiculo>();

    public virtual StatusFrotum IdStatusFrotaNavigation { get; set; } = null!;

    public virtual ICollection<RetornoVeiculo> RetornoVeiculos { get; set; } = new List<RetornoVeiculo>();

    public virtual ICollection<VeiculoBeneficio> VeiculoBeneficios { get; set; } = new List<VeiculoBeneficio>();
}
