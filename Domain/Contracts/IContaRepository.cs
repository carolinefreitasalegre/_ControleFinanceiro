using Domain.Entities;

namespace Domain.Contracts
{
    public interface IContaRepository
    {
        Task<Conta> CriarConta(Conta input);
        Task<Conta> EditarConta(Guid Id, Conta input);
        Task<Conta> BuscarContaId(Guid Id);
        Task<List<Conta>> BuscarContas();


        Task<Usuario> ObterUsuarioPorId(Guid id);

    }
}
