using GestaoVeiculo.Models;

namespace GestaoVeiculo.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorEmail(string email);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel BuscarPorId(int id);
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha);

        bool Apagar(int id);    
    }
}
