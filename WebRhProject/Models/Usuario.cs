using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbUsuario")]
    public class Usuario
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email {get; set; }
        [DataType(DataType.Password)]
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
