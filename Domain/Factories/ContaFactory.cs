using Domain.Entities;
using Domain.Enums;
using Domain.Factories;

public class ContaFactory : IContaFactory
{
    public Conta CriarConta(TipoDeConta tipoDeConta, Usuario usuario)
    {
        return tipoDeConta switch
        {
            TipoDeConta.Variavel => new ContaVariavel(usuario),
            TipoDeConta.Fixa => new ContaFixa(usuario),
            TipoDeConta.Parcelada => new ContaParcelada(usuario),
            _ => throw new ArgumentException("Tipo de conta inválida")
        };
    }
}
