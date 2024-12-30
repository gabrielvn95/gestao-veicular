using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace GestaoVeiculo.Services.SenhaService
{
    public class SenhaService : ISenhaInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificaSenha(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash);
            }
        }
    }
}
