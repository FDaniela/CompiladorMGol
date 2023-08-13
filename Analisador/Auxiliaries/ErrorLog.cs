namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class ErrorLog
    {
        public void ImprimeErroLexico(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(msg);
            Console.ResetColor();
        }

        public void ImprimeErroSintatico(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(msg);
            Console.ResetColor();
        }

        public void ImprimeErroSemantico(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(msg);
            Console.ResetColor();
        }

        public void ImprimeMensagemRecuperacaoInsercao(string tokenEncontrado, string tokenEsperado)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //System.Console.WriteLine();
            System.Console.WriteLine("MODO RECUPERAÇÃO POR INSERÇÃO:");
            System.Console.WriteLine($"O token \"{tokenEncontrado}\" foi entrado no lugar do Token \"{tokenEsperado}\", \npara prosseguir com o processo de análise o Token correto foi inserido.");
            //System.Console.WriteLine();
            Console.ResetColor();
        }

        public void ImprimeMensagemModoPanico()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //System.Console.WriteLine();
            System.Console.WriteLine("MODO PANICO ATIVO:");
            System.Console.WriteLine($"O próximo ponto e virgula será buscado para tentar continuar a execução do programa.");
            //System.Console.WriteLine();
            Console.ResetColor();
        }



    }
}