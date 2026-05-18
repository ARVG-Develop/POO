namespace Exercicio02_Veiculos;

public class Moto : Veiculo, IManutencao
{
    public Moto(string marca, string modelo) : base(marca, modelo)
    {
    }

    public override string ExibirInformacoes()
    {
        return $"Moto - Marca: {Marca}, Modelo: {Modelo}";
    }

    public void RealizarManutencao()
    {
        Console.WriteLine($"Manutenção da moto {Marca} {Modelo} realizada.");
    }
}
