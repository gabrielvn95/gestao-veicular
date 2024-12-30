using GestaoVeiculo.Models;
using GestaoVeiculo.Repositorio;
using GestaoVeiculo.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace Gatti.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessaoInterface _sessaoInterface;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessaoInterface sessaoInterface)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessaoInterface = sessaoInterface;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessaoInterface.BuscarSessao();
                alterarSenhaModel.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Index", alterarSenhaModel);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar sua senha, Detalhe do erro: {erro.Message}";
                return View("Index", alterarSenhaModel);
            }
        }
    }
}
