using System.ComponentModel.DataAnnotations;

namespace ProjectFor7COMm.DTOs
{
    public class UpdateUserDTO
    {
        [Required(ErrorMessage = "O Id do usuário é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }
    }
}
