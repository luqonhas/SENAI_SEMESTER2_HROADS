using senai.hroads.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Interfaces
{
    interface ITipoHabilidadeRepository
    {
        List<TipoHabilidadeDomain> Listar();

        TipoHabilidadeDomain BuscarPorId(int id);

        TipoHabilidadeDomain BuscarPorNome(string nome);

        void Cadastrar(TipoHabilidadeDomain novoTipoHabilidade);

        bool Atualizar(int id, TipoHabilidadeDomain tipoHabilidadeAtualizado);

        void Deletar(int id);
    }
}
