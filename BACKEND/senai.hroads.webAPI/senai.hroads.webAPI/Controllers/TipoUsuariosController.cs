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
            try
            {
                return Ok(_tipoUsuarioRepository.Listar());
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
                TipoUsuarioDomain tipoBuscada = _tipoUsuarioRepository.BuscarPorId(id);

                if (tipoBuscada == null)
                {
                    return NotFound("Nenhum tipo de usuário encontrado!");
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
        public IActionResult Post(TipoUsuarioDomain novoTipo)
        {
            try
            {
                TipoUsuarioDomain tipoBuscada = _tipoUsuarioRepository.BuscarPorNome(novoTipo.permissao);

                if (tipoBuscada == null)
                {
                    if (String.IsNullOrWhiteSpace(novoTipo.permissao))
                    {
                        return NotFound("Campo 'permissao' obrigatório!");
                    }
                    else
                        _tipoUsuarioRepository.Cadastrar(novoTipo);

                    return StatusCode(201);
                }

                return BadRequest("Não foi possível cadastrar, tipo de usuário já existente!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, TipoUsuarioDomain tipoAtualizado)
        {
            try
            {
                TipoUsuarioDomain tipoBuscada = _tipoUsuarioRepository.BuscarPorId(id);

                if (tipoBuscada != null)
                {
                    TipoUsuarioDomain permissaoBuscada = _tipoUsuarioRepository.BuscarPorNome(tipoAtualizado.permissao);

                    if (permissaoBuscada == null)
                    {
                        _tipoUsuarioRepository.Atualizar(id, tipoAtualizado);

                        return StatusCode(204);
                    }
                    else
                        return BadRequest("Já existe um tipo de usuário com esse nome!");
                }

                return NotFound("Tipo de usuário não encontrado!");
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
                TipoUsuarioDomain tipoBuscada = _tipoUsuarioRepository.BuscarPorId(id);

                if (tipoBuscada != null)
                {
                    _tipoUsuarioRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Tipo de usuário não encontrado!");
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }


    }
}
