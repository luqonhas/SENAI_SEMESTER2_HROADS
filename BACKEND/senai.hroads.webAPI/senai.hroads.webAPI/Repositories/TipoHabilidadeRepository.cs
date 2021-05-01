using senai.hroads.webAPI.Contexts;
using senai.hroads.webAPI.Domains;
using senai.hroads.webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Repositories
{
    public class TipoHabilidadeRepository : ITipoHabilidadeRepository
    {
        HroadsContext context = new HroadsContext();

        public bool Atualizar(int id, TipoHabilidadeDomain tipoHabilidadeAtualizado)
        {
            TipoHabilidadeDomain tipoBuscada = BuscarPorId(id);

            TipoHabilidadeDomain tipoBuscadaNome = context.TipoHabilidades.FirstOrDefault(x => x.nomeTipoHabilidade == tipoHabilidadeAtualizado.nomeTipoHabilidade);

            if (tipoHabilidadeAtualizado.nomeTipoHabilidade != null && tipoBuscadaNome == null)
            {
                tipoBuscada.nomeTipoHabilidade = tipoHabilidadeAtualizado.nomeTipoHabilidade;

                context.TipoHabilidades.Update(tipoBuscada);

                context.SaveChanges();

                return true;
            }

            return false;
        }

        public TipoHabilidadeDomain BuscarPorId(int id)
        {
            return context.TipoHabilidades.FirstOrDefault(x => x.idTipoHabilidade == id);
        }

        public TipoHabilidadeDomain BuscarPorNome(string nome)
        {
            TipoHabilidadeDomain tipoBuscada = context.TipoHabilidades.FirstOrDefault(x => x.nomeTipoHabilidade == nome);

            if (tipoBuscada != null)
            {
                return tipoBuscada;
            }

            return null;
        }

        public void Cadastrar(TipoHabilidadeDomain novoTipoHabilidade)
        {
            context.TipoHabilidades.Add(novoTipoHabilidade);

            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            context.TipoHabilidades.Remove(BuscarPorId(id));

            context.SaveChanges();
        }

        public List<TipoHabilidadeDomain> Listar()
        {
            return context.TipoHabilidades.ToList();
        }

    }
}
