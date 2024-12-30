namespace GestaoVeiculo.Services.SenhaService
{
    public interface ISenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out Byte[] senhaSalt);

        bool VerificaSenha(string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}
