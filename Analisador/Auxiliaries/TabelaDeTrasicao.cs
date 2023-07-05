
namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class TabelaDeTrasicao
    {
        int[,] tabela = new int[31, 23];
        private Alfabeto alfabeto = new();

        public TabelaDeTrasicao()
        {

            PrencherTabelaComEstadosPossiveis();

        }

        public void PrencherTabelaComEstadosPossiveis()
        {
            IniciarlizarTabela();

            EstadoIgnorar();
            EstadoFinalNum();
            EstadoFinalLit();
            EstadoFinalId();
            EstadoFinalComentario();
            EstadoFinalEOF();
            EstadoFinalOPR();
            EstadoFinalRCB();
            EstadoFinalOPM();
            EstadoFinalAB_P();
            EstadoFinalFC_P();
            EstadoFinalPT_V();
            EstadoFinalVIR();
        }

        private void IniciarlizarTabela()
        {
            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                for (int j = 0; j < tabela.GetLength(1); j++)
                {
                    tabela[i, j] = -1;
                }
            }
        }

        private void EstadoIgnorar()
        {
            tabela[1, 1] = 0;
            tabela[2, 1] = 0;
            tabela[3, 1] = 0;
        }

        private void EstadoFinalVIR()
        {
            tabela[7, 1] = 7;
        }

        private void EstadoFinalPT_V()
        {
            tabela[8, 1] = 19;
        }

        private void EstadoFinalFC_P()
        {
            tabela[19, 1] = 18;
        }

        private void EstadoFinalAB_P()
        {
            tabela[18, 1] = 17;
        }

        private void EstadoFinalOPM()
        {
            tabela[14, 1] = 16;
            tabela[15, 1] = 16;
            tabela[16, 1] = 16;
            tabela[17, 1] = 16;
        }

        private void EstadoFinalRCB()
        {
            tabela[24, 1] = 12;
            tabela[16, 13] = 15;
        }

        private void EstadoFinalOPR()
        {
            tabela[24, 1] = 12;
            tabela[25, 1] = 14;
            tabela[25, 13] = 13;
            tabela[26, 1] = 13;
            tabela[26, 13] = 13;
            tabela[26, 15] = 13;
        }

        private void EstadoFinalEOF()
        {
            tabela[30, 1] = 11;
        }

        private void EstadoFinalComentario()
        {
            // tabela[10, 10] = 9;
            tabela[20, 1] = 9;
            for (int i = 1; i < 31; i++)
            {
                tabela[i, 10] = 9;
            }


            tabela[21, 10] = 10;

        }

        private void EstadoFinalId()
        {
            tabela[5, 1] = 8;
            tabela[5, 9] = 8;

            tabela[4, 9] = 8;

            tabela[29, 9] = 8;

        }

        private void EstadoFinalLit()
        {
            tabela[28, 1] = 20;
            //tabela[10, 21] = 20;


            for (int i = 1; i < 31; i++)
            {
                tabela[i, 21] = 20;
            }
            tabela[28, 21] = 21;

        }

        private void EstadoFinalNum()
        {

            tabela[4, 1] = 1;
            tabela[4, 2] = 1;
            tabela[4, 3] = 3;
            tabela[4, 4] = 3;
            tabela[4, 5] = 6;
            tabela[4, 6] = 6;
            tabela[4, 7] = 6;
            tabela[4, 9] = 8;

            tabela[6, 2] = 4;
            tabela[6, 4] = 4;

            tabela[10, 2] = 2;
            //tabela[10, 0] = 2;

            tabela[15, 5] = 5;
            tabela[16, 5] = 5;

        }

        public int ConsultarTransicao(int linha, int coluna)
        {
            return tabela[linha, coluna];
        }

        public int EstadoDeTransicao(string caractere, int index, int estado)
        {

            int linha, coluna;
            linha = LinhaDeTransicao(caractere[index]);
            coluna = ConsultarTransicao(linha, estado) + 1;

            //System.Console.WriteLine($"LOG: {caractere} ,\t{index+1}-{estado}");
            //System.Console.WriteLine($"LOG: {linha},{coluna}");

            if (linha == 0) return 404;

            if (ConsultarTransicao(linha, estado) == -1) return 99;

            if (index == caractere.Length - 1) return ConsultarTransicao(linha, estado);

            return EstadoDeTransicao(caractere, index + 1, coluna);

        }

        public int LinhaDeTransicao(char caracter)
        {

            if (caracter == alfabeto.ESPACO) return 1;
            if (caracter == '\r') return 1;

            if (alfabeto.CaracterDigito(caracter)) return 4;
            if (alfabeto.CaracterLetra(caracter)) return 5;

            if (caracter == alfabeto.VIRGULA) return 7;
            if (caracter == alfabeto.PONTO_E_VIRGULA) return 8;
            if (caracter == alfabeto.DOIS_PONTOS) return 9;

            if (caracter == alfabeto.PONTO) return 10;
            if (caracter == alfabeto.EXCLAMACAO) return 11;
            if (caracter == alfabeto.INTEROGACAO) return 12;
            if (caracter == alfabeto.BARRA_INVERTIDA) return 13;

            if (caracter == alfabeto.ASTERISTICO) return 14;
            if (caracter == alfabeto.MAIS) return 15;
            if (caracter == alfabeto.MENOS) return 16;
            if (caracter == alfabeto.BARRA) return 17;

            if (caracter == alfabeto.ABRE_PARENTESES) return 18;
            if (caracter == alfabeto.FECHA_PARENTENSES) return 19;
            if (caracter == alfabeto.ABRE_COLCHETES) return 22;
            if (caracter == alfabeto.ABRE_CHAVES) return 20;
            if (caracter == alfabeto.FECHA_CHAVES) { return 21; }
            if (caracter == alfabeto.FECHA_COLCHETES) { return 23; }
            if (caracter == alfabeto.MENOR) return 24;
            if (caracter == alfabeto.MAIOR) return 25;
            if (caracter == alfabeto.IGUAL) return 26;

            if (caracter == alfabeto.ASPAS_SIMPLES) return 27;
            if (caracter == alfabeto.ASPAS_DUPLAS) return 28;

            if (caracter == alfabeto.SOBRE_LINHA) return 29;


            //--------------------------------


            return 0;

        }

        public void ImprimirTabela()
        {
            for (int i = 1; i < tabela.GetLength(0); i++)
            {
                for (int j = 1; j < tabela.GetLength(1); j++)
                    Console.Write(tabela[i, j].ToString().PadRight(5));
                Console.WriteLine();
            }
        }

    }
}
