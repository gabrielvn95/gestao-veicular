using GestaoVeiculo.Models;

namespace GestaoVeiculo.Services.SessaoService
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();

        void CriarSessao(UsuarioModel usuarioModel);

        void RemoveSessao();
    }
}
