
namespace CompiladorMGol.Analisador.Exceptions
{
        public class ConstrucaoSintaticaException : Exception
    {
            public ConstrucaoSintaticaException() : base("O contéudo do arquivo da tabela sintática está em um padrão incorreto")
            {
            }
    }
}