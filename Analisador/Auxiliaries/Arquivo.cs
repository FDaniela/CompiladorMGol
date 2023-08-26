using CompiladorMGol.Analisador.Exceptions;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Arquivo
    {

        private string path = Directory.GetCurrentDirectory().ToString();

        public StreamReader LeituraArquivo()
        {
            try
            {

                var arquivo = path + "/Analisador/Resource/Codes/Fonte.alg";
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
                var arquivo = path + "/Analisador/Resource/Tabela de Análise SLR.csv";
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
                var arquivo = path + "/Analisador/Resource/Gramática.txt";
                return new StreamReader(arquivo);
            }
            catch (Exception)
            {
                throw new LeituraArquivoGramaticaExpection();
            }
        }

    }
}
