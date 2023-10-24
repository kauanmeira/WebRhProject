using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbBeneficioColaborador")]
    public class BeneficioColaborador
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Colaborador))]
        public int ColaboradorId { get; set; }
        [ForeignKey(nameof(Beneficio))]
        public int BeneficioId { get; set; }

        public Colaborador Colaborador { get; set; }
        public Beneficio Beneficio { get; set; }
    }
}
