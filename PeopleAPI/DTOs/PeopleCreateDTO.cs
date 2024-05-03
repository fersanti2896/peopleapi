using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.DTOs{
    public class PeopleCreateDTO {
        [Required(ErrorMessage = "El Nombre es requerido.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El nombre debe tener entre 5 y 50 caracteres.")]
        public string Name { get; set; }
        public string LastName { get; set; }

        [Range(1, 100, ErrorMessage = "La edad debe ser un número.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "El Correo Electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
        public string Email { get; set; }
    }
}
