using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services;

public interface IReservaVeiculoService
{
    IEnumerable<ReservaVeiculo> ObterTodosComRelacionamentos();
    ReservaVeiculo? ObterPorIdComRelacionamentos(int id);
    IEnumerable<Veiculo> ObterVeiculos();
    IEnumerable<Local> ObterLocais();
    IEnumerable<Pessoa> ObterPessoas();
    IEnumerable<Funcionario> ObterFuncionarios();
    bool Salvar(ReservaVeiculo reserva, RetornoVeiculo retorno);
    bool Remover(int id);
}
