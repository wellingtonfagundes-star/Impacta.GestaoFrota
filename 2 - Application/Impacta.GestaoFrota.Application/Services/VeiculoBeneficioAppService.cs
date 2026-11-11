using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class VeiculoBeneficioAppService : IVeiculoBeneficioAppService
{
    private readonly IVeiculoBeneficioService _service;

    public VeiculoBeneficioAppService(IVeiculoBeneficioService service)
    {
        _service = service;
    }

    public IEnumerable<VeiculoBeneficioViewModel> ObterTodos()
    {
        return _service.ObterTodosComRelacionamentos().Select(Mapear);
    }

    public VeiculoBeneficioViewModel? ObterPorId(int id)
    {
        var beneficio = _service.ObterPorIdComRelacionamentos(id);
        return beneficio is null ? null : Mapear(beneficio);
    }

    public IEnumerable<PessoaOpcaoViewModel> ObterPessoas()
    {
        return _service.ObterPessoas().Select(pessoa => new PessoaOpcaoViewModel
        {
            IdPessoa = pessoa.IdPessoa,
            Nome = pessoa.Nome
        });
    }

    public IEnumerable<VeiculoOpcaoViewModel> ObterVeiculos()
    {
        return _service.ObterVeiculos().Select(veiculo => new VeiculoOpcaoViewModel
        {
            IdVeiculo = veiculo.IdVeiculo,
            Descricao = $"{veiculo.Placa} | {veiculo.Nome} | {veiculo.Fabricante}"
        });
    }

    public bool Adicionar(VeiculoBeneficioViewModel viewModel)
    {
        if (!ReferenciasValidas(viewModel))
            return false;

        return _service.Adicionar(CriarEntidade(viewModel));
    }

    public bool Atualizar(VeiculoBeneficioViewModel viewModel)
    {
        var beneficio = _service.ObterPorIdComRelacionamentos(viewModel.IdBeneficio);
        if (beneficio is null || !ReferenciasValidas(viewModel))
            return false;

        AtualizarEntidade(beneficio, viewModel);
        return _service.Atualizar(beneficio);
    }

    public bool Remover(int id)
    {
        return _service.Remover(id);
    }

    private bool ReferenciasValidas(VeiculoBeneficioViewModel viewModel)
    {
        var pessoas = _service.ObterPessoas().Select(pessoa => pessoa.IdPessoa).ToHashSet();
        var veiculos = _service.ObterVeiculos().Select(veiculo => veiculo.IdVeiculo).ToHashSet();
        return pessoas.Contains(viewModel.IdBeneficiado)
            && pessoas.Contains(viewModel.IdMotorista)
            && veiculos.Contains(viewModel.IdVeiculo);
    }

    private static VeiculoBeneficio CriarEntidade(VeiculoBeneficioViewModel viewModel)
    {
        var beneficio = new VeiculoBeneficio();
        AtualizarEntidade(beneficio, viewModel);
        return beneficio;
    }

    private static void AtualizarEntidade(VeiculoBeneficio beneficio, VeiculoBeneficioViewModel viewModel)
    {
        beneficio.IdVeiculo = viewModel.IdVeiculo;
        beneficio.IdBeneficiado = viewModel.IdBeneficiado;
        beneficio.IdMotorista = viewModel.IdMotorista;
        beneficio.DataEntrega = viewModel.DataEntrega;
        beneficio.DataDevolucao = viewModel.DataDevolucao;
        beneficio.UsoFds = viewModel.UsoFds;
        beneficio.LocalVaga = viewModel.LocalVaga;
    }

    private static VeiculoBeneficioViewModel Mapear(VeiculoBeneficio beneficio)
    {
        return new VeiculoBeneficioViewModel
        {
            IdBeneficio = beneficio.IdBeneficio,
            IdVeiculo = beneficio.IdVeiculo,
            IdBeneficiado = beneficio.IdBeneficiado,
            IdMotorista = beneficio.IdMotorista,
            DataEntrega = beneficio.DataEntrega,
            DataDevolucao = beneficio.DataDevolucao,
            UsoFds = beneficio.UsoFds ?? false,
            LocalVaga = beneficio.LocalVaga,
            VeiculoDescricao = $"{beneficio.IdVeiculoNavigation.Placa} | {beneficio.IdVeiculoNavigation.Nome}",
            BeneficiadoNome = beneficio.IdBeneficiadoNavigation.Nome,
            MotoristaNome = beneficio.IdMotoristaNavigation.Nome,
            BeneficiadoSelecao = beneficio.IdBeneficiadoNavigation.Nome,
            MotoristaSelecao = beneficio.IdMotoristaNavigation.Nome
        };
    }
}