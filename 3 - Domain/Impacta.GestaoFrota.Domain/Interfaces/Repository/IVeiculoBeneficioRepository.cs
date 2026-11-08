using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository;

public interface IVeiculoBeneficioRepository : IRepositoryRead<VeiculoBeneficio>, IRepositoryWrite<VeiculoBeneficio>
{
    IEnumerable<VeiculoBeneficio> ObterTodosComRelacionamentos();
    VeiculoBeneficio? ObterPorIdComRelacionamentos(int id);
    IEnumerable<Pessoa> ObterPessoas();
    IEnumerable<Veiculo> ObterVeiculos();
    bool RemoverPorId(int id);
}