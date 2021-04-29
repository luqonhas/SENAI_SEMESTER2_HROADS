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

        public void Atualizar(int id, TipoHabilidadeDomain tipoHabilidadeAtualizado)
        {
            TipoHabilidadeDomain tipoHabilidadeBuscado = context.TipoHabilidades.Find(id);

            if (tipoHabilidadeAtualizado != null)
            {
                tipoHabilidadeBuscado.nomeTipoHabilidade = tipoHabilidadeAtualizado.nomeTipoHabilidade;
            }

            context.TipoHabilidades.Update(tipoHabilidadeBuscado);

            context.SaveChanges();
        }

        public TipoHabilidadeDomain BuscarPorId(int id)
        {
            return context.TipoHabilidades.FirstOrDefault(x => x.idTipoHabilidade == id);
        }

        public void Cadastrar(TipoHabilidadeDomain novoTipoHabilidade)
        {
            context.TipoHabilidades.Add(novoTipoHabilidade);

            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            TipoHabilidadeDomain tipoHabilidadeBuscado = context.TipoHabilidades.Find(id);

            context.TipoHabilidades.Remove(tipoHabilidadeBuscado);

            context.SaveChanges();
        }

        public List<TipoHabilidadeDomain> Listar()
        {
            return context.TipoHabilidades.ToList();
        }
    }
}
