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

        List<PersonagemDomain> ListarOrdenado();

        List<PersonagemDomain> ListarComJogadores(int id);

        PersonagemDomain BuscarPorId(int id);

        PersonagemDomain BuscarPorNome(string nome);

        void Cadastrar(PersonagemDomain novoPersonagem, int id);

        void Atualizar(int id, PersonagemDomain personagemAtualizado);

        void Deletar(int id);
    }
}
