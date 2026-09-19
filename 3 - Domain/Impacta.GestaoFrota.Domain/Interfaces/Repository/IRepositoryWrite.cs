using Impacta.GestaoFrota.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository
{
    public interface IRepositoryWrite<TEntity> : IDisposable
    {
        bool Adicionar(TEntity obj);
        bool Atualizar(TEntity obj);
        bool Remover(Guid id);
        int SaveChanges();
    }
}
