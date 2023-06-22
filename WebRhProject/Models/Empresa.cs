using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbEmpresa")]
    public class Empresa
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }
        public List<Colaborador>? Colaboradores { get; set; }

        public Empresa()
        {

        }

        public Empresa(int id, string cnpj, string razaoSocial, string nomeFantasia)
        {
            Id = id;
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
        }
    }
}
