using System.ComponentModel.DataAnnotations;
using System.Linq;

public class CpfValidoAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;

        string cpf = value.ToString().Replace(".", "").Replace("-", "");

        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            return new ValidationResult("CPF deve conter 11 dígitos numéricos.");

        // Verifica se todos os dígitos são iguais
        if (cpf.Distinct().Count() == 1)
            return new ValidationResult("CPF inválido.");

        // Valida primeiro dígito verificador
        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * (10 - i);

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[9].ToString()) != digito1)
            return new ValidationResult("CPF inválido.");

        // Valida segundo dígito verificador
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(cpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[10].ToString()) != digito2)
            return new ValidationResult("CPF inválido.");

        return ValidationResult.Success;
    }
}
