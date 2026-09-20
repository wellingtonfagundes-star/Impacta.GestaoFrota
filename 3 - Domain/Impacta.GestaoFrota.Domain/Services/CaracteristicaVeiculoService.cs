using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services
{
    public class CaracteristicaVeiculoService : ICaracteristicaVeiculoService
    {
        private readonly ICaracteristicaVeiculoRepository _repository;

        public CaracteristicaVeiculoService(ICaracteristicaVeiculoRepository repository)
        {
            _repository = repository;
        }

        public CaracteristicaVeiculo ObterPorIds(int idVeiculo, int idCaracteristica)
        {
            return _repository.ObterPorIds(idVeiculo, idCaracteristica);
        }

        public IEnumerable<CaracteristicaVeiculo> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public void Adicionar(CaracteristicaVeiculo caracteristicaVeiculo)
        {
            if (caracteristicaVeiculo == null)
                throw new ArgumentNullException(nameof(caracteristicaVeiculo));

            _repository.Adicionar(caracteristicaVeiculo);
        }

        public void Atualizar(CaracteristicaVeiculo caracteristicaVeiculo)
        {
            if (caracteristicaVeiculo == null)
                throw new ArgumentNullException(nameof(caracteristicaVeiculo));

            _repository.Atualizar(caracteristicaVeiculo);
        }

        public bool Remover(int idVeiculo, int idCaracteristica)
        {
            return _repository.Remover(idVeiculo, idCaracteristica);
        }

        public void Salvar()
        {
            _repository.SaveChanges();
        }
    }
}
