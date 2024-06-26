﻿using System.ComponentModel.DataAnnotations;

namespace ProjectFor7COMm.DTOs
{
    public class PasswordResetDTO
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        public string NewPassword { get; set; }
    }
}
