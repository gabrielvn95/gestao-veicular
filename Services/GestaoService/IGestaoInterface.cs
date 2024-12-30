using GestaoVeiculo.Models;
using System.Data;

namespace GestaoVeiculo.Services.GestaoService
{
    public interface IGestaoInterface
    {
        Task<DataTable> BuscarDadosExcel();
        Task<ResponseModel<List<GestaoModel>>> BuscarGestao();
        Task<ResponseModel<GestaoModel>> BuscarPorId(int? id);
        Task<ResponseModel<GestaoModel>> CadastrarGestao(GestaoModel gestaoModel);
        Task<ResponseModel<GestaoModel>> EditarGestao(GestaoModel gestaoModel);
        Task<ResponseModel<GestaoModel>> RemoveGestao(GestaoModel gestaoModel);
        Task<ResponseModel<GestaoModel>> Detalhes(int? id);
    }
}
