using Domain.Entities;
using Domain.Enums;

namespace Domain.Factories
{
    public interface IContaFactory
    {
        Conta CriarConta(TipoDeConta tipoDeConta, Usuario usuario);
    }
}
