using System.ComponentModel.DataAnnotations;

namespace AluguerVeiculos.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória")]
        public required string Marca { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório")]
        public required string Modelo { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "Formato de matrícula inválido")]
        public required string Matricula { get; set; }

        [Required(ErrorMessage = "O ano de fabrico é obrigatório")]
        [CustomValidation(typeof(Veiculo), nameof(ValidarAnoDeFabrico))]
        public int Ano_de_fabrico { get; set; }

        public static ValidationResult ValidarAnoDeFabrico (int ano, ValidationContext context)
        {
            if ( ano > DateTime.Now.Year)
            {
                return new ValidationResult("O ano de fabrico não pode ser posterior ao ano atual ");
            }
            return ValidationResult.Success;
        }

        [Required(ErrorMessage = "O tipo de combustível é obrigatório")]
        public required string Tipo_de_Combustivel { get; set; }

        public int Quilometragem { get; set; }

        [Required(ErrorMessage ="O estado é obrigatório")]
        public string Estado { get; set; } = "Disponível";
       
    }
}