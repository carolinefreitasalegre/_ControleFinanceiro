using Application.Dtos.Request;
using Azure.Core;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IJwtService
    {
        string GerarToken(LoginRequest request);
    }
}
