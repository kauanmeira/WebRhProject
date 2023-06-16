using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbEmpresa")]
    public class Empresa
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }

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
