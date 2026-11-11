using System.ComponentModel.DataAnnotations;

namespace Impacta.GestaoFrota.Application.ViewModel;

public class ReservaVeiculoViewModel
{
    public int IdReserva { get; set; }
    public int? IdRetorno { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione um veículo.")]
    public int IdVeiculo { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o local de destino.")]
    public int IdLocalDestino { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o solicitante.")]
    public int IdClienteSolicitante { get; set; }

    [Required(ErrorMessage = "Informe a data da reserva.")]
    public DateOnly? Data { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o aprovador.")]
    public int IdFuncionarioAprovador { get; set; }

    public bool Aprovado { get; set; }
    public string? Motivo { get; set; }

    [Required(ErrorMessage = "Informe a quilometragem estimada.")]
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "A quilometragem estimada deve ser maior ou igual a zero.")]
    public decimal? KmEstimada { get; set; }

    public bool Estacionamento { get; set; }
    public bool Capacete { get; set; }
    public bool Gps { get; set; }
    public bool EqpSeguranca { get; set; }
    public bool Emergencia { get; set; }
    public string? ObsGeral { get; set; }

    public int? IdLocalRetorno { get; set; }
    public DateOnly? DataRetorno { get; set; }
    public decimal? KmEntregue { get; set; }
    public string? Combustivel { get; set; }
    public string? Lataria { get; set; }
    public string? Pneus { get; set; }
    public string? ObsRetorno { get; set; }

    public string VeiculoNome { get; set; } = string.Empty;
    public string VeiculoPlaca { get; set; } = string.Empty;
    public string ClienteNome { get; set; } = string.Empty;
    public string LocalDestinoNome { get; set; } = string.Empty;
    public string LocalRetornoNome { get; set; } = string.Empty;
    public decimal? KmRetorno { get; set; }
}
