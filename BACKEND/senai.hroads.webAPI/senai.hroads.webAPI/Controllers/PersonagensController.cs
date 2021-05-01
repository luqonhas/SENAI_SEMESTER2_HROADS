using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.hroads.webAPI.Domains;
using senai.hroads.webAPI.Interfaces;
using senai.hroads.webAPI.Repositories;
using senai.hroads.webAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            try
            {
                return Ok(_personagemRepository.Listar());
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpGet("ordem")]
        public IActionResult GetOrderBy()
        {
            try
            {
                return Ok(_personagemRepository.ListarOrdenado());
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpGet("jogadores")]
        public IActionResult GetWithPlayers()
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                return Ok(_personagemRepository.ListarComJogadores(idUsuario));
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                PersonagemDomain personagemBuscado = _personagemRepository.BuscarPorId(id);

                if (personagemBuscado == null)
                {
                    return NotFound("Nenhum personagem encontrado!");
                }

                return Ok(personagemBuscado);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize(Roles = "JOGADOR")]
        [HttpPost]
        public IActionResult Post(PersonagemDomain novoPersonagem)
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                PersonagemDomain personagemBuscado = _personagemRepository.BuscarPorNome(novoPersonagem.nomePersonagem);

                if (personagemBuscado == null)
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
                        _personagemRepository.Cadastrar(novoPersonagem, idUsuario);

                    return StatusCode(201);
                }

                return BadRequest("Não foi possível cadastrar, nome de personagem já existente!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpPatch]
        public IActionResult Patch(int id, PersonagemViewModel personagemAtualizado)
        {
            try
            {
                PersonagemDomain personagemBuscado = _personagemRepository.BuscarPorId(id);

                PersonagemDomain personagemBuscadoNome = _personagemRepository.BuscarPorNome(personagemAtualizado.nomePersonagem);

                if (personagemBuscadoNome == null)
                {
                    if (personagemBuscado != null)
                    {
                        personagemBuscado = new PersonagemDomain
                        {
                            nomePersonagem = personagemAtualizado.nomePersonagem,
                            maxVida = personagemAtualizado.maxVida,
                            maxMana = personagemAtualizado.maxMana,
                            idClasse = personagemAtualizado.idClasse
                        };

                        _personagemRepository.Atualizar(id, personagemBuscado);

                        return StatusCode(204);
                    }

                    return NotFound("Personagem não encontrado!");
                }

                return NotFound("Não foi possível atualizar, nome de personagem já existente!");
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
            try
            {
                PersonagemDomain personagemBuscado = _personagemRepository.BuscarPorId(id);

                if (personagemBuscado != null)
                {
                    PersonagemDomain nomeBuscado = _personagemRepository.BuscarPorNome(personagemAtualizado.nomePersonagem);

                    if (nomeBuscado == null)
                    {
                        _personagemRepository.Atualizar(id, personagemAtualizado);

                        return StatusCode(204);
                    }
                    else
                        return BadRequest("Já existe um personagem com esse nome!");
                }

                return NotFound("Personagem não encontrado!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                PersonagemDomain personagemBuscado = _personagemRepository.BuscarPorId(id);

                if (personagemBuscado != null)
                {
                    _personagemRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Personagem não encontrado!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }


    }
}
