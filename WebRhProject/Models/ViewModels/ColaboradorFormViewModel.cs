using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebRhProject.Models.ViewModels
{
    public class ColaboradorFormViewModel
    {
        public Colaborador Colaborador { get; set; }
        public ICollection<Cargo> Cargos { get; set; }
        public ICollection<Empresa> Empresas { get; set; }
        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString = "R$ {0:#,##0.00}")]
        public decimal SalarioBase { get; set; }
    }
}