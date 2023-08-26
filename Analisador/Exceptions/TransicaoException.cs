
namespace CompiladorMGol.Analisador.Exceptions
{
    public class TransicaoException : Exception
    {

        public TransicaoException() : base("Não existe transição possível para a entrada apresentado.")
        {
        }
        

    }
}