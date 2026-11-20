namespace Impacta.GestaoFrota.Domain.Models;

public class VeiculoDisponivelConsulta
{
    public int IdVeiculo { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public short AnoModelo { get; set; }
}

public class VeiculoEmManutencaoConsulta
{
    public string Placa { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public short AnoModelo { get; set; }
    public string NomeStatus { get; set; } = string.Empty;
}

public class ReservaAprovadaConsulta
{
    public int IdReserva { get; set; }
    public string Solicitante { get; set; } = string.Empty;
    public string Aprovador { get; set; } = string.Empty;
    public DateOnly? Data { get; set; }
    public double? KmEstimada { get; set; }
}

public class QuilometragemAtualFrotaConsulta
{
    public long KmTotalFrota { get; set; }
    public double KmMediaFrota { get; set; }
}

public class ValorTotalPorTipoEventoConsulta
{
    public string Nome { get; set; } = string.Empty;
    public decimal TotalGasto { get; set; }
}

public class BeneficiarioVeiculoConsulta
{
    public int IdBeneficio { get; set; }
    public string Beneficiado { get; set; } = string.Empty;
    public string Motorista { get; set; } = string.Empty;
    public string Placa { get; set; } = string.Empty;
    public DateOnly? DataEntrega { get; set; }
    public DateOnly? DataDevolucao { get; set; }
}

public class RankingClienteReservasConsulta
{
    public string Nome { get; set; } = string.Empty;
    public int TotalReservas { get; set; }
}

public class DashboardGerencialFrotaConsulta
{
    public int TotalVeiculos { get; set; }
    public int Disponiveis { get; set; }
    public int Reservados { get; set; }
    public int Manutencao { get; set; }
}

public class QuantidadeVeiculosFabricanteRelatorio
{
    public string Fabricante { get; set; } = string.Empty;
    public int Quantidade { get; set; }
}

public class UtilizacaoCompletaVeiculosRelatorio
{
    public string Placa { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public short AnoModelo { get; set; }
    public string NomeStatus { get; set; } = string.Empty;
    public int TotalReservas { get; set; }
    public DateOnly? UltimoEvento { get; set; }
}
