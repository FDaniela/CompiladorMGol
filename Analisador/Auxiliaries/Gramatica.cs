using CompiladorMGol.Analisador.Exceptions;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Gramatica
    {

        private List<Gramatica> listaDeRegras = new();
        private Arquivo arquivo = new();
        Gramatica? gramatica;
        public string? Identficacao { get; set; }
        public string? Antecessor { get; set; }
        public string[]? Sucessor { get; set; }

        public Gramatica()
        {
            //Producoes();
            //ImprimirProduçoesGLC();
        }

        public void Producoes()
        {

            using (var stream = arquivo.LeituraAlfabeto())
            {
                int id = 1;
                while (!stream.EndOfStream)
                {
                    var linha = stream.ReadLine();
                    var valores = linha?.Split("->");

                    gramatica = new();
                    gramatica.Identficacao = $"r{id++}";
                    gramatica.Antecessor = valores[0];
                    gramatica.Sucessor = valores[1].Trim().Split(' ');

                    listaDeRegras.Add(gramatica);
                }
            }

        }

        public Gramatica Producoes(string acao)
        {

            foreach (var r in listaDeRegras)
            {
                if (r.Identficacao.Equals(acao.Trim()))
                    return r;
            }

            throw new RegraExpection();

        }
        public int IdentificadorDaProducao(string classe)
        {
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
                case "eof":
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
        public void ImprimirProduçoesGLC()
        {
            foreach (var regra in listaDeRegras)
            {

                Console.Write($"{regra.Identficacao}\t");
                Console.Write($"{regra.Antecessor}->");
                foreach (var valor in regra.Sucessor)
                {
                    Console.Write($" {valor}");
                }
                Console.WriteLine();
            }

        }

        public override string ToString()
        {
            return $"{Antecessor}-> {Sucessor.Aggregate((frase, palavra) => frase + " " + palavra)}";
            //return $"{Identficacao}\t-\t{Antecessor}-> {Sucessor.Aggregate((frase, palavra) => frase + " " + palavra)}";

        }

    }
}