using System.Text;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Registrador
    {

        private readonly StringBuilder linhaAcumulada;
        private readonly StringBuilder conteudoAcumulado;
        int tabAcumulado=0;

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
        public void RegistroTab()
        {
            linhaAcumulada.Append("\t");
        }

        public void RemoverTab()
        {
              int lastIndex = linhaAcumulada.Length - 1;
            if (lastIndex >= 0 && linhaAcumulada[lastIndex] == '\t')
            {
                linhaAcumulada.Length = lastIndex;
            }
        }
        public override string ToString() => conteudoAcumulado.ToString();
    }
}