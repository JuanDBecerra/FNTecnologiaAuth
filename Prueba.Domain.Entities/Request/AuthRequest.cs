using System.ComponentModel.DataAnnotations;

namespace Prueba.Domain.Entities.Request
{
    public class AuthRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
