﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using WebRhProject.Models;

namespace WebRhProject.Models
{
    [Table("TbColaborador")]
    public class Colaborador
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} size must be between {2} and {1} caracters")]
        public string Nome { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} size must be between {2} and {1} caracters")]
        public string Sobrenome { get; set; }
        [Display(Name = "Salário Base")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double SalarioBase { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Data de Admissão")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataAdmissao { get; set; }
        public int Dependentes { get; set; }
        public int Filhos { get; set; }
        public Cargo Cargo { get; set; }
        [ForeignKey(nameof(Cargo))]
        public int CargoId { get; set; }

        public Empresa Empresa { get; set; }

        [ForeignKey(nameof(Empresa))]
        public int EmpresaId { get; set; }
        public Colaborador()
        {

        }

        public Colaborador(int id, string nome, string sobrenome, double salarioBase, DateTime dataNascimento, DateTime dataAdmissao, int dependentes, int filhos, Cargo cargo, Empresa empresa)
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
            Empresa = empresa;
        }
    }
}
