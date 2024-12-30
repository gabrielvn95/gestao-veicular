using System.ComponentModel.DataAnnotations;

namespace GestaoVeiculo.Models
{
    public class AlterarSenhaModel
    {
        [Required(ErrorMessage = "Digite a senha atual do usuário")]
        public int Id { get; set; }
        public string? SenhaAtual { get; set; }
        [Required(ErrorMessage = "Digite a nova senha do usuário")]
        public string? NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirme a nova senha do usuário")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova ")]
        public string? ConfirmarNovaSenha { get; set; }

    }
}
