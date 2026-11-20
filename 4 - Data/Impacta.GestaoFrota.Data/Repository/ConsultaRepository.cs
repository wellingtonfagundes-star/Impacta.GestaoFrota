using System.Data;
using Dapper;
using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class ConsultaRepository : IConsultaRepository
{
    private readonly FrotaContext _db;

    public ConsultaRepository(FrotaContext db)
    {
        _db = db;
    }

    public IEnumerable<VeiculoDisponivelConsulta> ObterVeiculosDisponiveis()
    {
        return _db.Veiculos.AsNoTracking()
            .Where(item => item.IdStatusFrotaNavigation.NomeStatus == "DISPONIVEL")
            .OrderBy(item => item.Placa)
            .Select(item => new VeiculoDisponivelConsulta
            {
                IdVeiculo = item.IdVeiculo,
                Placa = item.Placa,
                Fabricante = item.Fabricante,
                AnoModelo = item.AnoModelo
            })
            .ToList();
    }

    public IEnumerable<VeiculoEmManutencaoConsulta> ObterVeiculosEmManutencao()
    {
        const string sql = """
            SELECT v.placa AS Placa,
                   v.fabricante AS Fabricante,
                   v.ano_modelo AS AnoModelo,
                   sf.nome_status AS NomeStatus
            FROM frota.veiculos AS v
            INNER JOIN frota.status_frota AS sf
                ON sf.id_status_frota = v.id_status_frota
            WHERE sf.nome_status = 'MANUTENCAO'
            ORDER BY v.placa;
            """;

        return Query<VeiculoEmManutencaoConsulta>(sql);
    }

    public IEnumerable<ReservaAprovadaConsulta> ObterReservasAprovadas()
    {
        return (from reserva in _db.ReservaVeiculos.AsNoTracking()
                join solicitante in _db.Pessoas.AsNoTracking()
                    on reserva.IdClienteSolicitante equals (int?)solicitante.IdPessoa
                join aprovador in _db.Funcionarios.AsNoTracking()
                    on reserva.IdFuncionarioAprovador equals (int?)aprovador.IdFuncionario
                where reserva.Aprovado == true
                orderby reserva.Data descending, reserva.IdReserva
                select new ReservaAprovadaConsulta
                {
                    IdReserva = reserva.IdReserva,
                    Solicitante = solicitante.Nome,
                    Aprovador = aprovador.Nome,
                    Data = reserva.Data,
                    KmEstimada = reserva.KmEstimada
                })
            .ToList();
    }

    public QuilometragemAtualFrotaConsulta ObterQuilometragemAtualFrota()
    {
        var veiculos = _db.Veiculos.AsNoTracking();
        return new QuilometragemAtualFrotaConsulta
        {
            KmTotalFrota = veiculos.Sum(item => (long?)item.KmAtual) ?? 0,
            KmMediaFrota = veiculos.Average(item => (double?)item.KmAtual) ?? 0
        };
    }

    public IEnumerable<ValorTotalPorTipoEventoConsulta> ObterValorTotalPorTipoEvento()
    {
        return (from historico in _db.Historicos.AsNoTracking()
                join tipoEvento in _db.TipoEventos.AsNoTracking()
                    on historico.IdTipoevento equals tipoEvento.IdTipoevento
                group historico by tipoEvento.Nome into grupo
                orderby grupo.Sum(item => item.Valor) descending
                select new ValorTotalPorTipoEventoConsulta
                {
                    Nome = grupo.Key,
                    TotalGasto = grupo.Sum(item => item.Valor)
                })
            .ToList();
    }

    public IEnumerable<BeneficiarioVeiculoConsulta> ObterBeneficiariosVeiculos()
    {
        const string sql = """
            SELECT beneficio.id_beneficio AS IdBeneficio,
                   beneficiado.nome AS Beneficiado,
                   motorista.nome AS Motorista,
                   veiculo.placa AS Placa,
                   beneficio.data_entrega AS DataEntrega,
                   beneficio.data_devolucao AS DataDevolucao
            FROM frota.veiculo_beneficio AS beneficio
            INNER JOIN frota.pessoa AS beneficiado
                ON beneficiado.id_pessoa = beneficio.id_beneficiado
            INNER JOIN frota.pessoa AS motorista
                ON motorista.id_pessoa = beneficio.id_motorista
            INNER JOIN frota.veiculos AS veiculo
                ON veiculo.id_veiculo = beneficio.id_veiculo
            ORDER BY beneficio.id_beneficio;
            """;

        return Query<BeneficiarioVeiculoConsulta>(sql);
    }

    public IEnumerable<RankingClienteReservasConsulta> ObterRankingClientesComMaisReservas()
    {
        return (from reserva in _db.ReservaVeiculos.AsNoTracking()
                join pessoa in _db.Pessoas.AsNoTracking()
                    on reserva.IdClienteSolicitante equals (int?)pessoa.IdPessoa
                group reserva by pessoa.Nome into grupo
                orderby grupo.Count() descending
                select new RankingClienteReservasConsulta
                {
                    Nome = grupo.Key,
                    TotalReservas = grupo.Count()
                })
            .ToList();
    }

    public DashboardGerencialFrotaConsulta ObterIndicadoresDashboardFrota()
    {
        const string sql = """
            SELECT COUNT(*)::integer AS TotalVeiculos,
                   COUNT(*) FILTER (WHERE sf.nome_status = 'DISPONIVEL')::integer AS Disponiveis,
                   COUNT(*) FILTER (WHERE sf.nome_status = 'RESERVADO')::integer AS Reservados,
                   COUNT(*) FILTER (WHERE sf.nome_status = 'MANUTENCAO')::integer AS Manutencao
            FROM frota.veiculos AS veiculo
            INNER JOIN frota.status_frota AS sf
                ON sf.id_status_frota = veiculo.id_status_frota;
            """;

        return QuerySingle<DashboardGerencialFrotaConsulta>(sql);
    }

    public IEnumerable<QuantidadeVeiculosFabricanteRelatorio> ObterQuantidadeVeiculosPorFabricante()
    {
        return _db.Veiculos.AsNoTracking()
            .GroupBy(item => item.Fabricante)
            .Select(grupo => new QuantidadeVeiculosFabricanteRelatorio
            {
                Fabricante = grupo.Key,
                Quantidade = grupo.Count()
            })
            .OrderByDescending(item => item.Quantidade)
            .ToList();
    }

    public IEnumerable<UtilizacaoCompletaVeiculosRelatorio> ObterUtilizacaoCompletaVeiculos()
    {
        var consulta =
            from veiculo in _db.Veiculos.AsNoTracking()
            join status in _db.StatusFrota.AsNoTracking()
                on veiculo.IdStatusFrota equals status.IdStatusFrota
            join beneficio in _db.VeiculoBeneficios.AsNoTracking()
                on veiculo.IdVeiculo equals beneficio.IdVeiculo into beneficios
            from beneficio in beneficios.DefaultIfEmpty()
            join reserva in _db.ReservaVeiculos.AsNoTracking()
                on (int?)beneficio.IdBeneficiado equals reserva.IdClienteSolicitante into reservas
            from reserva in reservas.DefaultIfEmpty()
            join historico in _db.Historicos.AsNoTracking()
                on (int?)beneficio.IdBeneficiado equals (int?)historico.IdClienteAtendido into historicos
            from historico in historicos.DefaultIfEmpty()
            group new { Reserva = reserva, Historico = historico } by new
            {
                veiculo.Placa,
                veiculo.Fabricante,
                veiculo.AnoModelo,
                status.NomeStatus
            } into grupo
            orderby grupo.Count(item => item.Reserva != null) descending
            select new UtilizacaoCompletaVeiculosRelatorio
            {
                Placa = grupo.Key.Placa,
                Fabricante = grupo.Key.Fabricante,
                AnoModelo = grupo.Key.AnoModelo,
                NomeStatus = grupo.Key.NomeStatus,
                TotalReservas = grupo.Count(item => item.Reserva != null),
                UltimoEvento = grupo.Max(item => item.Historico == null ? (DateOnly?)null : item.Historico.DataEvento)
            };

        return consulta.ToList();
    }

    private IEnumerable<T> Query<T>(string sql)
    {
        var connection = _db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.Query<T>(sql).AsList();
        }
        finally
        {
            if (shouldClose)
                connection.Close();
        }
    }

    private T QuerySingle<T>(string sql)
    {
        var connection = _db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.QuerySingle<T>(sql);
        }
        finally
        {
            if (shouldClose)
                connection.Close();
        }
    }
}
