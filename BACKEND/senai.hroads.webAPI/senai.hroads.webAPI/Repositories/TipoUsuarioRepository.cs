using senai.hroads.webAPI.Contexts;
using senai.hroads.webAPI.Domains;
using senai.hroads.webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        HroadsContext context = new HroadsContext();

        public void Atualizar(int id, TipoUsuarioDomain tipoUsuarioAtualizado)
        {
            TipoUsuarioDomain tipoBuscado = context.TipoUsuarios.Find(id);

            if (tipoUsuarioAtualizado != null)
            {
                tipoBuscado.permissao = tipoUsuarioAtualizado.permissao;
            }

            context.TipoUsuarios.Update(tipoBuscado);

            context.SaveChanges();
        }

        public TipoUsuarioDomain BuscarPorId(int id)
        {
            return context.TipoUsuarios.FirstOrDefault(x => x.idTipoUsuario == id);
        }

        public void Cadastrar(TipoUsuarioDomain novoTipoUsuario)
        {
            context.TipoUsuarios.Add(novoTipoUsuario);

            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            TipoUsuarioDomain tipoBuscado = context.TipoUsuarios.Find(id);

            context.TipoUsuarios.Remove(tipoBuscado);

            context.SaveChanges();
        }

        public List<TipoUsuarioDomain> Listar()
        {
            return context.TipoUsuarios.ToList();
        }
    }
}
