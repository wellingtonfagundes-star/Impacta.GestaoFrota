using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class ClienteRepository : Repository<Pessoa>, IClienteRepository
{
    public ClienteRepository(FrotaContext context)
        : base(context)
    {
    }

    public bool ExisteCpf(string cpf)
    {
        return DbSet.Any(pessoa => pessoa.Cpf == cpf);
    }

    public bool ExisteCnpj(string cnpj)
    {
        return DbSet.Any(pessoa => pessoa.Cnpj == cnpj);
    }

    public override bool Atualizar(Pessoa pessoa)
    {
        try
        {
            var registro = DbSet.Find(pessoa.IdPessoa);
            if (registro is null)
                return false;

            registro.Nome = pessoa.Nome;
            registro.Endereco = pessoa.Endereco;
            registro.Bairro = pessoa.Bairro;
            registro.Cidade = pessoa.Cidade;
            registro.Estado = pessoa.Estado;
            registro.Cep = pessoa.Cep;
            registro.Sexo = pessoa.Sexo;
            registro.TipoCliente = pessoa.TipoCliente;
            registro.Cpf = pessoa.Cpf;
            registro.Cnpj = pessoa.Cnpj;
            registro.DataUltAlteracao = pessoa.DataUltAlteracao;

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
            var pessoa = DbSet.Find(id);
            if (pessoa is null)
                return false;

            DbSet.Remove(pessoa);
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
