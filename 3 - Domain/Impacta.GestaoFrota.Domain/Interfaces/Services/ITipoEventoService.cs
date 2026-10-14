using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services;

public interface ITipoEventoService
{
    TipoEvento? ObterPorId(int id);
    IEnumerable<TipoEvento> ObterTodos();
    void Adicionar(TipoEvento tipoEvento);
    void Atualizar(TipoEvento tipoEvento);
    bool Remover(int id);
    void Salvar();
}
