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
    public class TipoHabilidadesController : ControllerBase
    {
        private ITipoHabilidadeRepository _tipoHabilidadeRepository;

        public TipoHabilidadesController()
        {
            _tipoHabilidadeRepository = new TipoHabilidadeRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_tipoHabilidadeRepository.Listar());
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
                TipoHabilidadeDomain tipoBuscada = _tipoHabilidadeRepository.BuscarPorId(id);

                if (tipoBuscada == null)
                {
                    return NotFound("Nenhum tipo de habilidade encontrada!");
                }

                return Ok(tipoBuscada);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(TipoHabilidadeDomain novoTipo)
        {
            try
            {
                TipoHabilidadeDomain tipoBuscada = _tipoHabilidadeRepository.BuscarPorNome(novoTipo.nomeTipoHabilidade);

                if (tipoBuscada == null)
                {
                    if (String.IsNullOrWhiteSpace(novoTipo.nomeTipoHabilidade))
                    {
                        return NotFound("Campo 'nomeTipoHabilidade' obrigatório!");
                    }
                    else
                        _tipoHabilidadeRepository.Cadastrar(novoTipo);

                    return StatusCode(201);
                }

                return BadRequest("Não foi possível cadastrar, tipo de habilidade já existente!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, TipoHabilidadeDomain tipoAtualizado)
        {
            try
            {
                TipoHabilidadeDomain tipoBuscada = _tipoHabilidadeRepository.BuscarPorId(id);

                if (tipoBuscada != null)
                {
                    TipoHabilidadeDomain nomeBuscado = _tipoHabilidadeRepository.BuscarPorNome(tipoAtualizado.nomeTipoHabilidade);

                    if (nomeBuscado == null)
                    {
                        _tipoHabilidadeRepository.Atualizar(id, tipoAtualizado);

                        return StatusCode(204);
                    }
                    else
                        return BadRequest("Já existe um tipo de habilidade com esse nome!");
                }

                return NotFound("Tipo de habilidade não encontrada!");
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
                TipoHabilidadeDomain tipoBuscada = _tipoHabilidadeRepository.BuscarPorId(id);

                if (tipoBuscada != null)
                {
                    _tipoHabilidadeRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Tipo de habilidade não encontrada!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }


    }
}
