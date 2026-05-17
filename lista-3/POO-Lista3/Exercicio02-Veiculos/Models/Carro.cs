namespace Exercicio02_Veiculos;

public class Carro : Veiculo
{
    public Carro(string marca, string modelo) : base(marca, modelo)
    {
    }
    public override string ExibirInformacoes()
    {
        return $"Carro - Marca: {Marca}, Modelo: {Modelo}";
    }
}
