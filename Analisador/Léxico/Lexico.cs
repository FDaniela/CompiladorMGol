using System.Reflection;
using System.Text;
using CompiladorMGol.Analisador.Auxiliaries;
using CompiladorMGol.Analisador.Exceptions;

namespace CompiladorMGol.Analisador.Léxico
{
    public class Lexico
    {
        private StreamReader? reader;
        private ErrorLog log = new();
        private Token token;

        private TabelaDeSimbolos tabelaDeSimbolos = new();
        private TabelaDeTrasicao tabelaDeTrasicao = new();
        private Alfabeto alfabeto = new();
        private Arquivo arquivo = new();

        public bool ParaScanner { get; set; }
        public int caracter, linha = 1, coluna = 0;
        long tamanhoArquivo, posicao;
        string palavraAtual = "", palavraBuffer = "";
        bool ignorar, lit = false, com = false;

        public Lexico()
        {
            Scanner();
        }

        public Token Scanner()
        {
            reader = arquivo.LeituraArquivo();
            //LeituraArquivo();

            StringBuilder caracterArquivo = new StringBuilder();
            tamanhoArquivo = reader.BaseStream.Length;
            reader.BaseStream.Position = posicao;

            while (posicao <= tamanhoArquivo)
            {

                char caractere = (char)reader.Read();

                //System.Console.WriteLine($"LOG: Caractere lido = {caractere}");
                //System.Console.WriteLine($"LOG: Posicao = {posicao}");

                if (reader.EndOfStream)
                {
                    if (palavraAtual.Length > 0)
                    {
                        palavraAtual += caractere;
                        return NovoToken(palavraAtual);
                    }
                    ParaScanner = true;
                    return NovoToken("EOF"); ;
                }

                if (lit)
                {
                    palavraBuffer += caractere;
                    if (caractere == alfabeto.ASPAS_DUPLAS)
                    {

                        lit = false;
                        palavraAtual = "";
                        posicao++;
                        return NovoToken(palavraBuffer);

                    }
                    else if (caractere == alfabeto.SALTO_DE_LINHA)
                    {
                        //ErrorLog.
                        //System.Console.WriteLine("7777777777777777");
                    }
                    posicao++;
                    continue;
                }
                if (caractere == alfabeto.ASPAS_DUPLAS)
                {
                    palavraBuffer += caractere;
                    lit = !lit;
                    posicao++;
                    continue;
                }
                if (ignorar)
                {

                    palavraBuffer += caractere;

                    if (!alfabeto.CaracterValido(caractere))
                    {
                        log.ImprimeErroLexico($"ERRO LÉXICO - Caractere inválido para linguagem MGol.\tLinha {linha}, Coluna {coluna}.");
                        com = true;
                        posicao++;
                        return ErroToken(caractere.ToString());

                    }

                    if (caractere == alfabeto.FECHA_CHAVES)
                    {

                        ignorar = false;
                        if (!com) ImprimeComentario(palavraBuffer);
                        posicao++;

                    }

                    posicao++;
                    continue;
                }
                if (caractere == alfabeto.ABRE_CHAVES)
                {
                    palavraBuffer += caractere;
                    ignorar = true;
                    com = false;
                    posicao++;
                    continue;
                }

                if (alfabeto.CaracterValido(caractere) && !ignorar)
                {

                    Contagem(caractere);


                    if (char.IsWhiteSpace(caractere) || alfabeto.CaractereEspecial(caractere))
                    {

                        if (!string.IsNullOrEmpty(palavraAtual))
                        {
                            return NovoToken(palavraAtual);

                        }
                        if (alfabeto.CaractereEspecial(caractere) && alfabeto.CaractereEspecial((char)reader.Peek()))
                        {

                            palavraAtual += caractere.ToString();
                            palavraAtual += (char)reader.Peek();
                            posicao++;
                        }



                        // else if ((char)reader.Peek() == '.' &&
                        //         (alfabeto.CaracterDigito(caractere))
                        //         )
                        // {
                        //     System.Console.WriteLine("here");
                        //     //System.Console.WriteLine(palavraAtual);
                        //     palavraAtual += caractere.ToString();
                        //     palavraAtual += (char)reader.Peek();
                        //     posicao++;
                        //    // 

                        //    // posicao++;
                        //    // return NovoToken(caractere.ToString());
                        // }


                        else if (alfabeto.CaractereEspecial(caractere) &&
                                (char.IsLetterOrDigit((char)reader.Peek()) || char.IsWhiteSpace((char)reader.Peek()))
                                )
                        {
                            posicao++;
                            return NovoToken(caractere.ToString());
                        }

                    }

                    else
                    {

                        palavraAtual += caractere;

                    }
                }

                else
                {

                    palavraAtual += caractere;
                    log.ImprimeErroLexico($"ERRO LÉXICO - Caractere inválido para linguagem MGol.\tLinha {linha}, Coluna {coluna}.");
                    posicao++;
                    return ErroToken(palavraAtual);

                }

                posicao++;

            }

            reader.Close();

            throw new RetornoScannerExpection();
        }

        private Token? ExisteToken(string lexema)
        {

            var tokenTemporario = tabelaDeSimbolos.BuscarToken(lexema);

            if (tokenTemporario == null)
            {
                return null;
            }
            else
            {
                return tokenTemporario;
            }

        }

        private Token NovoToken(string lexema)
        {
            palavraAtual = "";
            palavraBuffer = "";

            if (lexema.Equals("EOF")) return new Token("EOF", lexema, "Nulo");

            Token? verificaToken = ExisteToken(lexema);

            if (!(ExisteToken(lexema) == null)) return verificaToken;

            switch (tabelaDeTrasicao.EstadoDeTransicao(lexema, 0, 1))
            {
                case 1:
                    return new Token("Num", lexema, "inteiro");
                case 3:
                    return new Token("Num", lexema, "real");
                case 6:
                    return new Token("Num", lexema, "real");
                case 8:
                    {
                        Token novoToken = new Token("id", lexema, "Nulo");
                        tabelaDeSimbolos.InserirToken(novoToken);
                        return novoToken;
                    }
                case 7:
                    return new Token("Vir", lexema, "Nulo");
                case 19:
                    return new Token("PT_V", lexema, "Nulo");
                case 15:
                    return new Token("RCB", lexema, "Nulo");
                case 16:
                    return new Token("OPM", lexema, "Nulo");
                case 17:
                    return new Token("AB_P", lexema, "Nulo");
                case 18:
                    return new Token("FC_P", lexema, "Nulo");
                case 12:
                    return new Token("OPR", lexema, "Nulo");
                case 13:
                    return new Token("OPR", lexema, "Nulo");
                case 14:
                    return new Token("OPR", lexema, "Nulo");
                case 10:
                    { //Comentário
                        return Scanner();
                    }
                case 21:
                    return new Token("Lit", lexema, "Nulo");
                case 99: //trasição não existe
                    {
                        throw new TransicaoException();
                    }
                case 404:
                    return Scanner();
                    //throw new NotImplementedException("caractere não reconhecido pela linguagem MGol");
            }

            throw new NovoTokenExpection();

        }

        public void Contagem(char caractere)
        {

            if ('\n' == caractere)
            {
                coluna = 0;
                linha++;
            }
            else
            {
                coluna++;
            }

        }

        private Token ErroToken(string lexema)
        {
            palavraAtual = "";
            palavraBuffer = "";
            return new Token("ERRO", lexema, "Nulo");
        }

        public void ImprimeErroLexico()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"ERRO LÉXICO - Caractere inválido para linguagem MGol.\tLinha {linha}, Coluna {coluna}.");
            Console.ResetColor();
        }

        public void ImprimeComentario(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Error.WriteLine($"COMENTÁRIO IGNORADO - {msg.PadRight(33)} Linha {linha}, Coluna {coluna}.");
            Console.ResetColor();
        }

        public void ImprimeTabelaDeSimbolos()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------     TABELA DE SIMBOLOS      ------------");
            Console.WriteLine();
            Console.WriteLine(tabelaDeSimbolos.ToString());
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }

        public void ImprimeTabelaDeTransicao()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            tabelaDeTrasicao.ImprimirTabela();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }

    }
}
