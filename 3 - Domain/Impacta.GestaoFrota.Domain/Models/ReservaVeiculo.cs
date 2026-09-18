using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class ReservaVeiculo
{
    public int IdReserva { get; set; }

    public int? IdLocalDestino { get; set; }

    public int? IdClienteSolicitante { get; set; }

    public DateOnly? Data { get; set; }

    public int? IdFuncionarioAprovador { get; set; }

    public bool? Aprovado { get; set; }

    public string? Motivo { get; set; }

    public double? KmEstimada { get; set; }

    public bool? Estacionamento { get; set; }

    public bool? Capacete { get; set; }

    public bool? Gps { get; set; }

    public bool? EqpSeguranca { get; set; }

    public string? Preferencia { get; set; }

    public string? ObsGeral { get; set; }

    public bool? Emergencia { get; set; }

    public virtual Pessoa? IdClienteSolicitanteNavigation { get; set; }

    public virtual Funcionario? IdFuncionarioAprovadorNavigation { get; set; }

    public virtual Locai? IdLocalDestinoNavigation { get; set; }

    public virtual ICollection<RetornoVeiculo> RetornoVeiculos { get; set; } = new List<RetornoVeiculo>();
}
