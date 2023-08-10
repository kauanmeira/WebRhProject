using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebRhProject.Models
{
    [Table("TbCargo")]
    public class Cargo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [JsonIgnore]
        public ICollection<Colaborador>? Colaboradores { get; set; }

        public Cargo()
        {
            Colaboradores = new List<Colaborador>();
        }

        public Cargo(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Colaboradores = new List<Colaborador>();
        }
    }
}
