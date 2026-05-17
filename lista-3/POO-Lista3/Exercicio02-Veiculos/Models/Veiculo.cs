namespace Exercicio02_Veiculos;

public abstract class Veiculo
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public Veiculo(string marca, string modelo)
    {
        Marca = marca;
        Modelo = modelo;
    }
    public abstract string ExibirInformacoes();
}

/*namespace Exercicio01_Funcionarios;

abstract class Funcionario
{
    public string Nome { get; set; }
    public double SalarioBase { get; set; }

    public Funcionario(string nome, double salarioBase)
    {
        Nome = nome;
        SalarioBase = salarioBase;
    }

    public abstract double CalcularSalario();
}*/
