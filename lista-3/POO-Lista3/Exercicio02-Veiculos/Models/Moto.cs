namespace Exercicio02_Veiculos;

public class Moto : Veiculo
{
    public Moto(string marca, string modelo) : base(marca, modelo)
    {
    }
    public override string ExibirInformacoes()
    {
        return $"Moto - Marca: {Marca}, Modelo: {Modelo}";
    }
}
