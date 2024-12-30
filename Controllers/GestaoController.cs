using ClosedXML.Excel;
using GestaoVeiculo.Models;
using GestaoVeiculo.Services.GestaoService;
using GestaoVeiculo.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVeiculo.Controllers
{
    public class GestaoController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IGestaoInterface _gestaoInterface;
        public GestaoController(ISessaoInterface sessaoInterface, IGestaoInterface gestaoInterface)
        {
            _sessaoInterface = sessaoInterface;
            _gestaoInterface = gestaoInterface;
        }
        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var gestao = await _gestaoInterface.BuscarGestao();
            return View(gestao.Dados);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if(usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if(usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var gestao = await _gestaoInterface.BuscarPorId(id);

            return View(gestao.Dados);
        }
        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }


            var gestao = await _gestaoInterface.BuscarPorId(id);

            return View(gestao.Dados);
        }

        public async Task<IActionResult> Exportar()
        {
            var dados = await _gestaoInterface.BuscarDadosExcel();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Gestao");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Gestao.xls");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var response = await _gestaoInterface.Detalhes(id);

            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;
                return RedirectToAction("Index");
            }

            return View(response.Dados);
        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(GestaoModel gestaoModel)
        {
            if (ModelState.IsValid)
            {
                var gestaoResult = await _gestaoInterface.CadastrarGestao(gestaoModel);

                if (gestaoResult.Status)
                {
                    TempData["MensagemSucesso"] = gestaoResult.Mensagem;
                }
                else
                {
                    TempData["MensagemErro"] = gestaoResult.Mensagem;
                    return View(gestaoModel);
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Editar(GestaoModel gestaoModel)
        {
            if (ModelState.IsValid)
            {
                var gestaoResult = await _gestaoInterface.EditarGestao(gestaoModel);

                if (gestaoResult.Status)
                {
                    TempData["MensagemSucesso"] = gestaoResult.Mensagem;
                }
                else
                {
                    TempData["MensagemR"] = gestaoResult.Mensagem;
                    return View(gestaoModel);
                }


                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a edição !";

            return View(gestaoModel);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(GestaoModel gestaoModel)
        {
            if (gestaoModel == null)
            {
                TempData["MensagemErro"] = "Dados não localizado";
                return View(gestaoModel);
            }

            var gestaoResult = await _gestaoInterface.RemoveGestao(gestaoModel);

            if (gestaoResult.Status)
            {
                TempData["MensagemSucesso"] = gestaoResult.Mensagem;
            }
            else
            {
                TempData["MensagemErro"] = gestaoResult.Mensagem;
                return View(gestaoResult);
            }



            return RedirectToAction("Index");
        }

    }
}

    

