using System.ComponentModel.DataAnnotations;

namespace GestaoVeiculo.Models
{
    public class GestaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite seu nome completo!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite seu CPF!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Digite seu RG!")]
        public string Rg { get; set; }

        [Required(ErrorMessage = "Digite seu Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite seu Número")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Digite o nome do Vendedor!")]
        public string NomeVendedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do Avaliador!")]
        public string NomeAvaliador { get; set; }

        [Required(ErrorMessage = "Digite a marca do Carro!")]
        public string MarcaCarro { get; set; }

        [Required(ErrorMessage = "Digite o modelo do Carro!")]
        public string ModeloCarro { get; set; }

        [Required(ErrorMessage = "Digite o ano do Carro!")]
        public string Ano { get; set; }

        [Required(ErrorMessage = "Informe situação da Documentação!")]
        public string Documentacao { get; set; }

        [Required(ErrorMessage = "Informe Valor do financiamento!")]
        public string ValorFinanciamento { get; set; }

        [Required(ErrorMessage = "Informe valor de Venda!")]
        public string ValorVenda { get; set; }

        [Required(ErrorMessage = "Informe os Opcionais!")]
        public string Opcionais { get; set; }

        [Required(ErrorMessage = "Informe se a MULTAS ou NÃO ")]
        public string Multas { get; set; }

        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;
    }
}
