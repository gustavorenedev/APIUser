using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFor7COMm.Models
{
    [Table("TBC_User")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome de usuário deve ter no máximo 50 caracteres.")]
        [Column("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [Column("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Column("PasswordHash")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "O salt da senha é obrigatório.")]
        [Column("PasswordSalt")]
        public string PasswordSalt { get; set; }
    }
}
