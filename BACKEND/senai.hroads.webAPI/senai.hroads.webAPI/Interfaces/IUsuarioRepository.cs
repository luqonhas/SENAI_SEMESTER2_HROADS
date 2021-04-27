using senai.hroads.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuarioDomain> Listar();

        UsuarioDomain BuscarPorId();

        void Cadastrar(UsuarioDomain novoUsuario);

        void Atualizar(int id, UsuarioDomain usuarioAtualizado);

        void Deletar(int id);
    }
}
