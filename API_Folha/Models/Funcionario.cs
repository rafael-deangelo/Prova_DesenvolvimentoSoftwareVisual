using System;
using System.ComponentModel.DataAnnotations;
using API_Folha.Validations;

namespace API_Folha.Models
{
    //Data Annotations
    public class Funcionario
    {
        public Funcionario () => CriadoEm = DateTime.Now;
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório!")]  // VALIDAÇÃO | Não consigo mais cadastrar se não for preenchido o campo | Não esquecer do Using
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório!")]
        [StringLength(
            11,
            MinimumLength = 11,
            ErrorMessage = "O campo CPF deve conter 11 caracteres!"
        )]
        [CpfEmUso]
        public string Cpf { get; set; }

        [EmailAddress(ErrorMessage = "E-mail Inválido!")]
        public string Email { get; set; }
         public DateTime Nascimento { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}