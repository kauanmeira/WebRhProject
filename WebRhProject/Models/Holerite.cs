using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbHolerite")]
    public class Holerite
    {
        public int Id { get; set; }
        public Colaborador Colaborador { get; set; }
        public DateTime Data { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal Descontos { get; set; }
        public decimal Beneficios { get; set; }
    }
}
