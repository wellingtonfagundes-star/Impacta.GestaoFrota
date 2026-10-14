using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impacta.GestaoFrota.Data.Repository
{
    public class TipoEventoRepository : Repository<TipoEvento>, ITipoEventoRepository
    {
        public TipoEventoRepository(FrotaContext context)
            :base(context)
        {
            
        }

        public bool Remover(int id)
        {
            try
            {
                var tipoEvento = DbSet.Find(id);
                if (tipoEvento is null)
                    return false;

                DbSet.Remove(tipoEvento);
                return SaveChanges() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
