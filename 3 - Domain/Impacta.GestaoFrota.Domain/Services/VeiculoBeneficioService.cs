using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class VeiculoBeneficioService : IVeiculoBeneficioService
{
    private readonly IVeiculoBeneficioRepository _repository;

    public VeiculoBeneficioService(IVeiculoBeneficioRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<VeiculoBeneficio> ObterTodosComRelacionamentos() => _repository.ObterTodosComRelacionamentos();

    public VeiculoBeneficio? ObterPorIdComRelacionamentos(int id) => _repository.ObterPorIdComRelacionamentos(id);

    public IEnumerable<Pessoa> ObterPessoas() => _repository.ObterPessoas();

    public IEnumerable<Veiculo> ObterVeiculos() => _repository.ObterVeiculos();

    public bool Adicionar(VeiculoBeneficio veiculoBeneficio)
    {
        ArgumentNullException.ThrowIfNull(veiculoBeneficio);
        return _repository.Adicionar(veiculoBeneficio);
    }

    public bool Atualizar(VeiculoBeneficio veiculoBeneficio)
    {
        ArgumentNullException.ThrowIfNull(veiculoBeneficio);
        return _repository.Atualizar(veiculoBeneficio);
    }

    public bool Remover(int id) => _repository.RemoverPorId(id);
}