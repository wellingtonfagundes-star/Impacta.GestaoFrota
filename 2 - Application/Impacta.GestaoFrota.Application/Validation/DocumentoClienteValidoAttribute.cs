using System.ComponentModel.DataAnnotations;
using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class DocumentoClienteValidoAttribute : ValidationAttribute
{
    public DocumentoClienteValidoAttribute()
    {
        ErrorMessage = "Informe um CPF ou CNPJ válido para o tipo de cliente selecionado.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string documento || string.IsNullOrWhiteSpace(documento))
            return ValidationResult.Success;

        if (validationContext.ObjectInstance is not ClienteViewModel cliente)
            return ValidationResult.Success;

        var digitos = new string(documento.Where(char.IsAsciiDigit).ToArray());
        var valido = cliente.TipoCliente switch
        {
            "PF" => ValidarCpf(digitos),
            "PJ" => ValidarCnpj(digitos),
            _ => false
        };

        return valido
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage, new[] { validationContext.MemberName ?? nameof(ClienteViewModel.Documento) });
    }

    private static bool ValidarCpf(string cpf)
    {
        if (cpf.Length != 11 || cpf.All(digito => digito == cpf[0]))
            return false;

        var digitos = cpf.Select(digito => digito - '0').ToArray();
        var soma = 0;
        for (var indice = 0; indice < 9; indice++)
            soma += digitos[indice] * (10 - indice);

        var primeiro = (soma * 10) % 11;
        if (primeiro == 10)
            primeiro = 0;
        if (digitos[9] != primeiro)
            return false;

        soma = 0;
        for (var indice = 0; indice < 10; indice++)
            soma += digitos[indice] * (11 - indice);

        var segundo = (soma * 10) % 11;
        if (segundo == 10)
            segundo = 0;

        return digitos[10] == segundo;
    }

    private static bool ValidarCnpj(string cnpj)
    {
        if (cnpj.Length != 14 || cnpj.All(digito => digito == cnpj[0]))
            return false;

        var digitos = cnpj.Select(digito => digito - '0').ToArray();
        var pesosPrimeiroDigito = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var pesosSegundoDigito = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var primeiro = CalcularDigitoCnpj(digitos, pesosPrimeiroDigito);
        if (digitos[12] != primeiro)
            return false;

        var segundo = CalcularDigitoCnpj(digitos, pesosSegundoDigito);
        return digitos[13] == segundo;
    }

    private static int CalcularDigitoCnpj(int[] digitos, int[] pesos)
    {
        var soma = 0;
        for (var indice = 0; indice < pesos.Length; indice++)
            soma += digitos[indice] * pesos[indice];

        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
