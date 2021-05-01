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
            try
            {
                return Ok(_classeRepository.Listar());
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
                ClasseDomain classeBuscada = _classeRepository.BuscarPorId(id);

                if (classeBuscada == null)
                {
                    return NotFound("Nenhuma classe encontrada!");
                }

                return Ok(classeBuscada);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(ClasseDomain novaClasse)
        {
            try
            {
                // busca por um "nomeClasse" existente
                ClasseDomain classeBuscada = _classeRepository.BuscarPorNome(novaClasse.nomeClasse);

                // se na "classeBuscada" não conter um "nomeClasse" já existente...
                if (classeBuscada == null)
                {
                    // cadastra uma nova classe
                    _classeRepository.Cadastrar(novaClasse);

                    return StatusCode(201);
                }

                // se já existir uma classe com esse nome, retorna um BadRequest
                return BadRequest("Não foi possível cadastrar, classe já existente!");
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
            try
            {
                // busca pelo id colocado
                ClasseDomain classeBuscada = _classeRepository.BuscarPorId(id);

                // se o id na "classeBuscada" não possuir nenhuma classe...
                if (classeBuscada != null)
                {
                    // busca se o novo nome existe no BD
                    ClasseDomain nomeBuscado = _classeRepository.BuscarPorNome(classeAtualizada.nomeClasse);

                    // se NÃO existir...
                    if (nomeBuscado == null)
                    {
                        // cria normal
                        _classeRepository.Atualizar(id, classeAtualizada);

                        return StatusCode(204);
                    }
                    // se existir...
                    else
                        return BadRequest("Já existe uma classe com esse nome!");
                }

                // se não existir uma classe com o id colocado...
                return NotFound("Classe não encontrada!");

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
                ClasseDomain classeBuscada = _classeRepository.BuscarPorId(id);

                // se o id buscado existir...
                if (classeBuscada != null)
                {
                    // apaga
                    _classeRepository.Deletar(id);

                    return StatusCode(204);
                }

                // se não...
                return NotFound("Classe não encontrada!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

    }
}