using CompiladorMGol.Analisador.Exceptions;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Arquivo
    {

        public StreamReader LeituraArquivo()
        {

            try
            {
                var arquivo = "/home/danfer/Projetos/CompiladorMGol/Analisador/Resource/Codes/Fonte4.alg";
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
                var arquivo = "/home/danfer/Projetos/CompiladorMGol/Analisador/Resource/Tabela de Análise SLR.csv";
                return new StreamReader(arquivo);
            }
            catch (Exception)
            {
                throw new LeituraArquivoGramaticaExpection();
            }
        }

        public StreamReader LeituraAlfabeto(){
             try
            {
                var arquivo = "/home/danfer/Projetos/CompiladorMGol/Analisador/Resource/Gramática.txt";
                return new StreamReader(arquivo);
            }
            catch (Exception)
            {
                throw new LeituraArquivoGramaticaExpection();
            }
        }
        
        
        // var nomeArquivoCompleto = "CompiladorMGol.Analisador.Resource.Codes.Fonte0.alg";
        // var assembly = Assembly.GetExecutingAssembly();
        // var resourceStream = assembly.GetManifestResourceStream(nomeArquivoCompleto);
        // var arquivo = resourceStream;
        // Console.WriteLine(arquivo.ToString());

    }
}