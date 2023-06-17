using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbUsuario")]
    public class Usuario
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} size must be between {2} and {1} caracters")]

        public string Email {get; set; }
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} size must be between {2} and {1} caracters")]

        public string Senha { get; set; }

        public Colaborador Colaborador { get; set; }
        public int ColaboradorId { get; set; }

        public Usuario()
        {

        }

        public Usuario(int id, string email, string senha, Colaborador colaborador)
        {
            Id = id;
            Email = email;
            Senha = senha;
            Colaborador = colaborador;
        }
    }
}
