using CompiladorMGol.Analisador.Auxiliaries;
using CompiladorMGol.Analisador.Léxico;


namespace CompiladorMGol.Analisador.Semântico
{
    public class Semantico
    {

        private TabelaDeSimbolos tabelaDeSimbolos;
        private Stack<Token> pilhaSemantica;
        private Gerador gerador;
        private ErrorLog log;
        private int variavelTX = 0;

        public Semantico(TabelaDeSimbolos tabelaDeSimbolos, Stack<Token> pilhaSemantica)
        {
            gerador = new();
            log = new();

            this.tabelaDeSimbolos = tabelaDeSimbolos;
            this.pilhaSemantica = pilhaSemantica;
        }

        public void AplicarRegraSemantica(Gramatica gramatica, Token token, int linha, int coluna)
        {
            switch (gramatica.Antecessor.Trim())
            {
                case "LV":
                    Regra_LV(gramatica);
                    break;
                case "TIPO":
                    Regra_TIPO(gramatica, token);
                    break;
                case "L":
                    Regra_L(gramatica, token);
                    break;
                case "D":
                    Regra_D();
                    break;
                case "ES":
                    Regra_ES(gramatica, linha, coluna);
                    break;
                case "ARG":
                    Regra_ARG(gramatica, linha, coluna);
                    break;
                case "CMD":
                    Regra_CMD(linha, coluna);
                    break;
                case "LD":
                    Regra_LD(gramatica, linha, coluna);
                    break;
                case "OPRD":
                    Regra_OPRD(gramatica, linha, coluna);
                    break;
                case "COND":
                    Regra_COND();
                    break;
                case "CAB":
                    Regra_CAB();
                    break;
                case "EXP_R":
                    Regra_EXP_R(linha, coluna);
                    break;
                case "CP":
                    Regra_CP(gramatica);
                    break;
                case "V":
                    break;
                case "A":
                    break;
                case "P":
                    break;
                default:
                    log.ImprimeErroSemantico($"ERRO SEMÂNTICO - Regra gramatical \"{gramatica.ToString()}\" inválida encontrada na linha {linha} e coluna {coluna}.");
                    break;
            }
        }

        private void Regra_LV(Gramatica gramatica)
        {
            if (gramatica.Sucessor.Contains("pt_v"))
            {
                var pt_v = pilhaSemantica.Pop();
                var varfim = pilhaSemantica.Pop();

                var lv = new Token("LV", "LV", varfim.Tipo);
                pilhaSemantica.Push(lv);

                gerador.QuebraDeLinha();
                gerador.QuebraDeLinha();
                gerador.QuebraDeLinha();
            }
        }
        private void Regra_D()
        {
            var pt_v = pilhaSemantica.Pop();
            var l = pilhaSemantica.Pop();
            var tipo = pilhaSemantica.Pop();

            var d = new Token("D", "D", tipo.Tipo);
            pilhaSemantica.Push(d);
            gerador.QuebraDeLinhaVariavel();
        }
        private void Regra_L(Gramatica gramatica, Token token)
        {

            if (gramatica.Sucessor.Contains("vir"))
            {
                var tipo = pilhaSemantica.Where(p => p.Classe == "TIPO").FirstOrDefault();

                var l = pilhaSemantica.Pop();
                var vig = pilhaSemantica.Pop();
                var id = pilhaSemantica.Pop();

                var l2 = new Token("L", "L", l.Tipo);
                
                id.Tipo = tipo.Tipo;
                
                gerador.ImprimeVariavel($"{tipo.Tipo} {id.Lexema};\n");
                gerador.QuebraDeLinha();
                tabelaDeSimbolos.AtualizarToken(id);
            
                pilhaSemantica.Push(l2);
            }
            else
            {
                var tipo = pilhaSemantica.Where(p => p.Classe == "TIPO").FirstOrDefault();
                var id = pilhaSemantica.Pop();

                var l = new Token("L", "L", id.Tipo);

                id.Tipo = tipo.Tipo;

                gerador.ImprimeVariavel(" " + id.Lexema + ";\n");
                gerador.QuebraDeLinhaVariavel();
                tabelaDeSimbolos.AtualizarToken(id);
                pilhaSemantica.Push(l);
            }
        }
        private void Regra_TIPO(Gramatica gramatica, Token token)
        {
            if (gramatica.Sucessor.Contains("inteiro"))
                token.Tipo = "inteiro";

            else if (gramatica.Sucessor.Contains("real"))
                token.Tipo = "real";

            else if (gramatica.Sucessor.Contains("literal"))
                token.Tipo = "literal";


            var tipo = pilhaSemantica.Pop();
            Token tipo2 = new Token("TIPO", "TIPO", token.Tipo);

            gerador.ImprimeVariavel($"{ConversorTipo(tipo2)} ");
            tabelaDeSimbolos.AtualizarToken(token);
            pilhaSemantica.Push(tipo2);
        }
        private void Regra_ES(Gramatica gramatica, int linha, int coluna)
        {

            if (gramatica.Sucessor.Contains("leia"))
            {
                var ptv = pilhaSemantica.Pop();
                var id = pilhaSemantica.Pop();
                var leia = pilhaSemantica.Pop();

                var idt = tabelaDeSimbolos.BuscarToken(id.Lexema);
                if (idt != null)
                {
                    if (idt.Tipo == "literal")
                    {
                        gerador.ArquivoFinal($"scanf(\"%s\", {idt.Lexema});\n");
                    }
                    else if (idt.Tipo == "int")
                    {
                        gerador.ArquivoFinal($"scanf(\"%d\", &{idt.Lexema});\n");
                    }
                    else if (idt.Tipo == "double")
                    {
                        gerador.ArquivoFinal($"scanf(\"%lf\", &{idt.Lexema});\n");
                    }
                    gerador.QuebraDeLinha();
                }
                else
                {
                   log.ImprimeErroSemantico($"ERRO SEMÂNTICO - Variável não declarada encontrada na linha {linha} e coluna {coluna}.");
                }
                var es = new Token("ES", "ES", id.Tipo);
                pilhaSemantica.Push(es);
            }
            else if (gramatica.Sucessor.Contains("escreva"))
            {
                var pt_v = pilhaSemantica.Pop();
                var arg = pilhaSemantica.Pop();
                var escreva = pilhaSemantica.Pop();

                var es = new Token("ES", "ES", arg.Tipo);

                pilhaSemantica.Push(es);
                if (arg.Tipo == "literal")
                    gerador.ArquivoFinal($"printf(\"%s\", {arg.Lexema});\n");
                else if (arg.Tipo == "int")
                    gerador.ArquivoFinal($"printf(\"%d\", {arg.Lexema});\n");
                else if (arg.Tipo == "double")
                    gerador.ArquivoFinal($"printf(\"%lf\", {arg.Lexema});\n");
                else
                    gerador.ArquivoFinal($"printf({arg.Lexema});\n");
                gerador.QuebraDeLinha();
            }
        }
        private void Regra_ARG(Gramatica gramatica, int linha, int coluna)
        {

            if (gramatica.Sucessor.Contains("lit"))
            {
                var lit = pilhaSemantica.Pop();
                var arg = new Token("ARG", lit.Lexema, lit.Tipo);
                pilhaSemantica.Push(arg);

            }
            else if (gramatica.Sucessor.Contains("num"))
            {
                var num = pilhaSemantica.Pop();
              
                var arg = new Token("ARG", num.Lexema, num.Tipo);
                pilhaSemantica.Push(arg);


            }
            else if (gramatica.Sucessor.Contains("id"))
            {
                var id = pilhaSemantica.Pop();
                if (id.Classe == "id" && id.Tipo != "Nulo")
                {
                    var arg = new Token("ARG", id.Lexema, id.Tipo);
                    pilhaSemantica.Push(arg);
                }
                else
                {
                  log.ImprimeErroSemantico($"ERRO SEMÂNTICO - Variável não declarada encontrada na linha {linha} e coluna {coluna}.");

                }
            }

        }
        private void Regra_CMD(int linha, int coluna)
        {
            var pt_v = pilhaSemantica.Pop();
            var ld = pilhaSemantica.Pop();
            var rcb = pilhaSemantica.Pop();
            var id = pilhaSemantica.Pop();
            if (id.Tipo != "Nulo")
            {
                if (id.Tipo == ld.Tipo)
                {
                    gerador.ArquivoFinal($"{id.Lexema}={ld.Lexema};\n");
                    gerador.QuebraDeLinha();
                }
                else
                {
                    log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Tipos diferentes para atribuição encontrados na linha {linha} e coluna {coluna}.");
                }
                var cmd = new Token("CMD", "CMD", "Nulo");
                pilhaSemantica.Push(cmd);
            }
            else
            {
                log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Variável não declarada encontrada na linha {linha} e coluna {coluna}.");
            }
        }
        private void Regra_LD(Gramatica gramatica, int linha, int coluna)
        {

            if (gramatica.Sucessor.Contains("opm"))
            {
                var oprd1 = pilhaSemantica.Pop();
                var opa = pilhaSemantica.Pop();
                var oprd2 = pilhaSemantica.Pop();

                var t = $"T{variavelTX++}";

                var tmp = ConversorTipo(oprd1);

                gerador.ImprimeVariaveisTemporarias($"{tmp} {t};\n");
                var ld = new Token("LD", t, oprd1.Tipo);

                if (oprd1.Tipo == oprd2.Tipo && oprd1.Tipo != "literal")
                {
                    pilhaSemantica.Push(ld);
                    gerador.ArquivoFinal($"{t}={oprd2.Lexema}{opa.Lexema}{oprd1.Lexema};\n");
                    gerador.QuebraDeLinha();
                }
                else
                {
                    log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Operandos com tipos incompatíveis encontrados na linha {linha} e coluna {coluna}.");
                }
            }
            else
            {
                var oprd = pilhaSemantica.Pop();
        
                var ld = new Token("LD", oprd.Lexema, oprd.Tipo);
                pilhaSemantica.Push(ld);
            }

        }
        private void Regra_OPRD(Gramatica gramatica, int linha, int coluna)
        {
            bool eRegra22 = gramatica.Sucessor.Contains("id");
            if (eRegra22)
            {
                var id = pilhaSemantica.Pop();
                var idt = tabelaDeSimbolos.BuscarToken(id.Lexema);
                if (idt != null)
                {
                    var oprd = new Token("OPRD", idt.Lexema, idt.Tipo);
                    pilhaSemantica.Push(oprd);
                }
                else
                {
                    log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Variável não declarada encontrada na linha {linha} e coluna {coluna}.");
                }
            }
            else
            {
                var num = pilhaSemantica.Pop();
                var oprd = new Token("OPRD", num.Lexema, num.Tipo);
                pilhaSemantica.Push(oprd);
            }
        }
        private void Regra_COND()
        {
            var cp = pilhaSemantica.Pop();
            var cab = pilhaSemantica.Pop();
            var cond = new Token("COND", "COND", cab.Tipo);

            gerador.QuebraDeLinha();
            gerador.ArquivoFinal("}\n");
            gerador.QuebraDeLinha();
        }
        private void Regra_CAB()
        {
            var entao = pilhaSemantica.Pop();
            var fc_p = pilhaSemantica.Pop();
            var ext_r = pilhaSemantica.Pop();
            var ab_p = pilhaSemantica.Pop();
            var se = pilhaSemantica.Pop();

            var cab = new Token("CAB", "CAB", ext_r.Tipo);

            pilhaSemantica.Push(cab);

            gerador.ArquivoFinal($"if({ext_r.Lexema})\n");
            gerador.QuebraDeLinha();
            gerador.ArquivoFinal("{\n");
            gerador.QuebraDeLinha();
        }
        private void Regra_EXP_R(int linha, int coluna)
        {
            var oprd = pilhaSemantica.Pop();
            var opr = pilhaSemantica.Pop();
            var oprd2 = pilhaSemantica.Pop();

            var tx = $"T{variavelTX++}";

            gerador.ImprimeVariaveisTemporarias($"{ConversorTipo(oprd)} {tx};\n");
            
            var exp_r = new Token("EXR_R", tx, oprd.Tipo);

            pilhaSemantica.Push(exp_r);

            if (oprd.Tipo == oprd2.Tipo)
            {
                gerador.ArquivoFinal($"{tx}={oprd2.Lexema}{opr.Lexema}{oprd.Lexema};\n");
                gerador.QuebraDeLinha();
            }
            else
            {
                log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Operandos com tipos incompatíveis encontrados na linha {linha} e coluna {coluna}.");
            }

        }
        private void Regra_CP(Gramatica gramatica)
        {

            if (gramatica.Sucessor.Contains("fimse"))
            {
                var cp2 = new Token("CP", "CP", "Nulo");
                pilhaSemantica.Push(cp2);
            }
            else
            {

                var token1 = pilhaSemantica.Pop();
                var token2 = pilhaSemantica.Pop();
                var cp2 = new Token("CP", "CP", token2.Tipo);
                pilhaSemantica.Push(cp2);
            }
        }

        private string ConversorTipo(Token token)
        {
           
            if (token.Tipo == "inteiro")
            {
                return token.Tipo = "int";
            }
            else if (token.Tipo == "real")
            {
                return token.Tipo = "double";
            }
            else if (token.Tipo == "literal")
            {
                return token.Tipo = "literal";
            }
            else
            {
                return token.Tipo.ToString();
            }
        }
        public void FinalisaGeracaoArquivo()
        {
            gerador.GerarCodigoFinal();
        }

    }

}
