using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services;

public interface ILocalAssistenciaService
{
    Local ObterPorId(int id);

    IEnumerable<Local> ObterTodos();

    bool Adicionar(Local local);

    bool Atualizar(Local local);

    bool Remover(int id);
}
