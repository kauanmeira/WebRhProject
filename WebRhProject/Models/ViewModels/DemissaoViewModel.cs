namespace WebRhProject.Models.ViewModels
{
    public class DemissaoViewModel
    {
        public int ColaboradorId { get; set; }
        public List<Colaborador> Colaboradores { get; set; }
        public Colaborador Colaborador { get; set; } // Adicione esta propriedade

    }
}
