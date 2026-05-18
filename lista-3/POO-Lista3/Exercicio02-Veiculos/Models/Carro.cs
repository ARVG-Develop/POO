namespace Exercicio02_Veiculos;

public class Carro : Veiculo, IManutencao
{
    public Carro(string marca, string modelo) : base(marca, modelo)
    {
    }

    public override string ExibirInformacoes()
    {
        Console.WriteLine("Estou no método ExibirInformacoes da classe Carro");
        return "";
    }

    public void RealizarManutencao()
    {
        Console.WriteLine("Estou no método RealizarManutencao da classe Carro");
    }
}
