using Application.Dtos.Request;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IContaService
    {
        Task<Conta> CriarConta(ContaInput input);
        Task<Conta> EditarConta(Guid Id, ContaEdicaoInput input);
        Task<ContaResponse> BuscarContaId(Guid Id);
        Task<List<ContaResponse>> BuscarContas();
    }
}

