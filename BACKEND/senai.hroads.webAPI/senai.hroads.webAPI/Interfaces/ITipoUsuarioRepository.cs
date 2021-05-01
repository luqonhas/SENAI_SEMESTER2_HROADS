using senai.hroads.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Interfaces
{
    interface ITipoUsuarioRepository
    {
        List<TipoUsuarioDomain> Listar();

        TipoUsuarioDomain BuscarPorId(int id);

        TipoUsuarioDomain BuscarPorNome(string nome);

        void Cadastrar(TipoUsuarioDomain novoTipoUsuario);

        bool Atualizar(int id, TipoUsuarioDomain tipoUsuarioAtualizado);

        void Deletar(int id);
    }
}
