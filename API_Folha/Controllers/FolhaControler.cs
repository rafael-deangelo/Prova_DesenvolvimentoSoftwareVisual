using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using API_Folha.Models;

namespace API_Folha.Controllers
{
    [ApiController]
    [Route("api/folha")]
    public class FolhaController : ControllerBase
    {
        private readonly DataContext _context; // Criar uma variável global

        // Criar o construtor da variavel global recebendo o context
        public FolhaController(DataContext context)
        {
            _context = context;
        }

        // GET: /api/folha/listar
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar() => Ok(_context.FolhaPagamentos.Include(a => a.Funcionario).ToList());


        // POST: /api/folha/cadastrar
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] FolhaPagamento folhaPagamento)
        {
            folhaPagamento.Bruto = folhaPagamento.QuantidadeHoras * folhaPagamento.ValorHora;

            if(folhaPagamento.Bruto < 1903.98){
                folhaPagamento.ImpostoRenda = 0;
            }else if(folhaPagamento.Bruto > 1903.99 && folhaPagamento.Bruto < 2826.65){
                folhaPagamento.ImpostoRenda = 142.80;
            }else if(folhaPagamento.Bruto > 2826.66 && folhaPagamento.Bruto < 3751.05){
                folhaPagamento.ImpostoRenda = 354.80;
            }else if(folhaPagamento.Bruto > 3751.06 && folhaPagamento.Bruto < 4664.68){
                folhaPagamento.ImpostoRenda = 636.13;
            }else{
                folhaPagamento.ImpostoRenda = 869.36;
            }

            if(folhaPagamento.Bruto < 1693.72){
                folhaPagamento.Inss = folhaPagamento.Bruto % 8;
            }else if(folhaPagamento.Bruto > 1693.73 && folhaPagamento.Bruto < 2822.90){
                folhaPagamento.Inss = folhaPagamento.Bruto % 9;
            }else if(folhaPagamento.Bruto > 2822.91  && folhaPagamento.Bruto < 5645.80){
                folhaPagamento.Inss = folhaPagamento.Bruto % 11;
            }else{
                folhaPagamento.Inss = folhaPagamento.Bruto - 621.03;
            }

            folhaPagamento.Fgts = folhaPagamento.Bruto % 8;

            folhaPagamento.Liquido = folhaPagamento.Bruto - folhaPagamento.Inss - folhaPagamento.ImpostoRenda;

            if(folhaPagamento != null && _context.Funcionarios.Any(a => a.FuncionarioId == folhaPagamento.FuncionarioId))
            {
                _context.FolhaPagamentos.Add(folhaPagamento);
                _context.SaveChanges();
                return Created("", folhaPagamento);
            }
            else
                return NotFound("Funcionario não cadastrado no sistema de folha de pagamento!");
        }

        // GET: /api/folhas/buscar/{cpf}, {nascimento}
        [HttpGet]
        [Route("buscar/{cpf}/{nascimento}")]
        public IActionResult Buscar([FromRoute] string cpf, DateTime nascimento)
        {
            FolhaPagamento folhaPagamento = _context.FolhaPagamentos.Include(a => a.Funcionario).FirstOrDefault(a => a.Funcionario.Cpf.Equals(cpf) && a.Funcionario.Nascimento.Equals(nascimento));
            return folhaPagamento != null ? Ok(folhaPagamento) : NotFound();
        }

        // GET: /api/folhas/filtrar/{cpf}, {nascimento}
        [HttpGet]
        [Route("filtrar/{nascimento}")]
        public IActionResult Filtrar([FromRoute] DateTime nascimento)
        {
            FolhaPagamento folhaPagamento = _context.FolhaPagamentos.Include(a => a.Funcionario).FirstOrDefault(a => a.Funcionario.Nascimento.Equals(nascimento));
            return folhaPagamento != null ? Ok(folhaPagamento) : NotFound();
        }

    }
}

