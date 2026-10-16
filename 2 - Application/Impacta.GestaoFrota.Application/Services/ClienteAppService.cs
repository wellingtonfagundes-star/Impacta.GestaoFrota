using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class ClienteAppService : IClienteAppService
{
    private readonly IClienteService _service;
    private readonly IMapper _mapper;

    public ClienteAppService(IClienteService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public ClienteViewModel? ObterPorId(int id)
    {
        var pessoa = _service.ObterPorId(id);
        return pessoa is null ? null : _mapper.Map<ClienteViewModel>(pessoa);
    }

    public IEnumerable<ClienteViewModel> ObterTodos()
    {
        return _mapper.Map<IEnumerable<ClienteViewModel>>(_service.ObterTodos());
    }

    public bool Adicionar(ClienteViewModel viewModel)
    {
        try
        {
            var documento = NormalizarDocumento(viewModel.Documento, viewModel.TipoCliente);
            var documentoJaCadastrado = viewModel.TipoCliente == "PF"
                ? _service.ExisteCpf(documento)
                : _service.ExisteCnpj(documento);
            if (documentoJaCadastrado)
                return false;

            var pessoa = _mapper.Map<Pessoa>(viewModel);
            AplicarDocumento(pessoa, viewModel);
            pessoa.DataCriacao = DateOnly.FromDateTime(DateTime.Today);
            return _service.Adicionar(pessoa);
        }
        catch
        {
            return false;
        }
    }

    public bool Atualizar(ClienteViewModel viewModel)
    {
        try
        {
            var pessoa = _service.ObterPorId(viewModel.IdPessoa);
            if (pessoa is null)
                return false;

            var tipoClienteExistente = pessoa.TipoCliente;
            var cpfExistente = pessoa.Cpf;
            var cnpjExistente = pessoa.Cnpj;
            _mapper.Map(viewModel, pessoa);
            pessoa.TipoCliente = tipoClienteExistente;
            pessoa.Cpf = cpfExistente;
            pessoa.Cnpj = cnpjExistente;
            pessoa.DataUltAlteracao = DateOnly.FromDateTime(DateTime.Today);
            return _service.Atualizar(pessoa);
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

    private static void AplicarDocumento(Pessoa pessoa, ClienteViewModel viewModel)
    {
        var documento = NormalizarDocumento(viewModel.Documento, viewModel.TipoCliente);
        pessoa.Cpf = viewModel.TipoCliente == "PF" ? documento : null;
        pessoa.Cnpj = viewModel.TipoCliente == "PJ" ? documento : null;
    }

    private static string NormalizarDocumento(string documento, string tipoCliente)
    {
        return tipoCliente == "PF"
            ? new string(documento.Where(char.IsAsciiDigit).ToArray())
            : new string(documento
                .Where(char.IsAsciiLetterOrDigit)
                .Select(char.ToUpperInvariant)
                .ToArray());
    }
}
