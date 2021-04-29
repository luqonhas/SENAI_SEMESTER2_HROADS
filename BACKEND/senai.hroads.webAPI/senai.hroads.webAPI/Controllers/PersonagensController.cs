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
    public class PersonagensController : ControllerBase
    {
        private IPersonagemRepository _personagemRepository;

        public PersonagensController()
        {
            _personagemRepository = new PersonagemRepository();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personagemRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            PersonagemDomain personagemBuscado = _personagemRepository.BuscarPorId(id);

            if (personagemBuscado != null)
            {
                return Ok(personagemBuscado);
            }

            return NotFound("Nenhum personagem encontrado!");
        }

        [Authorize(Roles = "2")]
        [HttpPost]
        public IActionResult Post(PersonagemDomain novoPersonagem)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(novoPersonagem.nomePersonagem))
                {
                    return NotFound("Campo 'nomePersonagem' obrigatório!");
                }
                if (String.IsNullOrWhiteSpace(novoPersonagem.idClasse.ToString()))
                {
                    return NotFound("Campo 'idClasse' obrigatório!");
                }
                if (String.IsNullOrWhiteSpace(novoPersonagem.maxVida.ToString()))
                {
                    return NotFound("Campo 'maxVida' obrigatório!");
                }
                if (String.IsNullOrWhiteSpace(novoPersonagem.maxMana.ToString()))
                {
                    return NotFound("Campo 'maxMana' obrigatório!");
                }
                else
                    _personagemRepository.Cadastrar(novoPersonagem);

                return StatusCode(201);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, PersonagemDomain personagemAtualizado)
        {
            _personagemRepository.Atualizar(id, personagemAtualizado);

            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personagemRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
