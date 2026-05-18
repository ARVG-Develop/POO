using Exercicio02_Veiculos;

List<Veiculo> veiculos = new()
{
    new Carro("Toyota", "Corolla"),
    new Moto("Yamaha", "MT-07")
};

Console.WriteLine("=== VEÍCULOS ===\n");

foreach (var v in veiculos)
{
    Console.WriteLine(v.ExibirInformacoes());

    if (v is IManutencao manutencao)
    {
        manutencao.RealizarManutencao();
    }

    Console.WriteLine();
}
