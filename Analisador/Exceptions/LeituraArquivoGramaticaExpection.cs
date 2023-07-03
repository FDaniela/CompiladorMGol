using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompiladorMGol.Analisador.Exceptions 
{

    public class LeituraArquivoGramaticaExpection : Exception
    {
            public LeituraArquivoGramaticaExpection() : base("Não foi possível realizar a leitura do arquivo que contem a gramática.")
            {
            }
    }



}