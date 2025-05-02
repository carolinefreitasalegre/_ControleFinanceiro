using Application.Contracts;
using Application.Dtos.Request;
using Domain.Contracts;
using Domain.Enums;
using Domain.Factories;

namespace Application.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IContaFactory _contaFactory;

        public ContaService(IContaRepository contaRepository, IContaFactory contaFactory)
        {
            _contaRepository = contaRepository;
            _contaFactory = contaFactory;
        }

        public async Task<ContaResponse> BuscarContaId(Guid Id)
        {
            var conta = await _contaRepository.BuscarContaId(Id);

            if (conta == null)
                throw new Exception("Erro ao buscar conta");

            return new ContaResponse()
            {
                Nome = conta.Nome,
                Valor = conta.Valor,
                Status = conta.Status,
                UsuarioName = conta.Usuario.Name,
            };

        }

        public async Task<List<ContaResponse>> BuscarContas()
        {
            var contas = await _contaRepository.BuscarContas();
            
            return contas.Select(conta => new ContaResponse
            {
                Nome = conta.Nome,
                Valor = conta.Valor,
                DataLancamento = conta.DataLancamento,
                Status = conta.Status,
                UsuarioName = conta.Usuario.Name,
            }).ToList();
        }

        public async Task<Conta> CriarConta(ContaInput input)
        {

            var usuario = await _contaRepository.ObterUsuarioPorId(input.UsuarioId);

            var conta = _contaFactory.CriarConta(input.TipoDeConta, usuario);

            if(input.TipoDeConta == TipoDeConta.Parcelada)
            {
                if(input.Parcelas < 2)
                {
                    throw new Exception("Quantidade de parcelas não pode ser menor que 2.");
                }
            } else if(input.TipoDeConta == TipoDeConta.Variavel ||  input.TipoDeConta == TipoDeConta.Fixa)
            {
                if(input.Parcelas >= 2)
                {
                    throw new Exception("Tipo de conta Variável e Fixa não devem ter mais de uma parcela.");
                }
            }

            conta.Nome = input.Nome.ToLower();
            conta.Valor = input.Valor;
            conta.Parcelas = input.Parcelas;
            conta.DataLancamento = DateTime.Now;
            conta.Status = input.Status;

            await _contaRepository.CriarConta(conta);

            return conta;
        }

        public async Task<Conta> EditarConta(Guid Id, ContaEdicaoInput input)
        {
            var conta = await _contaRepository.BuscarContaId(Id);

            if (conta == null)
                throw new Exception("Dado não encontrado");

            conta.Nome = input.Nome.ToLower();
            conta.Valor = input.Valor;
            conta.Status = input.Status;

            return await _contaRepository.EditarConta(Id, conta);

        }
    }
}
