using System.ComponentModel.DataAnnotations;

namespace AluguerVeiculos.Models
{
    public class Contrato
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        [Required]
        [CustomValidation(typeof(Contrato), nameof(ValidarDataInicio))]
        public DateTime DataInicio { get; set; }

        [Required]
        [CustomValidation(typeof(Contrato), nameof(ValidarDataFim))]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "A quilometragem inicial é obrigatória")]
        public int QuilometragemInicial { get; set; }

        public int QuilometragemFinal { get; set; }

        [Required]
        public string Estado_Contrato { get; set; } = "Ativo"; 


        public static ValidationResult ValidarDataInicio(DateTime dataInicio, ValidationContext context)
        {
            if (dataInicio < DateTime.Now)
            {
                return new ValidationResult("A data de início do aluguer não pode ser anterior à data atual ");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidarDataFim(DateTime dataFim, ValidationContext context)
        {
            var instance = context.ObjectInstance as Contrato;

            if (instance == null)
            {
                return new ValidationResult("Erro ao validar a data de fim ");
            }

            if (dataFim <= instance.DataInicio)
            {
                return new ValidationResult("A data de fim do aluguer deve ser posterior à data de início ");
            }

            return ValidationResult.Success;
        }

        public void AtualizarEstadoVeiculo()
        {
            if (DataFim > DateTime.Now)
            {
                Estado_Contrato = "Ativo";
                Veiculo.Estado = "Alugado";
            }
            else
            {
                Estado_Contrato = "Encerrado";
                Veiculo.Estado = "Disponível";
            }
        }
    }
}
