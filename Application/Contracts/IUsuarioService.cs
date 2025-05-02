using Application.Dtos.Request;
using Application.Dtos.Response;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarUsuario(UsuarioInput input);
        Task<Usuario> EditarUsuario(Guid Id, UsuarioEdicaoInput input);
        Task<UsuarioResponse> BuscarUsuarioId(Guid Id);
        Task<List<UsuarioResponse>> BuscarUsuarios();

    }
}
