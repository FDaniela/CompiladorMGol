using System.Text;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class TabelaDaGramatica
    {
        private readonly string[,] table;
        private Dictionary<string, int> primeira_linha_tabela = new();
        private Arquivo arquivo = new();

        public TabelaDaGramatica()
        {
            table = LerArquivoCSV(arquivo.LeituraGramatica());
        }
        private string[,] LerArquivoCSV(StreamReader reader)
        {
            List<string[]> linhas = new List<string[]>();

            int line = 0;

            var firstLine = reader.ReadLine();
            var lineVector = firstLine.Split(',');

            for (int k = 1; k < lineVector.Length; k++)
            {
                primeira_linha_tabela.Add(lineVector[k].Trim(), k);
            }


            while (!reader.EndOfStream)
            {
                var linha = reader.ReadLine();
                var valores = linha.Split(',');

                for (int i = 0; i < valores.Length; i++)
                {
                    if (string.IsNullOrEmpty(valores[i]))
                    {
                        valores[i] = $"E{line}";
                    }
                }

                linhas.Add(valores);
                line++;
            }

            int numLinhas = linhas.Count;
            int numColunas = linhas[0].Length;

            string[,] matriz = new string[numLinhas, numColunas];

            for (int i = 0; i < numLinhas; i++)
            {
                for (int j = 0; j < numColunas; j++)
                {

                    matriz[i, j] = linhas[i][j];
                }
            }
            // for (int i = 0; i < numLinhas; i++)
            // {
            //     for (int j = 1; j < numColunas; j++)
            //     {
            //         Console.Write(matriz[i, j] + ", ");
            //     }
            //     Console.WriteLine();
            // }

            // foreach (var item in primeira_linha_tabela)
            // {
            //     Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            // }

            return matriz;
        }
        public string GetAcao(int linha, int coluna)
        {
            return table[linha, coluna];
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            int linhas = table.GetLength(0);
            int colunas = table.GetLength(1);

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 1; j < colunas; j++)
                    sb.Append(table[i, j] + " ");
                sb.Append("\n");
            }
            sb.Append("\n");

            return sb.ToString();
        }

    }
}