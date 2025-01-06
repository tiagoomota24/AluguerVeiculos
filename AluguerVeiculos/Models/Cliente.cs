using System.ComponentModel.DataAnnotations;

namespace AluguerVeiculos.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        public required string Nome_Completo { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [RegularExpression(@"^\d{9,15}$", ErrorMessage = "Telefone inválido")]
        public int Telefone { get; set; }

        [Required(ErrorMessage = "A carta de condução é obrigatória")]
        public required string Carta_de_Conducao { get; set; }
    }
}