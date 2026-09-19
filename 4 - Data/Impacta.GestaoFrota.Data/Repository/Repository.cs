using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Impacta.GestaoFrota.Data.Repository
{
    public abstract class Repository<TEntity> : IRepositoryRead<TEntity>, IRepositoryWrite<TEntity> where TEntity : class, new()
    {
        protected FrotaContext db;
        protected DbSet<TEntity> DbSet;

        public Repository(FrotaContext context)
        {
            db = context;
            DbSet = db.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.AsNoTracking<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> ObterTodosPaginado(int size, int reg)
        {
            return DbSet.Take(reg).Skip(size);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public virtual TEntity ObterPorId(int id)
        {
            return DbSet.Find(id);
        }

        public virtual bool Adicionar(TEntity obj)
        {
            try
            {
                var result = DbSet.Add(obj);

                if (result != null)
                {
                    if (SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public virtual bool Atualizar(TEntity obj)
        {
            try
            {
                var entry = db.Entry(obj);
                DbSet.Attach(obj);
                entry.State = EntityState.Modified;

                if (SaveChanges() > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }


        public virtual bool Remover(Guid id)
        {
            try
            {
                var entry = ObterPorId(id);

                DbSet.Remove(entry);
                if (SaveChanges() > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
    }
}
