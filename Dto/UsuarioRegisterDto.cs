using System.ComponentModel.DataAnnotations;

namespace GestaoVeiculo.Dto
{
    public class UsuarioRegisterDto
    {
        [Required(ErrorMessage = "Digite o nome!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o sobrenome!")]
        public string SobreNome { get; set; }
        [Required(ErrorMessage = "Digite o email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "digite a senha!")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Digite a confirmação da senha"),
        Compare("Senha", ErrorMessage = "As senhas não estão iguais")]
        public string ConfirmaSenha { get; set; }
    }
}
