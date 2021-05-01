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
            PersonagemDomain personagemBuscado = BuscarPorId(id);

            PersonagemDomain personagemBuscadoNome = context.Personagens.FirstOrDefault(x => x.nomePersonagem == personagemAtualizado.nomePersonagem);

            if (personagemAtualizado.nomePersonagem != null && personagemBuscadoNome == null)
            {
                personagemBuscado.nomePersonagem = personagemAtualizado.nomePersonagem;
            }
            if (personagemAtualizado.maxVida >= 10 && personagemAtualizado.maxVida <= 100)
            {
                personagemBuscado.maxVida = personagemAtualizado.maxVida;
            }
            if (personagemAtualizado.maxMana >= 10 && personagemAtualizado.maxMana <= 100)
            {
                personagemBuscado.maxMana = personagemAtualizado.maxMana;
            }
            if (context.Classes.Find(personagemAtualizado.idClasse) != null)
            {
                personagemBuscado.idClasse = personagemAtualizado.idClasse;
            }

            personagemBuscado.dataAtualizacao = DateTime.Now;

            context.Personagens.Update(personagemBuscado);

            context.SaveChanges();
        }

        public PersonagemDomain BuscarPorId(int id)
        {
            return context.Personagens.FirstOrDefault(x => x.idPersonagem == id);
        }

        public PersonagemDomain BuscarPorNome(string nome)
        {
            PersonagemDomain personagemBuscado = context.Personagens.FirstOrDefault(x => x.nomePersonagem == nome);

            if (personagemBuscado != null)
            {
                return personagemBuscado;
            }

            return null;
        }

        public void Cadastrar(PersonagemDomain novoPersonagem, int id)
        {
            novoPersonagem.dataCriacao = DateTime.Now;
            novoPersonagem.dataAtualizacao = DateTime.Now;

            switch (novoPersonagem.maxVida)
            {
                case > 100:
                    novoPersonagem.maxVida = 100;
                    break;

                case < 10:
                    novoPersonagem.maxVida = 10;
                    break;

                default:
                    novoPersonagem.maxVida = 50;
                    break;
            }

            switch (novoPersonagem.maxMana)
            {
                case > 100:
                    novoPersonagem.maxMana = 100;
                    break;

                case < 10:
                    novoPersonagem.maxMana = 10;
                    break;

                default:
                    novoPersonagem.maxMana = 50;
                    break;
            }

            context.Personagens.Where(x => x.usuario.idUsuario == id);

            context.Personagens.Add(novoPersonagem);

            context.SaveChanges();
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

        public List<PersonagemDomain> ListarOrdenado()
        {
            return context.Personagens.Include(x => x.classe).OrderBy(x => x.classe.nomeClasse).ToList();
        }

        public List<PersonagemDomain> ListarComJogadores(int id)
        {
            var personagemBuscado = context.Personagens.Include(x => x.usuario).Where(x => x.usuario.idUsuario == id).Select(x => new PersonagemDomain()
            {
                idPersonagem = x.idPersonagem,
                nomePersonagem = x.nomePersonagem,
                maxVida = x.maxVida,
                maxMana = x.maxMana,
                dataCriacao = x.dataCriacao,

                classe = new ClasseDomain()
                {
                    idClasse = x.idClasse,
                    nomeClasse = x.classe.nomeClasse
                },

                usuario = new UsuarioDomain()
                {
                    idUsuario = x.usuario.idUsuario,
                    email = x.usuario.email
                }
            });

            return personagemBuscado.ToList();
        }


    }
}
