using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebRhProject.Models.ViewModels
{
    public class ColaboradorFormViewModel
    {
        public Colaborador Colaborador { get; set; }
        public ICollection<Cargo> Cargos { get; set; }
        public ICollection<Empresa> Empresas { get; set; }
    }
}