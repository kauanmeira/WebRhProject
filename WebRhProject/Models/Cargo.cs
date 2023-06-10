using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbCargo")]
    public class Cargo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Cargo()
        {

        }

        public Cargo(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public static implicit operator Cargo(List<Cargo> v)
        {
            throw new NotImplementedException();
        }
    }
}
