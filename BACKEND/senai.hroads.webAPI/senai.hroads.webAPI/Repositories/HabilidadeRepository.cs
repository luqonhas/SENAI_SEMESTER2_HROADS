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
    public class HabilidadeRepository : IHabilidadeRepository
    {
        HroadsContext context = new HroadsContext();
        
        public void Atualizar(int id, HabilidadeDomain habilidadeAtualizado)
        {
            HabilidadeDomain habilidadeBuscado = context.Habilidades.Find(id);

            if (habilidadeAtualizado != null)
            {
                habilidadeBuscado.nomeHabilidade = habilidadeAtualizado.nomeHabilidade;
            }

            if (habilidadeAtualizado != null)
            {
                habilidadeBuscado.idTipoHabilidade = habilidadeAtualizado.idTipoHabilidade;
            }

            context.Habilidades.Update(habilidadeBuscado);

            context.SaveChanges();
        }

        public HabilidadeDomain BuscarPorId(int id)
        {
            return context.Habilidades.Include(x => x.tipoHabilidade).FirstOrDefault(x => x.idHabilidade == id);
        }

        public void Cadastrar(HabilidadeDomain novoHabilidade)
        {
            context.Habilidades.Add(novoHabilidade);

            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            HabilidadeDomain habilidadeBuscado = context.Habilidades.Find(id);

            context.Habilidades.Remove(habilidadeBuscado);

            context.SaveChanges();
        }

        public List<HabilidadeDomain> Listar()
        {
            return context.Habilidades.Include(x => x.tipoHabilidade).ToList();
        }
    }
}
