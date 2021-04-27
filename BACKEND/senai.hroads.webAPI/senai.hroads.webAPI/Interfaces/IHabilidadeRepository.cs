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

        HabilidadeDomain BuscarPorId();

        void Cadastrar(HabilidadeDomain novoHabilidade);

        void Atualizar(int id, HabilidadeDomain habilidadeAtualizado);

        void Deletar(int id);
    }
}
