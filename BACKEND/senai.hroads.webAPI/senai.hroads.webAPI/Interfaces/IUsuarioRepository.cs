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

        List<UsuarioDomain> ListarComPersonagens(int id);

        List<UsuarioDomain> ListarSemSenha();

        UsuarioDomain BuscarPorId(int id);

        UsuarioDomain BuscarPorEmail(string email);

        void Cadastrar(UsuarioDomain novoUsuario);

        bool Atualizar(int id, UsuarioDomain usuarioAtualizado);

        void Deletar(int id);

        UsuarioDomain Logar(string email, string senha);
    }
}
