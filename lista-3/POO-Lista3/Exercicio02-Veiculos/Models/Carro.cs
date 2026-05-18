namespace Exercicio02_Veiculos;

public class Carro : Veiculo, IManutencao
{
    public Carro(string marca, string modelo) : base(marca, modelo)
    {
    }

    public override string ExibirInformacoes()
    {
        return $"Carro - Marca: {Marca}, Modelo: {Modelo}";
    }

    public void RealizarManutencao()
    {
        Console.WriteLine($"Manutenção do carro {Marca} {Modelo} realizada.");
    }
}
