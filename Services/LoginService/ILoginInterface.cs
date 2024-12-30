using GestaoVeiculo.Dto;
using GestaoVeiculo.Models;

namespace GestaoVeiculo.Services.LoginService
{
    public interface ILoginInterface
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto);
        Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto);
    }
}
