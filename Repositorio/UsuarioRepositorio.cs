using GestaoVeiculo.Data;
using GestaoVeiculo.Models;
using GestaoVeiculo.Repositorio;

namespace Gatti.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha)
        {
            UsuarioModel usuarioDB = BuscarPorId(alterarSenha.Id);

            if (usuarioDB == null)
                throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!VerificarSenhaHash(alterarSenha.SenhaAtual, usuarioDB.SenhaHash, usuarioDB.SenhaSalt))
                throw new Exception("Senha atual não confere!");

            if (VerificarSenhaHash(alterarSenha.NovaSenha, usuarioDB.SenhaHash, usuarioDB.SenhaSalt))
                throw new Exception("Nova senha deve ser diferente da senha atual!");

            CriarSenhaHash(alterarSenha.NovaSenha, out byte[] novaSenhaHash, out byte[] novaSenhaSalt);
            usuarioDB.SenhaHash = novaSenhaHash;
            usuarioDB.SenhaSalt = novaSenhaSalt;


            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        private bool VerificarSenhaHash(string senha, byte[] senhaHashArmazenada, byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(senhaSalt))
            {
                var senhaHashCalculada = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return senhaHashCalculada.SequenceEqual(senhaHashArmazenada);
            }
        }

        private void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = BuscarPorId(id);

            if (usuarioDb == null) throw new Exception("Erro ao deletar o usuário");

            _context.Usuarios.Remove(usuarioDb);
            _context.SaveChanges();

            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = BuscarPorId((int)usuario.Id);

            if (usuarioDb == null) throw new Exception("Erro na atualização do Usuário");

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;

            _context.Usuarios.Update(usuario);
            _context.SaveChanges();

            return usuarioDb;
        }

        public UsuarioModel BuscarPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _context.Usuarios.ToList();
        }
    }
}
