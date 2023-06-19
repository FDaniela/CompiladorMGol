using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompiladorMGol.Analisador.Exceptions
{
    public class NovoTokenExpection : Exception
    {

        public NovoTokenExpection() : base("Houve um erro na criação de um novo Token.")
        {
        }

    }
}