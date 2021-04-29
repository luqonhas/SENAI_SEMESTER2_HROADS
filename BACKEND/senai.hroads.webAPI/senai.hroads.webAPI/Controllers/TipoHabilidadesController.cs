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
            return Ok(_tipoHabilidadeRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TipoHabilidadeDomain tipoHabilidadeBuscado = _tipoHabilidadeRepository.BuscarPorId(id);

            if (tipoHabilidadeBuscado != null)
            {
                return Ok(tipoHabilidadeBuscado);
            }

            return NotFound("Nenhum tipo de habilidade encontrada! :c");
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(TipoHabilidadeDomain novoTipo)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(novoTipo.nomeTipoHabilidade))
                {
                    return NotFound("Campo 'nomeTipoHabilidade' obrigatório!");
                }
                else
                    _tipoHabilidadeRepository.Cadastrar(novoTipo);

                return StatusCode(201);
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
            _tipoHabilidadeRepository.Atualizar(id, tipoAtualizado);

            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tipoHabilidadeRepository.Deletar(id);

            return StatusCode(204);
        }


    }
}
