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
    public class TipoUsuariosController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuariosController()
        {
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TipoUsuarioDomain tipoBuscado = _tipoUsuarioRepository.BuscarPorId(id);

            if (tipoBuscado == null)
            {
                return NotFound("Nenhuma tipo de usuário encontrado!");
            }

            return Ok(tipoBuscado);
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(TipoUsuarioDomain novaTipo)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(novaTipo.permissao))
                {
                    return NotFound("Campo 'permissao' obrigatório!");
                }
                else
                    _tipoUsuarioRepository.Cadastrar(novaTipo);

                return StatusCode(201);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, TipoUsuarioDomain tipoAtualizada)
        {
            _tipoUsuarioRepository.Atualizar(id, tipoAtualizada);

            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tipoUsuarioRepository.Deletar(id);

            return StatusCode(204);
        }


    }
}
