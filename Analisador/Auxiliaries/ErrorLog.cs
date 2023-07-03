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
    }
}