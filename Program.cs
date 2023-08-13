using CompiladorMGol.Analisador.Sintático;
using CompiladorMGol.Analisador.Léxico;

//AnalisadorLexico();
AnalisadorSintatico_Semantico();

static void AnalisadorLexico()
{

    Lexico lexico = new();

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("-------------------     SAÍDA ANÁLISE LÉXICA      -----------------------");
    Console.WriteLine();
    while (!lexico.ParaScanner)
    {
        Console.WriteLine(lexico.Scanner());
    }
    Console.WriteLine("---------------------------------------------------------------------------");
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();

    //lexico.ImprimeTabelaDeSimbolos();
}

static void AnalisadorSintatico_Semantico()
{
    Sintatico sintatico_semantico = new();
}