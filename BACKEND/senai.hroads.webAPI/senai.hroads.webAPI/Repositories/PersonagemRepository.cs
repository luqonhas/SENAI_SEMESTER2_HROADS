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
    public class PersonagemRepository : IPersonagemRepository
    {
        HroadsContext context = new HroadsContext();

        public void Atualizar(int id, PersonagemDomain personagemAtualizado)
        {
            PersonagemDomain personagemBuscado = context.Personagens.Find(id);

            if (personagemAtualizado.nomePersonagem != null)
            {
                personagemBuscado.nomePersonagem = personagemAtualizado.nomePersonagem;
                personagemBuscado.idClasse = personagemAtualizado.idClasse;
                personagemBuscado.maxVida = personagemAtualizado.maxVida;
                personagemBuscado.maxMana = personagemAtualizado.maxMana;
                personagemBuscado.dataAtualizacao = DateTime.Now;
            }

            context.Personagens.Update(personagemBuscado);

            context.SaveChanges();
        }

        public PersonagemDomain BuscarPorId(int id)
        {
            return context.Personagens.FirstOrDefault(x => x.idPersonagem == id);
        }

        public void Cadastrar(PersonagemDomain novoPersonagem)
        {
            context.Personagens.Add(novoPersonagem);
        }

        public void Deletar(int id)
        {
            PersonagemDomain personagemBuscado = context.Personagens.Find(id);

            context.Personagens.Remove(personagemBuscado);

            context.SaveChanges();
        }

        public List<PersonagemDomain> Listar()
        {
            return context.Personagens.ToList();
        }
    }
}
