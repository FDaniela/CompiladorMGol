namespace CompiladorMGol.Analisador.Exceptions
{
    public class LeituraArquivoExpection : Exception
    {
            public LeituraArquivoExpection() : base("Não foi possível realizar a leitura do arquivo.")
            {
            }
    }
}