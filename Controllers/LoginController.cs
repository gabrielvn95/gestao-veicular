using GestaoVeiculo.Dto;
using GestaoVeiculo.Repositorio;
using GestaoVeiculo.Services.LoginService;
using GestaoVeiculo.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace GestaoVeiculo.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginInterface _loginInterface;
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(ILoginInterface loginInterface, ISessaoInterface sessaoInterface, IUsuarioRepositorio usuarioRepositorio)
        {
            _loginInterface = loginInterface;
            _sessaoInterface = sessaoInterface;
            _usuarioRepositorio = usuarioRepositorio;

        }

        public IActionResult Login()
        {
            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _sessaoInterface.RemoveSessao();
            return RedirectToAction("Login");
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioRegisterDto usuarioRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _loginInterface.RegistrarUsuario(usuarioRegisterDto);

                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioRegisterDto);
                }
            }
            return View(usuarioRegisterDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _loginInterface.Login(usuarioLoginDto);

                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioLoginDto);
                }
            }
            return View(usuarioLoginDto);
        }



        private string GerarNovaSenha()
        {
            const int tamanho = 8;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, tamanho)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private (byte[] Hash, byte[] Salt) GerarSenhaHash(string senha)
        {
            using (var hmac = new HMACSHA256())
            {
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return (hash, salt);
            }
        }
    }
}
