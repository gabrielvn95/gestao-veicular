using DocumentFormat.OpenXml.Office2010.Excel;
using GestaoVeiculo.Data;
using GestaoVeiculo.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GestaoVeiculo.Services.GestaoService
{
    public class GestaoService : IGestaoInterface
    {
        private readonly ApplicationDbContext _context;

        public GestaoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DataTable> BuscarDadosExcel()
        {
            DataTable dataTable = new DataTable
            {
                TableName = "Gestao"
            };

            dataTable.Columns.Add("Nome", typeof(string));
            dataTable.Columns.Add("Cpf", typeof(string));
            dataTable.Columns.Add("Rg", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Telefone", typeof(string));
            dataTable.Columns.Add("NomeVendedor", typeof(string));
            dataTable.Columns.Add("NomeAvaliador", typeof(string));
            dataTable.Columns.Add("MarcaCarro", typeof(string));
            dataTable.Columns.Add("ModeloCarro", typeof(string));
            dataTable.Columns.Add("Ano", typeof(string));
            dataTable.Columns.Add("Documentacao", typeof(string));
            dataTable.Columns.Add("ValorVenda", typeof(string));
            dataTable.Columns.Add("ValorFinanciamento", typeof(string));
            dataTable.Columns.Add("Opcionais", typeof(string));
            dataTable.Columns.Add("Multas", typeof(string));
            dataTable.Columns.Add("DataUltimaAtualizacao", typeof(string));


            var gestao = await BuscarGestao();

            if(gestao.Dados != null && gestao.Dados.Count > 0)
            {
                gestao.Dados.ForEach(g =>
                {
                    dataTable.Rows.Add(
                        g.Nome, g.Cpf, g.Rg, g.Email, g.Telefone, g.NomeVendedor, g.NomeAvaliador,
                        g.MarcaCarro, g.ModeloCarro, g.Ano, g.Documentacao, g.ValorVenda,
                        g.ValorFinanciamento, g.Opcionais, g.Multas, g.DataUltimaAtualizacao
                        );
                });
            }
            return dataTable;
        }

        public async Task<ResponseModel<List<GestaoModel>>> BuscarGestao()
        {
            var response = new ResponseModel<List<GestaoModel>>();

            try
            {
                var gestao = await _context.Gestao.ToListAsync();
                response.Dados = gestao;
                response.Mensagem = "Dados coletados com sucesso";
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<GestaoModel>> BuscarPorId(int? id)
        {
            var response = new ResponseModel<GestaoModel>();

            try
            {
                if(id == null)
                {
                    response.Mensagem = "Cadastro não localizado!";
                    response.Status = false;
                    return response;
                }

                var gestao = await _context.Gestao.FirstOrDefaultAsync(x => x.Id == id);
                if(gestao == null)
                {
                    response.Mensagem = "Cadastro não localizado!";
                    response.Status = false;
                }else
                {
                    response.Dados = gestao;
                    response.Mensagem = "Dados coletados com sucesso!";
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<GestaoModel>> CadastrarGestao(GestaoModel gestaoModel)
        {
            ResponseModel<GestaoModel> response = new ResponseModel<GestaoModel>();
            try
            {
                _context.Add(gestaoModel);
                await _context.SaveChangesAsync();
                response.Mensagem = "Cadastro realizado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<GestaoModel>> Detalhes(int? id)
        {
            var response = new ResponseModel<GestaoModel>();

            try
            {
                if (id == null)
                {
                    response.Mensagem = "Cadastro não localizado!";
                    response.Status = false;
                    return response;
                }

                var gestao = await _context.Gestao.FirstOrDefaultAsync(x => x.Id == id);
                if (gestao == null)
                {
                    response.Mensagem = "Cadastro não localizado!";
                    response.Status = false;
                }
                else
                {
                    response.Dados = gestao;
                    response.Mensagem = "Dados coletados com sucesso!";
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }
        

        public async Task<ResponseModel<GestaoModel>> EditarGestao(GestaoModel gestaoModel)
        {
            ResponseModel<GestaoModel> response = new ResponseModel<GestaoModel>();

            try
            {
                var gestao = await BuscarPorId(gestaoModel.Id);
                if(gestao.Status == false)
                {
                    return gestao;
                }

                gestao.Dados.Nome = gestaoModel.Nome;
                gestao.Dados.Cpf = gestaoModel.Cpf;
                gestao.Dados.Rg = gestaoModel.Rg;
                gestao.Dados.Email = gestaoModel.Email;
                gestao.Dados.Telefone = gestaoModel.Telefone;
                gestao.Dados.NomeVendedor = gestaoModel.NomeVendedor;
                gestao.Dados.NomeAvaliador = gestaoModel.NomeAvaliador;
                gestao.Dados.MarcaCarro = gestaoModel.MarcaCarro;
                gestao.Dados.ModeloCarro = gestaoModel.ModeloCarro;
                gestao.Dados.Ano = gestaoModel.Ano;
                gestao.Dados.Documentacao = gestaoModel.Documentacao;
                gestao.Dados.ValorFinanciamento = gestaoModel.ValorFinanciamento;
                gestao.Dados.ValorVenda = gestaoModel.ValorVenda;
                gestao.Dados.Opcionais = gestaoModel.Opcionais;
                gestao.Dados.Multas = gestaoModel.Multas;

                _context.Update(gestao.Dados);
                await _context.SaveChangesAsync();

                response.Mensagem = "Edição realizada com sucesso!";
                return response;

            }catch (Exception ex)
            {
                response.Mensagem = ex.Message; 
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<GestaoModel>> RemoveGestao(GestaoModel gestaoModel)
        {
            ResponseModel<GestaoModel> response = new ResponseModel<GestaoModel>();

            try
            {
                _context.Remove(gestaoModel);
                await _context.SaveChangesAsync();

                response.Mensagem = "Remoção realizada com sucesso!";
                return response;

            }catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
