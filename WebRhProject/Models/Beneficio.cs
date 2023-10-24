using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbBeneficio")]
    public class Beneficio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
    }
}
