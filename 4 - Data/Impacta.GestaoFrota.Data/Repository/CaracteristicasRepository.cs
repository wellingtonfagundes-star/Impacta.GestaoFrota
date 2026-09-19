using System.Data;
using System.Linq.Expressions;
using Dapper;
using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class CaracteristicasRepository : Repository<Caracteristica>, ICaracteristicaRepository
{
    public CaracteristicasRepository(FrotaContext context)
        : base(context)
    {
    }

    public override IEnumerable<Caracteristica> ObterTodos()
    {
        const string sql = """
            SELECT id_caracteristica AS IdCaracteristica,
                   descricao AS Descricao,
                   opcional AS Opcional,
                   obs AS Obs
            FROM frota.caracteristicas
            ORDER BY id_caracteristica;
            """;

        return Query(sql);
    }

    public override Caracteristica ObterPorId(int id)
    {
        const string sql = """
            SELECT id_caracteristica AS IdCaracteristica,
                   descricao AS Descricao,
                   opcional AS Opcional,
                   obs AS Obs
            FROM frota.caracteristicas
            WHERE id_caracteristica = @Id;
            """;

        return QuerySingleOrDefault(sql, new { Id = id });
    }

    public override Caracteristica ObterPorId(Guid id)
    {
        return ObterPorId(ConvertGuidToInt(id));
    }

    public override IEnumerable<Caracteristica> ObterTodosPaginado(int size, int reg)
    {
        const string sql = """
            SELECT id_caracteristica AS IdCaracteristica,
                   descricao AS Descricao,
                   opcional AS Opcional,
                   obs AS Obs
            FROM frota.caracteristicas
            ORDER BY id_caracteristica
            OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;
            """;

        return Query(sql, new { Offset = size, Limit = reg });
    }

    public override IEnumerable<Caracteristica> Buscar(Expression<Func<Caracteristica, bool>> predicate)
    {
        return base.Buscar(predicate);
    }

    public override bool Adicionar(Caracteristica obj)
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

    public override bool Atualizar(Caracteristica obj)
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

    private IEnumerable<Caracteristica> Query(string sql, object? parameters = null)
    {
        var connection = db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.Query<Caracteristica>(sql, parameters).AsList();
        }
        finally
        {
            if (shouldClose)
                connection.Close();
        }
    }

    private Caracteristica QuerySingleOrDefault(string sql, object parameters)
    {
        var connection = db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.QuerySingleOrDefault<Caracteristica>(sql, parameters);
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
