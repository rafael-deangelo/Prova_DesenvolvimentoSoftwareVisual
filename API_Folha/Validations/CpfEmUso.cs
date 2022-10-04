using System.ComponentModel.DataAnnotations;
using API_Folha.Models;
using System.Linq;

namespace API_Folha.Validations
{
    public class CpfEmUso : ValidationAttribute
    {
        // public CpfEmUso(string Cpf) {}  // Uma outra possibilidade com construtor
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cpf = (string) value; /// converter o object value

            DataContext context = (DataContext)validationContext.GetService(typeof(DataContext));

            Funcionario funcionario = context.Funcionarios.FirstOrDefault(f => f.Cpf.Equals(cpf));
            
            if (funcionario == null)
            {
                // Caso de Sucesso
                return ValidationResult.Success;
            }
            
            //Caso de Erro
            return new ValidationResult("O CPF do funcionário já está em uso!");
        }
    }
}