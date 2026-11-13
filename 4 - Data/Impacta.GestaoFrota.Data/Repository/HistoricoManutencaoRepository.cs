using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class HistoricoManutencaoRepository : IHistoricoManutencaoRepository
{
    private readonly FrotaContext _db;

    public HistoricoManutencaoRepository(FrotaContext db)
    {
        _db = db;
    }

    public IEnumerable<Historico> ObterTodos()
    {
        return _db.Historicos.AsNoTracking()
            .Include(item => item.IdLocalNavigation)
            .Include(item => item.IdTipoeventoNavigation)
            .Include(item => item.IdFuncionarioResponsavelNavigation)
            .Include(item => item.IdClienteAtendidoNavigation)
            .OrderByDescending(item => item.DataEvento)
            .ThenByDescending(item => item.IdHistorico)
            .ToList();
    }

    public Historico? ObterPorId(int id)
    {
        return _db.Historicos
            .Include(item => item.IdLocalNavigation)
            .Include(item => item.IdTipoeventoNavigation)
            .Include(item => item.IdFuncionarioResponsavelNavigation)
            .Include(item => item.IdClienteAtendidoNavigation)
            .SingleOrDefault(item => item.IdHistorico == id);
    }

    public IEnumerable<Local> ObterLocais() => _db.Locais.AsNoTracking().OrderBy(item => item.Nome).ToList();

    public IEnumerable<TipoEvento> ObterTiposEvento() => _db.TipoEventos.AsNoTracking().OrderBy(item => item.Nome).ToList();

    public IEnumerable<Funcionario> ObterFuncionarios() => _db.Funcionarios.AsNoTracking().OrderBy(item => item.Nome).ToList();

    public IEnumerable<Pessoa> ObterClientes() => _db.Pessoas.AsNoTracking().OrderBy(item => item.Nome).ToList();

    public bool Adicionar(Historico historico)
    {
        try
        {
            _db.Historicos.Add(historico);
            return _db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool Atualizar(Historico historico)
    {
        try
        {
            var existente = _db.Historicos.Find(historico.IdHistorico);
            if (existente is null)
                return false;

            _db.Entry(existente).CurrentValues.SetValues(historico);
            return _db.SaveChanges() > 0;
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
            var historico = _db.Historicos.Find(id);
            if (historico is null)
                return false;

            _db.Historicos.Remove(historico);
            return _db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
