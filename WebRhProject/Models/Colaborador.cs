using System.ComponentModel.DataAnnotations.Schema;
using WebRhProject.Models;

namespace WebRhProject.Models
{
    [Table("TbColaborador")]
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public double SalarioBase { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public int Dependentes { get; set; }
        public int Filhos { get; set; }
        public Cargo Cargo { get; set; }
        [ForeignKey(nameof(Cargo))]
        public int CargoId { get; set; }
        public Colaborador()
        {

        }

        public Colaborador(int id, string nome, string sobrenome, double salarioBase, DateTime dataNascimento, DateTime dataAdmissao, int dependentes, int filhos, Cargo cargo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            SalarioBase = salarioBase;
            DataNascimento = dataNascimento;
            DataAdmissao = dataAdmissao;
            Dependentes = dependentes;
            Filhos = filhos;
            Cargo = cargo;
        }
    }
}
