using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class FuncionarioAppService : IFuncionarioAppService
{
    private readonly IFuncionarioService _service;
    private readonly IMapper _mapper;

    public FuncionarioAppService(IFuncionarioService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public FuncionarioViewModel? ObterPorId(int id)
    {
        var funcionario = _service.ObterPorId(id);
        return funcionario is null ? null : _mapper.Map<FuncionarioViewModel>(funcionario);
    }

    public IEnumerable<FuncionarioViewModel> ObterTodos()
    {
        return _mapper.Map<IEnumerable<FuncionarioViewModel>>(_service.ObterTodos());
    }

    public bool Adicionar(FuncionarioViewModel viewModel)
    {
        try
        {
            var funcionario = _mapper.Map<Funcionario>(viewModel);
            funcionario.Cpf = NormalizarCpf(viewModel.Cpf);
            return _service.Adicionar(funcionario);
        }
        catch
        {
            return false;
        }
    }

    public bool Atualizar(FuncionarioViewModel viewModel)
    {
        try
        {
            var funcionario = _service.ObterPorId(viewModel.IdFuncionario);
            if (funcionario is null)
                return false;

            _mapper.Map(viewModel, funcionario);
            funcionario.Cpf = NormalizarCpf(viewModel.Cpf);
            return _service.Atualizar(funcionario);
        }
        catch
        {
            return false;
        }
    }

    public bool Remover(int id)
    {
        try
        {
            return _service.Remover(id);
        }
        catch
        {
            return false;
        }
    }

    private static string NormalizarCpf(string cpf)
    {
        return new string(cpf.Where(char.IsAsciiDigit).ToArray());
    }
}
