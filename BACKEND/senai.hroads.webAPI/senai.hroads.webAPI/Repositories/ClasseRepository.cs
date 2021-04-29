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
    public class ClasseRepository : IClasseRepository
    {
        /// <summary>
        /// esse objeto "context" é por onde conseguimos chamar as entidades/tabelas com seus atributos
        /// puxarmos o "context" porque ele vai vir com as coisas que podem ter sido atualizadas no SQL Server e acompanha também os comando "ToList" e entre outros, por isso não chamamos a Domain ao invés do "context"
        /// </summary>
        HroadsContext context = new HroadsContext();

        public void Atualizar(int id, ClasseDomain classeAtualizada)
        {
            // busca a classe atráves do seu id
            ClasseDomain classeBuscada = context.Classes.Find(id);

            // verifica se a classe tem um nome informado...
            if (classeAtualizada.nomeClasse != null)
            {
                // se tiver, atribui os novos valores aos valores existentes
                classeBuscada.nomeClasse = classeAtualizada.nomeClasse;
            }
            //se não tiver...
            // atualiza a classe que foi buscada e...
            context.Classes.Update(classeBuscada);

            // salva as informações para que sejam gravadas no BD
            context.SaveChanges();
        }

        public ClasseDomain BuscarPorId(int id)
        {
            // retorna a primeira classe encontrada para o id informado
            return context.Classes.FirstOrDefault(objClasse => objClasse.idClasse == id);
        }

        public void Cadastrar(ClasseDomain novaClasse)
        {
            // adiciona essa nova classe
            context.Classes.Add(novaClasse);

            // salva as informações para que sejam gravadas no BD
            context.SaveChanges();
        }

        public void Deletar(int id)
        {
            // busca a classe atráves do seu id
            ClasseDomain classeBuscada = context.Classes.Find(id);

            // remove a classe que foi encontrada e colocada dentro do "classeBuscada"
            context.Classes.Remove(classeBuscada);

            // salva as informações para que sejam gravadas no BD
            context.SaveChanges();
        }

        public List<ClasseDomain> Listar()
        {
            // retorna uma lista com todas as informações da tabela/entidade classe
            return context.Classes.ToList();
        }
    }
}
