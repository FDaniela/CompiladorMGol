using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompiladorMGol.Analisador.Exceptions;


namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Gramatica
    {

        private List<Gramatica> regras = new();
        //Gramatica ra = new();
        private Arquivo arquivo = new();

        Gramatica? ra;

        public string? Identificador { get; set; }
        public string? LadoEsquerdo { get; set; }
        public string[]? LadoDireito { get; set; }

        public Gramatica()
        {

             //Producoes();
            //ImprimirProduçoesGramatica();
        }
        //Producoes();

        public void Producoes()
        {

            using (var stream = arquivo.LeituraAlfabeto())
            {
                int id = 1;
                while (!stream.EndOfStream)
                {
                    var linha = stream.ReadLine();
                    var valores = linha?.Split("->");

                    ra = new();
                    ra.Identificador = $"r{id++}";
                    ra.LadoEsquerdo = valores[0];
                    ra.LadoDireito = valores[1].Trim().Split(' ');

                    regras.Add(ra);
                }
            }

            //ImprimirProduçoesGramatica();

        }

        public Gramatica Reducoes(string acao)
        {

            foreach (var r in regras)
            {
                if (r.Identificador.Equals(acao.Trim()))
                    return r;
            }

            throw new ArgumentNullException("Regra não encontrada. Regra: " + acao);

        }

        public int ColunaDaReducao(string classe)
        {
           // switch (LadoEsquerdo.Trim())
            switch (classe)

            {
                case "inicio":
                    return 1;
                case "varinicio":
                    return 2;
                case "varfim":
                    return 3;
                case "leia":
                    return 4;
                case "escreva":
                    return 5;
                case "se":
                    return 6;
                case "entao":
                    return 7;
                case "fimse":
                    return 8;
                case "fimrepita":
                    return 9;
                case "fim":
                    return 10;
                case "repita":
                    return 11;
                case "inteiro":
                    return 12;
                case "real":
                    return 13;
                case "literal":
                    return 14;
                case "pt_v":
                    return 15;
                case "id":
                    return 16;
                case "vir":
                    return 17;
                case "lit":
                    return 18;
                case "num":
                    return 19;
                case "rcb":
                    return 20;
                case "opm":
                    return 21;
                case "ab_p":
                    return 22;
                case "fc_p":
                    return 23;
                case "opr":
                    return 24;
                case "$":
                    return 25;
                case "P":
                    return 26;
                case "V":
                    return 27;
                case "A":
                    return 28;
                case "LV":
                    return 29;
                case "D":
                    return 30;
                case "TIPO":
                    return 31;
                case "L":
                    return 32;
                case "ES":
                    return 33;
                case "ARG":
                    return 34;
                case "CMD":
                    return 35;
                case "LD":
                    return 36;
                case "OPRD":
                    return 37;
                case "CP":
                    return 38;
                case "COND":
                    return 39;
                case "CAB":
                    return 40;
                case "R":
                    return 41;
                case "CABR":
                    return 42;
                case "CPR":
                    return 43;
                case "EXP_R":
                    return 44;
                default:
                    return -1;
            }
        }

        public void ImprimirProduçoesGramatica()
        {
            foreach (var regra in regras)
            {

                Console.Write($"{regra.Identificador}\t");
                Console.Write($"{regra.LadoEsquerdo} -->");
                foreach (var valor in regra.LadoDireito)
                {
                    Console.Write($"  {valor}");
                }
                Console.WriteLine();
            }

        }

        // public override string ToString()
        // {

        //     // var sb = new StringBuilder();
        //     // foreach (var ra in regras)
        //     // {
        //     //     sb.Append(ra.ToString());
        //     //     sb.Append("\n");
        //     // }
        //     // return sb.ToString();


        //     var direito = LadoDireito.Aggregate((frase, palavra) => frase + " " + palavra);
        //     return $"{Identificador.PadLeft(3)} - {LadoEsquerdo.PadLeft(5)} --> {direito}";
        // }

    }
}