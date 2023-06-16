using System.ComponentModel.DataAnnotations;

namespace WebRhProject.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um email válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
