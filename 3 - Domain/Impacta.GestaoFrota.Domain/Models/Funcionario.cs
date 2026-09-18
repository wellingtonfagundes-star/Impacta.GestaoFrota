using System;
using System.Collections.Generic;

namespace Impacta.GestaoFrota.Domain.Models;

public partial class Funcionario
{
    public int IdFuncionario { get; set; }

    public string Matricula { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Endereco { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Cep { get; set; }

    public char Sexo { get; set; }

    public string Cpf { get; set; } = null!;

    public DateOnly? DataCriacao { get; set; }

    public DateOnly? DataUltAlteracao { get; set; }

    public virtual ICollection<Historico> Historicos { get; set; } = new List<Historico>();

    public virtual ICollection<ReservaVeiculo> ReservaVeiculos { get; set; } = new List<ReservaVeiculo>();
}
