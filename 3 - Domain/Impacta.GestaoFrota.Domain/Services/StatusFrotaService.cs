using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services
{
    public class StatusFrotaService : IStatusFrotaService
    {
        private readonly IStatusFrotaRepository _repository;

        public StatusFrotaService(IStatusFrotaRepository repository)
        {
            _repository = repository;
        }

        public StatusFrota ObterPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public IEnumerable<StatusFrota> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public void Adicionar(StatusFrota statusFrota)
        {
            if (statusFrota == null)
                throw new ArgumentNullException(nameof(statusFrota));

            _repository.Adicionar(statusFrota);
        }

        public void Atualizar(StatusFrota statusFrota)
        {
            if (statusFrota == null)
                throw new ArgumentNullException(nameof(statusFrota));

            _repository.Atualizar(statusFrota);
        }

        public bool Remover(int id)
        {
            return _repository.Remover(id);
        }

        public void Salvar()
        {
            _repository.SaveChanges();
        }
    }
}
