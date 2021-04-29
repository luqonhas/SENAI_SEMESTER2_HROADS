using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.hroads.webAPI.Domains;
using senai.hroads.webAPI.Interfaces;
using senai.hroads.webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private IClasseRepository _classeRepository;

        public ClassesController()
        {
            _classeRepository = new ClasseRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_classeRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ClasseDomain classeBuscada = _classeRepository.BuscarPorId(id);

            if (classeBuscada == null)
            {
                return NotFound("Nenhuma classe encontrada!");
            }

            return Ok(classeBuscada);
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(ClasseDomain novaClasse)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(novaClasse.nomeClasse))
                {
                    return NotFound("Campo 'nomeClasse' obrigatório!");
                }
                else
                    _classeRepository.Cadastrar(novaClasse);

                return StatusCode(201);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, ClasseDomain classeAtualizada)
        {
            _classeRepository.Atualizar(id, classeAtualizada);

            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _classeRepository.Deletar(id);

            return StatusCode(204);
        }

    }
}