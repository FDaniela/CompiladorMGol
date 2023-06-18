using CompiladorMGol.Analisador.Auxiliaries;
using CompiladorMGol.Analisador.Lexico;


//Console.WriteLine("Hello, World!");

 class Program
{
    private static void Main(string[] args)
    {
        Lexico lexico = new();



        //Console.WriteLine(lexico.Lit().ToString());
        while (!lexico.ParaScanner)
        {
            Console.WriteLine(lexico.Scanner().ToString());

        }

    }
}
//}

