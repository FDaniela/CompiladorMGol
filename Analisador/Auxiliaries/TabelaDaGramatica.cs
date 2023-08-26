
namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class TabelaDaGramatica
    {
        private string[,] tabela;
        private Dictionary<string, int> simbolo = new();
        private Arquivo arquivo = new();

        public TabelaDaGramatica()
        {
            tabela = LerArquivoCSV(arquivo.LeituraGramatica());
        }
        private string[,] LerArquivoCSV(StreamReader reader)
        {
            List<string[]> linhas = new List<string[]>();

            int linha = 0;

            var linhaComSimbolos = reader.ReadLine();
            var vetorDeSimbolos = linhaComSimbolos.Split(',');

            for (int k = 1; k < vetorDeSimbolos.Length; k++)
            {
                simbolo.Add(vetorDeSimbolos[k].Trim(), k);
            }

            while (!reader.EndOfStream)
            {
                var linhaLida = reader.ReadLine();
                var movimentos = linhaLida.Split(',');

                for (int i = 0; i < movimentos.Length; i++)
                {
                    if (string.IsNullOrEmpty(movimentos[i]))
                    {
                        movimentos[i] = $"E{linha}";
                    }
                }

                linhas.Add(movimentos);
                linha++;
            }

            int numLinhas = linhas.Count;
            int numColunas = linhas[0].Length;

            string[,]
            tabelaCompleta = new string[numLinhas, numColunas];

            for (int i = 0; i < numLinhas; i++)
                for (int j = 0; j < numColunas; j++)
                    tabelaCompleta[i, j] = linhas[i][j];

            return tabelaCompleta;
        }
        public string ConsultarSLR(int linhaLida, int coluna)
        {
            return tabela[linhaLida, coluna];
        }

    }
}