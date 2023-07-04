using CompiladorMGol.Analisador.Auxiliaries;
using CompiladorMGol.Analisador.Léxico;

namespace CompiladorMGol.Analisador.Sintático
{
    public class Sintatico
    {

        private TabelaDaGramatica tabelaDaGramatica = new();
        private Lexico lexico = new();
        private Gramatica gramatica = new();
        Stack<int> pilha = new Stack<int>();
        private bool entradaAceita = false;
        private bool rotinaDeErro = false;
        private Token? token;

        public Sintatico()
        {

            gramatica.Producoes();
            //gramatica.ImprimirProduçoesGramatica();
            AnaliseAscendenteSR();
            //System.Console.WriteLine(tabelaDaGramatica.ToString());
        }

        public void AnaliseAscendenteSR()
        {
            pilha.Push(0);
            token = lexico.Scanner();

            while (!entradaAceita && !rotinaDeErro)
            {
                int estado = pilha.Peek();

                // System.Console.WriteLine("["+estado+","+gramatica.IdentificadorDaProducao(token.Classe)+"]");

                var acao = tabelaDaGramatica.ConsultarSLR(estado, gramatica.IdentificadorDaProducao(token.Classe.ToLower()));

                //System.Console.WriteLine("LOG: Ação=" + acao +" pilha= "+estado+ " token-classe " + token.Lexema);
                //ImprimePilha();

                if ('s'.Equals(acao.Trim().ToCharArray()[0]))
                {
                    // System.Console.WriteLine("Empilhou");

                    var goTo = acao.Remove(0, 1);
                    pilha.Push(int.Parse(goTo));
                    token = lexico.Scanner();

                }
                else if ('r'.Equals(acao.Trim().ToCharArray()[0]))
                {
                    //System.Console.WriteLine("Reduziu");

                    Gramatica regraReduzida = gramatica.Producoes(acao);

                    Console.WriteLine(regraReduzida.ToString());

                    int desempilhar = regraReduzida.Sucessor.Length;
                    for (int i = 0; i < desempilhar; i++)
                        pilha.Pop();

                    var t = pilha.Peek();
                    var nova_acao = tabelaDaGramatica.ConsultarSLR(t, gramatica.IdentificadorDaProducao(regraReduzida.Antecessor.Trim()));
                    int novo_estado = int.Parse(nova_acao);
                    pilha.Push(novo_estado);
                }
                else if ("acc".Equals(acao.Trim().ToLower()))
                {
                    //System.Console.WriteLine("Aceitou");
                    entradaAceita = true;

                }
                else
                {
                    //Rotina de recuperação de erro
                    rotinaDeErro = true;
                    System.Console.WriteLine("não rolou");
                }
            }

        }
        private void ImprimePilha()
        {
            Console.Write("LOG: Pilha: ");
            foreach (var p in pilha)
            {
                Console.Write($"{p} ");
            }
            Console.WriteLine();
        }

    }
}