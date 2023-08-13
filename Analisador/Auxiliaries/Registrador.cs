using System.Text;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Registrador
    {

        private readonly StringBuilder linhaAcumulada;
        private readonly StringBuilder conteudoAcumulado;
        public string Linha => linhaAcumulada.ToString();

        public Registrador()
        {
            linhaAcumulada = new StringBuilder();
            conteudoAcumulado = new StringBuilder();
        }

        public void Registro(string msg) => linhaAcumulada.Append(" " + msg);

        public void RegistroQuebraDeLinha()
        {
            conteudoAcumulado.Append(linhaAcumulada.ToString().TrimStart());
            linhaAcumulada.Clear();
        }
        public override string ToString() => conteudoAcumulado.ToString();
    }
}