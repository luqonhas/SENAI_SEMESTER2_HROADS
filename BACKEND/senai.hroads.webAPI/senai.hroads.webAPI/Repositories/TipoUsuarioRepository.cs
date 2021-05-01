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

        public bool Atualizar(int id, TipoUsuarioDomain tipoUsuarioAtualizado)
        {
            TipoUsuarioDomain tipoBuscada = BuscarPorId(id);

            TipoUsuarioDomain tipoBuscadaPermissao = context.TipoUsuarios.FirstOrDefault(x => x.permissao == tipoUsuarioAtualizado.permissao);

            if (tipoUsuarioAtualizado.permissao != null && tipoBuscadaPermissao == null)
            {
                tipoBuscada.permissao = tipoUsuarioAtualizado.permissao;

                context.TipoUsuarios.Update(tipoBuscada);

                context.SaveChanges();

                return true;
            }

            return false;
        }

        public TipoUsuarioDomain BuscarPorId(int id)
        {
            return context.TipoUsuarios.FirstOrDefault(x => x.idTipoUsuario == id);
        }

        public TipoUsuarioDomain BuscarPorNome(string permissao)
        {
            TipoUsuarioDomain tipoBuscada = context.TipoUsuarios.FirstOrDefault(x => x.permissao == permissao);

            if (tipoBuscada != null)
            {
                return tipoBuscada;
            }

            return null;
        }

        public void Cadastrar(TipoUsuarioDomain novoTipoUsuario)
        {
            context.TipoUsuarios.Add(novoTipoUsuario);

            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            context.TipoUsuarios.Remove(BuscarPorId(id));

            context.SaveChanges();
        }

        public List<TipoUsuarioDomain> Listar()
        {
            return context.TipoUsuarios.ToList();
        }

    }
}
