using Application.Contracts;
using Application.Dtos.Request;
using Domain.Contracts;
using Domain.Factories;
using Microsoft.EntityFrameworkCore;

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
            conta.Valor = input.Valor;
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
