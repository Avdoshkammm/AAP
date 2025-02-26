using System.ComponentModel.DataAnnotations;

namespace AAP.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Введите отчество")]
        public string Pathronomic { get; set; }
        [Required(ErrorMessage = "Введите почту")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}
