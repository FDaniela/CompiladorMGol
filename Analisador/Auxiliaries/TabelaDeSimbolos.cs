using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class TabelaDeSimbolos
    {

        private Dictionary<string, Token> tokens = new Dictionary<string, Token>();

        public TabelaDeSimbolos()
        {

            // tokens = new Dictionary<string, Token>();
            PreencherTabelaComPalavrasReservadas();
            // var teste = new Token("teste", "teste", "teste");
            // InserirToken(teste);
            //Console.WriteLine(BuscarToken(teste));
            //BuscarToken(teste);
        }

        public void PreencherTabelaComPalavrasReservadas()
        {
            tokens.Add("inicio", new Token("inicio", "inicio", "inicio"));
            tokens.Add("varinicio", new Token("varinicio", "varinicio", "varinicio"));
            tokens.Add("varfim", new Token("varfim", "varfim", "varfim"));
            tokens.Add("escreva", new Token("escreva", "escreva", "escreva"));
            tokens.Add("leia", new Token("leia", "leia", "leia"));
            tokens.Add("se", new Token("se", "se", "se"));
            tokens.Add("entao", new Token("entao", "entao", "entao"));
            tokens.Add("fimse", new Token("fimse", "fimse", "fimse"));
            tokens.Add("repita", new Token("repita", "repita", "repita"));
            tokens.Add("fimrepita", new Token("fimrepita", "fimrepita", "fimrepita"));
            tokens.Add("fim", new Token("fim", "fim", "fim"));
            tokens.Add("inteiro", new Token("inteiro", "inteiro", "inteiro"));
            tokens.Add("literal", new Token("literal", "literal", "literal"));
            tokens.Add("real", new Token("real", "real", "real"));
            //tokens.Add("", new Token("", "", "");

        }

        public void InserirToken(Token t)
        {
            tokens.Add(t.Lexema, t);
        }

        public Token? BuscarToken(Token t)
        {
            if (tokens.ContainsKey(t.Lexema))
            {
                return tokens[t.Lexema];
                //return t;
            }
            return null;
        }

        public void AtualizarToken(Token t)
        {
            if (tokens.ContainsKey(t.Lexema))
            {
                tokens[t.Lexema] = t;
            }

        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var t in tokens)
            {
                sb.AppendLine(t.Value.ToString());
            }
            return sb.ToString();
        }

    }
}
