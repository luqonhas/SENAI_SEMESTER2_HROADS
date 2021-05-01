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
    public class HabilidadesController : ControllerBase
    {
        private IHabilidadeRepository _habilidadeRepository;

        public HabilidadesController()
        {
            _habilidadeRepository = new HabilidadeRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_habilidadeRepository.Listar());
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
                HabilidadeDomain habilidadeBuscada = _habilidadeRepository.BuscarPorId(id);

                if (habilidadeBuscada == null)
                {
                    return NotFound("Nenhuma habilidade encontrada!");
                }

                return Ok(habilidadeBuscada);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(HabilidadeDomain novaHabilidade)
        {
            try
            {
                HabilidadeDomain habilidadeBuscada = _habilidadeRepository.BuscarPorNome(novaHabilidade.nomeHabilidade);

                if (habilidadeBuscada == null)
                {
                    _habilidadeRepository.Cadastrar(novaHabilidade);

                    return StatusCode(201);
                }

                return BadRequest("Não foi possível cadastrar, habilidade já existente!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, HabilidadeDomain habilidadeAtualizada)
        {
            try
            {
                HabilidadeDomain habilidadeBuscada = _habilidadeRepository.BuscarPorId(id);

                if (habilidadeBuscada != null)
                {
                    HabilidadeDomain nomeBuscado = _habilidadeRepository.BuscarPorNome(habilidadeAtualizada.nomeHabilidade);

                    if (nomeBuscado == null)
                    {
                        _habilidadeRepository.Atualizar(id, habilidadeAtualizada);

                        return StatusCode(204);
                    }
                    else
                        return BadRequest("Já existe uma habilidade com esse nome!");
                }

                return NotFound("Habilidade não encontrada!");
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
                HabilidadeDomain habilidadeBuscada = _habilidadeRepository.BuscarPorId(id);

                if (habilidadeBuscada != null)
                {
                    _habilidadeRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Habilidade não encontrada!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }


    }
}
