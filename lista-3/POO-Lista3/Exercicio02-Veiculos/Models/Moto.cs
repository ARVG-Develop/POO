namespace Exercicio02_Veiculos;

public class Moto : Veiculo, IManutencao
{
    public Moto(string marca, string modelo) : base(marca, modelo)
    {
    }

    public override string ExibirInformacoes()
    {
        Console.WriteLine("Estou no método ExibirInformacoes da classe Moto");
        return "";
    }

    public void RealizarManutencao()
    {
        Console.WriteLine("Estou no método RealizarManutencao da classe Moto");
    }
}
