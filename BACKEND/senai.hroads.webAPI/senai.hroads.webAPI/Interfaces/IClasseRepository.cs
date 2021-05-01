using senai.hroads.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Interfaces
{
    interface IClasseRepository
    {
        List<ClasseDomain> Listar();

        ClasseDomain BuscarPorId(int id);

        ClasseDomain BuscarPorNome(string nome);

        void Cadastrar(ClasseDomain novaClasse);

        bool Atualizar(int id, ClasseDomain classeAtualizada);

        void Deletar(int id);
    }
}
