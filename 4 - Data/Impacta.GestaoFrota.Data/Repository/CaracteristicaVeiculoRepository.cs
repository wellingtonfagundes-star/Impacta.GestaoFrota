using System.Data;
using System.Linq.Expressions;
using Dapper;
using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class CaracteristicaVeiculoRepository : Repository<CaracteristicaVeiculo>, ICaracteristicaVeiculoRepository
{
    public CaracteristicaVeiculoRepository(FrotaContext context)
        : base(context)
    {
    }

    public override IEnumerable<CaracteristicaVeiculo> ObterTodos()
    {
        const string sql = """
            SELECT cv.id_veiculo AS IdVeiculo,
                   cv.id_caracteristica AS IdCaracteristica,
                   cv.valor AS Valor,
                   cv.comentario AS Comentario,
                   cv.data_criacao AS DataCriacao,
                   cv.data_ult_alteracao AS DataUltAlteracao
            FROM frota.caracteristica_veiculo cv
            ORDER BY cv.id_veiculo, cv.id_caracteristica;
            """;

        return Query(sql);
    }

    public CaracteristicaVeiculo ObterPorIds(int idVeiculo, int idCaracteristica)
    {
        const string sql = """
            SELECT cv.id_veiculo AS IdVeiculo,
                   cv.id_caracteristica AS IdCaracteristica,
                   cv.valor AS Valor,
                   cv.comentario AS Comentario,
                   cv.data_criacao AS DataCriacao,
                   cv.data_ult_alteracao AS DataUltAlteracao
            FROM frota.caracteristica_veiculo cv
            WHERE cv.id_veiculo = @IdVeiculo
              AND cv.id_caracteristica = @IdCaracteristica;
            """;

        return QuerySingleOrDefault(sql, new { IdVeiculo = idVeiculo, IdCaracteristica = idCaracteristica });
    }

    public override IEnumerable<CaracteristicaVeiculo> ObterTodosPaginado(int size, int reg)
    {
        const string sql = """
            SELECT cv.id_veiculo AS IdVeiculo,
                   cv.id_caracteristica AS IdCaracteristica,
                   cv.valor AS Valor,
                   cv.comentario AS Comentario,
                   cv.data_criacao AS DataCriacao,
                   cv.data_ult_alteracao AS DataUltAlteracao
            FROM frota.caracteristica_veiculo cv
            ORDER BY cv.id_veiculo, cv.id_caracteristica
            OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;
            """;

        return Query(sql, new { Offset = size, Limit = reg });
    }

    public override IEnumerable<CaracteristicaVeiculo> Buscar(Expression<Func<CaracteristicaVeiculo, bool>> predicate)
    {
        return base.Buscar(predicate);
    }

    public override bool Adicionar(CaracteristicaVeiculo obj)
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

    public override bool Atualizar(CaracteristicaVeiculo obj)
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

    public bool Remover(int idVeiculo, int idCaracteristica)
    {
        try
        {
            var entity = DbSet.Find(idVeiculo, idCaracteristica);
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
        // Para chave composta, este método não é aplicável
        throw new NotImplementedException("Use Remover(int idVeiculo, int idCaracteristica) para chave composta");
    }

    private IEnumerable<CaracteristicaVeiculo> Query(string sql, object? parameters = null)
    {
        var connection = db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.Query<CaracteristicaVeiculo>(sql, parameters).AsList();
        }
        finally
        {
            if (shouldClose)
                connection.Close();
        }
    }

    private CaracteristicaVeiculo QuerySingleOrDefault(string sql, object parameters)
    {
        var connection = db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.QuerySingleOrDefault<CaracteristicaVeiculo>(sql, parameters);
        }
        finally
        {
            if (shouldClose)
                connection.Close();
        }
    }
}
