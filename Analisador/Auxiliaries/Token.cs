
namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Token
    {
        public string Classe { get; set; }
        public string Lexema { get; set; }
        public string Tipo { get; set; }

        public Token(string classe, string lexema, string tipo)
        {
            Classe = classe;
            Lexema = lexema;
            Tipo = tipo;
        }

        public override string ToString() => 
        $"Classe: {Classe.PadLeft(10)},\t Lexema: {Lexema.PadLeft(20)},\t Tipo: {Tipo.PadLeft(10)}";
    }
}
