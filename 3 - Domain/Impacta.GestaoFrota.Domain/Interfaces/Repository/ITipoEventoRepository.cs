using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository
{
    public interface ITipoEventoRepository : IRepositoryRead<TipoEvento>, IRepositoryWrite<TipoEvento>
    {
        bool Remover(int id);
    }
}
