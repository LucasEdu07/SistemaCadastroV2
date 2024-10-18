using System.ComponentModel.DataAnnotations;

namespace SistemaCadastro.Models
{
    public class Veiculos
    {
        [Display(Name = "Id")]
        public int VeiculosId { get; set; }

        public string Placa { get; set; }

        public int Renavam { get; set; }

        public string Chassi { get; set; }

        public string Motor { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Combustivel { get; set; }

        public string Cor { get; set; }

        public int Ano { get; set; }

        public string Status { get; set; }
    }
}