using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Impacta.GestaoFrota.Application.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class CpfValidoAttribute : ValidationAttribute
{
    private static readonly Regex FormatoCpf = new(
        "^(?:[0-9]{11}|[0-9]{3}\\.[0-9]{3}\\.[0-9]{3}-[0-9]{2})$",
        RegexOptions.Compiled);

    public CpfValidoAttribute()
    {
        ErrorMessage = "O CPF informado é inválido.";
    }

    public override bool IsValid(object? value)
    {
        if (value is not string cpf || string.IsNullOrWhiteSpace(cpf))
            return true;

        if (!FormatoCpf.IsMatch(cpf))
            return false;

        var digitos = cpf.Where(char.IsAsciiDigit).Select(digito => digito - '0').ToArray();
        if (digitos.Length != 11 || digitos.All(digito => digito == digitos[0]))
            return false;

        var soma = 0;
        for (var indice = 0; indice < 9; indice++)
            soma += digitos[indice] * (10 - indice);

        var primeiroDigito = (soma * 10) % 11;
        if (primeiroDigito == 10)
            primeiroDigito = 0;
        if (digitos[9] != primeiroDigito)
            return false;

        soma = 0;
        for (var indice = 0; indice < 10; indice++)
            soma += digitos[indice] * (11 - indice);

        var segundoDigito = (soma * 10) % 11;
        if (segundoDigito == 10)
            segundoDigito = 0;

        return digitos[10] == segundoDigito;
    }
}
