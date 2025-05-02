using Domain.Contracts;
using Domain.Entities;
using Infrastruture.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContaRepository : IContaRepository
    {

        private readonly AppDbContext _context;

        public ContaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Conta> BuscarContaId(Guid Id)
        {
            var conta = await _context.Contas
                .Include(u => u.Usuario)                
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (conta == null)
                throw new Exception("Algo deu errado ao buscar dado no banco");

            return conta;

        }

        public async Task<List<Conta>> BuscarContas()
        {
            
            return await _context.Contas
                .Include(c => c.Usuario)
                .ToListAsync();
 
        }

        public async Task<Conta> CriarConta(Conta input)
        {

            _context.Contas.Add(input);
            await _context.SaveChangesAsync();

            return input;
        }

        public async Task<Conta> EditarConta(Guid Id, Conta input)
        {
            _context.Contas.Update(input);
            await _context.SaveChangesAsync();

            return input;
        }

        public async Task<Usuario> ObterUsuarioPorId(Guid id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            return usuario;

        }
    }
}
