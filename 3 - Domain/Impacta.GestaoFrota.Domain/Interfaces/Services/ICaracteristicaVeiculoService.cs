using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services
{
    public interface ICaracteristicaVeiculoService
    {
        CaracteristicaVeiculo ObterPorIds(int idVeiculo, int idCaracteristica);
        IEnumerable<CaracteristicaVeiculo> ObterTodos();
        void Adicionar(CaracteristicaVeiculo caracteristicaVeiculo);
        void Atualizar(CaracteristicaVeiculo caracteristicaVeiculo);
        bool Remover(int idVeiculo, int idCaracteristica);
        void Salvar();
    }
}
