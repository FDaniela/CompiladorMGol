using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompiladorMGol.Analisador.Exceptions
{
    public class TransicaoException : Exception
    {

        public TransicaoException() : base("Não existe transição possível para a entrada apresentado.")
        {
        }
        

    }
}