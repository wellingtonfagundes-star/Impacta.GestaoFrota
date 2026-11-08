using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services;

public interface IVeiculoBeneficioService
{
    IEnumerable<VeiculoBeneficio> ObterTodosComRelacionamentos();
    VeiculoBeneficio? ObterPorIdComRelacionamentos(int id);
    IEnumerable<Pessoa> ObterPessoas();
    IEnumerable<Veiculo> ObterVeiculos();
    bool Adicionar(VeiculoBeneficio veiculoBeneficio);
    bool Atualizar(VeiculoBeneficio veiculoBeneficio);
    bool Remover(int id);
}