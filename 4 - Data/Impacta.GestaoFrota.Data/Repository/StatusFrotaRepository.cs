using System.Data;
using System.Linq.Expressions;
using Dapper;
using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class StatusFrotaRepository : Repository<StatusFrota>, IStatusFrotaRepository
{
    public StatusFrotaRepository(FrotaContext context)
        : base(context)
    {
    }

    public override IEnumerable<StatusFrota> ObterTodos()
    {
        const string sql = """
            SELECT id_status_frota AS IdStatusFrota,
                   nome_status AS NomeStatus,
                   descricao AS Descricao
            FROM frota.status_frota
            ORDER BY id_status_frota;
            """;

        return Query(sql);
    }

    public override StatusFrota ObterPorId(int id)
    {
        const string sql = """
            SELECT id_status_frota AS IdStatusFrota,
                   nome_status AS NomeStatus,
                   descricao AS Descricao
            FROM frota.status_frota
            WHERE id_status_frota = @Id;
            """;

        return QuerySingleOrDefault(sql, new { Id = id });
    }

    public override StatusFrota ObterPorId(Guid id)
    {
        return ObterPorId(ConvertGuidToInt(id));
    }

    public override IEnumerable<StatusFrota> ObterTodosPaginado(int size, int reg)
    {
        const string sql = """
            SELECT id_status_frota AS IdStatusFrota,
                   nome_status AS NomeStatus,
                   descricao AS Descricao
            FROM frota.status_frota
            ORDER BY id_status_frota
            OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;
            """;

        return Query(sql, new { Offset = size, Limit = reg });
    }

    public override IEnumerable<StatusFrota> Buscar(Expression<Func<StatusFrota, bool>> predicate)
    {
        return base.Buscar(predicate);
    }

    public override bool Adicionar(StatusFrota obj)
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

    public override bool Atualizar(StatusFrota obj)
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

    private IEnumerable<StatusFrota> Query(string sql, object? parameters = null)
    {
        var connection = db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.Query<StatusFrota>(sql, parameters).AsList();
        }
        finally
        {
            if (shouldClose)
                connection.Close();
        }
    }

    private StatusFrota QuerySingleOrDefault(string sql, object parameters)
    {
        var connection = db.Database.GetDbConnection();
        var shouldClose = connection.State == ConnectionState.Closed;

        try
        {
            if (shouldClose)
                connection.Open();

            return connection.QuerySingleOrDefault<StatusFrota>(sql, parameters);
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
