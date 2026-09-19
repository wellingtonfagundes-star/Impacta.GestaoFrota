using System.Data;
using System.Linq.Expressions;
using Dapper;
using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class VeiculoRepository : Repository<Veiculo>, IVeiculoRepository
{
    public VeiculoRepository(FrotaContext context)
        : base(context)
    {
    }

    public override IEnumerable<Veiculo> ObterTodos()
    {
        const string sql = """
            SELECT id_veiculo AS IdVeiculo,
                   placa AS Placa,
                   renavam AS Renavam,
                   chassi AS Chassi,
                   fabricante AS Fabricante,
                   ano_modelo AS AnoModelo,
                   nr_identificacao AS NrIdentificacao,
                   cor AS Cor,
                   data_compra AS DataCompra,
                   km_atual AS KmAtual,
                   seguradora AS Seguradora,
                   nr_apolice_seguro AS NrApoliceSeguro,
                   tipo_habilitacao AS TipoHabilitacao,
                   info_rodizio AS InfoRodizio,
                   nr_ativo AS NrAtivo,
                   id_status_frota AS IdStatusFrota,
                   tipo_veiculo AS TipoVeiculo
            FROM frota.veiculos
            ORDER BY id_veiculo;
            """;

        return Query(sql);
    }

    public override Veiculo ObterPorId(int id)
    {
        const string sql = """
            SELECT id_veiculo AS IdVeiculo,
                   placa AS Placa,
                   renavam AS Renavam,
                   chassi AS Chassi,
                   fabricante AS Fabricante,
                   ano_modelo AS AnoModelo,
                   nr_identificacao AS NrIdentificacao,
                   cor AS Cor,
                   data_compra AS DataCompra,
                   km_atual AS KmAtual,
                   seguradora AS Seguradora,
                   nr_apolice_seguro AS NrApoliceSeguro,
                   tipo_habilitacao AS TipoHabilitacao,
                   info_rodizio AS InfoRodizio,
                   nr_ativo AS NrAtivo,
                   id_status_frota AS IdStatusFrota,
                   tipo_veiculo AS TipoVeiculo
            FROM frota.veiculos
            WHERE id_veiculo = @Id;
            """;

        return QuerySingleOrDefault(sql, new { Id = id });
    }

    public override Veiculo ObterPorId(Guid id)
    {
        return ObterPorId(ConvertGuidToInt(id));
    }

    public override IEnumerable<Veiculo> ObterTodosPaginado(int size, int reg)
    {
        const string sql = """
            SELECT id_veiculo AS IdVeiculo,
                   placa AS Placa,
                   renavam AS Renavam,
                   chassi AS Chassi,
                   fabricante AS Fabricante,
                   ano_modelo AS AnoModelo,
                   nr_identificacao AS NrIdentificacao,
                   cor AS Cor,
                   data_compra AS DataCompra,
                   km_atual AS KmAtual,
                   seguradora AS Seguradora,
                   nr_apolice_seguro AS NrApoliceSeguro,
                   tipo_habilitacao AS TipoHabilitacao,
                   info_rodizio AS InfoRodizio,
                   nr_ativo AS NrAtivo,
                   id_status_frota AS IdStatusFrota,
                   tipo_veiculo AS TipoVeiculo
            FROM frota.veiculos
            ORDER BY id_veiculo
            OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;
            """;

        return Query(sql, new { Offset = size, Limit = reg });
    }

    public override IEnumerable<Veiculo> Buscar(Expression<Func<Veiculo, bool>> predicate)
    {
        return base.Buscar(predicate);
    }

    public override bool Adicionar(Veiculo obj)
    {
        try
        {
            DbSet.Add(obj);
            return SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public override bool Atualizar(Veiculo obj)
    {
        try
        {
            DbSet.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
            return SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool Remover(int id)
    {
        try
        {
            var entity = DbSet.Find(id);
            if (entity is null)
                return false;

            DbSet.Remove(entity);
            return SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public override bool Remover(Guid id)
    {
        return Remover(ConvertGuidToInt(id));
    }

    private IEnumerable<Veiculo> Query(string sql, object? parameters = null)
    {
        var connection = db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.Query<Veiculo>(sql, parameters).AsList();
        }
        finally
        {
            if (shouldClose)
                connection.Close();
        }
    }

    private Veiculo QuerySingleOrDefault(string sql, object parameters)
    {
        var connection = db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.QuerySingleOrDefault<Veiculo>(sql, parameters);
        }
        finally
        {
            if (shouldClose)
                connection.Close();
        }
    }

    private static int ConvertGuidToInt(Guid id)
    {
        return BitConverter.ToInt32(id.ToByteArray(), 0);
    }
}
