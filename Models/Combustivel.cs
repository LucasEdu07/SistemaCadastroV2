using System.ComponentModel.DataAnnotations;

namespace SistemaCadastro.Models
{
    public class Combustivel
    {
        [Display(Name = "Id")]
        public int CombustivelId { get; set; }

        [Required(ErrorMessage = "O nome do combustível é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}