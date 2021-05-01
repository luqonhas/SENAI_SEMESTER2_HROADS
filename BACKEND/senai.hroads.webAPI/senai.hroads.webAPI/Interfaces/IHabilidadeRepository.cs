using senai.hroads.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Interfaces
{
    interface IHabilidadeRepository
    {
        List<HabilidadeDomain> Listar();

        HabilidadeDomain BuscarPorId(int id);

        HabilidadeDomain BuscarPorNome(string nome);

        void Cadastrar(HabilidadeDomain novoHabilidade);

        bool Atualizar(int id, HabilidadeDomain habilidadeAtualizado);

        void Deletar(int id);
    }
}
