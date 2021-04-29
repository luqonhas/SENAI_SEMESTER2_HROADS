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

        public void Atualizar(int id, UsuarioDomain usuarioAtualizado)
        {
            UsuarioDomain usuarioBuscado = context.Usuarios.Find(id);

            if (usuarioAtualizado != null)
            {
                usuarioBuscado.email = usuarioAtualizado.email;
            }

            if (usuarioAtualizado != null)
            {
                usuarioBuscado.senha = usuarioAtualizado.senha;
            }

            context.Usuarios.Update(usuarioBuscado);

            context.SaveChanges();
        }

        public UsuarioDomain BuscarPorId(int id)
        {
            return context.Usuarios.Include(x => x.tipoUsuario).FirstOrDefault(x => x.idUsuario == id);
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            context.Usuarios.Add(novoUsuario);

            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            UsuarioDomain usuarioBuscado = context.Usuarios.Find(id);

            context.Usuarios.Remove(usuarioBuscado);

            context.SaveChanges();
        }

        public List<UsuarioDomain> Listar()
        {
            return context.Usuarios.Include(x => x.tipoUsuario).ToList();
        }


        public UsuarioDomain Logar(string email, string senha)
        {
            UsuarioDomain login = context.Usuarios.Include(x => x.tipoUsuario).FirstOrDefault(x => x.email == email && x.senha == senha);

            return login;
        }


    }
}