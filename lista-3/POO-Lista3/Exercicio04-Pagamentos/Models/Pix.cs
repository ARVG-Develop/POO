namespace Exercicio04_Pagamentos;

public class Pix : Pagamento, IComprovante
{
    public Pix(double valor) : base(valor)
    {
    }

    public override void ProcessarPagamento()
    {
        Console.WriteLine($"Processando pagamento de R$ {Valor:F2} via Pix.");
    }

    public void EmitirComprovante()
    {
        Console.WriteLine($"Comprovante: Pagamento de R$ {Valor:F2} realizado via Pix.");
    }
}
