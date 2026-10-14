using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class LocalAssistenciaRepository : Repository<Local>, ILocalAssistenciaRepository
{
    public LocalAssistenciaRepository(FrotaContext context)
        : base(context)
    {
    }

    public bool Remover(int id)
    {
        try
        {
            var local = DbSet.Find(id);
            if (local is null)
                return false;

            DbSet.Remove(local);
            return SaveChanges() > 0;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public override bool Remover(Guid id)
    {
        return Remover(BitConverter.ToInt32(id.ToByteArray(), 0));
    }
}
