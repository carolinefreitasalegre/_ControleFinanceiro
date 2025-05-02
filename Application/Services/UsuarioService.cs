using Application.Contracts;
using Application.Dtos.Request;
using Application.Dtos.Response;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioResponse> BuscarUsuarioId(Guid Id)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioId(Id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            return new UsuarioResponse()
            {
                Name = usuario.Name,
                Email = usuario.Email,
                Contas = usuario.Contas.Select(conta => new ContaResponse()
                {
                    Nome = conta.Nome,
                    Valor = conta.Valor,
                    DataLancamento = conta.DataLancamento,
                    Status = conta.Status,
                }).ToList()
            };

        }

        public async Task<List<UsuarioResponse>> BuscarUsuarios()
        {
            var usuarios = await _usuarioRepository.BuscarUsuarios();

            return usuarios.Select(usuario => new UsuarioResponse()
            {
                Name = usuario.Name,
                Email = usuario.Email,
                Contas = usuario.Contas.Select(conta => new ContaResponse()
                {
                    Nome = conta.Nome,
                    Valor = conta.Valor,
                    DataLancamento = conta.DataLancamento,
                    Status = conta.Status,
                }).ToList(),
            }).ToList();

        }

        public async Task<Usuario> CriarUsuario(UsuarioInput input)
        {
            var usuario = new Usuario()
            {
                Id = Guid.NewGuid(),
                Name = input.Name.ToLower(),
                Email = input.Email.ToLower(),
                Senha = input.Senha.ToLower(),
            };

            await _usuarioRepository.CriarUsuario(usuario);
            return usuario;

        }

        public async Task<Usuario> EditarUsuario(Guid Id, UsuarioEdicaoInput input)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioId(Id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            usuario.Name = input.Name;
            usuario.Email = input.Email;
            usuario.Senha = input.Senha;


            return await _usuarioRepository.EditarUsuario(Id, usuario);

        }

    }
}
