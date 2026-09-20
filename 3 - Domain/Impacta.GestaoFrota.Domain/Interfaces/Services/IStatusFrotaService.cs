using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services
{
    public interface IStatusFrotaService
    {
        StatusFrota ObterPorId(int id);
        IEnumerable<StatusFrota> ObterTodos();
        void Adicionar(StatusFrota statusFrota);
        void Atualizar(StatusFrota statusFrota);
        bool Remover(int id);
        void Salvar();
    }
}
