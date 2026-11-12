using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class ReservaVeiculoRepository : IReservaVeiculoRepository
{
    private readonly FrotaContext _db;

    public ReservaVeiculoRepository(FrotaContext db)
    {
        _db = db;
    }

    public IEnumerable<ReservaVeiculo> ObterTodosComRelacionamentos()
    {
        return _db.ReservaVeiculos.AsNoTracking()
            .Include(item => item.IdClienteSolicitanteNavigation)
            .Include(item => item.IdFuncionarioAprovadorNavigation)
            .Include(item => item.IdLocalDestinoNavigation)
            .Include(item => item.RetornoVeiculos)
                .ThenInclude(item => item.IdLocalRetornoNavigation)
            .Include(item => item.RetornoVeiculos)
                .ThenInclude(item => item.IdVeiculoNavigation)
            .OrderByDescending(item => item.IdReserva)
            .ToList();
    }

    public ReservaVeiculo? ObterPorIdComRelacionamentos(int id)
    {
        return _db.ReservaVeiculos
            .Include(item => item.RetornoVeiculos)
                .ThenInclude(item => item.IdStatusNavigation)
            .Include(item => item.RetornoVeiculos)
                .ThenInclude(item => item.IdLocalRetornoNavigation)
            .Include(item => item.RetornoVeiculos)
                .ThenInclude(item => item.IdVeiculoNavigation)
            .SingleOrDefault(item => item.IdReserva == id);
    }

    public IEnumerable<Veiculo> ObterVeiculos() => _db.Veiculos.AsNoTracking().OrderBy(item => item.Placa).ToList();
    public IEnumerable<Local> ObterLocais() => _db.Locais.AsNoTracking().OrderBy(item => item.Nome).ToList();
    public IEnumerable<Pessoa> ObterPessoas() => _db.Pessoas.AsNoTracking().OrderBy(item => item.Nome).ToList();
    public IEnumerable<Funcionario> ObterFuncionarios() => _db.Funcionarios.AsNoTracking().OrderBy(item => item.Nome).ToList();

    public bool Salvar(ReservaVeiculo reserva, RetornoVeiculo retorno)
    {
        using var transaction = _db.Database.BeginTransaction();
        try
        {
            var statusAnteriorId = retorno.IdRetorno == 0
                ? (int?)null
                : _db.Entry(retorno).Property(item => item.IdStatus).OriginalValue;

            if (reserva.IdReserva == 0)
            {
                reserva.RetornoVeiculos.Add(retorno);
                _db.ReservaVeiculos.Add(reserva);
            }
            else if (retorno.IdRetorno == 0)
            {
                reserva.RetornoVeiculos.Add(retorno);
            }

            retorno.IdReservaNavigation = reserva;
            _db.SaveChanges();

            if (statusAnteriorId.HasValue && retorno.IdStatus != statusAnteriorId
                && !_db.RetornoVeiculos.Any(item => item.IdStatus == statusAnteriorId))
            {
                var statusAnterior = _db.StatusVeiculos.Find(statusAnteriorId.Value);
                if (statusAnterior is not null)
                {
                    _db.StatusVeiculos.Remove(statusAnterior);
                    _db.SaveChanges();
                }
            }
            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    public bool Remover(int id)
    {
        using var transaction = _db.Database.BeginTransaction();
        try
        {
            var reserva = _db.ReservaVeiculos
                .Include(item => item.RetornoVeiculos)
                .SingleOrDefault(item => item.IdReserva == id);
            if (reserva is null)
                return false;

            var statusIds = reserva.RetornoVeiculos
                .Where(item => item.IdStatus.HasValue)
                .Select(item => item.IdStatus!.Value)
                .ToList();
            _db.RetornoVeiculos.RemoveRange(reserva.RetornoVeiculos);
            _db.ReservaVeiculos.Remove(reserva);
            _db.SaveChanges();

            var statuses = _db.StatusVeiculos
                .Where(item => statusIds.Contains(item.IdStatus)
                    && !_db.RetornoVeiculos.Any(retorno => retorno.IdStatus == item.IdStatus))
                .ToList();
            if (statuses.Count > 0)
            {
                _db.StatusVeiculos.RemoveRange(statuses);
                _db.SaveChanges();
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }
}
