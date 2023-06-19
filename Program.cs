using CompiladorMGol.Analisador.Auxiliaries;
using CompiladorMGol.Analisador.Lexico;


//Console.WriteLine("Hello, World!");

 class Program
{
    private static void Main(string[] args)
    {
        Lexico lexico = new();

        //lexico.ImprimeTabelaDeTransicao();

        Console.WriteLine("----------------     SAÍDA NA TELA DO COMPUTADOR      ------------");
        Console.WriteLine();
        while (!lexico.ParaScanner)
        {
           Console.WriteLine(lexico.Scanner().ToString());

        }
            Console.WriteLine("-------------------------------------------------------------------");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

        //lexico.ImprimeTabelaDeSimbolos();

           
    }
}
//}

