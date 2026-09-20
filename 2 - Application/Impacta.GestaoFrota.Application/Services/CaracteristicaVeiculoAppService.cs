using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class CaracteristicaVeiculoAppService : ICaracteristicaVeiculoAppService
{
    private readonly ICaracteristicaVeiculoService _service;
    private readonly ICaracteristicaVeiculoRepository _repository;
    private readonly IMapper _mapper;

    public CaracteristicaVeiculoAppService(
        ICaracteristicaVeiculoService service,
        ICaracteristicaVeiculoRepository repository,
        IMapper mapper)
    {
        _service = service;
        _repository = repository;
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
        // Usar o método que retorna com descrições
        var dtos = _repository.ObterTodosComDescricoes();

        // Mapear DTOs para ViewModel
        return dtos.Select(dto => new CaracteristicaVeiculoViewModel
        {
            IdVeiculo = dto.IdVeiculo,
            IdCaracteristica = dto.IdCaracteristica,
            Valor = dto.Valor,
            Comentario = dto.Comentario,
            DataCriacao = dto.DataCriacao,
            DataUltAlteracao = dto.DataUltAlteracao,
            VeiculoDescricao = dto.VeiculoDescricao,
            CaracteristicaDescricao = dto.CaracteristicaDescricao
        });
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
                Comentario = viewModel.Comentario ?? string.Empty,
                DataCriacao = DateOnly.FromDateTime(DateTime.Now),
                DataUltAlteracao = DateOnly.FromDateTime(DateTime.Now)
            };

            _service.Adicionar(caracteristicaVeiculo);
            _service.Salvar();
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception for debugging
            System.Diagnostics.Debug.WriteLine($"Erro ao adicionar CaracteristicaVeiculo: {ex.Message}");
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
            caracteristicaVeiculo.Comentario = viewModel.Comentario ?? string.Empty;
            caracteristicaVeiculo.DataUltAlteracao = DateOnly.FromDateTime(DateTime.Now);

            _service.Atualizar(caracteristicaVeiculo);
            _service.Salvar();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao atualizar CaracteristicaVeiculo: {ex.Message}");
            return false;
        }
    }

    public bool Remover(int idVeiculo, int idCaracteristica)
    {
        try
        {
            return _service.Remover(idVeiculo, idCaracteristica);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao remover CaracteristicaVeiculo: {ex.Message}");
            return false;
        }
    }
}

