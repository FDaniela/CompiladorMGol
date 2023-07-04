using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompiladorMGol.Analisador.Exceptions
{
    public class RegraExpection : Exception
    {
        public RegraExpection() : base("Regra de Produção inválida"){

        }
    }


}