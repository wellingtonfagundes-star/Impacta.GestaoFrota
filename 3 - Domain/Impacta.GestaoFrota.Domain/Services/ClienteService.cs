using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public Pessoa? ObterPorId(int id)
    {
        return _repository.ObterPorId(id);
    }

    public IEnumerable<Pessoa> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public bool ExisteCpf(string cpf)
    {
        return _repository.ExisteCpf(cpf);
    }

    public bool ExisteCnpj(string cnpj)
    {
        return _repository.ExisteCnpj(cnpj);
    }

    public bool Adicionar(Pessoa pessoa)
    {
        ArgumentNullException.ThrowIfNull(pessoa);
        return _repository.Adicionar(pessoa);
    }

    public bool Atualizar(Pessoa pessoa)
    {
        ArgumentNullException.ThrowIfNull(pessoa);
        return _repository.Atualizar(pessoa);
    }

    public bool Remover(int id)
    {
        return _repository.Remover(id);
    }
}
