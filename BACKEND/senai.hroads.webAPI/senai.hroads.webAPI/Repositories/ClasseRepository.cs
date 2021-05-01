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

        public bool Atualizar(int id, ClasseDomain classeAtualizada)
        {
            // busca uma classe pelo id que foi passado como parâmetro
            ClasseDomain classeBuscada = BuscarPorId(id);

            // busca uma classe pelo "nomeClasse" da classeAtualizada
            ClasseDomain classeBuscadaNome = context.Classes.FirstOrDefault(x => x.nomeClasse == classeAtualizada.nomeClasse);

            // se o "nomeClasse" da "classeAtualizada" for diferente de null e se a "classeBuscadaNome" for igual a null...
            if (classeAtualizada.nomeClasse != null && classeBuscadaNome == null)
            {
                // executa o método
                // coloca o novo "nomeClasse" no lugar do antigo
                classeBuscada.nomeClasse = classeAtualizada.nomeClasse;

                // atualiza o a tabela com a nova informação no "classeBuscada"
                context.Classes.Update(classeBuscada);

                // salva as alterações feitas
                context.SaveChanges();

                // retorna "true", e para conseguir colocar isso, tem que mudar o tipo do método de "void" para "bool" (true ou false)
                return true;
            }

            // se for igual a null, retorna false e não executa o método
            return false;
        }

        public ClasseDomain BuscarPorId(int id)
        {
            // retorna a primeira classe encontrada para o id informado
            return context.Classes.FirstOrDefault(x => x.idClasse == id);
        }

        public ClasseDomain BuscarPorNome(string nome)
        {
            // busca uma classe pelo "nomeClasse" pelo "nome" que buscarem
            ClasseDomain classeBuscada = context.Classes.FirstOrDefault(x => x.nomeClasse == nome);

            // se a "classeBuscada" existir e for diferente de null...
            if (classeBuscada != null)
            {
                // retorna uma "classeBuscada" com o nome buscado
                return classeBuscada;
            }

            // se não, retorna null
            return null;
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
            // remove a classe que foi encontrada e colocada dentro do "classeBuscada"
            context.Classes.Remove(BuscarPorId(id));

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
