using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using CompiladorMGol.Analisador.Auxiliaries;

namespace CompiladorMGol.Analisador.Lexico
{
    public class Lexico
    {
        private StreamReader reader;
        public bool ParaScanner { get; set; }
        public int caracter;
        long tamanhoArquivo, posicao, buffer;
        private TabelaDeSimbolos tabelaDeSimbolos = new();
        private TabelaDeTrasicao tabelaDeTrasicao = new();
        private Alfabeto alfabeto = new();
        public static int Erro = 0, linha = 1, coluna = 0;
        string palavraAtual = "", palavraBuffer = "";
        //char caractere;
        bool ignorar, lit=false;
        bool teste;

        public Lexico()
        {

            //ImprimeTabelaDeSimbolos();
            //ImprimeTabelaDeTransicao();
            
        }

        public Token Lit()
        {
            //string = 
            
            return NovoToken("\"Digite B:\"");
        }


        public Token Scanner()
        {

            try
            {

                StringBuilder caracterArquivo = new StringBuilder();
                var arquivo = "/home/danfer/Projetos/CompiladorBackup/CompiladorMGol/Analisador/Resource/Fonte0.alg";
                reader = new StreamReader(arquivo);
                tamanhoArquivo = reader.BaseStream.Length;
                reader.BaseStream.Position = posicao;
               // Console.WriteLine("tam= " + tamanhoArquivo);

               


                while (posicao < tamanhoArquivo)
               // while (!reader.EndOfStream)
                {
                  
                    char caractere = (char)reader.Read();
                  // Console.WriteLine(posicao+"/"+tamanhoArquivo+"   =   "+caractere);

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
                            // Console.WriteLine(palavraBuffer);
                            
                            lit = false;
                            palavraAtual = "";
                            posicao++;
                            return NovoToken(palavraBuffer);

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
                        // palavraBuffer += caractere;
                        if (caractere == alfabeto.FECHA_CHAVES) ignorar = false;
                        posicao++;
                        continue;
                    }
                    if (caractere == alfabeto.ABRE_CHAVES)
                    {

                        ignorar = true;
                        posicao++;
                        continue;
                    }




                    if (alfabeto.CaracterValido(caractere) && !ignorar && !teste)
                    {
                     
                        Contagem(caractere);
                        
                       
                        if (char.IsWhiteSpace(caractere) || alfabeto.CaractereEspecial(caractere))// || (alfabeto.CaractereEspecial(caractere) && Char.IsLetterOrDigit((char)reader.Peek())))
                        {

                            if (!string.IsNullOrEmpty(palavraAtual)){
                                return NovoToken(palavraAtual);
                            } 
                            if (alfabeto.CaractereEspecial(caractere) && alfabeto.CaractereEspecial((char)reader.Peek()) )
                            {
                                palavraAtual += caractere.ToString();
                                palavraAtual += (char)reader.Peek();
                                posicao++;
                            }

                            else if (alfabeto.CaractereEspecial(caractere) && (Char.IsLetterOrDigit((char)reader.Peek()) || Char.IsWhiteSpace((char)reader.Peek())))
                            {
                                posicao++;
                                return NovoToken(caractere.ToString());
                            }

                           



                        }

                        else
                        {
                           // teste = !teste;
                            palavraAtual += caractere;

                        }
                    }
                   

                    else
                    {

                        Console.Error.WriteLine($"ERRO léxico - Caractere inválido na linguagem. Linha {linha}, coluna {(posicao+1)/linha}.");

                    }
                   // Console.WriteLine(palavraBuffer);
                    posicao++;

                }

                reader.Close();

            }
            catch (Exception e)
            {
                //throw new NotImplementedException("Erro na leitura do arquivo:" + e + "\n");
                ImprimeErrolexico("Erro na leitura do arquivo:" + e + "\n");
            }
            throw new NotImplementedException("Deu ruim no retorno do Scanner");
        }

        private string TokenJaExiste(string lexema)
        {


            var tokenTemporario = new Token(lexema, lexema, lexema);

            var existe = tabelaDeSimbolos.BuscarToken(tokenTemporario);

            if (existe != null)
            {
                return lexema;
            }
            else
            {
                return "null";
            }

        }

        private Token NovoToken(string lexema)
        {
            palavraAtual = "";
            palavraBuffer = "";

            if (lexema.Equals("EOF"))
            {
                return new Token("EOF", lexema, "Nulo");
            }

            if (TokenJaExiste(lexema) != "null")
            {
                return new Token(lexema, lexema, lexema);
            }

            switch (tabelaDeTrasicao.EstadoDeTransicao(lexema, 0, 1))
            {
                case 1:
                    return new Token("Num", lexema, "inteiro");
                case 3:
                    return new Token("Num", lexema, "real");
                case 6:
                    return new Token("Num", lexema, "real");
                case 8:
                    return new Token("id", lexema, "Nulo");
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
                case 21:
                    return new Token("Lit", lexema, "Nulo");
                case 99:
                    return new Token("transição não existe", lexema, "Nulo");
                case 333:
                    return new Token("craactere não reconhecido", lexema, "Nulo");

            }

            return new Token("---------", lexema, "Nulo");

        }

        public void Contagem(char caractere)
        {

            if ('\n' == caractere)
            {
                coluna = 0;
               // Console.WriteLine(" pulou - " +coluna);
                
                linha++;
            }
            if ('\r' == caractere)
            {
                coluna--;
            }
            else
            {
               coluna++;
            }

        }

        public void ImprimeErrolexico(string msg)
        {
            Console.Error.WriteLine(msg);
        }

        public void ImprimeTabelaDeSimbolos()
        {
            Console.WriteLine("----------------     TABELA DE SIMBOLOS      ------------");
            Console.WriteLine();
            Console.WriteLine(tabelaDeSimbolos.ToString());
            Console.WriteLine("---------------------------------------------------------");
        }

        public void ImprimeTabelaDeTransicao()
        {
            tabelaDeTrasicao.ImprimirTabela();
        }

    }
}
