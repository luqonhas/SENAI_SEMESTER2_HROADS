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

        ClasseDomain BuscarPorId();

        void Cadastrar(ClasseDomain novaClasse);

        void Atualizar(int id, ClasseDomain ClasseAtualizada);

        void Deletar(int id);
    }
}
