using Microsoft.EntityFrameworkCore;
using senai.hroads.webAPI.Contexts;
using senai.hroads.webAPI.Domains;
using senai.hroads.webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        HroadsContext context = new HroadsContext();

        public bool Atualizar(int id, UsuarioDomain usuarioAtualizado)
        {
            UsuarioDomain usuarioBuscada = BuscarPorId(id);

            UsuarioDomain usuarioBuscadaEmail = context.Usuarios.FirstOrDefault(x => x.email == usuarioAtualizado.email);

            if (usuarioAtualizado.email != null && usuarioBuscadaEmail == null && usuarioAtualizado.senha != null)
            {
                usuarioBuscada.email = usuarioAtualizado.email;

                usuarioBuscada.senha = usuarioAtualizado.senha;

                context.Usuarios.Update(usuarioBuscada);

                context.SaveChanges();

                return true;
            }

            return false;
        }

        public UsuarioDomain BuscarPorId(int id)
        {
            return context.Usuarios.Include(x => x.tipoUsuario).FirstOrDefault(x => x.idUsuario == id);
        }

        public UsuarioDomain BuscarPorEmail(string email)
        {
            UsuarioDomain usuarioBuscado = context.Usuarios.FirstOrDefault(x => x.email == email);

            if (usuarioBuscado != null)
            {
                return usuarioBuscado;
            }

            return null;
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            context.Usuarios.Add(novoUsuario);

            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            context.Usuarios.Remove(BuscarPorId(id));

            context.SaveChanges();
        }

        public List<UsuarioDomain> Listar()
        {
            return context.Usuarios.Include(x => x.tipoUsuario).ToList();
        }

        public List<UsuarioDomain> ListarSemSenha()
        {
            var usuarioSemSenha = context.Usuarios
            .Include(x => x.tipoUsuario)
            .Select(x => new UsuarioDomain()
            {
                idUsuario = x.idUsuario,
                email = x.email,
                idTipoUsuario = x.idTipoUsuario,
                tipoUsuario = x.tipoUsuario
            })
            .Where(x => x.tipoUsuario.idTipoUsuario == 2);

            return usuarioSemSenha.ToList();
        }

        public List<UsuarioDomain> ListarComPersonagens(int id)
        {
            return context.Usuarios
                .Include(x => x.tipoUsuario)
                .Include(x => x.personagens)
                .Where(x => x.idUsuario == id)
                .ToList();
        }

        public UsuarioDomain Logar(string email, string senha)
        {
            UsuarioDomain login = context.Usuarios.Include(x => x.tipoUsuario).FirstOrDefault(x => x.email == email && x.senha == senha);

            return login;
        }


    }
}