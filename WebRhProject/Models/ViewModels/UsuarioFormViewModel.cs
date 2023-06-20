using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebRhProject.Models.ViewModels
{
    public class UsuarioFormViewModel
    {
        public Usuario Usuario { get; set; }
        public ICollection<Colaborador> Colaboradores { get; set; }
        public string ConfirmarSenha { get; set; }

    }
}