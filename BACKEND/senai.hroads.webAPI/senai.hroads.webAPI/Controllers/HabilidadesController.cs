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
            return Ok(_habilidadeRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            HabilidadeDomain habilidadeBuscada = _habilidadeRepository.BuscarPorId(id);

            if (habilidadeBuscada != null)
            {
                return Ok(habilidadeBuscada);
            }

            return NotFound("Nenhuma habilidade encontrada! :c");
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(HabilidadeDomain novaHabilidade)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(novaHabilidade.nomeHabilidade))
                {
                    return NotFound("Campo 'nomeHabilidade' obrigatório!");
                }
                if (String.IsNullOrWhiteSpace(novaHabilidade.idTipoHabilidade.ToString()))
                {
                    return NotFound("Campo 'idTipoHabilidade' obrigatório!");
                }
                else
                    _habilidadeRepository.Cadastrar(novaHabilidade);

                return StatusCode(201);
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
            _habilidadeRepository.Atualizar(id, habilidadeAtualizada);

            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _habilidadeRepository.Deletar(id);

            return StatusCode(204);
        }


    }
}
