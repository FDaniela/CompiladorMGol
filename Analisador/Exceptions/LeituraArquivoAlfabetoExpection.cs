using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompiladorMGol.Analisador.Exceptions 
{

    public class LeituraArquivoAlfabetoExpection : Exception
    {
            public LeituraArquivoAlfabetoExpection() : base("Não foi possível realizar a leitura do arquivo que contem o alfabeto.")
            {
            }
    }



}