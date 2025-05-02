using Application.Dtos.Request;
using Domain.Entities;

namespace Application.Dtos.Response
{
    public class UsuarioResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<ContaResponse>? Contas { get; set; }
    }
}
