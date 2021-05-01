using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.hroads.webAPI.Domains;
using senai.hroads.webAPI.Interfaces;
using senai.hroads.webAPI.Repositories;
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
            try
            {
                return Ok(_usuarioRepository.Listar());
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpGet("personagens")]
        public IActionResult GetWithCharacters()
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
                
                return Ok(_usuarioRepository.ListarComPersonagens(id));
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpGet("senha")]
        public IActionResult GetWithoutPassword()
        {
            try
            {
                return Ok(_usuarioRepository.ListarSemSenha());
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                UsuarioDomain usuarioBuscada = _usuarioRepository.BuscarPorId(id);

                if (usuarioBuscada == null)
                {
                    return NotFound("Nenhum usuário encontrado!");
                }

                return Ok(usuarioBuscada);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(UsuarioDomain novoUsuario)
        {
            try
            {
                UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorEmail(novoUsuario.email);

                if (usuarioBuscado == null)
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

                return BadRequest("Não foi possível cadastrar, e-mail já existente!");
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
            try
            {
                UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

                if (usuarioBuscado != null)
                {
                    UsuarioDomain emailBuscado = _usuarioRepository.BuscarPorEmail(usuarioBuscado.email);

                    if (emailBuscado == null)
                    {
                        _usuarioRepository.Atualizar(id, usuarioAtualizado);

                        return StatusCode(204);
                    }
                    else
                        return BadRequest("Já existe um usuário com esse e-mail!");
                }

                return NotFound("E-mail não encontrado!");
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
                UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

                if (usuarioBuscado != null)
                {
                    _usuarioRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Usuário não encontrado!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }
    }
}
