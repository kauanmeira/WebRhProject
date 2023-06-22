using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebRhProject.Models.enums;

namespace WebRhProject.Models.ViewModels
{
    public class UsuarioFormViewModel
    {
        public Usuario Usuario { get; set; }
        public ICollection<Colaborador> Colaboradores { get; set; }
        public string ConfirmarSenha { get; set; }
        public Tipo TipoSelecionado { get; set; }
        public List<SelectListItem> Tipos { get; set; }

    }
}
