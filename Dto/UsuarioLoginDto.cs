using System.ComponentModel.DataAnnotations;

namespace GestaoVeiculo.Dto
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "Digite o Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite sua senha!")]
        public string Senha { get; set; }
    }
}
