using System;
using System.ComponentModel.DataAnnotations;

public class DataNascimentoValidaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        DateTime dataNascimento = (DateTime)value;
        DateTime hoje = DateTime.Today;

        // Verifica se não é data futura
        if (dataNascimento > hoje)
            return new ValidationResult("A data de nascimento não pode ser futura.");

        int idade = hoje.Year - dataNascimento.Year;
        if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;

        // Verifica idade máxima
        int idadeMaxima = 120;
        if (idade > idadeMaxima)
            return new ValidationResult($"Data de nascimento muito antiga.");

        return ValidationResult.Success;
    }
}
