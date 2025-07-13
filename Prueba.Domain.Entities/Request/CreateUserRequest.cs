using System.ComponentModel.DataAnnotations;

namespace Prueba.Domain.Entities.Request
{
    public class CreateUserRequest
    {
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 10 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 10 caracteres.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de documento solo puede contener números.")]
        [StringLength(10, ErrorMessage = "El número de documento no puede tener más de 10 caracteres.")]
        public string DocumentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(1|2)$", ErrorMessage = "Solo se permite el valor '1' para 'Admin' o '2' para 'Usuario'.")]
        public int Rol { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
