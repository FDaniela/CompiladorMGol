﻿using CompiladorMGol.Analisador.Sintático;
using CompiladorMGol.Analisador.Léxico;

//AnalisadorLexico();
AnalisadorSintatico();

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
        //Console.WriteLine(lexico.Scanner().Classe);
    }
    Console.WriteLine("---------------------------------------------------------------------------");
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();

    //lexico.ImprimeTabelaDeSimbolos();

}

static void AnalisadorSintatico()
{

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("--------------------     SAÍDA ANÁLISE SINTÁTICA      --------------------");
    Console.WriteLine();
    Sintatico sintatico = new();
    Console.WriteLine();
    Console.WriteLine("---------------------------------------------------------------------------");
    Console.WriteLine();
    Console.WriteLine();

    //sintatico.AnaliseAscendenteSR();

}


