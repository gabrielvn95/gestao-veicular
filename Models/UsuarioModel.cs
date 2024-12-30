namespace GestaoVeiculo.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
    }
}
