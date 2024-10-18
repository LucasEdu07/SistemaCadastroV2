using System.ComponentModel.DataAnnotations;

namespace SistemaCadastro.Models
{
    public class Cor
    {
        [Display(Name = "Id")]
        public int CorId { get; set; }

        [Required(ErrorMessage = "O nome da cor é obrigatória.")]
        [Display(Name = "Nome")]
        public string NomeCor { get; set; }
    }
}