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
        
        public bool Atualizar(int id, HabilidadeDomain habilidadeAtualizado)
        {
            HabilidadeDomain habilidadeBuscada = BuscarPorId(id);

            HabilidadeDomain habilidadeBuscadaNome = context.Habilidades.FirstOrDefault(x => x.nomeHabilidade == habilidadeAtualizado.nomeHabilidade);

            if (habilidadeAtualizado.nomeHabilidade != null && habilidadeBuscadaNome == null)
            {
                habilidadeBuscada.nomeHabilidade = habilidadeAtualizado.nomeHabilidade;

                context.Habilidades.Update(habilidadeBuscada);

                context.SaveChanges();

                return true;
            }

            return false;
        }

        public HabilidadeDomain BuscarPorId(int id)
        {
            // retorna a primeira classe encontrada para o id informado
            return context.Habilidades.Include(x => x.tipoHabilidade).FirstOrDefault(x => x.idHabilidade == id);
        }

        public HabilidadeDomain BuscarPorNome(string nome)
        {
            HabilidadeDomain habilidadeBuscada = context.Habilidades.FirstOrDefault(x => x.nomeHabilidade == nome);

            if (habilidadeBuscada != null)
            {
                return habilidadeBuscada;
            }

            return null;
        }

        public void Cadastrar(HabilidadeDomain novoHabilidade)
        {
            context.Habilidades.Add(novoHabilidade);

            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            context.Habilidades.Remove(BuscarPorId(id));

            context.SaveChanges();
        }

        public List<HabilidadeDomain> Listar()
        {
            return context.Habilidades.Include(x => x.tipoHabilidade).ToList();
        }
    }
}
