using Impacta.GestaoFrota.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository
{
    //public interface IRepositoryRead<TEntity> : IDisposable where TEntity : Entity
    public interface IRepositoryRead<TEntity> : IDisposable
    {
        TEntity ObterPorId(Guid id);
        TEntity ObterPorId(int id);
        IEnumerable<TEntity> ObterTodos();
        IEnumerable<TEntity> ObterTodosPaginado(int size, int reg);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
    }
}
