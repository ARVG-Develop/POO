namespace Exercicio04_Pagamentos;

public class Pix : Pagamento, IComprovante
{
    public Pix(double valor) : base(valor)
    {
    }

    public override void ProcessarPagamento()
    {
        Console.WriteLine("Estou no método ProcessarPagamento da classe Pix");
    }

    public void EmitirComprovante()
    {
        Console.WriteLine("Estou no método EmitirComprovante da classe Pix");
    }
}
