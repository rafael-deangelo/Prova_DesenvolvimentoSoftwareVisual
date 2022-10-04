using System.Reflection;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_Folha.Models;
using System.Collections.Generic;

namespace API_Folha.Models
{

    public class FolhaPagamento
    {
        public FolhaPagamento () => CriadoEm = DateTime.Now;
        public int FolhaPagamentoId { get; set; }
        public int ValorHora { get; set; }
        public int QuantidadeHoras { get; set; }
        public int Bruto { get; set; }
        public Double ImpostoRenda { get; set; }
        public Double Inss { get; set; }
        public Double Fgts { get; set; }
        public Double Liquido { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}