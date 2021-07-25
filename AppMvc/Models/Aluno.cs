using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMvc.Models
{
    public class Aluno 
    {
        [Key]  
        public int id { get; set; }
            
        [DisplayName("Nome Completo")]
        [Required(ErrorMessage ="o campo {0} ")]
        [MaxLength(100, ErrorMessage = "no max 100 caracteres ")]
        public string  Nome { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "o campo {0} ")]
        [MaxLength(100, ErrorMessage = "O email e invalido ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "o campo {0} ")]
        public string CPF { get; set; }

        public DateTime DataMatricula { get; set; }
        public bool Ativo { get; set; }

    }
}