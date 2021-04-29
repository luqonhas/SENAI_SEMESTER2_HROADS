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
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usuarioRepository.Listar());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (usuarioBuscado != null)
            {
                return Ok(usuarioBuscado);
            }

            return NotFound("Nenhum usuário encontrado!");
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(UsuarioDomain novoUsuario)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(novoUsuario.email))
                {
                    return NotFound("Campo 'email' obrigatório!");
                }
                if (String.IsNullOrWhiteSpace(novoUsuario.senha))
                {
                    return NotFound("Campo 'senha' obrigatório!");
                }
                if (String.IsNullOrWhiteSpace(novoUsuario.idTipoUsuario.ToString()))
                {
                    return NotFound("Campo 'idTipoUsuario' obrigatório!");
                }
                else
                    _usuarioRepository.Cadastrar(novoUsuario);

                return StatusCode(201);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, UsuarioDomain usuarioAtualizado)
        {
            _usuarioRepository.Atualizar(id, usuarioAtualizado);

            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usuarioRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
