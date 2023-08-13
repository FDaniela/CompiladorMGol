using CompiladorMGol.Analisador.Exceptions;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Arquivo
    {

        private string path = "/home/danfer/Projetos";

        public StreamReader LeituraArquivo()
        {
            try
            {
                var arquivo = path + "/CompiladorMGol/Analisador/Resource/Codes/Fonte3.alg";
                return new StreamReader(arquivo);
            }
            catch (Exception)
            {
                throw new LeituraArquivoFonteExpection();
            }
        }

        public StreamReader LeituraGramatica()
        {
            try
            {
                var arquivo = path + "/CompiladorMGol/Analisador/Resource/Tabela de Análise SLR.csv";
                return new StreamReader(arquivo);
            }
            catch (Exception)
            {
                throw new LeituraArquivoGramaticaExpection();
            }
        }

        public StreamReader LeituraAlfabeto()
        {
            try
            {
                var arquivo = path + "/CompiladorMGol/Analisador/Resource/Gramática.txt";
                return new StreamReader(arquivo);
            }
            catch (Exception)
            {
                throw new LeituraArquivoGramaticaExpection();
            }
        }

    }
}