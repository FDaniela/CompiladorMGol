namespace CompiladorMGol.Analisador.Exceptions
{
    public class LeituraArquivoFonteExpection : Exception
    {
            public LeituraArquivoFonteExpection() : base("Não foi possível realizar a leitura do arquivo-fonte.")
            {
            }
    }
}