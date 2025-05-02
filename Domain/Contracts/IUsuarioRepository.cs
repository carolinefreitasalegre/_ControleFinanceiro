using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUsuarioRepository
    {
        Task<Usuario> CriarUsuario(Usuario input);
        Task<Usuario> EditarUsuario(Guid Id, Usuario input);
        Task<Usuario> BuscarUsuarioId(Guid Id);
        Task<List<Usuario>> BuscarUsuarios();


    }
}
