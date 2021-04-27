using senai.hroads.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Interfaces
{
    interface IPersonagemRepository
    {
        List<PersonagemDomain> Listar();

        PersonagemDomain BuscarPorId();

        void Cadastrar(PersonagemDomain novoPersonagem);

        void Atualizar(int id, PersonagemDomain personagemAtualizado);

        void Deletar(int id);
    }
}
