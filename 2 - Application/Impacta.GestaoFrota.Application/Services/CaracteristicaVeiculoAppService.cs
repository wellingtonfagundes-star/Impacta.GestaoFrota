using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class CaracteristicaVeiculoAppService : ICaracteristicaVeiculoAppService
{
    private readonly ICaracteristicaVeiculoService _service;
    private readonly IMapper _mapper;

    public CaracteristicaVeiculoAppService(
        ICaracteristicaVeiculoService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public CaracteristicaVeiculoViewModel? ObterPorId(int idVeiculo, int idCaracteristica)
    {
        var caracteristicaVeiculo = _service.ObterPorIds(idVeiculo, idCaracteristica);

        if (caracteristicaVeiculo == null)
            return null;

        return _mapper.Map<CaracteristicaVeiculoViewModel>(caracteristicaVeiculo);
    }

    public IEnumerable<CaracteristicaVeiculoViewModel> ObterTodos()
    {
        var características = _service.ObterTodos();
        return _mapper.Map<IEnumerable<CaracteristicaVeiculoViewModel>>(características);
    }

    public bool Adicionar(CaracteristicaVeiculoViewModel viewModel)
    {
        try
        {
            // Verificar duplicata
            var existente = _service.ObterPorIds(viewModel.IdVeiculo, viewModel.IdCaracteristica);
            if (existente != null)
                return false;

            var caracteristicaVeiculo = new CaracteristicaVeiculo
            {
                IdVeiculo = viewModel.IdVeiculo,
                IdCaracteristica = viewModel.IdCaracteristica,
                Valor = viewModel.Valor,
                Comentario = viewModel.Comentario,
                DataCriacao = DateOnly.FromDateTime(DateTime.Now),
                DataUltAlteracao = DateOnly.FromDateTime(DateTime.Now)
            };

            _service.Adicionar(caracteristicaVeiculo);
            _service.Salvar();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Atualizar(CaracteristicaVeiculoViewModel viewModel)
    {
        try
        {
            var caracteristicaVeiculo = _service.ObterPorIds(viewModel.IdVeiculo, viewModel.IdCaracteristica);

            if (caracteristicaVeiculo == null)
                return false;

            caracteristicaVeiculo.Valor = viewModel.Valor;
            caracteristicaVeiculo.Comentario = viewModel.Comentario;
            caracteristicaVeiculo.DataUltAlteracao = DateOnly.FromDateTime(DateTime.Now);

            _service.Atualizar(caracteristicaVeiculo);
            _service.Salvar();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Remover(int idVeiculo, int idCaracteristica)
    {
        try
        {
            return _service.Remover(idVeiculo, idCaracteristica);
        }
        catch
        {
            return false;
        }
    }
}
