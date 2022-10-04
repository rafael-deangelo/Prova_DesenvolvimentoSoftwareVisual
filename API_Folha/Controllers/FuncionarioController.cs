using System;
using API_Folha.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API_Folha.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly DataContext _context; // Criar uma variável global

        // Criar o construtor da variavel global recebendo o context
        public FuncionarioController(DataContext context)
        {
            _context = context;
        }

        // GET: /api/funcionario/listar
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            return Ok(_context.Funcionarios.ToList());
        }

        // POST: /api/funcionario/cadastrar
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();   // Sempre tem que colocar os SaveChanges para cadastrar, deletar, alterar
            return Created("", funcionario);
        }

        // GET: /api/funcionario/buscar/{cpf}
        [HttpGet]
        [Route("buscar/{cpf}")] 
        public IActionResult Buscar([FromRoute] string cpf)
        {
            Funcionario funcionario = _context.Funcionarios.FirstOrDefault(f => f.Cpf.Equals(cpf));  // Tanto faz o que eu coloco no lugar do "f" é o mesmo que funcionarioCadastrado, eu invento
  
            return funcionario != null ? Ok(funcionario) : NotFound();  /// IF TERNÁRIO | Forma mais simplificada do if caso eu tenha que retornar 1 coisa apenas, se não volta pro if normal
        }

        // DELETE: /api/funcionario/deletar/{id}
        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            Funcionario funcionario = _context.Funcionarios.Find(id);
            if(funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                _context.SaveChanges();
                return Ok(funcionario);
            }
            return NotFound();
        }

        // PATCH: /api/funcionario/alterar
        [HttpPatch]
        [Route("alterar")]
        public IActionResult Alterar([FromBody] Funcionario funcionario)
        {
            try
            {
                _context.Funcionarios.Update(funcionario);
                _context.SaveChanges();
                return Ok(funcionario);
            }
            catch
            {
                return NotFound();
            }
            
        }
    }
}

