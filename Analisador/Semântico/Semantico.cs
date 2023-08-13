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
        public bool ErroSemantico { get; set; }
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
                    {
                        log.ImprimeErroSemantico($"ERRO SEMÂNTICO - Regra gramatical \"{gramatica.ToString()}\" inválida encontrada na linha {linha} e coluna {coluna}.");
                        ErroSemantico = true;
                        break;
                    }
            }
        }

        private void Regra_LV(Gramatica gramatica)
        {
            if (gramatica.Sucessor.Contains("pt_v"))
            {
                var pt_v = pilhaSemantica.Pop();
                var varfim = pilhaSemantica.Pop();

                var lv = new Token("LV", "LV", "Nulo");
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
        }
        private void Regra_L(Gramatica gramatica, Token token)
        {
            var tipo = pilhaSemantica.Where(p => p.Classe == "TIPO").FirstOrDefault();

            if (gramatica.Sucessor.Contains("vir"))
            {

                var l1 = pilhaSemantica.Pop();
                var vir = pilhaSemantica.Pop();
                var id = pilhaSemantica.Pop();

                id.Tipo = tipo.Tipo;

                gerador.ImprimeVariavel($"{tipo.Tipo} {id.Lexema};\n");
                tabelaDeSimbolos.AtualizarToken(id);

                var l = new Token("L", "L", l1.Tipo);
                pilhaSemantica.Push(l);
            }
            else
            {
                var id = pilhaSemantica.Pop();

                id.Tipo = tipo.Tipo;

                gerador.ImprimeVariavel(" " + id.Lexema + ";\n");
                tabelaDeSimbolos.AtualizarToken(id);

                var l = new Token("L", "L", id.Tipo);
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

                var es = new Token("ES", "ES", id.Tipo);
                pilhaSemantica.Push(es);

                var idt = tabelaDeSimbolos.BuscarToken(id.Lexema);
                if (idt != null)
                {
                    if (idt.Tipo == "literal")
                    {
                        gerador.ImprimeCodigo($"scanf(\"%s\",&{idt.Lexema});\n");
                    }
                    else if (idt.Tipo == "int")
                    {
                        gerador.ImprimeCodigo($"scanf(\"%d\",&{idt.Lexema});\n");
                    }
                    else if (idt.Tipo == "double")
                    {
                        gerador.ImprimeCodigo($"scanf(\"%lf\",&{idt.Lexema});\n");
                    }
                    gerador.QuebraDeLinha();
                }
                else
                {
                    log.ImprimeErroSemantico($"ERRO SEMÂNTICO - Variável não declarada encontrada na linha {linha} e coluna {coluna}.");
                    ErroSemantico = true;
                }

            }
            else if (gramatica.Sucessor.Contains("escreva"))
            {
                var pt_v = pilhaSemantica.Pop();
                var arg = pilhaSemantica.Pop();
                var escreva = pilhaSemantica.Pop();

                var es = new Token("ES", "ES", arg.Tipo);
                pilhaSemantica.Push(es);

                if (arg.Tipo == "literal")
                    gerador.ImprimeCodigo($"printf(\"%s\",{arg.Lexema});\n");
                else if (arg.Tipo == "int")
                    gerador.ImprimeCodigo($"printf(\"%d\",{arg.Lexema});\n");
                else if (arg.Tipo == "double")
                    gerador.ImprimeCodigo($"printf(\"%lf\",{arg.Lexema});\n");
                else
                    gerador.ImprimeCodigo($"printf({arg.Lexema});\n");
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
                    ErroSemantico = true;
                }
            }

        }
        private void Regra_CMD(int linha, int coluna)
        {
            var pt_v = pilhaSemantica.Pop();
            var ld = pilhaSemantica.Pop();
            var rcb = pilhaSemantica.Pop();
            var id = pilhaSemantica.Pop();

            var cmd = new Token("CMD", "CMD", "Nulo");
            pilhaSemantica.Push(cmd);

            if (id.Tipo != "Nulo")
            {
                if (id.Tipo == ld.Tipo)
                {
                    gerador.ImprimeCodigo($"{id.Lexema}={ld.Lexema};\n");
                    gerador.QuebraDeLinha();
                }
                else
                {
                    log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Tipos diferentes para atribuição encontrados na linha {linha} e coluna {coluna}.");
                    ErroSemantico = true;
                }
            }
            else
            {
                log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Variável não declarada encontrada na linha {linha} e coluna {coluna}.");
                ErroSemantico = true;
            }

        }
        private void Regra_LD(Gramatica gramatica, int linha, int coluna)
        {

            if (gramatica.Sucessor.Contains("opm"))
            {
                var oprd2 = pilhaSemantica.Pop();
                var opm = pilhaSemantica.Pop();
                var oprd1 = pilhaSemantica.Pop();

                var t = $"T{variavelTX++}";

                gerador.ImprimeVariaveisTemporarias($"{ConversorTipo(oprd2)} {t};\n");
                var ld = new Token("LD", t, oprd2.Tipo);

                if (oprd2.Tipo == oprd1.Tipo && oprd2.Tipo != "literal")
                {
                    pilhaSemantica.Push(ld);
                    gerador.ImprimeCodigo($"{t}={oprd1.Lexema}{opm.Lexema}{oprd2.Lexema};\n");
                    gerador.QuebraDeLinha();
                }
                else
                {
                    log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Operandos com tipos incompatíveis encontrados na linha {linha} e coluna {coluna}.");
                    ErroSemantico = true;
                }
            }
            else
            {
                var oprd = pilhaSemantica.Pop();
                var ld = new Token("LD", oprd.Lexema, ConversorTipo(oprd));
                pilhaSemantica.Push(ld);
            }

        }
        private void Regra_OPRD(Gramatica gramatica, int linha, int coluna)
        {

            if (gramatica.Sucessor.Contains("id"))
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
                    ErroSemantico = true;
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

            gerador.ImprimeCodigo("}\n");
            gerador.QuebraDeLinha();
        }
        private void Regra_CAB()
        {
            var entao = pilhaSemantica.Pop();
            var fc_p = pilhaSemantica.Pop();
            var exp_r = pilhaSemantica.Pop();
            var ab_p = pilhaSemantica.Pop();
            var se = pilhaSemantica.Pop();

            var cab = new Token("CAB", "CAB", exp_r.Tipo);

            pilhaSemantica.Push(cab);

            gerador.ImprimeCodigo($"if({exp_r.Lexema})\n");
            gerador.QuebraDeLinha();

            gerador.ImprimeCodigo("{\n");
            // gerador.Tabulacao();
            // gerador.TabulacaoN();
            gerador.QuebraDeLinha();
        }
        private void Regra_EXP_R(int linha, int coluna)
        {
            var oprd = pilhaSemantica.Pop();
            var opr = pilhaSemantica.Pop();
            var oprd1 = pilhaSemantica.Pop();

            var tx = $"T{variavelTX++}";
            gerador.ImprimeVariaveisTemporarias($"{ConversorTipo(oprd)} {tx};\n");

            var exp_r = new Token("EXR_R", tx, oprd.Tipo);
            pilhaSemantica.Push(exp_r);

            if (oprd.Tipo == oprd1.Tipo)
            {
                gerador.ImprimeCodigo($"{tx}={oprd1.Lexema}{opr.Lexema}{oprd.Lexema};\n");
                gerador.QuebraDeLinha();
            }
            else
            {
                log.ImprimeErroSemantico($"ERRO SEMÂNTICO -  Operandos com tipos incompatíveis encontrados na linha {linha} e coluna {coluna}.");
                ErroSemantico = true;
            }

        }
        private void Regra_CP(Gramatica gramatica)
        {

            if (gramatica.Sucessor.Contains("fimse"))
            {
                var cp = new Token("CP", "CP", "Nulo");
                pilhaSemantica.Push(cp);
            }
            else
            {
                var token1 = pilhaSemantica.Pop();
                var token2 = pilhaSemantica.Pop();
                var cp = new Token("CP", "CP", token2.Tipo);
                pilhaSemantica.Push(cp);
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
        public void CriacaoArquivoOBJ()
        {
            gerador.GerarCodigoFinal();
        }

    }

}
