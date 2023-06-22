using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbHolerite")]
    public class Holerite
    {
        public int Id { get; set; }
        public Colaborador Colaborador { get; set; }
        [ForeignKey(nameof(Colaborador))]
        public int ColaboradorId { get; set; }
        public int Mes { get; set; }
        public double SalarioBruto { get; set; }
        public double DescInss { get; set; }
        public double DescIrrf { get; set; }
        public double HorasNormais { get; set; }
        public double SalarioLiquido { get; set; }
        public int Dependentes { get; set; }
        public int Tipo { get; set; }

        public Holerite()
        {

        }

        public Holerite(int id, Colaborador colaborador, int mes, double salarioBruto, double descInss, double descIrrf, double horasNormais, double salarioLiquido, int tipo)
        {
            Id = id;
            Colaborador = colaborador;
            Mes = mes;
            SalarioBruto = salarioBruto;
            DescInss = descInss;
            DescIrrf = descIrrf;
            HorasNormais = horasNormais;
            SalarioLiquido = salarioLiquido;
            Tipo = tipo;
        }
        public void CalcularDescontoInss()
        {
            double salarioContribuicao = Colaborador.SalarioBase;

            if (salarioContribuicao <= 1320.00)
            {
                DescInss = salarioContribuicao * 0.075; // 7.5%
            }
            else if (salarioContribuicao <= 2571.29)
            {
                DescInss = salarioContribuicao * 0.09; // 9%
            }
            else if (salarioContribuicao <= 3856.94)
            {
                DescInss = salarioContribuicao * 0.12; // 12%
            }
            else if (salarioContribuicao <= 7507.49)
            {
                DescInss = salarioContribuicao * 0.14; // 14%
            }
            else
            {
                DescInss = 7507.49 * 0.14; // Valor máximo para a alíquota de 14%
            }
        }

        public void CalcularDescontoIrrf()
        {
            double salarioBase = Colaborador.SalarioBase;
            double salarioLiquido = SalarioLiquido;

            double salarioDescontoInss = salarioBase - DescInss;

            double baseCalculoIrrf = salarioDescontoInss - Dependentes * 189.59;

            if (baseCalculoIrrf <= 1903.98)
            {
                DescIrrf = 0; // Isento
            }
            else if (baseCalculoIrrf <= 2826.65)
            {
                DescIrrf = (baseCalculoIrrf * 0.075) - 142.80;
            }
            else if (baseCalculoIrrf <= 3751.05)
            {
                DescIrrf = (baseCalculoIrrf * 0.15) - 354.80;
            }
            else if (baseCalculoIrrf <= 4664.68)
            {
                DescIrrf = (baseCalculoIrrf * 0.225) - 636.13;
            }
            else
            {
                DescIrrf = (baseCalculoIrrf * 0.275) - 869.36;
            }

            // Verificar se o desconto do IRRF ultrapassa o salário líquido
            if (DescIrrf > salarioLiquido)
            {
                DescIrrf = salarioLiquido; // Limitar o desconto ao valor do salário líquido
            }
        }
    }
}
