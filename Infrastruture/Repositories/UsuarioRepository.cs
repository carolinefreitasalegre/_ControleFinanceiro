using Domain.Contracts;
using Domain.Entities;
using Infrastruture.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Usuario> BuscarUsuarioId(Guid Id)
        {
            var usuario = await _context.Usuarios
                .Include(c => c.Contas)
                .FirstOrDefaultAsync(u => u.Id == Id);

            if (usuario == null)
            {
                throw new Exception("Erro ao buscar usuário no banco.");
            }

            return usuario;

        }

        public async Task<List<Usuario>> BuscarUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(c => c.Contas)
                .ToListAsync();


            return usuarios;
        }

        public async Task<Usuario> CriarUsuario(Usuario input)
        {
            _context.Usuarios.Add(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<Usuario> EditarUsuario(Guid Id, Usuario input)
        {
            _context.Usuarios.Update(input);
            await _context.SaveChangesAsync();

            return input;
        }
    }
}


/*
 
    preciso buscar uma lista de contas a partir do Id do usuario
    puxo o UsuarioId e a partiir dele vem uma lista

    var lista = tabela.listar(UsuarioId
 
*/