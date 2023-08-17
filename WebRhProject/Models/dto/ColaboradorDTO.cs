namespace WebRhProject.Models.dto
{
    public class ColaboradorDTO
    {

        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public double SalarioBase { get; set; }
        public DateTime DataNascimento { get; set; }    
        public DateTime DataAdmissao { get; set; }
        public int Dependentes { get; set; }
        public int Filhos { get; set; }
        public int CargoId { get; set; }
        public int EmpresaId { get; set; }
        public string CEP { get; set; }

    }

}
