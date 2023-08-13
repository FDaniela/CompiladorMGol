using CompiladorMGol.Analisador.Auxiliaries;
using CompiladorMGol.Analisador.Léxico;
using CompiladorMGol.Analisador.Semântico;

namespace CompiladorMGol.Analisador.Sintático
{
    public class Sintatico
    {

        private TabelaDaGramatica tabelaDaGramatica = new();
        private Lexico lexico = new();
        private Gramatica gramatica = new();
        List<Gramatica> gramaticas = new List<Gramatica>();
        List<Token> tokens = new List<Token>();
        private ErrorLog log = new();
        Stack<int> pilha = new Stack<int>();
        private bool entradaAceita = false;
        private bool rotinaDeErro = false;
        private Token? token;
        private Stack<Token> pilhaSemantica = new();
        private Semantico semantico;
        private int estado, topoDaPilha;
        private int controle = 0;
        private string? acao;
        public bool ErroSintatico { get; set; }


        public Sintatico()
        {

            semantico = new Semantico(lexico.TabelaDeSimbolos, pilhaSemantica);

            gramatica.Producoes();
            //gramatica.ImprimirProduçoesGLC(); 
            AnaliseAscendenteSR();

            //ImprimirRegraGramatical();

        }

        public void AnaliseAscendenteSR()
        {

            controle = 0;

            if (!rotinaDeErro)
            {
                pilha.Push(0);
                token = lexico.Scanner();
                tokens.Add(token);
                rotinaDeErro = false;
            }

            while (!entradaAceita)
            {
                estado = pilha.Peek();
                acao = tabelaDaGramatica.ConsultarSLR(estado, gramatica.IdentificadorDaProducao(token.Classe.ToLower()));

                if ('s'.Equals(acao.Trim().ToCharArray()[0]))
                {
                    var goTo = acao.Remove(0, 1);
                    pilha.Push(int.Parse(goTo));
                    // System.Console.WriteLine(token);
                    pilhaSemantica.Push(token);
                    token = lexico.Scanner();
                    tokens.Add(token);
                }
                else if ('r'.Equals(acao.Trim().ToCharArray()[0]))
                {
                    Gramatica regraGramatical = gramatica.Producoes(acao);

                    //Console.WriteLine(regraGramatical.ToString());
                    gramaticas.Add(regraGramatical);

                    int desempilhar = regraGramatical.Sucessor.Length;
                    for (int i = 0; i < desempilhar; i++)
                        pilha.Pop();

                    topoDaPilha = pilha.Peek();
                    var nova_acao = tabelaDaGramatica.ConsultarSLR(topoDaPilha, gramatica.IdentificadorDaProducao(regraGramatical.Antecessor.Trim()));
                    int novo_estado = int.Parse(nova_acao);
                    pilha.Push(novo_estado);

                    semantico.AplicarRegraSemantica(regraGramatical, token, lexico.linha, lexico.coluna);

                }
                else if ("acc".Equals(acao.Trim().ToLower()))
                {
                    entradaAceita = true;
                }
                else
                {
                    controle++;

                    if (token != null) log.ImprimeErroSintatico($"ERRO SINTÁTICO - Token inválido encontrado ({token.Classe}).\tLinha {lexico.linha - 1}, Coluna {lexico.coluna}.");
                    else log.ImprimeErroSintatico($"ERRO SINTÁTICO - Token inválido encontrado ({token.Classe}).\tLinha {lexico.linha}, Coluna {lexico.coluna}.");

                    ErroSintatico = true;
                    RotinhaDeRecuperaçãoDeErro();

                    if (controle > 1)
                    {
                        System.Console.WriteLine("Análise Encerrada....");
                        break;
                    }

                }
            }
            //ImprimirTokens();
            //ImprimirRegraGramatical();
            if (!lexico.ErroLexico && !ErroSintatico && !semantico.ErroSemantico)
                semantico.CriacaoArquivoOBJ();

        }


        public void RotinhaDeRecuperaçãoDeErro()
        {
            RecuperacaoPorInserção();
        }

        private void RecuperacaoPorInserção()
        {

            string tokenEsperado;
            var tokenEncontrado = token.Classe.ToLower();

            switch (acao.Trim())
            {
                case "E0":
                    {
                        TratamentoDeErro("inicio", tokenEncontrado, 2);
                        break;
                    }
                case "E2":
                    {
                        TratamentoDeErro("varinicio", tokenEncontrado, 4);
                        break;
                    }
                case "E4":
                    {
                        TratamentoDeErro("varfim", tokenEncontrado, 7);
                        break;
                    }
                case "E30":
                    {
                        TratamentoDeErro("ab_p", tokenEncontrado, 66);
                        break;
                    }

                case "E69":
                    {
                        TratamentoDeErro("entao", tokenEncontrado, 69);
                        break;
                    }

                case "E25":
                    {
                        TratamentoDeErro("id", tokenEncontrado, 35);
                        break;
                    }

                default:
                    ModoPanico();
                    break;
            }

        }

        private void TratamentoDeErro(string tokenEsperado, string tokenEncontrado, int valorPilha)
        {
            log.ImprimeMensagemRecuperacaoInsercao(tokenEncontrado, tokenEsperado);
            Token tokenTemporario = new Token(tokenEsperado, tokenEsperado, tokenEsperado);
            pilha.Push(valorPilha);
            topoDaPilha = pilha.Peek();
            acao = tabelaDaGramatica.ConsultarSLR(topoDaPilha, gramatica.IdentificadorDaProducao(tokenEsperado));
            rotinaDeErro = true;
            AnaliseAscendenteSR();
        }
        private void ModoPanico()
        {
            log.ImprimeMensagemModoPanico();
            while (token.Classe.Trim().ToLower() != "fim" && !PontoDeSincronizacao(token))
            {
                token = lexico.Scanner();
            }
            rotinaDeErro = true;

        }

        private bool PontoDeSincronizacao(Token token)
        {
            var tokenTmp = token.Classe.Trim().ToLower();
            return tokenTmp == "pt_v";
        }
        public void ImprimirTokens()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-------------------     SAÍDA ANÁLISE LÉXICA      -----------------------");
            Console.WriteLine();
            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        public void ImprimirRegraGramatical()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("--------------------     SAÍDA ANÁLISE SINTÁTICA      --------------------");
            Console.WriteLine();
            foreach (Gramatica gramatica in gramaticas)
            {
                Console.WriteLine(gramatica);
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }


    }
}