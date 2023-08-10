using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebRhProject.Models
{
    [Table("TbUsuario")]
    public class Usuario 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O tamanho do campo {0} deve ser entre {2} e {1} caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O tamanho do campo {0} deve ser entre {2} e {1} caracteres.")]
        public string Senha { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O tamanho do campo {0} deve ser entre {2} e {1} caracteres.")]
        [Compare("Senha", ErrorMessage = "As senhas não correspondem.")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }

        [JsonIgnore]
        public Colaborador? Colaborador { get; set; }
        [ForeignKey(nameof(Colaborador))]
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
