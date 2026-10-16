using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(FrotaContext context)
        : base(context)
    {
    }

    public bool ExisteCpf(string cpf)
    {
        return DbSet.Any(funcionario => funcionario.Cpf == cpf);
    }

    public override bool Adicionar(Funcionario funcionario)
    {
        using var transaction = db.Database.BeginTransaction();
        try
        {
            funcionario.Matricula = $"TMP{Guid.NewGuid():N}"[..30];
            DbSet.Add(funcionario);
            if (SaveChanges() <= 0)
                return false;

            funcionario.Matricula = $"MAT{funcionario.IdFuncionario:D4}";
            if (SaveChanges() <= 0)
                return false;

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    public override bool Atualizar(Funcionario funcionario)
    {
        try
        {
            var registro = DbSet.Find(funcionario.IdFuncionario);
            if (registro is null)
                return false;

            registro.Nome = funcionario.Nome;
            registro.Endereco = funcionario.Endereco;
            registro.Bairro = funcionario.Bairro;
            registro.Cidade = funcionario.Cidade;
            registro.Estado = funcionario.Estado;
            registro.Cep = funcionario.Cep;
            registro.Sexo = funcionario.Sexo;
            registro.Cpf = funcionario.Cpf;
            registro.DataUltAlteracao = DateOnly.FromDateTime(DateTime.Today);

            return SaveChanges() > 0;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public bool Remover(int id)
    {
        try
        {
            var funcionario = DbSet.Find(id);
            if (funcionario is null)
                return false;

            DbSet.Remove(funcionario);
            return SaveChanges() > 0;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public override bool Remover(Guid id)
    {
        return Remover(BitConverter.ToInt32(id.ToByteArray(), 0));
    }
}
