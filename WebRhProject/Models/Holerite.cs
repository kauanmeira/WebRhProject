using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRhProject.Models
{
    [Table("TbHolerite")]
    public class Holerite
    {
        public int Id { get; set; }
        public Colaborador? Colaborador { get; set; }
        [ForeignKey(nameof(Colaborador))]
        public int ColaboradorId { get; set; }
        public int MesAno { get; set; }
        public double SalarioBruto { get; set; }
        public double DescontoINSS { get; set; }
        public double DescontoIRRF { get; set; }
        public double HorasNormais { get; set; }
        public double SalarioLiquido { get; set; }
        public int DependentesHolerite { get; set; }
        public int? Tipo { get; set; }

        public Holerite()
        {

        }

        public Holerite(int id, Colaborador colaborador, int mesAno, double salarioBruto, double descontoInss, double descontoIrrf, double horasNormais, double salarioLiquido, int tipo)
        {
            Id = id;
            Colaborador = colaborador;
            MesAno = mesAno;
            SalarioBruto = salarioBruto;
            DescontoINSS = descontoInss;
            DescontoIRRF = descontoIrrf;
            HorasNormais = horasNormais;
            SalarioLiquido = salarioLiquido;
            Tipo = tipo;
        }
        public void CalcularSalarioBruto()
        {
            if (Colaborador != null)
            {
                SalarioBruto = Colaborador.SalarioBase;
            }
        }


        public void CalcularDescontoInss()
        {
            if (Colaborador != null)
            {

                double salarioContribuicao = Colaborador.SalarioBase;

                if (salarioContribuicao <= 1320.00)
                {
                    DescontoINSS = salarioContribuicao * 0.075; // 7.5%
                }
                else if (salarioContribuicao <= 2571.29)
                {
                    DescontoINSS = salarioContribuicao * 0.09; // 9%
                }
                else if (salarioContribuicao <= 3856.94)
                {
                    DescontoINSS = salarioContribuicao * 0.12; // 12%
                }
                else if (salarioContribuicao <= 7507.49)
                {
                    DescontoINSS = salarioContribuicao * 0.14; // 14%
                }
                else
                {
                    DescontoINSS = 7507.49 * 0.14; // Valor máximo para a alíquota de 14%
                }
            }
        }

        public void CalcularDescontoIrrf()
        {
            if (Colaborador != null)
            {
                double salarioBase = Colaborador.SalarioBase;
                double salarioLiquido = SalarioLiquido;

                double salarioDescontoInss = salarioBase - DescontoINSS;

                double baseCalculoIrrf = salarioDescontoInss - DependentesHolerite * 189.59;

                if (baseCalculoIrrf <= 1903.98)
                {
                    DescontoIRRF = 0; // Isento
                }
                else if (baseCalculoIrrf <= 2826.65)
                {
                    DescontoIRRF = (baseCalculoIrrf * 0.075) - 142.80;
                }
                else if (baseCalculoIrrf <= 3751.05)
                {
                    DescontoIRRF = (baseCalculoIrrf * 0.15) - 354.80;
                }
                else if (baseCalculoIrrf <= 4664.68)
                {
                    DescontoIRRF = (baseCalculoIrrf * 0.225) - 636.13;
                }
                else
                {
                    DescontoIRRF = (baseCalculoIrrf * 0.275) - 869.36;
                }

                // Verificar se o desconto do IRRF ultrapassa o salário líquido
                if (DescontoIRRF > salarioLiquido)
                {
                    DescontoIRRF = salarioLiquido; // Limitar o desconto ao valor do salário líquido
                }

            }
        }
        public void CalcularSalarioLiquido()
        {
            if (Colaborador != null)
            {
                SalarioLiquido = SalarioBruto - DescontoINSS - DescontoIRRF;
            }
        }

        public void CalcularHolerite()
        {
            if (Colaborador != null)
            {
                CalcularSalarioBruto();
                CalcularDescontoInss();
                CalcularDescontoIrrf();
                CalcularSalarioLiquido();
            }

        }
    }
}
