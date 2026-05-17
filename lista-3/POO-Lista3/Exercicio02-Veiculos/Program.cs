using Exercicio02_Veiculos;

List<Veiculo> veiculos = new()
{
    new Carro("Toyota", "Corolla"),
    new Carro("Honda", "Civic"),
    new Moto("Yamaha", "MT-07"),
    new Moto("Honda", "CB 500")
};

foreach (var v in veiculos)
{
    Console.WriteLine(v.ExibirInformacoes());
}
